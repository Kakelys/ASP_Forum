import { AuthService } from './auth.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  public isLogin = true;
  public isLoading = false;
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
      // TODO: mb add error handling with messagin on the page and redirct to main page
      authObs = this.authService.login(form.value)
    }
    else{
      //TODO: register

    }

    authObs.subscribe({
      next: _ => {
        this.isLoading = false;
        this.router.navigate(['/main']);
      }
    });

    form.reset();
  }
}
