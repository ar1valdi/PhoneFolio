import { Component, HostListener, inject } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from '../services/AuthService';
import { RedirectService } from '../services/RedirectService';
import { CommonModule } from '@angular/common';
import { ErrorResponse } from '../models/ErrorResponse';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  isLogged: boolean = false;
  username = "";
  title = 'Frontend';
  private _authService: AuthService = inject(AuthService); 
  private _redirectService: RedirectService = inject(RedirectService);
  private _router: Router = inject(Router);

  constructor() { 
    this.setAuthVariables();
  }

  async setAuthVariables() : Promise<void> {
    // if valid token is present: set app in logged mode
    try {
      const username = await this._authService.getUsernameFromServer();
      this.username = username;
    } catch {}

    this.isLogged = this._authService.authState.getValue();
    this._authService.authState.subscribe({
      next: (isLogged) => {
        this.username = this._authService.loggedUsername;
        this.isLogged = isLogged;
      }
    })
  }

  onLogoutClicked() {
    this._authService.logout().subscribe({
      complete: () => {this._router.navigate(['/'])},
      error: (error: ErrorResponse) => {
        this._redirectService.redirectToError(error);
      }
    });
  }
}
