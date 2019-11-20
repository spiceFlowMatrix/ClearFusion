import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ActivatedRoute } from '@angular/router';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { ToastrService } from 'ngx-toastr';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { of, Observable, ReplaySubject } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import {
  ICandidateDetail,
  IHiringRequestDetailModel,
  ILanguageDetailModel
} from '../models/hiring-requests-models';
import { takeUntil } from 'rxjs/operators';
import { MatDialog } from '@angular/material';
import { AddNewLanguageComponent } from './add-new-language/add-new-language.component';
import { AddNewTraningComponent } from './add-new-traning/add-new-traning.component';

@Component({
  selector: 'app-interview-detail',
  templateUrl: './interview-detail.component.html',
  styleUrls: ['./interview-detail.component.scss']
})
export class InterviewDetailComponent implements OnInit {
  languagesHeaders$ = of([
    'Language Id',
    'Language',
    'Reading',
    'Writing',
    'Listining',
    'Speaking'
  ]);
  traningHeaders$ = of([
    'Traning Id',
    'Traning Type',
    'Name',
    'Country/City',
    'Start Date',
    'End Date'
  ]);
  interviewersHeaders$ = of(['Employee Id', 'Employee Code', 'Full Name']);
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  interviewDetailForm: FormGroup;
  hiringRequestDetail: IHiringRequestDetailModel;
  candidateDetails: ICandidateDetail;
  noticePeriodList$: Observable<IDropDownModel[]>;
  statusList$: Observable<IDropDownModel[]>;
  languagesList$: Observable<ILanguageDetailModel[]>;
  traningList$: Observable<any[]>;
  interviewersList$: Observable<any[]>;
  ratingBasedCriteriaQuestionList: any[] = [];
  ratingBasedDropDown: any[];
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    public dialog: MatDialog,
    private fb: FormBuilder,
    private commonLoader: CommonLoaderService,
    private routeActive: ActivatedRoute,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService
  ) {
    this.ratingBasedDropDown = [
      {
        Id: 1,
        value: '1-Poor'
      },
      {
        Id: 2,
        value: '2-Good'
      },
      {
        Id: 3,
        value: '3-Very Good'
      },
      {
        Id: 4,
        value: '4-Excellent'
      }
    ];
    this.interviewDetailForm = this.fb.group({
      RatingBasedCriteriaList: [],
      Description: '',
      NoticePeriod: '',
      AvailableDate: '',
      WrittenTestMarks: '',
      CurrentBase: '',
      CurrentOther: '',
      ExpectationBase: '',
      ExpectationOther: '',
      Status: ''
    });

    this.noticePeriodList$ = of([
      { name: '5 Days', value: 5 },
      { name: '10 Days', value: 10 },
      { name: '15 Days', value: 15 },
      { name: '20 Days', value: 20 },
      { name: '25 Days', value: 25 },
      { name: '30 Days', value: 30 },
      { name: '35 Days', value: 35 },
      { name: '40 Days', value: 40 },
      { name: '45 Days', value: 45 }
    ] as IDropDownModel[]);

    this.statusList$ = of([
      { name: 'Hire', value: 1 },
      { name: 'Not Hire', value: 2 },
      { name: 'Other', value: 3 }
    ] as IDropDownModel[]);
  }

  ngOnInit() {
    this.candidateDetails = {
      FullName: '',
      DutyStation: '',
      Gender: '',
      Qualification: '',
      DateOfBirth: null
    };
    this.hiringRequestDetail = {
      Office: '',
      Position: '',
      JobGrade: '',
      TotalVacancy: null,
      FilledVacancy: null,
      PayCurrency: '',
      PayHourlyRate: null,
      BudgetLine: '',
      JobType: '',
      AnouncingDate: null,
      ClosingDate: null,
      ContractType: '',
      ContractDuration: null,
      JobShift: ''
    };

    // this.jobShiftList$ = of([
    //   { name: 'Day', value: 1 },
    //   { name: 'Night', value: 2 }
    // ] as IDropDownModel[]);

    this.languagesList$ = of([
      {
        LanguageName: '',
        LanguageReading: null,
        LanguageWriting: null,
        LanguageListining: null,
        LanguageSpeaking: null
      }
    ] as ILanguageDetailModel[]);

    this.getScreenSize();
    this.getRatingBasedCriteriaQuestion();
    this.getCandidateDetails();
    this.getAllHiringRequestDetails();
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize() {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }

  //#endregion

  // #region "getRatingBasedCriteriaQuestion"
  getRatingBasedCriteriaQuestion() {
    const OfficeId = 1;

    this.hiringRequestService
      .GetRatingBasedCriteriaQuestion(OfficeId)
      .subscribe(
        (response: IResponseData) => {
          this.commonLoader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach(element => {
              this.ratingBasedCriteriaQuestionList.push({
                Id: element.QuestionsId,
                value: element.Question
              });
            });
            console.log(this.ratingBasedCriteriaQuestionList);
          }
          this.commonLoader.hideLoader();
        },
        error => {
          this.commonLoader.hideLoader();
        }
      );
  }
  //#endregion

  // #region "getCandidateDetails"
  getCandidateDetails() {
    const CandidateId = 1;
    // this.candidateDetails = null;
    this.hiringRequestService
      .GetCandidateDetailsByCandidateId(CandidateId)
      .subscribe(
        (response: IResponseData) => {
          this.commonLoader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.candidateDetails = {
              FullName: response.data.FullName,
              DutyStation: response.data.DutyStation,
              Gender: response.data.Gender,
              Qualification: response.data.Qualification,
              DateOfBirth: response.data.DateOfBirth
            };
            console.log(this.ratingBasedCriteriaQuestionList);
          }
          this.commonLoader.hideLoader();
        },
        error => {
          this.commonLoader.hideLoader();
        }
      );
  }
  //#endregion

  // #region "getAllHiringRequestDetails"
  getAllHiringRequestDetails() {
    const model: any = {
      HiringRequestId: 2,
      ProjectId: 2
    };
    // this.candidateDetails = null;
    this.hiringRequestService
      .GetAllHiringRequestDetailForInterviewByHiringRequestId(model)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          this.commonLoader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.hiringRequestDetail = {
              Office: response.data.Office,
              Position: response.data.Position,
              JobGrade: response.data.JobGrade,
              TotalVacancy: response.data.TotalVacancy,
              FilledVacancy: response.data.FilledVacancy,
              PayCurrency: response.data.PayCurrency,
              PayHourlyRate: response.data.PayHourlyRate,
              BudgetLine: response.data.BudgetLine,
              JobType: response.data.JobType,
              AnouncingDate: response.data.AnouncingDate,
              ClosingDate: response.data.ClosingDate,
              ContractType: response.data.ContractType,
              ContractDuration: response.data.ContractDuration,
              JobShift: response.data.JobShift
            };
          }
          this.commonLoader.hideLoader();
        },
        error => {
          this.commonLoader.hideLoader();
        }
      );
  }
  //#endregion

  addNewLanguage(): void {
    const dialogRef = this.dialog.open(AddNewLanguageComponent, {
      width: '850px'
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      this.languagesList$.subscribe(res => {
        res.push(result);
        this.languagesList$ = of(res);
      });
      // this.languagesList$ = of(
      //   result.map(element => {
      //     return {
      //       LanguageName: element.LanguageName,
      //       LanguageReading: element.LanguageReading,
      //       LanguageWriting: element.LanguageWriting,
      //       LanguageListining: element.LanguageListining,
      //       LanguageSpeaking: element.LanguageSpeaking
      //     } as ILanguageDetailModel;
      //   })
      // );
    });
  }
  addNewTraning(): void {
    const dialogRef = this.dialog.open(AddNewTraningComponent, {
      width: '850px'
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
    });
  }
  onFormSubmit(data: any) {
    console.log(data);
  }
}
