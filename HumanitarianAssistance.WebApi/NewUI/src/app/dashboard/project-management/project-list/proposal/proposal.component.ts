import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  HostListener,
  OnDestroy,
  OnChanges
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ProjectListService } from '../service/project-list.service';
import {
  ProposalDocModel,
  CurrencyModel
} from '../project-details/models/project-details.model';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { ToastrService } from 'ngx-toastr';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-proposal',
  templateUrl: './proposal.component.html',
  styleUrls: ['./proposal.component.scss']
})
export class ProposalComponent implements OnInit, OnChanges, OnDestroy {

  @Output() proposalApprovedChange = new EventEmitter<any>();

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  projectId: number;
  IsProposalSubmit = null;
  proposalModel: ProposalDocModel;
  currencyList: CurrencyModel[];
  IsApproved?: boolean = null;
  pageId = ApplicationPages.Proposal;

  // flags
  isProposalDocumentAvailable = false;

  // loader flag
  startSubmitProposalLoader = false;
  currencyDetailLoader = true;
  budgetDetailLoader = false;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  // permission
  isEditingAllowed = false;


  constructor(
    private routeActive: ActivatedRoute,
    private appurl: AppUrlService,
    private projectListService: ProjectListService,
    private localStorageService: LocalStorageService,
    private toastr: ToastrService
  ) {
  }

  ngOnChanges() {
  }

  ngOnInit() {
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    // this.ProjectId = +this.routeActive.snapshot.paramMap.get('id');
    this.initForms();
    this.getProposalDetail(this.projectId);
    this.getAllCurrency();
    this.getScreenSize();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
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

  //#region "initForm"
  initForms() {
    this.proposalModel = {
      ProjectId: this.projectId,
      ProposalStartDate: null,
      ProposalBudget: null,
      ProposalDueDate: null,
      IsProposalAccept: null,
      CurrencyId: 0,
      IsApproved: false
    };
  }
  //#endregion

  //#region "getProposalDetail"
  getProposalDetail(projectid: number) {
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

              this.proposalModel.ProposalDueDate = dataItem.ProposalDueDate;
              this.proposalModel.ProposalStartDate = dataItem.ProposalStartDate;
              this.proposalModel.ProposalBudget = dataItem.ProposalBudget;

              this.IsApproved = dataItem.IsApproved; // ==null?false:dataItem.IsApproved;
              this.IsProposalSubmit = dataItem.IsProposalAccept; // == null ? this.proposalSubmitnull :dataItem.IsProposalAccept ;

              if (dataItem.CurrencyId > 0) {
                this.proposalModel.CurrencyId = dataItem.CurrencyId;
              }

            }
          }
        }
      });
  }
  //#endregion

  //#region "proposalDetailsChange"
  proposalDetailsChange(ev, data: any) {
    if (data != null && data !== '' && data !== undefined) {
      if (ev === 'proposalStartDate') {
        this.proposalModel.ProposalStartDate =
          data != null ? StaticUtilities.getLocalDate(data) : null;
        this.AddEditProjectProposal(this.proposalModel);
      }
      if (ev === 'dueDate') {
        this.proposalModel.ProposalDueDate =
          data != null ? StaticUtilities.getLocalDate(data) : null;
        this.AddEditProjectProposal(this.proposalModel);
      }
      if (ev === 'proposalAccept') {
        this.startSubmitProposalLoader = true;
        this.proposalModel.IsProposalAccept = data;
        this.AddEditProjectProposal(this.proposalModel);
      }
      if (ev === 'proposalBudget') {
        if (
          this.proposalModel.CurrencyId !== 0 &&
          this.proposalModel.CurrencyId != null &&
          this.proposalModel.CurrencyId !== undefined
        ) {
          this.proposalModel.ProposalBudget = data;
          this.budgetDetailLoader = true;
          this.AddEditProjectProposal(this.proposalModel);
        } else {
          this.toastr.warning('Please select Currency');
        }
      }
      if (ev === 'Currency') {
        this.proposalModel.CurrencyId = this.currencyList.find(
          x => x.CurrencyCode.toUpperCase() === data.toUpperCase()
        ).CurrencyId;

        this.AddEditProjectProposal(this.proposalModel);
      }
    }
  }
  //#endregion

  //#region "currencyDetailsChange"
  currencyDetailsChange(value) {
    this.currencyDetailLoader = true;
    this.proposalModel.CurrencyId = value;

    this.AddEditProjectProposal(this.proposalModel);
  }
  //#endregion

  //#region  add/edit other project
  AddEditProjectProposal(model: any) {
    const proposalDocModel: ProposalDocModel = {
      ProposalStartDate: model.ProposalStartDate,
      ProjectId: model.ProjectId,
      ProposalBudget: model.ProposalBudget,
      ProposalDueDate: model.ProposalDueDate,
      IsProposalAccept: model.IsProposalAccept,
      CurrencyId: model.CurrencyId
    };

    this.projectListService
      .AddEditProjectProposalDetail(
        this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddEditProjectProposalDetail,
        proposalDocModel
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            this.startSubmitProposalLoader = false;
            this.currencyDetailLoader = false;
            this.budgetDetailLoader = false;
            if (response.data.ProjectProposalDetail != null) {
              proposalDocModel.ProposalStartDate =
                response.data.ProjectProposalDetail.ProposalStartDate;
              if (
                response.data.ProjectProposalDetail.IsProposalAccept === true
              ) {
                this.IsProposalSubmit = true;
                this.IsApproved = true;
              }
            }
            this.proposalApprovedChange.emit(model.ProjectId);
          }
        },
        error => {
          this.startSubmitProposalLoader = false;
          this.currencyDetailLoader = false;
          this.budgetDetailLoader = false;
        }
      );
  }
  //#endregion

  //#region "getAllCurrency"
  getAllCurrency() {
    this.currencyDetailLoader = true;
    this.currencyList = [];
    this.projectListService
      .GetAllCurrency(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        data => {
          if (data != null && data.StatusCode === 200) {
            if (data.data.CurrencyList != null) {
              data.data.CurrencyList.forEach(element => {
                this.currencyList.push({
                  CurrencyId: element.CurrencyId,
                  CurrencyCode: element.CurrencyCode
                });
              });
            }
          }
          this.currencyDetailLoader = false;
        },
        error => {
          this.currencyDetailLoader = false;
        }
      );
  }
  //#endregion

  changeDocumentAvailableFlag(flag: boolean) {
    this.isProposalDocumentAvailable = flag;
    console.log(this.isProposalDocumentAvailable );
  }

  changeStartDate(data: any) {
    this.proposalModel.ProposalStartDate = new Date();
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
