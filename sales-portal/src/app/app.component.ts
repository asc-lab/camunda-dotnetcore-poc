import { Component } from '@angular/core';
import { User } from './_model/user.dto';
import { Router } from '@angular/router';
import { AuthService } from './_services/auth-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'sales-portal';
  currentUser: User;

  constructor(
    private router: Router,
    private auth: AuthService) {
    this.auth.currentUser.subscribe(x => this.currentUser = x);
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
}
}
