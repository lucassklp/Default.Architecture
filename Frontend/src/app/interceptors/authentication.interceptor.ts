import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

    constructor(private authService: AuthenticationService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<any> {

        if (this.authService.isAuthenticated) {
            req = req.clone({
              setHeaders: {
                Authorization: `Bearer ${this.authService.token}`
              }
            });
        }

        return next.handle(req);
    }
}
