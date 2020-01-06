import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { ReplaySubject, of, Observable, forkJoin } from 'rxjs';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { takeUntil } from 'rxjs/operators';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { IAnalyticalInfoList } from '../models/hiring-requests-models';

@Component({
  selector: 'app-add-analytical-info',
  templateUrl: './add-analytical-info.component.html',
  styleUrls: ['./add-analytical-info.component.scss']
})
export class AddAnalyticalInfoComponent implements OnInit {
  addAnalyticalInfoForm: FormGroup;
  isFormSubmitted = false;
  projectId: number;
  employeeId: number;
  hiringRequestId: number;
  budgetLineId: number;
  analyticalInfoList$: Observable<IAnalyticalInfoList[]>;
  accountList$: Observable<IDropDownModel[]>;
  budgetLineList$: Observable<IDropDownModel[]>;
  projectList$: Observable<IDropDownModel[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  onAddAnalyticalInfoRefresh = new EventEmitter();

  analyticalInfoHeaders$ = of([
    'Project',
    'Budget Line',
    'Account',
    'Percentage'
  ]);
  constructor(
    public dialogRef: MatDialogRef<AddAnalyticalInfoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private commonLoader: CommonLoaderService,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {
    //#region "Initialize candidate form"
    this.addAnalyticalInfoForm = this.fb.group({
      EmployeeID: [null],
      ProjectId: [null, [Validators.required]],
      BudgetlineId: [null, [Validators.required]],
      AccountCode: [null, [Validators.required]],
      SalaryPercentage: [null, [Validators.required]]
    });
    //#endregion
  }

  ngOnInit() {
    this.projectId = this.data.projectId;
    this.hiringRequestId = this.data.hiringRequestId;
    this.employeeId = this.data.employeeId;
    this.budgetLineId = this.data.budgetLineId;
    this.addAnalyticalInfoForm.controls['EmployeeID'].setValue(this.employeeId);
    forkJoin([
      this.getProjectList(),
      this.getBudgetLineList(),
      this.getAccountList(),
      this.getAnalyticalInfo()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeProjectList(result[0]);
        this.subscribeBudgetLineList(result[1]);
        this.subscribeAccountList(result[2]);
        this.subscribeAnalyticalInfo(result[3]);
      });
  }
  //#region "Get all budget line list"
  getBudgetLineList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetBudgetLineList(this.projectId);
  }
  subscribeBudgetLineList(response: any) {
    this.commonLoader.hideLoader();
    this.budgetLineList$ = of(
      response.data.map(y => {
        return {
          value: y.BudgetLineId,
          name: y.BudgetCodeName
        };
      })
    );
  }
  //#endregion
  //#region "Get all budget line list"
  getProjectList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetProjectList();
  }
  subscribeProjectList(response: any) {
    this.commonLoader.hideLoader();
    this.projectList$ = of(
      response.data.map(y => {
        return {
          value: y.ProjectId,
          name: y.ProjectName
        };
      })
    );
  }
  //#endregion
  //#region "Get all budget line list"
  getAccountList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetAccountList();
  }
  subscribeAccountList(response: any) {
    this.commonLoader.hideLoader();
    this.accountList$ = of(
      response.data.map(y => {
        return {
          value: y.AccountCode,
          name: y.AccountName
        };
      })
    );
  }
  //#endregion

  getAnalyticalInfo() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetAnalyticalInfoByEmployeeId(
      this.employeeId
    );
  }
  subscribeAnalyticalInfo(response: any) {
    this.commonLoader.hideLoader();
    if (response.data !== undefined) {
      let projectName: string;
      let accountName: string;
      this.analyticalInfoList$ = of(
        response.data.map(y => {
          this.projectList$.subscribe(res => {
            projectName = res.find(x => x.value === y.ProjectId).name;
          });
          this.accountList$.subscribe(res => {
            accountName = res.find(x => x.value === y.AccountCode).name;
          });
          return {
            Project: projectName,
            Budgetline: y.BudgetLineName,
            Account: accountName,
            Percentage: y.SalaryPercentage
          };
        })
      );
    }
    this.addAnalyticalInfoForm.patchValue({
      ProjectId: this.projectId,
      BudgetlineId: this.budgetLineId
    });
  }
  //#region "Refresh candidate status after adding analytical"
  AddAnalyticalInfoRefresh() {
    this.onAddAnalyticalInfoRefresh.emit();
  }
  // #endregion
  //#region "on cancel popup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion
  //#region "On form submission"
  onFormSubmit(data: any) {
    let allowedPercentage = 0;
    if (this.analyticalInfoList$ !== undefined) {
      this.analyticalInfoList$.subscribe(res => {
        res.forEach(element => {
          allowedPercentage = allowedPercentage + (+element.Percentage);
        });
      });
    }
    if (this.addAnalyticalInfoForm.valid) {
      if (data.SalaryPercentage + allowedPercentage === 100) {
        this.isFormSubmitted = true;
        this.hiringRequestService.AddAnalyticalInfo(data).subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200) {
              this.toastr.success('Analytical info added successfully');
              // this.AddAnalyticalInfoRefresh();
              this.isFormSubmitted = false;
            } else {
              this.toastr.error(response.message);
              this.isFormSubmitted = false;
            }
            this.onCancelPopup();
          },
          () => {
            this.toastr.error('Someting went wrong. Please try again');
            this.isFormSubmitted = false;
          }
        );
      } else {
        this.toastr.warning('Percentage total can not be more then 100');
      }
    }
  }
  //#endregion
}
