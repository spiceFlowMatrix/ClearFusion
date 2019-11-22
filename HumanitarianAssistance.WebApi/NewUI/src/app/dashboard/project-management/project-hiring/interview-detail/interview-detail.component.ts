import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
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
  ILanguageDetailModel,
  ITraningDetailModel,
  IInterviewerDetailModel,
  InterviewQuestionDetailModel,
  InterviewDetailModel
} from '../models/hiring-requests-models';
import { takeUntil } from 'rxjs/operators';
import { MatDialog, MatSelectChange } from '@angular/material';
import { AddNewLanguageComponent } from './add-new-language/add-new-language.component';
import { AddNewTraningComponent } from './add-new-traning/add-new-traning.component';
import { RatingAction } from 'src/app/shared/enum';
import { AddNewInterviewerComponent } from './add-new-interviewer/add-new-interviewer.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-interview-detail',
  templateUrl: './interview-detail.component.html',
  styleUrls: ['./interview-detail.component.scss']
})
export class InterviewDetailComponent implements OnInit {
  temp = 0;
  professionalCriteriaMarks = 0;
  marksObtain = 0;
  totalMarksObtain = 0;
  languagesHeaders$ = of([
    'Language',
    'Reading',
    'Writing',
    'Listining',
    'Speaking'
  ]);
  traningHeaders$ = of([
    'Traning Type',
    'Name',
    'Country/City',
    'Start Date',
    'End Date'
  ]);
  interviewerHeaders$ = of(['Employee Id', 'Employee Code', 'Full Name']);
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  candidateId: number;
  hiringRequestId: number;
  projectId: number;
  interviewDetailForm: FormGroup;
  hiringRequestDetail: IHiringRequestDetailModel;
  candidateDetails: ICandidateDetail;
  noticePeriodList$: Observable<IDropDownModel[]>;
  statusList$: Observable<IDropDownModel[]>;
  languagesList$: Observable<ILanguageDetailModel[]>;
  traningList$: Observable<ITraningDetailModel[]>;
  interviewerList$: Observable<IInterviewerDetailModel[]>;
  ratingBasedCriteriaQuestionList: any[] = [];
  technicalQuestionList: any[] = [];
  ratingBasedCriteriaAnswerList: InterviewQuestionDetailModel[] = [];
  technicalAnswerList: InterviewQuestionDetailModel[] = [];
  ratingBasedDropDown: any[];
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    public dialog: MatDialog,
    private datePipe: DatePipe,
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
      RatingBasedCriteriaList: [[], [Validators.required]],
      TechnicalQuestionList: [],
      LanguageList: [[], [Validators.required]],
      TraningList: [[], [Validators.required]],
      InterviewerList: [[], [Validators.required]],
      Description: [null, [Validators.required]],
      NoticePeriod: [null, [Validators.required]],
      AvailableDate: [null, [Validators.required]],
      WrittenTestMarks: [null, [Validators.required]],
      CurrentBase: [null, [Validators.required]],
      CurrentOther: [null, [Validators.required]],
      ExpectationBase: [null, [Validators.required]],
      ExpectationOther: [null, [Validators.required]],
      Status: [null, [Validators.required]],
      InterviewQuestionOne: [false, [Validators.required]],
      InterviewQuestionTwo: [false, [Validators.required]],
      InterviewQuestionThree: [false, [Validators.required]],
      CurrentTransport: [false, [Validators.required]],
      CurrentMeal: [false, [Validators.required]],
      ExpectationTransport: [false, [Validators.required]],
      ExpectationMeal: [false, [Validators.required]],
      ProfessionalCriteriaMark: [null, [Validators.required]],
      MarksObtain: [null, [Validators.required]],
      TotalMarksObtain: [null, [Validators.required]]
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
    this.routeActive.queryParams.subscribe(params => {
      this.candidateId = +params['candId'];
      this.hiringRequestId = +params['hiringId'];
    });
    this.routeActive.parent.parent.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
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
      JobShift: '',
      KnowledgeAndSkillsRequired: '',
      Profession: '',
      EducationDegree: '',
      TotalExperienceInYear: ''
    };
    this.getScreenSize();
    this.getRatingBasedCriteriaQuestion();
    this.getTechnicalQuestionsByDesignationId();
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
          }
          this.commonLoader.hideLoader();
        },
        error => {
          this.commonLoader.hideLoader();
        }
      );
  }
  //#endregion

  // #region "getTechnicalQuestionsByDesignationId"
  getTechnicalQuestionsByDesignationId() {
    const DesignationId = 1;

    this.hiringRequestService
      .GetTechnicalQuestionsByDesignationId(DesignationId)
      .subscribe(
        (response: IResponseData) => {
          this.commonLoader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            response.data.forEach(element => {
              this.technicalQuestionList.push({
                Id: element.QuestionId,
                value: element.Question
              });
            });
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
    // this.candidateDetails = null;
    this.hiringRequestService
      .GetCandidateDetailsByCandidateId(this.candidateId)
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
      HiringRequestId: this.hiringRequestId,
      ProjectId: this.projectId
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
              JobShift: response.data.JobShift,
              KnowledgeAndSkillsRequired:
                response.data.KnowledgeAndSkillsRequired,
              Profession: response.data.Profession,
              EducationDegree: response.data.EducationDegree,
              TotalExperienceInYear: response.data.TotalExperienceInYear
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
      if (result !== undefined) {
        if (this.languagesList$ === undefined) {
          this.languagesList$ = of([
            {
              LanguageName: result.LanguageName,
              LanguageReading: RatingAction[result.LanguageReading],
              LanguageWriting: RatingAction[result.LanguageWriting],
              LanguageListining: RatingAction[result.LanguageListining],
              LanguageSpeaking: RatingAction[result.LanguageSpeaking]
            }
          ] as ILanguageDetailModel[]);
        } else {
          result.LanguageName = result.LanguageName;
          result.LanguageReading = RatingAction[result.LanguageReading];
          result.LanguageWriting = RatingAction[result.LanguageWriting];
          result.LanguageListining = RatingAction[result.LanguageListining];
          result.LanguageSpeaking = RatingAction[result.LanguageSpeaking];
          this.languagesList$.subscribe(res => {
            res.push(result);
            this.languagesList$ = of(res);
          });
        }
        this.toastr.success('Language Added Successfully');
        this.languagesList$.subscribe(res => {
          this.interviewDetailForm.controls['LanguageList'].setValue(res);
        });
      }
    });
  }
  addNewTraning(): void {
    const dialogRef = this.dialog.open(AddNewTraningComponent, {
      width: '850px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        result.TraningStartDate = this.datePipe.transform(
          result.TraningStartDate,
          'dd-MM-yyyy'
        );
        result.TraningEndDate = this.datePipe.transform(
          result.TraningEndDate,
          'dd-MM-yyyy'
        );
        if (this.traningList$ === undefined) {
          this.traningList$ = of([
            {
              TraningType: result.TraningType,
              TraningName: result.TraningName,
              TraningCountryAndCity: result.TraningCountryAndCity,
              TraningStartDate: result.TraningStartDate,
              TraningEndDate: result.TraningEndDate
            }
          ] as ITraningDetailModel[]);
        } else {
          this.traningList$.subscribe(res => {
            res.push(result);
            this.traningList$ = of(res);
          });
        }
        this.toastr.success('Traning Added Successfully');
        this.traningList$.subscribe(res => {
          this.interviewDetailForm.controls['TraningList'].setValue(res);
        });
      }
    });
  }

  addInterviewers(): void {
    const dialogRef = this.dialog.open(AddNewInterviewerComponent, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        result.forEach(element => {
          if (this.temp === 0) {
            this.interviewerList$ = of([
              {
                EmployeeId: element.EmployeeId,
                EmployeeCode: element.EmployeeCode,
                EmployeeName: element.EmployeeName
              }
            ] as IInterviewerDetailModel[]);
            this.temp = 1;
            this.toastr.success('Added Successfully', element.EmployeeName);
          } else {
            this.interviewerList$.subscribe(res => {
              if (res.find(x => x.EmployeeId === element.EmployeeId) != null) {
                this.toastr.warning(' Already Selected', element.EmployeeName);
              } else {
                res.push(element);
                this.interviewerList$ = of(res);
                this.toastr.success('Added Successfully', element.EmployeeName);
              }
            });
          }
        });
        this.interviewerList$.subscribe(res => {
          this.interviewDetailForm.controls['InterviewerList'].setValue(res);
        });
      }
    });
  }
  onChangeRatingBasedCriteria(questionId: any, score: MatSelectChange) {
    if (
      this.ratingBasedCriteriaAnswerList.find(
        x => x.QuestionId === questionId
      ) == null
    ) {
      this.ratingBasedCriteriaAnswerList.push({
        QuestionId: questionId,
        Score: score.value
      });
    } else {
      this.ratingBasedCriteriaAnswerList.find(
        x => x.QuestionId === questionId
      ).Score = score.value;
    }
    this.professionalCriteriaMarks =
      this.ratingBasedCriteriaAnswerList.reduce(
        (sum, item) => sum + item.Score,
        0
      ) / this.ratingBasedCriteriaAnswerList.length;
      this.totalMarksObtain =
      this.marksObtain + this.professionalCriteriaMarks;
    this.interviewDetailForm.controls['ProfessionalCriteriaMark'].setValue(
      this.professionalCriteriaMarks
    );
    this.interviewDetailForm.controls['RatingBasedCriteriaList'].setValue(
      this.ratingBasedCriteriaAnswerList
    );
  }
  onChangeTechnicalQuestion(questionId: any, score: MatSelectChange) {
    if (
      this.technicalAnswerList.find(x => x.QuestionId === questionId) == null
    ) {
      this.technicalAnswerList.push({
        QuestionId: questionId,
        Score: score.value
      });
    } else {
      this.technicalAnswerList.find(x => x.QuestionId === questionId).Score =
        score.value;
    }
    this.marksObtain = this.technicalAnswerList.reduce(
      (sum, item) => sum + item.Score,
      0
    );
    this.totalMarksObtain =
      this.marksObtain + this.professionalCriteriaMarks;
    this.interviewDetailForm.controls['MarksObtain'].setValue(this.marksObtain);
    this.interviewDetailForm.controls['TotalMarksObtain'].setValue(
      this.totalMarksObtain
    );
    this.interviewDetailForm.controls['TechnicalQuestionList'].setValue(
      this.technicalAnswerList
    );
  }
  onFormSubmit(data: any) {
    if (this.interviewDetailForm.valid) {
      this.AddInterviewDetails(data);
    } else {
      this.toastr.warning('Please Insert Data For All Fileds and Lists');
    }
  }

  //#region "AddInterviewDetails"
  AddInterviewDetails(data: InterviewDetailModel) {
    this.hiringRequestService.AddInterviewDetails(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.toastr.success('New request is created successfully');
        } else {
          this.toastr.error(response.message);
        }
      },
      error => {
        this.toastr.error('Someting went wrong. Please try again');
      }
    );
  }
  //#endregion
}
