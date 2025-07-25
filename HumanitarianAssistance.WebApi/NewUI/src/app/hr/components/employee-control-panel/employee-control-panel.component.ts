import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeDetailComponent } from '../employee-detail/employee-detail.component';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeListService } from '../../services/employee-list.service';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { EmployeeTerminationComponent } from '../employee-termination/employee-termination.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-employee-control-panel',
  templateUrl: './employee-control-panel.component.html',
  styleUrls: ['./employee-control-panel.component.scss']
})
export class EmployeeControlPanelComponent implements OnInit {
  @ViewChild(EmployeeDetailComponent) employeeDetail: EmployeeDetailComponent;

  activeTabId = 0;
  employeeId: number;
  employeeStatus = 0;
  constructor(private activatedRoute: ActivatedRoute, private router: Router,
    private employeeListService: EmployeeListService,
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService,
    public dialog: MatDialog) {
    this.activatedRoute.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
    this.activatedRoute.queryParams.subscribe(params => {
      this.activeTabId = +params['tabId'];
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
    const dialogRef = this.dialog.open(EmployeeTerminationComponent, {
      width: '500px',
      autoFocus: false,
      data: {
        EmployeeId: this.employeeId
      }
    });
    dialogRef.afterClosed().subscribe(() => {
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

  resignationStatusChange(value) {
    this.employeeDetail.employeeDetail.IsResigned = value;
  }
}
