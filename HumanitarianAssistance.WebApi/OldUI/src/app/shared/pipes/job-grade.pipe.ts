import { Pipe, PipeTransform } from '@angular/core';
import { JobGradeTypeDropdown } from '../../dashboard/hr/job-hiring-details/job-hiring.service';

@Pipe({
  name: 'jobGrade'
})
export class JobGradePipe implements PipeTransform {

  transform(value: number, list: IDatasource[]): any {
    if (value != null && value !== undefined  && list !== undefined) {

      if (list.length === 0) {
        return '';
      }

      const index = list.findIndex(x => x.Id === value);

      if (index !== -1) {
        return list[index].Name;

      } else {
        return '';
      }

    } else {
      return '';
    }
  }

}

export interface IDatasource {
  Id: number;
  Name: string;
}
