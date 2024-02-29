import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { AuthService } from './auth/auth.service';
import { provideHttpClient } from '@angular/common/http';
import { provideToastr } from 'ngx-toastr';

import { provideAnimations } from '@angular/platform-browser/animations';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    AuthService,
    provideHttpClient(),
    provideAnimations(),
    provideToastr({
      preventDuplicates: true,
      timeOut: 2000,
      tapToDismiss: false,
      progressBar: true,
      countDuplicates: true,
    }),
  ],
};
