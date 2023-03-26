import { Author } from './../../shared/author.model';
import { AuthService } from './../auth/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  public user: Author = null;
  collapsed = true;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.$user.subscribe(user => {
      this.user = user;
    });
  }

  onLogout() {
    this.authService.logout();
  }
}
