import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatCalendarCellCssClasses } from '@angular/material';
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
    let date = new Date();
    if (this.data.FromDate.includes('/')) {
      date = this.data.FromDate.split('/');
    }

    if (this.data.FromDate.includes('-')) {
      this.selectedDate = this.data.FromDate.replace(/(\d{2})-(\d{2})-(\d{4})/, '$2/$1/$3');
    }

    this.selectedDate = new Date(+date[2], (+date[0]) - 1, +date[1]);
  }

  ngOnInit() {
    // console.log('x', this.datePipe.transform(this.data.ToDate, 'd/M/yyyy'));
    // this.selectedDate = new Date(this.datePipe.transform(this.data.ToDate, 'M/d/yyyy'));
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

  dateClass() {
    return (date: Date): MatCalendarCellCssClasses => {
      const dates = [];
      const fromDate = this.data.FromDate.replace(/(\d{2})-(\d{2})-(\d{4})/, '$2/$1/$3');
      const toDate = this.data.ToDate.replace(/(\d{2})-(\d{2})-(\d{4})/, '$2/$1/$3');

      if (date >= new Date(fromDate) && date <= new Date(toDate)) {
        dates.push(date);
      }

      const highlightDate = dates
        .map(strDate => new Date(strDate))
        .some(d => d.getDate() === date.getDate() && d.getMonth() === date.getMonth() && d.getFullYear() === date.getFullYear());
      return highlightDate ? 'special-date' : '';
    };
  }
}
