import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { StartEndTimeModel } from '../model/policy-model';
import { BroadcastPolicyService } from '../service/broadcast-policy.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';

@Component({
  selector: 'app-start-end-time',
  templateUrl: './start-end-time.component.html',
  styleUrls: ['./start-end-time.component.scss']
})

export class StartEndTimeComponent implements OnInit {
  onListRefresh = new EventEmitter();
  startEndTimeForm: any;
  timeModel: StartEndTimeModel = {};
  // tslint:disable-next-line:max-line-length
  constructor( private appurl: AppUrlService, private policyService: BroadcastPolicyService, public dialogRef: MatDialogRef<StartEndTimeComponent>, @Inject(MAT_DIALOG_DATA) public data: DataSources) { }

  ngOnInit() {
   this.initForm();
   if (this.data.id !== 0 && this.data.id !== undefined && this.data.id !== null) {
    this.getPolicyTimeScheduleById(this.data.id);
   } else {
    this.startEndTimeForm.startTime = '';
    this.startEndTimeForm.endTime = '';
   }
  }

    initForm() {
      this.startEndTimeForm = {
        startTime: '',
        endTime: ''
    };
  }

  getPolicyTimeScheduleById(id) {
      this.policyService.Post(this.appurl.getApiUrl() + GLOBAL.API_Policy_GetPolicyTimeScheduleById, id)
      .subscribe(data => {
        this.startEndTimeForm.startTime = data.data.policyTimeDetailsById.StartTime;
        this.startEndTimeForm.endTime = data.data.policyTimeDetailsById.EndTime;
      });
  }

  policyListRefresh(data) {
    this.onListRefresh.emit(data);
  }

  SaveTime(): void {
    this.timeModel.startTime = this.startEndTimeForm.startTime;
    this.timeModel.endTime = this.startEndTimeForm.endTime;
     this.onCancelPopup();
     this.policyListRefresh(this.timeModel);
  }

  onCancelPopup(): void {
    this.dialogRef.close();
  }
}

interface DataSources {
  data: any;
  id?: number;
}
