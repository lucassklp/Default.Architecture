import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Token } from '../models/token';
import { Credential } from '../models/credential';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient: HttpClient) { }
  

  public login(credential: Credential): Observable<Token> {
    return this.httpClient.post<Token>('api/login', credential);
  }
}
