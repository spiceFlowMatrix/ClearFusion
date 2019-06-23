import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { projectPagesMaster } from 'src/app/shared/applicationpagesenum';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { ProposalReportService } from './proposal-report.service';
import { MatDialog } from '@angular/material/dialog';

import {
  ICurrencyModel,
  IProposalReport,
  IProposalReportFilter,
  IAmountSummary
} from './models/proposal-report.model';
import { IResponseData } from '../../accounting/vouchers/models/status-code.model';
import { map } from 'rxjs/internal/operators/map';
import { Observable } from 'rxjs/internal/Observable';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-proposal-report',
  templateUrl: './proposal-report.component.html',
  styleUrls: ['./proposal-report.component.scss']
})
export class ProposalReportComponent implements OnInit {
  //#region "variables"

  setProjectHeader = 'Projects';
  menuList: IMenuList[] = [
    {
      Id: 1,
      PageId: projectPagesMaster.ProjectDashboard,
      Text: 'Dashboard',
      Link: '/project/project-dashboard'
    },
    {
      Id: 2,
      PageId: projectPagesMaster.MyProjects,
      Text: 'My Projects',
      Link: '/project/my-projects'
    },
    {
      Id: 3,
      PageId: projectPagesMaster.Donors,
      Text: 'Donors',
      Link: '/project/project-donor'
    },
    {
      Id: 4,
      PageId: projectPagesMaster.ProjectCashFlow,
      Text: 'Cash Flow',
      Link: '/project/project-cash-flow'
    },
    {
      Id: 5,
      PageId: projectPagesMaster.ProposalReport,
      Text: 'Proposal Report',
      Link: '/project/proposal-report'
    },
    {
      Id: 6,
      PageId: projectPagesMaster.ProjectIndicators,
      Text: 'Project Indicators',
      Link: '/project/project-indicators'
    }
  ];
  authorizedMenuList: IMenuList[] = [];

  proposalReportList: IProposalReport[] = [];
  amountSummaryList$: Observable<IAmountSummary[]>;
  currencyList: ICurrencyModel[] = [];
  reportFilter: IProposalReportFilter;

  totalCount = 0;
  projectListLoaderFlag = false;

  // screen scroll
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;

  //#endregion

  constructor(
    public dialog: MatDialog,
    public proposalReportService: ProposalReportService,
    private globalService: GlobalSharedService,
    private localStorageservice: LocalStorageService
  ) {
    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);

    this.authorizedMenuList = this.localStorageservice.GetAuthorizedPages(
      this.menuList
    );

    // Set Menu Header List
    this.globalService.setMenuList(this.authorizedMenuList);

    this.getScreenSize();
    this.resetForm();
  }

  ngOnInit() {
    this.getCurrencyList();
    this.filterReport(this.reportFilter);
    this.getAmountSummaryList(this.reportFilter);
    // this.projectFilterModel = {
    //   pageIndex: 0,
    //   pageSize: 0,
    //   totalCount: 0,
    // }
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;
    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region "resetForm"
  resetForm() {
   this.reportFilter =  {
      ProjectName: '',
      DueDate: null,
      DueDateFilterOption: null,
      StartDate: null,
      StartDateFilterOption: null,
      CurrencyId: null,
      Amount: null,
      AmountFilterOption: null,
      IsCompleted: false,
      IsLate: false,
      pageIndex:0,
      pageSize:10,
      totalCount:0
    };
  }
  //#endregion

  //#region "getProjectProposalReport"
  getProjectProposalReport(data: IProposalReportFilter) {

    data.StartDate = data.StartDate != null ? this.proposalReportService.setDateTime(data.StartDate) : null,
    data.DueDate =  data.DueDate != null ? this.proposalReportService.setDateTime(data.DueDate) : null,


    // this.projectFilterModel.totalCount = 0;
    this.projectListLoaderFlag = true;
    // this.commonLoaderService.showLoader();
    this.proposalReportService.GetProjectProposalReport(data).subscribe(
      res => {
        if (res.data != null && res.statusCode === 200) {
          // this.projectFilterModel.totalCount = res.total != null ? res.total : 0;
          this.totalCount = res.total != null ? res.total : 0;
          this.proposalReportList = [];
          if (res.data.length > 0) {
            res.data.forEach((element: IProposalReport) => {
              this.proposalReportList.push({
                ProjectCode: element.ProjectCode,
                ProjectsName: element.ProjectsName,
                ProjectCurrencyId: element.ProjectCurrencyId,
                ProjectStartDate: element.ProjectStartDate != null ?
                                this.proposalReportService.getDateTime(element.ProjectStartDate) : null,
                Progress: element.Progress,
                TooltipText: '',
                ProjectEndDate: element.ProjectEndDate != null ? this.proposalReportService.getDateTime(element.ProjectEndDate) : null,
                BudgetEstimation: element.BudgetEstimation,
                ColorCode: element.ColorCode,
                ReviewCompletionDate: element.ReviewCompletionDate,
                DueDays: element.DueDays,
              });
            });
          }
        }
        this.projectListLoaderFlag = false;
      },
      error => {
        this.projectListLoaderFlag = false;
      }
    );
  }
  //#endregion

  //#region "getCurrencyList"
  getCurrencyList() {
    this.proposalReportService.GetCurrencyList().subscribe(
      (response: IResponseData) => {
        this.currencyList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.currencyList.push({
              CurrencyId: element.CurrencyId,
              CurrencyCode: element.CurrencyCode,
              CurrencyName: element.CurrencyName
            });

          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getAmountSummaryList"
  getAmountSummaryList(filterData: IProposalReportFilter) {
    this.amountSummaryList$ = this.proposalReportService.GetAmountSummaryList(filterData).pipe(map((x: IResponseData) => x.data));
  }
  //#endregion

  //#region "EMIT: filterReport"
  filterReport(data: IProposalReportFilter) {

    this.reportFilter = data;

    data.pageIndex= data.pageIndex == null? 0 : data.pageIndex;
    data.pageSize = data.pageSize == null? 10 : data.pageSize;
    this.getProjectProposalReport(this.reportFilter);
    this.getAmountSummaryList(this.reportFilter);
  }
  //#endregion

  //#region "pageEvent"
  pageEvent(e) {
    this.reportFilter.pageIndex = e.pageIndex;
    this.reportFilter.pageSize = e.pageSize;
    // this.voucherFilter.totalCount =  e.length;

    this.getProjectProposalReport(this.reportFilter);
  }
  //#endregion

  //#region "getToolTipText"
  getToolTipText(progress: number, completionFlag: number, date: any, days: any) {

    switch (completionFlag) {
      case 1 :
        // RED
        return 'Past due date by ' + days + (days>1? ' days': ' day');
      case 2 :
        // BLUE
        return 'Completed on ' + this.setDateforToolTip(date);
      case 3 :
        // GREEN
        return 'In progress ' + progress + '%';
      default :
        return '' ;
        // return 'Progress: ' + progress + ' ' + completionFlag ;
    }
  }
  //#endregion

  //#region "setDateforToolTip"
  setDateforToolTip(date: any) {
    if (date) {
      return new Date(date).getDate() + '/' +  (new Date(date).getMonth()+1) + '/' + new Date(date).getFullYear();
    } else {
      return '';
    }
  }
  //#endregion

  //#region "getColorTheme"
  getColorTheme(color: number) {
    switch (color) {
      case 1:
        return 'warn';
      case 2:
        return 'accent';
      case 3:
        return 'primary';
    }
  }
  //#endregion
}
