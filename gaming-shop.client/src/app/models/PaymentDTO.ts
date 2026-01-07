import { PaymentItem } from './PaymentItemDTO';

export interface Payment {
  id: number;
  userId: number;
  status: string;
  totalAmount: number;
  createdAt: string;
  stripeSessionId?: string;
  items: PaymentItem[];
}
