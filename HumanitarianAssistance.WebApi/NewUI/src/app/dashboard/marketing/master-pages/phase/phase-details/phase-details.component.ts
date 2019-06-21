import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { GLOBAL } from 'src/app/shared/global';
import { MasterPageServiceService } from '../../service/master-page-service.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { PhaseModel } from '../../model/mastrer-pages.model';
import { Validators, FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { MatDialog } from '@angular/material';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';

@Component({
  selector: 'app-phase-details',
  templateUrl: './phase-details.component.html',
  styleUrls: ['./phase-details.component.scss']
})
export class PhaseDetailsComponent implements OnInit {
  @Input() phaseId: number;
  @Output() hideDetailPanel = new EventEmitter<any>();
  @Output() deletePhase = new EventEmitter<any>();
  @Output() addPhase = new EventEmitter<any>();
  @Output() updatePhase = new EventEmitter<any>();
  phaseDetailsForm;
  archiveButton = false;
  phaseDetail: PhaseModel = {};
  phaseDetailsLoaderFlag = false;
  // tslint:disable-next-line:max-line-length
  constructor(public dialog: MatDialog, public toastr: ToastrService, private commonLoaderService: CommonLoaderService, private phaseService: MasterPageServiceService, private appurl: AppUrlService) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    this.initForm();
    // this.GetCategory();
    if (this.phaseId !== 0 && this.phaseId !== undefined) {
      this.archiveButton = true;
      this.GetPhaseById(this.phaseId);
    } else {
      this.archiveButton = false;
    }
  }

  initForm() {
    this.phaseDetailsForm = new FormGroup({
      phaseName: new FormControl('', [Validators.required])
    });
  }

  ResetFormOnAddNewPhase() {
    this.phaseDetail = {};
    this.phaseDetailsForm.reset();
    this.phaseId = 0;
  }

  ngOnInit() {
  }

  GetPhaseById(id) {
    // this.commonLoaderService.showLoader();
    this.phaseDetailsLoaderFlag = true;
    // tslint:disable-next-line:max-line-length
    this.phaseService.GetById(this.appurl.getApiUrl() + GLOBAL.API_Job_GetPhaseById, id).subscribe(result => {
      if (result.StatusCode === 200) {
        // this.commonLoaderService.hideLoader();
        this.phaseDetailsLoaderFlag = false;
        this.phaseDetail = result.data.phaseById;
        this.phaseDetailsForm.controls['phaseName'].setValue(
          this.phaseDetail.Phase
        );
        // this.phaseDetailsForm = new FormGroup({
        //   phaseName: new FormControl(this.phaseDetail.Phase, [Validators.required])
        // });
      } else {
        // this.commonLoaderService.hideLoader();
        this.phaseDetailsLoaderFlag = false;
        this.toastr.error('Some error occured.Please try again later.');
      }
    },
    error => {
      // this.commonLoaderService.hideLoader();
      this.phaseDetailsLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  onChange(value) {
    this.phaseDetail.Phase = value;
    if (this.phaseId === 0 || this.phaseId === undefined || this.phaseId === null) {
      this.CreatePhase();
    } else {
      this.EditPhase();
    }
  }

  CreatePhase() {
    this.phaseDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.phaseService.Add(this.appurl.getApiUrl() + GLOBAL.API_Job_AddPhase, this.phaseDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.phaseDetailsForm.enable();
        this.phaseDetail = result.data.phaseById;
        this.phaseId =  result.data.phaseById.JobPhaseId;
        this.addPhase.emit(this.phaseDetail);
        this.archiveButton = true;
      } else {
        this.toastr.error(result.Message);
        this.phaseDetailsForm.enable();
      }
    },
    error => {
      this.phaseDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  EditPhase() {
    this.phaseDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.phaseService.Add(this.appurl.getApiUrl() + GLOBAL.API_Job_AddPhase, this.phaseDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.phaseDetailsForm.enable();
        this.phaseDetail = result.data.phaseById;
        this.updatePhase.emit(this.phaseDetail);
      }  else {
        this.phaseDetailsForm.enable();
        this.toastr.error(result.Message);
      }
    },
    error => {
      this.phaseDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion

  DeletePhase(id) {
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
      this.phaseDetailsForm.disable();
      // tslint:disable-next-line:max-line-length
      this.phaseService.Delete(this.appurl.getApiUrl() + GLOBAL.API_Job_DeletePhase, id).subscribe(result => {
        if (result.StatusCode === 200) {
          this.phaseDetailsForm.enable();
          this.toastr.success(result.Message);
          dialogRef.componentInstance.onCancelPopup();
          this.deletePhase.emit({ id: id });
          this.phaseDetail = {};
          this.phaseId = 0;
        } else {
          this.phaseDetailsForm.enable();
          this.toastr.error(result.Message);
        }
        dialogRef.componentInstance.isLoading = false;
      },
      error => {
        this.phaseDetailsForm.enable();
        dialogRef.componentInstance.isLoading = false;
        this.toastr.error('Some error occured. Please try again later');
      });
    });
  }

  CreatePhaseonAddNew() {
    this.phaseDetail = {};
    // tslint:disable-next-line:max-line-length
    this.phaseService.Add(this.appurl.getApiUrl() + GLOBAL.API_Job_AddPhase, this.phaseDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
      this.phaseDetail = result.data.phaseById;
      this.phaseId =  result.data.phaseById.JobPhaseId;
      this.addPhase.emit(this.phaseDetail);
      this.archiveButton = true;
      } else {
        this.toastr.error(result.Message);
      }
    },
    error => {
      this.toastr.error('Some error occured. Please try again later');
    });
  }
}
