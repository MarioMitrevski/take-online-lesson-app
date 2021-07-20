import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {Category} from '../models/Category.interface';
import {CategoryService} from '../services/category.service';
import {MatSelectionListChange} from '@angular/material/list';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html',
  styleUrls: ['./categories-list.component.css']
})
export class CategoriesListComponent implements OnInit {

  categories$: Observable<Category[]>;

  constructor(private categoryService: CategoryService) {
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }

  onSelect(event: MatSelectionListChange): void {
    this.categoryService.categorySelected.next(event.option.value);
  }
}
