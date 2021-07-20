import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CourseGroup} from '../../models/courseGroup.interface';

@Component({
  selector: 'app-course-group-short-in-form',
  templateUrl: './course-group-short-in-form.component.html',
  styleUrls: ['./course-group-short-in-form.component.css']
})
export class CourseGroupShortInFormComponent implements OnInit {

  @Input() courseGroup: CourseGroup;
  @Input() rowNumber: number;
  @Output() deleted = new EventEmitter<number>();

  constructor() {
  }

  ngOnInit(): void {
  }

  onDeleteCourseGroup(rowNumber: number): void {
    this.deleted.emit(rowNumber);
  }
}
