import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { HrService } from 'src/app/hr/services/hr.service';
import { MatDialog } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AddQualificationComponent } from './add-qualification/add-qualification.component';

@Component({
  selector: 'app-qualification-master',
  templateUrl: './qualification-master.component.html',
  styleUrls: ['./qualification-master.component.scss']
})
export class QualificationMasterComponent implements OnInit {

  qualificationList$: Observable<any[]>;
  qualificationHeaders$ = of(['Id', 'Qualification Name']);

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
  this.getQualificationList();
}
getQualificationList() {
  this.commonLoader.showLoader();
  this.hrService.getQualificationList(this.pageModel).subscribe(x => {
    this.commonLoader.hideLoader();
    this.qualificationList$ = of(x.Result.map(element => {
      return {
        QualificationId: element.QualificationId,
        QualificationName: element.QualificationName,
      };
    }));
    this.RecordCount = x.RecordCount;
  }, error => {
    this.commonLoader.hideLoader();
  });
}

addQualification() {
  const dialogRef = this.dialog.open(AddQualificationComponent, {
    width: '450px',
  });

  dialogRef.afterClosed().subscribe(x => {
     this.getQualificationList();
  });
}

actionEvents(event: any) {
  if (event.type === 'edit') {
    const dialogRef = this.dialog.open(AddQualificationComponent, {
      width: '450px',
      data: event.item
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getQualificationList();
    });
  }
}

//#region "pageEvent"
pageEvent(e) {
  this.pageModel.PageIndex = e.pageIndex;
  this.pageModel.PageSize = e.pageSize;
  this.getQualificationList();
}
//#endregion
}
