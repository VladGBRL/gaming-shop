import { Component, OnInit } from '@angular/core';
import { CartService } from '../../../services/cart.service';
import { AuthService } from '../../../services/auth.service';
import { CartDto } from '../../../models/CartModel';
import { PaymentService } from '../../../services/payment.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cart: CartDto | null = null;
  token: string = '';
  userId: number | null = null;

  constructor(
    private cartService: CartService,
    private auth: AuthService,
    private paymentService: PaymentService
  ) { }

  ngOnInit(): void {
    const token = this.auth.getToken();
    if (!token) {
      console.error('No JWT token found!');
      return;
    }
    this.token = token;

    this.auth.getUserId().subscribe(id => {
      this.userId = id;
      this.loadCart();
    });
  }

  loadCart() {
    if (!this.userId) return;

    this.cartService.getCart(this.userId, this.token)
      .subscribe({
        next: cart => this.cart = cart,
        error: err => console.error('Failed to load cart', err)
      });
  }

  removeFromCart(productId: number) {
    if (!this.userId) return;

    this.cartService.removeFromCart(this.userId, productId, this.token)
      .subscribe({
        next: () => this.loadCart(),
        error: err => console.error('Failed to remove item', err)
      });
  }

  clearCart() {
    if (!this.userId) return;

    this.cartService.clearCart(this.userId, this.token)
      .subscribe({
        next: () => this.cart = null,
        error: err => console.error('Failed to clear cart', err)
      });
  }
  pay() {
    if (!this.userId || !this.cart) return;

    this.paymentService.redirectToCheckout(this.userId, this.cart.cartId, this.token);
  }
}
