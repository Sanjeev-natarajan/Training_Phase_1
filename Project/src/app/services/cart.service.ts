import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CartItem, CartResponse } from '../models/cart.model'

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5191/api/cart';

  constructor(private http: HttpClient) {}

private getAuthHeaders(): HttpHeaders {
  const token = localStorage.getItem('token');
  if (!token) {
    console.error('No token found in localStorage');
  }
  return new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: `Bearer ${token}`
  });
}


addToCart(productId: number, quantity: number): Observable<any> {
  return this.http.post(
    `${this.apiUrl}/add`,
    { productId, quantity },
    {
      headers: this.getAuthHeaders(),
      responseType: 'text' as 'json'   
    }
  );
}

getMyCart(): Observable<CartResponse> {
  return this.http.get<CartResponse>(`${this.apiUrl}/mycart`, {
    headers: this.getAuthHeaders()
  });
}



  updateCartItem(cartItemId: number, quantity: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/update/${cartItemId}`, { quantity }, { headers: this.getAuthHeaders() });
  }

  removeFromCart(cartItemId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/remove/${cartItemId}`, { headers: this.getAuthHeaders() });
  }
}
