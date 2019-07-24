import { Component, OnInit, Inject, EventEmitter, OnChanges , } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import {
  IHiringReuestDataSource,
  IBudgetLineModel,
  IOfficeListModel,
  IJobGradeModel,
  IHiringRequestDetailModel,
  IProfessionList
} from '../models/hiring-requests-model';
import { ICurrencyList } from 'src/app/dashboard/accounting/gain-loss-report/gain-loss-report.model';
import { HiringRequestsService } from '../hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Component({
  selector: 'app-add-hiring-requests',
  templateUrl: './add-hiring-requests.component.html',
  styleUrls: ['./add-hiring-requests.component.scss']
})
export class AddHiringRequestsComponent implements OnInit , OnChanges {
  //#region "Input/Output"
  onHiringRequestListRefresh = new EventEmitter();
  onUpdateHiringRequestListRefresh = new EventEmitter<any[]>();

  //#endregion

  //#region "variables"
  hiringRequestForm: FormGroup;

  // listing data source
  employeeList: any[] = [];
  budgetLineList: IBudgetLineModel[] = [];
  currencyList: ICurrencyList[] = [];
  officeList: IOfficeListModel[] = [];
  jobGrageList: IJobGradeModel[] = [];
  professionList: IProfessionList[] = [];
  hiringRequestDetail: any;

  projectId: number;
  activityId: number;

  // loader
  addHiringRequestLoader = false;
  //#endregion

  constructor(
    public dialogRef: MatDialogRef<AddHiringRequestsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IHiringReuestDataSource,
    private fb: FormBuilder,
    public toastr: ToastrService,
    public hiringRequestService: HiringRequestsService
  ) {
    this.budgetLineList = data.BudgetLineList;
    this.employeeList = data.EmployeeList;
    this.currencyList = data.CurrencyList;
    this.officeList = data.OfficeList;
    this.jobGrageList = data.JobGradeList;
    this.projectId = data.ProjectId;
    this.hiringRequestDetail = data.HiringRequestDetail;
    this.professionList = data.ProfessionList;
  }

  ngOnInit() {
    this.initForm();
    console.log('check', this.hiringRequestDetail);
    if (
      this.hiringRequestDetail != null &&
      this.hiringRequestDetail !== undefined
    ) {
      this.setHirectingrequestDetail();
    }
  }
  ngOnChanges() {
    console.log('openvalue', this.hiringRequestDetail);
    this.initForm();
  }
  //#region "initForm"
  initForm() {
    this.hiringRequestForm = this.fb.group({
      Description: ['', Validators.required],
      Position: ['', [Validators.required]],
      ProfessionId: ['', Validators.required],
      TotalVacancies: [null, Validators.required],
      FilledVacancies: [null],
      BasicPay: [null, Validators.required],
      CurrencyId: [null, Validators.required],
      BudgetLineId: [null, Validators.required],
      OfficeId: [null, Validators.required],
      GradeId: [null, Validators.required],
      ProjectId: [null]
    });
  }
  //#endregion

  onFormSubmit(data): void {
    if (data.HiringRequestId != null && data.HiringRequestId != undefined) {
      this.EditHiringRequestDetail(data);
    } else {
      this.AddHiringRequest(data);
    }
  }

  //#region "AddHiringRequest"
  AddHiringRequest(data: IHiringRequestDetailModel) {
    console.log('projectId'
    + this.projectId);
    const project =  this.projectId;
    console.log(' const' + project);
    if (this.hiringRequestForm.valid) {
      this.addHiringRequestLoader = true;
      const hiringRequestDetail: IHiringRequestDetailModel = {
        HiringRequestCode: data.HiringRequestCode,
        Description: data.Description,
        ProfessionId: data.ProfessionId,
        Position: data.Position,
        TotalVacancies: data.TotalVacancies,
        FilledVacancies: data.FilledVacancies,
        BasicPay: data.BasicPay,
        BudgetLineId: data.BudgetLineId,
        OfficeId: data.OfficeId,
        GradeId: data.GradeId,
        EmployeeID: data.EmployeeID,
        ProjectId: this.projectId,
        IsCompleted: data.IsCompleted,
        CurrencyId: data.CurrencyId
      };
      this.hiringRequestService
        .AddHiringRequestDetail(hiringRequestDetail)
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200) {
              this.toastr.success('New request is created successfully');
            } else {
              this.toastr.error(response.message);
            }
            this.onCancelPopup();
            this.hiringRequestListRefresh();
            this.addHiringRequestLoader = false;
          },
          error => {
            this.toastr.error('Someting went wrong. Please try again');
            this.addHiringRequestLoader = false;
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

  //#region "hiringRequestListRefresh"
  hiringRequestListRefresh() {
    this.onHiringRequestListRefresh.emit();
  }
  //#endregion

hiringRequestListRefreshOnUpdate(data: any) {
  this.onUpdateHiringRequestListRefresh.emit(data);
}

  //#region "setHirectingrequestDetail"
  setHirectingrequestDetail() {
    this.hiringRequestForm = this.fb.group({
      Description: [this.hiringRequestDetail.Description],
      HiringRequestId: [this.hiringRequestDetail.HiringRequestId],
      HiringRequestCode: [this.hiringRequestDetail.HiringRequestCode],
      Position: [this.hiringRequestDetail.Position],
      ProfessionId: [this.hiringRequestDetail.ProfessionId],
      TotalVacancies: [this.hiringRequestDetail.TotalVacancies],
      FilledVacancies: [this.hiringRequestDetail.FilledVacancies],
      BasicPay: [this.hiringRequestDetail.BasicPay],
      CurrencyId: [this.hiringRequestDetail.CurrencyId],
      BudgetLineId: [this.hiringRequestDetail.BudgetLineId],
      OfficeId: [this.hiringRequestDetail.OfficeId],
      EmployeeID: [this.hiringRequestDetail.EmployeeID],
      GradeId: [this.hiringRequestDetail.GradeId],
      ProjectId: [this.hiringRequestDetail.ProjectId],
      RequestedBy: [this.hiringRequestDetail.RequestedBy]
    });
  }
  //#endregion

  //#region "EditHiringRequest"
  EditHiringRequestDetail(data: IHiringRequestDetailModel) {
    if (this.hiringRequestForm.valid) {
      this.addHiringRequestLoader = true;
      const hiringRequestDetail: IHiringRequestDetailModel = {
        HiringRequestId: data.HiringRequestId,
        HiringRequestCode: data.HiringRequestCode,
        Description: data.Description,
        ProfessionId: data.ProfessionId,
        Position: data.Position,
        TotalVacancies: data.TotalVacancies,
        FilledVacancies: data.FilledVacancies,
        BasicPay: data.BasicPay,
        BudgetLineId: data.BudgetLineId,
        OfficeId: data.OfficeId,
        GradeId: data.GradeId,
        EmployeeID: data.EmployeeID,
        ProjectId: this.projectId,
        IsCompleted: data.IsCompleted,
        CurrencyId: data.CurrencyId,
        RequestedBy: data.RequestedBy
      };
      this.hiringRequestService
        .EditHiringRequestDetail(hiringRequestDetail)
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200) {
              this.onCancelPopup();
              this.hiringRequestListRefreshOnUpdate(hiringRequestDetail);
              this.hiringRequestForm.reset(this.hiringRequestForm.value);
              this.toastr.success('Hiring request updated successfully');
            } else {
              this.toastr.error(response.message);
            }
            this.addHiringRequestLoader = false;
          },
          error => {
            this.toastr.error('Someting went wrong');
            this.addHiringRequestLoader = false;
          }
        );
    }
  }
  //#endregion
}
