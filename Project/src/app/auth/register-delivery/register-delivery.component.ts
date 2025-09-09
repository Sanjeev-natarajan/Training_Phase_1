import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register-delivery',
  templateUrl: './register-delivery.component.html',
  styleUrls: ['./register-delivery.component.css']
})
export class RegisterDeliveryStaffComponent {
  registerForm: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      vehicleType: ['', Validators.required],
      licenseNumber: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
      shopName: [''],   
      roleId: [2]       
    });
  }

  submit() {
    if (this.registerForm.valid) {
      this.authService.registerDeliveryStaff(this.registerForm.value).subscribe({
        next: res => {
          this.successMessage = 'Delivery Staff Registered successfully!';
          this.errorMessage = '';
          this.registerForm.reset({
            shopName: '',
            roleId: 2
          });
        },
        error: err => {
          this.errorMessage = err.error.message || 'Registration failed';
          this.successMessage = '';
        }
      });
    }
  }
}
