import { Component, inject, OnInit } from '@angular/core';
import { CheckoutService } from '../../../core/services/checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  standalone: true,
  imports: [],
  templateUrl: './checkout-delivery.component.html',
  styleUrl: './checkout-delivery.component.scss'
})
export class CheckoutDeliveryComponent implements OnInit{
  checkoutService = inject(CheckoutService);

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe();
  }
}
