import { CartItemDto } from "./CartItem";

export interface CartDto {
  cartId: number;
  items: CartItemDto[];
  totalPrice: number;
}
