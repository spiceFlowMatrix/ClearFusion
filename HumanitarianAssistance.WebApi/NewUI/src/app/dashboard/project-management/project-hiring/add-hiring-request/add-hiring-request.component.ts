import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
  ValidatorFn,
  AbstractControl
} from '@angular/forms';
import { Observable, forkJoin, ReplaySubject, of } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { takeUntil } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ActivatedRoute } from '@angular/router';
import {
  OfficeDetailModel,
  IHiringRequestModel
} from '../models/hiring-requests-models';
import { HrService } from 'src/app/hr/services/hr.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-add-hiring-request',
  templateUrl: './add-hiring-request.component.html',
  styleUrls: ['./add-hiring-request.component.scss']
})
export class AddHiringRequestComponent implements OnInit {
  projectId: number;
  OfficeId: number;
  hiringRequestId: number;
  AvailableVacancies: number;
  hiringRequestDetail: IHiringRequestModel;
  addHiringRequestForm: FormGroup;
  professionList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
  genderList$: Observable<IDropDownModel[]>;
  jobShiftList$: Observable<IDropDownModel[]>;
  jobList$: Observable<IDropDownModel[]>;
  jobGradeList$: Observable<IDropDownModel[]>;
  countryList$: Observable<IDropDownModel[]>;
  provinceList$: Observable<IDropDownModel[]>;
  designationList$: Observable<IDropDownModel[]>;
  budgetLineList$: Observable<IDropDownModel[]>;
  departmentList$: Observable<IDropDownModel[]>;
  currencyList$: Observable<IDropDownModel[]>;
  educationDegreeList$: Observable<IDropDownModel[]>;
  onAddHiringRequestListRefresh = new EventEmitter();
  onUpdateHiringRequestListRefresh = new EventEmitter();
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    public dialogRef: MatDialogRef<AddHiringRequestComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private commonLoader: CommonLoaderService,
    private routeActive: ActivatedRoute,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private loader: CommonLoaderService,
  ) {
    this.addHiringRequestForm = this.fb.group({
      ProjectId: [null],
      HiringRequestId: [null],
      TotalVacancy: [null, [Validators.required, Validators.min(1),
      (control: AbstractControl) => Validators.max(this.AvailableVacancies)(control)]],
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
    });
    this.genderList$ = of([
      { name: 'Male', value: 1 },
      { name: 'Female', value: 2 },
      { name: 'Other', value: 3 }
    ] as IDropDownModel[]);

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
       this.getAllHiringRequestDetail();
    }
    forkJoin([this.getAllOfficeList(), this.getAllCountryList()])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeOfficeList(result[0]);
        this.subscribeCountryList(result[1]);
      });

    this.getDesignationList();
    this.getBudgetLineList();
    this.getJobGradeList();
    this.getCurrencyList();
    this.getEducationDegreeList();
    this.getAllProfessionList();
  }

  getAllOfficeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetOfficeList();
  }
  getAllCountryList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetCountryList();
  }
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
            this.addHiringRequestForm.setValue({
              HiringRequestId: response.data.HiringRequestId,
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
              KnowledgeAndSkillsRequired: response.data.KnowledgeAndSkillsRequired,
              SubmissionGuidelines: response.data.SubmissionGuidelines
            });

            this.getDepartmentList(response.data.Office);
          }
          this.loader.hideLoader();
        },
        error => {
          this.loader.hideLoader();
        }
      );
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
  // getAllProfessionList(OfficeId: number) {
  //   const model: OfficeDetailModel = {
  //     ProjectId: this.projectId,
  //     ProfessionId: OfficeId
  //   };
  //   this.hiringRequestService.GetProfessionListByOfficeId(model).subscribe(
  //     (response: IResponseData) => {
  //       this.commonLoader.showLoader();
  //       if (response.statusCode === 200 && response.data !== null) {
  //         this.professionList$ = of(
  //           response.data.map(element => {
  //             return {
  //               value: element.ProfessionId,
  //               name: element.ProfessionName
  //             } as IDropDownModel;
  //           })
  //         );
  //       }
  //       this.commonLoader.hideLoader();
  //     },
  //     error => {
  //       this.commonLoader.hideLoader();
  //     }
  //   );
  // }
  getAllJobList(PositionId: number) {
    const model: OfficeDetailModel = {
      ProjectId: this.projectId,
      ProfessionId: PositionId,
      OfficeId: this.OfficeId
    };
    this.hiringRequestService.GetJobList(model).subscribe(
      (response: IResponseData) => {
        this.commonLoader.showLoader();
        if (response.statusCode === 200 && response.data !== null) {
          this.jobList$ = of(
            response.data.map(element => {
              return {
                value: element.JobId,
                name: element.JobCode
              } as IDropDownModel;
            })
          );
        }
        this.commonLoader.hideLoader();
      },
      error => {
        this.commonLoader.hideLoader();
      }
    );
  }

  getAllProvinceList(CountryId: number) {
    this.hiringRequestService
      .getAllProvinceListByCountryId([CountryId])
      .subscribe(
        (response: IResponseData) => {
          this.commonLoader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.provinceList$ = of(
              response.data.map(element => {
                return {
                  value: element.ProvinceId,
                  name: element.ProvinceName
                } as IDropDownModel;
              })
            );
          }
          this.commonLoader.hideLoader();
        },
        error => {
          this.commonLoader.hideLoader();
        }
      );
  }
  getRemainingVacancy(JobId: number) {
    this.hiringRequestService.getRemainingVacancyByJobId(JobId).subscribe(
      (response: IResponseData) => {
        this.commonLoader.showLoader();
        if (response.statusCode === 200 && response.data !== null) {
          this.AvailableVacancies = response.data;
        }
        this.commonLoader.hideLoader();
      },
      error => {
        this.commonLoader.hideLoader();
      }
    );
  }

  //#region "AddHiringRequest"
  AddHiringRequest(data: IHiringRequestModel) {
    data.AnouncingDate = StaticUtilities.getLocalDate(data.AnouncingDate);
    data.ClosingDate = StaticUtilities.getLocalDate(data.ClosingDate);
    this.hiringRequestService.AddHiringRequestDetail(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.toastr.success('New request is created successfully');
          this.AddHiringRequestListRefresh();
        } else {
          this.toastr.error(response.message);
        }
        this.onCancelPopup();
      },
      error => {
        this.toastr.error('Someting went wrong. Please try again');
      }
    );
  }
  //#endregion

  //#region "EditHirinRequest"
  EditHiringRequest() {
    this.addHiringRequestForm.value.ClosingDate = StaticUtilities.getLocalDate(this.addHiringRequestForm.value.ClosingDate);
    this.addHiringRequestForm.value.AnouncingDate = StaticUtilities.getLocalDate(this.addHiringRequestForm.value.AnouncingDate);

    this.hiringRequestService.EditHiringRequestDetail(this.addHiringRequestForm.value).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.toastr.success('Hiring request updated successfully');
          this.UpdateHiringRequestListRefresh();
        } else {
          this.toastr.error(response.message);
        }
        this.onCancelPopup();
      },
      error => {
        this.toastr.error('Someting went wrong. Please try again');
      }
    );
  }
  //#endregion
  onChangeDutyStation(e) {
   // this.professionList$ = null;
    this.jobList$ = null;
    this.OfficeId = e;
    // this.getAllProfessionList(e);
    this.getDepartmentList(e);
  }

  onChangePosition(e) {
    this.jobList$ = null;
    this.getAllJobList(e);
  }
  onChangeCountry(e) {
    this.provinceList$ = null;
    this.getAllProvinceList(e);
  }
  onChangeJobCategory(e) {
   // this.getRemainingVacancy(e);
  }
  onFormSubmit(data: any) {
    if (this.addHiringRequestForm.valid) {
      if (this.hiringRequestId === 0) {
        this.AddHiringRequest(data);
      } else {
        this.EditHiringRequest();
      }
    }
  }
  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion

  //#region "hiringRequestListRefresh"
  AddHiringRequestListRefresh() {
    this.onAddHiringRequestListRefresh.emit();
  }
  UpdateHiringRequestListRefresh() {
    this.onUpdateHiringRequestListRefresh.emit();
  }
  //#endregion

  //#region "getDesignationList"
  getDesignationList() {
    this.hiringRequestService.getDesignationList().subscribe(x => {
      this.designationList$ = of(x.map(element => {
        return {
          value: element.DesignationId,
          name: element.Designation,
        };
      }));
    });
  }
  //#endregion

  //#region "getBudgetLineList"
  getBudgetLineList() {
    this.hiringRequestService.GetBudgetLineList(this.projectId).subscribe(x => {
      this.budgetLineList$ = of(x.data.map(element => {
        return {
          value: element.BudgetLineId,
          name: element.BudgetCodeName,
        };
      }));
    });
  }
  //#endregion

  //#region "getJobGradeList"
  getJobGradeList() {
    this.hiringRequestService.GetJobGradeList().subscribe(x => {
      this.jobGradeList$ = of(x.data.map(element => {
        return {
          value: element.GradeId,
          name: element.GradeName,
        };
      }));
    });
  }
  //#endregion

  //#region "getJobGradeList"
  getDepartmentList(officeId) {
    this.hiringRequestService.getDepartmentList(officeId).subscribe(x => {
      this.departmentList$ = of(x.data.Departments.map(element => {
        return {
          value: element.DepartmentId,
          name: element.DepartmentName,
        };
      }));
    });
  }
  //#endregion

   //#region "getCurrencyList"
  getCurrencyList() {
    this.hiringRequestService.GetCurrencyList().subscribe(x => {
      this.currencyList$ = of(x.data.map(element => {
        return {
          value: element.CurrencyId,
          name: element.CurrencyName,
        };
      }));
    });
  }
  //#endregion

  //#region "getEducationDegreeList"
  getEducationDegreeList() {
    this.hiringRequestService.GetEducationDegreeList().subscribe(x => {
      this.educationDegreeList$ = of(x.map(element => {
        return {
          value: element.Id,
          name: element.Name,
        };
      }));
    });
  }
  //#endregion

  //#region "getAllProfessionList"
  getAllProfessionList() {
    this.hiringRequestService.GetProfessionList().subscribe(x => {
      this.professionList$ = of(x.data.map(element => {
        return {
          value: element.ProfessionId,
          name: element.ProfessionName,
        };
      }));
    });
  }
  //#endregion
}
