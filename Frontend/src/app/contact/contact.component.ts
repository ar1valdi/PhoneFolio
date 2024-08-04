import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { Contact } from '../../models/Contact';
import { ContactService } from '../../services/ContactService';
import { ErrorResponse } from '../../models/ErrorResponse';
import { RedirectService } from '../../services/RedirectService';
import { AuthService } from '../../services/AuthService';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {
  @Input() contactData!: Contact;
  @Output() contactDeleted: EventEmitter<void> = new EventEmitter<void>();
  private _contactService: ContactService = inject(ContactService);
  private _redirectService: RedirectService = inject(RedirectService);
  authService: AuthService = inject(AuthService);

  onContactDelete() {
    this._contactService.removeContact(this.contactData.id).subscribe({
      next: (result) => {
        this.contactDeleted.emit(); // -> home.component.html
      },
      error: (error: ErrorResponse) => {
        this._redirectService.redirectToError(error);
      }
    });
  }
}
