import { Pipe, PipeTransform } from '@angular/core';
import { IProjectPhasesModel } from 'src/app/dashboard/project-management/project-list/project-activities/service/project-activities.service';

@Pipe({
  name: 'projectPhase'
})
export class ProjectPhasePipe implements PipeTransform {
  transform(value: number, projectPhaseList: IProjectPhasesModel[]): any {
    if (value !== null && projectPhaseList.length > 0) {
      if (projectPhaseList.findIndex(x => x.Id === value) > -1) {
        return projectPhaseList.find(x => x.Id === value).Name;
      }
      return '';
    } else {
      return '';
    }
  }
}
