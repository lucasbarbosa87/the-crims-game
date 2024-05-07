import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { BaseUrlInterceptor } from './core/services/interceptors/base-url.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    // provideHttpClient(withInterceptors([new BaseUrlInterceptor()]))
  ]
};
