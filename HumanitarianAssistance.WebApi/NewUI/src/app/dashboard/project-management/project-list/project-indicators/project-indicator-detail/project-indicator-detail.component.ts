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
  IndicatorDetailModel
} from '../project-indicators-model';
import { AddProjectIndicatorComponent } from '../add-project-indicator/add-project-indicator.component';

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
  constructor(
    public router: Router,
    public dialog: MatDialog,
    private fb: FormBuilder,
    public projectListService: ProjectListService,
    private appurl: AppUrlService,
    public toastr: ToastrService,
    private routeActive: ActivatedRoute
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.initializeModel();
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
  }

  ngOnChanges() {
  if (
    this.ProjectindicatorDetail != null &&
    this.ProjectindicatorDetail !== 0 &&
    this.ProjectindicatorDetail !== undefined
  ) {
  this.setIndicatorFormValue();
  }
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
    debugger;
    this.EditLoaderFlag = true;

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
              // this.indicatorDetail.projectIndicatorCode =
              //   response.data.ProjectIndicator.IndicatorCode;
              // this.indicatorDetail.projectIndicatorId =
              //   response.data.ProjectIndicator.ProjectIndicatorId;
              // this.indicatorDetail.projectIndicatorName =
              //   response.data.ProjectIndicator.IndicatorName;
              // this.editIndicator.emit(this.indicatorDetail);
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

  GetProjectIndicatorDetailById(id) {
    this.NewIndicatorLoaderFlag = true;

    if (id != 0 && id != undefined && id != null) {
      this.projectListService
        .GetProjectIndicatorById(
          this.appurl.getApiUrl() +
            GLOBAL.API_Project_GetProjectIndicatorDetailById,
          id
        )
        .subscribe(
          response => {
            if (response.StatusCode == 200) {
              this.initializeModel();
              const control = <FormArray>this.indicatorForm.controls.questions;
              response.data.IndicatorModel.IndicatorQuestions.forEach(x => {
                control.push(
                  this.fb.group({
                    questionid: x.QuestionId,
                    question: x.QuestionText
                  })
                );
              });

              this.indicatorForm.patchValue({
                indicatorName: response.data.IndicatorModel.IndicatorName,
                id: response.data.IndicatorModel.IndicatorId
              });
              this.NewIndicatorLoaderFlag = false;
              this.EditLoaderFlag = false;
            }
            if (response.StatusCode == 400) {
              this.toastr.error(response.Message);
              this.EditLoaderFlag = false;
            }
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
    dialogRef.componentInstance.onUpdateIndicatorListRefresh.subscribe(
      (data: any) => {
        this.indicatorListRefresh.emit(data);
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
}
