import { Injectable } from '@angular/core'
import { Contact } from '../models/Contact'
import { environment } from '../environments/environment';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse, HttpStatusCode } from '@angular/common/http';
import { catchError, map, Observable, of, throwError } from 'rxjs';
import { ErrorResponse } from '../models/ErrorResponse';
import { AddNewContactRequest } from '../requests/Contacts/AddNewContactRequest';
import { EditContactRequest } from '../requests/Contacts/EditContactRequest';

@Injectable({
    providedIn: 'root'
})
export class ContactRepository {
    constructor(private http: HttpClient) {}

    getContactList() : Observable<Contact[]> {
      // calls server and returns Contact[] or error as a response
        return this.http.get<{ contacts: Contact[] }>(
          environment.allContactsUrl,
          { withCredentials: true }
        ).pipe(
          map(response => response.contacts),
          catchError((error) => 
            throwError(() => ErrorResponse.fromHttp(error))
          )
        );
    }

    getContactDetails(id: Number) : Observable<Contact> {
      // calls server and returns details or error as a response
      return this.http.get<Contact>(
        `${environment.contactDetailsUrl}/${id}`,
        { withCredentials: true }
      ).pipe(
        catchError((error: HttpErrorResponse) => 
          throwError(() => ErrorResponse.fromHttp(error))
        )
      );
    }

    addContact(contact: Contact) : Observable<Contact> {
      const headers = new HttpHeaders({'Content-Type': 'application/json'}); 
      const contactRequest: AddNewContactRequest = AddNewContactRequest.fromContact(contact);
        
      // calls server with pre-fabricated request and returns contact or error as response
      return this.http.post<Contact>(environment.addContactUrl, 
          contactRequest, 
          { headers, observe: 'response', withCredentials: true }
        ).pipe(
          map(response => response.body as Contact),
          catchError((error: HttpErrorResponse) => 
            throwError(() => ErrorResponse.fromHttp(error))
          )
        );
    }
    
    removeContact(id: number) : Observable<number> {
      // send request to server, return 204 or error from response
      return this.http.delete<HttpResponse<any>>(
        `${environment.removeContactUrl}/${id}`, 
        { observe: 'response', withCredentials: true }
        ).pipe(
          map(response => response.status),
          catchError((error: HttpErrorResponse) => 
            throwError(() => ErrorResponse.fromHttp(error))
          )
        );
    }

    editContact(id: number, newData: Contact) {
      const request: EditContactRequest = EditContactRequest.fromContact(newData);

      // send request to server, return 204 or error from response
      return this.http.put<HttpResponse<any>>(
        `${environment.editContactUrl}/${id}`, 
        request,
        { observe: 'response', withCredentials: true })
      .pipe(
        map(response => response.status),
        catchError((error: HttpErrorResponse) => 
          throwError(() => ErrorResponse.fromHttp(error))
        )
      );
    }
}