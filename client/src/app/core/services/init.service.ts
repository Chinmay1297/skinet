import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { forkJoin, of } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private cartService = inject(CartService);
  private accountService = inject(AccountService);

  init() {
    const cartId = localStorage.getItem('cart_id');
    const cart$ = cartId ? this.cartService.getCart(cartId) : of(null);

    //fork join allows us to run multiple observables in parallel and wait for them to complete & emit latest values in an array
    return forkJoin({  
      cart: cart$,
      user: this.accountService.getUserInfo()
    });
  }
}
