import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProjectListService } from './project-list/service/project-list.service';
import { UIModuleHeaders } from '../../shared/enum';


@Component({
  selector: 'app-project-management',
  templateUrl: './project-management.component.html',
  styleUrls: ['./project-management.component.scss']
})
export class ProjectManagementComponent implements OnInit {
  shoWHideHeader = false;

  setSelectedHeader = UIModuleHeaders.ProjectModule;

  constructor(
    public router: Router,
    public projectListService: ProjectListService,
  ) {
    // NOTE: To hide the header
    this.projectListService.showHideHeader = this.router.url === '/projects' ? true : false;
  }
  ngOnInit() {

  }

  onShowHideHeader(flag: boolean) {
    this.shoWHideHeader = flag;
  }

}
