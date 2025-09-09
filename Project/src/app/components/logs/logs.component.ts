import { Component, OnInit } from '@angular/core';
import { LogsService } from '../../services/logs.service';
import { AuditLog } from '../../models/audit-log.model';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.css']
})
export class LogsComponent implements OnInit {
  allLogs: AuditLog[] = []; 
  logs: AuditLog[] = [];    
  loading = true;
  error: string | null = null;
  selectedFilter: string = 'today'; 

  constructor(private logsService: LogsService) {}

  ngOnInit(): void {
    this.logsService.getLogs().subscribe({
      next: (data) => {
        this.allLogs = data;
        this.applyFilter(); 
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load logs';
        this.loading = false;
      }
    });
  }

  applyFilter() {
    const now = new Date();

    this.logs = this.allLogs.filter(log => {
      const logDate = new Date(log.createdAt);

      switch (this.selectedFilter) {
        case 'today':
          return logDate.toDateString() === now.toDateString();

        case 'week':
          const startOfWeek = new Date(now);
          startOfWeek.setDate(now.getDate() - now.getDay()); 
          startOfWeek.setHours(0, 0, 0, 0);
          return logDate >= startOfWeek && logDate <= now;

        case 'month':
          return logDate.getMonth() === now.getMonth() && logDate.getFullYear() === now.getFullYear();

        case 'all':
        default:
          return true;
      }
    });
  }
}
