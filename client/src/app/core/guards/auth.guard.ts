import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { of, map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);

  if (accountService.currentUser()) {
    return of(true);
  }
  else {
    return accountService.getAuthState()        //we dont need to subscribe here, because auth guard will do that for us
      .pipe(
        map(auth => {
          if (auth.isAuthenticated) {
            return true;
          }
          else {
            router.navigate(['/account/login'], { queryParams: { returnUrl: state.url } });     //this returnUrl will help us to go back to that url which we were trying to visit (once we're logged in)
            return false;
          }
        })
      )
  }
};
