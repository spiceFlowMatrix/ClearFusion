import { Component, OnInit, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';
import {
  ICurrencyModel,
  IFilterOption,
  IProposalReportFilter
} from '../models/proposal-report.model';

@Component({
  selector: 'app-proposal-report-filter',
  templateUrl: './proposal-report-filter.component.html',
  styleUrls: ['./proposal-report-filter.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProposalReportFilterComponent implements OnInit {
  @Input() currencyList: ICurrencyModel[] = [];
  @Output() filterReport = new EventEmitter<any>();

  reportFilter: IProposalReportFilter;
  filterOptionList: IFilterOption[] = [
    {
      FilterId: 1,
      FilterName: 'Equals'
    },
    {
      FilterId: 2,
      FilterName: 'Greater than'
    },
    {
      FilterId: 3,
      FilterName: 'Lesser than'
    },
    {
      FilterId: 4,
      FilterName: 'Not equals'
    }
  ];

  constructor() {
   this.resetForm();
  }

  ngOnInit() {}

  resetForm() {
    this.reportFilter = {
      ProjectName: '',
      DueDate: null,
      DueDateFilterOption: null,
      StartDate: null,
      StartDateFilterOption: null,
      CurrencyId: null,
      Amount: null,
      AmountFilterOption: 0,
      IsCompleted: false,
      IsLate: false
    };
  }

  onFilterReport() {
    console.log(this.reportFilter);
    this.filterReport.emit(this.reportFilter);
  }

  clearFilterAmount() {
    this.reportFilter.AmountFilterOption = 0;
    return true;
  }
}
