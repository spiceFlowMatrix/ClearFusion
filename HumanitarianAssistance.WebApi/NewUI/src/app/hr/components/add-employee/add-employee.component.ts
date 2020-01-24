import { AddEmployeeService } from './../../services/add-employee.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { Observable, of, forkJoin, ReplaySubject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { PurchaseService } from 'src/app/store/services/purchase.service';
import { Month } from 'src/app/shared/enum';
import { IEmployeeAllDetails } from '../../models/employee-detail.model';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.scss']
})
export class AddEmployeeComponent implements OnInit {
  employeeDetailForm: FormGroup;
  employeeProfessionalDetailForm: FormGroup;
  employeePensionDetailForm: FormGroup;
  genderList$: Observable<IDropDownModel[]>;
  maritalStatusList$: Observable<IDropDownModel[]>;
  previousYearsList$: Observable<IDropDownModel[]>;
  monthsList$: Observable<IDropDownModel[]>;
  professionList$: Observable<IDropDownModel[]>;
  countryList$: Observable<IDropDownModel[]>;
  provinceList$: Observable<IDropDownModel[]>;
  districtList$: Observable<IDropDownModel[]>;
  gradeList$: Observable<IDropDownModel[]>;
  employeeTypeList$: Observable<IDropDownModel[]>;
  educationDegreeList$: Observable<IDropDownModel[]>;
  currencyList$: Observable<IDropDownModel[]>;
  designationList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
  departmentList$: Observable<IDropDownModel[]>;
  contractTypeList$: Observable<IDropDownModel[]>;
  attendanceGroupList$: Observable<IDropDownModel[]>;
  employeeAllDetails: IEmployeeAllDetails;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    private fb: FormBuilder,
    private commonLoader: CommonLoaderService,
    private purchaseService: PurchaseService,
    private employeeService: AddEmployeeService
  ) {
    this.employeeDetailForm = this.fb.group({
      FirstName: ['', [Validators.required]],
      LastName: ['', [Validators.required]],
      Email: ['', [Validators.required, Validators.email]],
      PhoneNo: [
        null,
        [
          Validators.required,
          Validators.pattern(/^-?(0|[1-9]\d*)?$/),
          Validators.minLength(10),
          Validators.maxLength(14)
        ]
      ],
      Password: ['', [Validators.required]],
      ConfirmPassword: ['', [Validators.required]],
      Gender: ['', [Validators.required]],
      DateOfBirth: ['', [Validators.required]],
      MaritalStatus: ['', [Validators.required]],
      Country: ['', [Validators.required]],
      Province: ['', [Validators.required]],
      District: ['', [Validators.required]],
      BirthPlace: ['', [Validators.required]],
      TinNumber: ['', [Validators.required]],
      PassportNumber: ['', [Validators.required]],
      University: ['', [Validators.required]],
      Profession: ['', [Validators.required]],
      Qualification: ['', [Validators.required]],
      ExperienceYear: ['', [Validators.required]],
      ExperienceMonth: ['', [Validators.required]],
      IssuePlace: ['', [Validators.required]],
      ReferBy: ['', [Validators.required]],
      PreviousWork: ['', [Validators.required]],
      CurrentAddress: ['', [Validators.required]],
      PermanentAddress: ['', [Validators.required]]
    });
    this.employeeProfessionalDetailForm = this.fb.group({
      EmployeeType: ['', [Validators.required]],
      JobGrade: ['', [Validators.required]],
      Office: ['', [Validators.required]],
      Department: ['', [Validators.required]],
      Designation: ['', [Validators.required]],
      EmployeeCotractType: ['', [Validators.required]],
      HiredOn: ['', [Validators.required]],
      AttendanceGroup: ['', [Validators.required]],
      DutyStation: ['', [Validators.required]],
      TrainingAndBenefits: ['', [Validators.required]],
      JobDescription: ['', [Validators.required]]
    });
    this.employeePensionDetailForm = this.fb.group({
      Currency: ['', [Validators.required]],
      Amount: ['', [Validators.required]]
    });
    this.genderList$ = of([
      { name: 'Male', value: 1 },
      { name: 'Female', value: 2 },
      { name: 'Other', value: 3 }
    ] as IDropDownModel[]);
    this.maritalStatusList$ = of([
      { name: 'Single', value: 1 },
      { name: 'Married', value: 2 },
      { name: 'Divorced', value: 3 },
      { name: 'Widow', value: 3 }
    ] as IDropDownModel[]);
  }

  ngOnInit() {
    forkJoin([
      this.getAllCountryList(),
      this.getAllJobGradeList(),
      this.getAllProfessionList(),
      this.getAllEducationDegreeList(),
      this.getEmployeeTypeList(),
      this.getAllOfficeList(),
      this.getDesignationList(),
      this.getCurrencyList(),
      this.getContractTypeList(),
      this.getAttendanceGroupList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeCountryList(result[0]);
        this.subscribeGradeList(result[1]);
        this.subscribeProfessionList(result[2]);
        this.subscribeEducationDegreeList(result[3]);
        this.subscribeEmployeeTypeList(result[4]);
        this.subscribeOfficeList(result[5]);
        this.subscribeDesignationList(result[6]);
        this.subscribeCurrencyList(result[7]);
        this.subscribeContractTypeList(result[8]);
        this.subscribeAttendanceGroupList(result[9]);
      });
    this.getAllMonthList();
    this.getPreviousYearsList();
  }

  //#region "Get all month list for ExperienceInMonth dropdown"
  getAllMonthList() {
    const monthDropDown: IDropDownModel[] = [];
    for (let i = Month['January']; i <= Month['December']; i++) {
      monthDropDown.push({ name: Month[i], value: i });
    }
    this.monthsList$ = of(monthDropDown);
  }
  //#endregion
  //#region "Get all previous years list for ExperienceInYears dropdown"
  getPreviousYearsList() {
    this.previousYearsList$ = this.purchaseService.getPreviousYearsList(40);
  }
  //#endregion

  //#region "Get all countries list for country dropdown"
  getAllCountryList() {
    this.commonLoader.showLoader();
    return this.employeeService.GetCountryList();
  }
  subscribeCountryList(response: any) {
    this.commonLoader.hideLoader();
    this.countryList$ = of(
      response.data.CountryDetailsList.map(y => {
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
    return this.employeeService.GetJobGradeList();
  }
  subscribeGradeList(response: any) {
    this.commonLoader.hideLoader();
    this.gradeList$ = of(
      response.data.JobGradeList.map(y => {
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
    return this.employeeService.GetProfessionList();
  }
  subscribeProfessionList(response: any) {
    this.commonLoader.hideLoader();
    this.professionList$ = of(
      response.data.ProfessionList.map(y => {
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
    return this.employeeService.GetEducationDegreeList();
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
  //#region "Get all get employee Type dropdown"
  getEmployeeTypeList() {
    this.commonLoader.showLoader();
    return this.employeeService.GetEmployeeTypeList();
  }
  subscribeEmployeeTypeList(response: any) {
    this.commonLoader.hideLoader();
    this.employeeTypeList$ = of(
      response.data.EmployeeTypeList.map(y => {
        return {
          value: y.EmployeeTypeId,
          name: y.EmployeeTypeName
        };
      })
    );
  }
  //#endregion
  //#region "Get all office List"
  getAllOfficeList() {
    this.commonLoader.showLoader();
    return this.employeeService.GetOfficeList();
  }
  subscribeOfficeList(response: any) {
    this.commonLoader.hideLoader();
    this.officeList$ = of(
      response.data.OfficeDetailsList.map(y => {
        return {
          value: y.OfficeId,
          name: y.OfficeName
        };
      })
    );
  }
  //#endregion
  //#region "Get all designation List"
  getDesignationList() {
    this.commonLoader.showLoader();
    return this.employeeService.getDesignationList();
  }
  subscribeDesignationList(response: any) {
    this.commonLoader.hideLoader();
    this.designationList$ = of(
      response.ResponseData.map(y => {
        return {
          value: y.DesignationId,
          name: y.Designation
        };
      })
    );
  }
  //#endregion
  //#region "Get all currency list"
  getDepartmentList(OfficeId: number) {
    this.employeeService.GetDepartmentList(OfficeId).subscribe(x => {
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
  //#region "Get all currency list"
  getCurrencyList() {
    this.commonLoader.showLoader();
    return this.employeeService.GetCurrencyList();
  }
  subscribeCurrencyList(response: any) {
    this.commonLoader.hideLoader();
    this.currencyList$ = of(
      response.data.CurrencyList.map(y => {
        return {
          value: y.CurrencyId,
          name: y.CurrencyName
        };
      })
    );
  }
  //#endregion
  //#region "Get all Contract Type List"
  getContractTypeList() {
    this.commonLoader.showLoader();
    return this.employeeService.getContractTypeList();
  }
  subscribeContractTypeList(response: any) {
    this.commonLoader.hideLoader();
    this.contractTypeList$ = of(
      response.data.EmployeeContractTypeList.map(y => {
        return {
          value: y.EmployeeContractTypeId,
          name: y.EmployeeContractTypeName
        };
      })
    );
  }
  //#endregion
  //#region "Get all Contract Type List"
  getAttendanceGroupList() {
    this.commonLoader.showLoader();
    return this.employeeService.GetAllAttendanceGroupList();
  }
  subscribeAttendanceGroupList(response: any) {
    this.commonLoader.hideLoader();
    this.attendanceGroupList$ = of(
      response.data.AttendanceGroupMasterList.map(y => {
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
    this.employeeService.getAllProvinceListByCountryId([CountryId]).subscribe(
      response => {
        this.commonLoader.showLoader();
        if (response.StatusCode === 200 && response.data !== null) {
          this.provinceList$ = of(
            response.data.ProvinceDetailsList.map(element => {
              return {
                value: element.ProvinceId,
                name: element.ProvinceName
              } as IDropDownModel;
            })
          );
        }
        this.commonLoader.hideLoader();
      },
      () => {
        this.commonLoader.hideLoader();
      }
    );
  }
  //#endregion
  //#region "Get all district list on selection of province dropdown"
  getAllDistrictList(ProvinceId: any) {
    this.employeeService
      .GetAllDistrictvalueByProvinceId([ProvinceId])
      .subscribe(
        response => {
          this.commonLoader.showLoader();
          if (response.StatusCode === 200 && response.data !== null) {
            this.districtList$ = of(
              response.data.Districtlist.map(element => {
                return {
                  value: element.DistrictID,
                  name: element.District
                } as IDropDownModel;
              })
            );
          }
          this.commonLoader.hideLoader();
        },
        () => {
          this.commonLoader.hideLoader();
        }
      );
  }
  //#endregion

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
  onChangeOffice(e) {
    this.departmentList$ = null;
    this.getDepartmentList(e);
  }
  onFormSubmit(data: any) {
    if (
      this.employeeDetailForm.valid &&
      this.employeeProfessionalDetailForm.valid &&
      this.employeePensionDetailForm.valid
    ) {
      console.log('Ok');
    }
  }
}
