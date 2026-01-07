import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Payment } from '../models/PaymentDTO';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private readonly baseUrl = 'https://localhost:7239/api/stripe';

  constructor(private http: HttpClient) { }

  createCheckoutSession(userId: number, cartId: number, token: string): Observable<{ url: string }> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    const params = new HttpParams()
      .set('userId', userId)
      .set('cartId', cartId);

    return this.http.post<{ url: string }>(
      `${this.baseUrl}/create-checkout-session`,
      null,
      { params, headers },
    );
  }

  redirectToCheckout(userId: number, cartId: number, token: string): void {

    this.createCheckoutSession(userId, cartId, token)
      .subscribe({
        next: response => {
          window.location.href = response.url;
        },
        error: err => {
          console.error('Stripe checkout error', err);
          alert('Payment failed. Please try again.');
        }
      });
  }
  getAllPayments(token: string): Observable<Payment[]> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    return this.http.get<Payment[]>(`${this.baseUrl}/all`, { headers });
  }
}
