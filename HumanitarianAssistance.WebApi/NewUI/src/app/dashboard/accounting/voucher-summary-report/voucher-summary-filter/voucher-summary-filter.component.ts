import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { IOpenedChange, IDataSource } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { IOfficeListModel } from '../../vouchers/models/voucher.model';
import { CurrencyModel } from 'src/app/dashboard/project-management/project-list/project-details/models/project-details.model';
import { VoucherSummaryReportService } from '../voucher-summary-report.service';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { IProjectJobModel } from 'src/app/dashboard/project-management/project-list/budgetlines/models/budget-line.models';
import { BudgetlineListModel, VoucherSummaryFilterModel } from '../voucher-summary-model';
import { ToastrService } from 'ngx-toastr';
import { IResponseData } from '../../vouchers/models/status-code.model';

@Component({
  selector: 'app-voucher-summary-filter',
  templateUrl: './voucher-summary-filter.component.html',
  styleUrls: ['./voucher-summary-filter.component.scss']
})
export class VoucherSummaryFilterComponent implements OnInit {

  // projectJobList: IProjectJobModel[] = [];
  // budgetLineList: BudgetlineListModel[] = [];
  filterModel: VoucherSummaryFilterModel;
  selectedCurrency: any = null;
  selectedRecordType: any = null;
  @Output() filterApplied = new EventEmitter<VoucherSummaryFilterModel>();

// Input Properties
@Input() multiAccountsList: IDataSource[];
@Input() multiOfficesList: IDataSource[];
@Input() multiJournalList: IDataSource[];
@Input() multiProjectList: IDataSource[];
@Input() currencyList: CurrencyModel[];
@Input() recordType: any[];

multiBudgetLineList: IDataSource[];
multiProjectJobList: IDataSource[];

// subscription destroy
private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

// dropdown multiselect resultset
public accountFilter: any[] = [];
public officeFilter: any[] = [];
public journalFilter: any[] = [];
public projectFilter: any[] = [];
public budgetLineFilter: any[] = [];
public projectJobFilter: any[] = [];

  constructor(private voucherSummaryService: VoucherSummaryReportService,
               private toastr: ToastrService) { }

  ngOnInit() {
    this.initializeModel();
  }

  initializeModel() {
    this.filterModel = {
      Accounts: [],
      BudgetLines: [],
      Journals: [],
      Offices: [],
      ProjectJobs: [],
      Projects: [],
      RecordType: null,
      Currency: null,
      pageIndex: 0,
      pageSize: 10
    };

    this.accountFilter = [];
    this.budgetLineFilter = [];
    this.officeFilter = [];
    this.journalFilter = [];
    this.projectFilter = [];
    this.projectJobFilter = [];
    this.selectedCurrency = null;
    this.selectedRecordType = null;
    this.multiBudgetLineList = [];
    this.multiProjectJobList = [];
  }

  //#region "getProjectJobList"
  getProjectJobList(projectIds: number[]) {
    this.voucherSummaryService.getProjectJobList(projectIds).pipe(
      takeUntil(this.destroyed$)
    ).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          //this.projectJobList = [];
          response.data.forEach(element => {
            // this.projectJobList.push({
            //   ProjectJobCode: element.ProjectJobCode,
            //   ProjectJobId: element.ProjectJobId,
            //   ProjectJobName: element.ProjectJobName
            // });

            this.multiProjectJobList.push({
              Id: element.ProjectJobId,
              Name: element.ProjectJobName
            });

          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "getProjectJobList"
  getProjectBudgetLineList(projectJobIds: number[]) {
    this.voucherSummaryService.getProjectBudgetLineList(projectJobIds).pipe(
      takeUntil(this.destroyed$)
    ).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          // this.budgetLineList = [];
          response.data.forEach(element => {
            // this.budgetLineList.push({
            //   BudgetCode: element.BudgetCode,
            //   BudgetLineId: element.BudgetLineId,
            //   BudgetName: element.BudgetName
            // });

            this.multiBudgetLineList.push({
              Id: element.BudgetLineId,
              Name: element.BudgetName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  onApplyFilter() {
    if (this.accountFilter.length > 0) {
      this.filterModel.Accounts = [];
      this.filterModel.Accounts = this.accountFilter;
    } else {
      this.toastr.warning('Accounts not selected');
      return;
    }

    if (this.officeFilter.length > 0) {
      this.filterModel.Offices = [];
      this.filterModel.Offices = this.officeFilter;
    } else {
      this.toastr.warning('Office not selected');
      return;
    }

    if (this.journalFilter.length > 0) {
      this.filterModel.Journals = [];
      this.filterModel.Journals = this.journalFilter;
    } else {
      this.toastr.warning('Journal not selected');
      return;
    }

    if (this.projectFilter.length > 0) {
      this.filterModel.Projects = [];
      this.filterModel.Projects = this.projectFilter;
    } else {
      this.toastr.warning('Project not selected');
      return;
    }

    if (this.projectJobFilter.length > 0) {
      this.filterModel.ProjectJobs = [];
      this.filterModel.ProjectJobs = this.projectJobFilter;
    } else {
      this.toastr.warning('Project Job not selected');
      return;
    }

    if (this.budgetLineFilter.length > 0) {
      this.filterModel.BudgetLines = [];
      this.filterModel.BudgetLines = this.budgetLineFilter;
    } else {
      this.toastr.warning('Budgetline not selected');
      return;
    }

    if (this.selectedCurrency !== null && this.selectedCurrency !== undefined) {
      this.filterModel.Currency = this.selectedCurrency;
    } else {
      this.toastr.warning('Currency not selected');
      return;
    }

    if (this.selectedRecordType !== null && this.selectedRecordType !== undefined) {
      this.filterModel.RecordType = this.selectedRecordType;
    } else {
      this.toastr.warning('Record type not selected');
      return;
    }

    this.filterApplied.emit(this.filterModel);
  }

  // #region "onOpenedAccountMultiSelectChange"
  onOpenedAccountMultiSelectChange(event: IOpenedChange) {
    this.accountFilter = event.Value;
  }
  // #endregion

  // #region "onOpenedOfficeMultiSelectChange"
  onOpenedOfficeMultiSelectChange(event: IOpenedChange) {
    this.officeFilter = event.Value;
  }
  // #endregion

  // #region "onOpenedJournalMultiSelectChange"
  onOpenedJournalMultiSelectChange(event: IOpenedChange) {
    this.journalFilter = event.Value;
  }
  // #endregion

  // #region "onOpenedProjectMultiSelectChange"
  onOpenedProjectMultiSelectChange(event: IOpenedChange) {
    this.projectFilter = event.Value;
    if (this.projectFilter.length > 0 ) {
      this.getProjectJobList(this.projectFilter);
    }
  }
  // #endregion

  // #region "onOpenedBudgetLineMultiSelectChange"
  onOpenedBudgetLineMultiSelectChange(event: IOpenedChange) {
    this.budgetLineFilter = event.Value;
  }
  // #endregion

  // #region "onOpenedProjectJobMultiSelectChange"
  onOpenedProjectJobMultiSelectChange(event: IOpenedChange) {
    this.projectJobFilter = event.Value;
    if (this.projectJobFilter.length > 0 ) {
      this.getProjectBudgetLineList(this.projectJobFilter);
    }
  }
  // #endregion

}
