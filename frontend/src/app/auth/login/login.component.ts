import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { NavigationExtras, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  loginFormModel: any = {
    username: '',
    password: '',
  };

  constructor(public authService: AuthService, public router: Router) {}

  login(form: any) {
    console.log(form);

    this.authService.login().subscribe(() => {
      if (this.authService.isLoggedIn) {
        const redirectUrl = '/dashboard';

        const navigationExtras: NavigationExtras = {
          queryParamsHandling: 'preserve',
          preserveFragment: true,
        };

        this.router.navigate([redirectUrl], navigationExtras);
      }
    });
  }

  logout() {
    this.authService.logOut();
  }
}
