import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { HrService } from 'src/app/hr/services/hr.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AddAttendanceGroupComponent } from './add-attendance-group/add-attendance-group.component';

@Component({
  selector: 'app-attendance-group-master',
  templateUrl: './attendance-group-master.component.html',
  styleUrls: ['./attendance-group-master.component.scss']
})
export class AttendanceGroupMasterComponent implements OnInit {
  attendanceGroupList$: Observable<any[]>;
  attendanceGroupHeaders$ = of(['Id', 'Attendance Group Name', 'Description']);

  actions: TableActionsModel;
  pageModel = {
    PageSize: 10,
    PageIndex: 0
  };
  RecordCount: number;
  Id: number;
  constructor(
    private hrService: HrService,
    private dialog: MatDialog,
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService
  ) {}

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        download: false,
        edit: true,
        delete: true
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false
      }
    };
    this.getAttendanceGroupList();
  }
  getAttendanceGroupList() {
    this.commonLoader.showLoader();
    this.hrService.getAttendanceGroupList(this.pageModel).subscribe(
      x => {
        this.commonLoader.hideLoader();
        this.attendanceGroupList$ = of(
          x.Result.map(element => {
            return {
              AttendanceGroupId: element.AttendanceGroupId,
              Name: element.Name,
              Description: element.Description
            };
          })
        );
        this.RecordCount = x.RecordCount;
      },
      error => {
        this.commonLoader.hideLoader();
      }
    );
  }

  addAttendanceGroup() {
    const dialogRef = this.dialog.open(AddAttendanceGroupComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getAttendanceGroupList();
    });
  }

  actionEvents(event: any) {
    if (event.type === 'delete') {
      this.hrService.openDeleteDialog().subscribe(res => {
        if (res === true) {
          this.Id = event.item.AttendanceGroupId;
          this.hrService.deleteAttendenceDetail(this.Id).subscribe(response => {
            if (response === true) {
              this.getAttendanceGroupList();
            }
          });
        }
      });
    }
    if (event.type === 'edit') {
      const dialogRef = this.dialog.open(AddAttendanceGroupComponent, {
        width: '450px',
        data: event.item
      });

      dialogRef.afterClosed().subscribe(x => {
        this.getAttendanceGroupList();
      });
    }
  }

  //#region "pageEvent"
  pageEvent(e) {
    this.pageModel.PageIndex = e.pageIndex;
    this.pageModel.PageSize = e.pageSize;
    this.getAttendanceGroupList();
  }
  //#endregion
}
