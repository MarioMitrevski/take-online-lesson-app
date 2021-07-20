import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';
import {TimeslotsDetailsDialog} from '../../dialogs/timeslots-details/timeslots-details.dialog';
import {CourseService} from '../../services/course.service';

@Component({
  selector: 'app-edit-course-group',
  templateUrl: './edit-course-group.component.html',
  styleUrls: ['./edit-course-group.component.css']
})
export class EditCourseGroupComponent implements OnInit {

  @Input() courseGroupFormGroup: any;
  @Input() courseGroupName: string;
  @Input() courseGroupArrayIndex: number;
  @Input() viewMode = false;
  @Input() courseGroupId;
  @Output() deleteCourseGroup = new EventEmitter<number>();

  constructor(public dialog: MatDialog, private courseService: CourseService) {
  }

  ngOnInit(): void {
    console.log(this.courseGroupFormGroup);
  }

  showDates(): void {
    const dialogRef = this.dialog.open(TimeslotsDetailsDialog, {
      width: 'auto',
      data: {courseFormGroup: this.courseGroupFormGroup, viewMode: this.viewMode}
    });
  }

  onCourseGroupDeletion(courseGroupArrayIndex: number): void {
    this.deleteCourseGroup.emit(courseGroupArrayIndex);
  }

  enrollToCourse(): void {
    this.courseService.enrollCourse(this.courseGroupId);
  }
}
