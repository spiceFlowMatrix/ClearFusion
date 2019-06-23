import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IBudgetLineModel } from '../models/budget-line.models';
import { BudgetLineService } from '../budget-line.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ToastrService } from 'ngx-toastr';
import {
  ICurrencyList,
  IProjectList
} from 'src/app/dashboard/accounting/gain-loss-report/gain-loss-report.model';
import { IProjectJobModel } from '../../project-jobs/project-jobsmodel';
import { IBudgetLineDataSource } from './budget-line-datasource.model';

@Component({
  selector: 'app-addbudget-line',
  templateUrl: './addbudget-line.component.html',
  styleUrls: ['./addbudget-line.component.scss']
})
export class AddbudgetLineComponent implements OnInit {
  currencyList: ICurrencyList[] = [];
  projectJobList: IProjectJobModel[] = [];
  projectList: IProjectList[] = [];
  budgetLineList: IBudgetLineModel[] = [];
  projectid: number;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: IBudgetLineDataSource,
    public dialogRef: MatDialogRef<AddbudgetLineComponent>,
    public fb: FormBuilder,
    public budgetService: BudgetLineService,
    public toastr: ToastrService
  ) {
    this.currencyList = data.CurrencyList;
    this.projectJobList = data.ProjectJobList;
    this.projectList = data.ProjectList;
    this.budgetLineList = data.BudgetLineDetailList;
    this.projectid = data.Projectid;
  }

  addBudgetForm: FormGroup;
  addBudgetLoader = false;
  Projectid: number;
  onListRefresh = new EventEmitter();

  ngOnInit() {
    this.initForm();
    this.projectid = this.data.Projectid;
  }

  //#region "initForm"
  initForm() {
    this.addBudgetForm = this.fb.group({
      CurrencyId: ['', [Validators.required]],
      InitialBudget: ['', [Validators.required]],
      ProjectJobId: ['', [Validators.required]],
      BudgetName: ['', [Validators.required]]
      // ProjectId: [''],

      // BudgetLineId: ['', [Validators.required]]
    });
  }
  //#endregion

  //#region "onAddVoucher"
  onAddBudget(data): void {
    const budgetData: IBudgetLineModel = {
      CurrencyId: data.value.CurrencyId,
      BudgetName: data.value.BudgetName,
      ProjectJobId: data.value.ProjectJobId,
      InitialBudget: data.value.InitialBudget,
      ProjectId: this.projectid,
      BudgetCode: data.value.BudgetCode,
      CurrencyName: data.value.CurrencyName,
      ProjectJobCode: data.value.ProjectJobCode,
      BudgetLineId: data.value.BudgetLineId,
      ProjectJobName: data.value.ProjectJobName,
      CreatedDate: data.value.CreatedDate,
      DebitPercentage: data.value.DebitPercentage
      // FinancialYearId: data.value.FinancialYearId, // calculate on backend
    };
    this.addBudget(budgetData);
  }
  //#endregion

  //#region "onAddBudeget"
  addBudget(data: IBudgetLineModel) {
    if (this.addBudgetForm.valid) {
      this.addBudgetLoader = true;
      const budgetData: IBudgetLineModel = {
        BudgetLineId: data.BudgetLineId,
        BudgetCode: data.BudgetCode,
        BudgetName: data.BudgetName,
        ProjectJobId: data.ProjectJobId,
        InitialBudget: data.InitialBudget,
        ProjectId: data.ProjectId,
        CurrencyId: data.CurrencyId,
        ProjectJobCode: data.ProjectJobCode,
        ProjectJobName: data.ProjectJobName,
        CurrencyName: data.CurrencyName,
        CreatedDate: data.CreatedDate,
        DebitPercentage: data.DebitPercentage
        // FinancialYearId: data.value.FinancialYearId, // calculate on backend
      };

      this.budgetService.AddBudgetLineDetail(budgetData).subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            this.onCancelPopup();
            this.budgetListRefresh();
            this.toastr.success('BudgetLine is created successfully');
          } else {
            this.toastr.error(response.message);
          }
          this.addBudgetLoader = false;
        },
        error => {
          this.toastr.error('Someting went wrong');
          this.addBudgetLoader = false;
        }
      );
    }
  }
  //#endregion

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion

  //#region "onListRefresh"
  budgetListRefresh() {
    this.onListRefresh.emit();
  }
  //#endregion

}

