import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Observable} from 'rxjs';
import {CourseService} from '../services/course.service';
import {CourseForPage} from '../models/CourseForPage.interace';

@Component({
  selector: 'app-user-courses',
  templateUrl: './user-courses.component.html',
  styleUrls: ['./user-courses.component.css']
})
export class UserCoursesComponent implements OnInit {

  courseStatuses = [
    {index: 0, value: 'inprogress'},
    {index: 2, value: 'finished'},
    {index: 1, value: 'upcoming'}
  ];

  currentRole: string;
  currentSelectedIndex: number;
  upComingCourses: Observable<CourseForPage>;
  inProgressCourses: Observable<CourseForPage>;
  finishedCourses: Observable<CourseForPage>;
  showEditButtons = false;

  constructor(private router: Router, private coursesService: CourseService) {
  }

  ngOnInit(): void {
    this.currentSelectedIndex = 0;
    const role = localStorage.getItem('role');
    this.currentRole = role;
    if (role === 'Teacher') {
      this.showEditButtons = true;
      this.upComingCourses = this.coursesService.getTeacherCourses('upcomming');
      this.finishedCourses = this.coursesService.getTeacherCourses('finished');
      this.inProgressCourses = this.coursesService.getTeacherCourses('inprogress');
    } else {
      this.upComingCourses = this.coursesService.getStudentCourses('upcomming');
      this.finishedCourses = this.coursesService.getStudentCourses('finished');
      this.inProgressCourses = this.coursesService.getStudentCourses('inprogress');
    }
  }

  onCourseStatusChanged(tabIndex: number): void {
    const courseStatusUrl = this.courseStatuses.filter(it => it.index === tabIndex);
    this.router.navigate([`courses/${courseStatusUrl[0].value}`]);
  }
}
