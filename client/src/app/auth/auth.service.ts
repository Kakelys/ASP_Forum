import { User } from '../../shared/user.model';
import { AuthResponse } from '../../shared/auth-response.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { BehaviorSubject, map, tap, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private user = new BehaviorSubject<User>(null);
  public user$ = this.user.asObservable();

  private baseUrl = 'http://localhost:5100/api/v1/accounts';

  constructor(private http: HttpClient) {}

  signup(data: { username: string; password: string }) {
    return this.http.post<AuthResponse>(this.baseUrl + '/register', data).pipe(
      tap(data => this.handleAuth(data))
    );
  }

  login(data: { username: string; password: string }) {
    return this.http.post<AuthResponse>(this.baseUrl + '/login', data).pipe(
      tap(data => this.handleAuth(data))
    );
  }

  autoLogin() {
    const refreshToken = localStorage.getItem('refresh-token');
    if (!refreshToken)
      return;

    localStorage.removeItem('access-token');
    localStorage.removeItem('refresh-token');

    return this.http
      .post<AuthResponse>(this.baseUrl + '/auth', {refreshToken : refreshToken})
      .pipe(
        map((data) => {
          if (data) this.handleAuth(data);

          return data;
        })
      );
  }

  logout() {
    localStorage.removeItem('access-token');
    localStorage.removeItem('refresh-token');
    this.user.next(null);
  }

  private handleAuth(res: AuthResponse) {
    if(!res)
      return;

    localStorage.setItem('access-token', res.jwt.accessToken);
    localStorage.setItem('refresh-token', res.jwt.refreshToken);

    this.user.next(res.user);
  }
}
