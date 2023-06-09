import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<Object>> {
    const accessToken = localStorage.getItem('access-token');
    if (!accessToken) return next.handle(req);

    const modifiedReq = req.clone({
      headers: req.headers.append('Authorization', 'Bearer ' + accessToken),
    });

    return next.handle(modifiedReq).pipe(
      catchError((err) => {

        // If it's Unauthorized try to refresh tokens and resend request
        if(err instanceof HttpErrorResponse && err.status === 401) {
          if(!localStorage.getItem('refresh-token')) {
            return throwError(err);
          }

          var tmp = this.authService.autoLogin().subscribe({
            next: _ => {return next.handle(modifiedReq)},
            error: _ => {return throwError(new Error('Failed to login'))}
          });
        }

        return throwError(err);
      })
    );
  }
}
