import { Component, OnInit } from '@angular/core';
import { AddJobGradeComponent } from './add-job-grade/add-job-grade.component';
import { of } from 'rxjs/internal/observable/of';
import { HrService } from 'src/app/hr/services/hr.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { TableActionsModel } from 'projects/library/src/public_api';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-job-grade-master',
  templateUrl: './job-grade-master.component.html',
  styleUrls: ['./job-grade-master.component.scss']
})
export class JobGradeMasterComponent implements OnInit {

  jobGradeList$: Observable<any[]>;
  jobGradeHeaders$ = of(['Id', 'Grade Name']);

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
  this.getJobGradeList();
}
getJobGradeList() {
  this.commonLoader.showLoader();
  this.hrService.getJobGradeList(this.pageModel).subscribe(x => {
    this.commonLoader.hideLoader();
    this.jobGradeList$ = of(x.Result.map(element => {
      return {
        GradeId: element.GradeId,
        GradeName: element.GradeName,
      };
    }));
    this.RecordCount = x.RecordCount;
  }, error => {
    this.commonLoader.hideLoader();
  });
}

addJobGrade() {
  const dialogRef = this.dialog.open(AddJobGradeComponent, {
    width: '450px',
  });

  dialogRef.afterClosed().subscribe(x => {
     this.getJobGradeList();
  });
}

actionEvents(event: any) {
  if (event.type === 'delete') {
    this.hrService.openDeleteDialog().subscribe(res => {
      if (res === true) {
        this.Id = event.item.GradeId;
        this.hrService.deleteJobGradeDetail(this.Id).subscribe(response => {
         if (response === true) {
          this.getJobGradeList();
        }
        });
      }
    });
  }
  if (event.type === 'edit') {
    const dialogRef = this.dialog.open(AddJobGradeComponent, {
      width: '450px',
      data: event.item
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getJobGradeList();
    });
  }
}

//#region "pageEvent"
pageEvent(e) {
  this.pageModel.PageIndex = e.pageIndex;
  this.pageModel.PageSize = e.pageSize;
  this.getJobGradeList();
}
//#endregion

}
