import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import {
  IIndicatorDataSource,
  IProjectIndicatorModel
} from '../project-indicators-model';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ProjectIndicatorService } from '../project-indicator.service';

@Component({
  selector: 'app-add-project-indicator',
  templateUrl: './add-project-indicator.component.html',
  styleUrls: ['./add-project-indicator.component.scss']
})
export class AddProjectIndicatorComponent implements OnInit {
  //#region "Input/Output"

  @Output() onIndicatorListRefresh = new EventEmitter();
  @Output() onUpdateIndicatorListRefresh = new EventEmitter();
  //#endregion

  //#region  "Variables"
  indicatorForm: FormGroup;
  projectId: number;
  IndicatorDetail: any;
  addIndicatorLoader = false;

  //#endregion
  constructor(
    public dialogRef: MatDialogRef<AddProjectIndicatorComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IIndicatorDataSource,
    private fb: FormBuilder,
    public toastr: ToastrService,
    public indicatorService: ProjectIndicatorService
  ) {
    this.projectId = data.ProjectId;
    this.IndicatorDetail = data.ProjectindicatorDetail;
  }

  ngOnInit() {
    this.initForm();
    if (
      this.IndicatorDetail != null &&
      this.IndicatorDetail !== undefined
    ) {
      this.setIndicatorDetail();
    }
  }

  //#region "InitForm"
  initForm() {
    this.indicatorForm = this.fb.group({
      IndicatorName: ['', Validators.required],
      Description: ['', Validators.required],
      ProjectIndicatorId: []
    });
  }
  //#endregion

  //#region "OnSubmitForm"
  onFormSubmit(data): void {
    if (
      data.ProjectIndicatorId != null &&
      data.ProjectIndicatorId != undefined
    ) {
      this.EditProjectIndicator(data);
    } else {
      this.AddProjectIndicator(data);
    }
  }
  //#endregion
  AddProjectIndicator(data: IProjectIndicatorModel) {
    if (this.indicatorForm.valid && this.projectId != undefined && this.projectId != null) {
      this.addIndicatorLoader = true;
      const indicatorDetail: IProjectIndicatorModel = {
        IndicatorName: data.IndicatorName,
        Description: data.Description,
        ProjectId: this.projectId
      };
      this.indicatorService.AddIndicatorDetail(indicatorDetail).subscribe(
        (response: IResponseData) => {
          if (response.statusCode === 200) {
            this.toastr.success('New indicator is created successfully');
          } else {
            this.toastr.error(response.message);
          }
          this.onCancelPopup();
          this.indicatorListRefresh(indicatorDetail);

          this.addIndicatorLoader = false;
        },
        error => {
          this.toastr.error('Someting went wrong. Please try again');
          this.addIndicatorLoader = false;
        }
      );
    }
  }

  EditProjectIndicator(data: IProjectIndicatorModel) {
    if (this.indicatorForm.valid) {
      this.addIndicatorLoader = true;
      const indicatorDetail: IProjectIndicatorModel = {
        ProjectIndicatorId: data.ProjectIndicatorId,
        IndicatorName: data.IndicatorName,
        Description: data.Description,
        ProjectId: this.projectId
      };
      this.indicatorService
        .EditProjectIndicatorDetail(indicatorDetail)
        .subscribe(
          (response: IResponseData) => {
            if (response.statusCode === 200) {
              this.toastr.success('New indicator is created successfully');
            } else {
              this.toastr.error(response.message);
            }
            this.onCancelPopup();
            this.indicatorListRefreshOnUpdate(indicatorDetail);
            this.addIndicatorLoader = false;
          },
          error => {
            this.toastr.error('Someting went wrong. Please try again');
            this.addIndicatorLoader = false;
          }
        );
    }
  }

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion

  //#region "hiringRequestListRefresh"
  indicatorListRefresh(indicatorDetail: any) {
    this.onIndicatorListRefresh.emit(indicatorDetail);
  }

  indicatorListRefreshOnUpdate(data: any) {
    this.onUpdateIndicatorListRefresh.emit(data);
  }
  //#endregion

  //#region "setHirectingrequestDetail"
  setIndicatorDetail() {
    this.indicatorForm = this.fb.group({
      Description: [this.IndicatorDetail.Description],
      IndicatorName: [this.IndicatorDetail.IndicatorName],
      ProjectIndicatorId: [this.IndicatorDetail.ProjectIndicatorId]
    });
  }
  //#endregion
}
