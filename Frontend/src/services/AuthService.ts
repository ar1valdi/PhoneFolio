import { Injectable, inject } from '@angular/core'
import { BehaviorSubject, catchError, lastValueFrom, map, Observable, throwError } from 'rxjs';
import { ErrorResponse } from '../models/ErrorResponse';
import { AuthRepository } from '../repositories/AuthRepository';
import { User } from '../models/User';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private _authRepostiory = inject(AuthRepository);
    public authState = new BehaviorSubject<boolean>(false);
    public loggedUsername: string = "";

    registerUser(user: User) : Observable<number> {
        return this._authRepostiory.addUser(user);
    }

    // sends login request and sets logged vars
    login(user: User) : Observable<number> {
        return this._authRepostiory.getToken(user).pipe(
            map(response => {
                // if succedded: set variables and emit to logged in
                this.loggedUsername = user.username;
                this.authState.next(true);
                return response;
            })
        );
    }

    // sends token removal and sets logged vars
    logout() : Observable<number> {
        return this._authRepostiory.removeToken().pipe(
            map(response => {
                // if succedded: set variables and emit to logged out
                this.loggedUsername = "";
                this.authState.next(false);
                return response;
            })
        );
    }

    // gets logged user username, throws error if not logged 
    getUsernameFromServer() : Promise<string> {
        return new Promise(async (resolve, reject) => {
            try {
                const username = await lastValueFrom(this._authRepostiory.getCurrentUsername());
                this.loggedUsername = username;
                this.authState.next(true);
                resolve(username)
            } catch (error) {
                // if unauthorized recieved: set frontend logged vars and emit logged out
                if (error instanceof ErrorResponse && error.code == 401) {
                    this.authState.next(false);
                    this.loggedUsername = "";
                }
                // rethrow error
                reject(error);
            }
        }) 
    }

    // returns if user is logged based on previous fetches
    isLoggedNoFetch() : boolean {
        return this.authState.getValue();
    }
}