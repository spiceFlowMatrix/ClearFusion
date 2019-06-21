import { Component, OnInit } from '@angular/core';
import { PMUProjectList, PMUProjectDetail, PmuService } from '../pmu.service';

@Component({
  selector: 'app-monitoring',
  templateUrl: './monitoring.component.html',
  styleUrls: ['./monitoring.component.css']
})
export class MonitoringComponent implements OnInit {

  pmuProjects: any[];
  popupVisible = false;
  projectPmu: PMUProjectList[];
  pmuProjectDetails: PMUProjectDetail;
  data: any;
  constructor(private pmuservice: PmuService) {
    this.pmuProjects = this.pmuservice.getPMUProjects();
    this.projectPmu = this.pmuservice.getPMUProjectsList();
  }

  ngOnInit() {
  }

  addProjectPMU() {
    this.popupVisible = true;
  }

  cancelDeleteVoucher() {
    this.popupVisible = false;
  }

  selectedFinancialYear(value) {
     
  }

}
