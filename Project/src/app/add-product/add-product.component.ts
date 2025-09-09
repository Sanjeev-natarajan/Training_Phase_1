import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  product: any = {
    name: '',
    brand: '',
    price: 0,
    category: '',
    stock: 0
  };

  selectedFile: File | null = null;
  apiUrl = 'http://localhost:5191/api'; 

  constructor(private http: HttpClient, private router: Router) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  addProduct() {
    if (!this.product.name || !this.product.price) return;

    const formData = new FormData();
    formData.append('name', this.product.name);
    formData.append('brand', this.product.brand);
    formData.append('price', this.product.price);
    formData.append('category', this.product.category);
    formData.append('stock', this.product.stock);

    if (this.selectedFile) {
      formData.append('image', this.selectedFile, this.selectedFile.name);
    }

    const token = localStorage.getItem('token'); 
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    this.http.post(`${this.apiUrl}/Products/Add`, formData, { headers })
      .subscribe({
        next: () => {
          alert('Product added successfully!');
          this.router.navigate(['/storekeeper']); 
        },
        error: (err) => console.error('Failed to add product', err)
      });
  }
}
