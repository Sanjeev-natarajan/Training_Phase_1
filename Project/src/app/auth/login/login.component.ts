import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

submit() {
  if (this.loginForm.valid) {
    this.authService.login(this.loginForm.value).subscribe({
      next: res => {
        console.log('Login success', res);

        localStorage.setItem('token', res.token);
        localStorage.setItem('userId', res.userId);
        localStorage.setItem('roleId', res.roleId);
        localStorage.setItem('email', res.email);
        localStorage.setItem('name', res.name);

        const roleRoutes: { [key: number]: string } = {
          1: '/super-admin-dashboard',   
          2: '/admin-dashboard',        
          3: '/storekeeper',   
          4: '/delivery-dashboard',      
          5: '/customer'      
        };

        const roleId = Number(res.roleId);  
        const route = roleRoutes[roleId] || '/'; 
        console.log('Navigating to:', route); 
        this.router.navigate([route]);
      },
      error: err => {
        this.errorMessage = err.error?.message || 'Login failed';
      }
    });
  }
}

}
