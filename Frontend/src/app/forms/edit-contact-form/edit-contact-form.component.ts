import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ContactService } from '../../../services/ContactService';
import { RedirectService } from '../../../services/RedirectService';
import { Contact } from '../../../models/Contact';
import { ErrorResponse } from '../../../models/ErrorResponse';
import { CategoryPickerComponent } from '../category-picker/category-picker.component';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../../services/AuthService';
import { FormErrorComponent } from '../form-error/form-error.component';

@Component({
  selector: 'app-edit-contact-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, CategoryPickerComponent, FormErrorComponent],
  templateUrl: './edit-contact-form.component.html',
  styleUrl: '../form.component.css'
})
export class EditContactFormComponent {
  @ViewChild('formSpot', {read: ElementRef, static: true}) formSpot?: ElementRef;
  @ViewChild(CategoryPickerComponent) categoryPicker!: CategoryPickerComponent;
  private _contactService: ContactService = inject(ContactService);
  private _authService: AuthService = inject(AuthService);
  private _activateRoute: ActivatedRoute = inject(ActivatedRoute);
  private _redirectService: RedirectService = inject(RedirectService);
  private _router: Router = inject(Router);

  contactForm?: FormGroup;
  formVisible = false;
  contactId = 0;
  initName?: string = undefined;
  initSurname: string = "";
  defCategory?: string = undefined;
  defSubcategory?: string = undefined;
  error = "";

  async ngOnInit() {
    // ensure that user is logged
    try {
      await this._authService.getUsernameFromServer()
    } catch (error) {
      this._redirectService.redirectToError(error as ErrorResponse);
      return;
    }

    // fill form with contact details 
    const contactId: number = this._activateRoute.snapshot.params['id'];
    this.contactId = contactId;
    await this.loadContactDetailsToForm(contactId);

    // fill data for section title
    this.initName = this.contactForm?.value.name;
    this.initSurname = this.contactForm?.value.surname;
  }

  populateForm(contact: Contact) {
    // define contact form
    this.contactForm = new FormGroup({
      name: new FormControl(contact.name, Validators.required),
      surname: new FormControl(contact.surname, Validators.required),
      email: new FormControl(contact.email, Validators.required),
      password: new FormControl(contact.password, Validators.required), 
      phoneNumber: new FormControl(contact.phoneNumber, Validators.required),
      birthDate: new FormControl(this._contactService.extractDate(contact.birthDate?.toString()!), Validators.required)
    });
  }

  // populate and show form
  async loadContactDetailsToForm(id: number) : Promise<void> {
    try {
      const contact = await this._contactService.getContactDetails(id);   // fetch database
      this.populateForm(contact);                                         // populate form
      this.defCategory = contact.category;
      this.defSubcategory = contact.subcategory;
      this.formVisible = true;                                            // set html form to visible
    } catch (error) {
      this._redirectService.redirectToError(error as ErrorResponse);
    }
  }

  onSubmit(): void {
    if (!this.contactForm) {
      return;
    }

    if (this.contactForm.invalid) {
      this.error = "Fill every field";
      return;
    }

    // map data
    const birthDateString = this.contactForm.value.birthDate;
    let contact: Contact = {
      id: this.contactId,
      name: this.contactForm.value.name ?? '',
      surname: this.contactForm.value.surname ?? '',
      email: this.contactForm.value.email ?? '',
      password: this.contactForm.value.password ?? '',
      category: this.categoryPicker.getSelectedCategoryName() ?? '',
      subcategory: this.categoryPicker.getSelectedSubcategoryName() ?? '',
      phoneNumber: this.contactForm.value.phoneNumber ?? '',
      birthDate: birthDateString ? new Date(birthDateString) : new Date(0),
      username: ""
    };
    
    // send request
    this._contactService.editContact(contact).subscribe({
      next: (result) => {
        this._router.navigate(['/contact', this.contactId]);
      },
      error: (error) => {
        this._redirectService.redirectToError(error);
      }
    });
  }
}
