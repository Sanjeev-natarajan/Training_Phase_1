import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';

@Component({
  selector: 'app-super-admin-dashboard',
  templateUrl: './super-admin-dashboard.component.html',
  styleUrls: ['./super-admin-dashboard.component.css']
})
export class SuperAdminDashboardComponent implements OnInit {
  users: any[] = [];
  auditLogs: any[] = [];
  loadingUsers = true;
  loadingLogs = true;
  errorMessage = '';

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.fetchUsers();
    this.fetchAuditLogs();
  }

  fetchUsers() {
    this.loadingUsers = true;
    this.adminService.getAllUsers().subscribe({
      next: (res) => {
        this.users = res;
        this.loadingUsers = false;
      },
      error: () => {
        this.errorMessage = 'Failed to load users';
        this.loadingUsers = false;
      }
    });
  }

  fetchAuditLogs() {
    this.loadingLogs = true;
    this.adminService.getAuditLogs().subscribe({
      next: (res) => {
        this.auditLogs = res;
        this.loadingLogs = false;
      },
      error: () => {
        this.errorMessage = 'Failed to load audit logs';
        this.loadingLogs = false;
      }
    });
  }
}
