import {Component, Input, OnInit} from '@angular/core';
import {CourseForPage} from '../models/CourseForPage.interace';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent implements OnInit {

  @Input() courses: CourseForPage[];
  @Input() showEditButtons = false;

  constructor() {
  }

  ngOnInit(): void {
  }

}
