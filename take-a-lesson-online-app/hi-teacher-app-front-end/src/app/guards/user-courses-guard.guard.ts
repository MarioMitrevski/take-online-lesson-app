import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {AuthService} from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserCoursesGuard implements CanActivate {

  constructor(private router: Router, private authService: AuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot,
              state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (route.params.status !== 'inprogress' && route.params.status !== 'upcoming' && route.params.status !== 'finished') {
      return this.router.createUrlTree(['']);
    }

    if (this.authService.isUserLogged()) {
      if (localStorage.getItem('role') === 'Teacher') {
        return true;
      } else {
        return this.router.createUrlTree(['']);
      }
    } else {
      return this.router.createUrlTree(['']);
    }

  }
}
