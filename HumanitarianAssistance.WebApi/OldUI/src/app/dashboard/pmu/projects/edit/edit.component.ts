import { Component, OnInit } from '@angular/core';
import { ProjectsService, MeasureType, ProjectType } from '../projects.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  colCountByScreen: Object;
  measuretype: MeasureType[];
  projecttype: ProjectType[];

  constructor(private projectsService: ProjectsService) {
      this.measuretype = this.projectsService.getMeasureType();
      this.projecttype = this.projectsService.getProjectType();
      this.colCountByScreen = {
      md: 2,
      sm: 2
      };
  }

 screen(width) {
  return width < 720 ? "sm" : "md";
}
valueChanged(e) {
  if (e.value) {
      this.colCountByScreen = {
          md: 2,
          sm: 2
      }
  } else {
      this.colCountByScreen = null;
  }
}

ngOnInit() {
}

}
