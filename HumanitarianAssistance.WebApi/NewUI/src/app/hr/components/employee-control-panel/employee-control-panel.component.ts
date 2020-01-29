import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeDetailComponent } from '../employee-detail/employee-detail.component';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-employee-control-panel',
  templateUrl: './employee-control-panel.component.html',
  styleUrls: ['./employee-control-panel.component.scss']
})
export class EmployeeControlPanelComponent implements OnInit {
  @ViewChild(EmployeeDetailComponent) employeeDetail: EmployeeDetailComponent;

  employeeId: number;
  constructor(private activatedRoute: ActivatedRoute, private router: Router) {
    this.activatedRoute.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
  }

  ngOnInit() {}

  showEmployeeDetails() {
    this.employeeDetail.show();
  }
  editEmployee() {
    this.router.navigate(['/hr/addEmployee'], { queryParams: { empId: this.employeeId } });
  }

  backClick() {
    this.router.navigate(['/hr/employees']);
  }
}
