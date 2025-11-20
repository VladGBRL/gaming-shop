import { Component, OnInit } from '@angular/core';
import { SupplierModel } from '../../../models/SupplierModel';
import { CategoryModel } from '../../../models/CategoryModel';
import { SuppliersService } from '../../../services/suppliers.service';
import { CategoryService } from '../../../services/category.service';

declare var bootstrap: any;

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  suppliers: SupplierModel[] = [];
  categories: CategoryModel[] = [];

  supplierForm = {
    id: null as number | null,
    supplierName: '',
    contact: '',
    address: ''
  };

  categoryForm = {
    id: null as number | null,
    categoryName: ''
  };

  constructor(
    private supplierService: SuppliersService,
    private categoryService: CategoryService
  ) { }

  ngOnInit(): void {
    this.loadSuppliers();
    this.loadCategories();
  }

  // LOADERS
  loadSuppliers() {
    this.supplierService.getAll().subscribe(res => this.suppliers = res);
  }

  loadCategories() {
    this.categoryService.getAll().subscribe(res => this.categories = res);
  }

  // SUPPLIER MODAL
  openSupplierModal(s?: SupplierModel) {
    this.supplierForm = s
      ? { id: s.supplierID, supplierName: s.supplierName, contact: s.contact, address: s.address }
      : { id: null, supplierName: '', contact: '', address: '' };

    const modal = new bootstrap.Modal(document.getElementById('supplierModal'));
    modal.show();
  }

  saveSupplier() {
    if (this.supplierForm.id) {
      this.supplierService
        .update(this.supplierForm.id, {
          supplierName: this.supplierForm.supplierName,
          contact: this.supplierForm.contact,
          address: this.supplierForm.address
        })
        .subscribe(() => this.loadSuppliers());
    } else {
      this.supplierService
        .create({
          supplierName: this.supplierForm.supplierName,
          contact: this.supplierForm.contact,
          address: this.supplierForm.address
        })
        .subscribe(() => this.loadSuppliers());
    }

    bootstrap.Modal.getInstance(document.getElementById('supplierModal')).hide();
  }

  deleteSupplier(id: number) {
    this.supplierService.delete(id).subscribe(() => this.loadSuppliers());
  }

  // CATEGORY MODAL
  openCategoryModal(c?: CategoryModel) {
    this.categoryForm = c
      ? { id: c.categoryID, categoryName: c.categoryName }
      : { id: null, categoryName: '' };

    const modal = new bootstrap.Modal(document.getElementById('categoryModal'));
    modal.show();
  }

  saveCategory() {
    if (this.categoryForm.id) {
      this.categoryService
        .update(this.categoryForm.id, { categoryName: this.categoryForm.categoryName })
        .subscribe(() => this.loadCategories());
    } else {
      this.categoryService
        .create({ categoryName: this.categoryForm.categoryName })
        .subscribe(() => this.loadCategories());
    }

    bootstrap.Modal.getInstance(document.getElementById('categoryModal')).hide();
  }

  deleteCategory(id: number) {
    this.categoryService.delete(id).subscribe(() => this.loadCategories());
  }
}
