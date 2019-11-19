import { Component, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ActivatedRoute } from '@angular/router';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { ToastrService } from 'ngx-toastr';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { of, Observable } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';

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
    'Speaking',
  ]);
  traningHeaders$ = of([
    'Traning Id',
    'Traning Type',
    'Name',
    'Country/City',
    'Start Date',
    'End Date',
  ]);
  interviewersHeaders$ = of([
    'Employee Id',
    'Employee Code',
    'Full Name',
  ]);
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  interviewDetailForm: FormGroup;
  noticePeriodList$: Observable<IDropDownModel[]>;
  statusList$: Observable<IDropDownModel[]>;
  languagesList$: Observable<any[]>;
  traningList$: Observable<any[]>;
  interviewersList$: Observable<any[]>;
  ratingBasedCriteriaQuestionList: any[] = [];
  ratingBasedDropDown: any[];
  constructor(
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
      { name: 'Other', value: 3 },
    ] as IDropDownModel[]);
  }

  ngOnInit() {
    this.getScreenSize();
    this.getRatingBasedCriteriaQuestion();
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
  onFormSubmit(data: any) {
    console.log(data);
  }
}
