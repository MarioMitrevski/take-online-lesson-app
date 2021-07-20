import {AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {CourseService} from '../../services/course.service';
import {Observable} from 'rxjs';
import {Category} from '../../models/Category.interface';
import {CategoryService} from '../../services/category.service';

@Component({
  selector: 'app-basic-course-info-form',
  templateUrl: './basic-course-info-form.component.html',
  styleUrls: ['./basic-course-info-form.component.css'],
})
export class BasicCourseInfoFormComponent implements OnInit {

  basicCourseInfoForm: FormGroup;
  categories$: Observable<Category[]>;
  selectedFile = null;

  constructor(private formBuilder: FormBuilder, private categoryService: CategoryService) {
  }

  ngOnInit(): void {
    this.basicCourseInfoForm = this.formBuilder.group({
      courseTitle: ['', Validators.required],
      courseCategory: ['', Validators.required],
      pricePerStudentPerSession: ['', Validators.required],
      courseDescription: ['', Validators.required],
      coursePhoto: ['', Validators.required]
    });
    this.categories$ = this.categoryService.getAllCategories();
  }

  onCoursePhotoSelected($event): void {
    this.selectedFile = $event.target.files[0];
  }
}
