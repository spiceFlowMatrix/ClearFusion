import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, of, forkJoin, ReplaySubject } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { takeUntil } from 'rxjs/operators';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ICandidateDetailModel } from '../models/hiring-requests-models';
import { PurchaseService } from 'src/app/store/services/purchase.service';
import { Month, FileSourceEntityTypes } from 'src/app/shared/enum';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-add-new-candidate',
  templateUrl: './add-new-candidate.component.html',
  styleUrls: ['./add-new-candidate.component.scss']
})
export class AddNewCandidateComponent implements OnInit {
  projectId: number;
  hiringRequestId: number;
  candidateId: number;
  isFormSubmitted = false;
  cvName: string;
  addNewCandidateForm: FormGroup;
  professionList$: Observable<IDropDownModel[]>;
  countryList$: Observable<IDropDownModel[]>;
  provinceList$: Observable<IDropDownModel[]>;
  districtList$: Observable<IDropDownModel[]>;
  accountStatusList$: Observable<IDropDownModel[]>;
  genderList$: Observable<IDropDownModel[]>;
  gradeList$: Observable<IDropDownModel[]>;
  PreviousYearsList$: Observable<IDropDownModel[]>;
  MonthsList$: Observable<IDropDownModel[]>;
  educationDegreeList$: Observable<IDropDownModel[]>;
  onAddCandidateListRefresh = new EventEmitter();
  attachmentCV: any[] = [];
  autoGenratedPassword: string;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    public dialogRef: MatDialogRef<AddNewCandidateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private commonLoader: CommonLoaderService,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private purchaseService: PurchaseService,
    private globalSharedService: GlobalSharedService
  ) {
    this.accountStatusList$ = of([
      { name: 'Prospective', value: 1 },
      { name: 'Active', value: 2 }
    ] as IDropDownModel[]);

    this.genderList$ = of([
      { name: 'Male', value: 1 },
      { name: 'Female', value: 2 },
      { name: 'Other', value: 3 }
    ] as IDropDownModel[]);
  }
  ngOnInit() {
    this.initCadidateForm();
    this.getAllMonthList();
    this.getPreviousYearsList();
    this.projectId = this.data.projectId;
    this.hiringRequestId = this.data.hiringRequestId;
    this.candidateId = this.data.candidateId;
    this.addNewCandidateForm.controls['ProjectId'].setValue(this.projectId);
    this.addNewCandidateForm.controls['HiringRequestId'].setValue(
      this.hiringRequestId
    );
    forkJoin([
      this.getAllCountryList(),
      this.getAllJobGradeList(),
      this.getAllProfessionList(),
      this.getAllEducationDegreeList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeCountryList(result[0]);
        this.subscribeGradeList(result[1]);
        this.subscribeProfessionList(result[2]);
        this.subscribeEducationDegreeList(result[3]);
      });
    if (this.candidateId > 0) {
      this.getCandidateDetails();
    } else {
      this.candidateId = 0;
    }
  }
  //#region "Initialize candidate form"
  initCadidateForm() {
    this.PasswordAutoGenrate();
    this.addNewCandidateForm = this.fb.group({
      ProjectId: [null],
      HiringRequestId: [null],
      FirstName: [null, [Validators.required]],
      LastName: [null, [Validators.required]],
      Email: [null, [Validators.required, Validators.email]],
      Password: [null, [Validators.required]],
      PhoneNumber: [
        null,
        [
          Validators.required,
          Validators.pattern(/^-?(0|[1-9]\d*)?$/),
          Validators.minLength(10),
          Validators.maxLength(14)
        ]
      ],
      EducationDegree: [null, [Validators.required]],
      Gender: [null, [Validators.required]],
      DateOfBirth: [null, [Validators.required]],
      Country: [null, [Validators.required]],
      Province: [null, [Validators.required]],
      District: [null, [Validators.required]],
      ExperienceYear: [null, [Validators.required]],
      ExperienceMonth: [null, [Validators.required]],
      PreviousWork: [null, [Validators.required]],
      CurrentAddress: [null, [Validators.required]],
      PermanentAddress: [null, [Validators.required]],
      Profession: [null, [Validators.required]],
      RelevantExperienceInYear: [null, [Validators.required]],
      IrrelevantExperienceInYear: [null, [Validators.required]],
      Remarks: [null, [Validators.required]]
    });
  }
  //#endregion
  //#region "Get candidate details"
  getCandidateDetails() {
    this.commonLoader.showLoader();
    this.hiringRequestService
      .GetCandidateDetailByCandidateId(this.candidateId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200 && response.data !== null) {
            this.cvName = response.data.AttachmentName;
            this.getAllProvinceList(response.data.CountryId);
            this.getAllDistrictList(response.data.ProvinceId);
            this.addNewCandidateForm.patchValue({
              FirstName: response.data.FirstName,
              LastName: response.data.LastName,
              Email: response.data.Email,
              Password: response.data.Password,
              PhoneNumber: response.data.PhoneNumber,
              EducationDegree: response.data.EducationDegreeId,
              Gender: response.data.GenderId,
              DateOfBirth: response.data.DateOfBirth,
              Country: response.data.CountryId,
              Province: response.data.ProvinceId,
              District: response.data.DistrictID,
              ExperienceYear: response.data.ExperienceYear,
              ExperienceMonth: response.data.ExperienceMonth,
              PreviousWork: response.data.PreviousWork,
              CurrentAddress: response.data.CurrentAddress,
              PermanentAddress: response.data.PermanentAddress,
              Profession: response.data.ProfessionId,
              RelevantExperienceInYear: response.data.RelevantExperienceInYear,
              IrrelevantExperienceInYear:
                response.data.IrrelevantExperienceInYear,
              Remarks: response.data.Remarks
            });
          }
          this.commonLoader.hideLoader();
        },
        () => {
          this.commonLoader.hideLoader();
        }
      );
  }
  //#region

  //#region "Auto genrate password for new candidate"
  PasswordAutoGenrate() {
    this.autoGenratedPassword = Math.random()
      .toString(36)
      .slice(-8);
  }
  //#endregion
  //#region "Get all month list for ExperienceInMonth dropdown"
  getAllMonthList() {
    const monthDropDown: IDropDownModel[] = [];
    for (let i = Month['January']; i <= Month['December']; i++) {
      monthDropDown.push({ name: Month[i], value: i });
    }
    this.MonthsList$ = of(monthDropDown);
  }
  //#endregion
  //#region "Get all previous years list for ExperienceInYears dropdown"
  getPreviousYearsList() {
    this.PreviousYearsList$ = this.purchaseService.getPreviousYearsList(40);
  }
  //#endregion

  //#region "Get all countries list for country dropdown"
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
  //#region "Get all job grades list for job grade dropdown"
  getAllJobGradeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetJobGradeList();
  }
  subscribeGradeList(response: any) {
    this.commonLoader.hideLoader();
    this.gradeList$ = of(
      response.data.map(y => {
        return {
          value: y.GradeId,
          name: y.GradeName
        };
      })
    );
  }
  //#endregion
  //#region "Get all profession list for profession dropdown"
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
  //#region "Get all education degree list for education dropdown"
  getAllEducationDegreeList() {
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
  //#region "Get all province list on selection of country dropdown"
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
  //#endregion
  //#region "Get all district list on selection of province dropdown"
  getAllDistrictList(ProvinceId: any) {
    this.hiringRequestService
      .GetAllDistrictvalueByProvinceId([ProvinceId])
      .subscribe(
        (response: IResponseData) => {
          this.commonLoader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.districtList$ = of(
              response.data.map(element => {
                return {
                  value: element.DistrictID,
                  name: element.District
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
  //#endregion
  //#region "Adding new candidate"
  addNewCandidate(data: ICandidateDetailModel, attachmentCV) {
    this.isFormSubmitted = true;
    this.hiringRequestService.AddNewCandidateDetail(data).subscribe(
      response => {
        // response.CommonId.Id is CandidateId
        if (response.StatusCode === 200 && response.CommonId.Id != null) {
          this.globalSharedService
            .uploadFile(
              FileSourceEntityTypes.HiringRequestCandidateCV,
              response.CommonId.Id,
              attachmentCV
            )
            .pipe(takeUntil(this.destroyed$))
            .subscribe(y => {
              this.toastr.success('New Candidate added successfully');
              this.isFormSubmitted = false;
              this.AddCandidateListRefresh();
            });
        } else {
          this.toastr.error(response.message);
          this.isFormSubmitted = false;
        }
        this.onCancelPopup();
      },
      error => {
        this.toastr.error('Someting went wrong. Please try again');
        this.isFormSubmitted = false;
      }
    );
  }
  //#endregion
  //#region "Adding new candidate"
  editCandidate(data: ICandidateDetailModel) {
    this.isFormSubmitted = true;
    data.CandidateId = this.candidateId;
    (data.IsCvUpdated = this.attachmentCV.length === 0 ? false : true),
      this.hiringRequestService.EditCandidateDetails(data).subscribe(
        response => {
          // response.CommonId.Id is CandidateId
          if (response.StatusCode === 200 && response.CommonId.Id != null) {
            if (this.attachmentCV.length !== 0) {
              this.globalSharedService.uploadFile(
                FileSourceEntityTypes.HiringRequestCandidateCV,
                response.CommonId.Id,
                this.attachmentCV[0][0]
              ).pipe(takeUntil(this.destroyed$))
              .subscribe(y => {
                this.toastr.success('New Candidate added successfully');
                this.isFormSubmitted = false;
              });
            }
          } else {
            this.toastr.error(response.message);
            this.isFormSubmitted = false;
          }
          this.onCancelPopup();
          this.AddCandidateListRefresh();
        },
        error => {
          this.toastr.error('Someting went wrong. Please try again');
          this.isFormSubmitted = false;
        }
      );
  }
  //#endregion

  //#region "Cv upload fucntionality"
  openInput() {
    document.getElementById('fileInput').click();
  }
  fileChange(file: any) {
    this.attachmentCV = [];
    this.attachmentCV.push(file);
  }
  //#endregion
  //#region "Refresh candidate list after adding new candidate"
  AddCandidateListRefresh() {
    this.onAddCandidateListRefresh.emit();
  }
  // #endregion
  //#region "On change country selection"
  onChangeCountry(e) {
    this.provinceList$ = null;
    this.districtList$ = null;
    this.getAllProvinceList(e);
  }
  //#endregion
  //#region "On change province selection"
  onChangeProvince(e) {
    this.districtList$ = null;
    this.getAllDistrictList(e);
  }
  //#endregion
  //#region "On form submission"
  onFormSubmit(data: any) {
    if (this.addNewCandidateForm.valid) {
      if (this.candidateId > 0) {
        this.editCandidate(data);
      } else {
        if (this.attachmentCV.length === 0) {
          this.toastr.warning('Please attach CV!');
          return;
        }
        this.addNewCandidate(data, this.attachmentCV[0][0]);
      }
    }
  }
  //#endregion
  //#region "on cancel popup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion
}
