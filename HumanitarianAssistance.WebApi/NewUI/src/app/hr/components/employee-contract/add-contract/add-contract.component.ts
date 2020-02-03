import { EmployeeContractService } from './../../../services/employee-contract.service';
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable, of, forkJoin, ReplaySubject } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { takeUntil } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-contract',
  templateUrl: './add-contract.component.html',
  styleUrls: ['./add-contract.component.scss']
})
export class AddContractComponent implements OnInit {
  @Output() isDisplayContractForm = new EventEmitter<any>();
  isFormSubmitted = false;
  employeeId: number;
  contractDetailForm: FormGroup;
  testDropDownList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
  countryList$: Observable<IDropDownModel[]>;
  provinceList$: Observable<IDropDownModel[]>;
  designationList$: Observable<IDropDownModel[]>;
  jobGradeList$: Observable<IDropDownModel[]>;
  projectList$: Observable<IDropDownModel[]>;
  budgetLineList$: Observable<IDropDownModel[]>;
  onAddContractListRefresh = new EventEmitter();
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    private fb: FormBuilder,
    private routeActive: ActivatedRoute,
    private commonLoader: CommonLoaderService,
    private contractService: EmployeeContractService,
    private toastr: ToastrService
  ) {
    this.contractDetailForm = this.fb.group({
      EmployeeId: [''],
      FirstName: ['', [Validators.required]],
      LastName: [''],
      EmployeeCode: ['', [Validators.required]],
      Designation: ['', [Validators.required]],
      ContractStartDate: ['', [Validators.required]],
      ContractEndDate: ['', [Validators.required]],
      DurationOfContract: ['', [Validators.required]],
      Salary: ['', [Validators.required]],
      Grade: ['', [Validators.required]],
      DutyStation: ['', [Validators.required]],
      Country: ['', [Validators.required]],
      Province: ['', [Validators.required]],
      Project: ['', [Validators.required]],
      Job: ['', [Validators.required]],
      BudgetLine: ['', [Validators.required]],
      WorkTime: ['', [Validators.required]],
      WorkDayHours: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.routeActive.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
    forkJoin([
      this.getAllOfficeList(),
      this.getAllCountryList(),
      this.getDesignationList(),
      this.getJobGradeList(),
      this.getProjectList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeOfficeList(result[0]);
        this.subscribeCountryList(result[1]);
        this.subscribeDesignationList(result[2]);
        this.subscribeJobGradeList(result[3]);
        this.subscribeProjectList(result[4]);
      });
    this.getEmployeeDetails();
  }
  //#region "Get all office List"
  getAllOfficeList() {
    this.commonLoader.showLoader();
    return this.contractService.GetOfficeList();
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
  //#region "Get all country List"
  getAllCountryList() {
    this.commonLoader.showLoader();
    return this.contractService.GetCountryList();
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
  //#region "Get all designation List"
  getDesignationList() {
    this.commonLoader.showLoader();
    return this.contractService.getDesignationList();
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
  //#region "Get all job grade list"
  getJobGradeList() {
    this.commonLoader.showLoader();
    return this.contractService.GetJobGradeList();
  }
  subscribeJobGradeList(response: any) {
    this.commonLoader.hideLoader();
    this.jobGradeList$ = of(
      response.data.JobGradeList.map(y => {
        return {
          value: y.GradeId,
          name: y.GradeName
        };
      })
    );
  }
  //#endregion
  //#region "Get all job grade list"
  getProjectList() {
    this.commonLoader.showLoader();
    return this.contractService.getAllProjectList();
  }
  subscribeProjectList(response: any) {
    this.commonLoader.hideLoader();
    this.projectList$ = of(
      response.data.ProjectDetailModel.map(y => {
        return {
          value: y.ProjectId,
          name: y.ProjectCode + '-' + y.ProjectName
        };
      })
    );
  }
  //#endregion
  //#region "Get all job grade list"
  getEmployeeDetails() {
    this.contractService.getEmployeeDetail(this.employeeId).subscribe(
      x => {
        if (x.EmployeeDetail) {
          this.onChangeCountry(x.EmployeeDetail.Country);
          this.contractDetailForm.patchValue({
            FirstName: x.EmployeeDetail.FirstName,
            LastName: x.EmployeeDetail.LastName,
            EmployeeCode: x.EmployeeDetail.EmployeeCode,
            Designation: x.EmployeeDetail.Designation,
            DutyStation: x.EmployeeDetail.DutyStation,
            Country: x.EmployeeDetail.Country,
            Province: x.EmployeeDetail.Province
          });
        }
      },
      error => {
        this.toastr.warning(error);
      }
    );
  }
  //#endregion

  //#region onChangeCountry
  onChangeCountry(CountryId: number) {
    this.contractService
      .getAllProvinceListByCountryId([CountryId])
      .subscribe(x => {
        this.provinceList$ = of(
          x.data.ProvinceDetailsList.map(element => {
            return {
              value: element.ProvinceId,
              name: element.ProvinceName
            };
          })
        );
      });
  }
  //#endregion
  //#region onChangeCountry ProjectBudgetLineDetailList
  onChangeProject(ProjectId: number) {
    this.contractService.GetBudgetLineList(ProjectId).subscribe(x => {
      this.budgetLineList$ = of(
        x.data.ProjectBudgetLineDetailList.map(element => {
          return {
            value: element.BudgetLineId,
            name: element.BudgetCodeName
          };
        })
      );
    });
  }
  //#endregion
  onFormSubmit(data: any) {
    if (this.contractDetailForm.valid) {
      data.EmployeeId = this.employeeId;
      this.contractService.addEmployeeContractDetail(data).subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.toastr.success('Contract Successfully Added');
            this.hideContractForm();
          } else {
            this.toastr.warning(x.Message);
          }
        },
        error => {
          this.toastr.warning(error);
          this.isFormSubmitted = false;
        }
      );
    }
  }
  hideContractForm() {
    this.isDisplayContractForm.emit();
  }
}
