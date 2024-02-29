import { Component } from '@angular/core';
import {
  Router,
  RouterModule,
  RouterOutlet,
  NavigationExtras,
} from '@angular/router';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterOutlet, RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent {
  constructor(private authService: AuthService, private router: Router) {}

  logout() {
    this.authService.logOut();
    console.log('cliked');
    console.log(this.authService.isLoggedIn);
    if (this.authService.isLoggedIn == false) {
      const redirectUrl = '/login';

      const navigationExtras: NavigationExtras = {
        queryParamsHandling: 'preserve',
        preserveFragment: true,
      };

      this.router.navigate([redirectUrl], navigationExtras);
    }
  }
}
