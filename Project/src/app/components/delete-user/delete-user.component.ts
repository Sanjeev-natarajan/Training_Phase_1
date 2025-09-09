import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent {
  apiUrl = 'http://localhost:5191/api/admin/users';
  userId: number | null = null;
  message: string = '';

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): { headers: HttpHeaders } {
    const token = localStorage.getItem('token');
    return { headers: new HttpHeaders({ Authorization: `Bearer ${token}` }) };
  }

  deleteUser() {
    if (!this.userId) {
      this.message = 'Please enter a user ID';
      return;
    }

    this.http.delete(`${this.apiUrl}/${this.userId}`, this.getAuthHeaders())
      .subscribe({
        next: () => {
          this.message = `User ${this.userId} deactivated successfully`;
          this.userId = null;
        },
        error: err => {
          console.error('Error deactivating user:', err);
          this.message = 'Error deactivating user';
        }
      });
  }
}
