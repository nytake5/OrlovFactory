import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './service/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'factory-ui';
  isAuthorized$: Observable<boolean>;

  constructor(public authService: AuthService){
    this.isAuthorized$ = this.authService.checkCredsInCookie();
  }

  ngOnInit(){
    this.isAuthorized$ = this.authService.checkCredsInCookie();
  }
}
