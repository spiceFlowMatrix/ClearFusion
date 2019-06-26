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
} from '../models/hiring-requests-model';
import { MatDialog } from '@angular/material';
import { AddHiringRequestsComponent } from '../add-hiring-requests/add-hiring-requests.component';
import { AddCandidateDaialogComponent } from '../add-candidate-daialog/add-candidate-daialog.component';
import { HiringRequestsService } from '../hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ToastrService } from 'ngx-toastr';
import { EmployeeType } from 'src/app/shared/enum';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { isNgTemplate } from '@angular/compiler';
import { ActivatedRoute } from '@angular/router';
import { EditCandidateDetailDialogComponent } from '../edit-candidate-detail-dialog/edit-candidate-detail-dialog.component';

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
  @Input() budgetLineList: IBudgetLineModel;
  @Input() officeList: IOfficeListModel;
  @Input() jobGradeList: IJobGradeModel;
  @Input() professionList: IProfessionList;

  employeeList: IEmployeeListModel[] = [];
  attendanceGroupList: IAttendaneGroupModel[] = [];
  candidateList: IReuestedCandidateDetailModel[] = [];
  employeeContractist: IEmployeeContractList[] = [];
  interviewCandidatModel: IitervireCandidateModel;

  @Output() UpdatedHRListRefresh = new EventEmitter<any[]>();
  //#endregion
  //#region "variables"
  hiringReuestDetailLoader = false;
  addCandidateInterviewLoader = false;
  interviewCompleteCheckFlag = false;
  isShotlistedCandidateFlag = false;
  // screen scroll
  screenHeight: number;
  screenWidth: number;
  scrollStyles: any;
  SelctedHiringRequestId: number;
  projectId: number;
  //#endregion

  hiringRequestForm: FormGroup;
  // Flag:
  selectedCandidateFlag = false;
  // Employeetype from enum
  employeeType = {
    Active: EmployeeType.Active,
    Candidate: EmployeeType.Candidate,
    Terminated: EmployeeType.Terminated
  };

  // candidate status:
  CandidateStatus = false;

  constructor(
    private fb: FormBuilder,
    public dialog: MatDialog,
    public toastr: ToastrService,
    private appurl: AppUrlService,
    private routeActive: ActivatedRoute,
    public hiringRequestService: HiringRequestsService
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.initForm();
    this.GetAllEmployeeList();
    this.GetAllEmployeeContractTypelist();
    this.GetAllAttendanceGrouplist();
    console.log(this.hiringRequestId);
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    }
    )
    //  this.interviewCompleteCheckFlag = this.hiringRequestDetail.IsInterViewed;
  }

  ngOnChanges() {
    if (
      this.hiringRequestDetail != null &&
      this.hiringRequestDetail !== 0 &&
      this.hiringRequestDetail !== undefined
    ) {
      this.onChanges();
      this.GetSelectedEmployeeDetail(this.hiringRequestDetail.HiringRequestId);
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
    const valu = this.hiringRequestDetail;
    console.log(valu);
    this.hiringRequestForm = this.fb.group({
      Description: ['', Validators.required],
      Position: ['', [Validators.required]],
      ProfessionId: ['', Validators.required],
      TotalVacancies: [null, Validators.required],
      FilledVacancies: [null, Validators.required],
      BasicPay: [null, Validators.required],
      CurrencyId: [null, Validators.required],
      BudgetLineId: [null, Validators.required],
      OfficeId: [null, Validators.required],
      GradeId: [null, Validators.required],
      RequestedBy: [null],
      ProjectId: [null]
    });
  }
  //#endregion

  onChanges() {
    this.hiringRequestForm = this.fb.group({
      Description: [this.hiringRequestDetail.Description],
      HiringRequestId: [this.hiringRequestDetail.HiringRequestId],
      HiringRequestCode: [this.hiringRequestDetail.HiringRequestCode],
      Position: [this.hiringRequestDetail.Position],
      ProfessionId: [this.hiringRequestDetail.ProfessionId],
      TotalVacancies: [this.hiringRequestDetail.TotalVacancies],
      FilledVacancies: [this.hiringRequestDetail.FilledVacancies],
      BasicPay: [this.hiringRequestDetail.BasicPay],
      CurrencyId: [this.hiringRequestDetail.CurrencyId],
      BudgetLineId: [this.hiringRequestDetail.BudgetLineId],
      OfficeId: [this.hiringRequestDetail.OfficeId],
      EmployeeID: [this.hiringRequestDetail.EmployeeID],
      GradeId: [this.hiringRequestDetail.GradeId],
      ProjectId: [this.hiringRequestDetail.ProjectId],
      RequestedBy: [this.hiringRequestDetail.RequestedBy]
    });
  }
  //#region "onAddNewRequestClicked"
  onEditHiringRequestClicked() {
    this.openHiringRequestDialog();
  }
  //#endregion

  //#region "openHiringRequestDialog"
  openHiringRequestDialog(): void {
    // NOTE: It passed the data into the Add Activity Model
    const dialogRef = this.dialog.open(AddHiringRequestsComponent, {
      width: '550px',
      autoFocus: false,
      data: {
        BudgetLineList: this.budgetLineList,
        OfficeList: this.officeList,
        CurrencyList: this.currencyList,
        JobGradeList: this.jobGradeList,
        HiringRequestDetail: this.hiringRequestDetail,
        ProfessionList: this.professionList
      }
    });

    // refresh the list after new request created
    dialogRef.componentInstance.onUpdateHiringRequestListRefresh.subscribe(
      (data: any) => {
        console.log('emitter', data);
        this.UpdatedHRListRefresh.emit(data);
        this.hiringRequestForm = this.fb.group({
          Description: [data.Description],
          HiringRequestId: [data.HiringRequestId],
          HiringRequestCode: [data.HiringRequestCode],
          Position: [data.Position],
          Profession: [data.Profession],
          TotalVacancies: [data.TotalVacancies],
          FilledVacancies: [data.FilledVacancies],
          BasicPay: [data.BasicPay],
          CurrencyId: [data.CurrencyId],
          BudgetLineId: [data.BudgetLineId],
          OfficeId: [data.OfficeId],
          EmployeeID: [data.EmployeeID],
          GradeId: [data.GradeId],
          ProjectId: [data.ProjectId],
          RequestedBy: [data.RequestedBy]
        });
      }
    );

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

  //#region "onAddEmployeeClicked"
  onAddCandidateClicked() {
    const dialogRef = this.dialog.open(AddCandidateDaialogComponent, {
      width: '400px',
      autoFocus: false,
      data: {
        EmployeeList: this.employeeList,
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
  GetAllEmployeeList() {
    this.hiringRequestService.GetAllEmployeeList().subscribe(
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
  //#endregion

  //#region "GetSelectedEmployeeDetail"
  GetSelectedEmployeeDetail(data: number) {
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
                  EmployeeID: element.EmployeeID,
                  EmployeeCode: element.EmployeeCode,
                  EmployeeName: element.EmployeeName,
                  EmployeeTypeName: element.EmployeeTypeName,
                  Gender: element.Gender,
                  EmployeeTypeId: element.EmployeeTypeId,
                  IsInterViewed: element.IsInterViewed,
                  IsShortListed: element.IsShortListed,
                  IsSelected: element.IsSelected
                });
              });
            } else {
              this.toastr.error(response.message);
            }
            // this.addHiringRequestLoader = false;
          },
          error => {
            this.toastr.error('Someting went wrong');
            // this.addHiringRequestLoader = false;
          }
        );
    }
  }
  //#endregion
  OnShortListClick(data: IHiringReuestCandidateModel) {
    if (data != null) {
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
            // this.candidateloaderFlag = false;
          },
          error => {
            this.toastr.error('Someting went wrong');
            // this.candidateloaderFlag = false;
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
    error => {}
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
            EmployeeContractTypeName: element.EmployeeContractTypeName,
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
    if (data != null) {
      if (data.EmployeeTypeId === this.employeeType.Candidate) {

        const dialogRef = this.dialog.open(EditCandidateDetailDialogComponent, {
          width: '550px',
          autoFocus: false,
          data: {
            HiringRequestDetail: this.hiringRequestDetail,
            AttendanceGroupList: this.attendanceGroupList,
            EmployeeId: data.EmployeeID,
            EmployeeContractist: this.employeeContractist
          }
        });
        dialogRef.componentInstance.employeeTypeDetial.subscribe((data: any) => {
          data.IsSelected = !data.IsSelected;
          data.EmployeeTypeId = this.employeeType.Active;
          data.EmployeeTypeName = 'Active';
        });
        dialogRef.afterClosed().subscribe(result => {});

      }   else {
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
            } else {
              this.toastr.error(response.message);
            }
            // this.candidateloaderFlag = false;
          },
          error => {
            this.toastr.error('Someting went wrong');
            // this.candidateloaderFlag = false;
          }
        );
    }
  }
  }
  //#endregion

  //#region "onAddInterviewCandidate"
  onAddInterviewCandidate(event: IReuestedCandidateDetailModel) {
    if (event != null) {
      this.addCandidateInterviewLoader = true;
      const interviewCandidatModel: IitervireCandidateModel = {
        EmployeeID: event.EmployeeID,
        JobDescription: this.hiringRequestForm.get('Description').value
      };
      this.hiringRequestService
        .AddInterViewCandidateDetail(interviewCandidatModel)
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200) {
              event.IsInterViewed = !event.IsInterViewed;

              // this.interviewCompleteCheckFlag = true;
              // this.hiringRequestListRefresh();
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
  seeCandidateDetail(path: string) {
    window.open(this.appurl.getOldUiUrl() + path, '_blank');
  }
  //#endregion

  //#region "onCompleteHiringRequestClicked"
  onCompleteHiringRequestClicked() {
    this.SelctedHiringRequestId = this.hiringRequestForm.get(
      'HiringRequestId').value;
    this.hiringRequestService
      .IsCompltedeHrDEtail(this.SelctedHiringRequestId)
      .subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
          } else {
            this.toastr.error(response.message);
          }
          // this.candidateloaderFlag = false;
        },
        error => {
          this.toastr.error('Someting went wrong');
          // this.candidateloaderFlag = false;
        }
      );
    //#endregion
  }
}
