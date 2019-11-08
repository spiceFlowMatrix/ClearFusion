import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { IFilterModel, HiringList } from '../models/hiring-requests-models';

import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AddHiringRequestComponent } from '../add-hiring-request/add-hiring-request.component';
import { MatDialog, MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-hiring-requests',
  templateUrl: './hiring-requests.component.html',
  styleUrls: ['./hiring-requests.component.scss']
})
export class HiringRequestsComponent implements OnInit {
  constructor(
    public dialog: MatDialog,
    private routeActive: ActivatedRoute,
    public hiringRequestService: HiringRequestsService,
    private loader: CommonLoaderService
  ) {}
  projectId: number;
  filterModel: IFilterModel;
  selectedHiringRequestId: number[] = [];
  // 07-11-19
  displayHeaderColumns: string[] = [
    'select',
    'HiringRequestId',
    'JobCode',
    'JobGrade',
    'Position',
    'TotalVacancies',
    'FilledVacancies',
    'PayCurrency',
    'PayRate',
    'Status'
  ];

  hiringRequestList: HiringList[] = [];
  dataSource: any;
  // **

  hiringListHeaders$ = of([
    'Hiring Request Id',
    'Job Code',
    'Job Grade',
    'Position',
    'Total Vacancies',
    'Filled Vacancies',
    'Pay Currency',
    'Pay Rate',
    'Status'
  ]);
  hiringList$: Observable<HiringList[]>;

  selection = new SelectionModel<HiringList>(true, []);
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    debugger;
    console.log('this.selection.selected', this.selection.selected);
    const numSelected = this.selection.selected.length;
    const numRows = this.hiringRequestList.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    debugger;
    this.isAllSelected()
      ? this.selection.clear()
      : this.hiringRequestList.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {
    debugger;
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${
      this.selection.isSelected(row) ? 'deselect' : 'select'
    } row ${row.HiringRequestId + 1}`;
  }

  ngOnInit() {
    this.filterModel = {
      FilterValue: '',
      pageIndex: 0,
      pageSize: 10,
      ProjectId: null,
      TotalCount: 0
    };
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.getAllHiringRequestFilterList();
  }

  //#region  paginatorEvent
  pageEvent(e) {
    debugger;
    this.filterModel.pageIndex = e.pageIndex;
    this.filterModel.pageSize = e.pageSize;
    this.onFilterApplied();
  }
  //#endregion

  //#region "onFilterApplied"
  onFilterApplied() {
    this.getAllHiringRequestFilterList();
  }
  //#endregion

  //#region "getAllProjectActivityList"
  getAllHiringRequestFilterList() {
    debugger;
    this.filterModel.ProjectId = this.projectId;
    this.filterModel.TotalCount = 0;
    this.hiringRequestList = [];
    //   this.hiringRequestListLoader = true;
    this.hiringRequestService
      .GetProjectHiringRequestFilterList(this.filterModel)
      .subscribe(
        (response: IResponseData) => {
          this.loader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.filterModel.TotalCount =
              response.total != null ? response.total : 0;
            response.data.forEach(element => {
              this.hiringRequestList.push({
                HiringRequestId: element.HiringRequestId,
                JobCode: element.JobCode,
                JobGrade: element.JobGrade,
                Position: element.Position,
                TotalVacancies: element.TotalVacancies,
                FilledVacancies: element.FilledVacancies,
                PayCurrency: element.PayCurrency,
                PayRate: element.PayRate,
                Status: element.Status
              });
            });
            // to bind data in datasource in mat table
            this.dataSource = new MatTableDataSource<any>(
              this.hiringRequestList
            );
          }
          this.loader.hideLoader();
        },
        error => {
          this.loader.hideLoader();
        }
      );
  }
  //#endregion

  // #region adding new hiring request
  addNewHiringRequest(): void {
    // NOTE: It open AddHiringRequest dialog and passed the data into the AddHiringRequestsComponent Model
    const dialogRef = this.dialog.open(AddHiringRequestComponent, {
      width: '700px',
      autoFocus: false,
      data: {
        hiringRequestId: 0,
        projectId: this.projectId
      }
    });

    // refresh the list after new request created
    dialogRef.componentInstance.onAddHiringRequestListRefresh.subscribe(() => {
      // do something
      this.getAllHiringRequestFilterList();
    });

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  requestDetail(e) {
    // console.log(e.HiringRequestId);
    // this.router.navigate(['../hiring-request/' + e.HiringRequestId]);
  }

  // 07-09-2019
  //#region "onTabClick"
  onTabClick(event: any) {
    console.log(event);
  }
  //#endregion

  //#region onComplteRequest
  onComplteRequest() {
    this.selectedHiringRequestId = [];
    console.log('id', this.selection.selected);
    if (
      this.selection.selected.length > 0 &&
      this.selection.selected.length !== undefined
    ) {
      this.selection.selected.forEach(element => {
        this.selectedHiringRequestId.push(element.HiringRequestId);
      });
      this.hiringRequestService
        .IsCompltedeHrDEtail(this.selectedHiringRequestId)
        .subscribe((responseData: IResponseData) => {
          if (responseData.statusCode === 200 && responseData.data !== null) {

          }
        });
    }
  }
  //#endregion
}
