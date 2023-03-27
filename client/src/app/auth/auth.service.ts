import { Author } from '../../shared/author.model';
import { AuthResponse } from '../../shared/auth-response.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, map, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private user = new BehaviorSubject<Author>(null);
  public $user = this.user.asObservable();

  private baseUrl = 'http://localhost:5100/api/v1/accounts';

  constructor(private http: HttpClient) {}

  signup(data: { username: string; password: string }) {
    return this.http.post<AuthResponse>(this.baseUrl + '/register', data).pipe(
      tap((res) => {
        if (res) this.setAuth(res);
      })
    );
  }

  login(data: { username: string; password: string }) {
    return this.http.post<AuthResponse>(this.baseUrl + '/login', data).pipe(
      tap((res) => {
        if (res) this.setAuth(res);
      })
    );
  }

  autoLogin() {
    const refreshToken = localStorage.getItem('refresh-token');
    if (!refreshToken) return;

    localStorage.removeItem('access-token');
    localStorage.removeItem('refresh-token');

    let params = new HttpParams();
    params = params.append('refreshToken', refreshToken);

    return this.http
      .get<AuthResponse>(this.baseUrl + '/auth', { params: params })
      .pipe(
        map((data) => {
          if (data) this.setAuth(data);

          return data;
        })
      );
  }

  logout() {
    localStorage.removeItem('access-token');
    localStorage.removeItem('refresh-token');
    this.user.next(null);
  }

  private setAuth(auth: AuthResponse) {
    localStorage.setItem('access-token', auth.jwt.accessToken);
    localStorage.setItem('refresh-token', auth.jwt.refreshToken);
    this.user.next(auth.user);
  }
}
