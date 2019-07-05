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
  IEmployeeContractList
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
  @Output() UpdatedHRListRefresh = new EventEmitter<any[]>();
  //#endregion

  // Model:
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
      ProjectId: [null],
      IsCompleted: [null]
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
      RequestedBy: [this.hiringRequestDetail.RequestedBy],
      IsCompleted: [this.hiringRequestDetail.IsCompleted]
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
        HiringRequestDetail: this.hiringRequestForm.value,
        ProfessionList: this.professionList,
        ProjectId: this.projectId
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
    this.filteredEmployeeList = [];

    this.filteredEmployeeList = this.employeeList.filter((employee) =>
    this.candidateList.every((candidate) => employee.EmployeeId !== candidate.EmployeeID));
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
        JobDescription: this.hiringRequestForm.get('Description').value
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
  seeCandidateDetail(path: string) {
    window.open(this.appurl.getOldUiUrl() + path, '_blank');
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
}
