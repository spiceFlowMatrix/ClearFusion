import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { HrService } from 'src/app/hr/services/hr.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AddEducationDegreeComponent } from './add-education-degree/add-education-degree.component';

@Component({
  selector: 'app-education-degree',
  templateUrl: './education-degree.component.html',
  styleUrls: ['./education-degree.component.scss']
})
export class EducationDegreeComponent implements OnInit {

  educationDegreeList$: Observable<any[]>;
  educationDegreeListHeaders$ = of(['Id', 'Name']);
  Id: any;

  actions: TableActionsModel;
  pageModel = {
    PageSize: 10,
    PageIndex: 0
  };
  RecordCount: number;

  constructor(private hrService: HrService, private dialog: MatDialog, private toastr: ToastrService,
    private commonLoader: CommonLoaderService) {

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
    }

  ngOnInit() {
    this.getEducationDegreeList();
  }

  getEducationDegreeList() {
    this.commonLoader.showLoader();
    this.hrService.getEducationDegreeList(this.pageModel).subscribe(x => {
      this.commonLoader.hideLoader();
      this.educationDegreeList$ = of(x.EducationDegreeList.map(element => {
        return {
          Id: element.EducationDegreeId,
          Name: element.EducationDegreeName,
        };
      }));
      this.RecordCount = x.TotalCount;
    }, error => {
      this.commonLoader.hideLoader();
    });
  }

  addDegree() {
    const dialogRef = this.dialog.open(AddEducationDegreeComponent, {
      width: '450px',
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getEducationDegreeList();
    });
}

  actionEvents(event: any) {
    debugger;
    if (event.type === 'delete') {
      this.hrService.openDeleteDialog().subscribe(res => {
        if (res) {
          this.Id = event.item.Id;
          this.hrService.deleteEducationDegree(this.Id).subscribe(res => {
            this.getEducationDegreeList();
          });
        }
      });

    }
    if (event.type === 'edit') {
      const dialogRef = this.dialog.open(AddEducationDegreeComponent, {
        width: '450px',
        data: event.item
      });

      dialogRef.afterClosed().subscribe(x => {
        this.getEducationDegreeList();
      });
    }
  }

  //#region "pageEvent"
  pageEvent(e) {
    this.pageModel.PageIndex = e.pageIndex;
    this.pageModel.PageSize = e.pageSize;
    this.getEducationDegreeList();
  }
  //#endregion

}
