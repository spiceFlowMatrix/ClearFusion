import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeDetailComponent } from '../employee-detail/employee-detail.component';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeListService } from '../../services/employee-list.service';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-employee-control-panel',
  templateUrl: './employee-control-panel.component.html',
  styleUrls: ['./employee-control-panel.component.scss']
})
export class EmployeeControlPanelComponent implements OnInit {
  @ViewChild(EmployeeDetailComponent) employeeDetail: EmployeeDetailComponent;

  employeeId: number;
  employeeStatus = 0;
  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private employeeListService: EmployeeListService,
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService) {
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

  terminateEmployee() {
    if (this.employeeStatus === 3) {
      return;
    }
    this.commonLoader.showLoader();
    this.employeeListService.terminateEmployeeByEmployeeeId(this.employeeId).subscribe(res => {
      if (res) {
        this.commonLoader.hideLoader();
        this.toastr.success('Employee Terminated Successfully!');
        this.router.navigate(['/hr/employees']);
      }
    }, err => {
      this.commonLoader.hideLoader();
    });
  }

  deleteEmployee() {
    this.commonLoader.showLoader();
    this.employeeListService.deleteEmployeeByEmployeeeId(this.employeeId).subscribe(res => {
      if (res) {
        this.commonLoader.hideLoader();
        this.toastr.success('Employee Deleted Successfully!');
        this.router.navigate(['/hr/employees']);
      }
    }, err => {
      this.commonLoader.hideLoader();
    });
  }

  employeeStatusChange(value) {
    this.employeeStatus = value;
  }
}
