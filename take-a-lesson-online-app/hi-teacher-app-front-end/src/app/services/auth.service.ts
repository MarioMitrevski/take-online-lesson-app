import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LogInUser} from '../models/logInUser.interface';
import {LogInResponse} from '../models/LogInResponse.interface';
import {MatSnackBar} from '@angular/material/snack-bar';
import {Router} from '@angular/router';
import {Subject} from 'rxjs';
import {Confirmation} from '../models/Confirmation.inteface';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private url = 'http://localhost:8080/api';
  userLogged = new Subject<boolean>();
  helper = new JwtHelperService();

  constructor(private http: HttpClient, private snackBar: MatSnackBar, private router: Router) {
  }

  registerUser(formData: FormData): void {
    this.http.post(`${this.url}/users/register`, formData).subscribe(it => {
      this.router.navigate(['/sign-in']);
      this.snackBar.open('Successfully registered', 'Close', {
        duration: 2000,
        horizontalPosition: 'end',
        verticalPosition: 'top',
        panelClass: ['green-snackbar']
      });
    }, error => {
      console.log(error);
      this.snackBar.open('Something went wrong', 'Close', {
        duration: 2000,
        horizontalPosition: 'end',
        verticalPosition: 'top'
      });
    });
  }

  passwordsNotMatching(): void {
    this.snackBar.open('Confirm password must match password', 'Close', {
      duration: 2000,
      horizontalPosition: 'end',
      verticalPosition: 'top'
    });
  }

  isUserLogged(): boolean {
    return localStorage.getItem('jwt') !== undefined;
  }

  authenticateUser(logInUser: LogInUser): void {
    this.http.post<LogInResponse>(`${this.url}/users/authenticate`, logInUser).subscribe(it => {
      const decodedToken = this.helper.decodeToken(it.jwtToken);
      localStorage.setItem('role', decodedToken.role);
      localStorage.setItem('jwt', it.jwtToken);
      localStorage.setItem('imageUrl', it.imageUrl);
      localStorage.setItem('fullName', it.fullName);
      this.snackBar.open('Successfully logged', 'Close', {
        duration: 2000,
        horizontalPosition: 'end',
        verticalPosition: 'top',
        panelClass: ['green-snackbar']
      });
      this.router.navigate(['']);
      this.userLogged.next(true);
    }, error => {
      this.snackBar.open('Bad Credentials', 'Close', {
        duration: 2000,
        horizontalPosition: 'end',
        verticalPosition: 'top'
      });
    });
  }
}
