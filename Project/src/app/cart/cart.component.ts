import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { CartItem, CartResponse } from '../models/cart.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  totalAmount: number = 0;
  loading = false;

  constructor(
    private cartService: CartService,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.loading = true;
    this.cartService.getMyCart().subscribe({
      next: (data: CartResponse) => {
        this.cartItems = data.cartItems;
        this.totalAmount = data.totalAmount;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching cart', err);
        this.loading = false;
      }
    });
  }

  updateQuantity(item: CartItem, change: number): void {
    const newQty = item.quantity + change;
    if (newQty < 1) return;

    this.cartService.updateCartItem(item.cartItemId, newQty).subscribe({
      next: () => {
        item.quantity = newQty;
        item.total = item.price * newQty;
        this.totalAmount = this.getTotal();
      },
      error: (err) => console.error('Update failed', err)
    });
  }

  getTotal(): number {
    return this.cartItems.reduce((sum, item) => sum + item.total, 0);
  }

  removeItem(item: CartItem): void {
    if (!confirm('Are you sure you want to remove this item?')) return;

    this.cartService.removeFromCart(item.cartItemId).subscribe({
      next: () => {
        this.cartItems = this.cartItems.filter(ci => ci.cartItemId !== item.cartItemId);
        this.totalAmount = this.getTotal();
      },
      error: (err) => console.error('Remove failed', err)
    });
  }

makePayment() {
  if (!this.cartItems.length) return;

  const orderPayload = {
    storekeeperId: 14, 
    items: this.cartItems.map(item => ({
      productId: item.productId,
      quantity: item.quantity
    }))
  };

  const token = localStorage.getItem('token'); 

  this.http.post('http://localhost:5191/api/Orders/PlaceOrder', orderPayload, {
    headers: { Authorization: `Bearer ${token}` }
  })
  .subscribe({
    next: (res: any) => {
      console.log('Order placed successfully', res);


      this.cartItems = [];
      this.totalAmount = 0;

      this.router.navigate(['/payment'], { queryParams: { orderId: res.orderId } });
    },
    error: (err) => {
      console.error('Failed to place order', err);
    }
  });
}



}
