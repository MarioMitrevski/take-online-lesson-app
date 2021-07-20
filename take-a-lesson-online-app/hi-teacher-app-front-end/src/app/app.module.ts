import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MaterialModule} from './material-module/material.module';
import { MainNavComponent } from './main-nav/main-nav.component';
import { AppRoutingModule } from './app-routing.module';
import { JoinFormComponent } from './join-form/join-form.component';
import { SignInFormComponent } from './sign-in-form/sign-in-form.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { SignUpFormComponent } from './sign-up-form/sign-up-form.component';
import { ProgramCatalogComponent } from './program-catalog/program-catalog.component';
import { CategoriesListComponent } from './categories-list/categories-list.component';
import { CoursesListComponent } from './courses-list/courses-list.component';
import { CourseComponent } from './courses-list/course/course.component';
import { CreateCourseComponent } from './create-course/create-course.component';
import { BasicCourseInfoFormComponent } from './create-course/basic-course-info-form/basic-course-info-form.component';
import { CourseThemesComponent } from './create-course/course-themes/course-themes.component';
import { CreateCourseGroupsComponent } from './create-course/create-course-groups/create-course-groups.component';
import { CourseGroupShortInFormComponent } from './create-course/course-group-short-in-form/course-group-short-in-form.component';
import { FullCourseDetailComponent } from './full-course-detail/full-course-detail.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {AuthInterceptorService} from './interceptors/auth-interceptor.service';
import {EditCourseComponent} from './edit-course/edit-course.component';
import { EditCourseGroupComponent } from './edit-course/edit-course-group/edit-course-group.component';
import { UserCoursesComponent } from './user-courses/user-courses.component';
import { TimeslotsDetailsDialog } from './dialogs/timeslots-details/timeslots-details.dialog';
import { EnrollToCourseComponent } from './enroll-to-course/enroll-to-course.component';
import { EnrollFinishedDialog } from './dialogs/enroll-finished-dialog/enroll-finished.dialog';

@NgModule({
  declarations: [
    AppComponent,
    MainNavComponent,
    JoinFormComponent,
    SignInFormComponent,
    SignUpFormComponent,
    ProgramCatalogComponent,
    CategoriesListComponent,
    CoursesListComponent,
    CourseComponent,
    CreateCourseComponent,
    BasicCourseInfoFormComponent,
    CourseThemesComponent,
    CreateCourseGroupsComponent,
    CourseGroupShortInFormComponent,
    FullCourseDetailComponent,
    EditCourseComponent,
    EditCourseGroupComponent,
    UserCoursesComponent,
    TimeslotsDetailsDialog,
    EnrollToCourseComponent,
    EnrollFinishedDialog
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule

  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
