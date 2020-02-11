import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { of, Observable } from 'rxjs';
import {
  EmployeeAppraisalQuestionList,
  IAppraisalMemberList,
  ITrainigDetailModel,
  IAppraisalStrongPoints,
  IAppraisalWeakPoints,
  IAppraisalDetailModel
} from 'src/app/hr/models/IGeneralProfessionalIndicatorModel';
import { EmployeeAppraisalService } from 'src/app/hr/services/employee-appraisal.service';
import { MatDialog } from '@angular/material';
import { AddAppraisalMembersComponent } from '../add-appraisal-members/add-appraisal-members.component';
import { TableActionsModel } from 'projects/library/src/lib/models/table-actions-model';
import { EmployeeTrainingComponent } from '../employee-training/employee-training.component';
import { AddStrongPointsComponent } from '../add-strong-points/add-strong-points.component';
import { AddWeakPointsComponent } from '../add-weak-points/add-weak-points.component';

@Component({
  selector: 'app-add-employee-appraisal',
  templateUrl: './add-employee-appraisal.component.html',
  styleUrls: ['./add-employee-appraisal.component.scss']
})
export class AddEmployeeAppraisalComponent implements OnInit, OnChanges {
  employeeId: number;
  employeeAppraisalPeriod: any[];
  employeeAppraisalForm: FormGroup;
  // @Input() questionSourceData: EmployeeAppraisalQuestionList[];
  questionSourceData: EmployeeAppraisalQuestionList[] = [];
  appraisalMembersList$: Observable<IAppraisalMemberList[]> = of([]);
  actions: TableActionsModel;
  employeeDetailList: any[] = [];
  trainingDetailList: any[] = [];
  strongPontsDetailList: IAppraisalStrongPoints[] = [];
  weakPontsDetailList: IAppraisalWeakPoints[] = [];

  appraisalTrainingList$: Observable<ITrainigDetailModel[]> = of([]);
  strongPointsList$: Observable<IAppraisalStrongPoints[]> = of([]);
  weakPointsList$: Observable<IAppraisalWeakPoints[]> = of([]);

  appraisalPeriodDataSource$ = of([
    { value: 1, name: 'Weak' },
    { value: 2, name: 'Satisfactory' },
    { value: 3, name: 'Average' },
    { value: 4, name: 'Good' },
    { value: 5, name: 'Excellent' }
  ]);

  appraisalQuestionScore$ = of([
    {
      value: 0,
      name: '0 - Weak'
    },
    {
      value: 1,
      name: '1 - Satisfactory'
    },
    {
      value: 2,
      name: '2 - Average'
    },
    {
      value: 3,
      name: '3 - Good'
    },
    {
      value: 4,
      name: '4 - Excellent'
    }
  ]);

  appraisalMembersHeader$ = of(['Employee Code', 'Full Name', 'Type']);

  appraisalTrainigHeader$ = of([
    'Training program based on',
    'Program',
    'Participated',
    'Catch level',
    'Refresher Trm',
    'Other Recommended Trainings'
  ]);

  appraisalStrongPointsHeader$ = of(['Strong Points']);
  appraisalWeakPointsHeader$ = of(['Weak Points']);

  hideColumsEmployeeDetail$ = of({
    headers: ['Employee Code', 'Full Name', 'Type'],
    items: ['EmployeeCode', 'EmployeeName', 'Type']
  });

  hideColumsTrainigDetails$ = of({
    headers: [
      'Training program based on',
      'Program',
      'Participated',
      'Catch level',
      'Refresher Trm',
      'Other Recommended Trainings'
    ],
    items: [
      'TrainingProgramBasedOn',
      'Program',
      'Participated',
      'CatchLevel',
      'RefresherTrm',
      'OtherRecommemenedTraining'
    ]
  });

  hideColumsWeakPoints$ = of({
    headers: ['Weak Points'],
    items: ['WeakPoints']
  });

  hideColumsStrongPoints$ = of({
    headers: ['Strong Points'],
    items: ['StrongPoints']
  });


  constructor(
    private routeActive: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private appraisalService: EmployeeAppraisalService,
    public dialog: MatDialog
  ) {
    this.getAllAppraisalQuestions();
    this.actions = {
      items: {
        button: { status: false, text: '' },
        download: false,
        edit: false,
        delete: true
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false
      }
    };
  }

  ngOnInit() {
    debugger;
    this.routeActive.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
    this.employeeAppraisalPeriod = [
      {
        PeriodId: 1,
        PeriodDuration: 'Annual'
      },
      {
        PeriodId: 2,
        PeriodDuration: 'Probationary'
      }
    ];
    this.initForm();
  }

