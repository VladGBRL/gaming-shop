import { Component, OnInit } from '@angular/core';
import { WishlistService } from '../../../services/wishlist.service';
import { AuthService } from '../../../services/auth.service';
import { WishlistItem } from '../../../models/WishlistItem';

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
    private auth: AuthService
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
}
