import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { Observable } from 'rxjs/internal/Observable';
import { HrLeaveService } from '../../services/hr-leave.service';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeLeaveAddComponent } from './employee-leave-add/employee-leave-add.component';
import { AddLeaveTypeComponent } from '../../configuration/components/leave-type/add-leave-type/add-leave-type.component';
import { AssignLeaveComponent } from './assign-leave/assign-leave.component';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-employee-leave',
  templateUrl: './employee-leave.component.html',
  styleUrls: ['./employee-leave.component.scss']
})
export class EmployeeLeaveComponent implements OnInit {

  leaveListHeaders$ = of(['Id', 'Leave Type', 'Policy Allowed Hours', 'Applied/Approved Hours',
    'Hour Balance']);
    appliedleaveListHeaders$ = of(['Id', 'Leave Type', 'Applied Hours', 'Status']);
  hideColums$ = of({
    headers: ['Leave Type', 'Policy Allowed Hours', 'Applied/Approved Hours',
      'Hour Balance'],
    items: ['LeaveType', 'AllowedHours', 'ApprovedHours', 'HourBalance',]
  });
  hideColumsAppliedHours$ = of({
    headers: ['Leave Type', 'Applied Hours', 'Status'],
    items: ['LeaveType', 'AppliedHours', 'Status']
  });
  leaveList$: Observable<any[]>;
  assignedLeaveList$: Observable<any[]>;
  actions: TableActionsModel;
  employeeId: number;
  constructor(private hrLeave: HrLeaveService, private activatedRoute: ActivatedRoute, private dialog: MatDialog,
    private toastr: ToastrService, private datePipe: DatePipe) {
    this.activatedRoute.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
  }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: true, text: 'ADD LEAVE APPLICATION' },
        delete: false,
        download: false,
        edit: false
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false,
        edit: false
      }
    };

    this.getLeaveBalanceDetails();
    this.GetAllLeaveDetails();
  }

  actionEvents(event) {
    if (event.type === 'button') {
      const dialogRef = this.dialog.open(EmployeeLeaveAddComponent, {
        width: '450px',
        height: '450px',
        data: {
          EmployeeId: this.employeeId,
          LeaveReasonId: event.item.Id,
          LeaveType: event.item.LeaveType,
          HourBalance: event.item.HourBalance
        }
      });

      dialogRef.afterClosed().subscribe(result => {
        this.getLeaveBalanceDetails();
        this.GetAllLeaveDetails();
      });
    }
  }

  getLeaveBalanceDetails() {
    this.hrLeave
      .getEmployeeBalanceLeave(this.employeeId)
      .subscribe(data => {
        this.leaveList$ = of(data.data.AssignLeaveToEmployeeList.map((element) => {
          return {
            Id: element.LeaveReasonId,
            LeaveType: element.LeaveReasonName,
            AllowedHours: element.Unit,
            ApprovedHours: element.AssignUnit,
            HourBalance: element.BlanceLeave
          };
        })
        );
      }, error => {
        this.toastr.warning(error);
      });
  }

  // Leave Details
  //#region "Get All Leave Details"
  GetAllLeaveDetails() {
    this.hrLeave
      .getAllLeaveInfoById(this.employeeId)
      .subscribe(x => {
        if (x && x.LeaveList.length > 0) {
            this.assignedLeaveList$ = of(x.LeaveList.map((element) => {
              return {
                Id: element.ApplyLeaveId,
                LeaveType: element.LeaveReasonName,
                AppliedHours: element.LeaveHoursCount,
                Status: element.ApplyLeaveStatus,
                StatusId: element.ApplyLeaveStatusId,
                FromDate: element.FromDate,
                ToDate: element.ToDate,
                itemAction: (!element.ApplyLeaveStatusId) ? ([
                  {
                    button: {
                      status: true,
                      text: 'SEE DAYS',
                      type: 'text'
                    },
                    delete: false,
                    download: false,
                    edit: false
                  },
                  {
                    button: {
                      status: true,
                      text: 'APPROVE',
                      type: 'save'
                    },
                    delete: false,
                    download: false,
                    edit: false
                  },
                  {
                    button: {
                      status: true,
                      text: 'REJECT',
                      type: 'cancel'
                    },
                    delete: false,
                    download: false,
                    edit: false
                  },
                ]) : ([
                    {
                      button: {
                        status: true,
                        text: 'SEE DAYS',
                        type: 'text'
                      },
                      delete: false,
                      download: false,
                      edit: false
                    }])
              };
            })
            );
        }
      }, error => {
        this.toastr.warning(error);
      });
  }
  //#endregion

  appliedLeaveActionEvents(event) {
    if (event.type === 'SEE DAYS') {
      return;
    }
    const model = {
      Id: event.item.Id,
      EmployeeId: this.employeeId,
      Approved: false
    };
    if (event.type === 'APPROVE') {
      model.Approved = true;
    } else if (event.type === 'REJECT') {
      model.Approved = false;
    }

    this.hrLeave.approveRejectLeave(model).subscribe(x => {
      if (x) {
        this.getLeaveBalanceDetails();
        this.GetAllLeaveDetails();
      }
    }, error => {
      this.toastr.warning(error);
    });
  }

  assignLeave() {
    const dialogRef = this.dialog.open(AssignLeaveComponent, {
      width: '450px',
      data: {
        EmployeeId: this.employeeId,
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getLeaveBalanceDetails();
    });
  }
}
