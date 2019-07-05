import { Component, OnInit, HostListener, ViewChild, OnDestroy } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl
} from '@angular/forms';
import { ProjectCashFlowService } from './project-cash-flow.service';
import { IResponseData } from '../../accounting/vouchers/models/status-code.model';
import {
  IProjectCashFlow,
  IBudgetLineDetailModel,
  IBudgetLineBreakdownFlowModel
} from './project-cash-flow.models';
import { ToastrService } from 'ngx-toastr';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { Subject } from 'rxjs/internal/Subject';
import {
  ICurrencyList,
  IProjectList
} from '../../accounting/gain-loss-report/gain-loss-report.model';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { MatOption } from '@angular/material/core';
import {
  IDataSource,
  IOpenedChange
} from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { projectPagesMaster } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { DatePipe } from '@angular/common';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-project-cash-flow',
  templateUrl: './project-cash-flow.component.html',
  styleUrls: ['./project-cash-flow.component.scss']
})
export class ProjectCashFlowComponent implements OnInit, OnDestroy{
  //#region "Variables"

  @ViewChild('budgetLineAllSelected') private budgetLineAllSelected: MatOption;

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

  projectList: IProjectList[] = [];
  multiProjectList: IDataSource[] = [];
  multiBudgetlineList: IDataSource[] = [];
  currencyList: ICurrencyList[] = [];
  donorList: any[] = [];
  budgetLineList: IBudgetLineDetailModel[] = [];

  projectCashFlowList: any;
  budgetLineCashFlowList: any;
  options: any;

  selectAllBudgetLineFlag = false;

  projectCashFlowForm: FormGroup;
  budgetLineBreakdownFlowForm: FormGroup;

  projectCashFlowLoader = false;
  budgetLineBreakdownFlowLoader = false;
  cashFlowVoucherDates: any[] = [];
  budgetLineVoucherDates: any[] = [];

  //#region
  // currency filter
  public currencyFilterCtrl: FormControl = new FormControl();
  public filteredCurrency: ReplaySubject<ICurrencyList[]> = new ReplaySubject<
    ICurrencyList[]
  >(1);

  // project filter
  public projectFilterCtrl: FormControl = new FormControl();
  // public projectFilter: any[] = [];
  public budgetLineFilter: any[] = [];
  public filteredProject: ReplaySubject<IProjectList[]> = new ReplaySubject<
    IProjectList[]
  >(1);

  // budgetLine filter
  public budgetLineFilterCtrl: FormControl = new FormControl();
  public filteredBudgetLine: ReplaySubject<
    IBudgetLineDetailModel[]
  > = new ReplaySubject<IBudgetLineDetailModel[]>(1);

  public donorFilterCtrl: FormControl = new FormControl();
  public filteredDonor: ReplaySubject<
    IBudgetLineDetailModel[]
  > = new ReplaySubject<IBudgetLineDetailModel[]>(1);

  /** Subject that emits when the component has been destroyed. */
  protected _onDestroy = new Subject<void>();
  //#endregion

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  scrollStylesSearch: any;

  //#endregion

  constructor(
    public projectCashFlowService: ProjectCashFlowService,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private globalService: GlobalSharedService,
    private localStorageservice: LocalStorageService,
    public datepipe: DatePipe
  ) {
    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);

    this.authorizedMenuList = this.localStorageservice.GetAuthorizedPages(
      this.menuList
    );

    // Set Menu Header List
    this.globalService.setMenuList(this.authorizedMenuList);

