import { Injectable } from '@angular/core';
import { Observable, delay, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  isLoggedIn: any = 0;
  redirectUrl: string | null = null;

  // AUTH SERVICE TEMPORAL

  login(): Observable<boolean> {
    return of(true).pipe(
      delay(200),
      tap(() => (this.isLoggedIn = true))
    );
  }

  logOut(): void {
    this.isLoggedIn = false;
  }

  constructor() {}
}
