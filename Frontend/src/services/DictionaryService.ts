import { inject, Injectable } from "@angular/core";
import { DictionaryRepository } from "../repositories/DictionaryRepository";
import { catchError, lastValueFrom, Observable } from "rxjs";
import { Category } from "../models/Category";
import { Subcategory } from "../models/Subcategory";
import { CategoryPolicy } from "../models/CategoryPolicy";

@Injectable({
    providedIn: 'root'
})
export class DictionaryService {
    private _dictionaryRepository = inject(DictionaryRepository);
    private _redirectService: any;

    getAllCategories() : Observable<Category[]> {
        return this._dictionaryRepository.getCategories();
    }

    getCategorySubcategories(categoryName: string) : Observable<Subcategory[]> {
        return this._dictionaryRepository.getCategorySubcategories(categoryName);
    }
    
    async fetchCategories() : Promise<Category[]> {
        return new Promise(async (resolve, reject) => {
            try {

                // await for all categories
                const categories = await lastValueFrom(
                    this.getAllCategories().pipe(
                        catchError( (error) => { throw error; }) 
                    )
                );
                resolve(categories);

            } catch (error) {
                // redirect to error
                this._redirectService.redirectToError(error);
                reject(error);
            }
        })
    }

    async fetchSubcategories(categoryName: string) : Promise<Subcategory[]> {
        return new Promise(async (resolve, reject) => {
            try {

                // await for all subcategories
                const subcategories = await lastValueFrom(
                    this.getCategorySubcategories(categoryName).pipe(
                        catchError((error) => {throw error;})
                    )
                );
                resolve(subcategories);

            } catch (error) {
                // redirect to error
                this._redirectService.redirectToError(error);
                reject(error);
            }
        })
      }

    hasSubcategoriesAllowed(category: Category) {
        if (category.policy == CategoryPolicy.BLOCK_SUBCATEGORIES) {
            return false;
        }
        return true;
    }

    hasCustomSubcategoriesAllowed(category: Category) {
        if (category.policy == CategoryPolicy.CUSTOM_SUBCATEGORIES) {
            return true;
        }
        return false;
    }
}