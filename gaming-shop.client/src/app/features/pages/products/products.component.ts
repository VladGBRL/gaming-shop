import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { ProductModel } from '../../../models/ProductModel';
import { AuthService } from '../../../services/auth.service';
import { SuppliersService } from '../../../services/suppliers.service';
import { CategoryService } from '../../../services/category.service';


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

  constructor(
    private productService: ProductService,
    private supplierService: SuppliersService,
    private categoryService: CategoryService,
    public auth: AuthService
  ) { }

  ngOnInit(): void {
    this.loadProducts();
    this.loadSuppliers();
    this.loadCategories();
  }

  // LOAD PRODUCTS
  loadProducts() {
    this.productService.getAll().subscribe(res => {
      this.originalProducts = res;
      this.products = res;
    });
  }

  // LOAD SUPPLIERS (for filters)
  loadSuppliers() {
    this.supplierService.getAll().subscribe(res => {
      this.allSuppliers = res;
    });
  }

  // LOAD CATEGORIES (for filters)
  loadCategories() {
    this.categoryService.getAll().subscribe(res => {
      this.allCategories = res;
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
        .subscribe(() => this.loadProducts());
    } else {
      this.productService
        .create(this.productForm)
        .subscribe(() => this.loadProducts());
    }

    bootstrap.Modal.getInstance(document.getElementById('productModal')).hide();
  }

  // DELETE PRODUCT
  deleteProduct(id: number) {
    this.productService.delete(id).subscribe(() => this.loadProducts());
  }

  // CHECK ADMIN
  isAdmin(): boolean {
    return this.auth.isUserAdmin();
  }
}
