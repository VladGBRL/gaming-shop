
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Wishlist } from '../models/Wishlist';


@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private apiUrl = 'https://localhost:7239/api/Wishlist';

  constructor(private http: HttpClient) { }

  addToWishlist(userId: number, productId: number, token: string): Observable<void> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.post<void>(`${this.apiUrl}/${userId}/${productId}`, null, { headers });
  }

  removeFromWishlist(userId: number, productId: number, token: string): Observable<void> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.delete<void>(`${this.apiUrl}/${userId}/${productId}`, { headers });
  }

  getWishlist(userId: number, token: string): Observable<Wishlist> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return this.http.get<Wishlist>(`${this.apiUrl}/${userId}`, { headers });
  }
}
