import {CourseForPage} from './CourseForPage.interace';

export interface PageCourse{
  pageNumber: number;
  pageSize: number;
  totalPages: 1;
  totalRecords: 2;
  data: CourseForPage[];
}
