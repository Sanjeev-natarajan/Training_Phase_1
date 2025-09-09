import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-storekeeper-dashboard',
  templateUrl: './storekeeper-dashboard.component.html',
  styleUrls: ['./storekeeper-dashboard.component.css'],
})
export class StorekeeperDashboardComponent implements OnInit {
  products: any[] = [];
  apiUrl = 'http://localhost:5191/api'; 
  selectedProduct: any = null;
  selectedFile: File | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadMyProducts();
  }

  private getAuthHeaders(): { headers: HttpHeaders } {
    const token = localStorage.getItem('token');
    return { headers: new HttpHeaders({ Authorization: `Bearer ${token}` }) };
  }

  loadMyProducts() {
    this.http
      .get(`${this.apiUrl}/store/myproducts`, this.getAuthHeaders())
      .subscribe({
        next: (res: any) => {
          this.products = res.map((p: any) => ({
            ...p,
            imageUrl: p.imageUrl?.startsWith('http')
              ? p.imageUrl
              : `http://localhost:5191${p.imageUrl}` ||
                'assets/default-product.png',
          }));
        },
        error: (err) => console.error('Error loading products', err),
      });
  }


  openEditModal(product: any) {
    this.selectedProduct = { ...product };
    this.selectedFile = null;

    const modalEl = document.getElementById('editProductModal');
    if (modalEl) {
      const modal = new (window as any).bootstrap.Modal(modalEl);
      modal.show();
    }
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  saveEdit() {
    if (!this.selectedProduct) return;

    const formData = new FormData();
    formData.append('name', this.selectedProduct.name);
    formData.append('brand', this.selectedProduct.brand);
    formData.append('price', this.selectedProduct.price.toString());
    formData.append('category', this.selectedProduct.category);
    formData.append('stock', this.selectedProduct.stock.toString());

    if (this.selectedFile) {
      formData.append('image', this.selectedFile, this.selectedFile.name);
    }

    const token = localStorage.getItem('token');
    const headers = { Authorization: `Bearer ${token}` };

    this.http
      .put(
        `${this.apiUrl}/Products/Update/${this.selectedProduct.productId}`,
        formData,
        { headers }
      )
      .subscribe({
        next: () => {
          this.loadMyProducts();
          this.cancelEdit();
        },
        error: (err) => console.error('Error updating product', err),
      });
  }

  cancelEdit() {
    const modalEl = document.getElementById('editProductModal');
    if (modalEl) {
      (window as any).bootstrap.Modal.getInstance(modalEl)?.hide();
    }
    this.selectedProduct = null;
    this.selectedFile = null;
  }

  deleteProduct(productId: number) {
    if (!confirm('Are you sure you want to delete this product?')) return;

    this.http
      .delete(
        `${this.apiUrl}/Products/Delete/${productId}`,
        this.getAuthHeaders()
      )
      .subscribe({
        next: () => this.loadMyProducts(),
        error: (err) => console.error('Error deleting product', err),
      });
  }
}
