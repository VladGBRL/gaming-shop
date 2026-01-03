export interface ProductModel {
  productID: number;
  name: string;
  description: string;
  price: number;
  stock: number;

  supplierID: number;
  supplierName: string;

  categoryID: number;
  categoryName: string;

  inWishlist?: boolean;
}
