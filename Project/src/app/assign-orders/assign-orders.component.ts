import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-assign-orders',
  templateUrl: './assign-orders.component.html',
  styleUrls: ['./assign-orders.component.css']
})
export class AssignOrdersComponent implements OnInit {
  orders: any[] = [];
  apiUrl = 'http://localhost:5191/api';
  filterStatus: string = 'pending'; 

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  private getAuthHeaders(): { headers: HttpHeaders } {
    const token = localStorage.getItem('token');
    return { headers: new HttpHeaders({ Authorization: `Bearer ${token}` }) };
  }

  loadOrders() {
    this.http.get(`${this.apiUrl}/Orders/status/${this.filterStatus}`, this.getAuthHeaders())
      .subscribe({
        next: (res: any) => this.orders = res,
        error: err => console.error('Error fetching orders', err)
      });
  }

  setFilter(status: string) {
    this.filterStatus = status;
    this.loadOrders();
  }

  assignOrder(orderId: number) {
    this.http.put(`${this.apiUrl}/Orders/${orderId}/assign/10`, {}, this.getAuthHeaders())
      .subscribe({
        next: () => {
          alert('Order assigned to delivery person');
          this.loadOrders();
        },
        error: err => console.error('Error assigning order', err)
      });
  }
}