  ngOnChanges() {
    debugger;
    console.log(this.questionSourceData);
    if (
      this.questionSourceData.length > 0 &&
      this.questionSourceData !== undefined
    ) {
      this.setAppraisalQuestion();
      // this.setAppraisalMembers();
    }
  }

  initForm() {
    this.employeeAppraisalForm = this.fb.group({
      EmployeeAppraisalDetailsId: [null],
      AppraisalPeriod: [null, Validators.required],
      CurrentAppraisalDate: [null, Validators.required],
      GeneralProfessionalIndicator: this.fb.array([]),
      AppraisalMembers: this.fb.array([]),
      AppraisalTraining: this.fb.array([]),
      AppraisalStrongPoints: this.fb.array([]),
      AppraisalWeakPoints: this.fb.array([]),
      FinalResultQues1: [null, Validators.required],
      FinalResultQues2: [null, Validators.required],
      FinalResultQues3: [null, Validators.required],
      FinalResultQues4: [null, Validators.required],
      FinalResultQues5: [null, Validators.required],
      FinalResultQues6: [null, Validators.required]
    });
  }
  // if apprasial question list is not null
  setAppraisalQuestion() {
    this.employeeAppraisalForm.setControl(
      'GeneralProfessionalIndicator',
      this.setGeneralProfessionalQuestion(this.questionSourceData)
    );
  }

  setGeneralProfessionalQuestion(
    sources: EmployeeAppraisalQuestionList[]
  ): FormArray {
    debugger;
    const formArray = new FormArray([], [Validators.required]);
    sources.forEach(s => {
      formArray.push(
        this.fb.group({
          SequenceNumber: s.SequenceNo,
          QuestionEnglish: s.QuestionEnglish,
          Score: s.Score,
          Remarks: s.Remarks
        })
      );
    });
    return formArray;
  }

  // to get appraisalGeneralQuestions sources controls html
  get appraisalGeneralQuestions(): FormArray {
    return this.employeeAppraisalForm.get(
      'GeneralProfessionalIndicator'
    ) as FormArray;
  }

  //#region "set appraisal members"
  setAppraisalMembers() {
    debugger;
    this.employeeAppraisalForm.setControl(
      'AppraisalMembers',
      this.setAppraisalMemeberList(this.appraisalMembersList$)
    );
  }

  setAppraisalMemeberList(
    sources: Observable<IAppraisalMemberList[]>
  ): FormArray {
    debugger;
    const formArray = new FormArray([], [Validators.required]);
    this.appraisalMembersList$.subscribe(res => {
      res.forEach(s => {
        formArray.push(
          this.fb.group({
            EmployeeId: s.EmployeeId,
            EmployeeCode: s.EmployeeCode,
            Type: s.Type,
            EmployeeName: s.EmployeeName
          })
        );
      });
    });

    return formArray;
  }

  //#endregion

  backClick() {
    this.router.navigate(['/hr/employee/' + this.employeeId]);
  }

  getQuestionScoreeSelectedValue(event: any) {}
  //#region "Get All Appraisal Questions"
  getAllAppraisalQuestions() {
    debugger;
    // tslint:disable-next-line:radix
    this.appraisalService.GetAppraisalQuestions().subscribe(
      data => {
        this.questionSourceData = [];
        if (
          data.StatusCode === 200 &&
          data.data.AppraisalList.length > 0 &&
          data.data.AppraisalList != null
        ) {
          data.data.AppraisalList.forEach(element => {
            this.questionSourceData.push({
              AppraisalGeneralQuestionsId: element.AppraisalGeneralQuestionsId,
              QuestionEnglish: element.Question,
              QuestionDari: element.DariQuestion,
              SequenceNo: element.SequenceNo,
              Remarks: element.Remarks,
              Score: element.Score,
              AppraisalScore: element.AppraisalScore
            });
          });
          this.setAppraisalQuestion();
        } else {
          if (
            data.data.AppraisalList.length === 0 ||
            data.data.AppraisalList == null
          ) {
            // this.toastr.warning('Appraisal Questions Not Found !');
          } else if (data.StatusCode === 400) {
            // failStatusCode
            // this.toastr.error('Something went wrong !');
          }
        }
      },
      error => {
        if (error.StatusCode === 500) {
        }
      }
    );
  }
  //#endregion

