import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ChannelModel } from '../../model/mastrer-pages.model';
import { MatDialog } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MasterPageServiceService } from '../../service/master-page-service.service';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { MediumModel } from '../../../contracts/model/contract-details.model';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Component({
  selector: 'app-channel-detail',
  templateUrl: './channel-detail.component.html',
  styleUrls: ['./channel-detail.component.scss']
})
export class ChannelDetailComponent implements OnInit {

  channelDetailsForm;
  @Input() channelId: number;
  @Output() hideDetailPanel = new EventEmitter<any>();
  @Output() deleteChannel = new EventEmitter<any>();
  @Output() addChannel = new EventEmitter<any>();
  @Output() updateChannel = new EventEmitter<any>();
  archiveButton = false;
  channelDetail: ChannelModel = {};
  channelDetailsLoaderFlag = false;
  mediumList: MediumModel[];
  // tslint:disable-next-line:max-line-length
  constructor(public dialog: MatDialog, public toastr: ToastrService, private channelService: MasterPageServiceService, private appurl: AppUrlService) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    this.initForm();
    // this.GetCategory();
    if (this.channelId !== 0 && this.channelId !== undefined) {
      this.archiveButton = true;
      this.GetChannelById(this.channelId);
    } else {
      this.archiveButton = false;
    }
  }

  initForm() {
    this.channelDetailsForm = new FormGroup({
      channelName: new FormControl('', [Validators.required]),
      mediumId: new FormControl('', [Validators.required])
    });
  }

  ResetFormOnAddNewChannel() {
    this.channelDetail = {};
    this.channelDetailsForm.reset();
    this.channelId = 0;
  }

  ngOnInit() {
    this.getMediums();
  }

  getMediums() {
    this.mediumList = [];
    this.channelService.GetMediumList().subscribe((data:IResponseData) => {
      if (data.statusCode === 200) {
        this.mediumList =  data.data;
      } else {
      }
    },
    error => {
    });
  }

  selectionChanged(val, ev) {
    if (val === 'medium') {
      this.channelDetail.MediumId = ev.value;
    } if (val === 'channel') {
      this.channelDetail.ChannelName = ev;
    }

    if (this.channelId === 0 || this.channelId === undefined || this.channelId === null) {
          this.CreateChannel();
      }
      else {
          this.EditChannel();
      }
  }

  GetChannelById(id) {
    this.channelDetailsLoaderFlag = true;
    // tslint:disable-next-line:max-line-length
    this.channelService.GetChannelById(id).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.channelDetail = result.data;
        this.channelDetailsForm.controls['channelName'].setValue(
          this.channelDetail.ChannelName
        );
        this.channelDetailsForm.controls['mediumId'].setValue(
          this.channelDetail.MediumId
        );
      } else {
        this.toastr.error('Some error occured.Please try again later.');
      }
      this.channelDetailsLoaderFlag = false;
    },
    error => {
      this.channelDetailsLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  onChange(value) {

  }

  CreateChannel() {
    this.channelDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.channelService.AddChannel(this.channelDetail).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.toastr.success(result.message);
        this.channelDetail = result.data;
        this.channelId =  result.data.ChannelId;
        this.addChannel.emit(this.channelDetail);
        this.archiveButton = true;
      } else {
        this.toastr.error(result.message);
      }
      this.channelDetailsForm.enable();
    },
    error => {
      this.channelDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  EditChannel() {
    this.channelDetailsForm.disable();
    // tslint:disable-next-line:max-line-length
    this.channelService.AddChannel(this.channelDetail).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.toastr.success(result.message);
        this.channelDetailsForm.enable();
        this.channelDetail = result.data;
        this.updateChannel.emit(this.channelDetail);
      }  else {
        this.channelDetailsForm.enable();
        this.toastr.error(result.message);
      }
    },
    error => {
      this.channelDetailsForm.enable();
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  DeleteChannel(id) {
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
      this.channelDetailsForm.disable();
      // tslint:disable-next-line:max-line-length
      this.channelService.DeleteChannel(id).subscribe((result:IResponseData) => {
        if (result.statusCode === 200) {
          this.toastr.success(result.message);
          dialogRef.componentInstance.onCancelPopup();
          this.deleteChannel.emit({ id: id });
          this.channelDetail = {};
          this.channelId = 0;
        } else {
          this.toastr.error(result.message);
        }
        this.channelDetailsForm.enable();
        dialogRef.componentInstance.isLoading = false;
      },
      error => {
        this.channelDetailsForm.enable();
        dialogRef.componentInstance.isLoading = false;
        this.toastr.error('Some error occured. Please try again later');
      });
    });
  }

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion
}
