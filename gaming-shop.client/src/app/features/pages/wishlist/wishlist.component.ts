import { Component, OnInit } from '@angular/core';
import { WishlistService } from '../../../services/wishlist.service';
import { AuthService } from '../../../services/auth.service';
import { WishlistItem } from '../../../models/WishlistItem';
import { CartService } from '../../../services/cart.service';
import { ProductModel } from '../../../models/ProductModel';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {

  wishlist: WishlistItem[] = [];
  token: string = '';
  userId: number | null = null;

  constructor(
    private wishlistService: WishlistService,
    private auth: AuthService,
    private cartService: CartService,
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
      this.loadWishlist();
    });
  }

  loadWishlist() {
    if (this.userId === null) return;
    this.wishlistService.getWishlist(this.userId, this.token)
      .subscribe(res => {
        this.wishlist = res.items; // WishlistItem[]
      });
  }

  deleteFromWishlist(productId: number) {
    if (this.userId === null) return;
    this.wishlistService.removeFromWishlist(this.userId, productId, this.token)
      .subscribe(() => {
        this.wishlist = this.wishlist.filter(p => p.productId !== productId);
      });
  }
  addToCart(item: WishlistItem) {
    if (!this.userId) return;

    this.cartService.addToCart(
      this.userId,
      {
        productId: item.productId,
        quantity: 1
      },
      this.token
    ).subscribe({
      next: () => {
        console.log('Product added to cart');

        // OPTIONAL: remove from wishlist after adding to cart
        this.deleteFromWishlist(item.productId);
      },
      error: err => {
        console.error('Failed to add product to cart', err);
      }
    });
  }

}
