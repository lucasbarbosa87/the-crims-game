import { Component, OnInit } from '@angular/core';
import { UserSessionService } from './shared/services/user-session.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'crims-backoffice';

  constructor(private sessionService: UserSessionService, private router: Router) { }
  ngOnInit(): void {
    var user = this.sessionService.getUser();
    if (user == null) {
      this.router.navigateByUrl("/login");
    }
  }
}
