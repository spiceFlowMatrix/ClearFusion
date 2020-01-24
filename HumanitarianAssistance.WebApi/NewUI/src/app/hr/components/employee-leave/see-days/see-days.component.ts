import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-see-days',
  templateUrl: './see-days.component.html',
  styleUrls: ['./see-days.component.scss']
})
export class SeeDaysComponent implements OnInit {

  selectedDate = new Date();
  year: any;
  DayAndDate: string;

  constructor(private dialogRef: MatDialogRef<SeeDaysComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private datePipe: DatePipe) {
    const date = this.data.FromDate.split('/');
    this.selectedDate = new Date(+date[2], (+date[0]) - 1, +date[1]);
  }

  ngOnInit() {
  }

  myFilter = (d: Date): boolean => {
    const date = this.datePipe.transform(d, 'M/d/yyyy');
    if (this.data.ToDate === this.data.FromDate) {
      if (date === this.data.FromDate) {
        return false;
      } else {
        return true;
      }
    } else {
      if (date >= this.data.FromDate && date <= this.data.ToDate) {
        return false;
      } else {
        return true;
      }
    }
  }
}