  //#region "addAppraisalMembers"
  addAppraisalMembers(): void {
    const dialogRef = this.dialog.open(AddAppraisalMembersComponent, {
      width: '500px',
      data: {}
    });
    // refresh the list after new request created

    dialogRef.componentInstance.employeeListEmit.subscribe(element => {
      dialogRef.componentInstance.isFormSubmitted = false;
      element.forEach(x => {
        this.employeeDetailList.push({
          EmployeeCode: x.EmployeeCode,
          EmployeeName: x.EmployeeName,
          Type: x.Type === undefined ? '' : x.Type,
          EmployeeId: x.EmployeeId
        });
      });
    });

    dialogRef.afterClosed().subscribe(() => {
      this.appraisalMembersList$ = of(
        this.employeeDetailList.map(y => {
          return {
            EmployeeCode: y.EmployeeCode,
            EmployeeName: y.EmployeeName,
            Type: y.Type,
            EmployeeId: y.EmployeeId
          };
        })
      );
      console.log(this.appraisalMembersList$);
      this.setAppraisalMembers();
    });
  }
  //#endregion
  // #region "Delete appraisal members employee"

  membersActionEvents(event: any) {
    if (event.type === 'delete') {
      this.deleteAppraisalMembersLog(event.item.EmployeeId);
    }
  }

  deleteAppraisalMembersLog(employeeId: number) {
    if (employeeId !== undefined && employeeId != null) {
      let index;
      this.appraisalMembersList$.subscribe(data => {
        index = data.findIndex(x => x.EmployeeId === employeeId);
        data.splice(index, 1);
        this.appraisalMembersList$ = of(data);
      });
      // Note to set the formarray value
      this.setAppraisalMembers();
    }
  }
  //#endregion

