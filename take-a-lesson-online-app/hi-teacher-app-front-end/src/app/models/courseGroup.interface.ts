import {DateTimeSlot} from './DateTimeSlot.interface';

export interface CourseGroup{
  CourseGroupName: string;
  CourseGroupGoogleMeetLink: string;
  MinStudents: number;
  MaxStudents: number;
  DateTimeSlots: DateTimeSlot[];
}
