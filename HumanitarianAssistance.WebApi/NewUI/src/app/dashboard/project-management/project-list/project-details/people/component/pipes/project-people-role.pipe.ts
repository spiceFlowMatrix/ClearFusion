import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'projectPeopleRole'
})
export class ProjectPeopleRolePipe implements PipeTransform {

  transform(value: number, projectPeopleRoleList: any[]): any {
    if (value !== null && projectPeopleRoleList.length > 0) {
      if (projectPeopleRoleList.findIndex(x => x.Id === value) > -1) {
        return projectPeopleRoleList.find(x => x.Id === value).Role;
      }
      return '';
    } else {
      return '';
    }
  }
}
