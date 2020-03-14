import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { EmployeeSalaryConfigService } from '../../services/employee-salary-config.service';
import { Router } from '@angular/router';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { ReplaySubject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-administer-payroll',
  templateUrl: './administer-payroll.component.html',
  styleUrls: ['./administer-payroll.component.scss']
})
export class AdministerPayrollComponent implements OnInit {

  TotalCount = 0;
  payrollList = [];
  PageIndex = 0;
  PageSize = 10;
  err = null;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(private dialogRef: MatDialogRef<AdministerPayrollComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService,
    private salaryConfigService: EmployeeSalaryConfigService,
    private appurl: AppUrlService,
    private globalSharedService: GlobalSharedService,
    private router: Router) { }

  ngOnInit() {
    this.getEmployeeAdministrationDetails();
  }

  getEmployeeAdministrationDetails() {
    this.err = null;
    this.commonLoader.showLoader();
    const model = {
      EmpIds: this.data.SelectedEmployees.map(x => {
        return x.EmployeeId;
      }),
      Month: this.data.Month,
      OfficeId: this.data.OfficeId,
      PageIndex: this.PageIndex,
      PageSize: this.PageSize
    };
    this.salaryConfigService.getPayrollAdministrationDetailById(model).subscribe(res => {
      this.payrollList = [];
      if (res && res.PayrollAdminList) {
        this.TotalCount = res.RecordCount;
        res.PayrollAdminList.forEach(element => {
          this.payrollList.push(element);
        });
        console.log(this.payrollList);
      }
      this.commonLoader.hideLoader();
    }, err => {
      this.err = err;
      this.commonLoader.hideLoader();
    });
  }

  pageEvent(e) {
    this.PageIndex = e.pageIndex;
    this.PageSize = e.pageSize;
    this.getEmployeeAdministrationDetails();
  }

  closePopUp() {
    this.dialogRef.close();
  }

  approveAll() {
    this.commonLoader.showLoader();
    for (const e of this.payrollList) {
      if (e.IsApproved) {
        continue;
      }
      const employeeSalary = {
        GrossSalary: e.GrossSalary,
        NetSalary: e.NETSalary,
        EmployeeId: e.EmployeeId,
        Month: this.data.Month,
        SalaryHeadList: e.AccumulatedPayrollHeadList.map(x => {
          return {
            Id: x.Id,
            SalaryAllowance: (x.TransactionType === 1) ? 0 : x.Amount,
            SalaryDeduction: (x.TransactionType === 1) ? x.Amount : 0
          };
        })
      };
      this.salaryConfigService.approvePayroll(employeeSalary).subscribe(
        x => {
          // if (x) {
          //   this.getEmployeePayroll();
          //   this.toastr.success("Salary Approved");
          // } else {
          //   this.toastr.warning("Please try again");
          // }
        },
        error => {
          // this.toastr.warning(error);
        }
      );
    }
    this.commonLoader.hideLoader();
    this.toastr.success('Payroll approved successfully!');
    this.dialogRef.close();
  }

  revokeAll() {
    this.commonLoader.showLoader();
    for (const e of this.payrollList) {
      if (!e.IsApproved) {
        continue;
      }

      const model = {
        EmployeeId: e.EmployeeId,
        Month: this.data.Month
      };
      this.salaryConfigService.revokeEmployeePayroll(model).subscribe(
        x => {
          // if (x) {
          //   this.getEmployeePayroll();
          //   this.toastr.success("Salary Revoked");
          // } else {
          //   this.toastr.warning("Please try again");
          // }
        },
        error => {
          // this.toastr.warning(error);
        }
      );
    }
    this.commonLoader.hideLoader();
    this.toastr.success('Payroll revoked successfully!');
    this.dialogRef.close();
  }

  navigateToBreakdown(EmployeeId) {
    // this.dialogRef.close();
    const ref = document.createElement('a');
    ref.href = '/hr/employee/' + EmployeeId + '/?tabId=5';
    ref.target = '_blank';
    ref.click();
  }

  pdfExport(employeeId: number)
  {
    const data: any = {
      EmployeeId:employeeId,
      Month: this.data.Month
    };
    this.globalSharedService
      .getFile(
        this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetMonthlyPaySlipPdf,
        data
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe();
  }
}
