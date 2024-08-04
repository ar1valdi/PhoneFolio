import { Component, inject } from '@angular/core';
import { ErrorResponse } from '../../models/ErrorResponse';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/AuthService';

@Component({
  selector: 'app-error',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './error.component.html',
  styleUrl: './error.component.css'
})
export class ErrorComponent {
  error: ErrorResponse | undefined = undefined;
  moreErrorsKeys: string[] = [];
  private _authService: AuthService = inject(AuthService);

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const encodedError = params['error'];
      if (encodedError) {
        try {
          const decodedErrorJson = decodeURIComponent(encodedError);
          this.error = JSON.parse(decodedErrorJson) as ErrorResponse;
          this.checkForUnauthorized();
          this.moreErrorsKeys = this.getMoreErrors();
        } catch (e) {
          console.error('Failed to parse json errors:', e);
        }
      }
    });
  }

  async checkForUnauthorized() : Promise<void> {
    if (!this.error) return;

    // handle undescribed unathorized to show detailed error
    if (this.error.code == 401 && this.error.title == "Error") {
      this.error = ErrorResponse.AccessDenied();
      await this._authService.getUsernameFromServer();
    }

    // handle no connection error
    else if (this.error.code == 0) {
      this.error = ErrorResponse.LostConnection();
    }
  }

  getMoreErrors(): string[] {
    return this.error && this.error.errors ? Object.keys(this.error.errors) : [];
  }
}
