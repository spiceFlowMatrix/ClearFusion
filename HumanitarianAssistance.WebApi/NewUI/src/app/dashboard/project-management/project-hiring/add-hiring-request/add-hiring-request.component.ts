import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import {
  FormGroup,
  FormBuilder,
  Validators,
  AbstractControl
} from '@angular/forms';
import { Observable, forkJoin, ReplaySubject, of } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { takeUntil } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import {
  OfficeDetailModel,
  IHiringRequestModel
} from '../models/hiring-requests-models';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { DISABLED } from '@angular/forms/src/model';

@Component({
  selector: 'app-add-hiring-request',
  templateUrl: './add-hiring-request.component.html',
  styleUrls: ['./add-hiring-request.component.scss']
})
export class AddHiringRequestComponent implements OnInit {
  projectId: number;
  OfficeId: number;
  isEditable = true;
  isFormSubmitted = false;
  hiringRequestId: number;
  hiringRequestCode: string;
  addHiringRequestForm: FormGroup;
  professionList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
  jobShiftList$: Observable<IDropDownModel[]>;
  jobGradeList$: Observable<IDropDownModel[]>;
  countryList$: Observable<IDropDownModel[]>;
  designationList$: Observable<IDropDownModel[]>;
  budgetLineList$: Observable<IDropDownModel[]>;
  departmentList$: Observable<IDropDownModel[]>;
  currencyList$: Observable<IDropDownModel[]>;
  educationDegreeList$: Observable<IDropDownModel[]>;
  provinceList$: Observable<IDropDownModel[]>;
  onAddHiringRequestListRefresh = new EventEmitter();
  onUpdateHiringRequestListRefresh = new EventEmitter();
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    public dialogRef: MatDialogRef<AddHiringRequestComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private commonLoader: CommonLoaderService,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private loader: CommonLoaderService
  ) {
    this.addHiringRequestForm = this.fb.group({
      ProjectId: [null],
      HiringRequestId: [null],
      HiringRequestCode: [null],
      TotalVacancy: [null, [Validators.required]],
      Position: [null, [Validators.required]],
      Office: [null, [Validators.required]],
      BudgetLine: [null, [Validators.required]],
      ContractType: [null, [Validators.required]],
      ContractDuration: [null, [Validators.required]],
      JobGrade: [null, [Validators.required]],
      AnouncingDate: [null, [Validators.required]],
      ClosingDate: [null, [Validators.required]],
      JobType: [null, [Validators.required]],
      PayCurrency: [null, [Validators.required]],
      PayHourlyRate: [null, [Validators.required]],
      JobShift: [null, [Validators.required]],
      Experience: [null, [Validators.required]],
      EducationDegree: [null, [Validators.required]],
      Profession: [null, [Validators.required]],
      SpecificDutiesAndResponsibilities: [null, [Validators.required]],
      KnowledgeAndSkillsRequired: [null, [Validators.required]],
      SubmissionGuidelines: [null, [Validators.required]],
      Background: [null, [Validators.required]],
      Nationality: [null, [Validators.required]],
      ProvinceId: [null, [Validators.required]]
    });
    this.jobShiftList$ = of([
      { name: 'Day', value: 1 },
      { name: 'Night', value: 2 }
    ] as IDropDownModel[]);
  }

  ngOnInit() {
    this.projectId = this.data.projectId;
    this.hiringRequestId = this.data.hiringRequestId;
    this.addHiringRequestForm.controls['ProjectId'].setValue(this.projectId);
    this.addHiringRequestForm.controls['HiringRequestId'].setValue(
      this.hiringRequestId
    );
    if (this.data.hiringRequestId !== 0) {
      this.isEditable = false;
      this.getAllHiringRequestDetail();
    } else {
      this.getHiringRequestCode();
    }
    forkJoin([
      this.getAllOfficeList(),
      this.getAllCountryList(),
      this.getDesignationList(),
      this.getBudgetLineList(),
      this.getJobGradeList(),
      this.getCurrencyList(),
      this.getEducationDegreeList(),
      this.getAllProfessionList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeOfficeList(result[0]);
        this.subscribeCountryList(result[1]);
        this.subscribeDesignationList(result[2]);
        this.subscribeBudgetLineList(result[3]);
        this.subscribeJobGradeList(result[4]);
        this.subscribeCurrencyList(result[5]);
        this.subscribeEducationDegreeList(result[6]);
        this.subscribeProfessionList(result[7]);
      });
  }
  //#region "Get all office List"
  getAllOfficeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetOfficeList();
  }
  subscribeOfficeList(response: any) {
    this.commonLoader.hideLoader();
    this.officeList$ = of(
      response.data.map(y => {
        return {
          value: y.OfficeId,
          name: y.OfficeName
        };
      })
    );
  }
  //#endregion
  //#region "Get all country List"
  getAllCountryList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetCountryList();
  }
  subscribeCountryList(response: any) {
    this.commonLoader.hideLoader();
    this.countryList$ = of(
      response.data.map(y => {
        return {
          value: y.CountryId,
          name: y.CountryName
        };
      })
    );
  }
  //#endregion
  //#region "Get all designation List"
  getDesignationList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.getDesignationList();
  }
  subscribeDesignationList(response: any) {
    this.commonLoader.hideLoader();
    this.designationList$ = of(
      response.data.map(y => {
        return {
          value: y.DesignationId,
          name: y.Designation
        };
      })
    );
  }
  //#endregion
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
  //#region "Get all job grade list"
  getJobGradeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetJobGradeList();
  }
  subscribeJobGradeList(response: any) {
    this.commonLoader.hideLoader();
    this.jobGradeList$ = of(
      response.data.map(y => {
        return {
          value: y.GradeId,
          name: y.GradeName
        };
      })
    );
  }
  //#endregion
  //#region "Get all currency list"
  getCurrencyList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetCurrencyList();
  }
  subscribeCurrencyList(response: any) {
    this.commonLoader.hideLoader();
    this.currencyList$ = of(
      response.data.map(y => {
        return {
          value: y.CurrencyId,
          name: y.CurrencyName
        };
      })
    );
  }
  //#endregion
  //#region "Get all education degree list"
  getEducationDegreeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetEducationDegreeList();
  }
  subscribeEducationDegreeList(response: any) {
    this.commonLoader.hideLoader();
    this.educationDegreeList$ = of(
      response.map(y => {
        return {
          value: y.Id,
          name: y.Name
        };
      })
    );
  }
  //#endregion
  //#region "Get all profession list"
  getAllProfessionList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetProfessionList();
  }
  subscribeProfessionList(response: any) {
    this.commonLoader.hideLoader();
    this.professionList$ = of(
      response.data.map(y => {
        return {
          value: y.ProfessionId,
          name: y.ProfessionName
        };
      })
    );
  }
  //#endregion
  onChangeNationality(CountryId: number) {
    this.hiringRequestService
      .getAllProvinceListByCountryId([CountryId])
      .subscribe(x => {
        this.provinceList$ = of(
          x.data.map(element => {
            return {
              value: element.ProvinceId,
              name: element.ProvinceName
            };
          })
        );
      });
  }

  //#region "Get Department List"
  getDepartmentList(officeId) {
    this.hiringRequestService.getDepartmentList(officeId).subscribe(x => {
      this.departmentList$ = of(
        x.data.Departments.map(element => {
          return {
            value: element.DepartmentId,
            name: element.DepartmentName
          };
        })
      );
    });
  }
  //#endregion
  //#region "Get hiring request code"
  getHiringRequestCode() {
    this.commonLoader.hideLoader();
    this.hiringRequestService
      .GetHiringRequestCode(this.projectId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe((response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          this.hiringRequestCode = response.data;
          this.addHiringRequestForm.controls['HiringRequestCode'].setValue(
            this.hiringRequestCode
          );
        }
      });
  }
  //#endregion
  //#region "Get Hiring request details for edit"
  getAllHiringRequestDetail() {
    this.hiringRequestService
      .GetAllProjectHiringRequestDetailByHiringRequestId(
        this.data.hiringRequestId
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          this.loader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.onChangeNationality(response.data.Country);
            this.hiringRequestCode = response.data.HiringRequestCode;
            this.addHiringRequestForm.setValue({
              HiringRequestId: response.data.HiringRequestId,
              HiringRequestCode: response.data.HiringRequestCode,
              ProjectId: response.data.ProjectId,
              TotalVacancy: response.data.TotalVacancy,
              Position: response.data.Position,
              Office: response.data.Office,
              ContractType: response.data.ContractType,
              ContractDuration: response.data.ContractDuration,
              AnouncingDate: response.data.AnouncingDate,
              ClosingDate: response.data.ClosingDate,
              JobType: response.data.JobType,
              JobShift: response.data.JobShift,
              Experience: response.data.Experience,
              BudgetLine: response.data.BudgetLine,
              EducationDegree: response.data.EducationDegree,
              JobGrade: response.data.JobGrade,
              PayCurrency: response.data.PayCurrency,
              PayHourlyRate: response.data.PayHourlyRate,
              Profession: response.data.Profession,
              SpecificDutiesAndResponsibilities:
                response.data.SpecificDutiesAndResponsibilities,
              KnowledgeAndSkillsRequired:
                response.data.KnowledgeAndSkillsRequired,
              SubmissionGuidelines: response.data.SubmissionGuidelines,
              Background: response.data.Background,
              ProvinceId: response.data.Province,
              Nationality: response.data.Country
            });
            this.addHiringRequestForm.controls['ContractDuration'].disable();
            this.addHiringRequestForm.controls['ContractType'].disable();
            this.addHiringRequestForm.controls['TotalVacancy'].disable();
            this.getDepartmentList(response.data.Office);
          }
          this.loader.hideLoader();
        },
        () => {
          this.loader.hideLoader();
        }
      );
  }
  //#endregion
  //#region "Add hiring request"
  AddHiringRequest(data: IHiringRequestModel) {
    data.AnouncingDate = StaticUtilities.getLocalDate(data.AnouncingDate);
    data.ClosingDate = StaticUtilities.getLocalDate(data.ClosingDate);
    this.isFormSubmitted = true;
    this.hiringRequestService.AddHiringRequestDetail(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.toastr.success('New request is created successfully');
          this.AddHiringRequestListRefresh();
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
  }
  //#endregion
  //#region "Edit hiring request"
  EditHiringRequest(data: IHiringRequestModel) {
    this.addHiringRequestForm.value.ClosingDate = StaticUtilities.getLocalDate(
      this.addHiringRequestForm.value.ClosingDate
    );
    this.addHiringRequestForm.value.AnouncingDate = StaticUtilities.getLocalDate(
      this.addHiringRequestForm.value.AnouncingDate
    );
    this.isFormSubmitted = true;
    this.hiringRequestService.EditHiringRequestDetail(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.toastr.success('Hiring request updated successfully');
          this.UpdateHiringRequestListRefresh();
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
  }
  //#endregion
  //#region "On hiring request list refresh"
  AddHiringRequestListRefresh() {
    this.onAddHiringRequestListRefresh.emit();
  }
  UpdateHiringRequestListRefresh() {
    this.onUpdateHiringRequestListRefresh.emit();
  }
  //#endregion
  //#region "On change duty station"
  onChangeDutyStation(e) {
    this.OfficeId = e;
    this.getDepartmentList(e);
  }
  //#endregion
  //#region "On form submission"
  onFormSubmit() {
    if (this.addHiringRequestForm.valid) {
      if (this.hiringRequestId === 0) {
        this.AddHiringRequest(this.addHiringRequestForm.getRawValue());
      } else {
        console.log(this.addHiringRequestForm.getRawValue());
        this.EditHiringRequest(this.addHiringRequestForm.getRawValue());
      }
    }
  }
  //#endregion
  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion
}
