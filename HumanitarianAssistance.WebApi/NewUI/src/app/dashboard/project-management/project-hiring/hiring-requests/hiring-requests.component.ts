import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import {
  IFilterModel,
  HiringList,
  CompleteHiringRequestModel
} from '../models/hiring-requests-models';

import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AddHiringRequestComponent } from '../add-hiring-request/add-hiring-request.component';
import { MatDialog, MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { HiringRequestStatus } from 'src/app/shared/enum';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-hiring-requests',
  templateUrl: './hiring-requests.component.html',
  styleUrls: ['./hiring-requests.component.scss']
})
export class HiringRequestsComponent implements OnInit {
  constructor(
    public dialog: MatDialog,
    private routeActive: ActivatedRoute,
    private route: Router,
    public hiringRequestService: HiringRequestsService,
    private loader: CommonLoaderService,
    public toastr: ToastrService
  ) {}
  projectId: number;
  filterModel: IFilterModel;
  completeRequestModel: CompleteHiringRequestModel;
  hiringRequestListLoader = false;
  selectCheckBoxFlag = false;
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

  // hiringListHeaders$ = of([
  //   'Hiring Request Id',
  //   'Job Code',
  //   'Job Grade',
  //   'Position',
  //   'Total Vacancies',
  //   'Filled Vacancies',
  //   'Pay Currency',
  //   'Pay Rate',
  //   'Status'
  // ]);
  // hiringList$: Observable<HiringList[]>;

  selection = new SelectionModel<HiringList>(true, []);
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    // console.log('this.selection.selected', this.selection.selected);
    const numSelected = this.selection.selected.length;
    const numRows = this.hiringRequestList.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.hiringRequestList.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {
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
      TotalCount: 0,
      IsInProgress: HiringRequestStatus['In-Progress'],
      IsOpenFlagId: HiringRequestStatus.Open
    };

    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.getAllHiringRequestFilterList(this.filterModel);
    this.completeRequestModel = {
      HiringRequestId: [],
      ProjectId: this.projectId
    };
  }

  //#region  paginatorEvent
  pageEvent(e) {
    this.filterModel.pageIndex = e.pageIndex;
    this.filterModel.pageSize = e.pageSize;
    this.onFilterApplied(this.filterModel);
  }
  //#endregion

  //#region "onFilterApplied"
  onFilterApplied(filterModel: IFilterModel) {
    this.getAllHiringRequestFilterList(filterModel);
  }
  //#endregion

  //#region "getAllProjectActivityList"
  getAllHiringRequestFilterList(filterModel: IFilterModel) {
    filterModel.ProjectId = this.projectId;
    filterModel.TotalCount = 0;
    this.hiringRequestList = [];
     this.loader.showLoader();
    //  this.hiringRequestListLoader = true;
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
                Status: HiringRequestStatus[element.HiringRequestStatus]
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
      this.getAllHiringRequestFilterList(this.filterModel);
    });

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  requestDetail(e) {
    // console.log(e.HiringRequestId);
    // this.route.navigate([e.HiringRequestId], { relativeTo: this.routeActive });
    // this.router.navigate(['../hiring-request/' + e.HiringRequestId]);
  }

  // 07-09-2019
  //#region "onTabClick"
  onTabClick(event: any) {
    if (event.index === 0) {
      this.selectCheckBoxFlag = false;
      this.filterModel = {
        pageIndex: 0,
        pageSize: 10,
        IsOpenFlagId: HiringRequestStatus.Open,
        IsInProgress: HiringRequestStatus['In-Progress'],
        FilterValue: ''
      };
      this.getAllHiringRequestFilterList(this.filterModel);
    } else if (event.index === 1) {
      this.selectCheckBoxFlag = true;
      this.filterModel = {
        pageIndex: 0,
        pageSize: 10,
        IsOpenFlagId: HiringRequestStatus.Closed,
        IsInProgress: HiringRequestStatus.Completed,
        FilterValue: ''
      };
      this.getAllHiringRequestFilterList(this.filterModel);
    }
  }
  //#endregion

  //#region onComplteRequest
  onComplteRequest() {
    this.completeRequestModel = {
      HiringRequestId: [],
      ProjectId: this.projectId
    };
    if (
      this.selection.selected.length > 0 &&
      this.selection.selected.length !== undefined
    ) {
      this.selection.selected.forEach(element => {
        this.completeRequestModel.HiringRequestId.push(element.HiringRequestId);
      });
      this.hiringRequestService
        .IsCompltedeHrDetail(this.completeRequestModel)
        .subscribe(
          (responseData: IResponseData) => {
            if (responseData.statusCode === 200 ) {
              this.getAllHiringRequestFilterList(this.filterModel);
            } else if (responseData.statusCode === 400) {
              this.toastr.error('Something went wrong .Please try again.');
            }
          },
          error => {}
        );
    }
  }
  //#endregion
  //#region "onCloseRequest"
  onCloseRequest() {
    this.completeRequestModel = {
      HiringRequestId: [],
      ProjectId: this.projectId
    };
    if (
      this.selection.selected.length > 0 &&
      this.selection.selected.length !== undefined
    ) {
      this.selection.selected.forEach(element => {
        this.completeRequestModel.HiringRequestId.push(element.HiringRequestId);
      });
      this.hiringRequestService
        .IsCloasedHrDetail(this.completeRequestModel)
        .subscribe((responseData: IResponseData) => {
          if (responseData.statusCode === 200) {
            this.getAllHiringRequestFilterList(this.filterModel);
          } else if (responseData.statusCode === 400) {
            this.toastr.error('Something went wrong .Please try again.');
          }
        },
        error => { }
        );
    }
  }
  //#endregion
}
