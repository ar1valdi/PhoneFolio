import { inject, Injectable } from "@angular/core";
import { ErrorResponse } from "../models/ErrorResponse";
import { Router } from "@angular/router";


@Injectable({
    providedIn: 'root'
})
export class RedirectService {
    private _router: Router = inject(Router);

    // redirect to error page with error as parameter
    redirectToError(error: ErrorResponse) : void {
        const errorJson = JSON.stringify(error)
        const encoded = encodeURIComponent(errorJson)
        this._router.navigate(['/error'], {
            queryParams: {error: encoded }
        });
    }
}