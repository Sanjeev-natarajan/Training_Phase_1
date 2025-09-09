import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register-customer',
  templateUrl: './register-customer.component.html',
  styleUrls: ['./register-customer.component.css']
})
export class RegisterCustomerComponent {
  registerForm: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
    });
  }

  submit() {
    if (this.registerForm.valid) {
      this.authService.registerCustomer(this.registerForm.value).subscribe({
        next: res => {
          this.successMessage = 'Registered successfully!';
          this.registerForm.reset();
        },
        error: err => {
          this.errorMessage = err.error.message || 'Registration failed';
        }
      });
    }
  }
}
