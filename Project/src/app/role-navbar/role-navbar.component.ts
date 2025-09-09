import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-role-navbar',
  templateUrl: './role-navbar.component.html'
})
export class RoleNavbarComponent {
  @Input() role: string = '';

  constructor(private router: Router) {}

  logout() {
    localStorage.clear(); 
    this.router.navigate(['/Home']);
  }
}
