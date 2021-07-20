import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MainNavComponent} from './main-nav/main-nav.component';
import {JoinFormComponent} from './join-form/join-form.component';
import {ProgramCatalogComponent} from './program-catalog/program-catalog.component';
import {CreateCourseComponent} from './create-course/create-course.component';
import {FullCourseDetailComponent} from './full-course-detail/full-course-detail.component';
import {EditCourseComponent} from './edit-course/edit-course.component';
import {AuthCrudCourseGuard} from './guards/auth-crud-course.guard';
import {UserCoursesComponent} from './user-courses/user-courses.component';
import {UserCoursesGuard} from './guards/user-courses-guard.guard';
import {EnrollToCourseComponent} from './enroll-to-course/enroll-to-course.component';

const routes: Routes = [
  {
    path: '', component: MainNavComponent, children: [
      {path: 'programmes', component: ProgramCatalogComponent},
      {path: 'createCourse', component: CreateCourseComponent, canActivate: [AuthCrudCourseGuard]},
      {path: 'course/:id/enroll', component: EnrollToCourseComponent},
      {path: 'course/:id', component: FullCourseDetailComponent},
      {path: 'course/edit/:id', component: EditCourseComponent, canActivate: [AuthCrudCourseGuard]},
      {path: 'courses/:status', component: UserCoursesComponent, canActivate: [UserCoursesGuard]},
    ]
  },
  {path: 'sign-up', component: JoinFormComponent, pathMatch: 'full'},
  {path: 'sign-in', component: JoinFormComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
