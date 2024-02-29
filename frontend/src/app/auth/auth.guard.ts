import { inject } from '@angular/core';
import { CanMatchFn, Router } from '@angular/router';
import { AuthService } from './auth.service';

export const authGuard: CanMatchFn = (route, segments) => {
  // AUTH GUARD TEMPORAL
  const router = inject(Router);
  const authService = inject(AuthService);

  if (authService.isLoggedIn) return true;

  return router.createUrlTree(['/login']);
};
