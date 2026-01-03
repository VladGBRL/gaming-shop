import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { ProductModel } from '../../../models/ProductModel';
import { AuthService } from '../../../services/auth.service';
import { SuppliersService } from '../../../services/suppliers.service';
import { CategoryService } from '../../../services/category.service';
import { WishlistService } from '../../../services/wishlist.service';

declare var bootstrap: any;

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products: ProductModel[] = [];
  originalProducts: ProductModel[] = [];

  allSuppliers: any[] = [];
  allCategories: any[] = [];

  filters = {
    search: '',
    supplier: '',
    category: '',
    minPrice: null as number | null,
    maxPrice: null as number | null
  };

  productForm = {
    id: null as number | null,
    name: '',
    description: '',
    price: 0,
    stock: 0,
    supplierID: 0,
    categoryID: 0
  };

  token: string = '';
  userId: number | null = null;

  constructor(
    private productService: ProductService,
    private supplierService: SuppliersService,
    private categoryService: CategoryService,
    private wishlistService: WishlistService,
    public auth: AuthService
  ) { }

  ngOnInit(): void {
    const token = this.auth.getToken();
    if (!token) {
      console.error('No JWT token found!');
      return;
    }
    this.token = token;

    // Load everything only after we have userId
    this.auth.getUserId().subscribe(id => {
      this.userId = id;
      this.loadProducts();
      this.loadSuppliers();
      this.loadCategories();
    });
  }

  // LOAD PRODUCTS
  loadProducts() {
    this.productService.getAll().subscribe(res => {
      // Initialize inWishlist to false
      this.originalProducts = res.map(p => ({ ...p, inWishlist: false }));
      this.products = [...this.originalProducts];
      this.loadWishlist();
    }, err => {
      console.error('Failed to load products', err);
    });
  }

  // LOAD WISHLIST AND MARK PRODUCTS
  loadWishlist() {
    if (!this.userId) return;

    this.wishlistService.getWishlist(this.userId, this.token).subscribe({
      next: wishlist => {
        const wishlistIds = wishlist.items.map(i => i.productId);
        this.products.forEach(p => p.inWishlist = wishlistIds.includes(p.productID));
      },
      error: err => console.error('Failed to load wishlist', err)
    });
  }

  // TOGGLE WISHLIST
  toggleWishlist(product: ProductModel) {
    if (!this.userId) return;

    if (product.inWishlist) {
      this.wishlistService.removeFromWishlist(this.userId, product.productID, this.token)
        .subscribe({
          next: () => product.inWishlist = false,
          error: err => console.error('Failed to remove from wishlist', err)
        });
    } else {
      this.wishlistService.addToWishlist(this.userId, product.productID, this.token)
        .subscribe({
          next: () => product.inWishlist = true,
          error: err => console.error('Failed to add to wishlist', err)
        });
    }
  }

  // LOAD SUPPLIERS (for filters)
  loadSuppliers() {
    this.supplierService.getAll().subscribe({
      next: res => this.allSuppliers = res,
      error: err => console.error('Failed to load suppliers', err)
    });
  }

  // LOAD CATEGORIES (for filters)
  loadCategories() {
    this.categoryService.getAll().subscribe({
      next: res => this.allCategories = res,
      error: err => console.error('Failed to load categories', err)
    });
  }

  // APPLY FILTERS
  applyFilters() {
    this.products = this.originalProducts.filter(p => {

      const matchesSearch =
        this.filters.search === '' ||
        p.name.toLowerCase().includes(this.filters.search.toLowerCase());

      const matchesSupplier =
        this.filters.supplier === '' ||
        p.supplierName === this.filters.supplier;

      const matchesCategory =
        this.filters.category === '' ||
        p.categoryName === this.filters.category;

      const matchesMinPrice =
        this.filters.minPrice === null ||
        p.price >= this.filters.minPrice;

      const matchesMaxPrice =
        this.filters.maxPrice === null ||
        p.price <= this.filters.maxPrice;

      return matchesSearch && matchesSupplier && matchesCategory && matchesMinPrice && matchesMaxPrice;
    });
  }

  // OPEN MODAL
  openModal(p?: ProductModel) {
    this.productForm = p
      ? {
        id: p.productID,
        name: p.name,
        description: p.description,
        price: p.price,
        stock: p.stock,
        supplierID: p.supplierID,
        categoryID: p.categoryID
      }
      : {
        id: null,
        name: '',
        description: '',
        price: 0,
        stock: 0,
        supplierID: 0,
        categoryID: 0
      };

    new bootstrap.Modal(document.getElementById('productModal')).show();
  }

  // SAVE PRODUCT
  saveProduct() {
    if (this.productForm.id) {
      this.productService
        .update(this.productForm.id, this.productForm)
        .subscribe(() => this.loadProducts(), err => console.error('Failed to update product', err));
    } else {
      this.productService
        .create(this.productForm)
        .subscribe(() => this.loadProducts(), err => console.error('Failed to create product', err));
    }

    bootstrap.Modal.getInstance(document.getElementById('productModal')).hide();
  }

  // DELETE PRODUCT
  deleteProduct(id: number) {
    this.productService.delete(id).subscribe(() => this.loadProducts(), err => console.error('Failed to delete product', err));
  }

  // CHECK ADMIN
  isAdmin(): boolean {
    return this.auth.isUserAdmin();
  }
}
