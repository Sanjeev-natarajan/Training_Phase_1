import { Component, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-product-edit-dialog',
  templateUrl: './product-edit-dialog.component.html'
})
export class ProductEditDialogComponent {
  @Input() product: any = {}; 
  @Output() updated = new EventEmitter<boolean>(); 

  selectedFile: File | null = null;
  apiUrl = 'http://localhost:5191/api';

  constructor(private http: HttpClient) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  save() {
    const formData = new FormData();
    formData.append('name', this.product.name);
    formData.append('brand', this.product.brand);
    formData.append('price', this.product.price.toString());
    formData.append('category', this.product.category);
    formData.append('stock', this.product.stock.toString());

    if (this.selectedFile) {
      formData.append('image', this.selectedFile, this.selectedFile.name);
    }

    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });

    this.http.put(`${this.apiUrl}/Products/Update/${this.product.productId}`, formData, { headers })
      .subscribe({
        next: () => {
          this.updated.emit(true);
          this.closeModal();
        },
        error: (err) => console.error('Error updating product', err)
      });
  }

  cancel() {
    this.closeModal();
  }

  closeModal() {
    const modalEl = document.getElementById('editProductModal');
    if (modalEl) {
      (window as any).bootstrap.Modal.getInstance(modalEl)?.hide();
    }
  }
}
