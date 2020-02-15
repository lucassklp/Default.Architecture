import { Injectable } from '@angular/core';

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

  public logout(){
    localStorage.removeItem('token')
  }
}
