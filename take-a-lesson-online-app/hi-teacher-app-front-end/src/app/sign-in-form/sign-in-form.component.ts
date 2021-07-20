import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {LogInUser} from '../models/logInUser.interface';
import {AuthService} from '../services/auth.service';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-sign-in-form',
  templateUrl: './sign-in-form.component.html',
  styleUrls: ['./sign-in-form.component.css']
})
export class SignInFormComponent implements OnInit {

  @ViewChild('formElement') signInForm: NgForm;
  hide = true;

  constructor(private authService: AuthService) {
  }

  ngOnInit(): void {
  }

  getEmailErrorMessage(): string {
    if (this.signInForm.control.controls.email.hasError('required')) {
      return 'You must enter a value';
    }
    return this.signInForm.control.controls.email.hasError('email') ? 'Not a valid email' : '';
  }

  onSubmit(): void {
    const logInObject: LogInUser = {
      UserName: this.signInForm.form.controls.email.value,
      Password: this.signInForm.form.controls.password.value
    };
    this.authService.authenticateUser(logInObject);
  }
}
