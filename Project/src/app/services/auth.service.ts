import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Login } from '../models/login.model';
import { RegisterCustomer } from '../models/register-customer.model';
import { RegisterShopkeeper } from '../models/register-shopkeeper.model';
import { RegisterDeliveryStaff } from '../models/register-delivery.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'http://localhost:5191/api'; 

  constructor(private http: HttpClient) { }

  login(data: Login): Observable<any> {
    return this.http.post(`${this.baseUrl}/Auth/login`, data);
  }

  registerCustomer(data: RegisterCustomer): Observable<any> {
    return this.http.post(`${this.baseUrl}/Auth/register/customer`, data);
  }

  registerShopkeeper(data: RegisterShopkeeper): Observable<any> {
    return this.http.post(`${this.baseUrl}/Auth/register/shopkeeper`, data);
  }

  registerDeliveryStaff(data: RegisterDeliveryStaff): Observable<any> {
    return this.http.post(`${this.baseUrl}/Auth/register/deliverystaff`, data);
  }
}
