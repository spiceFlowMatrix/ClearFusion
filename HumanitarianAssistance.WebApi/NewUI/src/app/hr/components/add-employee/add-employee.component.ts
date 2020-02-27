import { StaticUtilities } from 'src/app/shared/static-utilities';
import { IEmployeePensionListModel } from './../../models/employee-detail.model';
import { AddEmployeeService } from './../../services/add-employee.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { Observable, of, forkJoin, ReplaySubject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { IEmployeeAllDetails } from '../../models/employee-detail.model';
import { MatDialog } from '@angular/material';
import { AddOpeningPensionComponent } from './add-opening-pension/add-opening-pension.component';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.scss']
})
export class AddEmployeeComponent implements OnInit {
  @ViewChild('employeeDetailFormButton') employeeDetailFormButton: ElementRef;
  @ViewChild('employeeProfessionalDetailFormButton')
  employeeProfessionalDetailFormButton: ElementRef;
  employeeId = 0;
  employeeDetailForm: FormGroup;
  employeeProfessionalDetailForm: FormGroup;
  genderList$: Observable<IDropDownModel[]>;
  maritalStatusList$: Observable<IDropDownModel[]>;
  professionList$: Observable<IDropDownModel[]>;
  countryList$: Observable<IDropDownModel[]>;
  provinceList$: Observable<IDropDownModel[]>;
  districtList$: Observable<IDropDownModel[]>;
  gradeList$: Observable<IDropDownModel[]>;
  employeeTypeList$: Observable<IDropDownModel[]>;
  qualificationList$: Observable<IDropDownModel[]>;
  currencyList$: Observable<IDropDownModel[]>;
  designationList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
  departmentList$: Observable<IDropDownModel[]>;
  contractTypeList$: Observable<IDropDownModel[]>;
  attendanceGroupList$: Observable<IDropDownModel[]>;
  pensionListDisplay: any[] = [];
  pensionList: IEmployeePensionListModel[] = [];
  employeeAllDetails: IEmployeeAllDetails;
  IsPensionDateSet = false;
  IsEditing = false;
  IsFormSubmitted = false;
  setPensionDate: Date;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    private fb: FormBuilder,
    public dialog: MatDialog,
    private commonLoader: CommonLoaderService,
    private employeeService: AddEmployeeService,
    private toastr: ToastrService,
    private routeActive: ActivatedRoute,
    private router: Router
  ) {
    this.employeeDetailForm = this.fb.group(
      {
        EmployeeId: [null],
        FullName: ['', [Validators.required]],
        FatherName: ['', [Validators.required]],
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
      },
      {
        validator: MustMatch('Password', 'ConfirmPassword')
      }
    );
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

    this.routeActive.queryParams.subscribe(params => {
      this.employeeId = +params['empId'];
    });
  }

  ngOnInit() {
    forkJoin([
      this.getAllCountryList(),
      this.getAllJobGradeList(),
      this.getAllProfessionList(),
      this.getAllQualificationList(),
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
        this.subscribeQualificationList(result[3]);
        this.subscribeEmployeeTypeList(result[4]);
        this.subscribeOfficeList(result[5]);
        this.subscribeDesignationList(result[6]);
        this.subscribeCurrencyList(result[7]);
        this.subscribeContractTypeList(result[8]);
        this.subscribeAttendanceGroupList(result[9]);
      });
    this.employeeAllDetails = {
      EmployeeBasicDetail: {},
      EmployeeProfessionalDetails: {},
      EmployeePensionDetail: {}
    };
    if (this.employeeId > 0) {
      this.IsEditing = true;
      this.employeeDetailForm.get('Password').clearValidators();
      this.employeeDetailForm.get('ConfirmPassword').clearValidators();
      this.employeeDetailForm.get('Password').updateValueAndValidity();
      this.employeeDetailForm.get('ConfirmPassword').updateValueAndValidity();

      this.getEmployeeDetailsByEmployeeId();
      this.getEmployeeProfessionalDetailsByEmployeeId();
      this.getAllPensionList();
    }
  }

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
  getAllQualificationList() {
    this.commonLoader.showLoader();
    return this.employeeService.GetQualificationList();
  }
  subscribeQualificationList(response: any) {
    this.commonLoader.hideLoader();
    this.qualificationList$ = of(
      response.data.QualificationDetailsList.map(y => {
        return {
          value: y.QualificationId,
          name: y.QualificationName
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
                  value: element.District,
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

  // #region "Add Pension"
  addPension(): void {
    /** Open Education dialog box*/
    const dialogRef = this.dialog.open(AddOpeningPensionComponent, {
      width: '400px',
      data: { pensionId: this.pensionList.length + 1 }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onPensionDetailListRefresh.subscribe(result => {
      if (result !== undefined) {
        this.pensionList.push(result);
        let currency;
        this.currencyList$.subscribe(res => {
          currency = res.find(x => x.value === result.Currency).name;
        });
        this.pensionListDisplay.push({
          Id: result.Id,
          Currency: currency,
          Amount: result.Amount
        });
      }
    });
    dialogRef.afterClosed().subscribe(() => {});
  }

  deletePensionItem(data: any) {
    const index = this.pensionList.findIndex(x => x.Id === data.Id);
    this.pensionList.splice(index, 1);
    this.pensionListDisplay.splice(index, 1);
  }
  editPensionItem(data: any) {
    const index = this.pensionList.findIndex(x => x.Id === data.Id);
    const model = {
      Id: this.pensionList[index].Id,
      PensionId: this.pensionList[index].PensionId,
      Currency: this.pensionList[index].Currency,
      Amount: this.pensionList[index].Amount
    };
    /** Open Education dialog box*/
    const dialogRef = this.dialog.open(AddOpeningPensionComponent, {
      width: '400px',
      data: { item: model }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onUpdatePensionDetailListRefresh.subscribe(
      result => {
        if (result !== undefined) {
          const pensionlist = this.pensionList.find(x => x.Id === result.Id);
          pensionlist.Currency = result.Currency;
          pensionlist.Amount = result.Amount;
          let currency;
          this.currencyList$.subscribe(res => {
            currency = res.find(x => x.value === result.Currency).name;
          });
          const pensionlistDisplay = this.pensionListDisplay.find(
            x => x.Id === result.Id
          );
          pensionlistDisplay.Amount = result.Amount;
          pensionlistDisplay.Currency = currency;
        }
      }
    );
    dialogRef.afterClosed().subscribe(() => {});
  }

  //#endregion
  checkExchangeRateVerified(exchangeRateDate: any) {
    const checkExchangeRateModel = {
      ExchangeRateDate: StaticUtilities.getLocalDate(exchangeRateDate)
    };
    this.employeeService
      .CheckExchangeRatesVerified(checkExchangeRateModel)
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (data.ResponseData) {
              this.IsPensionDateSet = true;
              this.employeeAllDetails.EmployeePensionDetail.PensionDate = StaticUtilities.getLocalDate(
                exchangeRateDate
              );
              this.toastr.success('Date is verified');
            } else {
              this.toastr.warning(
                'No Exchange Rate set/verified for this date'
              );
            }
          } else {
            this.toastr.error(data.Message);
          }
        },
        () => {}
      );
  }
  onFormSubmit() {
    this.IsFormSubmitted = true;
    this.employeeDetailFormButton.nativeElement.click();
    this.employeeProfessionalDetailFormButton.nativeElement.click();
    if (this.employeeId > 0) {
      this.EditEmployeeDetails();
    } else {
      this.AddEmployeeDetails();
    }
  }

  AddEmployeeDetails() {
    if (
      this.employeeDetailForm.valid &&
      this.employeeProfessionalDetailForm.valid &&
      this.pensionList.length !== 0
    ) {
      this.employeeDetailForm.controls['EmployeeId'].setValue(this.employeeId);
      this.employeeAllDetails.EmployeeBasicDetail = this.employeeDetailForm.value;
      this.employeeProfessionalDetailForm.controls['HiredOn'].setValue(StaticUtilities.getLocalDate
        (this.employeeProfessionalDetailForm.controls['HiredOn'].value));
      this.employeeAllDetails.EmployeeProfessionalDetails = this.employeeProfessionalDetailForm.value;
      this.employeeAllDetails.EmployeePensionDetail.PensionList = this.pensionList;
      this.employeeService
        .AddNewEmployeeDetails(this.employeeAllDetails)
        .subscribe(
          x => {
            if (x.StatusCode === 200) {
              this.toastr.success('Employee Successfully Added');
              this.employeeId = x.data.EmployeeDetailModel.EmployeeID;
              this.routeBackToListing();
            } else {
              this.toastr.warning(x.Message);
            }
          },
          error => {
            this.toastr.warning(error);
          }
        );
    } else {
      if (this.employeeDetailForm.invalid) {
        this.toastr.warning('Employee Genreal Form Is Not Valid');
      }
      if (this.employeeProfessionalDetailForm.invalid) {
        this.toastr.warning('Employee Professional Detail Form Is Not Valid');
      }
      if (this.pensionList.length === 0) {
        this.toastr.warning('Pension List Is Empty');
      }
    }
  }

  EditEmployeeDetails() {
    if (
      this.employeeDetailForm.valid &&
      this.employeeProfessionalDetailForm.valid &&
      this.pensionList.length !== 0
    ) {
      if (this.IsPensionDateSet === false) {
        this.employeeAllDetails.EmployeePensionDetail.PensionDate = this.setPensionDate;
      }
      this.employeeDetailForm.controls['EmployeeId'].setValue(this.employeeId);
      this.employeeAllDetails.EmployeeBasicDetail = this.employeeDetailForm.value;
      this.employeeProfessionalDetailForm.controls['HiredOn'].setValue(StaticUtilities.getLocalDate
        (this.employeeProfessionalDetailForm.controls['HiredOn'].value));
      this.employeeAllDetails.EmployeeProfessionalDetails = this.employeeProfessionalDetailForm.value;
      this.employeeAllDetails.EmployeePensionDetail.PensionList = this.pensionList;
      this.employeeService
        .EditEmployeeDetails(this.employeeAllDetails)
        .subscribe(
          x => {
            if (x.StatusCode === 200) {
              this.toastr.success('Employee Successfully Updated');
              this.routeBackToListing();
            } else {
              this.toastr.warning(x.Message);
            }
          },
          error => {
            this.toastr.warning(error);
          }
        );
    } else {
      if (this.employeeDetailForm.invalid) {
        this.toastr.warning('Employee Genreal Form Is Not Valid');
      }
      if (this.employeeProfessionalDetailForm.invalid) {
        this.toastr.warning('Employee Professional Detail Form Is Not Valid');
      }
      if (this.pensionList.length === 0) {
        this.toastr.warning('Pension List Is Empty');
      }
    }
  }

  getEmployeeDetailsByEmployeeId() {
    this.employeeService
      .GetEmployeeDetailByEmployeeId(this.employeeId)
      .subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.onChangeCountry(x.data.EmployeeDetailList[0].CountryId);
            this.onChangeProvince(x.data.EmployeeDetailList[0].ProvinceId);
            this.employeeDetailForm.patchValue({
              FullName: x.data.EmployeeDetailList[0].EmployeeName,
              FatherName: x.data.EmployeeDetailList[0].FatherName,
              Email: x.data.EmployeeDetailList[0].Email,
              PhoneNo: x.data.EmployeeDetailList[0].Phone,
              Gender: x.data.EmployeeDetailList[0].SexId,
              DateOfBirth: StaticUtilities.setLocalDate(
                new Date(x.data.EmployeeDetailList[0].DateOfBirth)
              ),
              MaritalStatus: x.data.EmployeeDetailList[0].MaritalStatus,
              Country: x.data.EmployeeDetailList[0].CountryId,
              Province: x.data.EmployeeDetailList[0].ProvinceId,
              District: x.data.EmployeeDetailList[0].District,
              BirthPlace: x.data.EmployeeDetailList[0].BirthPlace,
              TinNumber: x.data.EmployeeDetailList[0].TinNumber,
              PassportNumber: x.data.EmployeeDetailList[0].PassportNo,
              University: x.data.EmployeeDetailList[0].University,
              Profession: x.data.EmployeeDetailList[0].ProfessionId,
              Qualification: x.data.EmployeeDetailList[0].HigherQualificationId,
              ExperienceYear: x.data.EmployeeDetailList[0].ExperienceYear,
              ExperienceMonth: x.data.EmployeeDetailList[0].ExperienceMonth,
              IssuePlace: x.data.EmployeeDetailList[0].IssuePlace,
              ReferBy: x.data.EmployeeDetailList[0].ReferBy,
              PreviousWork: x.data.EmployeeDetailList[0].PreviousWork,
              CurrentAddress: x.data.EmployeeDetailList[0].CurrentAddress,
              PermanentAddress: x.data.EmployeeDetailList[0].PermanentAddress
            });
            this.employeeProfessionalDetailForm.patchValue({
              JobGrade: x.data.EmployeeDetailList[0].GradeId
            });
          } else {
            this.toastr.warning(x.Message);
          }
        },
        error => {
          this.toastr.warning(error);
        }
      );
  }
  getEmployeeProfessionalDetailsByEmployeeId() {
    this.employeeService
      .GetEmployeeProfessionalDetailByEmployeeId(this.employeeId)
      .subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.onChangeOffice(x.data.EmployeeProfessionalList[0].OfficeId);
            this.employeeProfessionalDetailForm.patchValue({
              EmployeeType: x.data.EmployeeProfessionalList[0].EmployeeTypeId,
              Office: x.data.EmployeeProfessionalList[0].OfficeId,
              Department: x.data.EmployeeProfessionalList[0].DepartmentId,
              Designation: x.data.EmployeeProfessionalList[0].DesignationId,
              EmployeeCotractType:
                x.data.EmployeeProfessionalList[0].EmployeeContractTypeId,
              HiredOn: StaticUtilities.setLocalDate(
                x.data.EmployeeProfessionalList[0].HiredOn
              ),
              AttendanceGroup:
                x.data.EmployeeProfessionalList[0].AttendanceGroupId,
              DutyStation: x.data.EmployeeProfessionalList[0].DutyStation,
              TrainingAndBenefits:
                x.data.EmployeeProfessionalList[0].TrainingBenefits,
              JobDescription: x.data.EmployeeProfessionalList[0].JobDescription
            });
          } else {
            this.toastr.warning(x.Message);
          }
        },
        error => {
          this.toastr.warning(error);
        }
      );
  }
  getAllPensionList() {
    this.employeeService.GetAllPensionList(this.employeeId).subscribe(
      x => {
        if (x.StatusCode === 200 && x.ResponseData.length > 0) {
          let id = 1;
          this.setPensionDate = StaticUtilities.setLocalDate(
            x.ResponseData[0].Date
          );
          x.ResponseData.forEach(element => {
            this.pensionListDisplay.push({
              Id: id,
              PensionId: element.PensionId,
              Currency: element.CurrencyName,
              Amount: element.Amount
            });
            this.pensionList.push({
              Id: id,
              PensionId: element.PensionId,
              Currency: element.CurrencyId,
              Amount: element.Amount
            });
            id += 1;
          });
        } else {
          this.toastr.warning(x.Message);
        }
      },
      error => {
        this.toastr.warning(error);
      }
    );
  }

  onTabChange(event: any) {
    if (event.index === 0 && this.IsFormSubmitted) {
      this.employeeDetailFormButton.nativeElement.click();
    } else if (event.index === 1 && this.IsFormSubmitted) {
      this.employeeProfessionalDetailFormButton.nativeElement.click();
    }
  }

  routeBackToListing() {
    if (this.employeeId > 0) {
    this.router.navigate(['/hr/employee/' + this.employeeId]);
    } else {
      this.router.navigate(['/hr/employees']);
    }
  }
}

// custom validator to check that two fields match GetEmployeeOpeningPensionDetail
export function MustMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];

    if (matchingControl.errors && !matchingControl.errors.mustMatch) {
      // return if another validator has already found an error on the matchingControl
      return;
    }

    // set error on matchingControl if validation fails
    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({ mustMatch: true });
    } else {
      matchingControl.setErrors(null);
    }
  };
}
