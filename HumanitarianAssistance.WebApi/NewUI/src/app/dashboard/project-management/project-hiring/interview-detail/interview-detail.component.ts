import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { ToastrService } from 'ngx-toastr';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { of, Observable, ReplaySubject, forkJoin } from 'rxjs';
import 'rxjs/add/operator/pairwise';
import { IDropDownModel } from 'src/app/store/models/purchase';
import {
  ICandidateDetail,
  IHiringRequestDetailModel,
  ILanguageDetailModel,
  ITraningDetailModel,
  IInterviewerDetailModel,
  InterviewQuestionDetailModel,
  InterviewDetailModel,
  ISelectBoxModel,
  TableActionsModel,
  CvDownloadModel
} from '../models/hiring-requests-models';
import { takeUntil, findIndex } from 'rxjs/operators';
import { MatDialog, MatSelectChange } from '@angular/material';
import { AddNewLanguageComponent } from './add-new-language/add-new-language.component';
import { AddNewTraningComponent } from './add-new-traning/add-new-traning.component';
import { RatingAction, Gender } from 'src/app/shared/enum';
import { AddNewInterviewerComponent } from './add-new-interviewer/add-new-interviewer.component';
import { DatePipe } from '@angular/common';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { GLOBAL } from 'src/app/shared/global';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';

