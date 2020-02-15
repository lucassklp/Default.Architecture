import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthenticationService, private loginService: LoginService) { }

  ngOnInit(): void {
    this.loginService.login({
      login: 'lucas.simas@test.com',
      password: '123456'
    }).subscribe(res => {
      this.authService.token = res.token;
    });
  }
}
