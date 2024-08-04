import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../services/AuthService';
import { User } from '../../../models/User';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { RedirectService } from '../../../services/RedirectService';
import { ErrorResponse } from '../../../models/ErrorResponse';
import { CommonModule } from '@angular/common';
import { FormErrorComponent } from '../form-error/form-error.component';

@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormErrorComponent],
  templateUrl: './register-form.component.html',
  styleUrl: '../form.component.css'
})
export class RegisterFormComponent {
  private _authService = inject(AuthService);
  private _router = inject(Router);
  private _redirectService = inject(RedirectService);
  error = "";

  registerForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required)
  });

  onSubmitForm(): void {
    if (this.registerForm.invalid) {
      this.error = "Fill every field";
      return;
    }

    // map form to variables
    const username = this.registerForm.value.username as string;
    const password = this.registerForm.value.password as string;
    const confirmPassword = this.registerForm.value.confirmPassword as string;

    // check if password and confirm password fields are equal
    if (password != confirmPassword) {
      this._redirectService.redirectToError(ErrorResponse.RepeatPassword());
      return;
    }

    // create model
    const user: User = {
      username: username,
      password: password
    };

    // register user
    this._authService.registerUser(user).subscribe({
      next: (response) => {
          this._router.navigate(['/login']);
      },
      error: (error) => {
          this._redirectService.redirectToError(error);
      }
    })
  }
}
