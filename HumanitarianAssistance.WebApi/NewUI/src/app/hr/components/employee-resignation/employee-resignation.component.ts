import { Component, OnInit } from '@angular/core';
import { mergeMap, groupBy, map, reduce } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { HrService } from '../../services/hr.service';
import { QuestionTypeName } from 'src/app/shared/enum';
import { of, Observable } from 'rxjs';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';

@Component({
  selector: 'app-employee-resignation',
  templateUrl: './employee-resignation.component.html',
  styleUrls: ['./employee-resignation.component.scss']
})
export class EmployeeResignationComponent implements OnInit {

  questionTypes = [
    {value: 1, name: 'Feeling About Employee Aspects'},
    {value: 2, name: 'Reason Of Leaving'},
    {value: 3, name: 'The Department'},
    {value: 4, name: 'The Job Itself'},
    {value: 5, name: 'My Supervisor'},
    {value: 6, name: 'The Management'},
  ];
  exitInterviewQuestionsList: any[];
  questionByType = [];
  groupedQuestions: Map<any, any>;
  resignationForm: FormGroup;
  constructor(private commonLoader: CommonLoaderService,
    private toastr: ToastrService,
    private hrService: HrService,
    private fb: FormBuilder) {
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
    console.log(value);
  }
}
