import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, map, Observable, throwError } from "rxjs";
import { environment } from "../environments/environment";
import { ErrorResponse } from "../models/ErrorResponse";
import { Category } from "../models/Category";
import { Subcategory } from "../models/Subcategory";

@Injectable({
    providedIn: 'root'
})
export class DictionaryRepository {
    constructor(private http: HttpClient) {}
    
    getCategories() : Observable<Category[]> {
        return this.http.get<{categories: Category[]}>(
            environment.categoriesUrl,
            {
              withCredentials: true
            }
        )
        .pipe(
            map(response => response.categories as Category[]),
            catchError((error: HttpErrorResponse) =>
                throwError(() => ErrorResponse.fromHttp(error))
            )
        );
    }

    getCategorySubcategories(categoryName: string) : Observable<Subcategory[]> {
        return this.http.get<{subcategories: Subcategory[]}>(
            `${environment.subcategoriesUrl}/${categoryName}`,
            {
              withCredentials: true
            }
        ).pipe(
            map(response => response.subcategories as Subcategory[]),
            catchError((error: HttpErrorResponse) =>
                throwError(() => ErrorResponse.fromHttp(error))
            )
        );
    }
}