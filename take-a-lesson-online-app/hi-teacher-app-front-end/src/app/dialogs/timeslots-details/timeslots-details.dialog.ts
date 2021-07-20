import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import {FormArray, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-timeslots-details',
  templateUrl: './timeslots-details.dialog.html',
  styleUrls: ['./timeslots-details.dialog.css']
})
// tslint:disable-next-line:component-class-suffix
export class TimeslotsDetailsDialog implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: { courseFormGroup: FormGroup, viewMode: boolean }) {
  }

  ngOnInit(): void {
    console.log(this.data.courseFormGroup);
  }

  get timeSlots(): FormArray {
    return this.data.courseFormGroup.get('DateTimeSlots') as FormArray;
  }

  onDateTimeSlotDeletion(i: number): void {
    this.timeSlots.removeAt(i);
  }

  addDateTimeSlot(): void {
    this.timeSlots.push(new FormGroup({
        startDateTime: new FormControl('', Validators.required),
        endDateTime: new FormControl('', Validators.required)
      }
    ));
  }
}
