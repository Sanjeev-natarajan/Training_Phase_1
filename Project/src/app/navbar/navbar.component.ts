import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;
  userRole: string = '';

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.updateLoginStatus();

    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.updateLoginStatus();
      }
    });
  }

  updateLoginStatus() {
    const token = localStorage.getItem('token');
    const roleId = Number(localStorage.getItem('roleId'));

    this.isLoggedIn = !!token;

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
    localStorage.clear();
    this.isLoggedIn = false;
    this.userRole = '';
    this.router.navigate(['/login']);
  }
}
