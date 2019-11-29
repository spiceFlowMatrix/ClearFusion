import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { TableActionsModel } from 'projects/library/src/public_api';
import { of } from 'rxjs/internal/observable/of';
import { HrService } from 'src/app/hr/services/hr.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AddProfessionComponent } from './add-profession/add-profession.component';

@Component({
  selector: 'app-profession-master',
  templateUrl: './profession-master.component.html',
  styleUrls: ['./profession-master.component.scss']
})
export class ProfessionMasterComponent implements OnInit {

  professionList$: Observable<any[]>;
  professionHeaders$ = of(['Id', 'Profession Name']);

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
  this.getProfessionList();
}
getProfessionList() {
  this.commonLoader.showLoader();
  this.hrService.getProfessionList(this.pageModel).subscribe(x => {
    this.commonLoader.hideLoader();
    this.professionList$ = of(x.Result.map(element => {
      return {
        ProfessionId: element.ProfessionId,
        ProfessionName: element.ProfessionName,
      };
    }));
    this.RecordCount = x.RecordCount;
  }, error => {
    this.commonLoader.hideLoader();
  });
}

addProfession() {
  const dialogRef = this.dialog.open(AddProfessionComponent, {
    width: '450px',
  });

  dialogRef.afterClosed().subscribe(x => {
     this.getProfessionList();
  });
}

actionEvents(event: any) {
  if (event.type === 'edit') {
    const dialogRef = this.dialog.open(AddProfessionComponent, {
      width: '450px',
      data: event.item
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getProfessionList();
    });
  }
}

//#region "pageEvent"
pageEvent(e) {
  this.pageModel.PageIndex = e.pageIndex;
  this.pageModel.PageSize = e.pageSize;
  this.getProfessionList();
}
//#endregion
}
