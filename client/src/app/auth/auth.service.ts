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
  public $user = this.user.asObservable();

  private baseUrl = 'http://localhost:5100/api/v1/accounts';

  constructor(private http: HttpClient) {}

  signup(data: { username: string; password: string }) {
    return this.http.post<AuthResponse>(this.baseUrl + '/register', data).pipe(
      tap(data => this.handleAuth(data)),
      catchError(this.handleAuthCatchError)
    );
  }

  login(data: { username: string; password: string }) {
    return this.http.post<AuthResponse>(this.baseUrl + '/login', data).pipe(
      tap(data => this.handleAuth(data)),
      catchError(this.handleAuthCatchError)
    );
  }

  autoLogin() {
    const refreshToken = localStorage.getItem('refresh-token');
    if (!refreshToken)
      return;

    localStorage.removeItem('access-token');
    localStorage.removeItem('refresh-token');

    let params = new HttpParams();
    params = params.append('refreshToken', refreshToken);

    return this.http
      .get<AuthResponse>(this.baseUrl + '/auth', { params: params })
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

  private handleAuthCatchError(err) {
    if(err instanceof HttpErrorResponse) {
      if(err.status === 500)
        return throwError("Internal server error");

      if(err.error){
        const errMessage = err.error;
        return throwError(errMessage);
      }

      if(err.error.errors){
        return throwError(err.error.errors.join('\n'));
      }
    }

    return throwError("Something went wrong");
  }
}
