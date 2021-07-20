import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseService} from '../services/course.service';
import {CourseDetailResponse} from '../models/CourseDetailResponse.interface';
import {FormBuilder, FormGroup} from '@angular/forms';
import {CourseGroup} from '../models/courseGroup.interface';
import {CourseGroupResponse} from '../models/CourseGroupResponse.interface';

@Component({
  selector: 'app-full-course-detail',
  templateUrl: './full-course-detail.component.html',
  styleUrls: ['./full-course-detail.component.css']
})
export class FullCourseDetailComponent implements OnInit {

  courseId: string;
  courseDetail: CourseDetailResponse;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private coursesService: CourseService) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.courseId = params.get('id');
    });
    this.coursesService.getCourse(this.courseId).subscribe(it => {
      this.courseDetail = it;
    });
  }

  toCourseEnroll(courseId: string): void {
    this.router.navigate([`course/${courseId}/enroll`]);
  }
}
