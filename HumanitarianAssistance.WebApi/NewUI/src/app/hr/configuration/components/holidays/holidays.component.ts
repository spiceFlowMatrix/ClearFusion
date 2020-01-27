import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material';
import { HrService } from 'src/app/hr/services/hr.service';

@Component({
  selector: 'app-holidays',
  templateUrl: './holidays.component.html',
  styleUrls: ['./holidays.component.scss']
})
export class HolidaysComponent implements OnInit {

  constructor(public dialog: MatDialog, public hrservice: HrService) { }
  @ViewChild("addHoliday") addHoliday: TemplateRef<any>;
  selectedDate = new Date();
  startAt = new Date();
  minDate = new Date();
  maxDate = new Date(new Date().setMonth(new Date().getMonth() + 1));
  year: any;
  DayAndDate: string;
  holidays: number[] = [];
  ngOnInit() {
    this.getWeeklyHolidays();
  }

  addHolidayPopup() {
    const diagRef = this.dialog.open(this.addHoliday, {
      width: '500px'
    });
  }
  onSelect(event) {
    console.log(event);
    this.selectedDate = event;
    const dateString = event.toDateString();
    const dateValue = dateString.split(' ');
    this.year = dateValue[3];
    this.DayAndDate = dateValue[0] + ',' + ' ' + dateValue[1] + ' ' + dateValue[2];
  }
  cfDateFilter(d: Date): boolean {
    const day = d.getDay();
    // Prevent Saturday and Sunday from being selected.
    return day !== 0 && day !== 6;
  }
  addWeekend() { }

  getWeeklyHolidays() {
    const officeid = 1;
    this.hrservice.getHolidays(officeid).subscribe(res => {
     console.log(res);
    })
  }
}
