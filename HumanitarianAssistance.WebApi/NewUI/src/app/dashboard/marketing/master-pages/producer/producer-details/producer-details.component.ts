import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ProducerModel } from '../../model/mastrer-pages.model';
import { MasterPageServiceService } from '../../service/master-page-service.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MatDialog } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from 'src/app/shared/global';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';

@Component({
  selector: 'app-producer-details',
  templateUrl: './producer-details.component.html',
  styleUrls: ['./producer-details.component.scss']
})
export class ProducerDetailsComponent implements OnInit {
  producerDetailsForm;
  @Input() producerId: number;
  @Output() hideDetailPanel = new EventEmitter<any>();
  @Output() deleteProducer = new EventEmitter<any>();
  @Output() addProducer = new EventEmitter<any>();
  @Output() updateProducer = new EventEmitter<any>();
  archiveButton = false;
  producerDetail: ProducerModel = {};
  producerDetailsLoaderFlag = false;
  // tslint:disable-next-line:max-line-length
  constructor(public dialog: MatDialog, public toastr: ToastrService, private commonLoaderService: CommonLoaderService, private producerService: MasterPageServiceService, private appurl: AppUrlService) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    this.initForm();
    if (this.producerId !== 0 && this.producerId !== undefined) {
      this.archiveButton = true;
      this.GetProducerById(this.producerId);
    } else {
      this.archiveButton = false;
    }
  }

  initForm() {
    this.producerDetailsForm = new FormGroup({
      producerName: new FormControl('', [Validators.required])
    });
  }

  ResetFormOnAddNewProducer() {
    this.producerDetail = {};
    this.producerDetailsForm.reset();
    this.producerId = 0;
  }

  ngOnInit() {
  }

  GetProducerById(id) {
    this.producerDetailsLoaderFlag = true;
    // tslint:disable-next-line:max-line-length
    this.producerService.GetById(this.appurl.getApiUrl() + GLOBAL.API_Producer_GetProducerById, id).subscribe(result => {
      if (result.StatusCode === 200) {
        this.producerDetailsLoaderFlag = false;
        this.producerDetail = result.data.producerById;
        this.producerDetailsForm.controls['producerName'].setValue(
          this.producerDetail.ProducerName
        );
      } else {
        this.producerDetailsLoaderFlag = false;
        this.toastr.error('Some error occured.Please try again later.');
      }
    },
    error => {
      this.producerDetailsLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  onChange(value) {
    this.producerDetail.ProducerName = value;
    if (this.producerId === 0 || this.producerId === undefined || this.producerId === null) {
      this.CreateProducer();
    } else {
      this.EditProducer();
    }
  }

  CreateProducer() {
    this.producerDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.producerService.Add(this.appurl.getApiUrl() + GLOBAL.API_Producer_AddProducer, this.producerDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.producerDetailsForm.enable();
        this.producerDetail = result.data.producerById;
        this.producerId =  result.data.producerById.ProducerId;
        this.addProducer.emit(this.producerDetail);
        this.archiveButton = true;
      } else {
        this.toastr.error(result.Message);
        this.producerDetailsForm.enable();
      }
    },
    error => {
      this.producerDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  EditProducer() {
    this.producerDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.producerService.Add(this.appurl.getApiUrl() + GLOBAL.API_Producer_AddProducer, this.producerDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
        this.producerDetailsForm.enable();
        this.producerDetail = result.data.producerById;
        this.updateProducer.emit(this.producerDetail);
      }  else {
        this.producerDetailsForm.enable();
        this.toastr.error(result.Message);
      }
    },
    error => {
      this.producerDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  DeleteProducer(id: number) {
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
      this.producerDetailsForm.disable();
      // tslint:disable-next-line:max-line-length
      this.producerService.Delete(this.appurl.getApiUrl() + GLOBAL.API_Producer_DeleteProducer, id).subscribe(result => {
        if (result.StatusCode === 200) {
          this.producerDetailsForm.enable();
          this.toastr.success(result.Message);
          dialogRef.componentInstance.onCancelPopup();
          this.deleteProducer.emit({ id: id });
          this.producerDetail = {};
          this.producerId = 0;
        } else {
          this.producerDetailsForm.enable();
          this.toastr.error(result.Message);
        }
        dialogRef.componentInstance.isLoading = false;
      },
      error => {
        this.producerDetailsForm.enable();
        dialogRef.componentInstance.isLoading = false;
        this.toastr.error('Some error occured. Please try again later.');
      });
    });
  }

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion

  CreateProduceronAddNew() {
    this.producerDetail = {};
    // tslint:disable-next-line:max-line-length
    this.producerService.Add(this.appurl.getApiUrl() + GLOBAL.API_Producer_AddProducer, this.producerDetail).subscribe(result => {
      if (result.StatusCode === 200) {
        this.toastr.success(result.Message);
      this.producerDetail = result.data.producerById;
      this.producerId =  result.data.producerById.ProducerId;
      this.addProducer.emit(this.producerDetail);
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
