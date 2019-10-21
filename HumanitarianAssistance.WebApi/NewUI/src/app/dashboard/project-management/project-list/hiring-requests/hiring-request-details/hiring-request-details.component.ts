import {
  Component,
  OnInit,
  Input,
  HostListener,
  EventEmitter,
  Output,
  OnChanges
} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ICurrencyList } from 'src/app/dashboard/accounting/gain-loss-report/gain-loss-report.model';
import {
  IBudgetLineModel,
  IOfficeListModel,
  IJobGradeModel,
  IEmployeeListModel,
  IReuestedCandidateDetailModel,
  IProfessionList,
  IitervireCandidateModel,
  IHiringReuestCandidateModel,
  IAttendaneGroupModel,
  IEmployeeContractList,
  IHiringRequestModel,
  IGender,
  IWorkingShift,
  ICountryList,
  IProvinceList,
  IJobTypeList,
} from '../models/hiring-requests-model';
import { MatDialog } from '@angular/material';
import { AddHiringRequestsComponent } from '../add-hiring-requests/add-hiring-requests.component';
import { AddCandidateDaialogComponent } from '../add-candidate-daialog/add-candidate-daialog.component';
import { HiringRequestsService } from '../hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ToastrService } from 'ngx-toastr';
import { EmployeeType, Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ActivatedRoute } from '@angular/router';
import { EditCandidateDetailDialogComponent } from '../edit-candidate-detail-dialog/edit-candidate-detail-dialog.component';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import {
  IProjectRoles,
  IProjectPeople
} from '../../project-details/models/project-people.model';
import { takeUntil } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';
import { ProjectListService } from '../../service/project-list.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { GLOBAL } from 'src/app/shared/global';

@Component({
  selector: 'app-hiring-request-details',
  templateUrl: './hiring-request-details.component.html',
  styleUrls: ['./hiring-request-details.component.scss']
})
export class HiringRequestDetailsComponent implements OnInit, OnChanges {
  //#region "input/output"
  @Input() hiringRequestId: number;
  @Input() currencyList: ICurrencyList[] = [];
  @Input() hiringRequestDetail: any;
  @Input() budgetLineList: IBudgetLineModel[];
  @Input() officeList: IOfficeListModel[];
  @Input() jobGradeList: IJobGradeModel[];
  @Input() professionList: IProfessionList[];
  @Input() genderList: IGender[];
  @Input() countryList: ICountryList[];
  @Input() workingShift: IWorkingShift[];
  @Input() provinceList: IProvinceList[];
  @Input() jobTypeList: IJobTypeList[];
  @Output() UpdatedHRListRefresh = new EventEmitter<any[]>();
  //#endregion

  // subscription destroy
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  // Model:
  hiringRequestModel: IHiringRequestModel;
  employeeList: IEmployeeListModel[] = [];
  attendanceGroupList: IAttendaneGroupModel[] = [];
  candidateList: IReuestedCandidateDetailModel[] = [];
  employeeContractist: IEmployeeContractList[] = [];
  interviewCandidatModel: IitervireCandidateModel;
  filteredEmployeeList: IEmployeeListModel[] = [];
  //#region "variables"

  // variables:
  hiringRequestForm: FormGroup;
  SelctedHiringRequestId: number;
  projectId: number;

  // flag:
  hiringReuestDetailLoader = false;
  addCandidateInterviewLoader = false;
  interviewCompleteCheckFlag = false;
  isShotlistedCandidateFlag = false;
  getCandidateDetailLoader = false;
  isCompletedFlag = false;
  isshortlistedLoaderFlag = false;
  isCompleted = false;
  officeSelectionFlag = false;

  // screen scroll
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;

  // Employeetype from enum
  employeeType = {
    Active: EmployeeType.Active,
    Candidate: EmployeeType.Candidate,
    Terminated: EmployeeType.Terminated
  };
  //#endregion

  // permission
  actualProjectPermissions: IProjectRoles[] = [];
  projectPermissions: IProjectPeople[] = [];
  markCompletePermission = false;

