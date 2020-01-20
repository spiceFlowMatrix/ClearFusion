import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeDetailComponent } from '../employee-detail/employee-detail.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employee-control-panel',
  templateUrl: './employee-control-panel.component.html',
  styleUrls: ['./employee-control-panel.component.scss']
})
export class EmployeeControlPanelComponent implements OnInit {

  @ViewChild(EmployeeDetailComponent) employeeDetail: EmployeeDetailComponent;

  employeeId: number;
  constructor(private activatedRoute: ActivatedRoute) {
      this.activatedRoute.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
  }

  ngOnInit() {
  }

  showEmployeeDetails() {
   this.employeeDetail.show();
   this.employeeDetail.employeeDetail
  }
}
