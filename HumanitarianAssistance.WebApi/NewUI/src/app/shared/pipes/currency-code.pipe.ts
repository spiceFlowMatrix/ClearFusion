import { Pipe, PipeTransform } from '@angular/core';
import { ICurrencyList } from 'src/app/dashboard/accounting/gain-loss-report/gain-loss-report.model';

@Pipe({
  name: 'currencyCode'
})
export class CurrencyCodePipe implements PipeTransform {

  transform(value: number, currencyList: ICurrencyList[]): any {
    if (value !== null && currencyList.length > 0) {
      if (currencyList.findIndex(x => x.CurrencyId === value) > -1) {
        return currencyList.find(x => x.CurrencyId === value).CurrencyCode;
      }
      return '';
    } else {
      return '';
    }
  }

}
