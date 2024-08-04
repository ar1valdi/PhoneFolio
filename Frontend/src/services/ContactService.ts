import { Injectable, inject } from '@angular/core'
import { Contact } from '../models/Contact'
import { ContactRepository } from '../repositories/ContactRepository';
import { catchError, lastValueFrom, Observable } from 'rxjs';
import { ErrorResponse } from '../models/ErrorResponse';
import { RedirectService } from './RedirectService';

@Injectable({
    providedIn: 'root'
})
export class ContactService {
    private _repository: ContactRepository = inject(ContactRepository);
    private _redirectService: RedirectService = inject(RedirectService);

    getContactList() : Promise<Contact[]> {
        return new Promise(async (resolve, reject) => {
            try {
                
                // await for all categories
                const contacts = await lastValueFrom(
                    this._repository.getContactList().pipe(
                        catchError( (error) => {throw error;} )
                    )
                );
                resolve (contacts);

            } catch (error) {
                // redirect to error
                this._redirectService.redirectToError(error as ErrorResponse);
                reject(error);
            }

        });
    }

    getContactDetails(id: Number) : Promise<Contact> {
        return new Promise(async (resolve, reject) => {
            try {
                
                // await for all categories
                const contacts = await lastValueFrom(
                    this._repository.getContactDetails(id).pipe(
                        catchError( (error) => {throw error;} )
                    )
                );
                resolve (contacts);

            } catch (error) {
                // redirect to error
                this._redirectService.redirectToError(error as ErrorResponse);
                reject(error);
            }
        });
    }

    addNewContact(contact: Contact) : Observable<Contact>{
        return this._repository.addContact(contact);
    }

    removeContact(id: number) : Observable<number> {
        return this._repository.removeContact(id);
    }

    editContact(contact: Contact) : Observable<number> {
        return this._repository.editContact(contact.id, contact);
    }

    // converts iso to date only
    extractDate(isoDateTime: string): string {
      const indexOfT = isoDateTime.indexOf('T');
      if (indexOfT === -1) { 
        return isoDateTime; 
      }
      return isoDateTime.substring(0, indexOfT);
    }
}