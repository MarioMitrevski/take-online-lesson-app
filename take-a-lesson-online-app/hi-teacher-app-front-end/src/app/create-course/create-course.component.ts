import {AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {Subscription} from 'rxjs';
import {CreateCourse} from '../models/createCourse.interface';
import {CourseService} from '../services/course.service';
import {BasicCourseInfoFormComponent} from './basic-course-info-form/basic-course-info-form.component';
import {CourseThemesComponent} from './course-themes/course-themes.component';
import {CreateCourseGroupsComponent} from './create-course-groups/create-course-groups.component';
import {CourseGroup} from '../models/courseGroup.interface';
import {CourseTheme} from '../models/courseTheme.interface';
import {Router} from '@angular/router';

@Component({
  selector: 'app-create-course',
  templateUrl: './create-course.component.html',
  styleUrls: ['./create-course.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreateCourseComponent implements OnInit, AfterViewInit, OnDestroy {

  isLinear = false;
  course: CreateCourse;
  courseGroups: CourseGroup[] = [];
  private subscription: Subscription;
  @ViewChild('basicCourseInfoForm', {static: false}) basicCourseInfo: BasicCourseInfoFormComponent;
  @ViewChild('courseThemesForm', {static: false}) courseThemesForm: CourseThemesComponent;
  @ViewChild('courseGroupsForm', {static: false}) courseGroupsForm: CreateCourseGroupsComponent;

  constructor(private formBuilder: FormBuilder,
              private createCourseService: CourseService,
              private cdRef: ChangeDetectorRef,
              private router: Router) {
  }

  ngOnInit(): void {
    this.subscription = this.createCourseService.courseGroupsCreated.subscribe(it => {
      this.courseGroups = it;
    });
  }


  ngAfterViewInit(): void {
    this.cdRef.detectChanges();
    this.isLinear = this.basicCourseInfo.basicCourseInfoForm.invalid
      && this.courseThemesForm.courseContentForm.invalid;
  }


  onCourseCreationFinished(): void {
    const courseThemes: CourseTheme[] = this.courseThemesForm.courseContentForm.controls.themes.value.map(it => {
      return {
        CourseThemeName: it.courseThemeTitle,
        CourseThemeDescription: it.courseThemeDescription
      };
    });
    const newCourse: CreateCourse = {
      CourseId: null,
      CourseTitle: this.basicCourseInfo.basicCourseInfoForm.controls.courseTitle.value,
      CourseDescription: this.basicCourseInfo.basicCourseInfoForm.controls.courseDescription.value,
      CategoryId: +this.basicCourseInfo.basicCourseInfoForm.controls.courseCategory.value,
      PricePerStudentForSession: this.basicCourseInfo.basicCourseInfoForm.controls.pricePerStudentPerSession.value,
      CourseThemes: courseThemes,
      CourseGroups: this.courseGroups
    };
    const coursePhotoReq = new FormData();
    coursePhotoReq.append('ImageFile', this.basicCourseInfo.selectedFile);
    this.createCourseService.createCourse(newCourse, coursePhotoReq);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
