import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { playoutMinutesModel } from '../model/schedulerModel';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { SchedulerService } from '../service/scheduler.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-playout-minutes',
  templateUrl: './playout-minutes.component.html',
  styleUrls: ['./playout-minutes.component.scss']
})
export class PlayoutMinutesComponent implements OnInit {
  playoutMinutesForm: any;
  playoutMinutesModel: playoutMinutesModel = {};
  disableTotalMinutes = false;
  disableDroppedMinutes = false;
  disableSubmitBtn = true;
  minute: any;
  addPlayoutMinutesLoader = false;
  constructor( private toastr: ToastrService, private schedulerService: SchedulerService, private appurl: AppUrlService, @Inject(MAT_DIALOG_DATA) public data: DataSources,
  public dialogRef: MatDialogRef<PlayoutMinutesComponent>) { }
  
  ngOnInit() {
    this.initForm();
    this.GetScheduleDetailsById(this.data.scheduleId);
  }

  initForm() {
    this.playoutMinutesForm = {
        totalMinutes: '',
        droppedMinutes: ''
    };
  }

  GetScheduleDetailsById(id) {
    this.schedulerService
    .Post(
       this.appurl.getApiUrl() + GLOBAL.API_Scheduler_GetScheduleDetailsById, id
     ).subscribe(result => {      
      if (result.StatusCode === 200) {
         this.playoutMinutesModel.startTime =  new Date().setHours(Number(result.data.scheduleDetailsModel.StartTime.split(':')[0]), Number(result.data.scheduleDetailsModel.StartTime.split(':')[1]), 0, 0 );
         this.playoutMinutesModel.endTime = new Date().setHours(Number(result.data.scheduleDetailsModel.EndTime.split(':')[0]), Number(result.data.scheduleDetailsModel.EndTime.split(':')[1]), 0, 0 );
         let time = this.playoutMinutesModel.endTime - this.playoutMinutesModel.startTime;  //msec
         let hoursDiff = time / (3600 * 1000);
         this.minute = hoursDiff * 60;
         this.playoutMinutesModel.allowedMinutes = this.minute ;
       } else {
          this.toastr.error(result.Message);
       }
      }, error => {
        this.toastr.error('Some error occured. Please try again later.');
    });
  }

  addPlayoutMinutes() {
    this.playoutMinutesModel.DroppedMinutes = this.playoutMinutesForm.droppedMinutes;
    this.playoutMinutesModel.TotalMinutes = this.playoutMinutesForm.totalMinutes;
    this.playoutMinutesModel.ScheduleId = this.data.scheduleId;
    this.addPlayoutMinutesLoader = true;
    this.schedulerService
    .Post(
       this.appurl.getApiUrl() + GLOBAL.API_Scheduler_AddPlayoutMinute, this.playoutMinutesModel
     ).subscribe(result => {
      if (result.StatusCode === 200) {
        this.onCancelPopup();
        this.toastr.success(result.Message);
       } else {
          this.toastr.error(result.Message);
       }
       this.addPlayoutMinutesLoader = false;
      }, error => {
        this.addPlayoutMinutesLoader = false;;
        this.toastr.error('Some error occured. Please try again later.');
    });
  }

  selectionChanged(type) {
     if (type === 'totalMinutes') {
        if (this.playoutMinutesForm.totalMinutes != '') {
          if (parseInt(this.playoutMinutesForm.totalMinutes) > parseInt(this.playoutMinutesModel.allowedMinutes)) {
             this.toastr.error('Total Minutes can not be greater than scheduled time');
             this.playoutMinutesForm.totalMinutes = '';
             this.playoutMinutesForm.droppedMinutes = '';
             this.disableSubmitBtn = true;
          } else {
            this.playoutMinutesForm.droppedMinutes =  this.playoutMinutesModel.allowedMinutes - this.playoutMinutesForm.totalMinutes;
            this.disableDroppedMinutes = true;
            this.disableSubmitBtn = false;
          }          
        } else {
          this.playoutMinutesForm.droppedMinutes = '';
          this.disableDroppedMinutes = false;
          this.disableSubmitBtn = true;
        }
    } if (type === 'droppedMinutes') {
        if (this.playoutMinutesForm.droppedMinutes != '') {
          if (parseInt(this.playoutMinutesForm.droppedMinutes) > parseInt(this.playoutMinutesModel.allowedMinutes)) {
            this.toastr.error('Dropped Minutes can not be greater than scheduled time');
            this.playoutMinutesForm.droppedMinutes = '';
            this.disableSubmitBtn = true;
          } else {
            this.playoutMinutesForm.totalMinutes =  this.playoutMinutesModel.allowedMinutes - this.playoutMinutesForm.droppedMinutes
            this.disableTotalMinutes = true;
            this.disableSubmitBtn = false;
          }
        } else {
          this.playoutMinutesForm.totalMinutes = '';
          this.disableTotalMinutes = false;
          this.disableSubmitBtn = true;
        }
      }
    }

    onCancelPopup(): void {
      this.dialogRef.close();
    }
}

interface DataSources {
  data: any;
  scheduleId: number;
}
