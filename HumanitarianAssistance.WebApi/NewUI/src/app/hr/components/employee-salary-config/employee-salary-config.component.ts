import { IDropDownModel } from './../../../store/models/purchase';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';

@Component({
  selector: 'app-employee-salary-config',
  templateUrl: './employee-salary-config.component.html',
  styleUrls: ['./employee-salary-config.component.scss']
})
export class EmployeeSalaryConfigComponent implements OnInit {
  accumulatedHeaders$ = of([
    'Salary Component',
    'Salary Allowance',
    'Salary Deduction'
  ]);
  bonusAndFineHeaders$ = of([
    'Salary Component',
    'Salary Allowance',
    'Salary Deduction'
  ]);
  accumulatedList$: Observable<any[]>;
  bonusAndFineList$: Observable<any[]>;
  monthDropdown$: Observable<IDropDownModel[]>;
  selectedMonth: IDropDownModel;
  actions: TableActionsModel;
  constructor() {
    this.monthDropdown$ = of([
      { name: '1st', value: 1 },
      { name: '2nd', value: 2 },
      { name: '3rd', value: 3 }
    ] as IDropDownModel[]);

    this.selectedMonth = { name: 'SELECT MONTH', value: 0 };
  }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: false,
        edit: false
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false
      }
    };
  }
  selectedMonthChanged(SelectedMonth) {
    this.selectedMonth = {
      name: SelectedMonth.name,
      value: SelectedMonth.value
    };
  }
  empActionEvents(event: any) {
    console.log(event.item);
  }
}
