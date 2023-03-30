import { AuthService } from './auth.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  public isLogin = true;
  public isLoading = false;

  public authFailed = false;
  public errorMessage: string;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  onSwitchMode() {
    this.isLogin = !this.isLogin;
  }

  onSubmit(form: NgForm) {
    this.isLoading = true;

    let authObs;

    if(this.isLogin) {
      authObs = this.authService.login(form.value);
    }
    else{
      authObs = this.authService.signup(form.value);
    }

    authObs.subscribe({
      next: _ => {
        this.isLoading = false;
        this.authFailed = false;
        this.router.navigate(['/main']);
        form.reset();
      },
      error: err => {
        this.isLoading = false;
        this.authFailed = true;
        this.errorMessage = err;
      }
    });
  }
}
