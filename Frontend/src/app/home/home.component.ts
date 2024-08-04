import { Component, inject, ViewChild } from '@angular/core';
import { ContactComponent } from "../contact/contact.component";
import { Contact } from '../../models/Contact';
import { ContactService } from '../../services/ContactService';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ErrorResponse } from '../../models/ErrorResponse';
import { RedirectService } from '../../services/RedirectService';
import { AuthService } from '../../services/AuthService';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ContactComponent, CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  contactList: Contact[] = [];
  private _contactService: ContactService = inject(ContactService);
  private _redirectService: RedirectService = inject(RedirectService);
  authService: AuthService = inject(AuthService);
  dataLoaded = false;

  ngOnInit() {
    this.fetchContactList();
  }

  async fetchContactList() {
    try {
      this.contactList = await this._contactService.getContactList();
      this.dataLoaded = true;
    } catch (error) {
      this._redirectService.redirectToError(error as ErrorResponse);
    }
  }
}
