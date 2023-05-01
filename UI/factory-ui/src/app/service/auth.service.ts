import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { UserCredentials } from '../types/UserCredentials';
import { of, Observable, retry, concatWith } from 'rxjs';
import { UserAccessToken } from '../types/UserAccessToken';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private httpClient: HttpClient,
              private cookieService: CookieService) 
  { }

  authorizeByAccessToken(token: UserAccessToken) : Observable<UserCredentials> {
    return this.httpClient.post<UserCredentials>(
        'http://localhost:5001/api/',
        token)
      .pipe(
        retry(2)
      );
  }
  
  checkCredsInCookie() : Observable<boolean> {
    const cookies: Array<string> = decodeURIComponent(document.cookie).split('; ');
  
    let userName = null;
    let accessToken = null;
    cookies.forEach(cook => {
      console.log(cook)

      const userCook = cook.split('=');
      if (userCook[0] === 'access_token') {
        accessToken = userCook[1]
      }
      
      if (userCook[0] === 'login') {
        userName = userCook[1]
      }
    });

    console.log(userName);
    console.log(accessToken);
    if (userName !== null && accessToken !== null)
    {
      return of(true);
    }

    return of(false);
  }

  addCredsInCookie(creds: UserAccessToken) : void {
    this.cookieService.set('login', creds.login);
    this.cookieService.set('access_token', creds.token);
  }

  authorizeByLogAndToken(user: UserCredentials) : Observable<boolean> {
    const result = this.httpClient.post<UserAccessToken>(
      'http://localhost:5001/api/User/Token',
      user)
      .pipe(
        retry(2)
      ); 
    if (result !== null)
    {
      result.subscribe(x => { 
        this.addCredsInCookie(x)
      });
    }

    return of(result !== null);
  }
} 
