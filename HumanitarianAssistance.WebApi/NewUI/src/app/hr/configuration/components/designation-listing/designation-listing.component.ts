import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { HrService } from 'src/app/hr/services/hr.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AddDesignationComponent } from '../add-designation/add-designation.component';
import { TableActionsModel } from 'projects/library/src/public_api';

@Component({
  selector: 'app-designation-listing',
  templateUrl: './designation-listing.component.html',
  styleUrls: ['./designation-listing.component.scss']
})
export class DesignationListingComponent implements OnInit {

  designationList$: Observable<any[]>;
  subListHeaders: Observable<any[]>;

  designationListHeaders$ = of(['Id', 'Name', 'Description']);
  subListHeaders$ = of(['Id', 'Question']);

  actions: TableActionsModel;
  pageModel = {
    PageSize: 10,
    PageIndex: 0
  };
  RecordCount: number;

  constructor(private hrService: HrService, private dialog: MatDialog, private toastr: ToastrService) {

  }

  ngOnInit() {
    this.getDesignationList();

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
  }

  getDesignationList() {
    this.hrService.getDesignationList(this.pageModel).subscribe(x => {
      this.designationList$ = of(x.DesignationList.map(element => {
        return {
          Id: element.DesignationId,
          Designation: element.Designation,
          Description: element.Description,
          subItems: element.TechnicalQuestionList ? (element.TechnicalQuestionList.map((r) => {
            return {
              QuestionId: r.QuestionId,
              Question: r.Question
            };
          })) : null
        };
      }));
      this.RecordCount = x.RecordCount;
    });
  }

  addDesignation() {
      const dialogRef = this.dialog.open(AddDesignationComponent, {
        width: '650px',
      });

      dialogRef.afterClosed().subscribe(x => {
        this.getDesignationList();
      });
  }

  actionEvents(event: any) {
    if (event.type === 'edit') {
      const dialogRef = this.dialog.open(AddDesignationComponent, {
        width: '650px',
        data: event.item
      });

      dialogRef.afterClosed().subscribe(x => {
        this.getDesignationList();
      });
    }
  }

  //#region "pageEvent"
  pageEvent(e) {
    this.pageModel.PageIndex = e.pageIndex;
    this.pageModel.PageSize = e.pageSize;
    this.getDesignationList();
  }
  //#endregion
}
