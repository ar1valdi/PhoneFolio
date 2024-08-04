import { Component, inject, Input, SimpleChanges } from '@angular/core';
import { DictionaryService } from '../../../services/DictionaryService';
import { RedirectService } from '../../../services/RedirectService';
import { ErrorResponse } from '../../../models/ErrorResponse';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Category } from '../../../models/Category';
import { Subcategory } from '../../../models/Subcategory';

@Component({
  selector: 'app-category-picker',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './category-picker.component.html',
  styleUrl: '../form.component.css'
})
export class CategoryPickerComponent {
  dictionaryService: DictionaryService = inject(DictionaryService);
  private _redirectService: RedirectService = inject(RedirectService)
  @Input() defaultCategory = "";
  @Input() defaultSubcategory = "";
  
  selectedCategory: Category = {name: "", policy: 0};
  selectedSubcategory: Subcategory = {name: ""};
  possibleCategories: Category[] = [];
  possibleSubcategories: Subcategory[] = [];

  async ngOnInit() {
    // fetch categories from server
    const categories = await this.dictionaryService.fetchCategories();
    if (categories!.length == 0) {
      this._redirectService.redirectToError(ErrorResponse.NoCategories());
      return;
    }

    // assign default category
    this.possibleCategories = categories;
    const newCategory = this.defaultCategory == "" ? this.possibleCategories[0].name : this.defaultCategory;
    this.assignSelectedCategory(newCategory)

    // fetch subcategories from server
    const subcategories = await this.dictionaryService.fetchSubcategories(this.selectedCategory.name)
    
    // assign default subcategory
    this.possibleSubcategories = subcategories;
    this.assignSelectedSubcategory(this.defaultSubcategory);
  }

  async onCategoryChange() {
    try {
      // set selectedCategory variable
      this.assignSelectedCategory(this.selectedCategory.name);

      // fetch subcategories
      const subcategories = await this.dictionaryService.fetchSubcategories(this.selectedCategory.name)
      this.possibleSubcategories = subcategories;

    } catch (error) {
      this._redirectService.redirectToError(ErrorResponse.Unexpected());
    }
  }

  getSelectedCategoryName() : string {
    return this.selectedCategory.name;
  }

  getSelectedSubcategoryName() : string {
    return this.selectedSubcategory.name;
  }

  private assignSelectedCategory(name: string) : void {
    if (name == "") { return; }
    
    // get actual category object
    const findCategoryResult = this.possibleCategories.find((c) => c.name == name);

    // if not found: redirect to error
    if (!findCategoryResult) {
      this._redirectService.redirectToError(ErrorResponse.NoCategoryFound());
      return;
    }

    // assign selected category to found object
    this.selectedCategory = {name: findCategoryResult.name, policy: findCategoryResult.policy};
  }

  private assignSelectedSubcategory(name: string) : void {
    if (name == "") { return; }

    // get actual subcategory object
    const findSubcategoryResult = this.possibleSubcategories.find((c) => c.name == name)

    // if not found: redirect to error
    if (!findSubcategoryResult) {
      this._redirectService.redirectToError(ErrorResponse.NoSubcategoryFound());
      return;
    }

    // assign selected subcategory to found object
    this.selectedSubcategory = {name: findSubcategoryResult.name };
  }
}
