import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ActivatedRoute } from '@angular/router';
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
  addNewCandidateForm: FormGroup;
  professionList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
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
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    public dialogRef: MatDialogRef<AddNewCandidateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private commonLoader: CommonLoaderService,
    private routeActive: ActivatedRoute,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private loader: CommonLoaderService,
    private purchaseService: PurchaseService,
    private globalSharedService: GlobalSharedService
  ) {
    this.addNewCandidateForm = this.fb.group({
      ProjectId: [null],
      HiringRequestId: [null],
      FirstName: [null, [Validators.required]],
      LastName: [null, [Validators.required]],
      Email: [null, [Validators.required, Validators.email]],
      PhoneNumber: [null, [Validators.required, Validators.maxLength(14)]],
      // AccountStatus: [null, [Validators.required]],
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
      Profession: [, [Validators.required]],
      // Grade: [null, [Validators.required]],
      // Office: [null, [Validators.required]],
      // TotalExperienceInYear: [null, [Validators.required]],
      RelevantExperienceInYear: [null, [Validators.required]],
      IrrelevantExperienceInYear: [null, [Validators.required]]
    });

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
    forkJoin([
      this.getAllOfficeList(),
      this.getAllCountryList(),
      this.getAllJobGradeList(),
      this.getAllProfessionList(),
      this.getAllEducationDegreeList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeOfficeList(result[0]);
        this.subscribeCountryList(result[1]);
        this.subscribeGradeList(result[2]);
        this.subscribeProfessionList(result[3]);
        this.subscribeEducationDegreeList(result[4]);
      });
      this.projectId = this.data.projectId;
      this.hiringRequestId = this.data.hiringRequestId;
      this.addNewCandidateForm.controls['ProjectId'].setValue(this.projectId);
      this.addNewCandidateForm.controls['HiringRequestId'].setValue(
        this.hiringRequestId
      );
      this.getPreviousYearsList();
      this.getAllMonthList();
  }

  getPreviousYearsList() {
    this.PreviousYearsList$ = this.purchaseService.getPreviousYearsList(40);
  }
  getAllMonthList() {
    const monthDropDown: IDropDownModel[] = [];
    for (let i = Month['January']; i <= Month['December']; i++) {
      monthDropDown.push({name: Month[i],
        value: i});
    }
    this.MonthsList$ = of(monthDropDown);
  }

  openInput() {
    document.getElementById('fileInput').click();
  }

  fileChange(file: any) {
    this.attachmentCV = [];
    this.attachmentCV.push(file);
  }
  getAllOfficeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetOfficeList();
  }
  getAllCountryList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetCountryList();
  }
  getAllJobGradeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetJobGradeList();
  }

  getAllProfessionList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetProfessionList();
  }
  getAllEducationDegreeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetEducationDegreeList();
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

  //#region "AddNewCandidate"
  AddNewCandidate(data: ICandidateDetailModel, attachmentCV) {
    this.commonLoader.showLoader();
    this.hiringRequestService.AddNewCandidateDetail(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.globalSharedService
              .uploadFile(FileSourceEntityTypes.HiringRequestCandidateCV, this.hiringRequestId, attachmentCV)
              .pipe(takeUntil(this.destroyed$))
              .subscribe(y => {
                this.commonLoader.hideLoader();
                this.toastr.success('New Candidate added successfully');
                this.AddCandidateListRefresh();
              });
        } else {
          this.commonLoader.hideLoader();
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

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion

  onChangeCountry(e) {
    this.provinceList$ = null;
    this.districtList$ = null;
    this.getAllProvinceList(e);
  }
  onChangeProvince(e) {
    this.districtList$ = null;
    this.getAllDistrictList(e);
  }
  onFormSubmit(data: any) {
    if (this.attachmentCV.length === 0) {
      this.toastr.warning('Please attach CV!');
      return;
    }
    if (this.addNewCandidateForm.valid) {
      this.AddNewCandidate(data, this.attachmentCV[0][0]);
    }
  }
      //#region "hiringRequestListRefresh"
      AddCandidateListRefresh() {
        this.onAddCandidateListRefresh.emit();
      }
      // #endregion
}
