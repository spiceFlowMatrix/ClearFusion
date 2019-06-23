import { Pipe, PipeTransform } from '@angular/core';
import { IUserListModel } from 'src/app/dashboard/project-management/project-list/project-details/models/project-details.model';

@Pipe({
  name: 'users'
})
export class UsersPipe implements PipeTransform {
  transform(value: number, usersList: IUserListModel[]): any {
    if (value !== null && usersList.length > 0) {
      if (usersList.findIndex(x => x.UserId === value) > -1) {
        return usersList.find(x => x.UserId === value).Username;
      }
      return '';
    } else {
      return '';
    }
  }
}
