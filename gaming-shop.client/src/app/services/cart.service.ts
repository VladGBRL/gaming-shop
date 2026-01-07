import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CartDto } from '../models/CartModel';
import { AddToCartDto } from '../models/AddToCartDTO';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private apiUrl = 'https://localhost:7239/api/Cart';

  constructor(private http: HttpClient) { }

  addToCart(userId: number, dto: AddToCartDto, token: string): Observable<void> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.post<void>(
      `${this.apiUrl}/${userId}`,
      dto,
      { headers }
    );
  }

  removeFromCart(userId: number, productId: number, token: string): Observable<void> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.delete<void>(
      `${this.apiUrl}/${userId}/${productId}`,
      { headers }
    );
  }

  getCart(userId: number, token: string): Observable<CartDto> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.get<CartDto>(
      `${this.apiUrl}/${userId}`,
      { headers }
    );
  }

  clearCart(userId: number, token: string): Observable<void> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.delete<void>(
      `${this.apiUrl}/${userId}`,
      { headers }
    );
  }
}
