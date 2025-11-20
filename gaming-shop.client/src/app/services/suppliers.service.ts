import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SupplierAddModel } from '../models/SupplierAddModel';
import { SupplierModel } from '../models/SupplierModel';

@Injectable({
  providedIn: 'root'
})
export class SuppliersService {
  private baseUrl = 'https://localhost:7239/api/Supplier';

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<SupplierModel[]>(this.baseUrl);
  }

  create(dto: SupplierAddModel) {
    return this.http.post<SupplierModel>(this.baseUrl, dto);
  }

  update(id: number, dto: SupplierAddModel) {
    return this.http.put<SupplierModel>(`${this.baseUrl}/${id}`, dto);
  }
  getById(id: number) {
    return this.http.get<SupplierModel>(`${this.baseUrl}/${id}`);
  } 
  delete(id: number) { return this.http.delete(`${this.baseUrl}/${id}`); }
}
