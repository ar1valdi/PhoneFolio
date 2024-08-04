import { Injectable } from "@angular/core";
import { User } from "../models/User";
import { AddNewUserRequest } from "../requests/Users/AddNewUserRequest";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { environment } from "../environments/environment";
import { catchError, map, Observable, of, throwError } from "rxjs";
import { ErrorResponse } from "../models/ErrorResponse";
import { LoginRequest } from "../requests/Users/LoginRequest";


@Injectable ({
    providedIn: 'root'
})
export class AuthRepository {
    constructor(private http: HttpClient) {}

    addUser(newUser: User) : Observable<number> {
        // prepeare request
        const headers = new HttpHeaders({'Content-Type': 'application/json'}); 
        const request: AddNewUserRequest = AddNewUserRequest.fromUser(newUser);
        
        // send to server and catch error
        return this.http.post(
            environment.registerUrl,
            request,
            {
                headers, 
                observe: 'response', 
                withCredentials: true
            })
        .pipe(
            map(response => response.status),
            catchError((error: HttpErrorResponse) =>
                throwError(() => ErrorResponse.fromHttp(error))
            )
        );
    }

    getToken(userData: User) : Observable<number> {
        // prepeare request
        const headers = new HttpHeaders({'Content-Type': 'application/json'});
        const request = LoginRequest.fromUser(userData);

        // send login credentials to server, expected response is token
        return this.http.post(
            environment.loginUrl,
            request,
            {
                headers, 
                observe: 'response', 
                withCredentials: true})
        .pipe(
            map(response => response.status),
            catchError((error: HttpErrorResponse) => 
                throwError(() => ErrorResponse.fromHttp(error))
            )
        );
    }

    removeToken(): Observable<number> {
        // send token removel request (http-only, has to be deleted on server)
        return this.http.delete(
            environment.logoutUrl,
            {
                observe: 'response', 
                withCredentials: true
            })
        .pipe(
            map(response => response.status),
            catchError((error: HttpErrorResponse) =>
                throwError(() => ErrorResponse.fromHttp(error))
            )
        );
    }

    getCurrentUsername(): Observable<string> {
        // returns username if logged
        return this.http.get<string>(
            environment.getUsernameUrl,
            {
                observe: 'response', 
                withCredentials: true,
                responseType: 'text' as 'json',
            })
        .pipe(
            map(response => {
                return response.body as string
            }),
            catchError((error) => {
                return throwError(() => ErrorResponse.fromHttp(error))
            })
        );
    }
}