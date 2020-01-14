import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-employee-leave',
  templateUrl: './employee-leave.component.html',
  styleUrls: ['./employee-leave.component.scss']
})
export class EmployeeLeaveComponent implements OnInit {

  leaveListHeaders$ = of(['Id', 'Leave Type', 'Policy Allowed Hours', 'Applied/Approved Hours',
                          'Hour Balance']);
  hideColums$ =  of({
    headers: ['Leave Type', 'Policy Allowed Hours', 'Applied/Approved Hours',
    'Hour Balance'],
    items: ['LeaveType', 'AllowedHours', 'ApprovedHours', 'HourBalance']
  });
  leaveList$: Observable<any[]>;
  actions: TableActionsModel;
  constructor() { }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: true, text: 'ADD LEAVE APPLICATION' },
        delete: false,
        download: false,
        edit: true
      },
      subitems: {
        button: { status: false, text: 'Add Return' },
        delete: false,
        download: false,
        edit: false
      }
    };
  }

  getLeaveBalanceDetails() {

  }
}
