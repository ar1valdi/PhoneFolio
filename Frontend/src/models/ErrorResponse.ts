import { HttpErrorResponse } from "@angular/common/http";

export class ErrorResponse {
  code: number;
  title: string;
  errors: Record<string, string>;

  constructor(
    code: number,
    title: string,
    errors: Record<string, string>
  ) {
    this.code = code,
    this.title = title,
    this.errors = errors
  }

  static Unexpected(traceId: string = ''): ErrorResponse {
    return new ErrorResponse(500, 'Unexpected error occured', {
      "Client.Unexpected": "🤷‍♂️ Just try again"
    });
  }
  static LostConnection(traceId: string = ''): ErrorResponse {
    return new ErrorResponse(0, 'Lost connection to the server', {
      "Client.LostConnection": "We'll be back soon 😥"
    });
  }
  static NoCategoryFound(traceId: string = ''): ErrorResponse {
    return new ErrorResponse(400, "NoCategory", {
      "Client.NoCategory": "Entered category is not in database categories"
    });
  }
  static NoSubcategoryFound(traceId: string = ''): ErrorResponse {
    return new ErrorResponse(400, "NoSubcategory", {
      "Client.NoSubcategory": "Entered subcategory is not in database subcategories"
    });
  }
  static NoCategories(traceId: string = ''): ErrorResponse {
    return new ErrorResponse(500, "NoCategories", {
      "Client.NoCategories": "Server responded with empty category list"
    });
  }
  static AccessDenied(): ErrorResponse {
    return new ErrorResponse(401, "Access denied", 
      {"Client.AccessDenied": "You did not log in or your session might expired. Please log in again."}
    );
  }
  static InvalidDate() : ErrorResponse {
    return new ErrorResponse(403, "Form validation error", 
      {"Client.FormValidation": "Inserted date is invalid"}
    );
  }
  static RepeatPassword() : ErrorResponse {
    return new ErrorResponse(403, "Form validation error", 
      {"Client.FormValidation": "Password and repeated password do not match"}
    );
  }
  static fromHttp(httpError: HttpErrorResponse): ErrorResponse {
    return new ErrorResponse(httpError.status, httpError.error?.title ?? 'Error', httpError.error?.errors ?? {})  
  }
}