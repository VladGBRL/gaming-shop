import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(
    public auth: AuthService,
    private router: Router
  ) { }

  handleButtonClick(targetRoute: string) {
    if (!this.auth.currentUser()) {
      // If not logged in, go to login
      this.router.navigate(['/login']);
    } else {
      // If logged in, go to the target route
      this.router.navigate([targetRoute]);
    }
  }
}
