import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = 'http://localhost:5191/api/Products'; 

  constructor(private http: HttpClient) {}

  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/Getall`);
  }

  searchProducts(
    query?: string,
    category?: string,
    priceMin?: number,
    priceMax?: number
  ): Observable<Product[]> {
    let params = new HttpParams();

    if (query) params = params.set('query', query);
    if (category) params = params.set('category', category);
    if (priceMin !== undefined) params = params.set('priceMin', priceMin.toString());
    if (priceMax !== undefined) params = params.set('priceMax', priceMax.toString());

    return this.http.get<Product[]>(`${this.baseUrl}/search`, { params });
  }
}
