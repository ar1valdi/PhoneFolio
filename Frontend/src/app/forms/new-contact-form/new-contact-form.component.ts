import { Component, inject, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Contact } from '../../../models/Contact';
import { RedirectService } from '../../../services/RedirectService';
import { ContactService } from '../../../services/ContactService';
import { ErrorResponse } from '../../../models/ErrorResponse';
import { CommonModule } from '@angular/common';
import { CategoryPickerComponent } from '../category-picker/category-picker.component';
import { AuthService } from '../../../services/AuthService';
import { FormErrorComponent } from '../form-error/form-error.component';


@Component({
  selector: 'app-new-contact-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, CategoryPickerComponent, FormErrorComponent],
  templateUrl: './new-contact-form.component.html',
  styleUrl: '../form.component.css'
})
export class NewContactFormComponent {
  contactForm = new FormGroup({
    name: new FormControl('', Validators.required),
    surname: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    phoneNumber: new FormControl('', Validators.required),
    birthDate: new FormControl('', Validators.required)
  });

  error = "";
  private _authService: AuthService = inject(AuthService);
  private _redirectService: RedirectService = inject(RedirectService);
  private _contactService: ContactService = inject(ContactService);
  private _router: Router = inject(Router);
  @ViewChild(CategoryPickerComponent) categoryPicker!: CategoryPickerComponent;

  async ngOnInit() {
    // ensure that user is logged
    try {
      await this._authService.getUsernameFromServer()
    } catch (error) {
      console.log("error");
      this._redirectService.redirectToError(error as ErrorResponse);
      return;
    }
  }

  getFormValidationErrors(form: FormGroup) : {control: string, error: string, value: ValidationErrors}[] {
    const errorMessages: {control: string, error: string, value: ValidationErrors}[] = [];

    Object.keys(form.controls).forEach(key => {
      const controlErrors = form.get(key)?.errors;
      if (controlErrors != null) {
        Object.keys(controlErrors).forEach(keyError => {
          errorMessages.push({
            control: key,
            error: keyError,
            value: controlErrors[keyError]
          });
        });
      }
    });
    return errorMessages;
  }

  onSubmitForm(): void {
    if (this.contactForm.invalid) {
      this.error = "Fill every field!";
      return;
    }

    // ensure birthDate is correct
    const birthDateString = this.contactForm.value.birthDate;
    if (!birthDateString) {
      this._redirectService.redirectToError(ErrorResponse.InvalidDate())
      return;
    }

    // map data
    let contact: Contact = {
      id: 0,
      name: this.contactForm.value.name ?? '',
      surname: this.contactForm.value.surname ?? '',
      email:  this.contactForm.value.email ?? '',
      password: this.contactForm.value.password ?? '',
      category: this.categoryPicker.getSelectedCategoryName() ?? '',
      subcategory: this.categoryPicker.getSelectedSubcategoryName() ?? '',
      phoneNumber: this.contactForm.value.phoneNumber ?? '',
      birthDate: new Date(birthDateString),
      username: undefined
    };

    // send add request
    this._contactService.addNewContact(contact).subscribe({
      next: (result) => {
        // if result is contact: redirect to home
        this._router.navigate(['']);
      },
      error: (error) => {
        this._redirectService.redirectToError(error);
      }
    });
  }
}
