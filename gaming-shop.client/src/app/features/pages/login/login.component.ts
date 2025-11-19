import { Component } from '@angular/core';
import { LoginModel } from '../../../models/LoginModel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      rememberMe: [false]
    });
  }
  get email() {
    return this.loginForm?.get('email');
  }
  get password() {
    return this.loginForm?.get('password');
  }
  showPassword: boolean = false;

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.loginForm?.invalid) {
      return;
    }

    const loginDto: LoginModel = this.loginForm?.value;

    this.authService.login(loginDto).subscribe({
      next: () => {
        this.router.navigate(['/home']);
      },
      error: err => {
        console.error(err);
        this.errorMessage = 'Login failed. Please check your credentials.';
      }
    });
  }
}
