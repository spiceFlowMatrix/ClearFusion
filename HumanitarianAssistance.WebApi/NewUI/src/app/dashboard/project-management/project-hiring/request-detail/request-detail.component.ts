import { Component, OnInit, HostListener } from '@angular/core';
import { of, Observable, ReplaySubject } from 'rxjs';
import {
  HiringList,
  HiringRequestDetailList,
  ICandidateDetailModel,
  IFilterModel,
  ICandidateDetailList
} from '../models/hiring-requests-models';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { AddHiringRequestComponent } from '../add-hiring-request/add-hiring-request.component';
import { MatDialog } from '@angular/material';
import { AddNewCandidateComponent } from '../add-new-candidate/add-new-candidate.component';

@Component({
  selector: 'app-request-detail',
  templateUrl: './request-detail.component.html',
  styleUrls: ['./request-detail.component.scss']
})
export class RequestDetailComponent implements OnInit {
  newCandidatesHeaders$ = of([
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
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  newCandidatesList$: Observable<[ICandidateDetailList]>;
  hiringRequestDetails: HiringRequestDetailList;
  filterValueModel: IFilterModel;
  hiringRequestId: any;
  projectId: any;
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  constructor(
    public dialog: MatDialog,
    private routeActive: ActivatedRoute,
    public hiringRequestService: HiringRequestsService,
    private loader: CommonLoaderService
  ) {
    this.filterValueModel = {
      pageIndex: 0,
      pageSize: 10,
      TotalCount: null,
      FilterValue: '',
      ProjectId: null,
      IsOpenFlagId: null,
      IsInProgress: null
    };
  }

  ngOnInit() {
    this.hiringRequestDetails = {
      HiringRequestId: '',
      Description: '',
      JobCode: '',
      JobGrade: '',
      Position: '',
      TotalVacancies: '',
      FilledVacancies: '',
      PayCurrency: '',
      PayRate: '',
      Status: '',
      Office: ''
    };
    this.routeActive.params.subscribe(params => {
      this.hiringRequestId = +params['id'];
    });
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });

    this.getHiringRequestDetailsByHiringRequestId();
    this.GetAllCandidateList(this.filterValueModel);
    this.getScreenSize();
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
  //#region "getAllProjectActivityList"
  getHiringRequestDetailsByHiringRequestId() {
    this.hiringRequestService
      .GetProjectHiringRequestDetailsByHiringRequestId(this.hiringRequestId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          this.loader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.hiringRequestDetails = {
              Description: response.data.Description,
              HiringRequestId: response.data.HiringRequestId,
              JobCode: response.data.JobCode,
              JobGrade: response.data.JobGrade,
              Position: response.data.Position,
              TotalVacancies: response.data.TotalVacancies,
              FilledVacancies: response.data.FilledVacancies,
              PayCurrency: response.data.PayCurrency,
              PayRate: response.data.PayRate,
              Status: response.data.Status,
              Office: response.data.Office
            };
          }
          this.loader.hideLoader();
        },
        error => {
          this.loader.hideLoader();
        }
      );
  }

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
    dialogRef.afterClosed().subscribe(result => {});
  }

  // #region adding new hiring request
  addNewCandidate(): void {
    // NOTE: It open AddHiringRequest dialog and passed the data into the AddHiringRequestsComponent Model
    const dialogRef = this.dialog.open(AddNewCandidateComponent, {
      width: '1000px',
      autoFocus: false
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onAddCandidateListRefresh.subscribe(() => {
      // do something
      this.GetAllCandidateList(this.filterValueModel);
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }
  //#endregion

  GetAllCandidateList(filter: IFilterModel) {
    this.loader.showLoader();
    this.hiringRequestService.getAllCandidateList(filter).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          this.newCandidatesList$ = of(
            response.data.map(element => {
              return {
               // CandidateId: element.CandidateId,
                FirstName: element.FirstName,
                LastName: element.LastName,
                Email: element.Email,
                Interview: '<a>Interview Id</a>',
                // PhoneNumber: element.PhoneNumber,
                AccountStatus: '<p style="color: #bcbc1e";>' + element.AccountStatus + '</p>',
                // Gender: element.Gender,
                // DateOfBirth: element.DateOfBirth,
                // EducationDegree: element.EducationDegree,
                // Grade: element.Grade,
                // Profession: element.Profession,
                // Office: element.Office,
                // Country: element.Country,
                // Province: element.Province,
                // District: element.District,
                // TotalExperienceInYear: element.TotalExperienceInYear,
                // RelevantExperienceInYear: element.RelevantExperienceInYear,
                // IrrelevantExperienceInYear: element.IrrelevantExperienceInYear
              } as ICandidateDetailList;
            })
          );
        }
        this.loader.hideLoader();
      },
      error => {
        this.loader.hideLoader();
      }
    );
  }
}
