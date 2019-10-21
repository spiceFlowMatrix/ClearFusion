import { Component, OnInit } from '@angular/core';
import { PMUProjectList, PMUProjectDetail, PmuService } from '../pmu.service';

@Component({
  selector: 'app-implementation',
  templateUrl: './implementation.component.html',
  styleUrls: ['./implementation.component.css']
})
export class ImplementationComponent implements OnInit {

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
