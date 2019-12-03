import { Component, OnInit, HostListener } from '@angular/core';
import { of, Observable, ReplaySubject } from 'rxjs';
import {
  HiringRequestDetailList,
  ICandidateDetailList,
  ISubCandidateList,
  TableActionsModel,
  ICandidateFilterModel,
  IExistingCandidateList,
  CompleteHiringRequestModel
} from '../models/hiring-requests-models';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { AddHiringRequestComponent } from '../add-hiring-request/add-hiring-request.component';
import { MatDialog, MatSelectChange } from '@angular/material';
import { AddNewCandidateComponent } from '../add-new-candidate/add-new-candidate.component';
import {
  CandidateStatus,
  CandidateAction,
  Shift,
  HiringRequestStatus
} from 'src/app/shared/enum';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-request-detail',
  templateUrl: './request-detail.component.html',
  styleUrls: ['./request-detail.component.scss']
})
export class RequestDetailComponent implements OnInit {
  statusFilter: any[] = [
    { Id: 0, value: 'Pending Shortlist' },
    { Id: 1, value: 'Pending Interview' },
    { Id: 2, value: 'Pending Selection' },
    { Id: 3, value: 'Selected' },
    { Id: 4, value: 'Rejected' }
  ];
  newCandidatesHeaders$ = of([
    'Candidate Id',
    'First Name',
    'Last Name',
    'Gender',
    'Interview',
    'Candidate Status'
  ]);
  subListHeaders$ = of([
    'Education',
    'Phone Number',
    'Profession',
    'Email Address',
    'Relevant Experience',
    'Irrelevant Experience',
    'Total Experience'
  ]);
  existingCandidatesHeaders$ = of([
    'Employee Id',
    'Employee Code',
    'Full Name',
    'Gender',
    'Employee Status'
  ]);
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  // existingEmployeesList$: Observable<IDropDownModel[]>;
  existingEmployeesList: any[] = [];
  newCandidatesList$: Observable<ICandidateDetailList[]>;
  newCandidatesList2$: Observable<ICandidateDetailList[]>;
  hiringRequestDetails: HiringRequestDetailList;
  existingCandidatesList$: Observable<IExistingCandidateList[]>;
  existingCandidatesList2$: Observable<IExistingCandidateList[]>;
  filterValueModel: ICandidateFilterModel;
  completeRequestModel: CompleteHiringRequestModel;
  hiringRequestId: any;
  IsHiringRequestCompleted: boolean;
  IsHiringRequestClosed: boolean;
  projectId: any;
  candidateId: any;
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  actions: TableActionsModel;
  CandidateStatusSelection = [0, 1, 2];
  EmployeeStatusSelection = [0, 1, 2];
  constructor(
    public dialog: MatDialog,
    private globalSharedService: GlobalSharedService,
    private appurl: AppUrlService,
    private toastr: ToastrService,
    private routeActive: ActivatedRoute,
    public hiringRequestService: HiringRequestsService,
    private loader: CommonLoaderService,
    private router: Router
  ) {
    this.filterValueModel = {
      pageIndex: 0,
      pageSize: 10,
      TotalCount: null,
      FilterValue: '',
      ProjectId: null,
      HiringRequestId: null
    };
  }
  ngOnInit() {
    this.IsHiringRequestCompleted = false;
    this.IsHiringRequestClosed = false;
    this.hiringRequestDetails = {
      HiringRequestId: null,
      JobGrade: '',
      Position: '',
      TotalVacancies: '',
      FilledVacancies: '',
      PayCurrency: '',
      PayRate: '',
      Status: '',
      Office: '',
      DepartmentName: '',
      BudgetName: '',
      AnouncingDate: null,
      ClosingDate: null,
      ContractType: '',
      ContractDuration: null,
      Shift: '',
      EducationDegree: '',
      Profession: '',
      Experience: '',
      KnowledgeAndSkills: '',
      HiringRequestStatus: null,
      SpecificDutiesAndResponsibilities: '',
      SubmissionGuidelines: '',
      HiringRequestCode: ''
    };
    this.routeActive.params.subscribe(params => {
      this.hiringRequestId = +params['id'];
    });
    this.routeActive.parent.parent.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });

    this.actions = {
      items: {
        button: { status: true, text: '' },
        delete: false,
        download: false,
        edit: false
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false
      }
    };
    this.completeRequestModel = {
      HiringRequestId: [],
      ProjectId: this.projectId
    };
    this.getHiringRequestDetailsByHiringRequestId();
    this.getAllCandidateList(this.filterValueModel);
    this.getAllExistingCandidateList(this.filterValueModel);
    this.getExistingEmployeeDropDownList();
    this.getScreenSize();
  }
  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize() {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 130 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region "get Hiring Request Details By HiringRequestId"
  getHiringRequestDetailsByHiringRequestId() {
    if (this.hiringRequestId != null && this.hiringRequestId !== undefined) {
      this.hiringRequestService
        .GetProjectHiringRequestDetailsByHiringRequestId(this.hiringRequestId)
        .pipe(takeUntil(this.destroyed$))
        .subscribe(
          (response: IResponseData) => {
            this.loader.showLoader();
            if (response.statusCode === 200 && response.data !== null) {
              this.hiringRequestDetails = {
                HiringRequestId: response.data.HiringRequestId,
                JobGrade: response.data.JobGrade,
                Position: response.data.Position,
                TotalVacancies: response.data.TotalVacancies,
                FilledVacancies: response.data.FilledVacancies,
                PayCurrency: response.data.PayCurrency,
                PayRate: response.data.PayRate,
                Office: response.data.Office,
                DepartmentName: response.data.DepartmentName,
                BudgetName: response.data.BudgetName,
                AnouncingDate: response.data.AnouncingDate,
                ClosingDate: response.data.ClosingDate,
                ContractType: response.data.ContractType,
                ContractDuration: response.data.ContractDuration,
                Shift: Shift[response.data.Shift],
                EducationDegree: response.data.EducationDegree,
                Profession: response.data.Profession,
                Experience: response.data.Experience,
                KnowledgeAndSkills: response.data.KnowledgeAndSkills,
                HiringRequestStatus: response.data.HiringRequestStatus,
                SpecificDutiesAndResponsibilities:
                  response.data.SpecificDutiesAndResponsibilities,
                SubmissionGuidelines: response.data.SubmissionGuidelines,
                HiringRequestCode: response.data.HiringRequestId
              };
              if (
                this.hiringRequestDetails.HiringRequestStatus ===
                HiringRequestStatus.Completed
              ) {
                this.IsHiringRequestCompleted = true;
              } else if (
                this.hiringRequestDetails.HiringRequestStatus ===
                HiringRequestStatus.Closed
              ) {
                this.IsHiringRequestClosed = true;
              }
            }
            this.loader.hideLoader();
          },
          () => {
            this.loader.hideLoader();
          }
        );
    }
  }
  //#endregion
  // #region edit hiring request
  editHiringRequest(): void {
    // NOTE: It open AddHiringRequest (AddHiringRequestsComponent)
    const dialogRef = this.dialog.open(AddHiringRequestComponent, {
      width: '700px',
      autoFocus: false,
      data: {
        hiringRequestId: this.hiringRequestDetails.HiringRequestId,
        projectId: this.projectId
      }
    });

    // refresh the list after new request created
    dialogRef.componentInstance.onUpdateHiringRequestListRefresh.subscribe(
      () => {
        this.getHiringRequestDetailsByHiringRequestId();
      }
    );
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion
  //#region "Download pdf of Hiring Request Details"
  onExportHiringRequestPdf() {
    this.loader.showLoader();
    const data: any = {
      HiringRequestId: this.hiringRequestId,
      ProjectId: this.projectId
    };
    this.globalSharedService
      .getFile(
        this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetHiringRequestFormPdf,
        data
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe();
    this.loader.hideLoader();
  }
  //#endregion
  //#region Complete and close hiring request
  onCompleteRequest() {
    this.completeRequestModel = {
      HiringRequestId: [],
      ProjectId: this.projectId
    };
    this.completeRequestModel.HiringRequestId.push(this.hiringRequestId);
    this.hiringRequestService
      .IsCompltedeHrDetail(this.completeRequestModel)
      .subscribe(
        (responseData: IResponseData) => {
          if (responseData.statusCode === 200) {
            this.toastr.success('Hiring Request Successfully Completed');
            this.IsHiringRequestCompleted = true;
          } else if (responseData.statusCode === 400) {
            this.toastr.error('Something went wrong .Please try again.');
          }
        },
        error => {}
      );
  }
  onCloseRequest() {
    this.completeRequestModel = {
      HiringRequestId: [],
      ProjectId: this.projectId
    };

    this.completeRequestModel.HiringRequestId.push(this.hiringRequestId);
    this.hiringRequestService
      .IsCloasedHrDetail(this.completeRequestModel)
      .subscribe(
        (responseData: IResponseData) => {
          if (responseData.statusCode === 200) {
            this.IsHiringRequestClosed = true;
          } else if (responseData.statusCode === 400) {
            this.toastr.error('Something went wrong .Please try again.');
          }
        },
        error => {}
      );
  }
  //#endregion
  //#region "get All Candidate List"
  getAllCandidateList(filter: ICandidateFilterModel) {
    filter.ProjectId = this.projectId;
    filter.HiringRequestId = this.hiringRequestId;
    this.loader.showLoader();
    this.hiringRequestService.getAllCandidateList(filter).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          this.newCandidatesList$ = of(
            response.data.map(element => {
              return {
                CandidateId: element.CandidateId,
                FirstName: element.FirstName,
                LastName: element.LastName,
                Gender: element.Gender,
                Interview:
                  element.InterviewId == 0
                    ? 'Not Interviewed'
                    : '<a href="/project/my-project/' +
                      this.projectId +
                      '/hiring-request/interview-detail?candId=' +
                      element.CandidateId +
                      '&hiringId=' +
                      this.hiringRequestId +
                      '&interviewId=' +
                      element.InterviewId +
                      '">Interview ' +
                      element.InterviewId +
                      '</a>',
                CandidateStatus: CandidateStatus[element.CandidateStatus],
                itemAction:
                  element.CandidateStatus != CandidateStatus.Rejected &&
                  element.CandidateStatus != CandidateStatus.Selected
                    ? [
                        {
                          button: {
                            status: true,
                            text: 'Reject',
                            type: 'cancel'
                          },
                          delete: false,
                          download: false,
                          edit: false
                        },
                        {
                          button: {
                            status: true,
                            text: CandidateAction[element.CandidateStatus],
                            type: 'save'
                          },
                          delete: false,
                          download: false,
                          edit: false
                        }
                      ]
                    : [],
                subItems: [
                  {
                    EducationDegree: element.EducationDegree,
                    PhoneNumber: element.PhoneNumber,
                    Profession: element.Profession,
                    Email: element.Email,
                    RelevantExperienceInYear: element.RelevantExperienceInYear,
                    IrrelevantExperienceInYear:
                      element.IrrelevantExperienceInYear,
                    TotalExperienceInYear:
                      element.RelevantExperienceInYear +
                      element.IrrelevantExperienceInYear
                  }
                ] as ISubCandidateList[]
              } as ICandidateDetailList;
            })
          );
          this.newCandidatesList2$ = this.newCandidatesList$;

          this.newCandidatesList$.subscribe(res => {
            this.newCandidatesList2$ = of(
              res.filter(x =>
                this.CandidateStatusSelection.includes(
                  CandidateStatus[x.CandidateStatus]
                )
              )
            );
          });
        }
        this.loader.hideLoader();
      },
      () => {
        this.loader.hideLoader();
      }
    );
  }
  //#endregion
  // #region adding new candidate
  addNewCandidate(): void {
    // NOTE: It open AddHiringRequest dialog and passed the data into the AddHiringRequestsComponent Model
    const dialogRef = this.dialog.open(AddNewCandidateComponent, {
      width: '700px',
      autoFocus: false,
      data: {
        hiringRequestId: this.hiringRequestDetails.HiringRequestId,
        projectId: this.projectId
      }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onAddCandidateListRefresh.subscribe(() => {
      this.getAllCandidateList(this.filterValueModel);
    });
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion

  // #region Changes Status of New candidate
  newCandActionEvents(data: any) {
    switch (data.type) {
      case 'Select':
        this.selectCandidate(data);
        break;
      case 'Reject':
        this.rejectCandidate(data);
        break;
      case 'Shortlist':
        this.shortListCandidate(data);
        break;
      case 'Interview':
        this.router.navigate(['interview-detail'], {
          relativeTo: this.routeActive.parent,
          queryParams: {
            candId: data.item.CandidateId,
            hiringId: this.hiringRequestId,
            projectId: this.projectId,
            hiringRequestId: this.hiringRequestId
          }
        });
        break;
      default:
        break;
    }
  }
  shortListCandidate(data: any) {
    const candidateDetails: any = {
      statusId: +CandidateStatus[data.item.CandidateStatus],
      candidateId: data.item.CandidateId,
      projectId: this.projectId,
      hiringRequestId: this.hiringRequestId
    };
    this.updateCandidateStatus(candidateDetails);
  }
  selectCandidate(data: any) {
    const candidateDetails: any = {
      statusId: +CandidateStatus[data.item.CandidateStatus],
      candidateId: data.item.CandidateId,
      projectId: this.projectId,
      hiringRequestId: this.hiringRequestId
    };
    this.updateCandidateStatus(candidateDetails);
  }
  rejectCandidate(data: any) {
    const candidateDetails: any = {
      statusId: 4,
      candidateId: data.item.CandidateId,
      projectId: this.projectId,
      hiringRequestId: this.hiringRequestId
    };
    this.updateCandidateStatus(candidateDetails);
  }
  //#endregion

  // #region Filter New Candidate list by their status
  onStatusFilter(data: MatSelectChange) {
    if (data.value == '') {
      this.getAllExistingCandidateList(this.filterValueModel);
    } else {
      this.existingCandidatesList2$.subscribe(res => {
        this.existingCandidatesList$ = of(
          res.filter(x =>
            data.value.includes(CandidateStatus[x.CandidateStatus])
          )
        );
      });
    }
  }
  //#endregion
  //#region "Download pdf of shortlist candidate"
  onExportPdf() {
    this.loader.showLoader();
    const data: any = {
      HiringRequestId: this.hiringRequestId,
      ProjectId: this.projectId
    };
    this.globalSharedService
      .getFile(
        this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetCandidateDetailReportPdf,
        data
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe();
    this.loader.hideLoader();
  }
  //#endregion

  //#region "get All Existing Candidate List"
  getAllExistingCandidateList(filter: ICandidateFilterModel) {
    filter.ProjectId = this.projectId;
    filter.HiringRequestId = this.hiringRequestId;
    this.loader.showLoader();
    this.hiringRequestService.GetAllExistingCandidateList(filter).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          this.existingCandidatesList2$ = of(
            response.data.map(element => {
              return {
                EmployeeId: element.EmployeeId,
                EmployeeCode: element.EmployeeCode,
                FullName: element.FullName,
                Gender: element.Gender,
                CandidateStatus: CandidateStatus[element.CandidateStatus],
                itemAction:
                  element.CandidateStatus != CandidateStatus.Rejected &&
                  element.CandidateStatus != CandidateStatus.Selected
                    ? [
                        {
                          button: {
                            status: true,
                            text: 'Reject',
                            type: 'cancel'
                          },
                          delete: false,
                          download: false,
                          edit: false
                        },
                        {
                          button: {
                            status: true,
                            text: CandidateAction[element.CandidateStatus],
                            type: 'save'
                          },
                          delete: false,
                          download: false,
                          edit: false
                        }
                      ]
                    : []
              } as IExistingCandidateList;
            })
          );
          this.existingCandidatesList$ = this.existingCandidatesList2$;

          this.existingCandidatesList2$.subscribe(res => {
            this.existingCandidatesList$ = of(
              res.filter(x =>
                this.EmployeeStatusSelection.includes(
                  CandidateStatus[x.CandidateStatus]
                )
              )
            );
          });
        }
        this.loader.hideLoader();
      },
      () => {
        this.loader.hideLoader();
      }
    );
  }
  //#endregion
  //#region "get Existing Employee DropDown List"
  getExistingEmployeeDropDownList() {
    this.hiringRequestService.GetAllEmployeeList().subscribe(
      (response: IResponseData) => {
        this.loader.showLoader();
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.existingEmployeesList.push({
              Id: element.EmployeeId,
              value: element.EmployeeName
            });
          });
        }
        this.loader.hideLoader();
      },
      error => {
        this.loader.hideLoader();
      }
    );
  }
  //#endregion
  // #region adding Existing Employee for hiring request
  OnExistingEmployeeSelection(data: MatSelectChange) {
    this.existingCandidatesList2$.subscribe(res => {
      if (res.findIndex(x => x.EmployeeId == data.value) > -1) {
        this.toastr.warning('Employee already selected');
      } else {
        this.loader.showLoader();
        const candidateDetails: any = {
          HiringRequestId: this.hiringRequestId,
          ProjectId: this.projectId,
          EmployeeId: data.value
        };

        this.hiringRequestService
          .AddExistingCandidateDetail(candidateDetails)
          .subscribe(
            (response: IResponseData) => {
              if (response.statusCode === 200) {
                this.getAllExistingCandidateList(this.filterValueModel);
                this.toastr.success('Employee successfully added');
                this.loader.hideLoader();
              } else {
                this.toastr.error(response.message);
                this.loader.hideLoader();
              }
            },
            error => {
              this.toastr.error('Someting went wrong. Please try again');
              this.loader.hideLoader();
            }
          );
      }
    });
  }
  //#endregion

  // #region Changes Status of existing candidate
  empActionEvents(data: any) {
    switch (data.type) {
      case 'Select':
        this.selectEmployee(data);
        break;
      case 'Reject':
        this.rejectEmployee(data);
        break;
      default:
        break;
    }
  }
  selectEmployee(data: any) {
    const candidateDetails: any = {
      statusId: +CandidateStatus[data.item.CandidateStatus],
      employeeId: data.item.EmployeeId,
      projectId: this.projectId,
      hiringRequestId: this.hiringRequestId
    };
    this.updateCandidateStatus(candidateDetails);
  }
  rejectEmployee(data: any) {
    const candidateDetails: any = {
      statusId: 4,
      employeeId: data.item.EmployeeId,
      projectId: this.projectId,
      hiringRequestId: this.hiringRequestId
    };
    this.updateCandidateStatus(candidateDetails);
  }
  //#endregion

  // #region Filter Existing Candidate list by their status
  onStatusFilterCandidate(data: MatSelectChange) {
    if (data.value == '') {
      this.getAllCandidateList(this.filterValueModel);
    } else {
      this.newCandidatesList$.subscribe(res => {
        this.newCandidatesList2$ = of(
          res.filter(x =>
            data.value.includes(CandidateStatus[x.CandidateStatus])
          )
        );
      });
    }
  }
  //#endregion
  //#region "Update Status of new and Existing candidates"
  updateCandidateStatus(candidateDetails: any) {
    this.loader.showLoader();
    this.hiringRequestService
      .UpdateCandidateStatus(candidateDetails)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          this.loader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            const data = response.data;
            if (data.EmployeeID) {
              this.existingCandidatesList$.subscribe(res => {
                const index = res.findIndex(
                  x => x.EmployeeId == data.EmployeeID
                );
                const employee = res.find(x => x.EmployeeId == data.EmployeeID);
                employee.CandidateStatus =
                  CandidateStatus[data.CandidateStatus];
                employee.itemAction = [];

                res[index] = employee;
                this.existingCandidatesList$ = of(res);
              });
            } else {
              this.getAllCandidateList(this.filterValueModel);
            }
            console.log(response.data);
          }
          this.loader.hideLoader();
        },
        () => {
          this.loader.hideLoader();
        }
      );
  }
  //#endregion

  //#region Navigate back to hiring requset list page
  backToList() {
    window.history.back();
  }
  //#endregion
}
