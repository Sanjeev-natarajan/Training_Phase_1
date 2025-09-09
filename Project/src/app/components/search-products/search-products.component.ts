import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-search-products',
  templateUrl: './search-products.component.html',
  styleUrls: ['./search-products.component.css']
})
export class SearchProductsComponent implements OnInit {
  products: Product[] = [];
  searchQuery = '';
  category = '';
  priceMin?: number;
  priceMax?: number;
  loading = false;
  apiBaseUrl = 'http://localhost:5191';

  constructor(
    private productService: ProductService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.productService.getAllProducts().subscribe({
      next: (data) => {
        this.products = data.slice(0, 12).map((p: Product) => ({
          ...p,
          imageUrl: this.getFullImageUrl(p.imageUrl)
        }));
        this.loading = false;
      },
      error: () => (this.loading = false)
    });
  }

  onSearch(): void {
    this.loading = true;
    this.productService.searchProducts(
      this.searchQuery,
      this.category,
      this.priceMin,
      this.priceMax
    ).subscribe({
      next: (data) => {
        console.log('Search results:', data);
        this.products = data.map((p: Product) => ({
          ...p,
          imageUrl: this.getFullImageUrl(p.imageUrl)
        }));
        this.loading = false;
      },
      error: () => (this.loading = false)
    });
  }

  addToCart(productId: number): void {
    this.cartService.addToCart(productId, 1).subscribe({
      next: () => {
        alert('✅ Product added to cart!');
      },
      error: (err) => {
        console.error('Error adding to cart', err);
        alert('❌ Failed to add product to cart');
      }
    });
  }

  private getFullImageUrl(imageUrl: string): string {
  if (!imageUrl) return '';

  imageUrl = imageUrl.trim();
  if (imageUrl.startsWith('http')) return imageUrl;

  if (!imageUrl.startsWith('/')) imageUrl = '/' + imageUrl;

  return `${this.apiBaseUrl}${imageUrl}`;
}

}
