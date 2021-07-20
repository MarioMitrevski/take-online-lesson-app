import {Component, OnInit, ViewChild} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {DateTimeSlot} from '../../models/DateTimeSlot.interface';
import {CourseService} from '../../services/course.service';
import {MatRadioChange} from '@angular/material/radio';

@Component({
  selector: 'app-create-course-groups',
  templateUrl: './create-course-groups.component.html',
  styleUrls: ['./create-course-groups.component.css']
})
export class CreateCourseGroupsComponent implements OnInit {

  courseGroupForm: FormGroup;
  custom = false;
  fixed = false;
  datesFixed = false;
  maxStudents = true;
  minStudents = 0;
  @ViewChild('fixed') fixedCheckbox;
  @ViewChild('custom') customCheckbox;

  constructor(private formBuilder: FormBuilder, public createCourseService: CourseService) {
  }

  ngOnInit(): void {
    this.courseGroupForm = this.formBuilder.group({
      courseGroupName: ['', Validators.required],
      courseGroupMeetLinkUrl: ['', Validators.required],
      minNumberOfStudents: ['', Validators.required],
      maxNumberOfStudents: [{value: '', disabled: this.maxStudents}, Validators.required],
      typeOfTimeSlots: ['', Validators.required],
      customTimeSlots: this.formBuilder.array([]),
      fixedTimeSlotsInformation: this.formBuilder.array([]),
      fixedTimeSlots: this.formBuilder.array([])
    });

  }

  get customTimeSlots(): FormArray {
    return this.courseGroupForm.get('customTimeSlots') as FormArray;
  }

  get fixedTimeSlotsInformation(): FormArray {
    return this.courseGroupForm.get('fixedTimeSlotsInformation') as FormArray;
  }

  get fixedTimeSlots(): FormArray {
    return this.courseGroupForm.get('fixedTimeSlots') as FormArray;
  }

  addCustomTimeSlot(): void {
    this.customTimeSlots.push(new FormGroup({
      customTimeSlot: new FormControl('', Validators.required),
      duration: new FormControl('', Validators.required)
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


  onTimesAWeekChanged(event: Event): void {
    const timesInAWeek = +event;
    this.fixedTimeSlots.clear();
    if (timesInAWeek != null) {
      this.datesFixed = true;
      for (let i = 0; i < timesInAWeek; i++) {
        this.fixedTimeSlots.push(new FormGroup({
          fixedTimeSlot: new FormControl('', Validators.required),
          duration: new FormControl('', Validators.required)
        }));
      }
    }
  }

  convertToDateTimeSlotFromCustom(customTimeSlot: any): DateTimeSlot {
    const date = new Date(customTimeSlot.customTimeSlot);
    const endDate = new Date(customTimeSlot.customTimeSlot);
    endDate.setHours(endDate.getHours() + +customTimeSlot.duration);
    return {
      Date: this.formatDate(date.toDateString()),
      StartTime: date.toTimeString().split(' ')[0],
      EndTime: endDate.toTimeString().split(' ')[0]
    };
  }

  convertToDateTimeSlotFromFixed(date: Date, duration: number): DateTimeSlot {
    const startDate = new Date(date);
    const endDate = new Date(date);
    startDate.setHours(startDate.getHours());
    endDate.setHours(endDate.getHours() + duration);
    return {
      Date: this.formatDate(startDate.toDateString()),
      StartTime: startDate.toTimeString().split(' ')[0],
      EndTime: endDate.toTimeString().split(' ')[0]
    };
  }


  onSubmit(): void {
    const dateTimeSlots: DateTimeSlot[] = [];
    const newCourseGroup = {
      CourseGroupName: this.courseGroupForm.controls.courseGroupName.value,
      CourseGroupGoogleMeetLink: this.courseGroupForm.controls.courseGroupMeetLinkUrl.value,
      MinStudents: this.courseGroupForm.controls.minNumberOfStudents.value,
      MaxStudents: this.courseGroupForm.controls.maxNumberOfStudents.value,
      DateTimeSlots: []
    };
    if (this.fixed) {
      // @ts-ignore
      const numberOfWeeks = +this.fixedTimeSlotsInformation.controls[0].controls.numberOfWeeks.value;
      for (const fixedTimeSlot of this.fixedTimeSlots.value) {
        const currentDate = new Date(fixedTimeSlot.fixedTimeSlot);
        for (let i = 0; i < numberOfWeeks; i++) {
          if (i !== 0) {
            currentDate.setDate(currentDate.getDate() + 7);
            dateTimeSlots.push(this.convertToDateTimeSlotFromFixed(currentDate, +fixedTimeSlot.duration));
          } else {
            dateTimeSlots.push(this.convertToDateTimeSlotFromFixed(currentDate, +fixedTimeSlot.duration));
          }
        }
      }
      newCourseGroup.DateTimeSlots = dateTimeSlots;
    } else {
      for (const customTimeSlot of this.customTimeSlots.value) {
        dateTimeSlots.push(this.convertToDateTimeSlotFromCustom(customTimeSlot));
      }
      newCourseGroup.DateTimeSlots = dateTimeSlots;
    }
    this.createCourseService.addCourseGroup(newCourseGroup);
    this.courseGroupForm.reset();
    this.fixedTimeSlots.clear();
    this.fixedTimeSlotsInformation.clear();
    this.customTimeSlots.clear();
    this.custom = false;
    this.fixed = false;
  }

  courseGroupDelete(courseGroupIndex: number): void {
    this.createCourseService.removeCourseGroup(courseGroupIndex);
  }

  onCourseGroupsCreated(): void {
    this.createCourseService.sendCreatedCourseGroups();
  }

  onTypeOfTimeSlotChange(event: MatRadioChange): void {
    if (event.value === 'custom') {
      this.datesFixed = false;
      this.custom = true;
      this.fixed = false;
      this.customTimeSlots.clear();
      this.customTimeSlots.push(new FormGroup({
        customTimeSlot: new FormControl('', Validators.required),
        duration: new FormControl('', Validators.required)
      }));
      this.fixedTimeSlotsInformation.clear();
      this.fixedTimeSlots.clear();
    } else {
      this.fixed = true;
      this.custom = false;
      this.fixedTimeSlotsInformation.clear();
      this.fixedTimeSlots.clear();
      this.customTimeSlots.clear();
      this.fixedTimeSlotsInformation.push(new FormGroup({
        numberOfWeeks: new FormControl('', Validators.required),
        timesInAWeek: new FormControl('', Validators.required)
      }));
    }
  }

  onCustomTimeSlotDeletion(i: number): void {
    this.customTimeSlots.removeAt(i);
  }

  onMinStudentsSelected(event: Event): void {
    this.courseGroupForm.get('maxNumberOfStudents').enable();
    this.maxStudents = false;
    this.minStudents = +this.courseGroupForm.get('minNumberOfStudents').value;
  }
}
