import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CategoryModel } from '../models/CategoryModel';
import { CategoryAddModel } from '../models/CategoryAddModel';

@Injectable({ providedIn: 'root' })
export class CategoryService {

  private baseUrl = 'https://localhost:7239/api/Category';

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<CategoryModel[]>(this.baseUrl);
  }

  getById(id: number) {
    return this.http.get<CategoryModel>(`${this.baseUrl}/${id}`);
  }

  create(dto: CategoryAddModel) {
    return this.http.post<CategoryModel>(this.baseUrl, dto);
  }

  update(id: number, dto: CategoryAddModel) {
    return this.http.put<CategoryModel>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
