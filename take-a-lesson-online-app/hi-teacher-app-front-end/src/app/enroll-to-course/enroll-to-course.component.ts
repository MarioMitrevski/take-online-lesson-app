import {Component, OnInit} from '@angular/core';
import {CourseDetailResponse} from '../models/CourseDetailResponse.interface';
import {ActivatedRoute, Router} from '@angular/router';
import {CourseService} from '../services/course.service';
import {Form, FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {CourseGroupResponse} from '../models/CourseGroupResponse.interface';
import {DateTimeSlotResponse} from '../models/DateTimeSlotResponse.interface';

@Component({
  selector: 'app-enroll-to-course',
  templateUrl: './enroll-to-course.component.html',
  styleUrls: ['./enroll-to-course.component.css']
})
export class EnrollToCourseComponent implements OnInit {

  courseId: string;
  courseDetail: CourseDetailResponse;
  courseGroupsForm: FormGroup;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private coursesService: CourseService,
              private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.courseId = params.get('id');
    });
    this.coursesService.getCourse(this.courseId).subscribe(it => {
      this.courseDetail = it;
      this.initForm(it.courseGroups);
    });
  }

  formatDateToDisplay(date: string, startTime: string): string {
    const dateArray = date.split('.');
    const timeArray = startTime.split(':');
    return dateArray[2] + '-' + dateArray[0] + '-' + dateArray[1] + 'T' + timeArray[0] + ':' + timeArray[1];
  }

  timeSlotsFormArray(dateTimeSlotsResponse: DateTimeSlotResponse[]): FormArray {
    const timeSlotsArray = this.formBuilder.array([]);
    dateTimeSlotsResponse.forEach(ts => {
      timeSlotsArray.push(new FormGroup({
        startDateTime:
          new FormControl({value: this.formatDateToDisplay(ts.date, ts.startTime), disabled: true}, Validators.required),
        endDateTime:
          new FormControl({value: this.formatDateToDisplay(ts.date, ts.endTime), disabled: true}, Validators.required)
      }));
    });
    return timeSlotsArray;
  }

  get courseGroups(): FormArray {
    return this.courseGroupsForm.get('CourseGroups') as FormArray;
  }

  initForm(courseGroups: CourseGroupResponse[]): void {
    const courseGroupsArray = this.formBuilder.array([]);
    courseGroups.forEach(cg => {
      courseGroupsArray.push(new FormGroup(
        {
          CourseGroupId: new FormControl(cg.courseGroupId),
          CourseGroupNameForDisplay: new FormControl(cg.courseGroupName),
          CourseGroupName: new FormControl({value: cg.courseGroupName, disabled: true}, Validators.required),
          CourseGroupGoogleMeetLink: new FormControl({value: cg.courseGroupGoogleMeetLink, disabled: true}, Validators.required),
          MinStudents: new FormControl({value: cg.minStudents, disabled: true}, Validators.required),
          MaxStudents: new FormControl({value: cg.maxStudents, disabled: true}, Validators.required),
          DateTimeSlots: this.timeSlotsFormArray(cg.dateTimeSlots)
        }
      ));
    });
    this.courseGroupsForm = this.formBuilder.group({
      CourseGroups: courseGroupsArray
    });

  }

}
