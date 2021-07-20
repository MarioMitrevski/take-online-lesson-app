import {Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {CategoryService} from '../services/category.service';
import {Observable, pipe} from 'rxjs';
import {Category} from '../models/Category.interface';
import {ActivatedRoute} from '@angular/router';
import {CourseService} from '../services/course.service';
import {CourseDetailResponse} from '../models/CourseDetailResponse.interface';
import {switchMap, tap} from 'rxjs/operators';
import {DateTimeSlotResponse} from '../models/DateTimeSlotResponse.interface';
import {CreateCourse} from '../models/createCourse.interface';
import {DateTimeSlot} from '../models/DateTimeSlot.interface';



@Component({
  selector: 'app-edit-course',
  templateUrl: './edit-course.component.html',
  styleUrls: ['./edit-course.component.css']
})
export class EditCourseComponent implements OnInit {

  editCourse: FormGroup;
  categories$: Observable<Category[]>;
  panelOpenState = false;
  courseId: string;

  constructor(private formBuilder: FormBuilder,
              private activeRoute: ActivatedRoute,
              private categoryService: CategoryService,
              private courseService: CourseService) {
  }

  ngOnInit(): void {
    this.activeRoute.paramMap.subscribe(params => {
      this.courseId = params.get('id');
      this.courseService.getCourse(this.courseId).subscribe(it => {
        this.initForm(it);
      });
    });
    this.categories$ = this.categoryService.getAllCategories();
  }


  initForm(courseDetail: CourseDetailResponse): void {
    this.editCourse = this.formBuilder.group({
      CourseId: [courseDetail.courseId],
      CourseTitle: [courseDetail.courseTitle, Validators.required],
      CategoryId: [courseDetail.category.categoryId.toString(), Validators.required],
      PricePerStudentForSession: [courseDetail.pricePerStudentForSession, Validators.required],
      CourseDescription: [courseDetail.courseDescription, Validators.required],
      CourseThemes: this.formBuilder.array([]),
      CourseGroups: this.formBuilder.array([])
    });
    courseDetail.courseThemes.forEach(it => {
      this.addExistingThemes(it.courseThemeName, it.courseThemeDescription);
    });
    courseDetail.courseGroups.forEach(it => {
      this.addExistingCourseGroups(it.courseGroupName,
        it.courseGroupGoogleMeetLink,
        it.minStudents,
        it.maxStudents,
        it.dateTimeSlots);
    });
  }

  get courseThemes(): FormArray {
    return this.editCourse.get('CourseThemes') as FormArray;
  }

  get courseGroups(): FormArray {
    return this.editCourse.get('CourseGroups') as FormArray;
  }

  onThemeAdded(): void {
    this.courseThemes.push(new FormGroup({
      CourseThemeName: new FormControl('', Validators.required),
      CourseThemeDescription: new FormControl('', Validators.required)
    }));
  }

  formatDateToDisplay(date: string, startTime: string): string {
    const dateArray = date.split('.');
    const timeArray = startTime.split(':');
    return dateArray[2] + '-' + dateArray[0] + '-' + dateArray[1] + 'T' + timeArray[0] + ':' + timeArray[1];
  }

  addExistingCourseGroups(courseName: string,
                          groupMeetLink: string,
                          minStudents: number,
                          maxStudent: number,
                          dateTimeSlots: DateTimeSlotResponse[]): void {
    const dateTimeSlotsFormArray = this.formBuilder.array([]);
    dateTimeSlots.forEach(it => {
      dateTimeSlotsFormArray.push(new FormGroup({
          startDateTime: new FormControl(this.formatDateToDisplay(it.date, it.startTime), Validators.required),
          endDateTime: new FormControl(this.formatDateToDisplay(it.date, it.endTime), Validators.required)
        }
      ));
    });
    this.courseGroups.push(new FormGroup({
      CourseGroupName: new FormControl(courseName, Validators.required),
      CourseGroupGoogleMeetLink: new FormControl(groupMeetLink, Validators.required),
      MinStudents: new FormControl(minStudents, Validators.required),
      MaxStudents: new FormControl(maxStudent, Validators.required),
      DateTimeSlots: dateTimeSlotsFormArray
    }));
  }

  onCourseGroupAdded(): void {
    this.courseGroups.push(new FormGroup({
      CourseGroupName: new FormControl('', Validators.required),
      CourseGroupGoogleMeetLink: new FormControl('', Validators.required),
      MinStudents: new FormControl('', Validators.required),
      MaxStudents: new FormControl('', Validators.required),
      DateTimeSlots: this.formBuilder.array([])
    }));
  }

  onThemeDeletion(i: number): void {
    this.courseThemes.removeAt(i);
  }

  addExistingThemes(themeName: string, themeTitle: string): void {
    this.courseThemes.push(new FormGroup({
      CourseThemeName: new FormControl(themeName, Validators.required),
      CourseThemeDescription: new FormControl(themeTitle, Validators.required)
    }));
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
    return month + '.' + day + '.' + newDate.getFullYear();
  }

  convertToDateTime(timeSlot: any): any {
    const startDate = new Date(timeSlot.startDateTime);
    const endDate = new Date(timeSlot.endDateTime);
    return {
      Date: this.formatDate(startDate.toDateString()),
      StartTime: startDate.toTimeString().split(' ')[0],
      EndTime: endDate.toTimeString().split(' ')[0]
    };
  }


  onSubmit(): void {
    const editedCourse: CreateCourse = this.editCourse.value;
    editedCourse.CourseGroups = editedCourse.CourseGroups.map(cg => {
      return {
        ...cg,
        DateTimeSlots: cg.DateTimeSlots.map(ts => {
          return this.convertToDateTime(ts);
        })
      };
    });

    this.courseService.updateCourse(editedCourse);
  }

  courseGroupDeletion(event: number): void {
    this.courseGroups.removeAt(event);
  }
}
