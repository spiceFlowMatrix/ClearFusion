import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  ChangeDetectorRef,
  HostListener,
  OnDestroy,
  ChangeDetectionStrategy
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ProjectListService } from '../service/project-list.service';
import {
  UploadEvent,
  UploadFile,
  FileSystemFileEntry,
  FileSystemDirectoryEntry
} from 'ngx-file-drop';
import {
  ProposalDocModel,
  CurrencyModel,
  UserListModel
} from '../project-details/models/project-details.model';
import { FormControl } from '@angular/forms';
import { ProposalDocument_Enum } from 'src/app/shared/enum';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { ToastrService } from 'ngx-toastr';
import { TooltipPosition } from '@angular/material/tooltip';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { NgOnChangesFeature } from '@angular/core/src/render3';

@Component({
  selector: 'app-proposal',
  templateUrl: './proposal.component.html',
  styleUrls: ['./proposal.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProposalComponent implements OnInit, OnDestroy {
  // private httpSubscription: Subscription;
  ProjectId: number;
  public files: UploadFile[] = [];
  proposalWebLink = '';
  EDIFileWebLink: any = null;
  BudgetFileWebLink: any = null;
  ConceptFileWebLink: any = null;
  PresentationFileWebLink: any = null;
  showWarning: false;
  // ProposalStartDate: any;
  ProposalExtType: any;
  EDIFileExtType: any;
  BudgetFileExtType: any;
  ConceptFileExtType: any;
  PresentationExtType: any;
  ProposalBudget: any;
  ProposalDueDate: any;
  // ProjectAssignTo: number;
  // Currency: string;
  IsProposalSubmit = null;
  proposalSubmitnull = false;
  ProposalModel: ProposalDocModel;
  IsproposalStart = false;
  CurrencyList: CurrencyModel[];
  UserList: UserListModel[];
  IsApproved?: boolean = null;
  isEditingAllowed = false;
  pageId = ApplicationPages.Proposal;
  docUrl: string;
  // loader flag
  startProposalLoader = false;
  startEDIFileLoader = false;
  startBudgetFileLoader = false;
  startConceptFileLoader = false;
  startPresentationFileLoader = false;
  startSubmitProposalLoader = false;
  assignUserDetailLoader = false;
  currencyDetailLoader = false;
  budgetDetailLoader = false;

  IsCompleteAllDoc = false;
  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  @Output() proposalApprovedChange = new EventEmitter<any>();

  positionOptions: TooltipPosition[] = ['above'];
  position = new FormControl(this.positionOptions[0]);

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    private routeActive: ActivatedRoute,
    private appurl: AppUrlService,
    public projectListService: ProjectListService,
    private _cdr: ChangeDetectorRef,
    private localStorageService: LocalStorageService,
    private toastr: ToastrService
  ) {
    this.docUrl = this.appurl.getDocUrl();
  }

  ngOnInit() {
    this.routeActive.parent.params
    .subscribe(params => {
      this.ProjectId = +params['id'];
  });
  this.GetAllUserList();
    // this.ProjectId = +this.routeActive.snapshot.paramMap.get('id');
    this.GetProposal(this.ProjectId);
    this.initProposalDocModel();
    this.GetAllCurrency();
    this.getScreenSize();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
  }

  ngOnChanges() {
    this.GetAllUserList();
  }
  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  initProposalDocModel() {
    this.ProposalModel = {
      ProjectId: this.ProjectId,
      ProposalStartDate: null,
      ProposalBudget: null,
      ProposalDueDate: null,
      ProjectAssignTo: 0,
      IsProposalAccept: null,
      CurrencyId: 0,
      IsApproved: false
    };
  }

  GetProposal(projectid: number) {
    this.projectListService
      .GetProjectproposalById(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectproposalsById,
        projectid
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe(response => {
        if (response != null) {
          if (response.StatusCode === 200) {
            if (response.data.ProjectProposalModel != null) {
              const dataItem = response.data.ProjectProposalModel;

              // proposal
              this.proposalWebLink = dataItem.ProposalWebLink;

              // EDI File
              this.EDIFileWebLink = dataItem.EDIFileWebLink;

              // Budget File
              this.BudgetFileWebLink = dataItem.BudgetFileWebLink;

              // Concept File
              this.ConceptFileWebLink = dataItem.ConceptFileWebLink;

              this.PresentationFileWebLink = dataItem.PresentationFileWebLink;

              // this.ProposalStartDate = dataItem.CreatedDate == null ? null : dataItem.CreatedDate;
              this.EDIFileExtType =
                dataItem.EDIFileExtType == null
                  ? dataItem.EDIFileExtType
                  : dataItem.EDIFileExtType.trim();
              if (this.EDIFileWebLink === undefined) {
                this.EDIFileWebLink = null;
              }
              this.BudgetFileExtType =
                dataItem.BudgetFileExtType == null
                  ? dataItem.BudgetFileExtType
                  : dataItem.BudgetFileExtType.trim();
              if (this.BudgetFileExtType === undefined) {
                this.BudgetFileExtType = null;
              }
              this.ConceptFileExtType =
                dataItem.ConceptFileExtType == null
                  ? dataItem.ConceptFileExtType
                  : dataItem.ConceptFileExtType.trim();
              if (this.ConceptFileExtType === undefined) {
                this.ConceptFileExtType = null;
              }
              this.PresentationExtType =
                dataItem.PresentationExtType == null
                  ? dataItem.PresentationExtType
                  : dataItem.PresentationExtType.trim();
              if (this.PresentationExtType === undefined) {
                this.PresentationExtType = null;
              }
              this.ProposalModel.ProposalDueDate = dataItem.ProposalDueDate;
              this.ProposalModel.ProposalStartDate = dataItem.ProposalStartDate;
              this.ProposalModel.ProposalBudget = dataItem.ProposalBudget;
              this.IsApproved = dataItem.IsApproved; // ==null?false:dataItem.IsApproved;

              if (dataItem.UserId != null) {
                this.ProposalModel.UserId = dataItem.UserId;
              }
              this.IsProposalSubmit = dataItem.IsProposalAccept; // == null ? this.proposalSubmitnull :dataItem.IsProposalAccept ;
              if (dataItem.CurrencyId > 0) {
                this.ProposalModel.CurrencyId = dataItem.CurrencyId;
              }
              if (this.proposalWebLink !== '') {
                this.IsproposalStart = true;
              }

              if (this.IsProposalSubmit === null) {
                this.proposalSubmitnull = false;
              }
              if (this.IsProposalSubmit === true) {
                this.proposalSubmitnull = true;
              }
              if (this.PresentationFileWebLink != null) {
                this.IsCompleteAllDoc = true;
              }
            }
          }
          // else if(response.StatusCode == 400){
          //   this.ProposalWebLink=null;
          // }
          this._cdr.detectChanges();
        }
      });
  }

  StartProposal() {
    this.startProposalLoader = true;
    this.projectListService
      .CreateProjectproposal(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditProjectproposals,
        this.ProjectId
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            if (res.data.ProjectProposalDetail != null) {
              if (res.data.ProjectProposalDetail.ProposalWebLink != null) {
                this._cdr.detectChanges();

                this.proposalWebLink =
                  res.data.ProjectProposalDetail.ProposalWebLink;

                if (this.proposalWebLink !== '') {
                  this.IsproposalStart = true;
                }
                // this.GetProposal(this.ProjectId);
                this.startProposalLoader = false;
              }
            }
          } else {
          }
          this.startProposalLoader = false;
        },
        error => {
          this.startProposalLoader = false;
        }
      );
  }
  public fileOver(event) {}

  public fileLeave(event) {}

  public dropped(event: UploadEvent, data: any) {
    if (this.proposalWebLink !== '') {
      this.files = event.files;

      if (data === ProposalDocument_Enum.proposal) {
        this.startProposalLoader = true;
        this._cdr.detectChanges();
      } else if (data === ProposalDocument_Enum.edifile) {
        this.startEDIFileLoader = true;
        this._cdr.detectChanges();
      } else if (data === ProposalDocument_Enum.budgetfile) {
        this.startBudgetFileLoader = true;
        this._cdr.detectChanges();
      } else if (data === ProposalDocument_Enum.conceptfile) {
        this.startConceptFileLoader = true;
        this._cdr.detectChanges();
      } else if (data === ProposalDocument_Enum.presentationfile) {
        this.startPresentationFileLoader = true;
        this._cdr.detectChanges();
      }

      for (const droppedFile of event.files) {
        // Is it a file?
        if (droppedFile.fileEntry.isFile) {
          const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
          fileEntry.file((file: File) => {
            // Here you can access the real file
            // You could upload it like this:
            const formData = new FormData();
            formData.append('filesData', file, droppedFile.relativePath);
            formData.append('projectId', this.ProjectId.toString());
            formData.append('data', data);

            this.projectListService
              .uploadEDIFile(
                this.appurl.getApiUrl() +
                  GLOBAL.API_Project_UploadEDIProposalFile,
                this.ProjectId,
                formData
              )
              .pipe(takeUntil(this.destroyed$))
              .subscribe(
                response => {
                  if (response.StatusCode === 400) {
                    this.toastr.error('Someting went wrong');
                    this.GetProposal(this.ProjectId);
                  } else if (response.StatusCode === 4440) {
                    this._cdr.detectChanges();
                    this.toastr.warning('Document type is invalid');
                    this.startProposalLoader = false;
                    this.startEDIFileLoader = false;
                    this.startBudgetFileLoader = false;
                    this._cdr.detectChanges();
                  }
                  if (data === ProposalDocument_Enum.proposal) {
                    if (response.StatusCode === 200) {
                      this.proposalWebLink =
                        response.data.ProposalWebLink != null
                          ? response.data.ProposalWebLink
                          : null;

                      this.ProposalExtType =
                        response.data.ProposalWebLinkExtType != null
                          ? response.data.ProposalWebLinkExtType
                          : null;
                    }
                    this.startProposalLoader = false;
                    this._cdr.detectChanges();
                  } else if (data === ProposalDocument_Enum.edifile) {
                    if (response.StatusCode === 200) {
                      this.EDIFileWebLink =
                        response.data.EDIWebLink != null
                          ? response.data.EDIWebLink
                          : null;
                      this.EDIFileExtType =
                        response.data.EDIWebLinkExtType != null
                          ? response.data.EDIWebLinkExtType
                          : null;
                    }
                    this.startEDIFileLoader = false;
                    this._cdr.detectChanges();
                  } else if (data === ProposalDocument_Enum.budgetfile) {
                    if (response.StatusCode === 200) {
                      this.BudgetFileWebLink =
                        response.data.BudgetWebLink != null
                          ? response.data.BudgetWebLink
                          : null;
                      this.BudgetFileExtType =
                        response.data.BudgetWebLinkExtType != null
                          ? response.data.BudgetWebLinkExtType
                          : null;
                    }
                    this.startBudgetFileLoader = false;
                    this._cdr.detectChanges();
                  } else if (data === ProposalDocument_Enum.conceptfile) {
                    if (response.StatusCode === 200) {
                      this.ConceptFileWebLink =
                        response.data.ConceptWebLink != null
                          ? response.data.ConceptWebLink
                          : null;
                      this.ConceptFileExtType =
                        response.data.ConceptWebLinkExtType != null
                          ? response.data.ConceptWebLinkExtType
                          : null;
                    }
                    this.startConceptFileLoader = false;
                    this._cdr.detectChanges();
                  } else if (data === ProposalDocument_Enum.presentationfile) {
                    if (response.StatusCode === 200) {
                      this.PresentationFileWebLink =
                        response.data.PresentationWebLink != null
                          ? response.data.PresentationWebLink
                          : null;
                      this.PresentationExtType =
                        response.data.PresentationWebLinkExtType != null
                          ? response.data.PresentationWebLinkExtType
                          : null;
                    }
                    if (this.PresentationFileWebLink != null) {
                      this.IsCompleteAllDoc = true;
                    }
                    this.startPresentationFileLoader = false;
                    this._cdr.detectChanges();
                  }
                },
                error => {
                  this._cdr.detectChanges();
                  this.startProposalLoader = false;
                  this.startEDIFileLoader = false;
                  this.startBudgetFileLoader = false;
                  this.startConceptFileLoader = false;
                  this.startPresentationFileLoader = false;
                  this.toastr.warning('Some error occured. Try Again');
                  this._cdr.detectChanges();
                }
              );
          });
        } else {
          this._cdr.detectChanges();
          // It was a directory (empty directories are added, otherwise only files)
          const fileEntry = droppedFile.fileEntry as FileSystemDirectoryEntry;
          this.startProposalLoader = false;
          this.startEDIFileLoader = false;
          this.startBudgetFileLoader = false;
          this.startConceptFileLoader = false;
          this.startPresentationFileLoader = false;
          this._cdr.detectChanges();
        }
      }
    } else {
      this._cdr.detectChanges();
      this.toastr.warning('Please start proposal first');
      this._cdr.detectChanges();
    }
  }
//#endregion
  //#region start proposal drag and drop
  public startProposalDragFile(event: UploadEvent, data: any) {
    this.files = event.files;
    if (data === ProposalDocument_Enum.proposal) {
      this.startProposalLoader = true;
      this._cdr.detectChanges();
    }

    for (const droppedFile of event.files) {
      // Is it a file?
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
        fileEntry.file((file: File) => {
          // Here you can access the real file
          // You could upload it like this:
          const formData = new FormData();
          formData.append('filesData', file, droppedFile.relativePath);
          formData.append('projectId', this.ProjectId.toString());
          formData.append('data', data);

          this.projectListService
            .uploadEDIFile(
              this.appurl.getApiUrl() +
                GLOBAL.API_Project_StartProposalDragAndDropFile,
              this.ProjectId,
              formData
            )
            .pipe(takeUntil(this.destroyed$))
            .subscribe(
              res => {
                if (res.StatusCode === 200) {
                  if (res.data.ProjectProposalDetail != null) {
                    if (
                      res.data.ProjectProposalDetail.ProposalWebLink != null
                    ) {
                      this._cdr.detectChanges();
                      this.proposalWebLink =
                        res.data.ProjectProposalDetail.ProposalWebLink;

                      if (this.proposalWebLink !== '') {
                        this.IsproposalStart = true;
                      }
                      // this.GetProposal(this.ProjectId);
                      this.startProposalLoader = false;
                    }
                    this.ProposalModel.ProposalStartDate =
                      res.data.ProjectProposalDetail.ProposalStartDate;
                  }
                } else if (res.StatusCode === 4440) {
                  this._cdr.detectChanges();
                  this.toastr.warning('Document type is invalid');
                  this.startProposalLoader = false;
                  this._cdr.detectChanges();
                }
                this.startProposalLoader = false;
                this._cdr.detectChanges();
              },
              error => {
                // this._cdr.detectChanges();
                this.startProposalLoader = false;
                this.toastr.warning('Some error occured. Try Again');
                this._cdr.detectChanges();
              }
            );
        });
      } else {
        // It was a directory (empty directories are added, otherwise only files)
        const fileEntry = droppedFile.fileEntry as FileSystemDirectoryEntry;
        this.startProposalLoader = false;
        this.startEDIFileLoader = false;
        this.startBudgetFileLoader = false;
        this.startConceptFileLoader = false;
        this.startPresentationFileLoader = false;
      }
    }
  }
  //#endregion
  ProposalDetailsChange(ev, data: any) {
    if (data != null && data !== '' && data !== undefined) {
      if (ev === 'proposalStartDate') {
        this.ProposalModel.ProposalStartDate =
          data != null ? this.setDateTime(data) : null;
        this.AddEditProjectProposal(this.ProposalModel);
      }
      if (ev === 'dueDate') {
        this.ProposalModel.ProposalDueDate =
          data != null ? this.setDateTime(data) : null;
        this.AddEditProjectProposal(this.ProposalModel);
      }
      if (ev === 'proposalAccept') {
        this.startSubmitProposalLoader = true;
        this.ProposalModel.IsProposalAccept = data;
        this.AddEditProjectProposal(this.ProposalModel);
      }
      if (ev === 'proposalBudget') {
        if (this.ProposalModel.CurrencyId !== 0 && this.ProposalModel.CurrencyId != null && this.ProposalModel.CurrencyId !== undefined) {
          this.ProposalModel.ProposalBudget = data;
          this.budgetDetailLoader = true;
          this.AddEditProjectProposal(this.ProposalModel);
        } else {
          this.toastr.warning('Please select Currency');
        }
      }
      if (ev === 'Currency') {
        this.ProposalModel.CurrencyId = this.CurrencyList.find(
          x => x.CurrencyCode.toUpperCase() === data.toUpperCase()
        ).CurrencyId;

        this.AddEditProjectProposal(this.ProposalModel);
      }
      // if (ev == 'Assigned') {
      //   this.ProposalModel.UserId = data;// this.UserList.find(x => x.UserID.toUpperCase() === data.toUpperCase()).CurrencyId;

      //   this.AddEditProjectProposal(this.ProposalModel);
      // }
    }
  }

  //#region "assinedUserDetailsChange"
  assinedUserDetailsChange(value) {
    this.assignUserDetailLoader = true;
    this.ProposalModel.UserId = value; // this.UserList.find(x => x.UserID.toUpperCase() === data.toUpperCase()).CurrencyId;

    this.AddEditProjectProposal(this.ProposalModel);
  }
  //#endregion

  //#region "currencyDetailsChange"
  currencyDetailsChange(value) {
    this.currencyDetailLoader = true;
    this.ProposalModel.CurrencyId = value;

    this.AddEditProjectProposal(this.ProposalModel);
  }
  //#endregion

  //#region  add/edit other project
  AddEditProjectProposal(model: any) {
    // this.startSubmitProposalLoader = true;
    const proposalDocModel: ProposalDocModel = {
      ProposalStartDate: model.ProposalStartDate,
      ProjectId: model.ProjectId,
      ProposalBudget: model.ProposalBudget,
      ProposalDueDate: model.ProposalDueDate,
      ProjectAssignTo: model.ProjectAssignTo,
      IsProposalAccept: model.IsProposalAccept,
      CurrencyId: model.CurrencyId,
      UserId: model.UserId
    };

    this.projectListService
      .AddEditProjectProposalDetail(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddEditProjectProposalDetail,
        proposalDocModel
      )
      .pipe()
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            this.startSubmitProposalLoader = false;
            this.assignUserDetailLoader = false;
            this.currencyDetailLoader = false;
            this.budgetDetailLoader = false;
            if (response.data.ProjectProposalDetail != null) {
              this._cdr.detectChanges();
              proposalDocModel.ProposalStartDate =
                response.data.ProjectProposalDetail.ProposalStartDate;
              if (
                response.data.ProjectProposalDetail.IsProposalAccept === true
              ) {
                this.IsProposalSubmit = true;
                this.IsApproved = true;
              }
            }
            // this.GetProposal(model.ProjectId);
            this.proposalApprovedChange.emit(model.ProjectId);
          }
          this._cdr.detectChanges();

        },
        error => {
          this.startSubmitProposalLoader = false;
          this.assignUserDetailLoader = false;
          this.currencyDetailLoader = false;
          this.budgetDetailLoader = false;
        }
      );
  }
  //#endregion
  setDateTime(data): any {
    return new Date(
      new Date(data).getFullYear(),
      new Date(data).getMonth(),
      new Date(data).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );
  }
  numberOnly(event): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }
  GetAllCurrency() {
    this.currencyDetailLoader = true;
    this.CurrencyList = [];
    this.projectListService
      .GetAllCurrency(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        data => {
          if (data != null && data.StatusCode === 200) {
            if (data.data.CurrencyList != null) {
              data.data.CurrencyList.forEach(element => {
                this.CurrencyList.push({
                  CurrencyId: element.CurrencyId,
                  CurrencyCode: element.CurrencyCode
                });
              });
            }
          }
          this._cdr.detectChanges();
          this.currencyDetailLoader = false;
        },
        error => {
          this.currencyDetailLoader = false;
        }
      );
  }

  GetAllUserList() {
    this.assignUserDetailLoader = true;
    this.UserList = [];
    this.projectListService
      .GetAllUserList()
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        response => {
          if (response.data != null) {
            if (response.data.length > 0) {
              response.data.forEach((element: any) => {
                this.UserList.push({
                  UserID: element.UserID,
                  Username: element.FirstName + ' ' + element.LastName
                });
              });
            }
          }
          this._cdr.detectChanges();
          this.assignUserDetailLoader = false;
        },
        error => {
          this.assignUserDetailLoader = false;
        }
      );
  }

  onGetProposalDoc(objectName: string) {
    const objectData = {
      ObjectName: objectName
    };
    this.projectListService
      .GetSignedUrl(
        this.appurl.getApiUrl() + GLOBAL.API_Project_DownloadFileFromBucket,
        objectData
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        data => {
          if (data != null) {
            if (data.StatusCode === 200 && data.data.SignedUrl != null) {
              window.open(data.data.SignedUrl, '_blank');
            }
          }
        },
        error => {}
      );
  }

  ngOnDestroy() {
    this._cdr.detach();
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
