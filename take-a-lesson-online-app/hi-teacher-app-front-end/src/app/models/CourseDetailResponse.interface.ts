import {CategoryResponseInterface} from './CategoryResponse.interface';
import {CourseGroupResponse} from './CourseGroupResponse.interface';
import {CourseThemeResponse} from './CourseThemeResponse.interface';
import {CourseTeacherDetailsResponse} from './CourseTeacherDetailsResponse.interface';

export interface CourseDetailResponse{
  courseId: number;
  category: CategoryResponseInterface;
  courseDescription: string;
  courseGroups: CourseGroupResponse[];
  courseThemes: CourseThemeResponse[];
  courseStatus: string;
  courseTitle: string;
  imageUrl: string;
  pricePerStudentForSession: number;
  startDate: string;
  teacher: CourseTeacherDetailsResponse;
  teacherId: number;
}
