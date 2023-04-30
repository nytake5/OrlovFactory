import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AuthService } from 'src/app/service/auth.service';
import { UserCredentials } from 'src/app/types/UserCredentials';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.scss']
})
export class BodyComponent implements OnInit {
  public userData$!: Observable<UserCredentials>;
  public isAuth$: Observable<boolean> = of(false);

  constructor(public authService: AuthService) {
    console.log('Starting BodyComponent');
  }
  
  ngOnInit(): void {
    this.isAuth$ = this.authService.checkCreds();
  }
}
