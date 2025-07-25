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
  IProfessionList,
  IWorkingShift,
  IGender,
  ICountryList,
  IProvinceList,
  IJobTypeList,
  IFilterModel
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
  countryList: ICountryList[] = [];
  provinceList: IProvinceList[] = [];
  workingShift: IWorkingShift[] = [
    { Id: 1, value: 'Day' },
    { Id: 2, value: 'Night' }
  ];
  gender: IGender[] = [
    { Id: 1, value: 'Male' },
    { Id: 2, value: 'Female' },
    { Id: 2, value: 'Other' }
  ];
  JobTypeList: IJobTypeList[] = [
    {JobTypeId: 1, JobTypeName: 'JobName1'},
    {JobTypeId: 2, JobTypeName: 'JobName2'}
  ];
  // model
  // projectHiringRequestFilter: ProjectHiringRequestFilterModel;
  hiringRequestModel: IFilterModel;

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
    this.getCurrencyList();
    this.getBudgetLineList();
    this.getOfficeList();
    this.getCountryList();
    this.getJobGradeList();
    this.getProfessionlist();
    this.getProvinceList();
    this.getAllHiringRequestFilterList();
  }

  //#region  "initForm"
  initForm() {
    this.hiringRequestModel = {
      pageIndex: 0,
      pageSize: 10,
      FilterValue: '',
      ProjectId: null,
      TotalCount: 0
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
    // NOTE: It passed the data into the AddHiringRequestsComponent Model
    const dialogRef = this.dialog.open(AddHiringRequestsComponent, {
      width: '550px',
      autoFocus: false,
      data: {
        BudgetLineList: this.budgetLineList,
        OfficeList: this.officeList,
        CurrencyList: this.currencyList,
        JobGradeList: this.jobGradeList,
        ProjectId: this.projectId,
        ProfessionList: this.professionList,
        workingShift: this.workingShift,
        gender: this.gender,
        countryList: this.countryList,
        provinceList: this.provinceList,
        JobTypeList: this.JobTypeList
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
    this.hiringRequestModel.ProjectId = this.projectId;
    this.hiringRequestModel.TotalCount = 0;
    this.hiringRequestListLoader = true;
    this.hiringRequestService
      .GetProjectHiringRequestFilterList(this.hiringRequestModel)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data !== null) {
            this.hiringRequestModel.TotalCount =
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
        Description: element.Description,
        ProfessionId: element.ProfessionId,
        Position: element.Position,
        TotalVacancies: element.TotalVacancies,
        FilledVacancies: element.FilledVacancies,
        BasicPay: element.BasicPay,
        BudgetLineId: element.BudgetLineId,
        OfficeId: element.OfficeId,
        GradeId: element.GradeId,
        ProjectId: element.ProjectId,
        IsCompleted: element.IsCompleted,
        CurrencyId: element.CurrencyId,
        RequestedBy: element.RequestedBy,
        AnouncingDate: element.AnouncingDate,
        JobType: element.JobType,
        JobCategory: element.JobCategory,
        Background: element.Background,
        JobStatus: element.JobStatus,
        KnowladgeAndSkillRequired: element.KnowladgeAndSkillRequired,
        SalaryRange: element.SalaryRange,
        Shift: element.Shift,
        ProvinceId: element.ProvinceId,
        SpecificDutiesAndResponsblities:
          element.SpecificDutiesAndResponsblities,
        SubmissionGuidlines: element.SubmissionGuidlines,
        ClosingDate: element.ClosingDate,
        ContractDuration: element.ContractDuration,
        ContractType: element.ContractType,
        CountryId: element.CountryId,
        GenderId: element.GenderId,
        MinimumEducationLevel: element.MinimumEducationLevel,
        Experience: element.Experience,
        Organization: element.Organization,
        GradeName: this.jobGradeList.find(x => x.GradeId === element.GradeId).GradeName
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

  //#region "getCountryList"
  getCountryList() {
    this.hiringRequestService.GetCountryList().subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response != null) {
          if (response.data != null) {
            response.data.forEach(element => {
              this.countryList.push({
                CountryId: element.CountryId,
                CountryName: element.CountryName
              });
            });
          }
        }
      },
      error => {
      }
    );
  }
  //#endregion

  //#region "getCountryList"
  getProvinceList() {
    this.hiringRequestService.GetProvinceList().subscribe(
      (response: IResponseData) => {
        this.provinceList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.provinceList.push({
              ProvinceId: element.ProvinceId,
              ProvinceName: element.ProvinceName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion
}
