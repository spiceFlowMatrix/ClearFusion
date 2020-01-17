import { AddFineComponent } from './add-fine/add-fine.component';
import { AddBonusComponent } from './add-bonus/add-bonus.component';
import { AddSalaryConfigurationComponent } from './add-salary-configuration/add-salary-configuration.component';
import { IDropDownModel } from './../../../store/models/purchase';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material';

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
  constructor(
    public dialog: MatDialog,
    private toastr: ToastrService,
  ) {
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

  // #region Add Salary Configuration
  addSalaryConfiguration(): void {
      // NOTE: It open AddSalaryConfiguration dialog and passed the data into the AddSalaryConfigurationComponent Model
      const dialogRef = this.dialog.open(AddSalaryConfigurationComponent, {
        width: '500px',
        autoFocus: false,
      });
      // refresh the data after new request created
      dialogRef.componentInstance.onAddSalaryConfigurationRefresh.subscribe(() => {});
      dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion

 // #region Add Bonus
 addBonus(): void {
  // NOTE: It open AddBonus dialog and passed the data into the AddBonusComponent Model
  const dialogRef = this.dialog.open(AddBonusComponent, {
    width: '500px',
    autoFocus: false,
  });
  // refresh the data after new request created
  dialogRef.componentInstance.onAddBonusRefresh.subscribe(() => {});
  dialogRef.afterClosed().subscribe(() => {});
}
//#endregion
// #region Add Bonus
addFine(): void {
  // NOTE: It open AddFine dialog and passed the data into the AddFineComponent Model
  const dialogRef = this.dialog.open(AddFineComponent, {
    width: '500px',
    autoFocus: false,
  });
  // refresh the data after new request created
  dialogRef.componentInstance.onAddFineRefresh.subscribe(() => {});
  dialogRef.afterClosed().subscribe(() => {});
}
//#endregion
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