@Component({
  selector: 'app-interview-detail',
  templateUrl: './interview-detail.component.html',
  styleUrls: ['./interview-detail.component.scss']
})
export class InterviewDetailComponent implements OnInit {
  languagesHeaders$ = of([
    'Language',
    'Reading',
    'Writing',
    'Listining',
    'Speaking'
  ]);
  traningHeaders$ = of([
    'Training Type',
    'Name',
    'Country/City',
    'Start Date',
    'End Date'
  ]);
  interviewerHeaders$ = of(['Employee Id', 'Employee Code', 'Full Name']);
  temp = 0;
  isDisplay = true;
  professionalCriteriaMarks = 0;
  marksObtain = 0;
  totalMarksObtain = 0;
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  candidateId: number;
  hiringRequestId: number;
  projectId: number;
  interviewId: any;
  interviewDetailForm: FormGroup;
  hiringRequestDetail: IHiringRequestDetailModel;
  candidateDetails: ICandidateDetail;
  noticePeriodList$: Observable<IDropDownModel[]>;
  statusList$: Observable<IDropDownModel[]>;
  languagesList$: Observable<ILanguageDetailModel[]>;
  tempLanguagesList: ILanguageDetailModel[] = [];
  traningList$: Observable<ITraningDetailModel[]>;
  traningListTwo$: Observable<ITraningDetailModel[]>;
  interviewerList$: Observable<IInterviewerDetailModel[]>;
  ratingBasedCriteriaQuestionList: any[] = [];
  technicalQuestionList: any[] = [];
  ratingBasedCriteriaAnswerList: InterviewQuestionDetailModel[] = [];
  technicalAnswerList: InterviewQuestionDetailModel[] = [];
  ratingBasedDropDown: ISelectBoxModel[];
  technicalQuestionDropdown: ISelectBoxModel[];
  interviewDetails: InterviewDetailModel;
  candidateCv: CvDownloadModel;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  actions: TableActionsModel;
  constructor(
    public dialog: MatDialog,
    private datePipe: DatePipe,
    private fb: FormBuilder,
    private commonLoader: CommonLoaderService,
    private routeActive: ActivatedRoute,
    private router: Router,
    private hiringRequestService: HiringRequestsService,
    private globalSharedService: GlobalSharedService,
    private appurl: AppUrlService,
    private toastr: ToastrService
  ) {
    this.ratingBasedDropDown = [
      {
        Id: 0,
        value: '0-Poor'
      },
      {
        Id: 3,
        value: '3-Below Average'
      },
      {
        Id: 5,
        value: '5-Average'
      },
      {
        Id: 7,
        value: '7-Above Average'
      },
      {
        Id: 10,
        value: '10-Outstanding'
      }
    ];

    this.technicalQuestionDropdown = [
      {
        Id: 0,
        value: '0-Poor'
      },
      {
        Id: 10,
        value: '10-Fair'
      },
      {
        Id: 15,
        value: '15-Good'
      },
      {
        Id: 20,
        value: '20-Excellent'
      },
      {
        Id: 30,
        value: '30-Perfect'
      }
    ];

    this.noticePeriodList$ = of([
      { name: '15 Days', value: 1 },
      { name: '30 Days', value: 2 },
      { name: 'Others', value: 3 }
    ] as IDropDownModel[]);

    this.statusList$ = of([
      { name: 'Hire', value: 1 },
      { name: '2nd Choice', value: 2 },
      { name: 'Test Day', value: 3 },
      { name: 'Recommended for Other Position', value: 4 },
      { name: 'Reject', value: 5 },
      { name: 'Over Qualified', value: 6 }
    ] as IDropDownModel[]);

    this.interviewDetailForm = this.fb.group({
      CandidateId: [null],
      HiringRequestId: [null],
      InterviewId: [null],
      RatingBasedCriteriaList: [[], [Validators.required]],
      TechnicalQuestionList: [[], [Validators.required]],
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
  }
  ngOnInit() {
    if (this.interviewId > 0) {
      this.isDisplay = false;
    }
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: false,
        edit: false
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false
      }
    };
    this.routeActive.queryParams.subscribe(params => {
      this.candidateId = +params['candId'];
      this.hiringRequestId = +params['hiringId'];
      this.interviewId = +params['interviewId'];
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
    this.getAllHiringRequestDetails();
    this.getCandidateDetails();
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
  // #region "Get hiring reuquest details"
  getAllHiringRequestDetails() {
    const model: any = {
      HiringRequestId: this.hiringRequestId,
      ProjectId: this.projectId
    };
    this.hiringRequestService
      .GetAllHiringRequestDetailForInterviewByHiringRequestId(model)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          this.commonLoader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.hiringRequestDetail = {
              OfficeId: response.data.OfficeId,
              DesignationId: response.data.DesignationId,
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
            forkJoin([
              this.getRatingBasedCriteriaQuestion(response.data.OfficeId),
              this.getTechnicalQuestionsByDesignationId(
                response.data.DesignationId
              )
            ]).subscribe(res => {
              this.commonLoader.showLoader();
              if (res[0].statusCode === 200 && res[0].data !== null) {
                res[0].data.forEach(element => {
                  this.ratingBasedCriteriaQuestionList.push({
                    Id: element.QuestionsId,
                    value: element.Question,
                    selected: null
                  });
                });
              }
              if (res[1].statusCode === 200 && res[1].data !== null) {
                res[1].data.forEach(element => {
                  this.technicalQuestionList.push({
                    Id: element.QuestionId,
                    value: element.Question
                  });
                });
              }
              this.commonLoader.hideLoader();
              if (this.interviewId > 0) {
                this.isDisplay = false;
                this.getInterviewDetailsByInterviewId();
              }
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
  // #region "Get candidate details"
  getCandidateDetails() {
    this.hiringRequestService
      .GetCandidateDetailsByCandidateId(this.candidateId)
      .subscribe(
        (response: IResponseData) => {
          this.commonLoader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.candidateDetails = {
              FullName: response.data.FullName,
              DutyStation: response.data.DutyStation,
              Gender: Gender[response.data.Gender],
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
  //#region "Download Candidate Cv"
  getCandidateCvByCandidateId() {
    const candidateId = this.candidateId;
    if (candidateId != null && candidateId !== undefined) {
      this.commonLoader.showLoader();
      this.hiringRequestService
        .DownloadCandidateCvByRequestId(candidateId)
        .pipe(takeUntil(this.destroyed$))
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200 && response.data !== null) {
              this.candidateCv = {
                AttachmentName: response.data.AttachmentName,
                AttachmentUrl: response.data.AttachmentUrl,
                UploadedBy: response.data.UploadedBy
              };
              const anchor = document.createElement('a');
              anchor.href = this.candidateCv.AttachmentUrl;
              anchor.target = '_blank';
              anchor.click();
            }
            this.commonLoader.hideLoader();
          },
          () => {
            this.commonLoader.hideLoader();
          }
        );
    }
  }
  //#endregion
  //#region "Calculate total marks"
  WritenTextMarks(data: any) {
    if (data.srcElement.value > 60) {
      this.toastr.warning('Written Test Marks Should be Less Then Or Equal to 60');
    } else {
      if (
        data.srcElement.value != null &&
        data.srcElement.value !== undefined
      ) {
        this.TotalMarks(data.srcElement.value);
      }
    }
  }
  TotalMarks(data: any) {
    if (data != null && data !== undefined) {
      this.totalMarksObtain =
        this.professionalCriteriaMarks + this.marksObtain + +data;
    } else {
      this.totalMarksObtain = this.professionalCriteriaMarks + this.marksObtain;
    }
    this.interviewDetailForm.controls['TotalMarksObtain'].setValue(
      this.totalMarksObtain
    );
  }
  //#endregion
  // #region "Get rating based criteria questions"
  getRatingBasedCriteriaQuestion(OfficeId: number) {
    return this.hiringRequestService.GetRatingBasedCriteriaQuestion(OfficeId);
  }
  //#endregion
  // #region "Get technical questions by designationId"
  getTechnicalQuestionsByDesignationId(DesignationId: number) {
    return this.hiringRequestService.GetTechnicalQuestionsByDesignationId(
      DesignationId
    );
  }
  //#endregion
  // #region "On change rating based criteria question score"
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
      this.ratingBasedCriteriaQuestionList.reduce(
        (sum, item) => sum + item.selected,
        0
      ) / this.ratingBasedCriteriaQuestionList.length;
    this.TotalMarks(this.interviewDetailForm.get('WrittenTestMarks').value);
    this.interviewDetailForm.controls['ProfessionalCriteriaMark'].setValue(
      this.professionalCriteriaMarks
    );
    this.interviewDetailForm.controls['RatingBasedCriteriaList'].setValue(
      this.ratingBasedCriteriaAnswerList
    );
  }
  //#endregion
  // #region "On change technical question score"
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
    this.marksObtain =
      this.technicalQuestionList.reduce((sum, item) => sum + item.selected, 0) /
      this.technicalQuestionList.length;
    this.TotalMarks(this.interviewDetailForm.get('WrittenTestMarks').value);

    this.interviewDetailForm.controls['MarksObtain'].setValue(this.marksObtain);

    this.interviewDetailForm.controls['TechnicalQuestionList'].setValue(
      this.technicalAnswerList
    );
  }
  //#endregion
  // #region "Add new language"
  addNewLanguage(): void {
    /** Open AddNewLaguage dialog box*/
    const dialogRef = this.dialog.open(AddNewLanguageComponent, {
      width: '950px'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        if (this.languagesList$ === undefined) {
          /** binding result data(language details) to laguages list*/
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
  //#endregion
  // #region "Add new traning"
  addNewTraning(): void {
    /** Open AddNewTraning dialog box*/
    const dialogRef = this.dialog.open(AddNewTraningComponent, {
      width: '850px'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        (result.TraningStartDate = this.datePipe.transform(
          result.TraningStartDate,
          'MMM d, y'
        )),
          (result.TraningEndDate = this.datePipe.transform(
            result.TraningEndDate,
            'MMM d, y'
          ));
        if (this.traningList$ === undefined) {
          /** binding result data(traning details) to traning list*/
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
        this.toastr.success('Training Added Successfully');
        this.traningList$.subscribe(res => {
          this.interviewDetailForm.controls['TraningList'].setValue(res);
        });
      }
    });
  }
  //#endregion
  // #region "Add inerviewers(multiple or single)"
  addInterviewers(): void {
    /** Open AddInterviewers dialog box*/
    const dialogRef = this.dialog.open(AddNewInterviewerComponent, {
      width: '500px',
      data: this.hiringRequestDetail.OfficeId
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        /** binding result data(interviewers details) to interviewer list*/
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
  //#endregion
  // #region "Delete perticular data from list"
  actionEventsLanguage(event: any) {
    if (event.type === 'delete') {
      let index;
      this.languagesList$.subscribe(res => {
        index = res.findIndex(x => x.LanguageName === event.item.LanguageName);
        res.splice(index, 1);
        this.languagesList$ = of(res);
      });
    }
  }
  actionEventsTraining(event: any) {
    if (event.type === 'delete') {
      let index;
      this.traningList$.subscribe(res => {
        index = res.findIndex(x => x.TraningName === event.item.TraningName);
        res.splice(index, 1);
        this.traningList$ = of(res);
      });
    }
  }
  actionEventsInterviewers(event: any) {
    if (event.type === 'delete') {
      let index;
      this.interviewerList$.subscribe(res => {
        index = res.findIndex(x => x.EmployeeId === event.item.EmployeeId);
        res.splice(index, 1);
        this.interviewerList$ = of(res);
      });
    }
  }
  //#endregion
  //#region "Add interview details"
  AddInterviewDetails(data: InterviewDetailModel) {
    if (+this.totalMarksObtain > 100) {
      this.toastr.warning('Marks Total Should be Less Then or Equal to 100');
    } else {
    this.commonLoader.showLoader();
    data.CandidateId = this.candidateId;
    data.HiringRequestId = this.hiringRequestId;
    this.hiringRequestService.AddInterviewDetails(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.toastr.success('Interview details added successfully');
          this.backToRequestDetail();
        } else {
          this.toastr.error(response.message);
        }
      },
      error => {
        this.toastr.error('Someting went wrong. Please try again');
      }
    );
    this.commonLoader.showLoader();
    }
  }
  //#endregion
  //#region "Add interview details"
  EditInterviewDetails(data: InterviewDetailModel) {
    if (+this.totalMarksObtain > 100) {
      this.toastr.warning('Marks Total Should be Less Then or Equal to 100');
    } else {
    this.commonLoader.showLoader();
    data.CandidateId = this.candidateId;
    data.HiringRequestId = this.hiringRequestId;
    data.InterviewId = this.interviewId;
    this.languagesList$.subscribe(res => {
      data.LanguageList = res;
    });
    this.traningList$.subscribe(res => {
      data.TraningList = res;
    });
    data.ProfessionalCriteriaMark = this.professionalCriteriaMarks;
    data.MarksObtain = this.marksObtain;
    data.RatingBasedCriteriaList = this.ratingBasedCriteriaQuestionList;
    data.TechnicalQuestionList = this.technicalQuestionList;
    this.interviewerList$.subscribe(res => {
      data.InterviewerList = res;
    });
    this.hiringRequestService.EditInterviewDetails(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.toastr.success('Interview details updated successfully');
        } else {
          this.toastr.error(response.message);
        }
      },
      error => {
        this.toastr.error('Someting went wrong. Please try again');
      }
    );
    this.commonLoader.hideLoader();
    }
  }
  //#endregion
  //#region "On submission of interview form"
  onFormSubmit(data: any) {
    if (this.interviewDetailForm.valid) {
      if (this.interviewId > 0) {
        this.EditInterviewDetails(data);
      } else {
        this.AddInterviewDetails(data);
      }
    } else {
      this.toastr.warning('Please fill all required fields');
    }
  }
  //#endregion
  // #region "gGet interview details by InterviewId"
  getInterviewDetailsByInterviewId() {
    const InterviewId = this.interviewId;
    this.hiringRequestService
      .GetInterviewDetailsByInterviewId(InterviewId)
      .subscribe(
        (response: IResponseData) => {
          this.commonLoader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.setRemainingInterviewDetails(response.data);
          }
          this.commonLoader.hideLoader();
        },
        error => {
          this.commonLoader.hideLoader();
        }
      );
  }
  //#endregion

  //#region "Set remaining interview details"
  setRemainingInterviewDetails(data: any) {
    this.interviewDetailForm.patchValue({
      CandidateId: data.CandidateId,
      HiringRequestId: data.HiringRequestId,
      InterviewId: data.InterviewId,
      Description: data.Description,
      NoticePeriod: data.NoticePeriod,
      AvailableDate: data.AvailableDate,
      WrittenTestMarks: data.WrittenTestMarks,
      CurrentBase: data.CurrentBase,
      CurrentOther: data.CurrentOther,
      ExpectationBase: data.ExpectationBase,
      ExpectationOther: data.ExpectationOther,
      Status: data.Status,
      InterviewQuestionOne: data.InterviewQuestionOne,
      InterviewQuestionTwo: data.InterviewQuestionTwo,
      InterviewQuestionThree: data.InterviewQuestionThree,
      CurrentTransport: data.CurrentTransport,
      CurrentMeal: data.CurrentMeal,
      ExpectationTransport: data.ExpectationTransport,
      ExpectationMeal: data.ExpectationMeal,
      LanguageList: data.LanguageList,
      TraningList: data.TraningList,
      InterviewerList: data.InterviewerList,
      ProfessionalCriteriaMark: data.ProfessionalCriteriaMark,
      MarksObtain: data.MarksObtain,
      TotalMarksObtain: data.TotalMarksObtain,
      RatingBasedCriteriaList: data.RatingBasedCriteriaList,
      TechnicalQuestionList: data.TechnicalQuestionList
    });
    data.CandidateId = this.candidateId;
    data.HiringRequestId = this.hiringRequestId;
    data.CandidateName = this.candidateDetails.FullName;
    data.Qualification = this.candidateDetails.Qualification;
    data.Position = this.hiringRequestDetail.Position;
    data.DutyStation = this.hiringRequestDetail.Office;
    data.MaritalStatus = '-';
    data.PassportNumber = '-';
    data.NameOfInstitute = '-';
    data.DateOfBirth = this.candidateDetails.DateOfBirth;
    data.TechnicalQuestionList.forEach((element, i) => {
      this.technicalQuestionList[i].selected = element.Score;
    });
    data.RatingBasedCriteriaList.forEach((element, i) => {
      this.ratingBasedCriteriaQuestionList[i].selected = element.Score;
    });
    this.totalMarksObtain = data.TotalMarksObtain;
    this.marksObtain = data.MarksObtain;
    this.professionalCriteriaMarks = data.ProfessionalCriteriaMark;
    this.languagesList$ = of(data.LanguageList);
    this.traningList$ = of(
      data.TraningList.map(r => {
        return {
          TraningType: r.TraningType,
          TraningName: r.TraningName,
          TraningCountryAndCity: r.TraningCountryAndCity,
          TraningStartDate: this.datePipe.transform(
            r.TraningStartDate,
            'MMM d, y'
          ),
          TraningEndDate: this.datePipe.transform(r.TraningEndDate, 'MMM d, y')
        } as ITraningDetailModel;
      })
    );
    this.interviewerList$ = of(data.InterviewerList);
    this.interviewDetails = data;
  }
  //#endregion

  //#region "On export of interview details pdf"
  onExportInterviewDetailsPdf() {
    this.commonLoader.showLoader();
    this.globalSharedService
      .getFile(
        this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetInterviewDetailReportPdf,
        this.interviewDetails
      )
      .pipe(takeUntil(this.destroyed$))
      .subscribe();
    this.commonLoader.hideLoader();
  }
  //#endregion
  //#region "Route back to request detail page"
  backToRequestDetail() {
    window.history.back();
  }
  //#endregion
}
