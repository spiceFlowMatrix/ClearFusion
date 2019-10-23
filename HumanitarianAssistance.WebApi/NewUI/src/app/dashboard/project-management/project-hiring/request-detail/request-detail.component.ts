import { Component, OnInit, HostListener } from '@angular/core';
import { of, Observable, ReplaySubject } from 'rxjs';
import {
  HiringList,
  HiringRequestDetailList
} from '../models/hiring-requests-models';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-request-detail',
  templateUrl: './request-detail.component.html',
  styleUrls: ['./request-detail.component.scss']
})
export class RequestDetailComponent implements OnInit {
  newCandidatesHeaders$ = of([
    'Employee Code',
    'Full Name',
    'Gender',
    'Interview',
    'Candidate Status'
  ]);
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  newCandidatesList$: Observable<HiringList[]>;
  hiringRequestDetails: HiringRequestDetailList;
  hiringRequestId: any;
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  constructor(
    private routeActive: ActivatedRoute,
    public hiringRequestService: HiringRequestsService,
    private loader: CommonLoaderService
  ) {}

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
    this.newCandidatesList$ = of([
      {
        JobCode: 'E1534',
        JobGrade: 'Employee Name',
        FilledVacancies: 'Male',
        PayCurrency: '<a>Interview Id</a>',
        PayRate: '<p style="color: #bcbc1e";>pending interview</p>'
      }
    ] as HiringList[]);
    this.routeActive.params.subscribe(params => {
      this.hiringRequestId = +params['id'];
    });

    this.getHiringRequestDetailsByHiringRequestId();
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
  //#endregion
}
//GetRequestedCandidateById
