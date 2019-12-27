import { Component, OnInit, ViewChild } from '@angular/core';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { ConfigurationFilterComponent } from './configuration-filter/configuration-filter.component';

@Component({
  selector: 'app-exchange-gain-loss-report',
  templateUrl: './exchange-gain-loss-report.component.html',
  styleUrls: ['./exchange-gain-loss-report.component.scss']
})
export class ExchangeGainLossReportComponent implements OnInit {

  displayedColumns = ['Checked', 'AccountCode', 'AccountName',
    'BalanceOnOriginalTransactionDates', 'BalanceOnComparisionDate', 'ResultingGainLoss'];

  dataSource = ELEMENT_DATA;
  labelText: string;
  @ViewChild(ConfigurationFilterComponent) fieldConfig: ConfigurationFilterComponent;

  constructor(private globalSharedService: GlobalSharedService) { }

  ngOnInit() {
    this.globalSharedService.setMenuHeaderName('Currency Exchange Gain Loss Calculator');
    this.globalSharedService.setMenuList([]);
  }

  getLabelClass(value) {
    this.labelText = value > 0 ? 'Gain' : 'Loss';
    return value > 0 ? 'label label-success' : 'label label-danger';
  }

  showConfiguration() {
    this.fieldConfig.show();
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
