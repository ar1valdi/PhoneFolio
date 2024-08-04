import { Component, inject } from '@angular/core';
import { AuthService } from '../../../services/AuthService';
import { Router } from '@angular/router';
import { RedirectService } from '../../../services/RedirectService';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { User } from '../../../models/User';
import { CommonModule } from '@angular/common';
import { FormErrorComponent } from "../form-error/form-error.component";

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormErrorComponent],
  templateUrl: './login-form.component.html',
  styleUrl: '../form.component.css'
})
export class LoginFormComponent {
  private _authService = inject(AuthService);
  private _router = inject(Router);
  private _redirectService = inject(RedirectService);
  error = "";

  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  onSubmitForm(): void {
    if (this.loginForm.invalid) {
      this.error = "Fill every field!";
      return;
    }

    // map form to variablwa
    const username = this.loginForm.value.username as string;
    const password = this.loginForm.value.password as string;

    // create mdoel
    const user: User = {
      username: username,
      password: password
    };

    // login user
    this._authService.login(user).subscribe({
      next: (response) => {
        this._router.navigate(['/']);
      },
      error: (error) => {
        this._redirectService.redirectToError(error);
      }
    })
  }
}
