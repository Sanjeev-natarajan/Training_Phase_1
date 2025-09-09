import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-delivery-dashboard',
  templateUrl: './delivery-dashboard.component.html',
  styleUrls: ['./delivery-dashboard.component.css']
})
export class DeliveryDashboardComponent implements OnInit {
  orders: any[] = [];
  apiUrl = 'http://localhost:5191/api';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  private getAuthHeaders(): { headers: HttpHeaders } {
    const token = localStorage.getItem('token');
    return { headers: new HttpHeaders({ Authorization: `Bearer ${token}` }) };
  }

  loadOrders() {
    this.http.get(`${this.apiUrl}/Orders/MyAssignedOrders`, this.getAuthHeaders())
      .subscribe({
        next: (res: any) => this.orders = res,
        error: err => console.error('Error fetching orders', err)
      });
  }

  updateStatus(orderId: number, status: string) {
    const statusBody = `"${status}"`; 
    this.http.put(`${this.apiUrl}/Orders/${orderId}/status`, statusBody, {
      headers: new HttpHeaders({
        'Authorization': `Bearer ${localStorage.getItem('token')}`,
        'Content-Type': 'application/json'
      })
    }).subscribe({
      next: () => {
        alert(`Order ${orderId} status updated to ${status}`);
        this.loadOrders();
      },
      error: err => console.error('Error updating order status', err)
    });
  }
}
