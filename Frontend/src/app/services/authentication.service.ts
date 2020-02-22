import { Injectable } from '@angular/core';
import * as jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor() { }

  public set token(value: string){
    sessionStorage.setItem('token', value)
  }

  public get token(): string {
    return sessionStorage.getItem('token')
  }

  public get isAuthenticated(): boolean {
    return this.token != null && this.token != undefined && this.token != '';
  }

  public get user(): any {
    let user = jwt_decode(this.token);
    console.log(user);
    return user;
  }

  public logout(){
    localStorage.removeItem('token')
  }
}
