import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  HostListener
} from '@angular/core';
import {
  IndicatorDetailModel,
  ProjectIndicatorModel
} from 'src/app/dashboard/project-management/project-indicators/project-indicators-model';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ProjectListService } from 'src/app/dashboard/project-management/project-list/service/project-list.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MatDialog } from '@angular/material/dialog';
import { GLOBAL } from 'src/app/shared/global';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-project-indicator-detail',
  templateUrl: './project-indicator-detail.component.html',
  styleUrls: ['./project-indicator-detail.component.scss']
})
export class ProjectIndicatorDetailComponent implements OnInit {

  constructor(
    public router: Router,
    public dialog: MatDialog,
    private fb: FormBuilder,
    public projectListService: ProjectListService,
    private appurl: AppUrlService,
    public toastr: ToastrService
  ) {
    this.getScreenSize();
  }

  get questions(): FormArray {
    return this.indicatorForm.get('questions') as FormArray;
  }

  indicatorDetail: ProjectIndicatorModel;

  // screen
  scrollStyles: any;
  screenHeight: any;
  screenWidth: any;

  // boolean flag
  EditLoaderFlag= false;
  NewIndicatorLoaderFlag= false;

  // Input/Output properties
  @Input() indicatorId: number;
  @Output() addIndicator = new EventEmitter<any>();
  @Output() editIndicator = new EventEmitter<any>();

  totalCount: number;
  //#endregion

  public indicatorForm: FormGroup;

  initializeModel() {
    this.indicatorForm = this.fb.group({
      indicatorName: ['', Validators.required],
      id: 0,
      questions: this.fb.array([])
    });

    this.indicatorDetail = {
      projectIndicatorCode: '',
      projectIndicatorId: 0,
      projectIndicatorName: ''
    };
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

  ngOnInit() {
    this.initializeModel();
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

    if (formValue.indicatorName != null && formValue.indicatorName != '') {
      const model: IndicatorDetailModel = {
        indicatorId: this.indicatorId,
        indicatorName: formValue.indicatorName,
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
        .subscribe(response => {
          if(response.StatusCode == 200 && response.data.ProjectIndicator != null){

            this.indicatorDetail.projectIndicatorCode= response.data.ProjectIndicator.IndicatorCode;
            this.indicatorDetail.projectIndicatorId= response.data.ProjectIndicator.ProjectIndicatorId;
            this.indicatorDetail.projectIndicatorName= response.data.ProjectIndicator.IndicatorName;
            this.editIndicator.emit(this.indicatorDetail);
            this.EditLoaderFlag= false;
            this.toastr.success('Project Indicator Updated Successfully');
          }
        },
          (error) => {
            this.EditLoaderFlag = false;
            this.toastr.success('Something went wrong');
          });
    }
    else{
      this.EditLoaderFlag = false;
    }
  }

  ngOnChanges(): void {

    if (this.indicatorId !== 0 && this.indicatorId !== undefined) {

       this.GetProjectIndicatorDetailById(this.indicatorId);
    }
  }

  GetProjectIndicatorDetailById(id){
    this.NewIndicatorLoaderFlag= true;

    if(id !=0 && id !=undefined && id !=null){
      this.projectListService
      .GetProjectIndicatorById(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetProjectIndicatorDetailById,id)
       .subscribe(response => {
        if (response.StatusCode == 200) {
          this.initializeModel();
          const control = <FormArray>this.indicatorForm.controls.questions;
          response.data.IndicatorModel.IndicatorQuestions.forEach(x => {
            control.push(
              this.fb.group({
              questionid: x.QuestionId,
              question: x.QuestionText,
              }));
          });

          this.indicatorForm.patchValue({
            indicatorName: response.data.IndicatorModel.IndicatorName,
            id: response.data.IndicatorModel.IndicatorId,
          })
          this.NewIndicatorLoaderFlag= false;
          this.EditLoaderFlag= false;
        }
        if (response.StatusCode == 400) {
          this.toastr.error(response.Message);
          this.EditLoaderFlag= false;
        }
      },
      (error) => {
        this.NewIndicatorLoaderFlag = false;
        this.EditLoaderFlag= false;
        this.toastr.error('Something went wrong');
      });
    }
  }

  CreateProjectIndicatorOnAddNew() {
    this.initializeModel();
    this.indicatorDetail.projectIndicatorId = 0;
    this.projectListService
      .AddProjectIndicatorQuestions(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddProjectIndicator
      )
      .subscribe(response => {
        if (response.StatusCode == 200) {
          this.indicatorId= response.data.ProjectIndicator.ProjectIndicatorId;

          const projectIndicatorModel: ProjectIndicatorModel = {
            projectIndicatorCode: response.data.ProjectIndicator.IndicatorCode,
            projectIndicatorId:
              response.data.ProjectIndicator.ProjectIndicatorId,
            projectIndicatorName: ''
          };

          this.addIndicator.emit({
            projectIndicator: projectIndicatorModel,
            count: response.data.TotalCount
          });
        }
      });
  }

  onDelete(index:number){
    const control = <FormArray>this.indicatorForm.controls['questions'];
    control.removeAt(index);
  }
}
