import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Payment } from '../../../models/PaymentDTO';
import { PaymentService } from '../../../services/payment.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})

export class HistoryComponent implements OnInit {
  payments: Payment[] = [];
  token: string = '';
  userId: number | null = null;
  loading: boolean = true;

  constructor(
    private paymentService: PaymentService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    const token = this.authService.getToken();
    if (!token) {
      console.error('No JWT token found!');
      this.loading = false;
      return;
    }
    this.token = token;

    this.authService.getUserId().subscribe(id => {
      this.userId = id;
      this.loadPayments();
    });
  }

  loadPayments() {
    if (!this.userId) return;

    this.paymentService.getAllPayments(this.token)
      .subscribe({
        next: payments => {
          this.payments = payments.filter(p => p.userId === this.userId);
          this.loading = false;
        },
        error: err => {
          console.error('Failed to load payments', err);
          this.loading = false;
        }
      });
  }
}
