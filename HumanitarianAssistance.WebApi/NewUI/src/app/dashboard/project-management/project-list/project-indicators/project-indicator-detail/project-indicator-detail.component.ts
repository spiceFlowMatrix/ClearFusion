import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  HostListener,
  OnChanges
} from '@angular/core';

import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ProjectListService } from 'src/app/dashboard/project-management/project-list/service/project-list.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MatDialog } from '@angular/material/dialog';
import { GLOBAL } from 'src/app/shared/global';
import { ToastrService } from 'ngx-toastr';
import {
  ProjectIndicatorModel,
  IndicatorDetailModel,
  IQuestionDetailModel
} from '../project-indicators-model';
import { AddProjectIndicatorComponent } from '../add-project-indicator/add-project-indicator.component';
import { AddQuestionsDialogComponent } from '../add-questions-dialog/add-questions-dialog.component';
import { ProjectIndicatorService } from '../project-indicator.service';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';

@Component({
  selector: 'app-project-indicator-detail',
  templateUrl: './project-indicator-detail.component.html',
  styleUrls: ['./project-indicator-detail.component.scss']
})
export class ProjectIndicatorDetailComponent implements OnInit, OnChanges {
  @Output() indicatorListRefresh = new EventEmitter();
  @Input() ProjectindicatorDetail: any;
  projectId: number;

  // screen
  scrollStyles: any;
  screenHeight: any;
  screenWidth: any;

  // boolean flag
  EditLoaderFlag = false;
  NewIndicatorLoaderFlag = false;

  // Input/Output properties
  @Input() indicatorId: number;
  @Output() addIndicator = new EventEmitter<any>();
  @Output() editIndicator = new EventEmitter<any>();

  totalCount: number;
  //#endregion

  public indicatorForm: FormGroup;
  public questionForm: FormGroup;
  questionDetailModel: IQuestionDetailModel;
  indicatorQuestionList: IQuestionDetailModel[];
  constructor(
    public router: Router,
    public dialog: MatDialog,
    private fb: FormBuilder,
    public projectListService: ProjectListService,
    private appurl: AppUrlService,
    public toastr: ToastrService,
    private routeActive: ActivatedRoute,
    public indicatorService: ProjectIndicatorService

  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.initializeModel();
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.initQuestionModel();
    }

  ngOnChanges() {
    debugger;
  if (
    this.ProjectindicatorDetail != null &&
    this.ProjectindicatorDetail !== 0 &&
    this.ProjectindicatorDetail !== undefined
  ) {
  this.setIndicatorFormValue();
  this.GetIndicatorQuestionDetailById(this.ProjectindicatorDetail.ProjectIndicatorId);
  }

}


initQuestionModel() {
  this.questionForm = this.fb.group({
    IndicatorQuestion: ['', Validators.required],
    IndicatorQuestionId: null,
    ProjectIndicatorId: null,
    QuestionType: ['', Validators.required],
    VerificationSources: this.fb.array([])
  });

  this.questionDetailModel = {
    IndicatorQuestionId: null,
    IndicatorQuestion: null,
    QuestionType: null,
    ProjectIndicatorId: null,
    QuestionTypeName: null,
    VerificationSources: []
  };
}

// set popup value
setIndicatorFormValue() {
  this.indicatorForm = this.fb.group({
    IndicatorName: this.ProjectindicatorDetail.IndicatorName,
    Description : this.ProjectindicatorDetail.Description,
    ProjectIndicatorId: this.ProjectindicatorDetail.ProjectIndicatorId
  });
}
  get questions(): FormArray {
    return this.indicatorForm.get('questions') as FormArray;
  }

