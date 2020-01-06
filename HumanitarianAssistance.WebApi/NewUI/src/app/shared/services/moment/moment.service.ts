import { Injectable } from '@angular/core';
import * as moment from 'moment/moment';

@Injectable({
  providedIn: 'root'
})
export class MomentService {

  constructor() { }

  utcFormatDate(date: any) {
    try {
      const formatedDate = moment(date)
        .utc().local()
        .format();
      return formatedDate;
    } catch (error) {
      console.log('Format is not correct');
    }
  }

  /**Formate type are :
   * && LTS => hh:mm:ss PM/AM
   * && L => DD/MM/YYYY
   * && ll => MM(Name) DD/YYYY
   * && LLLL => DD(Name),MM(Name) MM, YY hh:mm PM/AM*/
  utcFormatDateByFormatType(date: any, type: string) {
    try {
      const formatedDate = moment(date)
        .utc().local()
        .format(type);
      return formatedDate;
    } catch (error) {
      console.log('Format is not correct');
    }
  }
  DiffrenceBetweenTwoDate(fromDate: any, ToDate: any) {
    try {
      fromDate = moment(fromDate).format('L');
      ToDate = moment(ToDate).format('L');
      const FirstDate = moment(fromDate);
      const SecondDate = moment(ToDate);
      const years = SecondDate.diff(FirstDate, 'years');
      FirstDate.add(years, 'years');
      const months = SecondDate.diff(FirstDate, 'months');
      FirstDate.add(months, 'months');
      const days = SecondDate.diff(FirstDate, 'days');

      return years + ' Years ' + months + ' Month ' + days + ' Days';
    } catch (error) {
      console.log('Format is not correct');
    }
  }
}
