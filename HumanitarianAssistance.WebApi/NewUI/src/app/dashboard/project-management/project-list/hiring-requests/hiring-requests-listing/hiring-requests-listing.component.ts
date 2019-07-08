import { Component, OnInit, HostListener, EventEmitter } from '@angular/core';
import { MatDialog } from '@angular/material';
import { AddHiringRequestsComponent } from '../add-hiring-requests/add-hiring-requests.component';
import { HiringRequestsService } from '../hiring-requests.service';
import {
  ICurrencyListModel,
  IBudgetLineModel,
  IOfficeListModel,
  IJobGradeModel,
  ProjectHiringRequestFilterModel,
  IHiringRequestDetailModel,
  IProfessionList
} from '../models/hiring-requests-model';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-hiring-requests-listing',
  templateUrl: './hiring-requests-listing.component.html',
  styleUrls: ['./hiring-requests-listing.component.scss']
})
export class HiringRequestsListingComponent implements OnInit {
  //#region "variables
  colsm6 = 'col-sm-10 col-sm-offset-1';
  showHiringReuestDetail = false;

  onListRefresh = new EventEmitter();
  projectId: number;

  //#endregion

  // screen sizes
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  selectedHiringRequestId: number;
  currencyList: ICurrencyListModel[] = [];
  budgetLineList: IBudgetLineModel[] = [];
  officeList: IOfficeListModel[] = [];
  jobGradeList: IJobGradeModel[] = [];
  professionList: IProfessionList[] = [];

  // model
  projectHiringRequestFilter: ProjectHiringRequestFilterModel;
  hiringRequestModel: ProjectHiringRequestFilterModel;

  hiringRequestListLoader = false;

  hiringRequestlist: ProjectHiringRequestFilterModel[] = [];

  hiringRequestDetail: ProjectHiringRequestFilterModel;

