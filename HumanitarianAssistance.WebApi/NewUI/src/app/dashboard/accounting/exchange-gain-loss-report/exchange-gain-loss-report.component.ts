import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { ConfigurationFilterComponent } from './configuration-filter/configuration-filter.component';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ExchangeGainLossReportService } from './exchange-gain-loss-report.service';
import { IResponseData } from '../vouchers/models/status-code.model';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { takeUntil } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';

@Component({
  selector: 'app-exchange-gain-loss-report',
  templateUrl: './exchange-gain-loss-report.component.html',
  styleUrls: ['./exchange-gain-loss-report.component.scss']
})
export class ExchangeGainLossReportComponent implements OnInit, OnDestroy {

  showFilters = false;
  transactionFiltersForm: FormGroup;
  projectList: any[];
  journalList: any[];
  officeList: any[];
  displayedColumns = ['Checked', 'AccountCode', 'AccountName',
    'BalanceOnOriginalTransactionDates', 'BalanceOnComparisionDate', 'ResultingGainLoss'];

  dataSource = ELEMENT_DATA;
  labelText: string;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  @ViewChild(ConfigurationFilterComponent) fieldConfig: ConfigurationFilterComponent;

  constructor(private globalSharedService: GlobalSharedService,
    private gainLossReportService: ExchangeGainLossReportService, private fb: FormBuilder) { }

  ngOnInit() {
    this.globalSharedService.setMenuHeaderName('Currency Exchange Gain Loss Calculator');
    this.globalSharedService.setMenuList([]);

    this.transactionFiltersForm = this.fb.group({
      'offices': [[]],
      'journals': [[]],
      'projects': [[]]
    });

    forkJoin([
      this.getProjectList(),
      this.getJournalList(),
      this.getOfficeList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeProjectList(result[0]);
        this.subscribeJournalList(result[1]);
        this.subscribeOfficeList(result[2]);
      });
  }

  getLabelClass(value) {
    this.labelText = value > 0 ? 'Gain' : 'Loss';
    return value > 0 ? 'label label-success' : 'label label-danger';
  }

  showConfiguration() {
    this.fieldConfig.show();
  }

   //#region "getProjectList"
   getProjectList() {
    return this.gainLossReportService.GetProjectList();
  }
  //#endregion

  subscribeProjectList(response) {
        this.projectList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.projectList.push({
              ProjectId: element.ProjectId,
              ProjectName: element.ProjectName,
              ProjectCode: element.ProjectCode,
              IsChecked: false
            });
          });
        }
  }

  //#region "getJournalList"
  getJournalList() {
    return this.gainLossReportService.GetJournalList();
  }
  //#endregion

  subscribeJournalList(response) {
        this.journalList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.journalList.push({
              JournalCode: element.JournalCode,
              JournalName: element.JournalName,
              JournalType: element.JournalType,
              IsChecked: false
            });
          });
        }
    }

  //#region "getOfficeList"
  getOfficeList() {
    return this.gainLossReportService.GetOfficeList();
  }
  //#endregion

  subscribeOfficeList(response) {
        this.officeList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.officeList.push({
              OfficeId: element.OfficeId,
              OfficeName: element.OfficeName,
              IsChecked: false
            });
          });
        }
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}

export interface Element {
  Checked: boolean;
  AccountCode: string;
  AccountName: string;
  BalanceOnOriginalTransactionDates: number;
  BalanceOnComparisionDate: number;
  ResultingGainLoss: number;
}

const ELEMENT_DATA: Element[] = [
  { Checked: false, AccountCode: '1', AccountName: 'Hydrogen', BalanceOnOriginalTransactionDates: 1.0079, BalanceOnComparisionDate: 22, ResultingGainLoss: 0 }]
