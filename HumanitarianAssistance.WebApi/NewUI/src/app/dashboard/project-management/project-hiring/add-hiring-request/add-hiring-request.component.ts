import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
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

@Component({
  selector: 'app-add-hiring-request',
  templateUrl: './add-hiring-request.component.html',
  styleUrls: ['./add-hiring-request.component.scss']
})
export class AddHiringRequestComponent implements OnInit {
  projectId: number;
  OfficeId: number;
  AvailableVacancies: number;
  addHiringRequestForm: FormGroup;
  professionList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
  genderList$: Observable<IDropDownModel[]>;
  jobShiftList$: Observable<IDropDownModel[]>;
  jobList$: Observable<IDropDownModel[]>;
  countryList$: Observable<IDropDownModel[]>;
  provinceList$: Observable<IDropDownModel[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    public dialogRef: MatDialogRef<AddHiringRequestComponent>,
    private commonLoader: CommonLoaderService,
    private routeActive: ActivatedRoute,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {
    this.addHiringRequestForm = this.fb.group({
      ProjectId: [null],
      JobCategory: [null, [Validators.required]],
      MinEducationLevel: [null, [Validators.required]],
      TotalVacancy: [null, [Validators.required]],
      Position: [null, [Validators.required]],
      Organization: [null, [Validators.required]],
      Office: [null, [Validators.required]],
      ContractType: [null, [Validators.required]],
      ContractDuration: [null, [Validators.required]],
      Gender: [null, [Validators.required]],
      Nationality: [null, [Validators.required]],
      SalaryRange: [null, [Validators.required]],
      AnouncingDate: [null, [Validators.required]],
      ClosingDate: [null, [Validators.required]],
      Country: [null, [Validators.required]],
      Province: [null, [Validators.required]],
      JobType: [null, [Validators.required]],
      JobShift: [null, [Validators.required]],
      JobStatus: [null, [Validators.required]],
      Experience: [null, [Validators.required]],
      Background: [null, [Validators.required]],
      SpecificDutiesAndResponsibilities: [null, [Validators.required]],
      KnowledgeAndSkillsRequired: [null, [Validators.required]],
      SubmissionGuidelines: [null, [Validators.required]]
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
    this.projectId = 1;
    this.addHiringRequestForm.controls['ProjectId'].setValue(this.projectId);
    forkJoin([this.getAllOfficeList(), this.getAllCountryList()])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeOfficeList(result[0]);
        this.subscribeCountryList(result[1]);
      });
  }

  getAllOfficeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetOfficeList();
  }
  getAllCountryList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetCountryList();
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
  getAllProfessionList(OfficeId: number) {
    const model: OfficeDetailModel = {
      ProjectId: this.projectId,
      ProfessionId: OfficeId
    };
    this.hiringRequestService.GetProfessionListByOfficeId(model).subscribe(
      (response: IResponseData) => {
        this.commonLoader.showLoader();
        if (response.statusCode === 200 && response.data !== null) {
          this.professionList$ = of(
            response.data.map(element => {
              return {
                value: element.ProfessionId,
                name: element.ProfessionName
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
  getRemainingVacancy(JobId: any) {
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
    this.hiringRequestService.AddHiringRequestDetail(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.toastr.success('New request is created successfully');
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
    this.professionList$ = null;
    this.jobList$ = null;
    this.OfficeId = e;
    this.getAllProfessionList(e);
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
    this.getRemainingVacancy(e);
  }
  onFormSubmit(data: any) {
    if (this.addHiringRequestForm.valid) {
      this.AddHiringRequest(data);
      console.log(data);
    }
  }
  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion
}
