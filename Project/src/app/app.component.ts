import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'shopping-app';

  isLoggedIn = false;
  userRole: string = '';

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.updateLoginStatus();

    // Optional: update login status on route changes (helps with logout)
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.updateLoginStatus();
      }
    });
  }

  updateLoginStatus() {
    const token = localStorage.getItem('token');
    const roleId = Number(localStorage.getItem('roleId'));

    this.isLoggedIn = !!token; // true if token exists

    // Map roleId to role name
    const roles: { [key: number]: string } = {
      1: 'Super Admin',
      2: 'Admin',
      3: 'StoreKeeper',
      4: 'DeliveryPerson',
      5: 'Customer'
    };

    this.userRole = roles[roleId] || '';
  }

  logout() {
    // Clear local storage
    localStorage.clear();
    this.isLoggedIn = false;
    this.userRole = '';
    this.router.navigate(['/login']);
  }
}
