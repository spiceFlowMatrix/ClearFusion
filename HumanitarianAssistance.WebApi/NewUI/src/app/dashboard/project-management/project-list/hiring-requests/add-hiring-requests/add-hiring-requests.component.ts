import {
  Component,
  OnInit,
  Inject,
  EventEmitter,
  OnChanges
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import {
  IHiringReuestDataSource,
  IBudgetLineModel,
  IOfficeListModel,
  IJobGradeModel,
  IHiringRequestDetailModel,
  IProfessionList,
  ICountryList,
  IProvinceList
} from '../models/hiring-requests-model';
import { ICurrencyList } from 'src/app/dashboard/accounting/gain-loss-report/gain-loss-report.model';
import { HiringRequestsService } from '../hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Component({
  selector: 'app-add-hiring-requests',
  templateUrl: './add-hiring-requests.component.html',
  styleUrls: ['./add-hiring-requests.component.scss']
})
export class AddHiringRequestsComponent implements OnInit, OnChanges {
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
  workingShift: any[] = [];
  genderList: any[] = [];
  countryList: ICountryList[] = [];
  provinceList: IProvinceList[] = [];
  projectId: number;
  activityId: number;
  officeSelectionFlag = false;
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
    this.officeSelectionFlag = data.officeSelectionFlag;
    this.genderList = data.gender;
    this.workingShift = data.workingShift;
    this.countryList = data.countryList;
    this.provinceList = data.provinceList;
  }

  ngOnInit() {
    this.initForm();
    if (
      this.hiringRequestDetail != null &&
      this.hiringRequestDetail !== undefined
    ) {
      this.setHiringRequestDetail();
    }
  }
  ngOnChanges() {
    this.initForm();
  }
  //#region "initForm"
  initForm() {
    this.hiringRequestForm = this.fb.group({
      BasicPay: ['', Validators.required],
      BudgetLineId: [null, Validators.required],
      CurrencyId: [null, Validators.required],
      Description: ['', Validators.required],
      GradeId: [null, Validators.required],
      OfficeId: [null, Validators.required],
      Position: ['', Validators.required],
      ProfessionId: [null, Validators.required],
      TotalVacancies: [null, Validators.required],
      AnouncingDate: [null, Validators.required],
      JobType: ['', Validators.required],
      Background: ['', Validators.required],
      JobStatus: ['', Validators.required],
      KnowladgeAndSkillRequired: ['', Validators.required],
      SalaryRange: [null, Validators.required],
      Shift: [null, Validators.required],
      ProviceId: [null, Validators.required],
      SpecificDutiesAndResponsblities: ['', Validators.required],
      SubmissionGuidlines: ['', Validators.required],
      ClosingDate: [null, Validators.required],
      ContractDuration: ['', Validators.required],
      ContractType: ['', Validators.required],
      CountryId: [null, Validators.required],
      GenderId: [null, Validators.required],
      MinimumEducationLevel: ['', Validators.required],
      Experience: ['', Validators.required],
      Organization: ['', Validators.required]
    });
  }
  //#endregion

  onFormSubmit(data): void {
    if (data.HiringRequestId != null && data.HiringRequestId !== undefined) {
      this.EditHiringRequestDetail(data);
    } else {
      this.AddHiringRequest(data);
    }
  }

  //#region "AddHiringRequest"
  AddHiringRequest(data: IHiringRequestDetailModel) {
    if (this.hiringRequestForm.valid) {
      this.addHiringRequestLoader = true;
      const hiringRequestDetail: IHiringRequestDetailModel = {
        HiringRequestCode: data.HiringRequestCode,
        Description: data.Description,
        ProfessionId: data.ProfessionId,
        Position: data.Position,
        TotalVacancies: data.TotalVacancies,
        BasicPay: data.BasicPay,
        BudgetLineId: data.BudgetLineId,
        OfficeId: data.OfficeId,
        GradeId: data.GradeId,
        IsCompleted: data.IsCompleted,
        CurrencyId: data.CurrencyId,
        AnouncingDate: data.AnouncingDate,
        JobType: data.JobType,
        Background: data.Background,
        JobStatus: data.JobStatus,
        KnowladgeAndSkillRequired: data.KnowladgeAndSkillRequired,
        SalaryRange: data.SalaryRange,
        Shift: data.Shift,
        ProviceId: data.ProviceId,
        SpecificDutiesAndResponsblities: data.SpecificDutiesAndResponsblities,
        SubmissionGuidlines: data.SubmissionGuidlines,
        ClosingDate: data.ClosingDate,
        ContractDuration: data.ContractDuration,
        ContractType: data.ContractType,
        CountryId: data.CountryId,
        GenderId: data.GenderId,
        MinimumEducationLevel: data.MinimumEducationLevel,
        Experience: data.Experience,
        Organization: data.Organization
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

  hiringRequestListRefreshOnUpdate(data: any) {
    this.onUpdateHiringRequestListRefresh.emit(data);
  }

  //#endregion

  //#region "setHirectingrequestDetail"
  setHiringRequestDetail() {
    this.hiringRequestForm = this.fb.group({
      Description: [this.hiringRequestDetail.Description],
      HiringRequestId: [this.hiringRequestDetail.HiringRequestId],
      HiringRequestCode: [this.hiringRequestDetail.HiringRequestCode],
      Position: [this.hiringRequestDetail.Position],
      ProfessionId: [this.hiringRequestDetail.ProfessionId],
      TotalVacancies: [this.hiringRequestDetail.TotalVacancies],
      BasicPay: [this.hiringRequestDetail.BasicPay],
      CurrencyId: [this.hiringRequestDetail.CurrencyId],
      BudgetLineId: [this.hiringRequestDetail.BudgetLineId],
      OfficeId: [this.hiringRequestDetail.OfficeId],
      GradeId: [this.hiringRequestDetail.GradeId],
      RequestedBy: [this.hiringRequestDetail.RequestedBy],
      AnouncingDate: [this.hiringRequestDetail.AnouncingDate],
      JobType: [this.hiringRequestDetail.JobType],
      Background: [this.hiringRequestDetail.Background],
      JobStatus: [this.hiringRequestDetail.JobStatus],
      KnowladgeAndSkillRequired: [
        this.hiringRequestDetail.KnowladgeAndSkillRequired
      ],
      SalaryRange: [this.hiringRequestDetail.SalaryRange],
      Shift: [this.hiringRequestDetail.Shift],
      ProviceId: [this.hiringRequestDetail.ProviceId],
      SpecificDutiesAndResponsblities: [
        this.hiringRequestDetail.SpecificDutiesAndResponsblities
      ],
      SubmissionGuidlines: [this.hiringRequestDetail.SubmissionGuidlines],
      ClosingDate: [this.hiringRequestDetail.ClosingDate],
      ContractDuration: [this.hiringRequestDetail.ContractDuration],
      ContractType: [this.hiringRequestDetail.ContractType],
      CountryId: [this.hiringRequestDetail.CountryId],
      GenderId: [this.hiringRequestDetail.GenderId],
      MinimumEducationLevel: [this.hiringRequestDetail.MinimumEducationLevel],
      Experience: [this.hiringRequestDetail.Experience],
      Organization: [this.hiringRequestDetail.Organization]
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
