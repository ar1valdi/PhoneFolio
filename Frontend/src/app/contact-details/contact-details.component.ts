import { Component, inject, Input } from '@angular/core';
import { Contact } from '../../models/Contact';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactService } from '../../services/ContactService';
import { CommonModule } from '@angular/common';
import { ErrorResponse } from '../../models/ErrorResponse';
import { RedirectService } from '../../services/RedirectService';

@Component({
  selector: 'app-contact-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact-details.component.html',
  styleUrl: './contact-details.component.css'
})
export class ContactDetailsComponent {
  @Input() contactData!: Contact;
  birthDateString: string = "";
  private _route: ActivatedRoute = inject(ActivatedRoute);
  private _redirectService: RedirectService = inject(RedirectService);
  private _contactService: ContactService = inject(ContactService);
  private _contactID: Number = 0;
  dataLoaded = false;

  async ngOnInit() {
    this._contactID = this._route.snapshot.params['id'];

    try {

      // fetch detials
      let contactData = await this._contactService.getContactDetails(this._contactID);
      this.contactData = contactData;
      this.birthDateString = this._contactService.extractDate(contactData.birthDate!.toString());
      this.dataLoaded = true;

    } catch (error) {

      // redirect to error 
      this._redirectService.redirectToError(ErrorResponse.Unexpected());
      
    }
  }
}
