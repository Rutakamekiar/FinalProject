import { Injectable } from '@angular/core';
import {Observable, Subscription, empty} from 'rxjs';
import { HttpClient, HttpHeaders , HttpErrorResponse} from '@angular/common/http';
import { UserLogin } from '../models/user-login';
import { UserLoginResponse } from '../models/user-login-response';
import {UserRegister} from '../models/user-register';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  readonly baseUrl = 'http://localhost:52081';

  constructor(private http: HttpClient) { }

  loginUser(user: UserLogin): Observable<UserLoginResponse> {
    const url = `/token`;
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/x-www-form-urlencoded');
    return this.http.post<UserLoginResponse>(this.baseUrl + url, 'userName=' + encodeURIComponent(user.userName) +
      '&password=' + encodeURIComponent(user.password) +
      '&grant_type=password', {headers});
  }

  registerUser(user: UserRegister) {
    const url = this.baseUrl + '/api/account/register';
    return this.http.post(url, user);
  }
  logout() {
    localStorage.removeItem('access_token');
  }
}
