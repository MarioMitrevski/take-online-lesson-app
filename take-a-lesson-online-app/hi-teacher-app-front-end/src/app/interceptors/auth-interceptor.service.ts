import { Injectable } from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor{

  constructor() { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url !== 'http://localhost:8080/api/users/register' && (req.url !== 'http://localhost:8080/api/users/authenticate')) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${localStorage.getItem('jwt')}`,
        }
      });
    }
    return next.handle(req);
  }
}