  constructor(
    private fb: FormBuilder,
    public dialog: MatDialog,
    public toastr: ToastrService,
    private globalSharedService: GlobalSharedService,
    private appurl: AppUrlService,
    private routeActive: ActivatedRoute,
    public hiringRequestService: HiringRequestsService,
    public projectListService: ProjectListService
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.initForm();
    this.getActivityPermission();
    this.GetAllEmployeeContractTypelist();
    this.GetAllAttendanceGrouplist();
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
  }
  ngOnChanges() {
    if (
      this.hiringRequestDetail != null &&
      this.hiringRequestDetail !== 0 &&
      this.hiringRequestDetail !== undefined
    ) {
      this.onChanges();
      this.GetSelectedEmployeeDetail(this.hiringRequestDetail.HiringRequestId);
      this.isCompleted = this.hiringRequestDetail.IsCompleted;
    }
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;
    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 170 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region "initForm"
  initForm() {
    this.hiringRequestForm = this.fb.group({
      HiringRequestId: [null],
      JobCategory: ['', Validators.required],
      MinimumEducationLevel: ['', Validators.required],
      Organization: ['', Validators.required],
      ContractType: ['', Validators.required],
      JobStatus: ['', Validators.required],
      Experience: ['', Validators.required],
      Background: ['', Validators.required],
      Position: ['', [Validators.required]],
      SalaryRange: ['', [Validators.required]],
      ProvinceId: [null, Validators.required],
      ContractDuration: [null, Validators.required],
      GenderId: [null, Validators.required],
      CountryId: [null, Validators.required],
      JobType: [null, Validators.required],
      Shift: [null, Validators.required],
      TotalVacancies: [null, Validators.required],
      FilledVacancies: [null, Validators.required],
      OfficeId: [null, Validators.required],
      RequestedBy: [null],
      ProjectId: [null],
      IsCompleted: [null],
      AnouncingDate: [null],
      ClosingDate: [null],
      SpecificDutiesAndResponsblities: [null],
      KnowladgeAndSkillRequired: [null],
      SubmissionGuidlines: [null],
      Description: ['', Validators.required],
      ProfessionId: ['', Validators.required],
      BasicPay: [null, Validators.required],
      CurrencyId: [null, Validators.required],
      BudgetLineId: [null, Validators.required],
      GradeId: [null, Validators.required]
    });
    this.hiringRequestModel = {
      Description: '',
      Position: '',
      Profession: '',
      BudgetLine: '',
      TotalVacancies: '',
      Office: '',
      FilledVacancies: '',
      BasicPay: '',
      jobGrade: '',
      JobCategory: '',
      MinimumEducation: '',
      Organization: '',
      ContractType: '',
      JobStatus: '',
      Experience: '',
      Background: '',
      SalaryRange: '',
      Province: '',
      Country: '',
      ContractDuration: '',
      Gender: '',
      JobType: '',
      Shift: '',
      AnnouncingDate: '',
      ClosingDate: '',
      SpecificDutiesAndResponsiblities: '',
      KnowladgeAndSkillRequired: '',
      SubmissionGuidline: '',
      RequestedBy: '',
      Currency: '',
      IsCompleted: null
    };
  }
  //#endregion

  onChanges() {
    this.hiringRequestForm = this.fb.group({
      HiringRequestId: [this.hiringRequestDetail.HiringRequestId],
      Description: [this.hiringRequestDetail.Description],
      Position: [this.hiringRequestDetail.Position],
      ProfessionId: [this.hiringRequestDetail.ProfessionId],
      BudgetLineId: [this.hiringRequestDetail.BudgetLineId],
      TotalVacancies: [this.hiringRequestDetail.TotalVacancies],
      OfficeId: [this.hiringRequestDetail.OfficeId],
      FilledVacancies: [this.hiringRequestDetail.FilledVacancies],
      BasicPay: [this.hiringRequestDetail.BasicPay],
      GradeId: [this.hiringRequestDetail.GradeId],
      JobCategory: [this.hiringRequestDetail.JobCategory],
      MinimumEducationLevel: [this.hiringRequestDetail.MinimumEducationLevel],
      Organization: [this.hiringRequestDetail.Organization],
      ContractType: [this.hiringRequestDetail.ContractType],
      JobStatus: [this.hiringRequestDetail.JobStatus],
      Experience: [this.hiringRequestDetail.Experience],
      Background: [this.hiringRequestDetail.Background],
      SalaryRange: [this.hiringRequestDetail.SalaryRange],
      ProvinceId: [this.hiringRequestDetail.ProvinceId],
      ContractDuration: [this.hiringRequestDetail.ContractDuration],
      GenderId: [this.hiringRequestDetail.GenderId],
      CountryId: [this.hiringRequestDetail.CountryId],
      JobType: [this.hiringRequestDetail.JobType],
      Shift: [this.hiringRequestDetail.Shift],
      CurrencyId: [this.hiringRequestDetail.CurrencyId],
      AnouncingDate: [this.hiringRequestDetail.AnouncingDate],
      ClosingDate: [this.hiringRequestDetail.ClosingDate],
      SpecificDutiesAndResponsblities: [
        this.hiringRequestDetail.SpecificDutiesAndResponsblities
      ],
      KnowladgeAndSkillRequired: [
        this.hiringRequestDetail.KnowladgeAndSkillRequired
      ],
      SubmissionGuidlines: [this.hiringRequestDetail.SubmissionGuidlines],
      RequestedBy: [this.hiringRequestDetail.RequestedBy],
      ProjectId: [this.hiringRequestDetail.ProjectId],
      IsCompleted: [this.hiringRequestDetail.IsCompleted]
    });

    this.hiringRequestModel = {
      Description: this.hiringRequestForm.value.Description,
      Position: this.hiringRequestForm.value.Position,
      Profession: this.professionList.find(
        x => x.ProfessionId === this.hiringRequestForm.value.ProfessionId
      ).ProfessionName,
      BudgetLine: this.budgetLineList.find(
        x => x.BudgetLineId === this.hiringRequestForm.value.BudgetLineId
      ).BudgetName,
      TotalVacancies: this.hiringRequestForm.value.TotalVacancies,
      Office: this.officeList.find(
        x => x.OfficeId === this.hiringRequestForm.value.OfficeId
      ).OfficeName,
      FilledVacancies: this.hiringRequestForm.value.FilledVacancies,
      BasicPay: this.hiringRequestForm.value.BasicPay,
      jobGrade: this.jobGradeList.find(
        x => x.GradeId === this.hiringRequestForm.value.GradeId
      ).GradeName,
      JobCategory: this.hiringRequestForm.value.JobCategory,
      MinimumEducation: this.hiringRequestForm.value.MinimumEducationLevel,
      Organization: this.hiringRequestForm.value.Organization,
      ContractType: this.hiringRequestForm.value.ContractType,
      JobStatus: this.hiringRequestForm.value.JobStatus,
      Experience: this.hiringRequestForm.value.Experience,
      Background: this.hiringRequestForm.value.Background,
      SalaryRange: this.hiringRequestForm.value.SalaryRange,
      Province: this.provinceList.find(
        x => x.ProvinceId === this.hiringRequestForm.value.ProvinceId
      ).ProvinceName,
      Country: this.countryList.find(
        x => x.CountryId === this.hiringRequestForm.value.CountryId
      ).CountryName,
      ContractDuration: this.hiringRequestForm.value.ContractDuration,
      Gender: this.genderList.find(
        x => x.Id === this.hiringRequestForm.value.GenderId
      ).value,
      JobType: this.jobTypeList.find(x => x.JobTypeId === this.hiringRequestForm.value.JobType).JobTypeName,
      Shift: this.workingShift.find(
        x => x.Id === this.hiringRequestForm.value.Shift
      ).value,
      AnnouncingDate: this.hiringRequestForm.value.AnouncingDate,
      ClosingDate: this.hiringRequestForm.value.ClosingDate,
      SpecificDutiesAndResponsiblities: this.hiringRequestForm.value.SpecificDutiesAndResponsblities,
      KnowladgeAndSkillRequired: this.hiringRequestForm.value.KnowladgeAndSkillRequired,
      SubmissionGuidline: this.hiringRequestForm.value.SubmissionGuidlines,
      Currency: this.currencyList.find(x => x.CurrencyId === this.hiringRequestForm.value.CurrencyId).CurrencyName,
      IsCompleted: this.hiringRequestForm.value.IsCompleted
    };
    this.GetEmployeeListByOfficeId(this.hiringRequestForm.value.OfficeId);
  }

  //#region "Permission"
  getActivityPermission() {
    this.hiringRequestService.hiringPermissionSubject
      .pipe(takeUntil(this.destroyed$))
      .subscribe(data => {
        // get user permission/role
        this.projectPermissions = data;

        // get all permissions/role
        this.actualProjectPermissions = this.projectListService.GetHiringControlRole();

        if (this.projectPermissions.length > 0) {
          // Mark complete permission
          this.markCompletePermission = this.checkMarkCompletePermission();
        }
      });
  }

  checkMarkCompletePermission(): boolean {
    // NOTE: "PLANNING OFFICER" & "MONITORING OFFICER" can mark as complete
    return this.projectPermissions.filter(
      x => x.RoleId === this.actualProjectPermissions[0].Id
    ).length > 0
      ? true
      : false;
  }

  //#region "onAddNewRequestClicked"
  onEditHiringRequestClicked() {
    this.openHiringRequestDialog();
  }
  //#endregion

  //#region "openHiringRequestDialog"
  openHiringRequestDialog(): void {
    if (this.candidateList.length > 0) {
      this.officeSelectionFlag = true;
    }
    // NOTE: It passed the data into the Add Activity Model
    const dialogRef = this.dialog.open(AddHiringRequestsComponent, {
      width: '550px',
      autoFocus: false,
      data: {
        BudgetLineList: this.budgetLineList,
        OfficeList: this.officeList,
        CurrencyList: this.currencyList,
        JobGradeList: this.jobGradeList,
        HiringRequestDetail: this.hiringRequestForm.value,
        ProfessionList: this.professionList,
        ProjectId: this.projectId,
        workingShift: this.workingShift,
        gender: this.genderList,
        countryList: this.countryList,
        provinceList: this.provinceList,
        JobTypeList: this.jobTypeList,
        officeSelectionFlag: this.officeSelectionFlag
      }
    });

    // refresh the list after new request created
    dialogRef.componentInstance.onUpdateHiringRequestListRefresh.subscribe(
      (data: any) => {
        this.UpdatedHRListRefresh.emit(data);
        this.hiringRequestForm = this.fb.group({
          HiringRequestId: [data.HiringRequestId],
          JobCategory: [data.JobCategory],
          MinimumEducationLevel: [data.MinimumEducationLevel],
          Organization: [data.Organization],
          ContractType: [data.ContractType],
          JobStatus: [data.JobStatus],
          Experience: [data.Experience],
          Background: [data.Background],
          Position: [data.Position],
          SalaryRange: [data.SalaryRange],
          ProvinceId: [data.ProvinceId],
          ContractDuration: [data.ContractDuration],
          GenderId: [data.GenderId],
          CountryId: [data.CountryId],
          JobType: [data.JobType],
          CurrencyId: [data.CurrencyId],
          Shift: [data.Shift],
          TotalVacancies: [data.TotalVacancies],
          FilledVacancies: [data.FilledVacancies],
          OfficeId: [data.OfficeId],
          RequestedBy: [data.RequestedBy],
          ProjectId: [data.ProjectId],
          Description: [data.Description],
          ProfessionId: [data.ProfessionId],
          BudgetLineId: [data.BudgetLineId],
          BasicPay: [data.BasicPay],
          GradeId: [data.GradeId],
          AnouncingDate: [data.AnouncingDate],
          ClosingDate: [data.ClosingDate],
          SpecificDutiesAndResponsblities: [
            data.SpecificDutiesAndResponsblities
          ],
          KnowladgeAndSkillRequired: [data.KnowladgeAndSkillRequired],
          SubmissionGuidlines: [data.SubmissionGuidlines]
        });
        this.GetEmployeeListByOfficeId(data.OfficeId);
        this.hiringRequestModel = {
          Description: data.Description,
          Position: data.Position,
          Profession: this.professionList.find(
            x => x.ProfessionId === data.ProfessionId
          ).ProfessionName,
          BudgetLine: this.budgetLineList.find(
            x => x.BudgetLineId === data.BudgetLineId
          ).BudgetName,
          TotalVacancies: data.TotalVacancies,
          Office: this.officeList.find(
            x => x.OfficeId === data.OfficeId
          ).OfficeName,
          FilledVacancies: data.FilledVacancies,
          BasicPay: data.BasicPay,
          jobGrade: this.jobGradeList.find(
            x => x.GradeId === data.GradeId
          ).GradeName,
          JobCategory: data.JobCategory,
          MinimumEducation: data.MinimumEducationLevel,
          Organization: data.Organization,
          ContractType: data.ContractType,
          JobStatus: data.JobStatus,
          Experience: data.Experience,
          Background: data.Background,
          SalaryRange: data.SalaryRange,
          Province: this.provinceList.find(
            x => x.ProvinceId === data.ProvinceId
          ).ProvinceName,
          Country: this.countryList.find(
            x => x.CountryId === data.CountryId
          ).CountryName,
          ContractDuration: data.ContractDuration,
          Gender: this.genderList.find(
            x => x.Id === data.GenderId
          ).value,
          JobType: this.jobTypeList.find(x => x.JobTypeId === data.JobType).JobTypeName,
          Shift: this.workingShift.find(
            x => x.Id === data.Shift
          ).value,
          AnnouncingDate: data.AnouncingDate,
          ClosingDate: data.ClosingDate,
          SpecificDutiesAndResponsiblities: data.SpecificDutiesAndResponsblities,
          KnowladgeAndSkillRequired: data.KnowladgeAndSkillRequired,
          SubmissionGuidline: data.SubmissionGuidlines,
          Currency: this.currencyList.find(x => x.CurrencyId === data.CurrencyId).CurrencyName,
          IsCompleted: data.IsCompleted
        };
      }
    );

    dialogRef.afterClosed().subscribe(result => {
      this.officeSelectionFlag = false;
    });
  }
  //#endregion

  //#region "onAddEmployeeClicked"
  onAddCandidateClicked() {
    this.filteredEmployeeList = [];

    this.filteredEmployeeList = this.employeeList.filter(employee =>
      this.candidateList.every(
        candidate => employee.EmployeeId !== candidate.EmployeeID
      )
    );
    const dialogRef = this.dialog.open(AddCandidateDaialogComponent, {
      width: '420px',
      autoFocus: false,
      data: {
        EmployeeList: this.filteredEmployeeList,
        HiringRequestId: this.hiringRequestId,
        ProjectId: this.projectId
      }
    });
    dialogRef.componentInstance.selectedEmployeId.subscribe((data: any) => {
      this.GetSelectedEmployeeDetail(data);
    });
    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "GetEmployeeDetail"
  GetEmployeeListByOfficeId(OfficeId: number) {
    if (OfficeId == null) {
      // this.toastr.warning('Office Can not be null');
    } else {
      this.hiringRequestService.GetEmployeeListByOfficeId(OfficeId).subscribe(
        (response: IResponseData) => {
          this.employeeList = [];
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach(element => {
              this.employeeList.push({
                EmployeeId: element.EmployeeId,
                EmployeeName: element.EmployeeName
              });
            });
          }
        },
        error => {}
      );
    }
  }
  //#endregion

  //#region "GetSelectedEmployeeDetail"
  GetSelectedEmployeeDetail(data: number) {
    this.getCandidateDetailLoader = true;
    if (data != null) {
      this.candidateList = [];
      const candidateDetail: IReuestedCandidateDetailModel = {
        HiringRequestId: data
      };
      this.hiringRequestService
        .GetRequestedCandidateById(candidateDetail)
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200 && response.data != null) {
              response.data.forEach(element => {
                this.candidateList.push({
                  CandidateId: element.CandidateId,
                  EmployeeID: element.EmployeeID,
                  EmployeeCode: element.EmployeeCode,
                  EmployeeName: element.EmployeeName,
                  EmployeeTypeName: element.EmployeeTypeName,
                  Gender: element.Gender,
                  EmployeeTypeId: element.EmployeeTypeId,
                  IsInterViewed: element.IsInterViewed,
                  IsShortListed: element.IsShortListed,
                  IsSelected: element.IsSelected,
                  IsSelectedFlag: false
                });
              });
            } else {
              this.toastr.error(response.message);
            }
            this.getCandidateDetailLoader = false;
          },
          error => {
            this.toastr.error('Someting went wrong');
            this.getCandidateDetailLoader = false;
          }
        );
    }
  }
  //#endregion
  OnShortListClick(data: IHiringReuestCandidateModel) {
    if (data != null) {
      this.isshortlistedLoaderFlag = true;
      const candidateDetail: IHiringReuestCandidateModel = {
        EmployeeID: data.EmployeeID,
        HiringRequestId: this.hiringRequestForm.get('HiringRequestId').value,
        IsShortListed: !data.IsShortListed
      };

      this.hiringRequestService
        .EditHirinigRequestCandidateDEtail(candidateDetail)
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200) {
              data.IsShortListed = !data.IsShortListed;
            } else {
              this.toastr.error(response.message);
            }
            this.isshortlistedLoaderFlag = false;
          },
          error => {
            this.toastr.error('Someting went wrong');
            this.isshortlistedLoaderFlag = false;
          }
        );
    }
  }
  //#region  "GetAllAttendanceGrouplist"
  GetAllAttendanceGrouplist() {
    this.hiringRequestService.GetAllAttendanceGroupList().subscribe(
      (response: IResponseData) => {
        this.attendanceGroupList = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.attendanceGroupList.push({
              Id: element.Id,
              Name: element.Name,
              Description: element.Description
            });
          });
        }
      },
      error => {
        this.toastr.error('Something went wrong. Please try again...');
      }
    );
  }
  //#endregion
  //#region  "GetAllEmployeeContractTypelist"
  GetAllEmployeeContractTypelist() {
    this.hiringRequestService.GetAllEmloyeeContractList().subscribe(
      (response: IResponseData) => {
        this.employeeContractist = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.employeeContractist.push({
              EmployeeContractTypeId: element.EmployeeContractTypeId,
              EmployeeContractTypeName: element.EmployeeContractTypeName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region  "onSelectedCandidate"
  onSelectedCandidate(data: any) {
    // Note Check for is filled vacancies exceed total vacancies.
    const filledVacancy = this.hiringRequestForm.get('FilledVacancies').value;
    const totalVacancy = this.hiringRequestForm.get('TotalVacancies').value;
    if (filledVacancy <= totalVacancy) {
      if (data != null) {
        if (data.EmployeeTypeId === this.employeeType.Candidate) {
          const dialogRef = this.dialog.open(
            EditCandidateDetailDialogComponent,
            {
              width: '550px',
              autoFocus: false,
              data: {
                HiringRequestDetail: this.hiringRequestDetail,
                AttendanceGroupList: this.attendanceGroupList,
                EmployeeId: data.EmployeeID,
                EmployeeContractist: this.employeeContractist
              }
            }
          );
          dialogRef.componentInstance.employeeTypeDetial.subscribe(
            (obj: any) => {
              if (obj === this.employeeType.Active) {
                // Note: to update selected candidate detail
                this.EditselectedCandidate(data);
              }
            }
          );
          dialogRef.afterClosed().subscribe(result => {});
        } else {
          this.EditselectedCandidate(data);
        }
      }
    } else {
      this.toastr.warning('No vacancies left');
    }
  }
  //#endregion

  //#region "EditselectedCandidate" common function
  EditselectedCandidate(data: any) {
    // note:  enable loader when we click on select candidate
    const obj = this.candidateList.find(x => x.EmployeeID === data.EmployeeID);
    const indexOfCandidate = this.candidateList.indexOf(obj);
    this.candidateList[indexOfCandidate].IsSelectedFlag = true;

    const candidateDetail: any = {
      EmployeeId: data.EmployeeID,
      HiringRequestId: this.hiringRequestForm.get('HiringRequestId').value,
      IsSelected: !data.IsSelected,
      BudgetLineId: this.hiringRequestForm.get('BudgetLineId').value,
      ProjectId: this.projectId
    };

    this.hiringRequestService
      .EditSelectedCandidateDEtail(candidateDetail)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            data.IsSelected = !data.IsSelected;
            data.EmployeeTypeId = this.employeeType.Active;
            data.EmployeeTypeName = 'Active';
            this.hiringRequestForm.controls['FilledVacancies'].setValue(
              response.data.FilledVacancies
            );
          } else {
            this.toastr.error(response.message);
            this.candidateList[indexOfCandidate].IsSelectedFlag = false;
          }
        },
        error => {
          this.toastr.error('Someting went wrong');
          this.candidateList[indexOfCandidate].IsSelectedFlag = false;
        }
      );
  }
  //#endregion

  //#region "onAddInterviewCandidate"
  onAddInterviewCandidate(event: IReuestedCandidateDetailModel) {
    if (event != null) {
      this.addCandidateInterviewLoader = true;
      const interviewCandidatModel: IitervireCandidateModel = {
        EmployeeID: event.EmployeeID,
        JobDescription: this.hiringRequestForm.get('Description').value,
        OfficeId: this.hiringRequestForm.get('OfficeId').value
      };
      this.hiringRequestService
        .AddInterViewCandidateDetail(interviewCandidatModel)
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200) {
              event.IsInterViewed = !event.IsInterViewed;
              this.toastr.success(
                'Candidate Interview is created successfully'
              );
            } else {
              this.toastr.error(response.message);
            }
            this.addCandidateInterviewLoader = false;
          },
          error => {
            this.toastr.error('Someting went wrong. Please try again');
            this.addCandidateInterviewLoader = false;
          }
        );
    }
  }
  //#endregion

  //#region "seeCandidateDetail page of old Ui"
  seeCandidateDetail(path: string, empId) {
    const officeId = this.hiringRequestForm.controls['OfficeId'].value;
    window.open(
      this.appurl.getOldUiUrl() +
        path +
        '?empId=' +
        empId +
        '&officeId=' +
        officeId +
        '',
      '_blank'
    );
  }
  //#endregion

  //#region "onCompleteHiringRequestClicked"
  onCompleteHiringRequestClicked() {
    this.isCompletedFlag = true;
    this.SelctedHiringRequestId = this.hiringRequestForm.get(
      'HiringRequestId'
    ).value;
    this.hiringRequestService
      .IsCompltedeHrDEtail(this.SelctedHiringRequestId)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            this.isCompleted = response.data.IsCompleted;
          } else {
            this.toastr.error(response.message);
          }
          this.isCompletedFlag = false;
        },
        error => {
          this.toastr.error('Someting went wrong');
          this.isCompletedFlag = false;
        }
      );
  }
  //#endregion

  //#region delete donar datail
  onCandidateDetailDelete(item: any) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: 'delete',
      disableClose: false
    });

    dialogRef.componentInstance.confirmMessage =
      Delete_Confirmation_Texts.deleteText1;

    dialogRef.componentInstance.confirmText = Delete_Confirmation_Texts.yesText;

    dialogRef.componentInstance.cancelText = Delete_Confirmation_Texts.noText;

    dialogRef.afterClosed().subscribe(result => {});
    dialogRef.componentInstance.confirmDelete.subscribe(
      res => {
        dialogRef.componentInstance.isLoading = true;
        // Note : delete candidate from list
        const findIndex = this.candidateList.findIndex(
          x => x.EmployeeID === item.EmployeeID
        );
        this.candidateList.splice(findIndex, 1);
        dialogRef.componentInstance.onCancelPopup();
        if (
          item.CandidateId != null &&
          item.CandidateId !== undefined &&
          item.CandidateId !== 0
        ) {
          const candidateModel: IHiringReuestCandidateModel = {
            HiringRequestId: this.hiringRequestForm.get('HiringRequestId')
              .value,
            CandidateId: item.CandidateId
          };
          this.hiringRequestService
            .DeleteCandidateDetailDetail(candidateModel)
            .subscribe(
              response => {
                if (response.statusCode === 200) {
                  this.hiringRequestForm.controls['FilledVacancies'].setValue(
                    response.data.FilledVacancies
                  );
                } else {
                  this.toastr.error(response.message);
                }
              },
              error => {
                this.toastr.error('Someting went wrong');
                dialogRef.componentInstance.onCancelPopup();
              }
            );
        }
        dialogRef.componentInstance.isLoading = false;
      },
      error => {
        this.toastr.error('Someting went wrong');
        dialogRef.componentInstance.isLoading = false;
      }
    );
  }
  //#endregion
    //#region "onExportPdf"
    onExportPdf() {
      this.globalSharedService
        .getFile(this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetHiringRequestFormPdf,
                  this.hiringRequestModel
        )
        .pipe(takeUntil(this.destroyed$))
        .subscribe();
    }
    //#endregion
}
