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
  dataSource: any;
  projectId: number;
  filterModel: IFilterModel;
  hiringRequestList: HiringList[] = [];
  selection = new SelectionModel<HiringList>(true, []);
  completeRequestModel: CompleteHiringRequestModel;
  hiringRequestListLoader = false;
  selectCheckBoxFlag = false;
  displayHeaderColumns: string[] = [
    'select',
    'HiringRequestId',
    'JobGrade',
    'Position',
    'TotalVacancies',
    'FilledVacancies',
    'PayCurrency',
    'PayRate',
    'Status'
  ];
  constructor(
    public dialog: MatDialog,
    private routeActive: ActivatedRoute,
    private route: Router,
    public hiringRequestService: HiringRequestsService,
    private loader: CommonLoaderService,
    public toastr: ToastrService
  ) {}
  ngOnInit() {
    this.filterModel = {
      FilterValue: '',
      pageIndex: 0,
      pageSize: 50,
      ProjectId: null,
      TotalCount: 0,
      IsInProgress: HiringRequestStatus['In-Progress'],
      IsOpenFlagId: HiringRequestStatus.Open
    };
    this.completeRequestModel = {
      HiringRequestId: [],
      ProjectId: this.projectId
    };
    this.routeActive.parent.parent.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.getAllHiringRequestFilterList(this.filterModel);
  }
  //#region "Get all hiring request filter list"
  getAllHiringRequestFilterList(filterModel: IFilterModel) {
    filterModel.ProjectId = this.projectId;
    filterModel.TotalCount = 0;
    this.hiringRequestList = [];
    this.loader.showLoader();
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
  //#region "Get completed and closed hiring request on tab click"
  onTabClick(event: any) {
    if (event.index === 0) {
      this.selectCheckBoxFlag = false;
      this.filterModel = {
        pageIndex: 0,
        pageSize: 50,
        IsOpenFlagId: HiringRequestStatus.Open,
        IsInProgress: HiringRequestStatus['In-Progress'],
        FilterValue: ''
      };
      this.getAllHiringRequestFilterList(this.filterModel);
    } else if (event.index === 1) {
      this.selectCheckBoxFlag = true;
      this.filterModel = {
        pageIndex: 0,
        pageSize: 50,
        IsOpenFlagId: HiringRequestStatus.Closed,
        IsInProgress: HiringRequestStatus.Completed,
        FilterValue: ''
      };
      this.getAllHiringRequestFilterList(this.filterModel);
    }
  }
  //#endregion
  // #region Add new hiring reqeust"
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
    dialogRef.componentInstance.onAddHiringRequestListRefresh.subscribe(() => {
      this.getAllHiringRequestFilterList(this.filterModel);
    });

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion
  //#region On complete hiring request"
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
            if (responseData.statusCode === 200) {
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
  //#region "On close hiring request"
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
        .subscribe(
          (responseData: IResponseData) => {
            if (responseData.statusCode === 200) {
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
  //#region "On filter applied"
  onFilterApplied(filterModel: IFilterModel) {
    this.getAllHiringRequestFilterList(filterModel);
  }
  //#endregion
  //#region  Paginator event
  pageEvent(e) {
    this.filterModel.pageIndex = e.pageIndex;
    this.filterModel.pageSize = e.pageSize;
    this.onFilterApplied(this.filterModel);
  }
  //#endregion
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
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
  //#region  Navigate to request details page
  requestDetail(e) {
    this.route.navigate([e.HiringRequestId], {
      relativeTo: this.routeActive.parent
    });
  }
  //#endregion
}