  //#region "Training"
  addTraining(): void {
    const dialogRef = this.dialog.open(EmployeeTrainingComponent, {
      width: '750px',
      data: {}
    });
    // refresh the list after new request created

    dialogRef.componentInstance.trainingformDataEmit.subscribe(element => {
      debugger;
      if (element !== undefined && element != null) {
        dialogRef.componentInstance.isFormSubmitted = false;
        this.trainingDetailList.push({
          TrainingProgramBasedOn: element.TrainingProgramBasedOn,
          Program: element.Program,
          Participated: element.Participated,
          CatchLevel: element.CatchLevel,
          RefresherTrm: element.RefresherTrm,
          OtherRecommemenedTraining: element.OtherRecommemenedTraining
        });
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      debugger;
      this.appraisalTrainingList$ = of(
        this.trainingDetailList.map(y => {
          return {
            TrainingProgramBasedOn: y.TrainingProgramBasedOn,
            Program: y.Program,
            Participated: y.Participated,
            CatchLevel: y.CatchLevel,
            RefresherTrm: y.RefresherTrm,
            OtherRecommemenedTraining: y.OtherRecommemenedTraining,
            EmployeeEvaluationTrainingId: y.EmployeeEvaluationTrainingId
          };
        })
      );
      this.setAppraisalTraining();
    });
  }
  //#endregion

  setAppraisalTraining() {
    this.employeeAppraisalForm.setControl(
      'AppraisalTraining',
      this.setAppraisalTrainigList(this.appraisalTrainingList$)
    );
  }

  setAppraisalTrainigList(
    sources: Observable<ITrainigDetailModel[]>
  ): FormArray {
    debugger;
    const formArray = new FormArray([]);
    this.appraisalTrainingList$.subscribe(res => {
      res.forEach(y => {
        formArray.push(
          this.fb.group({
            TrainingProgramBasedOn: y.TrainingProgramBasedOn,
            Program: y.Program,
            Participated: y.Participated,
            CatchLevel: y.CatchLevel,
            RefresherTrm: y.RefresherTrm,
            OtherRecommemenedTraining: y.OtherRecommemenedTraining,
            EmployeeEvaluationTrainingId: y.EmployeeEvaluationTrainingId
          })
        );
      });
    });

    return formArray;
  }

  //#region "Delete apraisal trianing"
  trainingActionEvents(event: any) {
    debugger;
    if (event.type === 'delete') {
      this.deleteAppraisalTraining(event.item);
    }
  }

  deleteAppraisalTraining(data: any) {
    debugger;
    if (data != undefined && data != null) {
      let index;
      this.appraisalTrainingList$.subscribe(res => {
        index = res.findIndex(x => x === data);
        res.splice(index, 1);
        this.appraisalTrainingList$ = of(res);
      });
      // Note to set the formarray value
      this.setAppraisalTraining();
    }
  }
  //#endregion

  //#region "addStrongPoints"
  addStrongPoints() {
    const dialogRef = this.dialog.open(AddStrongPointsComponent, {
      width: '500px',
      data: {}
    });
    // refresh the list after new request created

    dialogRef.componentInstance.strongPointDataEmit.subscribe(element => {
      debugger;
      if (element !== undefined && element != null) {
        dialogRef.componentInstance.isFormSubmitted = false;
        this.strongPontsDetailList.push({
          StrongPoints: element.StrongPoints,
          AppraisalStrongPointsId: element.AppraisalStrongPointsId
        });
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      debugger;
      this.strongPointsList$ = of(
        this.strongPontsDetailList.map(y => {
          return {
            StrongPoints: y.StrongPoints,
            AppraisalStrongPointsId: y.AppraisalStrongPointsId
          };
        })
      );
      this.setAppraisalStrongPoints();
    });
  }

  setAppraisalStrongPoints() {
    this.employeeAppraisalForm.setControl(
      'AppraisalStrongPoints',
      this.setAppraisalStrongPointsList(this.strongPointsList$)
    );
  }

  setAppraisalStrongPointsList(
    sources: Observable<IAppraisalStrongPoints[]>
  ): FormArray {
    debugger;
    const formArray = new FormArray([]);
    this.strongPointsList$.subscribe(res => {
      res.forEach(y => {
        formArray.push(
          this.fb.group({
            StrongPoints: y.StrongPoints,
            AppraisalStrongPointsId: y.AppraisalStrongPointsId
          })
        );
      });
    });

    return formArray;
  }

  strongPointsActionEvents(event: any) {
    if (event.type === 'delete') {
      this.deleteStrongPoint(event.item);
    }
  }

  deleteStrongPoint(data: any) {
    debugger;
    if (data != undefined && data != null) {
      let index;
      this.strongPointsList$.subscribe(res => {
        index = res.findIndex(x => x === data);
        res.splice(index, 1);
        this.strongPointsList$ = of(res);
      });
      // Note to set the formarray value
      this.setAppraisalStrongPoints();
    }
  }

  //#endregion

  //#region addWeakpoints
  addWeakpoints() {
    const dialogRef = this.dialog.open(AddWeakPointsComponent, {
      width: '500px',
      data: {}
    });

    // refresh the list after new request created

    dialogRef.componentInstance.weakPointDataEmit.subscribe(element => {
      debugger;
      if (element !== undefined && element != null) {
        dialogRef.componentInstance.isFormSubmitted = false;
        this.weakPontsDetailList.push({
          WeakPoints: element.WeakPoints,
          AppraisalWeakPointsId: element.AppraisalWeakPointsId
        });
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      debugger;
      this.weakPointsList$ = of(
        this.weakPontsDetailList.map(y => {
          return {
            WeakPoints: y.WeakPoints,
            AppraisalWeakPointsId: y.AppraisalWeakPointsId
          };
        })
      );
      this.setWeakPoint();
    });
  }

  setWeakPoint() {
    this.employeeAppraisalForm.setControl(
      'AppraisalWeakPoints',
      this.setAppraisalWeakPointsList(this.weakPointsList$)
    );
  }
  setAppraisalWeakPointsList(
    sources: Observable<IAppraisalWeakPoints[]>
  ): FormArray {
    debugger;
    const formArray = new FormArray([]);
    this.weakPointsList$.subscribe(res => {
      res.forEach(y => {
        formArray.push(
          this.fb.group({
            WeakPoints: y.WeakPoints,
            AppraisalWeakPointsId: y.AppraisalWeakPointsId
          })
        );
      });
    });

    return formArray;
  }

  weakPointsActionEvents(event: any) {
    if (event.type === 'delete') {
      this.deleteWeakPoint(event.item);
    }
  }

  deleteWeakPoint(data: any) {
    debugger;
    if (data != undefined && data != null) {
      let index;
      this.weakPointsList$.subscribe(res => {
        index = res.findIndex(x => x === data);
        res.splice(index, 1);
        this.weakPointsList$ = of(res);
      });
      // Note to set the formarray value
      this.setWeakPoint();
    }
  }
  //#endregion

  //#region "onFormSubmit"
  onFormSubmit(form: IAppraisalDetailModel) {
    debugger;
    if (this.employeeAppraisalForm.valid) {
      if (
        this.employeeAppraisalForm.controls['EmployeeAppraisalDetailsId']
          .value !== undefined &&
        this.employeeAppraisalForm.controls['EmployeeAppraisalDetailsId'] !=
          null
      ) {
        this.addAppraisalForm(form);
      }
    } else {
      this.editAppraisalForm(form);
      console.log('invalid');
    }
  }
  //#endregion
  //#region "addAppraisalForm"
  addAppraisalForm(form: IAppraisalDetailModel) {
    debugger;
    this.appraisalService.addAppraisalForm(form).subscribe( res => {

    });
  }
  //#endregion

  //#region "addAppraisalForm"
  editAppraisalForm(form: IAppraisalDetailModel) {}
  //#endregion
}
