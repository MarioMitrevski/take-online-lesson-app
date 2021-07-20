import {Injectable} from '@angular/core';
import {Observable, Subject} from 'rxjs';
import {CourseGroup} from '../models/courseGroup.interface';
import {CreateCourse} from '../models/createCourse.interface';
import {HttpClient} from '@angular/common/http';
import {PageCourse} from '../models/PageCourse.interface';
import {CourseResponse} from '../models/CourseResponse.interface';
import {Router} from '@angular/router';
import {CourseDetailResponse} from '../models/CourseDetailResponse.interface';
import {CourseForPage} from '../models/CourseForPage.interace';
import {MatDialog} from '@angular/material/dialog';
import {TimeslotsDetailsDialog} from '../dialogs/timeslots-details/timeslots-details.dialog';
import {EnrollFinishedDialog} from '../dialogs/enroll-finished-dialog/enroll-finished.dialog';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  courseGroupsCreated = new Subject<CourseGroup[]>();
  courseGroups: CourseGroup[] = [];
  pageCourseLoaded = new Subject<PageCourse>();
  private url = 'http://localhost:8080/api/courses';

  constructor(private http: HttpClient, private router: Router, public dialog: MatDialog) {
  }

  addCourseGroup(cg: CourseGroup): void {
    this.courseGroups.push(cg);
    this.courseGroupsCreated.next(this.courseGroups);
  }

  removeCourseGroup(courseGroupIndex: number): void {
    this.courseGroups.splice(courseGroupIndex, 1);
    this.courseGroupsCreated.next(this.courseGroups);
  }

  sendCreatedCourseGroups(): void {
    this.courseGroupsCreated.next(this.courseGroups);
  }

  createCourse(createCourse: CreateCourse, coursePhotoReq: FormData): void {
    this.http.post<CourseResponse>(`${this.url}/create`, createCourse).subscribe(it => {
      coursePhotoReq.append('CourseId', it.courseId.toString());
      this.uploadCoursePhoto(coursePhotoReq);
    });
  }

  updateCourse(createCourse: CreateCourse): void {
    this.http.post<CourseResponse>(`${this.url}/create`, createCourse).subscribe();
  }

  uploadCoursePhoto(coursePhotoRequest: FormData): void {
    this.http.post(`${this.url}/uploadFile`, coursePhotoRequest).subscribe(it => {
      this.courseGroups = [];
      this.router.navigate(['programmes']);
    });
  }

  getCourses(page: number = 1, pageSize: number = 10, searchTerm: string = '', categoryId: string = '', sortBy: string = '',
             orderBy: string = ''): void {
    this.http.get<PageCourse>(`${this.url}?PageNumber=${page}&PageSize=${pageSize}&Search=${searchTerm}&CategoryId=${categoryId}&SortBy=${sortBy}&OrderBy=${orderBy}`)
      .subscribe(it => {
        this.pageCourseLoaded.next(it);
      });
  }

  getCourse(courseId: string): Observable<CourseDetailResponse> {
    return this.http.get<CourseDetailResponse>(`${this.url}/course?CourseId=${courseId}`);
  }

  getTeacherCourses(status: string): Observable<CourseForPage> {
    return this.http.get<CourseForPage>(`http://localhost:8080/api/teachers/courses/${status}`);
  }

  getStudentCourses(status: string): Observable<CourseForPage> {
    return this.http.get<CourseForPage>(`http://localhost:8080/api/students/courses/${status}`);
  }


  emptyCourseGroups(): void {
    this.courseGroups = [];
  }

  enrollCourse(courseGroupId: number): void {
    this.http.post(`${this.url}/enroll?CourseGroupId=${courseGroupId}`, {}).subscribe(it => {
      const dialogRef = this.dialog.open(EnrollFinishedDialog, {});
    });
  }


}
