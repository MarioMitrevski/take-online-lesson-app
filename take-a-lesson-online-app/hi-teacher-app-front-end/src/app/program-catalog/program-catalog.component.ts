import {Component, OnDestroy, OnInit} from '@angular/core';
import {PageEvent} from '@angular/material/paginator';
import {ActivatedRoute, Router} from '@angular/router';
import {PageCourse} from '../models/PageCourse.interface';
import {CourseService} from '../services/course.service';
import {Subject, Subscription} from 'rxjs';
import {debounceTime, distinctUntilChanged, map, mergeMap, tap} from 'rxjs/operators';
import {CategoryService} from '../services/category.service';
import {MatSelectChange} from '@angular/material/select';

@Component({
  selector: 'app-program-catalog',
  templateUrl: './program-catalog.component.html',
  styleUrls: ['./program-catalog.component.css']
})
export class ProgramCatalogComponent implements OnInit, OnDestroy {

  sortingTypes: any[] = [
    {value: '3', viewValue: 'Default', sortBy: '', orderBy: ''},
    {value: '0', viewValue: 'Sort By Price(ASC)', sortBy: 'Price', orderBy: 'Asc'},
    {value: '1', viewValue: 'Sort By Price(DESC)', sortBy: 'Price', orderBy: 'Desc'},
    {value: '4', viewValue: 'Sort By Date(ASC)', sortBy: 'Date', orderBy: 'Asc'},
    {value: '5', viewValue: 'Sort By Date(DESC)', sortBy: 'Desc', orderBy: 'Date'}
  ];

  page = 1;
  pageSize = 10;
  length = 100;
  course: PageCourse;
  private subscription: Subscription;
  private categorySubscription: Subscription;
  searchTerm = '';
  private searchTermSubject = new Subject<string>();
  categoryId: any = '';

  constructor(private route: ActivatedRoute, private router: Router, private courseService: CourseService,
              private categoryService: CategoryService) {
  }

  ngOnInit(): void {
    this.subscription = this.courseService.pageCourseLoaded.subscribe(it => {
      this.course = it;
      this.page = it.pageNumber;
      this.length = it.totalRecords;
      this.pageSize = 10;
    });
    this.route.queryParams.subscribe(it => {
        this.courseService.getCourses(it.page, it.pageSize, it.search, it.categoryId, it.sortBy, it.orderBy);
        if (it.search === undefined) {
          this.searchTerm = '';
        } else {
          this.searchTerm = it.search;
        }
      }
    );
    this.searchTermSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(it => {
      this.searchTerm = it;
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: {
          search: it,
        },
        queryParamsHandling: 'merge'
      });
      this.courseService.getCourses(this.page, this.pageSize, it, this.categoryId);
    });
    this.categorySubscription = this.categoryService.categorySelected.subscribe(it => {
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: {
          categoryId: it,
        },
        queryParamsHandling: 'merge'
      });
      this.categoryId = it;
      this.courseService.getCourses(this.page, this.pageSize, this.searchTerm, it);
    });

  }

  onPageChange(event: PageEvent): void {
    this.page = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        page: this.page,
        pageSize: this.pageSize
      },
      queryParamsHandling: 'merge'
    });
    this.courseService.getCourses(this.page, this.pageSize, this.searchTerm, this.categoryId);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    this.categorySubscription.unsubscribe();
  }


  search(term: string): void {
    this.searchTermSubject.next(term);
  }

  onFilterSelected(event: MatSelectChange): void {
    const filterItem = this.sortingTypes.filter(it => it.value === event.value);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        sortBy: filterItem[0].sortBy,
        orderBy: filterItem[0].orderBy
      },
      queryParamsHandling: 'merge'
    });
    this.courseService.getCourses(this.page, this.pageSize, this.searchTerm, this.categoryId, filterItem[0].sortBy, filterItem[0].orderBy);
  }
}
