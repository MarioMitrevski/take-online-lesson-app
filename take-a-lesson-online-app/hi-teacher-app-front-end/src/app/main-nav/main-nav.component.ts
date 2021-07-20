import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreakpointObserver, Breakpoints} from '@angular/cdk/layout';
import {Observable, Subscription} from 'rxjs';
import {map, shareReplay} from 'rxjs/operators';
import {Router} from '@angular/router';
import {AuthService} from '../services/auth.service';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.css']
})
export class MainNavComponent implements OnInit, OnDestroy {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
  name = '';
  imageUrl = '';
  loggedIn = false;
  private subscription: Subscription;
  currentRole = '';

  constructor(private breakpointObserver: BreakpointObserver,
              private router: Router,
              private authService: AuthService) {
  }

  resolveCurrentRole(): void {
    const role = localStorage.getItem('role');
    if (role !== undefined){
      this.currentRole = role;
    }
  }

  toSignUp(): void {
    this.router.navigate(['sign-up']);
  }

  ngOnDestroy(): void {
  }

  ngOnInit(): void {
    this.subscription = this.authService.userLogged.subscribe(it => {
      this.loggedIn = it;
      this.imageUrl = localStorage.getItem('imageUrl');
      this.name = localStorage.getItem('fullName').split(' ')[0];
    });
    if (localStorage.getItem('jwt')) {
      this.loggedIn = true;
      this.name = localStorage.getItem('fullName').split(' ')[0];
      this.imageUrl = localStorage.getItem('imageUrl');
    } else {
      this.loggedIn = false;
      this.imageUrl = '';
      this.name = '';
    }
    this.resolveCurrentRole();
  }

  onLogOut(): void {
    this.loggedIn = false;
    this.imageUrl = '';
    this.name = '';
    localStorage.clear();
  }
}
