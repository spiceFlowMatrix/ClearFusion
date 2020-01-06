import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { HrService } from 'src/app/hr/services/hr.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AddOfficeMasterComponent } from './add-office-master/add-office-master.component';

@Component({
  selector: 'app-office-master',
  templateUrl: './office-master.component.html',
  styleUrls: ['./office-master.component.scss']
})
export class OfficeMasterComponent implements OnInit {

  officeList$: Observable<any[]>;
  officeHeaders$ = of(['Id', 'Office Code', 'Office Name', 'Supervisor Name', 'Phone No', 'Fax No']);

  actions: TableActionsModel;
  pageModel = {
    PageSize: 10,
    PageIndex: 0
  };
  RecordCount: number;
  Id: number;

  constructor(private hrService: HrService, private dialog: MatDialog, private toastr: ToastrService,
    private commonLoader: CommonLoaderService) { }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        download: false,
        edit: true,
        delete: true,
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false,
      }
    };

    this.getOfficeList();
  }

  getOfficeList() {
    this.commonLoader.showLoader();
    this.hrService.getOfficeList(this.pageModel).subscribe(x => {
      this.commonLoader.hideLoader();
      this.officeList$ = of(x.Result.map(element => {
        return {
          OfficeId: element.OfficeId,
          OfficeCode: element.OfficeCode,
          OfficeName: element.OfficeName,
          SupervisorName: element.SupervisorName,
          PhoneNo: element.PhoneNo,
          FaxNo: element.FaxNo
        };
      }));
      this.RecordCount = x.RecordCount;
    }, error => {
      this.commonLoader.hideLoader();
    });
  }

  addOffice() {
    const dialogRef = this.dialog.open(AddOfficeMasterComponent, {
      width: '450px',
    });

    dialogRef.afterClosed().subscribe(x => {
       this.getOfficeList();
    });
}

  actionEvents(event: any) {
    if (event.type === 'delete') {
      this.hrService.openDeleteDialog().subscribe(res => {
        if (res) {
          this.Id = event.item.OfficeId;
          this.hrService.deleteOfficeDegree(this.Id).subscribe(response => {
            if (response.StatusCode === 200) {
              this.getOfficeList();
            }
          });
        }
      });
    }
    if (event.type === 'edit') {
      const dialogRef = this.dialog.open(AddOfficeMasterComponent, {
        width: '450px',
        data: event.item
      });

      dialogRef.afterClosed().subscribe(x => {
        this.getOfficeList();
      });
    }
  }

  //#region "pageEvent"
  pageEvent(e) {
    this.pageModel.PageIndex = e.pageIndex;
    this.pageModel.PageSize = e.pageSize;
    this.getOfficeList();
  }
  //#endregion

}
