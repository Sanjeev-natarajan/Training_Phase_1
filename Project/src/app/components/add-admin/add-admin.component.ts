import { Component } from '@angular/core';
import { AdminService } from '../../services/admin.service';

@Component({
  selector: 'app-add-admin',
  templateUrl: './add-admin.component.html',
  styleUrls: ['./add-admin.component.css']
})
export class AddAdminComponent {
  adminData = {
    name: '',
    email: '',
    password: '',
    phoneNumber: '',
    address: '',
    city: '',
    roleId: 2  
  };

  successMessage = '';
  errorMessage = '';

  constructor(private adminService: AdminService) {}

  onSubmit() {
    this.adminService.addAdmin(this.adminData).subscribe({
      next: () => {
        this.successMessage = 'Admin added successfully!';
        this.errorMessage = '';
        this.adminData = { name: '', email: '', password: '', phoneNumber: '', address: '', city: '', roleId: 2 };
      },
      error: () => {
        this.errorMessage = 'Failed to add admin';
        this.successMessage = '';
      }
    });
  }
}
