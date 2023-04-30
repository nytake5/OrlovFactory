import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { UserCredentials } from '../types/UserCredentials';
import { of, Observable, retry } from 'rxjs';
import { UserAccessToken } from '../types/UserAccessToken';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private httpClient: HttpClient) { }

  authorizeByLogAndToken(user: UserCredentials) : Observable<UserCredentials> {
    return this.httpClient.post<UserCredentials>(
      'http://localhost:3000/api/auth/login',
      user)
      .pipe(
        retry(2)
      ); 
  }

  authorizeByAccessToken(token: UserAccessToken) : Observable<UserCredentials> {
    return this.httpClient.post<UserCredentials>(
        'http://localhost:3000/api/auth/login',
        token)
      .pipe(
        retry(2)
      );
  }
  
  checkCreds() : Observable<boolean> {
    const cookies: Array<string> = decodeURIComponent(document.cookie).split('; ');
  
    let currentCookie: string = 'access_token';
    let result = null;
    cookies.forEach(cook => {
      if (cook === currentCookie) {
        const userCook = cook.split('=');

        if (userCook[0] === currentCookie) {
          const token = new UserAccessToken (userCook[1]);
          result = this.authorizeByAccessToken(token);
        }
      }    
    });
    console.log(!(result === null));
    return of((result === null));
    //to do v'ebat' !
  }
} 