  initializeModel() {
    this.indicatorForm = this.fb.group({
      ProjectIndicatorName: ['', Validators.required],
      Description: ['', Validators.required]
    });
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

  //#region  method to reset the form values for  both add and edit
  formReset() {
    // this.initializeModel();
  }

  createItem(): FormGroup {
    return this.fb.group({
      questionid: 0,
      question: ''
    });
  }

  addItem(): void {
    this.questions.push(this.createItem());
  }

  public OnSubmit(formValue: any) {
    this.EditLoaderFlag= true;

    if (formValue.ProjectIndicatorName != null && formValue.ProjectIndicatorName != '') {
      const model: IndicatorDetailModel = {
        indicatorId: this.indicatorId,
        indicatorName: formValue.ProjectIndicatorName,
        indicatorQuestions: []
      };

      formValue.questions.forEach(element => {
        model.indicatorQuestions.push({
          questionId: element.questionid,
          questiontext: element.question
        });
      });

      this.projectListService
        .EditProjectIndicatorQuestions(
          this.appurl.getApiUrl() + GLOBAL.API_Project_EditProjectIndicator,
          model
        )
        .subscribe(
          response => {
            if (
              response.StatusCode == 200 &&
              response.data.ProjectIndicator != null
            ) {
              this.EditLoaderFlag = false;
              this.toastr.success('Project Indicator Updated Successfully');
            }
          },
          error => {
            this.EditLoaderFlag = false;
            this.toastr.success('Something went wrong');
          }
        );
    } else {
      this.EditLoaderFlag = false;
    }
  }

  // ngOnChanges(): void {
  //   if (this.indicatorId !== 0 && this.indicatorId !== undefined) {
  //     this.GetProjectIndicatorDetailById(this.indicatorId);
  //   }
  // }

  GetIndicatorQuestionDetailById(id: number) {
    this.NewIndicatorLoaderFlag = true;
    if (id != 0 && id != undefined && id != null) {
      this.indicatorService
        .GetIndicatorQuestionById(
          id
        )
        .subscribe(
          response => {
            this.indicatorQuestionList = [];
            if (response.statusCode === 200) {
              debugger;
              response.data.forEach(element => {
                this.indicatorQuestionList.push(element);
                });
              }
            if (response.statusCode === 400) {
              this.toastr.error(response.message);
              this.EditLoaderFlag = false;
            }
            this.EditLoaderFlag = false;
          },
          error => {
            this.NewIndicatorLoaderFlag = false;
            this.EditLoaderFlag = false;
            this.toastr.error('Something went wrong');
          }
         );
     }
  }

  onDelete(index: number) {
    const control = <FormArray>this.indicatorForm.controls['questions'];
    control.removeAt(index);
  }

  onIndicatorEditClick() {
    this.openIndicatorDialog();
  }

  //#region "openHiringRequestDialog"
  openIndicatorDialog(): void {
    debugger;
    // NOTE: It passed the data into the Add Activity Model
    const dialogRef = this.dialog.open(AddProjectIndicatorComponent, {
      width: '550px',
      autoFocus: false,
      data: {
        ProjectId: this.projectId,
        ProjectindicatorDetail: this.indicatorForm.value
      }
    });

    // refresh the list after new request created
    dialogRef.componentInstance.onIndicatorListRefresh.subscribe(
      (data: any) => {
        this.OnQuestionListRefresh(data);
        this.indicatorForm = this.fb.group({
          IndicatorName: [data.IndicatorName],
          Description: [data.Description],
          ProjectIndicatorId: [data.ProjectIndicatorId]
        });
      }
    );

    dialogRef.afterClosed().subscribe(result => {});
  }
  //#endregion

// #region "Question"
// onQuestionsClick
onQuestionsClick(){
  this.openQuestionDialog();
}

//#region "openHiringRequestDialog"
openQuestionDialog(): void {
  debugger;
  // NOTE: It passed the data into the Add Activity Model
  const dialogRef = this.dialog.open(AddQuestionsDialogComponent, {
    width: '550px',
    autoFocus: false,
    data: {
      ProjectId: this.projectId,
      ProjectindicatorDetail: this.indicatorForm.value
    }
  });

 // refresh the list after new question created
  dialogRef.componentInstance.onAddQuestionListRefresh.subscribe(
    (data: any) => {
      this.OnQuestionListRefresh(data);
    }
  );

  dialogRef.afterClosed().subscribe(result => {});
}
//#endregion

//#region "Listupdate After update"
OnQuestionListRefresh(event: IQuestionDetailModel) {
  debugger
  this.indicatorQuestionList.unshift(event);
}
//#endregion

//#region "onDelete"
onDeleteQuestion(item: any){
  const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
    width: '300px',
    height: '250px',
    data: 'delete',
    disableClose: false
  });

  dialogRef.componentInstance.confirmMessage =
    Delete_Confirmation_Texts.deleteText1;

  dialogRef.componentInstance.confirmText =
    Delete_Confirmation_Texts.yesText;

  dialogRef.componentInstance.cancelText =
    Delete_Confirmation_Texts.noText;

  dialogRef.afterClosed().subscribe(result => {
  });
  dialogRef.componentInstance.confirmDelete.subscribe(res => {
    dialogRef.componentInstance.isLoading = true;
    if (
      item.IndicatorQuestionId != null &&
      item.IndicatorQuestionId !== undefined &&
      item.IndicatorQuestionId !== 0
    ) {
      this.indicatorService
        .DeleteQuestionDetail(
          item.IndicatorQuestionId
        )
        .subscribe(response => {
          // if (response.StatusCode === 200) {
          //   this.totalcount = response.data.TotalCount;
          //   this.deleteDonor.emit({ id: this.donorId, count: this.totalcount });
          //   dialogRef.componentInstance.onCancelPopup();
          //   this.toastr.success('Donor detail deleted successfully');

          // }
          dialogRef.componentInstance.isLoading = false;
        },
      error => {
        this.toastr.error('Someting went wrong');
        dialogRef.componentInstance.isLoading = false;
      });
    }
  });
}
//#endregion














//#endregion


}
