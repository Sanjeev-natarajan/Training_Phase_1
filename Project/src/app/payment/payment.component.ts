import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  orderId!: number;
  orderDetails: any;
  paymentStatus: string = '';

  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.orderId = +params['orderId'];
      if (this.orderId) {
        this.loadOrderDetails();
      }
    });
  }


  loadOrderDetails() {
    const token = localStorage.getItem('token'); 
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });

    this.http.get(`http://localhost:5191/api/Orders/${this.orderId}`, { headers })
      .subscribe({
        next: (res) => this.orderDetails = res,
        error: (err) => console.error('Failed to load order details', err)
      });
  }
  goBack() {
    this.router.navigate(['/search-products']); 
  }

makePayment() {
  if (!this.orderDetails) return;

  const token = localStorage.getItem('token');
  const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });

  const paymentData = {
    orderId: this.orderId,
    amount: this.orderDetails.totalAmount || 0
  };

  this.http.post<any>('http://localhost:5191/api/Payments/create', paymentData, { headers })
    .subscribe({
      next: (createRes) => {
        console.log('Payment created:', createRes);

        this.http.post<any>(`http://localhost:5191/api/Payments/pay/${this.orderId}`, {}, { headers })
          .subscribe({
            next: (payRes) => {
              console.log('Payment confirmed:', payRes);
              this.paymentStatus = '✅ Payment successful! (Completed)';
              this.orderDetails.paymentStatus = payRes.status;
            },
            error: (err) => {
              console.error('Payment confirmation failed', err);
              this.paymentStatus = '❌ Payment confirmation failed!';
            }
          });
      },
      error: (err) => {
        console.error('Payment creation failed', err);
        this.paymentStatus = '❌ Payment failed!';
      }
    });
}

}
