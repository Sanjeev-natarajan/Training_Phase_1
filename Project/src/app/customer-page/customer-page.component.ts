import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { CartService } from '../services/cart.service';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-customer-page',
  templateUrl: './customer-page.component.html',
  styleUrls: ['./customer-page.component.css']
})
export class CustomerPageComponent implements OnInit {
  role = 'Customer'; 
  products: Product[] = [];
  apiBaseUrl = 'http://localhost:5191'; 
  loading = false;

  selectedCategory = 'All Categories';
  selectedSort = 'Sort by';

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
      next: (data: Product[]) => {
        this.products = data.map((p: Product) => ({
          ...p,
          imageUrl: this.getFullImageUrl(p.imageUrl) 
        }));
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching products:', err);
        this.loading = false;
      }
    });
  }

  addToCart(product: Product): void {
    this.cartService.addToCart(product.productId, 1).subscribe({
      next: (res) => {
        alert(`${product.name} added to cart!`);
      },
      error: (err) => {
        console.error('Error adding to cart:', err);
        alert('Failed to add product to cart');
      }
    });
  }

  private getFullImageUrl(imageUrl: string): string {
    if (!imageUrl) return 'assets/images/no-image.png'; 

    imageUrl = imageUrl.trim();

    if (imageUrl.startsWith('http')) return imageUrl;

    if (!imageUrl.startsWith('/')) imageUrl = '/' + imageUrl;

    return `${this.apiBaseUrl}${imageUrl}`;
  }

  get filteredProducts(): Product[] {
    let filtered = [...this.products];

    if (this.selectedCategory !== 'All Categories') {
      filtered = filtered.filter(p => p.category === this.selectedCategory);
    }

    if (this.selectedSort === 'Price: Low to High') {
      filtered.sort((a, b) => a.price - b.price);
    } else if (this.selectedSort === 'Price: High to Low') {
      filtered.sort((a, b) => b.price - a.price);
    }

    return filtered.slice(0, 12); 
  }
}
