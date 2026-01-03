import { WishlistItem } from "./WishlistItem";

export interface Wishlist {
  userId: number;
  items: WishlistItem[];
}
