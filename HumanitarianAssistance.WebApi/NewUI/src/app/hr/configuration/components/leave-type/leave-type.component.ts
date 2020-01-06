import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { HrService } from 'src/app/hr/services/hr.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AddLeaveTypeComponent } from './add-leave-type/add-leave-type.component';
import { ConfigService } from 'src/app/store/services/config.service';

@Component({
  selector: 'app-leave-type',
  templateUrl: './leave-type.component.html',
  styleUrls: ['./leave-type.component.scss']
})
export class LeaveTypeComponent implements OnInit {

  leaveTypeList$: Observable<any[]>;
  leaveTypeHeaders$ = of(['Id', 'Name', 'Description', 'Allowed No Of Hours']);
  hideColums$: Observable<{ headers?: string[], items?: string[] }>;

  actions: TableActionsModel;
  pageModel = {
    PageSize: 10,
    PageIndex: 0
  };
  RecordCount: number;

  constructor(private hrService: HrService, private dialog: MatDialog, private toastr: ToastrService,
    private commonLoader: CommonLoaderService, private configservice: ConfigService) { }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: false,
        edit: true
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false,
      }
  };
  this.getLeaveTypeList();
}
getLeaveTypeList() {
  this.commonLoader.showLoader();
  this.hrService.getLeaveTypeList(this.pageModel).subscribe(x => {
    this.commonLoader.hideLoader();
    this.leaveTypeList$ = of(x.Result.map(element => {
      return {
        LeaveReasonId: element.LeaveReasonId,
        ReasonName: element.ReasonName,
        Description: element.Description,
        Unit: element.Unit,
      };
    }));
    // this.hideColums$ = of({headers: ['Id', 'Department Name', 'Office Name'], items: ['DepartmentId', 'DepartmentName', 'OfficeName']});
    this.RecordCount = x.RecordCount;
  }, error => {
    this.commonLoader.hideLoader();
  });
}

addLeaveType() {
  const dialogRef = this.dialog.open(AddLeaveTypeComponent, {
    width: '450px',
  });

  dialogRef.afterClosed().subscribe(x => {
     this.getLeaveTypeList();
  });
}

actionEvents(event: any) {
  if (event.type === 'edit') {
    const dialogRef = this.dialog.open(AddLeaveTypeComponent, {
      width: '450px',
      data: event.item
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getLeaveTypeList();
    });
  } else if (event.type === 'delete') {
    this.configservice.openDeleteDialog().subscribe(res => {
      if (res) {
        this.commonLoader.showLoader();
        this.hrService.deleteLeaveType(event.item.LeaveReasonId).subscribe(x => {
          this.commonLoader.hideLoader();
          if (x) {
            this.toastr.success('Deleted');
            this.getLeaveTypeList();
          }

          this.RecordCount = x.RecordCount;
        }, error => {
          this.commonLoader.hideLoader();
        });
      }
    });

  }
}

//#region "pageEvent"
pageEvent(e) {
  this.pageModel.PageIndex = e.pageIndex;
  this.pageModel.PageSize = e.pageSize;
  this.getLeaveTypeList();
}
//#endregion
}
