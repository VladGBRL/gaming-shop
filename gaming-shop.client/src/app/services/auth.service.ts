import { Injectable, signal } from '@angular/core';
import { LoginModel } from '../models/LoginModel';
import { UserModel } from '../models/UserModel';
import { map, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RegisterModel } from '../models/RegisterModel';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7239/api/AccountManagement';
  currentUser = signal<UserModel | null>(null);
  private tokenKey = 'token';
  private userKey = 'user';

  constructor(private http: HttpClient) { }

  login(login: LoginModel): Observable<UserModel> {
    return this.http.post<UserModel>(this.apiUrl + '/Login', login).pipe(
      map(response => {
        if (response) {
          this.setUser(response, login.rememberMe);
        }
        return response;
      })
    );
  }

  register(register: RegisterModel): Observable<UserModel> {
    return this.http.post<UserModel>(this.apiUrl + '/Register', register).pipe(
      map(response => {
        if (response) {
          this.setUser(response, false);
        }
        return response;
      })
    );
  }
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
  isAuthenticated(): boolean {
    return !!(localStorage.getItem(this.tokenKey) || sessionStorage.getItem(this.tokenKey));
  }

  private setUser(user: UserModel, rememberMe: boolean): void {
    const storage = rememberMe ? localStorage : sessionStorage;
    storage.setItem(this.tokenKey, user.token);
    storage.setItem(this.userKey, JSON.stringify(user));
    this.currentUser.set(user);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey) || sessionStorage.getItem(this.tokenKey);
  }

  getUserFullName(): string | null {
    const user =
      localStorage.getItem(this.userKey) || sessionStorage.getItem(this.userKey);
    return user ? JSON.parse(user).username : null;
  }

  getUserId(): Observable<number> {
    const token = this.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<number>(`${this.apiUrl}/current-user-id`, { headers });
  }

  isUserAdmin(): boolean {
    const token = this.getToken();
    if (!token) {
      return false;
    }

    try {
      const decodedToken: any = jwtDecode(token);
      return decodedToken && decodedToken.role?.includes('Admin');
    } catch (error) {
      console.error('Error decoding token:', error);
      return false;
    }
  }
}
