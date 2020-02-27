import { AddFineComponent } from './add-fine/add-fine.component';
import { AddBonusComponent } from './add-bonus/add-bonus.component';
import { AddSalaryConfigurationComponent } from './add-salary-configuration/add-salary-configuration.component';
import { IDropDownModel } from './../../../store/models/purchase';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { of, observable, empty, ReplaySubject } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material';
import { Month, TransactionType } from 'src/app/shared/enum';
import { ActivatedRoute } from '@angular/router';
import { EmployeeSalaryConfigService } from '../../services/employee-salary-config.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import 'rxjs/add/observable/empty';
import { AddAdvanceRecoveryComponent } from './add-advance-recovery/add-advance-recovery.component';
import { AttendanceService } from '../../services/attendance.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { GLOBAL } from 'src/app/shared/global';
import { takeUntil } from 'rxjs/operators';

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
  employeeSalary: IEmployeeSalary;
  isSalaryApproved = false;
  isNoError = true;
  errorMessage = '';
  hideColumsBounusFine = of({
    headers: ['Salary Component', 'Salary Allowance', 'Salary Deduction'],
    items: ['SalaryComponent', 'SalaryAllowance', 'SalaryDeduction']
  });
  hideColumsAccumulatedSalaryHeads = of({
    headers: ['Salary Component', 'Salary Allowance', 'Salary Deduction'],
    items: ['SalaryComponent', 'SalaryAllowance', 'SalaryDeduction']
  });
  employeeCurrencyAndAmount: any;
  showGenerateAttendanceButton = false;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    public dialog: MatDialog,
    private toastr: ToastrService,
    private appurl: AppUrlService,
    private activatedRoute: ActivatedRoute,
    private globalSharedService: GlobalSharedService,
    private salaryConfigService: EmployeeSalaryConfigService,
    private attendanceService: AttendanceService,
    private commonLoader: CommonLoaderService
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

    this.onInitForm();

    this.employeeSalary = {
      GrossSalary: 0,
      NetSalary: 0,
      EmployeeId: 0,
      Month: 0,
      HourlyRate: 0,
      SalaryHeadList: []
    };

    this.getEmployeeBasicPayAndCurrency();
    if (this.selectedMonth.value === 0) {
      this.isNoError = false;
      this.errorMessage =
        'Please select month for which payroll is to be generated';
    }
  }

  onInitForm() {
    this.monthlySalaryBreakdown = {
      GrossSalary: 0,
      HourlyRate: 0,
      Month: '',
      NetSalary: 0,
      SalaryPaidAmount: 0,
      Status: ''
    };
    this.accumulatedList$ = of();
    this.bonusAndFineList$ = of();
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
        MonthlyAmount: this.employeeCurrencyAndAmount.MonthlyAmount,
        CapacityBuilding: this.employeeCurrencyAndAmount.CapacityBuilding,
        Security: this.employeeCurrencyAndAmount.Security,
        ForAllEmployees: false
      }
    });
    // refresh the data after new request created
    dialogRef.componentInstance.onAddSalaryConfigurationRefresh.subscribe(
      () => {}
    );
    dialogRef.afterClosed().subscribe(() => {
      this.getEmployeeBasicPayAndCurrency();
    });
  }
  //#endregion

  // #region Add Bonus
  addBonus(): void {
    if (this.monthlySalaryBreakdown.Month === '' || this.isSalaryApproved) {
      return;
    }

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
    dialogRef.componentInstance.onAddBonusRefresh.subscribe(() => {});
    dialogRef.afterClosed().subscribe(() => {
      this.getEmployeeBonusFineSalaryHead();
      this.getEmployeePayroll();
    });
  }
  //#endregion
  // #region Add Bonus
  addFine(): void {
    if (this.monthlySalaryBreakdown.Month === '' || this.isSalaryApproved) {
      return;
    }
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
    dialogRef.componentInstance.onAddFineRefresh.subscribe(() => {});
    dialogRef.afterClosed().subscribe(() => {
      this.getEmployeeBonusFineSalaryHead();
      this.getEmployeePayroll();
    });
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
    this.getEmployeePayroll();
  }
  empActionEvents(event: any) {
    console.log(event.item);
    this.errorMessage = '';
    this.isNoError = true;
  }

  getEmployeeBasicPayAndCurrency() {
    this.salaryConfigService
      .getEmployeeBasicPayAndCurrency(this.employeeId)
      .subscribe(x => {
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
    this.salaryConfigService
      .getEmployeeBonusFineSalaryHead(model)
      .subscribe(x => {
        if (x && x.BonusFineSalaryHead && x.BonusFineSalaryHead.length > 0) {
          this.bonusAndFineList$ = of(
            x.BonusFineSalaryHead.map(y => {
              return {
                Id: y.Id,
                SalaryComponent: y.SalaryHeadName,
                SalaryAllowance:
                  y.TransactionTypeId === TransactionType.Debit ? y.Amount : 0,
                SalaryDeduction:
                  y.TransactionTypeId === TransactionType.Credit ? y.Amount : 0
              };
            })
          );
        } else {
          this.bonusAndFineList$ = of([]);
        }
      });
  }

  bonusFineEvents(event) {
    if (event.type === 'delete') {
      this.salaryConfigService
        .deleteEmployeeBonusFineSalaryHead(event.item.Id)
        .subscribe(
          x => {
            if (x) {
              this.toastr.success('Item Deleted Successfully');
              this.getEmployeeBonusFineSalaryHead();
              this.getEmployeePayroll();
            } else {
              this.toastr.warning('Please try again');
            }
          },
          error => {
            this.toastr.warning(error);
          }
        );
    }
  }

  getEmployeeAccumulatedSalaryHead() {
    this.salaryConfigService
      .getEmployeeAccumulatedSalaryHead(this.employeeId)
      .subscribe(x => {
        if (x && x.BonusFineSalaryHead && x.BonusFineSalaryHead.length > 0) {
          this.bonusAndFineList$ = of(
            x.BonusFineSalaryHead.map(y => {
              return {
                Id: y.Id,
                SalaryComponent: y.SalaryHeadName,
                SalaryAllowance:
                  y.TransactionTypeId === TransactionType.Debit ? y.Amount : 0,
                SalaryDeduction:
                  y.TransactionTypeId === TransactionType.Credit ? y.Amount : 0
              };
            })
          );
        }
      });
  }

  getEmployeePayroll() {
    const model = {
      EmployeeId: this.employeeId,
      Month: this.selectedMonth.value
    };

    this.salaryConfigService.getEmployeePayroll(model).subscribe(
      x => {
        this.showGenerateAttendanceButton = false;
        if (x && x.payroll) {
          this.isNoError = true;
          this.monthlySalaryBreakdown.NetSalary = x.payroll.NetSalary;
          this.monthlySalaryBreakdown.GrossSalary = x.payroll.GrossSalary;
          this.monthlySalaryBreakdown.SalaryPaidAmount = x.payroll.SalaryPaid;
          this.monthlySalaryBreakdown.Status = x.payroll.Status;
          this.isSalaryApproved = x.payroll.IsSalaryApproved;

          this.monthlySalaryBreakdown.HourlyRate = x.payroll.HourlyRate;
          this.monthlySalaryBreakdown.Month = Month[this.selectedMonth.value];

          if (!this.isSalaryApproved) {
            this.accumulatedList$ = of(
              x.payroll.AccumulatedPayrollHeadList.map(y => {
                return {
                  Id: y.Id,
                  SalaryComponent: y.PayrollHeadName,
                  SalaryAllowance:
                    y.TransactionType === TransactionType.Debit ? y.Amount : 0,
                  SalaryDeduction:
                    y.TransactionType === TransactionType.Credit ? y.Amount : 0
                };
              })
            );
          } else {
            this.accumulatedList$ = of(
              x.payroll.SavedAccumulatedPayrollHeadList.map(y => {
                return {
                  Id: y.Id,
                  SalaryComponent: y.PayrollHeadName,
                  SalaryAllowance: y.SalaryAllowance,
                  SalaryDeduction: y.SalaryDeduction
                };
              })
            );
          }
        } else {
          this.isNoError = false;
          this.toastr.warning('Cannot retrieve data. Please try again');
        }
      },
      error => {
        this.onInitForm();
        this.isNoError = false;
        this.errorMessage = error;
        if (error === 'Employee has no attendance for payroll') {
          this.showGenerateAttendanceButton = true;
        } else {
          this.showGenerateAttendanceButton = false;
        }
        // this.toastr.warning(error);

        // Payroll Hours are not configured for this month. Please Add Payroll Hours for the selected month for this
        // employee's Attendance Group.
      }
    );
  }

  approveSalary() {
    if (this.monthlySalaryBreakdown.Month === '' || this.isSalaryApproved) {
      return;
    }

    if (this.isSalaryApproved) {
      return;
    }

    this.employeeSalary = {
      GrossSalary: this.monthlySalaryBreakdown.GrossSalary,
      NetSalary: this.monthlySalaryBreakdown.NetSalary,
      EmployeeId: this.employeeId,
      Month: this.selectedMonth.value,
      HourlyRate: this.monthlySalaryBreakdown.HourlyRate,
      SalaryHeadList: []
    };

    this.accumulatedList$.subscribe(x => {
      x.forEach(element => {
        this.employeeSalary.SalaryHeadList.push({
          Id: element.Id,
          SalaryAllowance: element.SalaryAllowance,
          SalaryDeduction: element.SalaryDeduction
        });
      });
    });

    this.salaryConfigService.approvePayroll(this.employeeSalary).subscribe(
      x => {
        if (x) {
          this.getEmployeePayroll();
          this.toastr.success('Salary Approved');
        } else {
          this.toastr.warning('Please try again');
        }
      },
      error => {
        this.toastr.warning(error);
      }
    );
  }

  addAdvance() {
    if (this.monthlySalaryBreakdown.Month === '' || this.isSalaryApproved) {
      return;
    }

    if (this.selectedMonth.value === 0) {
      this.toastr.warning('Please select Month');
      return;
    }
    // NOTE: It open AddFine dialog and passed the data into the AddFineComponent Model
    const dialogRef = this.dialog.open(AddAdvanceRecoveryComponent, {
      width: '500px',
      autoFocus: false,
      data: {
        EmployeeId: this.employeeId,
        SelectedMonth: this.selectedMonth.value
      }
    });
    // refresh the data after new request created
    dialogRef.afterClosed().subscribe(() => {
      this.getEmployeeBonusFineSalaryHead();
      this.getEmployeePayroll();
    });
  }

  revokeSalary() {
    if (!this.isSalaryApproved) {
      return;
    }

    const model = {
      EmployeeId: this.employeeId,
      Month: this.selectedMonth.value
    };

    this.salaryConfigService.revokeEmployeePayroll(model).subscribe(
      x => {
        if (x) {
          this.getEmployeePayroll();
          this.toastr.success('Salary Revoked');
        } else {
          this.toastr.warning('Please try again');
        }
      },
      error => {
        this.toastr.warning(error);
      }
    );
  }

  markWholeMonthAttendance() {
    this.isNoError = true;
    const model = {
      EmployeeId: this.employeeId,
      Month: this.selectedMonth.value
    };
    this.commonLoader.showLoader();
    this.attendanceService.markWholeMonthAttendance(model).subscribe(
      res => {
        if (res) {
          this.toastr.success('Attendance Marked Successfully!');
          this.getEmployeePayroll();
          this.commonLoader.hideLoader();
        } else {
          this.commonLoader.hideLoader();
        }
      },
      err => {
        this.isNoError = false;
        this.errorMessage = err;
        this.commonLoader.hideLoader();
      }
    );
  }

  //#region "Download pdf of monthly pay slip"
  onExportMonthlyPaySlipPdf() {
    // if (this.monthlySalaryBreakdown.Month === '' || this.isSalaryApproved) {
    //   return;
    // }

    // if (this.isSalaryApproved) {
    //   return;
    // }
    const data: any = {
      EmployeeId: this.employeeId,
      Month: this.selectedMonth.value
    };
    this.globalSharedService
      .getFile(
        this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetMonthlyPaySlipPdf,
        data
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe();
  }
  //#endregion
}

export interface IMonthlySalaryBreakdown {
  HourlyRate: any;
  Month: string;
  GrossSalary: number;
  NetSalary: number;
  Status: string;
  SalaryPaidAmount: number;
}

export interface IEmployeeSalary {
  GrossSalary: number;
  NetSalary: number;
  EmployeeId: number;
  Month: number;
  HourlyRate: number;
  SalaryHeadList: ISalaryHead[];
}

export interface ISalaryHead {
  Id: number;
  SalaryAllowance: number;
  SalaryDeduction: number;
}