    this.getScreenSize();
  }

  ngOnInit() {
    this.projectCashFlowForm = this.fb.group({
      // ProjectId: [[], Validators.required],
      CurrencyId: [null, Validators.required],
      ProjectId: [null, Validators.required],
      ProjectCashFlowStartDate: [null, Validators.required],
      ProjectCashFlowEndDate: [null, Validators.required],
      DonorID: [null]
    });

    this.budgetLineBreakdownFlowForm = this.fb.group({
      ProjectId: [null, Validators.required],
      BudgetLineId: [[]],
      CurrencyId: [null, Validators.required],
      BudgetLineStartDate: [null, Validators.required],
      BudgetLineEndDate: [null, Validators.required]
    });
    this.GetAllProjectList();
    this.GetAllDonorList();
    this.getCurrencyList();
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 210 + 'px',
      'overflow-x': 'hidden'
    };

    this.scrollStylesSearch = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region getallprojectdetail
  GetAllProjectList() {
    this.projectList = [];
    this.projectCashFlowService
      .GetAllProjectList()
      .subscribe((response: IResponseData) => {
        if (response.statusCode === 200 && response.data != null) {
          this.multiProjectList = [];
          response.data.forEach(element => {
            this.projectList.push({
              ProjectId: element.ProjectId,
              ProjectName: element.ProjectCode + ' - ' + element.ProjectName,
              ProjectCode: element.ProjectCode,
              IsChecked: false
            });
            this.multiProjectList.push({
              Id: element.ProjectId,
              Name: element.ProjectCode + ' - ' + element.ProjectName
            });
          });

          // NOTE: load the initial Currency list
          this.filteredProject.next(this.projectList.slice());

          // listen for search field value changes
          this.projectFilterCtrl.valueChanges
            .pipe(takeUntil(this._onDestroy))
            .subscribe(() => {
              this.filterProjects();
            });
        }
      });
  }
  //#endregion

  //#region "getCurrencyList"
  getCurrencyList() {
    this.projectCashFlowService.GetCurrencyList().subscribe(
      (response: IResponseData) => {
        this.currencyList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.currencyList.push({
              CurrencyId: element.CurrencyId,
              CurrencyCode: element.CurrencyCode,
              CurrencyName: element.CurrencyCode + ' - ' + element.CurrencyName
            });
          });

          // NOTE: load the initial Currency list
          this.filteredCurrency.next(this.currencyList.slice());

          // listen for search field value changes
          this.currencyFilterCtrl.valueChanges
            .pipe(takeUntil(this._onDestroy))
            .subscribe(() => {
              this.filterAccounts();
            });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getAllDonorList"
  GetAllDonorList() {
    this.donorList = [];

    this.donorList = [];
    this.projectCashFlowService.GetAllDonorList().subscribe(
      (response: IResponseData) => {
        if (response.data != null) {
          response.data.forEach(element => {
            this.donorList.push({
              DonorId: element.DonorId,
              Name: element.Name
            });
          });

          // NOTE: load the initial Currency list
          this.filteredDonor.next(this.donorList.slice());

          // listen for search field value changes
          this.donorFilterCtrl.valueChanges
            .pipe(takeUntil(this._onDestroy))
            .subscribe(() => {
              this.filterDonor();
            });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getProjectBudgetLineList"
  getProjectBudgetLineList(projecId) {
    this.budgetLineList = [];
    this.multiBudgetlineList = [];

    this.projectCashFlowService.GetProjectBudgetLineList(projecId).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.budgetLineList.push({
              BudgetLineId: element.BudgetLineId,
              BudgetCode: element.BudgetCode,
              BudgetName: element.BudgetCode + ' - ' + element.BudgetName
            });
          });

          response.data.forEach(element => {
            this.multiBudgetlineList.push({
              Id: element.BudgetLineId,
              Name: element.BudgetCode + ' - ' + element.BudgetName
            });
          });

          // NOTE: load the initial Account list
          this.filteredBudgetLine.next(this.budgetLineList.slice());

          // listen for search field value changes
          this.budgetLineFilterCtrl.valueChanges
            .pipe(takeUntil(this._onDestroy))
            .subscribe(() => {
              this.filterBudgetLine();
            });
        }
      },

      error => {
        this.toastr.error('Something Went Wrong. Please try again');
      }
    );
  }
  //#endregion

  //#region "filter Project Cash Flow"
  filterProjectCashFlow(data: any) {
    if (this.projectCashFlowForm.valid) {
      const projectCashFlowData: IProjectCashFlow = {
        ProjectId: [],
        CurrencyId: data.CurrencyId,
        ProjectStartDate: new Date(
          data.ProjectCashFlowStartDate
        ).toLocaleString(),
        ProjectEndDate: new Date(data.ProjectCashFlowEndDate).toLocaleString(),
        DonorID: data.DonorID
      };

      this.projectCashFlowList = null;
      this.projectCashFlowForm.get('ProjectId').value.forEach(x => {
        projectCashFlowData.ProjectId.push(x);
      });
      this.projectCashFlowLoader = true;

      this.projectCashFlowService
        .FilterProjectCashFlow(projectCashFlowData)
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200 && response.data != null) {
              this.cashFlowVoucherDates = [];

              response.data.Date.forEach(x => {
                this.cashFlowVoucherDates.push(
                  this.datepipe.transform(
                    x != null ? this.getDateTime(x) : x,
                    'd/M/yy, h:mm:ss a'
                  )
                );
              });

              this.options = {
                title: {
                  display: true,
                  // text: 'My Title',
                  fontSize: 16
                },
                legend: {
                  position: 'top'
                },
                scales: {
                  xAxes: [
                    {
                      display: true
                    }
                  ],
                  yAxes: [
                    {
                      display: true
                    }
                  ]
                }
              };

              this.projectCashFlowList = {
                labels: this.cashFlowVoucherDates,
                datasets: [
                  {
                    label: 'Income',
                    lineTension: 0,
                    data: response.data.Income,
                    fill: false,
                    borderColor: '#71cdd6'
                  },
                  {
                    label: 'Expenditure',
                    lineTension: 0,
                    data: response.data.Expenditure,
                    fill: false,
                    borderColor: '#d65620'
                  },
                  {
                    label: 'Total Expected Budget',
                    lineTension: 0,
                    data: response.data.TotalExpectedBudget,
                    fill: false,
                    borderColor: '#707373'
                  }
                ]
              };
            } else {
              // this.toastr.error(response.message);
            }
            this.projectCashFlowLoader = false;
          },
          error => {
            this.toastr.error('Someting went wrong');
            this.projectCashFlowLoader = false;
          }
        );
    }
  }
  //#endregion

  //#region "filterBudgetLineBreakdown"
  filterBudgetLineBreakdown(data: IBudgetLineBreakdownFlowModel) {
    this.budgetLineBreakdownFlowLoader = true;
    this.budgetLineCashFlowList = null;

    const budgetLineBreakdownData: IBudgetLineBreakdownFlowModel = {
      ProjectId: data.ProjectId,
      BudgetLineId: [],
      CurrencyId: data.CurrencyId,
      BudgetLineStartDate:
        data.BudgetLineStartDate != null
          ? this.setDateTime(data.BudgetLineStartDate)
          : null,
      BudgetLineEndDate:
        data.BudgetLineEndDate != null
          ? this.setDateTime(data.BudgetLineEndDate)
          : null
    };

    this.budgetLineFilter.forEach(x => {
      budgetLineBreakdownData.BudgetLineId.push(x);
    });

    this.projectCashFlowService
      .FilterBudgetLineBreakdown(budgetLineBreakdownData)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data != null) {

            this.budgetLineVoucherDates = [];

            response.data.Date.forEach(x => {
              this.budgetLineVoucherDates.push(
                this.datepipe.transform(
                  x != null ? this.getDateTime(x) : x,
                  'd/M/yy, h:mm:ss a'
                )
              );
            });

            this.budgetLineCashFlowList = {
              labels: this.budgetLineVoucherDates,
              // labels: ['JAN','FEB'],
              datasets: [
                {
                  label: 'Expenditure',
                  lineTension: 0,
                  data: response.data.Expenditure,
                  fill: false,
                  borderColor: '#d65620'
                },
                {
                  label: 'Total Budget',
                  lineTension: 0,
                  data: response.data.TotalExpectedBudget,
                  fill: false,
                  borderColor: '#707373'
                }
              ]
            };
          } else {
            // this.toastr.error(response.message);
          }
          this.budgetLineBreakdownFlowLoader = false;
        },
        error => {
          this.budgetLineBreakdownFlowLoader = false;
        }
      );
  }
  //#endregion

  //#region "onCashFlowFormSubmit"
  onCashFlowFormSubmit(filterdata: any) {
    this.filterProjectCashFlow(filterdata);
  }
  //#endregion

  //#region "onBudgetBreakdownFormSubmit"
  onBudgetBreakdownFormSubmit(filterdata: IBudgetLineBreakdownFlowModel) {
    this.filterBudgetLineBreakdown(filterdata);
  }
  //#endregion

  //#region " On Project Id Select Filter Budget Id"
  onBreakdownProjectIdChanged(event: any) {
    // reset budget line
    this.setBudgetLineId(null);

    this.getProjectBudgetLineList(event.value);

    const budgetLineBreakdownData: IBudgetLineBreakdownFlowModel = {
      ProjectId: event.value,
      BudgetLineId: null,
      CurrencyId: null,
      BudgetLineStartDate: null,
      BudgetLineEndDate: null
    };
    this.filterBudgetLineBreakdown(budgetLineBreakdownData);
  }
  //#endregion

  //#region "FILTER: Account filter"
  protected filterAccounts() {
    if (!this.currencyList) {
      return;
    }
    // get the search keyword
    let search = this.currencyFilterCtrl.value;
    if (!search) {
      this.filteredCurrency.next(this.currencyList.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the Currency
    this.filteredCurrency.next(
      this.currencyList.filter(
        acc =>
          acc.CurrencyName.toLowerCase()
            .trim()
            .indexOf(search) > -1
      )
    );
  }
  //#endregion

  //#region "FILTER: Project filter"
  protected filterProjects() {
    if (!this.projectList) {
      return;
    }
    // get the search keyword
    let search = this.projectFilterCtrl.value;
    if (!search) {
      this.filteredProject.next(this.projectList.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the Project
    this.filteredProject.next(
      this.projectList.filter(
        acc =>
          acc.ProjectName.toLowerCase()
            .trim()
            .indexOf(search) > -1
      )
    );
  }
  //#endregion

  //#region "FILTER: BudgetLine filter"
  protected filterBudgetLine() {
    if (!this.budgetLineList) {
      return;
    }
    // get the search keyword
    let search = this.budgetLineFilterCtrl.value;
    if (!search) {
      this.filteredBudgetLine.next(this.budgetLineList.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the budgetLine
    this.filteredBudgetLine.next(
      this.budgetLineList.filter(
        acc =>
          acc.BudgetName.toLowerCase()
            .trim()
            .indexOf(search) > -1
      )
    );
  }
  //#endregion

  //#region "FILTER: Donor filter"
  protected filterDonor() {
    if (!this.donorList) {
      return;
    }
    // get the search keyword
    let search = this.donorFilterCtrl.value;
    if (!search) {
      this.filteredDonor.next(this.donorList.slice());
      return;
    } else {
      search = search.toLowerCase();
    }
    // filter the budgetLine
    this.filteredDonor.next(
      this.donorList.filter(
        acc =>
          acc.Name.toLowerCase()
            .trim()
            .indexOf(search) > -1
      )
    );
  }
  //#endregion

  // #region "onOpenedProjectMultiSelectChange"
  onOpenedProjectMultiSelectChange(event: IOpenedChange) {
    this.projectCashFlowForm.controls['ProjectId'].setValue(event.Value);
  }
  // #endregion

  // #region "onOpenedBudgetLineMultiSelectChange"
  onOpenedBudgetLineMultiSelectChange(event: IOpenedChange) {
    this.budgetLineFilter = event.Value;
  }
  // #endregion

  //#region "Get / Set"
  setBudgetLineId(value: any) {
    this.budgetLineBreakdownFlowForm.controls['BudgetLineId'].setValue(value);
  }

  get projectIds() {
    return this.projectCashFlowForm.get('ProjectId').value;
  }

  get projectStartDate() {
    return this.projectCashFlowForm.get('ProjectCashFlowStartDate').value;
  }

  get projectEndDate() {
    return this.projectCashFlowForm.get('ProjectCashFlowEndDate').value;
  }

  get budgetLineStartDate() {
    return this.budgetLineBreakdownFlowForm.get('BudgetLineStartDate').value;
  }

  get budgetLineEndDate() {
    return this.budgetLineBreakdownFlowForm.get('BudgetLineEndDate').value;
  }
  //#endregion

  //#region "Select All Budget Line"

  onTossleBudgetLine(all) {
    if (this.budgetLineAllSelected.selected) {
      this.budgetLineAllSelected.deselect();
      return false;
    }
    if (
      this.budgetLineBreakdownFlowForm.controls.BudgetLineEndDate.value
        .length === this.budgetLineList.length
    ) {
      this.budgetLineAllSelected.select();
    }
  }

  selectAllBudgetLine() {
    if (this.budgetLineAllSelected.selected) {
      this.budgetLineBreakdownFlowForm.controls.BudgetLineId.patchValue([
        ...this.budgetLineList.map(item => item.BudgetLineId),
        0
      ]);
    } else {
      this.budgetLineBreakdownFlowForm.controls.BudgetLineId.patchValue([]);
    }
  }
  //#endregion

  selectData(e) {}

  selectBudgetLineData(e) {}

  //#region "setDateTime"
  setDateTime(data): any {
    return new Date(
      new Date(data).getFullYear(),
      new Date(data).getMonth(),
      new Date(data).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );
  }
  //#endregion

  getDateTime(data): any {
    return new Date(
      new Date(data).getTime() - new Date().getTimezoneOffset() * 60000
    );
  }

  ngOnDestroy() {

    this._onDestroy.next();
    this._onDestroy.complete();

  }

}
