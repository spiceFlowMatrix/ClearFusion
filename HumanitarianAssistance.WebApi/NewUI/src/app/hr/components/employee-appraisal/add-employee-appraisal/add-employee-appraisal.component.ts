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
import {
  AppraisalTrainingProgram,
  AppraisalYesNoType,
  AppraisalCatchLevelType
} from 'src/app/shared/enum';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-add-employee-appraisal',
  templateUrl: './add-employee-appraisal.component.html',
  styleUrls: ['./add-employee-appraisal.component.scss']
})
export class AddEmployeeAppraisalComponent implements OnInit, OnChanges {
  isViewed = true;
  err = null;
  errMesg = null;
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
  employeeAppraisalId: number;
  appraisalList: IAppraisalDetailModel[] = [];
  score = 0;

  appraisalPeriodDataSource$ = of([
    { value: 1, name: 'Annual' },
    { value: 2, name: 'Probationary' }
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
      'TrainingProgramBasedOnName',
      'Program',
      'ParticipatedName',
      'CatchLevelName',
      'RefresherTrmName',
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
    public dialog: MatDialog,
    private loader: CommonLoaderService
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
    this.initForm();
    this.routeActive.url.subscribe(params => {
      this.employeeId = +params[1].path;
      console.log(params);
      if (params.length > 3) {
        this.employeeAppraisalId = +params[3].path;
        console.log(this.employeeAppraisalId);
      }
      this.getAllAppraisalList();
    });

    this.routeActive.queryParams.subscribe(p => {
      console.log(p);
      if (p['isViewed'] === 'false') {
        this.isViewed = false;
      }
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
    // if (
    //   this.employeeAppraisalId == undefined &&
    //   this.employeeAppraisalId == null
    // ) {

    // }
  }

  ngOnChanges() {
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
      GeneralProfessionalIndicatorQuestion: new FormArray([
        this.initAppraisalQuestion()
      ]),
      AppraisalMembers: new FormArray([], Validators.required),
      AppraisalTraining: new FormArray([], Validators.required),
      AppraisalStrongPoints: new FormArray([], Validators.required),
      AppraisalWeakPoints: new FormArray([], Validators.required),
      FinalResultQues1: [null, Validators.required],
      FinalResultQues2: [null, Validators.required],
      FinalResultQues3: [null, Validators.required],
      FinalResultQues4: [null, Validators.required],
      FinalResultQues5: [null, Validators.required],
      EmployeeEvaluationId: [null],
      CommentsByEmployee: [null, Validators.required],
      EmployeeId: this.employeeId,
      AppraisalScore: [null]
    });
  }
  initAppraisalQuestion() {
    return this.fb.group({
      AppraisalGeneralQuestionsId: ['', Validators.required],
      SequenceNumber: ['', Validators.required],
      QuestionEnglish: ['', Validators.required],
      Score: ['', Validators.required],
      Remarks: ['', Validators.required]
    });
  }

  //#region "get appraisal list"
  getAllAppraisalList() {
    this.loader.showLoader();
    if (this.employeeId !== undefined && this.employeeId != null) {
      this.appraisalService
        .getAllAppraisalListEmployeeId(this.employeeId)
        .subscribe(
          res => {
            if (
              res &&
              res.AppraisalList !== undefined &&
              res.AppraisalList.length > 0
            ) {
              this.appraisalList = res.AppraisalList;
              const filteredRecord = res.AppraisalList.filter(
                x => x.EmployeeAppraisalDetailsId == this.employeeAppraisalId
              );
              this.setEmployeeAppraisalDetail(
                this.employeeAppraisalId,
                filteredRecord
              );
            }
            this.loader.hideLoader();
          },
          err => {
            this.loader.hideLoader();
          }
        );
    }
  }
  //#endregion
  setEmployeeAppraisalDetail(id: number, filterData: any) {
    if (id != undefined && id != null) {
      filterData.forEach(element => {
        this.employeeAppraisalForm = this.fb.group({
          EmployeeAppraisalDetailsId: [element.EmployeeAppraisalDetailsId],
          AppraisalPeriod: [element.AppraisalPeriod],
          CurrentAppraisalDate: [element.CurrentAppraisalDate],

          FinalResultQues1: [element.FinalResultQues1],
          FinalResultQues2: [element.FinalResultQues2],
          FinalResultQues3: [element.FinalResultQues3],
          FinalResultQues4: [element.FinalResultQues4],
          FinalResultQues5: [element.FinalResultQues5],
          CommentsByEmployee: [element.CommentsByEmployee],
          EmployeeEvaluationId: [element.EmployeeEvaluationId],
          EmployeeId: this.employeeId,
          AppraisalScore: element.AppraisalScore
        });
        this.setProfessionalIndicatorQuestion(
          element.GeneralProfessionalIndicatorQuestion
        );
        this.setAppraisalMembersList(element.AppraisalMembers);
        this.setAppraisalTrainingList(element.AppraisalTraining);
        this.setAppraisalStrongPointList(element.AppraisalStrongPoints);
        this.setAppraisalWeakPointList(element.AppraisalWeakPoints);

        this.employeeDetailList = element.AppraisalMembers;
        this.appraisalMembersList$ = of(
          this.employeeDetailList.map(y => {
            return {
              EmployeeCode: y.EmployeeCode,
              EmployeeName: y.EmployeeName,
              Type: y.Type,
              EmployeeId: y.EmployeeId,
              EmployeeAppraisalTeamMemberId: y.EmployeeAppraisalTeamMemberId
            };
          })
        );

        // this.appraisalTrainingList$ = element.AppraisalTraining;
        this.trainingDetailList = element.AppraisalTraining;
        this.appraisalTrainingList$ = of(
          this.trainingDetailList.map(y => {
            return {
              TrainingProgramBasedOn: y.TrainingProgramBasedOn,
              TrainingProgramBasedOnName:
                AppraisalTrainingProgram[y.TrainingProgramBasedOn],
              Program: y.Program,
              Participated: y.Participated,
              ParticipatedName: AppraisalYesNoType[y.Participated],
              CatchLevel: y.CatchLevel,
              CatchLevelName: AppraisalCatchLevelType[y.CatchLevel],
              RefresherTrm: y.RefresherTrm,
              RefresherTrmName: AppraisalYesNoType[y.RefresherTrm],
              OtherRecommemenedTraining: y.OtherRecommemenedTraining,
              EmployeeEvaluationTrainingId: y.EmployeeEvaluationTrainingId
            };
          })
        );
        this.strongPontsDetailList = element.AppraisalStrongPoints;
        this.strongPointsList$ = of(
          this.strongPontsDetailList.map(y => {
            return {
              StrongPoints: y.StrongPoints,
              AppraisalStrongPointsId: y.AppraisalStrongPointsId
            };
          })
        );
        this.weakPontsDetailList = element.AppraisalWeakPoints;
        this.weakPointsList$ = of(
          this.weakPontsDetailList.map(y => {
            return {
              WeakPoints: y.WeakPoints,
              AppraisalWeakPointsId: y.AppraisalWeakPointsId
            };
          })
        );
      });

      this.score = this.employeeAppraisalForm.get('AppraisalScore').value;
    }
  }

  setProfessionalIndicatorQuestion(item: any[]) {
    const formArray = new FormArray([], Validators.required);
    for (const x of item) {
      formArray.push(
        this.fb.group({
          AppraisalGeneralQuestionsId: x.AppraisalGeneralQuestionsId,
          SequenceNumber: x.SequenceNumber,
          QuestionEnglish: x.QuestionEnglish,
          Score: x.Score,
          Remarks: x.Remarks,
          EmployeeAppraisalQuestionsId: x.EmployeeAppraisalQuestionsId
        })
      );
    }
    this.employeeAppraisalForm.setControl(
      'GeneralProfessionalIndicatorQuestion',
      formArray
    );
  }

  setAppraisalMembersList(item: any[]) {
    const formArray = new FormArray([]);
    for (const x of item) {
      formArray.push(
        this.fb.group({
          EmployeeId: x.EmployeeId,
          EmployeeCode: x.EmployeeCode,
          Type: x.Type,
          EmployeeName: x.EmployeeName
        })
      );
    }
    this.employeeAppraisalForm.setControl('AppraisalMembers', formArray);
  }

  setAppraisalTrainingList(item: any[]) {
    const formArray = new FormArray([]);
    for (const y of item) {
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
    }
    this.employeeAppraisalForm.setControl('AppraisalTraining', formArray);
  }

  setAppraisalStrongPointList(item: any[]) {
    const formArray = new FormArray([]);
    for (const y of item) {
      formArray.push(
        this.fb.group({
          StrongPoints: y.StrongPoints,
          AppraisalStrongPointsId: y.AppraisalStrongPointsId
        })
      );
    }
    this.employeeAppraisalForm.setControl('AppraisalStrongPoints', formArray);
  }

  setAppraisalWeakPointList(item: any[]) {
    const formArray = new FormArray([]);
    for (const y of item) {
      formArray.push(
        this.fb.group({
          WeakPoints: y.WeakPoints,
          AppraisalWeakPointsId: y.AppraisalWeakPointsId
        })
      );
    }
    this.employeeAppraisalForm.setControl('AppraisalWeakPoints', formArray);
  }

  // if apprasial question list is not null
  setAppraisalQuestion() {
    this.employeeAppraisalForm.setControl(
      'GeneralProfessionalIndicatorQuestion',
      this.setGeneralProfessionalQuestion(this.questionSourceData)
    );
  }
  // Add
  setGeneralProfessionalQuestion(
    sources: EmployeeAppraisalQuestionList[]
  ): FormArray {
    const formArray = new FormArray([], Validators.required);
    // const formArray =  this.fb.array([this.fb.control('', Validators.required)])

    sources.forEach(s => {
      formArray.push(
        this.fb.group({
          AppraisalGeneralQuestionsId: [
            s.AppraisalGeneralQuestionsId,
            Validators.required
          ],
          SequenceNumber: [s.SequenceNo, Validators.required],
          QuestionEnglish: [s.QuestionEnglish, Validators.required],
          Score: [s.Score, Validators.required],
          Remarks: [s.Remarks, Validators.required]
        })
      );
    });
    return formArray;
  }

  // to get appraisalGeneralQuestions sources controls html
  get appraisalGeneralQuestions(): FormArray {
    return this.employeeAppraisalForm.get(
      'GeneralProfessionalIndicatorQuestion'
    ) as FormArray;
  }

  get appraisalQuestionScore() {
    return this.score;
  }
  //#region "set appraisal members"
  setAppraisalMembers() {
    this.employeeAppraisalForm.setControl(
      'AppraisalMembers',
      this.setAppraisalMemeberList(this.appraisalMembersList$)
    );
  }

  setAppraisalMemeberList(
    sources: Observable<IAppraisalMemberList[]>
  ): FormArray {
    const formArray = new FormArray([], [Validators.required]);
    this.appraisalMembersList$.subscribe(res => {
      res.forEach(s => {
        formArray.push(
          this.fb.group({
            EmployeeAppraisalTeamMemberId: s.EmployeeAppraisalTeamMemberId,
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
    if (
      this.employeeAppraisalId === undefined ||
      this.employeeAppraisalId == null
    ) {
      this.router.navigate(['../'], {
        relativeTo: this.routeActive,
        queryParams: { tabId: 8 }
      });
    } else {
      this.router.navigate(['../../'], {
        relativeTo: this.routeActive,
        queryParams: { tabId: 8 }
      });
    }

    // this.router.navigate(['/hr/employee/' + this.employeeId]);
    // this.router.navigate(['/hr/employee/' + this.employeeId + '/employeeAppraisal']);
  }

  getQuestionScoreeSelectedValue(event: any) {
    console.log(event);
    this.score = this.score + event;
  }
  //#region "Get All Appraisal Questions"
  getAllAppraisalQuestions() {
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
              AppraisalScore: element.AppraisalScore,
              EmployeeAppraisalQuestionsId: element.EmployeeAppraisalQuestionsId
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
            EmployeeId: y.EmployeeId,
            EmployeeAppraisalTeamMemberId: y.EmployeeAppraisalTeamMemberId
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
      const findIndex = this.employeeDetailList.findIndex(
        x => x.EmployeeId === employeeId
      );
      if (findIndex !== -1) {
        this.employeeDetailList.splice(findIndex, 1);
      }
      // note : to remove
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
      if (element !== undefined && element != null) {
        dialogRef.componentInstance.isFormSubmitted = false;
        this.trainingDetailList.push({
          TrainingProgramBasedOn: element.TrainingProgramBasedOn,
          TrainingProgramBasedOnName:
            AppraisalTrainingProgram[element.TrainingProgramBasedOn],
          Program: element.Program,
          Participated: element.Participated,
          ParticipatedName: AppraisalYesNoType[element.Participated],
          CatchLevel: element.CatchLevel,
          CatchLevelName: AppraisalCatchLevelType[element.CatchLevel],
          RefresherTrm: element.RefresherTrm,
          RefresherTrmName: AppraisalYesNoType[element.RefresherTrm],
          OtherRecommemenedTraining: element.OtherRecommemenedTraining,
          EmployeeEvaluationTrainingId: element.EmployeeEvaluationTrainingId

          // TrainingProgramBasedOn: y.TrainingProgramBasedOn,
          // TrainingProgramBasedOnName: AppraisalTrainingProgram[y.TrainingProgramBasedOn],
          // Program: y.Program,
          // Participated: y.Participated,
          // ParticipatedName: AppraisalYesNoType[y.Participated],
          // CatchLevel: y.CatchLevel,
          // CatchLevelName: AppraisalCatchLevelType[y.CatchLevel],
          // RefresherTrm: y.RefresherTrm,
          // RefresherTrmName: AppraisalYesNoType[y.RefresherTrm],
          // OtherRecommemenedTraining: y.OtherRecommemenedTraining,
          // EmployeeEvaluationTrainingId: y.EmployeeEvaluationTrainingId
        });
      }
    });

    dialogRef.afterClosed().subscribe(() => {
      this.appraisalTrainingList$ = of(
        this.trainingDetailList.map(y => {
          return {
            TrainingProgramBasedOn: y.TrainingProgramBasedOn,
            TrainingProgramBasedOnName:
              AppraisalTrainingProgram[y.TrainingProgramBasedOn],
            Program: y.Program,
            Participated: y.Participated,
            ParticipatedName: AppraisalYesNoType[y.Participated],
            CatchLevel: y.CatchLevel,
            CatchLevelName: AppraisalCatchLevelType[y.CatchLevel],
            RefresherTrm: y.RefresherTrm,
            RefresherTrmName: AppraisalYesNoType[y.RefresherTrm],
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
    if (event.type === 'delete') {
      this.deleteAppraisalTraining(event.item);
    }
  }

  deleteAppraisalTraining(data: any) {
    if (data != undefined && data != null) {
      let index;
      const findIndex = this.trainingDetailList.findIndex(
        x => x.Program == data.Program
      );
      if (findIndex !== -1) {
        this.trainingDetailList.splice(findIndex, 1);
      }
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
      if (element !== undefined && element != null) {
        dialogRef.componentInstance.isFormSubmitted = false;
        this.strongPontsDetailList.push({
          StrongPoints: element.StrongPoints,
          AppraisalStrongPointsId: element.AppraisalStrongPointsId
        });
      }
    });

    dialogRef.afterClosed().subscribe(() => {
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
    if (data != undefined && data != null) {
      let index;
      const findIndex = this.strongPontsDetailList.findIndex(
        x => x.StrongPoints === data.StrongPoints
      );
      if (findIndex !== -1) {
        this.strongPontsDetailList.splice(findIndex, 1);
      }
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
      if (element !== undefined && element != null) {
        dialogRef.componentInstance.isFormSubmitted = false;
        this.weakPontsDetailList.push({
          WeakPoints: element.WeakPoints,
          AppraisalWeakPointsId: element.AppraisalWeakPointsId
        });
      }
    });

    dialogRef.afterClosed().subscribe(() => {
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
    if (data != undefined && data != null) {
      let index;
      const findIndex = this.weakPontsDetailList.findIndex(
        x => x.WeakPoints === data.WeakPoints
      );
      if (findIndex !== -1) {
        this.weakPontsDetailList.splice(findIndex, 1);
      }
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
    this.err = null;
    if (this.employeeAppraisalForm.valid) {
      if (this.employeeAppraisalForm.valid) {
        if (
          this.employeeAppraisalForm.get('EmployeeAppraisalDetailsId').value ==
            undefined &&
          this.employeeAppraisalForm.get('EmployeeAppraisalDetailsId').value ==
            null
        ) {
          if (
            this.employeeAppraisalForm.controls['EmployeeAppraisalDetailsId']
              .value !== undefined &&
            this.employeeAppraisalForm.controls['EmployeeAppraisalDetailsId'] !=
              null
          ) {
            form.CurrentAppraisalDate = StaticUtilities.setLocalDate(
              form.CurrentAppraisalDate
            );
            form.EmployeeId = this.employeeId;
            this.addAppraisalForm(form);
          }
        } else {
          this.editAppraisalForm(form);
        }
      }
    } else {
      this.err = 'Please fill entire form';
      document.getElementById('errorId').scrollIntoView();
      return;
    }
  }

  //#endregion
  //#region "addAppraisalForm"
  addAppraisalForm(form: IAppraisalDetailModel) {
    this.errMesg = null;
    this.appraisalService.addAppraisalForm(form).subscribe(
      res => {
        if (res === true) {
          this.router.navigate(['../'], {
            relativeTo: this.routeActive,
            queryParams: { tabId: 8 }
          });
        }
      },
      err => {
        this.errMesg = err;
        document.getElementById('alreadyExistId').scrollIntoView();
        return;
      }
    );
  }
  //#endregion

  //#region "editAppraisalForm"
  editAppraisalForm(form: IAppraisalDetailModel) {
    this.errMesg = null;
    this.appraisalService.editAppraisalForm(form).subscribe(
      res => {
        this.router.navigate(['../../'], {
          relativeTo: this.routeActive,
          queryParams: { tabId: 8 }
        });
      },
      err => {
        this.errMesg = err;
        document.getElementById('alreadyExistId').scrollIntoView();
        return;
      }
    );
  }
  //#endregion
}
