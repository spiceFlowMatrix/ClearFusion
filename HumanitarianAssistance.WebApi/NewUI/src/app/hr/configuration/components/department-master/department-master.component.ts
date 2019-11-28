import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { TableActionsModel } from 'projects/library/src/public_api';
import { of } from 'rxjs/internal/observable/of';
import { HrService } from 'src/app/hr/services/hr.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AddDepartmentMasterComponent } from './add-department-master/add-department-master.component';

@Component({
  selector: 'app-department-master',
  templateUrl: './department-master.component.html',
  styleUrls: ['./department-master.component.scss']
})
export class DepartmentMasterComponent implements OnInit {

  departmentList$: Observable<any[]>;
  departmentHeaders$ = of(['Id', 'Department Name', 'Office Name']);
  hideColums$: Observable<{ headers?: string[], items?: string[] }>;

  actions: TableActionsModel;
  pageModel = {
    PageSize: 10,
    PageIndex: 0
  };
  RecordCount: number;

  constructor(private hrService: HrService, private dialog: MatDialog, private toastr: ToastrService,
    private commonLoader: CommonLoaderService) { }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: false,
        download: false,
        edit: true
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false,
      }
  };
  this.getDepartmentList();
}
getDepartmentList() {
  this.commonLoader.showLoader();
  this.hrService.getDepartmentList(this.pageModel).subscribe(x => {
    this.commonLoader.hideLoader();
    this.departmentList$ = of(x.Result.map(element => {
      return {
        DepartmentId: element.DepartmentId,
        DepartmentName: element.DepartmentName,
        OfficeName: element.OfficeName,
        OfficeId: element.OfficeId
      };
    }));
    this.hideColums$ = of({headers: ['Id', 'Department Name', 'Office Name'], items: ['DepartmentId', 'DepartmentName', 'OfficeName']});
    this.RecordCount = x.RecordCount;
  }, error => {
    this.commonLoader.hideLoader();
  });
}

addDepartment() {
  const dialogRef = this.dialog.open(AddDepartmentMasterComponent, {
    width: '450px',
  });

  dialogRef.afterClosed().subscribe(x => {
     this.getDepartmentList();
  });
}

actionEvents(event: any) {
  if (event.type === 'edit') {
    const dialogRef = this.dialog.open(AddDepartmentMasterComponent, {
      width: '450px',
      data: event.item
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getDepartmentList();
    });
  }
}

//#region "pageEvent"
pageEvent(e) {
  this.pageModel.PageIndex = e.pageIndex;
  this.pageModel.PageSize = e.pageSize;
  this.getDepartmentList();
}
//#endregion
}
