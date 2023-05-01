import { Component, Input } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AuthService } from 'src/app/service/auth.service';
import { UserCredentials } from 'src/app/types/UserCredentials';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent {
  @Input() isAuth: Observable<boolean> = of(false);
  login: string = '';
  token: string = '';

  constructor(public authService: AuthService) {  
  }
  
  onClickSuccessButton(): void {
    const userCreds: UserCredentials = {
      Login: this.login,
      Token: this.token,
      Password: "moqed"
    }
    this.authService.authorizeByLogAndToken(userCreds);
    const example = this.isAuth.subscribe(val => {
      this.authService.checkCredsInCookie();
    });
  } 
}
