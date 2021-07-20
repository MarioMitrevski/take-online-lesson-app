import {CourseGroup} from './courseGroup.interface';
import {CourseTheme} from './courseTheme.interface';

export interface CreateCourse{
  CourseId: any;
  CourseTitle: string;
  CourseDescription: string;
  CategoryId: number;
  PricePerStudentForSession: string;
  CourseThemes: CourseTheme[];
  CourseGroups: CourseGroup[];
}
