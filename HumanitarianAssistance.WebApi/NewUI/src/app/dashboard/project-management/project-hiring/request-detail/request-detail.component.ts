import { Component, OnInit, HostListener, EventEmitter, Output } from '@angular/core';
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
  projectId: any;
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  constructor(
    public dialog: MatDialog,
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
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
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
    dialogRef.componentInstance.onUpdateHiringRequestListRefresh.subscribe(() => {
      this.getHiringRequestDetailsByHiringRequestId();
    });
    dialogRef.afterClosed().subscribe(result => {

    });
  }

   // #region adding new hiring request
   addNewCandidate(): void {
    // NOTE: It open AddHiringRequest dialog and passed the data into the AddHiringRequestsComponent Model
    const dialogRef = this.dialog.open(AddNewCandidateComponent, {
      width: '1000px',
      autoFocus: false,
    });
    // // refresh the list after new request created
    // dialogRef.componentInstance.onAddHiringRequestListRefresh.subscribe(() => {
    //   // do something
    //   this.getAllHiringRequestFilterList();
    // });

    // dialogRef.afterClosed().subscribe(result => {
    // });
  }
  //#endregion

  //#endregion
  // //#region "GetCandidateList"
  // GetCandidateList(data: number) {
  //   if (data != null) {
  //     this.candidateList = [];
  //     const candidateDetail: IReuestedCandidateDetailModel = {
  //       HiringRequestId: data
  //     };
  //     this.hiringRequestService
  //       .GetRequestedCandidateById(candidateDetail)
  //       .subscribe(
  //         (response: IResponseData) => {
  //           if (response.statusCode === 200 && response.data != null) {
  //             response.data.forEach(element => {
  //               this.candidateList.push({
  //                 CandidateId: element.CandidateId,
  //                 EmployeeID: element.EmployeeID,
  //                 EmployeeCode: element.EmployeeCode,
  //                 EmployeeName: element.EmployeeName,
  //                 EmployeeTypeName: element.EmployeeTypeName,
  //                 Gender: element.Gender,
  //                 EmployeeTypeId: element.EmployeeTypeId,
  //                 IsInterViewed: element.IsInterViewed,
  //                 IsShortListed: element.IsShortListed,
  //                 IsSelected: element.IsSelected,
  //                 IsSelectedFlag: false
  //               });
  //             });
  //           } else {
  //             this.toastr.error(response.message);
  //           }
  //           this.getCandidateDetailLoader = false;
  //         },
  //         error => {
  //           this.toastr.error('Someting went wrong');
  //           this.getCandidateDetailLoader = false;
  //         }
  //       );
  //   }
  // }
  // //#endregion
}
