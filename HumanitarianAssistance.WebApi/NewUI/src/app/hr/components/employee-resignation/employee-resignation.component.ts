import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { mergeMap, groupBy, map, reduce } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { HrService } from '../../services/hr.service';
import { QuestionTypeName } from 'src/app/shared/enum';
import { of, Observable } from 'rxjs';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { IEmployeeDetail } from '../../models/employee-detail.model';
import { EmployeeListService } from '../../services/employee-list.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employee-resignation',
  templateUrl: './employee-resignation.component.html',
  styleUrls: ['./employee-resignation.component.scss']
})
export class EmployeeResignationComponent implements OnInit, OnChanges {

  questionTypes = [
    {value: 1, name: 'Feeling About Employee Aspects'},
    {value: 2, name: 'Reason Of Leaving'},
    {value: 3, name: 'The Department'},
    {value: 4, name: 'The Job Itself'},
    {value: 5, name: 'My Supervisor'},
    {value: 6, name: 'The Management'},
  ];
  @Input() employeeDetail: IEmployeeDetail;
  exitInterviewQuestionsList: any[];
  questionByType = [];
  employeeId;
  groupedQuestions: Map<any, any>;
  resignationForm: FormGroup;
  constructor(private commonLoader: CommonLoaderService,
    private toastr: ToastrService,
    private hrService: HrService,
    private fb: FormBuilder,
    private employeeService: EmployeeListService,
    private activatedRoute: ActivatedRoute) {
      this.activatedRoute.params.subscribe(params => {
        this.employeeId = +params['id'];
      });
      this.resignationForm = this.fb.group({
        ResignDate: ['', Validators.required],
        IsIssueUnresolved: ['false'],
        Issues: [''],
        QuestionType1: this.fb.array([ this.createQuestion(null, null, null, null) ]),
        QuestionType2: this.fb.array([ this.createQuestion(null, null, null, null)]),
        QuestionType3: this.fb.array([ this.createQuestion(null, null, null, null)]),
        QuestionType4: this.fb.array([this.createQuestion(null, null, null, null) ]),
        QuestionType5: this.fb.array([this.createQuestion(null, null, null, null) ]),
        QuestionType6: this.fb.array([this.createQuestion(null, null, null, null) ]),
      });
    }

  ngOnInit() {
    this.getExitInterviewQuestionsList();
  }

  ngOnChanges() {
    // console.log(this.employeeDetail);
  }

  createQuestion(QuestionId, QuestionText, QuestionTypeId, Answer): FormGroup {
    return this.fb.group({
      QuestionId: [QuestionId],
      QuestionText: [QuestionText],
      QuestionTypeId: [QuestionTypeId],
      Answer: [Answer]
    });
  }

  getExitInterviewQuestionsList() {
    const pageModel = {
      PageSize: 0,
      PageIndex: 0,
      IsPaginated: false
    };
    this.hrService.getExitInterviewQuestionsList(pageModel).subscribe(x => {
      if (x.Result.length > 0) {
        this.exitInterviewQuestionsList = [];
        this.exitInterviewQuestionsList = x.Result.map(element => {
          return {
            Id: element.Id,
            QuestionText: element.QuestionText,
            QuestionType: element.QuestionType,
            QuestionTypeText: QuestionTypeName.get(element.QuestionType),
            SequencePosition: element.SequencePosition,
          };
        });
        this.groupedQuestions = StaticUtilities.groupBy(this.exitInterviewQuestionsList, y => y.QuestionType);
        this.groupedQuestions.forEach((value: any, key: any) => {
          this.questionByType[key] = value;
        });
        this.initializeAllQuestionByType();
      }
    }, error => {
      this.toastr.warning(error);
    });
  }

  initializeAllQuestionByType() {
    for (let i = 1; i <= 6 ; i++) {
      (<FormArray>this.resignationForm.get('QuestionType' + i)).removeAt(0);
      (this.resignationForm.controls['QuestionType' + i] as FormArray).setValue([]);
      this.questionByType[i].forEach(e => {
        if (i !== 2 ) {
          (this.resignationForm.controls['QuestionType' + i] as FormArray)
        .push(this.createQuestion(e.Id, e.QuestionText, e.QuestionType, '2'));
        } else {
          (this.resignationForm.controls['QuestionType' + i] as FormArray)
          .push(this.createQuestion(e.Id, e.QuestionText, e.QuestionType, false));
        }
      });
    }
  }

  saveResignationForm(value) {
    if (!this.resignationForm.valid) {
      this.toastr.warning('Please select Resign Date');
      return;
    }
    if (value.IsIssueUnresolved === 'true' && value.Issues === '') {
      this.toastr.warning('Please enter Comments & Issues!');
      return;
    }
    value.QuestionType2.forEach(element => {
      element.Answer = (element.Answer) ? '1' : '0';
    });
    const model = {
      ResignDate: StaticUtilities.getLocalDate(this.resignationForm.get('ResignDate').value),
      EmployeeID: this.employeeId,
      IsUnresolvedIssue: (value.IsIssueUnresolved === 'true'),
      CommentsIssues: value.Issues,
      QuestionType1: value.QuestionType1,
      QuestionType2: value.QuestionType2,
      QuestionType3: value.QuestionType3,
      QuestionType4: value.QuestionType4,
      QuestionType5: value.QuestionType5,
      QuestionType6: value.QuestionType6,
    };
    this.employeeService.saveResignation(model).subscribe(res => {
      if (res) {
        this.toastr.success('Resignation saved successfully!');
      }
    }, err =>  {
      this.toastr.warning(err);
    });
  }

  addResignation() {
    if (this.employeeDetail.IsResigned) {
      return;
    }
    this.employeeService.addResignation(this.employeeId).subscribe(res => {
      if (res) {
        this.employeeDetail.IsResigned = true;
      }
    }, err => {
      this.toastr.warning(err);
    });
  }

  // onCheckBoxChange(e, QuestionId) {
  //   if (e.checked) {
  //     (this.resignationForm.controls.QuestionType2 as FormArray)
  //   }
  // }
}
