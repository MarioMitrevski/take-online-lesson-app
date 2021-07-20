import {Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-course-themes',
  templateUrl: './course-themes.component.html',
  styleUrls: ['./course-themes.component.css']
})
export class CourseThemesComponent implements OnInit {

  courseContentForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.courseContentForm = this.formBuilder.group({
      themes: this.formBuilder.array([])
    });
    this.courseThemes.push(new FormGroup({
      courseThemeTitle: new FormControl('', Validators.required),
      courseThemeDescription: new FormControl('', Validators.required)
    }));
  }


  get courseThemes(): FormArray {
    return this.courseContentForm.get('themes') as FormArray;
  }

  onThemeAdded(): void {
    this.courseThemes.push(new FormGroup({
      courseThemeTitle: new FormControl('', Validators.required),
      courseThemeDescription: new FormControl('', Validators.required)
    }));
  }

  onSubmit(): void {

  }

  onThemeDeletion(i: number): void {
    this.courseThemes.removeAt(i);
  }
}
