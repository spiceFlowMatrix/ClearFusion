import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { NatureModel } from '../../../contracts/model/contract-details.model';
import { MasterPageServiceService } from '../../service/master-page-service.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { MatDialog } from '@angular/material';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';

@Component({
  selector: 'app-nautre-details',
  templateUrl: './nautre-details.component.html',
  styleUrls: ['./nautre-details.component.scss']
})
export class NautreDetailsComponent implements OnInit {
  @Input() natureId: number;
  @Output() hideDetailPanel = new EventEmitter<any>();
  @Output() deleteNature = new EventEmitter<any>();
  @Output() addNature = new EventEmitter<any>();
  @Output() updateNature = new EventEmitter<any>();
  natureDetailsForm;
  archiveButton = false;
  natureDetail: NatureModel = {};
  natureDetailsLoaderFlag = false;
  // tslint:disable-next-line:max-line-length
  constructor(public dialog: MatDialog, public toastr: ToastrService, private commonLoaderService: CommonLoaderService, private natureService: MasterPageServiceService, private appurl: AppUrlService) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    this.initForm();
    // this.GetCategory();
    if (this.natureId !== 0 && this.natureId !== undefined) {
      this.archiveButton = true;
      this.GetNatureById(this.natureId);
    } else {
      this.archiveButton = false;
    }
  }

  initForm() {
    this.natureDetailsForm = new FormGroup({
      natureName: new FormControl('', [Validators.required])
    });
  }

  ResetFormOnAddNewNature() {
    this.natureDetail = {};
    this.natureDetailsForm.reset();
    this.natureId = 0;
  }

  ngOnInit() {
  }

  GetNatureById(id) {
    // this.commonLoaderService.showLoader();
    this.natureDetailsLoaderFlag = true;
    // tslint:disable-next-line:max-line-length
    this.natureService.GetById(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetNatureById, id).subscribe(result => {
      if (result.StatusCode === 200) {
        // this.commonLoaderService.hideLoader();
        this.natureDetailsLoaderFlag = false;
        this.natureDetail = result.data.natureById;
        this.natureDetailsForm = new FormGroup({
          natureName: new FormControl(this.natureDetail.NatureName, [Validators.required])
        });
      } else {
        // this.commonLoaderService.hideLoader();
        this.natureDetailsLoaderFlag = false;
        this.toastr.error('Some error occured.Please try again later.');
      }
    },
    error => {
      // this.commonLoaderService.hideLoader();
      this.natureDetailsLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  onChange(value) {
    this.natureDetail.NatureName = value;
    if (this.natureId === 0 || this.natureId === undefined || this.natureId === null) {
      this.CreateNature();
    } else {
      this.EditNature();
    }
  }

  CreateNature() {
    this.natureDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.natureService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddNature, this.natureDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.natureDetailsForm.enable();
      this.natureDetail = result.data.natureById;
      this.natureId = result.data.natureById.NatureId;
      this.addNature.emit(this.natureDetail);
      this.archiveButton = true;
      } else {
        this.toastr.error(result.Message);
        this.natureDetailsForm.enable();
      }
    },
    error => {
      this.natureDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  EditNature() {
    this.natureDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.natureService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddNature, this.natureDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.natureDetailsForm.enable();
        this.natureDetail = result.data.natureById;
        this.updateNature.emit(this.natureDetail);
      }  else {
        this.natureDetailsForm.enable();
        this.toastr.error(result.Message);
      }
    },
    error => {
      this.natureDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion

  DeleteNature(id) {
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
    this.natureDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.natureService.Delete(this.appurl.getApiUrl() + GLOBAL.API_Contract_DeleteNature, id).subscribe(result => {
      if (result.StatusCode === 200) {
        this.natureDetailsForm.enable();
        this.toastr.success(result.Message);
        dialogRef.componentInstance.onCancelPopup();
        this.deleteNature.emit({ id: id });
      this.natureDetail  = {};
      this.natureId = 0;
      } else {
        this.natureDetailsForm.enable();
        this.toastr.error(result.Message);
      }
      dialogRef.componentInstance.isLoading = false;
    },
    error => {
      this.natureDetailsForm.enable();
      dialogRef.componentInstance.isLoading = false;
      this.toastr.error('Some error occured. Please try again later');
    });
    });
  }

  CreateNatureonAddNew() {
    this.natureDetail = {};
    // tslint:disable-next-line:max-line-length
    this.natureService.Add(this.appurl.getApiUrl() + GLOBAL.API_Contract_AddNature, this.natureDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
      this.natureDetail = result.data.natureById;
      this.natureId = result.data.natureById.NatureId;
      this.addNature.emit(this.natureDetail);
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
