import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductModel } from '../models/ProductModel';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:7239/api/Product';

  constructor(private http: HttpClient) { }

  getAll(): Observable<ProductModel[]> {
    return this.http.get<ProductModel[]>(this.apiUrl);
  }

  getById(id: number): Observable<ProductModel> {
    return this.http.get<ProductModel>(`${this.apiUrl}/${id}`);
  }

  create(dto: any): Observable<ProductModel> {
    return this.http.post<ProductModel>(this.apiUrl, dto);
  }

  update(id: number, dto: any): Observable<ProductModel> {
    return this.http.put<ProductModel>(`${this.apiUrl}/${id}`, dto);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
