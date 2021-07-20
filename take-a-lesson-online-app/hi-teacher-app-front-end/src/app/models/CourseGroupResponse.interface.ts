import {DateTimeSlotResponse} from './DateTimeSlotResponse.interface';

export interface CourseGroupResponse {
  courseGroupId: number;
  courseGroupName: string;
  courseGroupStatus: string;
  courseId: number;
  courseGroupGoogleMeetLink: string;
  dateTimeSlots: DateTimeSlotResponse[];
  enrolledStudents: number;
  maxStudents: number;
  minStudents: number;
}
