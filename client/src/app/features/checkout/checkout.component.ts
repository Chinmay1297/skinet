import { Component, EventEmitter, inject, OnDestroy, OnInit } from '@angular/core';
import { MatStepperModule } from '@angular/material/stepper';
import { OrderSummaryComponent } from "../../shared/components/order-summary/order-summary.component";
import { MatButton } from '@angular/material/button';
import { MatCheckboxChange, MatCheckboxModule } from '@angular/material/checkbox';
import { RouterLink } from '@angular/router';
import { StripeService } from '../../core/services/stripe.service';
import { StripeAddressElement } from '@stripe/stripe-js';
import { SnackbarService } from '../../core/services/snackbar.service';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { Address } from '../../shared/models/user';
import { firstValueFrom } from 'rxjs';
import { AccountService } from '../../core/services/account.service';
import { CheckoutDeliveryComponent } from "./checkout-delivery/checkout-delivery.component";

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [
    OrderSummaryComponent,
    MatStepperModule,
    MatButton,
    RouterLink,
    MatCheckboxModule,
    CheckoutDeliveryComponent
],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutComponent implements OnInit, OnDestroy {
  private stripeService = inject(StripeService);
  private snackbar = inject(SnackbarService);
  private accountService = inject(AccountService);
  addressElement?: StripeAddressElement;
  saveAddress = false;

  async ngOnInit() {
    try {
      this.addressElement = await this.stripeService.createAddressElement();
      this.addressElement.mount("#address-element");
    }
    catch (error: any) {
      this.snackbar.error(error.message);
    }
  }

  async onStepChange(event: StepperSelectionEvent) {
    if (event.selectedIndex === 1) {
      if (this.saveAddress) {
        const address = await this.getAddressFromStripeAddress();
        address && firstValueFrom(this.accountService.updateAddress(address));
      }
    }
  }
  private async getAddressFromStripeAddress(): Promise<Address | null> {
    const result = await this.addressElement?.getValue();
    const address = result?.value.address;

    if (address) {
      return {
        line1: address.line1,
        line2: address.line2 || "",
        city: address.city,
        country: address.country,
        state: address.state,
        postalCode: address.postal_code,
      }
    }
    return null;
  }

  onSaveAddressCheckboxChange(event: MatCheckboxChange) {
    this.saveAddress = event.checked;
  }

  ngOnDestroy() {
    this.stripeService.disposeElements();
  }
}