  constructor(
    public dialog: MatDialog,
    public hiringRequestService: HiringRequestsService,
    private routeActive: ActivatedRoute
  ) {
    this.getScreenSize();
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 170 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion
  ngOnInit() {
    this.initForm();
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.getAllHiringRequestFilterList();
    this.getCurrencyList();
    this.getBudgetLineList();
    this.getOfficeList();
    this.getJobGradeList();
    this.getProfessionlist();
  }

  //#region  "initForm"
  initForm() {
    this.hiringRequestModel = {
      pageIndex: 0,
      pageSize: 10,
      totalCount: 0,

      FilterValue: '',
      Description: null,
      Position: null,
      ProfessionId: null,
      TotalVacancies: null,
      FilledVacancies: null,
      BasicPay: null,
      CurrencyId: null,
      BudgetLineId: null,
      GradeId: null,
      EmployeeID: null,
      HiringRequestCode: null,
      HiringRequestId: null,
      IsCompleted: null,
      OfficeId: null,
      ProjectId: null,
      BudgetName: null,
      CurrencyName: null,
      EmployeeName: null,
      GradeName: null,
      RequestedBy: null,
    };
  }
  //#endregion

//#region  paginatorEvent
pageEvent(e) {
  this.hiringRequestModel.pageIndex = e.pageIndex;
  this.hiringRequestModel.pageSize = e.pageSize;

  this.onFilterApplied();
}
//#endregion

//#region "onFilterApplied"
onFilterApplied() {
  this.getAllHiringRequestFilterList();
}
//#endregion

  //#region "onItemClick"
  onItemClick(item: any) {
    console.log(item);
    this.selectedHiringRequestId = item.HiringRequestId;
    this.hiringRequestDetail = item;
    this.showHiringRequestDetailPanel();
  }
  //#endregion

  //#region "showHiringRequestDetailPanel"
  showHiringRequestDetailPanel() {
    this.showHiringReuestDetail = true;
    this.colsm6 = this.showHiringReuestDetail
      ? 'col-sm-6'
      : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "onAddNewRequestClicked"
  onAddNewRequestClicked() {
    this.openHiringRequestDialog();
  }
  //#endregion

  //#region "openHiringRequestDialog"
  openHiringRequestDialog(): void {
    // NOTE: It passed the data into the Add Activity Model
    const dialogRef = this.dialog.open(AddHiringRequestsComponent, {
      width: '550px',
      autoFocus: false,
      data: {
        BudgetLineList: this.budgetLineList,
        OfficeList: this.officeList,
        CurrencyList: this.currencyList,
        JobGradeList: this.jobGradeList,
        ProjectId: this.projectId,
        ProfessionList: this.professionList
      }
    });

    // refresh the list after new request created
    dialogRef.componentInstance.onHiringRequestListRefresh.subscribe(() => {
      // do something
      this.getAllHiringRequestFilterList();
    });

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "getCurrencyList"
  getCurrencyList() {
    this.hiringRequestService.GetCurrencyList().subscribe(
      (response: IResponseData) => {
        this.currencyList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.currencyList.push({
              CurrencyId: element.CurrencyId,
              CurrencyName: element.CurrencyName,
              CurrencyCode: element.CurrencyCode
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getBudgetLineList"
  getBudgetLineList() {
    this.hiringRequestService.GetBudgetLineList(this.projectId).subscribe(
      (response: IResponseData) => {
        this.budgetLineList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach((element: IBudgetLineModel) => {
            this.budgetLineList.push({
              BudgetCode: element.BudgetCode,
              BudgetLineId: element.BudgetLineId,
              BudgetName: element.BudgetName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getOfficeList"
  getOfficeList() {
    this.hiringRequestService.GetOfficeList().subscribe(
      (response: IResponseData) => {
        this.officeList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.officeList.push({
              OfficeId: element.OfficeId,
              OfficeName: element.OfficeName,
              OfficeCode: element.OfficeCode,
              OfficeCodeName: element.OfficeCodeName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getJobGradeList"
  getJobGradeList() {
    this.hiringRequestService.GetJobGradeList().subscribe(
      (response: IResponseData) => {
        this.jobGradeList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.jobGradeList.push({
              GradeId: element.GradeId,
              GradeName: element.GradeName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getProfessionlist"

  getProfessionlist() {
    this.hiringRequestService.GetProfessionList().subscribe(
      (response: IResponseData) => {
        this.professionList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.professionList.push({
              ProfessionId: element.ProfessionId,
              ProfessionName: element.ProfessionName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getAllProjectActivityList"
  getAllHiringRequestFilterList() {
    this.hiringRequestModel.totalCount = 0;
    this.hiringRequestModel.ProjectId = this.projectId;

    this.hiringRequestListLoader = true;
    this.hiringRequestService
      .GetProjectHiringRequestFilterList(this.hiringRequestModel)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data !== null) {
            this.hiringRequestModel.totalCount =
            response.total != null ? response.total : 0;
            this.setHiringrequestList(response.data);
          }
          this.hiringRequestListLoader = false;
        },
        error => {
          this.hiringRequestListLoader = false;
        }
      );
  }
  //#endregion

  setHiringrequestList(data: any) {
    this.hiringRequestlist = [];
    data.forEach((element: ProjectHiringRequestFilterModel) => {
      this.hiringRequestlist.push({
        HiringRequestId: element.HiringRequestId,
        HiringRequestCode: element.HiringRequestCode,
        Description: element.Description,
        ProfessionId: element.ProfessionId,
        Position: element.Position,
        TotalVacancies: element.TotalVacancies,
        FilledVacancies: element.FilledVacancies,
        BasicPay: element.BasicPay,
        BudgetLineId: element.BudgetLineId,
        OfficeId: element.OfficeId,
        GradeId: element.GradeId,
        EmployeeID: element.EmployeeID,
        ProjectId: element.ProjectId,
        IsCompleted: element.IsCompleted,
        CurrencyId: element.CurrencyId,
        BudgetName: element.BudgetName,
        CurrencyName: element.CurrencyName,
        EmployeeName: element.EmployeeName,
        GradeName: element.GradeName,
        RequestedBy: element.RequestedBy,
        FilterValue: element.FilterValue
      });
    });
  }

  //#region "OnEditHiringRequestListRefresh"
  OnEditHiringRequestListRefresh(event: any) {
    // Note :
    const data = this.hiringRequestlist.find(
      x => x.HiringRequestId === event.HiringRequestId
    );
    // To get grade name
    event.GradeName = this.jobGradeList.find(
      j => j.GradeId === event.GradeId
    ).GradeName;
    const indexOfHiringRequestList = this.hiringRequestlist.indexOf(data);
    this.hiringRequestlist[indexOfHiringRequestList] = event;
  }

  //#endregion
}
