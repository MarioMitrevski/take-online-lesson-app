import {Component, Input, OnInit} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  @Input() courseTitle: string;
  @Input() pricePerSession: number;
  @Input() imageUrl: any;
  @Input() courseId: number;
  @Input() showEditButtons;

  constructor(private router: Router) {
  }

  ngOnInit(): void {
  }

  openCourseDetails(): void {
    this.router.navigate([`course/${this.courseId}`]);
  }

  editCourse(courseId: number): void {
    this.router.navigate([`course/edit/${this.courseId}`]);
  }

  onDeleteCourse(): void {

  }
}
