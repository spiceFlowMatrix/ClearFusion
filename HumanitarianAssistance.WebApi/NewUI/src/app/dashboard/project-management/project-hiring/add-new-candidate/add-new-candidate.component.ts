import { Component, OnInit, Inject } from '@angular/core';
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

@Component({
  selector: 'app-add-new-candidate',
  templateUrl: './add-new-candidate.component.html',
  styleUrls: ['./add-new-candidate.component.scss']
})
export class AddNewCandidateComponent implements OnInit {
  addNewCandidateForm: FormGroup;

  professionList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
  countryList$: Observable<IDropDownModel[]>;
  provinceList$: Observable<IDropDownModel[]>;
  districtList$: Observable<IDropDownModel[]>;
  accountStatusList$: Observable<IDropDownModel[]>;
  genderList$: Observable<IDropDownModel[]>;
  gradeList$: Observable<IDropDownModel[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    public dialogRef: MatDialogRef<AddNewCandidateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private commonLoader: CommonLoaderService,
    private routeActive: ActivatedRoute,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private loader: CommonLoaderService
  ) {
    this.addNewCandidateForm = this.fb.group({
      FirstName: ['', [Validators.required]],
      LastName: ['', [Validators.required]],
      Email: ['', [Validators.required, Validators.email]],
      PhoneNumber: ['', [Validators.required, Validators.maxLength(14)]],
      AccountStatus: ['', [Validators.required]],
      Gender: ['', [Validators.required]],
      Country: ['', [Validators.required]],
      Province: ['', [Validators.required]],
      District: ['', [Validators.required]],
      Profession: [, [Validators.required]],
      Grade: ['', [Validators.required]],
      Office: ['', [Validators.required]],
      DateOfBirth: ['', [Validators.required]],
      ExperienceInYear: ['', [Validators.required]],
      ExperienceInMonth: ['', [Validators.required]]
    });

    this.accountStatusList$ = of([
      { name: 'Active', value: 1 },
      { name: 'Nonactive', value: 2 },

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
      this.getAllProfessionList()])
    .pipe(takeUntil(this.destroyed$))
    .subscribe(result => {
      this.subscribeOfficeList(result[0]);
      this.subscribeCountryList(result[1]);
      this.subscribeGradeList(result[2]);
      this.subscribeProfessionList(result[3]);
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
  getAllJobGradeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetJobGradeList();
  }

  getAllProfessionList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetProfessionList();
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
                  value: element.DistrictId,
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
   AddNewCandidate(data: ICandidateDetailModel) {
    // this.hiringRequestService.AddNewCandidateDetail(data).subscribe(
    //   (response: IResponseData) => {
    //     if (response.statusCode === 200) {
    //       this.toastr.success('New request is created successfully');
    //     } else {
    //       this.toastr.error(response.message);
    //     }
    //     this.onCancelPopup();
    //   },
    //   error => {
    //     this.toastr.error('Someting went wrong. Please try again');
    //   }
    // );
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

  }
}
