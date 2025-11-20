import { Component, HostListener } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  showUserMenu = false;

  constructor(
    public auth: AuthService,
    private router: Router
  ) {}

  toggleUserMenu(event: MouseEvent) {
    event.stopPropagation();
    this.showUserMenu = !this.showUserMenu;
  }

  logout(event: MouseEvent) {
    event.preventDefault();
    this.auth.logout();   
    this.router.navigate(['/login']);
    this.showUserMenu = false;
  }

  goToLogin(event: MouseEvent) {
    event.stopPropagation();
    this.router.navigate(['/login']);
    this.showUserMenu = false;
  }

  goToRegister(event: MouseEvent) {
    event.stopPropagation();
    this.router.navigate(['/register']);
    this.showUserMenu = false;
  }

  @HostListener('document:click', ['$event.target'])
  onClickOutside(target: HTMLElement) {
    if (!target.closest('.user-section')) {
      this.showUserMenu = false;
    }
  }
  isAdmin(): boolean {
    return this.auth.isUserAdmin();
  }
}
