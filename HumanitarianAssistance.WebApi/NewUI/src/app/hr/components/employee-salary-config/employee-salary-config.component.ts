import { AddFineComponent } from './add-fine/add-fine.component';
import { AddBonusComponent } from './add-bonus/add-bonus.component';
import { AddSalaryConfigurationComponent } from './add-salary-configuration/add-salary-configuration.component';
import { IDropDownModel } from './../../../store/models/purchase';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit } from '@angular/core';
import { of, observable, empty } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material';
import { Month, TransactionType } from 'src/app/shared/enum';
import { ActivatedRoute } from '@angular/router';
import { EmployeeSalaryConfigService } from '../../services/employee-salary-config.service';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import 'rxjs/add/observable/empty';

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
  monthsList$: Observable<IDropDownModel[]>;
  selectedMonth: IDropDownModel;
  actions: TableActionsModel;
  employeeId: number;
  monthlySalaryBreakdown: IMonthlySalaryBreakdown;
  hideColumsBounusFine = of({
    headers: ['Salary Component', 'Salary Allowance', 'Salary Deduction'],
    items: ['SalaryComponent', 'SalaryAllowance', 'SalaryDeduction']
  });
  employeeCurrencyAndAmount: any;


  constructor(
    public dialog: MatDialog,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute,
    private salaryConfigService: EmployeeSalaryConfigService,
  ) {
    this.selectedMonth = { name: 'SELECT MONTH', value: 0 };
    this.activatedRoute.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
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
    this.getAllMonthList();
    this.employeeCurrencyAndAmount = {
      CurrencyId: 0,
      CurrencyName: '-',
      MonthlyAmount: 0,
      PayrollId: 0
    };
    this.monthlySalaryBreakdown = {
      GrossSalary: 0,
      HourlyRate: 0,
      Month: '',
      NetSalary: 0,
      SalaryPaidAmount: 0,
      Status: ''
    };

    this.getEmployeeBasicPayAndCurrency();
  }

  // #region Add Salary Configuration
  addSalaryConfiguration(): void {
    // NOTE: It open AddSalaryConfiguration dialog and passed the data into the AddSalaryConfigurationComponent Model
    const dialogRef = this.dialog.open(AddSalaryConfigurationComponent, {
      width: '500px',
      autoFocus: false,
      data: {
        PayrollId: this.employeeCurrencyAndAmount.PayrollId,
        CurrencyId: this.employeeCurrencyAndAmount.CurrencyId,
        EmployeeId: this.employeeId,
        MonthlyAmount: this.employeeCurrencyAndAmount.MonthlyAmount
      }
    });
    // refresh the data after new request created
    dialogRef.componentInstance.onAddSalaryConfigurationRefresh.subscribe(() => { });
    dialogRef.afterClosed().subscribe(() => {
      this.getEmployeeBasicPayAndCurrency();
    });
  }
  //#endregion

  // #region Add Bonus
  addBonus(): void {
    if (this.selectedMonth.value === 0) {
      this.toastr.warning('Please select Month');
      return;
    }
    // NOTE: It open AddBonus dialog and passed the data into the AddBonusComponent Model
    const dialogRef = this.dialog.open(AddBonusComponent, {
      width: '500px',
      autoFocus: false,
      data: {
        EmployeeId: this.employeeId,
        SelectedMonth: this.selectedMonth.value
      }
    });
    // refresh the data after new request created
    dialogRef.componentInstance.onAddBonusRefresh.subscribe(() => { });
    dialogRef.afterClosed().subscribe(() => { this.getEmployeeBonusFineSalaryHead(); });
  }
  //#endregion
  // #region Add Bonus
  addFine(): void {
    if (this.selectedMonth.value === 0) {
      this.toastr.warning('Please select Month');
      return;
    }
    // NOTE: It open AddFine dialog and passed the data into the AddFineComponent Model
    const dialogRef = this.dialog.open(AddFineComponent, {
      width: '500px',
      autoFocus: false,
      data: {
        EmployeeId: this.employeeId,
        SelectedMonth: this.selectedMonth.value
      }
    });
    // refresh the data after new request created
    dialogRef.componentInstance.onAddFineRefresh.subscribe(() => { });
    dialogRef.afterClosed().subscribe(() => { this.getEmployeeBonusFineSalaryHead(); });
  }
  //#endregion

  //#region "Get all month list for ExperienceInMonth dropdown"
  getAllMonthList() {
    const monthDropDown: IDropDownModel[] = [];
    for (let i = Month['January']; i <= Month['December']; i++) {
      monthDropDown.push({ name: Month[i], value: i });
    }
    this.monthsList$ = of(monthDropDown);
  }
  //#endregion
  selectedMonthChanged(SelectedMonth) {
    this.selectedMonth = {
      name: SelectedMonth.name,
      value: SelectedMonth.value
    };

    this.getEmployeeBonusFineSalaryHead();
    this.monthlySalaryBreakdown.HourlyRate = (this.employeeCurrencyAndAmount.MonthlyAmount /
      StaticUtilities.getDaysInMonth(SelectedMonth.value, (new Date()).getFullYear())).toFixed(2);
      this.monthlySalaryBreakdown.Month = Month[SelectedMonth.value];
      this.getEmployeePayroll();
  }
  empActionEvents(event: any) {
    console.log(event.item);
  }

  getEmployeeBasicPayAndCurrency() {
    this.salaryConfigService.getEmployeeBasicPayAndCurrency(this.employeeId).subscribe(x => {
      if (x && x.EmployeeCurrencyAmount) {
        this.employeeCurrencyAndAmount = x.EmployeeCurrencyAmount;
      }
    });
  }

  getEmployeeBonusFineSalaryHead() {
    const model = {
      EmployeeId: this.employeeId,
      Month: this.selectedMonth.value
    };
    this.salaryConfigService.getEmployeeBonusFineSalaryHead(model).subscribe(x => {
      if (x && x.BonusFineSalaryHead && x.BonusFineSalaryHead.length > 0) {
        this.bonusAndFineList$ = of(x.BonusFineSalaryHead.map(y => {
          return {
            Id: y.Id,
            SalaryComponent: y.SalaryHeadName,
            SalaryAllowance: y.TransactionTypeId === TransactionType.Debit ? y.Amount : 0,
            SalaryDeduction: y.TransactionTypeId === TransactionType.Credit ? y.Amount : 0,
          };
        }));
      } else {
        this.bonusAndFineList$ =  of([]);
      }
    });
  }

  bonusFineEvents(event) {
    if (event.type === 'delete') {
      this.salaryConfigService.deleteEmployeeBonusFineSalaryHead(event.item.Id).subscribe(x => {
        if (x) {
          this.toastr.success('Item Deleted Successfully');
          this.getEmployeeBonusFineSalaryHead();
        } else {
          this.toastr.warning('Please try again');
        }
      }, error => {
        this.toastr.warning(error);
      });
    }
  }

  getEmployeeAccumulatedSalaryHead() {
    this.salaryConfigService.getEmployeeAccumulatedSalaryHead(this.employeeId).subscribe(x => {
      if (x && x.BonusFineSalaryHead && x.BonusFineSalaryHead.length > 0) {
        this.bonusAndFineList$ = of(x.BonusFineSalaryHead.map(y => {
          return {
            Id: y.Id,
            SalaryComponent: y.SalaryHeadName,
            SalaryAllowance: y.TransactionTypeId === TransactionType.Debit ? y.Amount : 0,
            SalaryDeduction: y.TransactionTypeId === TransactionType.Credit ? y.Amount : 0,
          };
        }));
      }
    });
  }

  getEmployeePayroll() {
    const model = {
      EmployeeId: this.employeeId,
      Month: this.selectedMonth.value
    };

    this.salaryConfigService.getEmployeePayroll(model).subscribe(x => {
      if (x && x.payroll) {

        this.monthlySalaryBreakdown.NetSalary = x.payroll.NetSalary;
        this.monthlySalaryBreakdown.GrossSalary = x.payroll.GrossSalary;
        this.monthlySalaryBreakdown.SalaryPaidAmount = x.payroll.SalaryPaid;

        this.accumulatedList$ = of(x.payroll.AccumulatedPayrollHeadList.map(y => {
          return {
            SalaryComponent: y.PayrollHeadName,
            SalaryAllowance: y.TransactionType === TransactionType.Debit ? y.Amount : 0,
            SalaryDeduction: y.TransactionType === TransactionType.Credit ? y.Amount : 0,
          };
        }));
      }
    }, error => {
      this.toastr.warning(error);
    });
  }

  approveSalary() {

  }

  revokeSalary() {

  }
}

export interface IMonthlySalaryBreakdown {
  HourlyRate: any;
  Month: string;
  GrossSalary: number;
  NetSalary: number;
  Status: string;
  SalaryPaidAmount: number;
}
