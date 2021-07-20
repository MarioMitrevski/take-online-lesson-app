import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from '@angular/forms';
import {AuthService} from '../services/auth.service';
import {MatRadioChange} from '@angular/material/radio';

@Component({
  selector: 'app-sign-up-form',
  templateUrl: './sign-up-form.component.html',
  styleUrls: ['./sign-up-form.component.css']
})
export class SignUpFormComponent implements OnInit {

  @ViewChild('formElement') signUpForm: NgForm;
  hide = true;
  hideConfirm = true;
  roleForSignUp = '';
  selectedFile = null;

  constructor(private authService: AuthService) {
  }

  ngOnInit(): void {
  }

  getEmailErrorMessage(): string {
    if (this.signUpForm.control.controls.email.hasError('required')) {
      return 'You must enter a value';
    }
    return this.signUpForm.control.controls.email.hasError('email') ? 'Not a valid email' : '';
  }

  formatDate(date: string): string {
    const newDate = new Date(date);
    let day = '';
    let month = '';
    if (newDate.getDate() < 10) {
      day = '0' + newDate.getDate().toString();
    } else {
      day = newDate.getDate().toString();
    }
    if (+newDate.getMonth() + 1 < 10) {
      month = '0' + (+newDate.getMonth() + 1).toString();
    } else {
      month = (+newDate.getMonth() + 1).toString();
    }
    return day + '/' + month + '/' + newDate.getFullYear();
  }

  checkPasswordMatch(): boolean {
    return this.signUpForm.form.controls.password.value === this.signUpForm.form.controls.passwordConfirm.value;
  }

  onSubmit(): void {
    if (this.checkPasswordMatch()) {
      const formData = new FormData();
      formData.append('ImageFile', this.selectedFile);
      formData.append('UserName', this.signUpForm.form.controls.email.value);
      formData.append('FullName', this.signUpForm.form.controls.firstName.value + ' ' + this.signUpForm.form.controls.lastName.value);
      formData.append('Password', this.signUpForm.form.controls.password.value);
      formData.append('BirthDate', this.formatDate(this.signUpForm.form.controls.birthDate.value.toDateString()));
      formData.append('IsTeacher', this.signUpForm.form.controls.roleForSignUp.value === 'teacher' ? 'true' : 'false');
      formData.append('TeacherDescription', this.signUpForm.form.controls.teacherDescription.value);
      this.authService.registerUser(formData);
    } else {
      this.authService.passwordsNotMatching();
    }
  }

  onFileSelected(event): void {
    this.selectedFile = event.target.files[0];
  }
}
