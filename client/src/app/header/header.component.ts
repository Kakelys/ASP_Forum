import { Subscription } from 'rxjs';
import { User } from '../../shared/user.model';
import { AuthService } from './../auth/auth.service';
import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  public user: User = null;
  collapsed = true;

  private userSub: Subscription;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.userSub = this.authService.$user.subscribe(user => {
      this.user = user;
    });
  }

  onLogout() {
    this.authService.logout();
  }

  ngOnDestroy(): void {
   this.userSub.unsubscribe();
  }
}
