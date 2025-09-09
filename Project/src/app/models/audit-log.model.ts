export interface AuditLog {
  id: number;
  action: string;
  userId: number;
  createdAt: string;
  details: string;
}
