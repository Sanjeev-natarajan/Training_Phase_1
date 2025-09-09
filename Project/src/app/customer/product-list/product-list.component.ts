import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  loading = false;
  apiBaseUrl = 'http://localhost:5191';

  categories = ['All Categories','Groceries', 'Vegetables', 'Snacks', 'Dairy'];
  selectedCategory: string = 'All Categories';
  sortOptions = ['Price: Low to High', 'Price: High to Low'];
  selectedSort: string = 'Price: Low to High';

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  onAddClick(): void {
    alert('Please login');
  }

loadProducts(): void {
  this.loading = true;

  const category = this.selectedCategory !== 'All Categories' ? this.selectedCategory : undefined;

  this.productService.searchProducts(undefined, category).subscribe({
    next: (data) => {
      if (this.selectedSort === 'Price: Low to High') {
        data.sort((a, b) => a.price - b.price);
      } else if (this.selectedSort === 'Price: High to Low') {
        data.sort((a, b) => b.price - a.price);
      }

      this.products = data.slice(0, 12).map(p => ({
        ...p,
        imageUrl: this.getFullImageUrl(p.imageUrl)
      }));

      this.loading = false;
    },
    error: (err) => {
      console.error('Error fetching products', err);
      this.loading = false;
    }
  });
}

  onCategoryChange(category: string) {
    this.selectedCategory = category;
    this.loadProducts();
  }

  onSortChange(sort: string) {
    this.selectedSort = sort;
    this.loadProducts();
  }

  private getFullImageUrl(imageUrl: string): string {
    if (!imageUrl) return 'assets/images/no-image.png';

    imageUrl = imageUrl.trim();
    if (imageUrl.startsWith('http')) return imageUrl;
    if (!imageUrl.startsWith('/')) imageUrl = '/' + imageUrl;

    return `${this.apiBaseUrl}${imageUrl}`;
  }
}
