import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register-shopkeeper',
  templateUrl: './register-shopkeeper.component.html',
  styleUrls: ['./register-shopkeeper.component.css']
})
export class RegisterShopkeeperComponent {
  registerForm: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      shopName: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
      vehicleType: [''],   
      licenseNumber: [''], 
      roleId: [3]          
    });
  }

  submit() {
    if (this.registerForm.valid) {
      this.authService.registerShopkeeper(this.registerForm.value).subscribe({
        next: () => {
          this.successMessage = 'Shopkeeper registered successfully!';
          this.errorMessage = '';
          this.registerForm.reset({
            roleId: 3
          });
        },
        error: err => {
          this.successMessage = '';
          this.errorMessage = err.error?.message || 'Registration failed';
        }
      });
    }
  }
}
