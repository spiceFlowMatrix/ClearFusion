import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';
import { Moment } from 'moment';
import { AmazingTimePickerService } from 'amazing-time-picker';
import { ScheduleDetailsModel, ScheduleDetails, SchedulerModel } from '../model/schedulerModel';
import { SchedulerService } from '../service/scheduler.service';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MarketingJobDetailModel } from '../../marketing-jobs/model/marketing-jobs.model';
import { PolicyModel, PolicyDayModel } from '../../broadcast-policy/model/policy-model';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-scheduler-add',
  templateUrl: './scheduler-add.component.html',
  styleUrls: ['./scheduler-add.component.scss']
})

export class SchedulerAddComponent implements OnInit {
  days = [
    { id: '1', Value: 'MON', status: false },
    { id: '2', Value: 'TUE', status: false },
    { id: '3', Value: 'WED', status: false },
    { id: '4', Value: 'THU', status: false },
    { id: '5', Value: 'FRI', status: false },
    { id: '6', Value: 'SAT', status: false },
    { id: '7', Value: 'SUN', status: false }
  ];

  scheduleType = [
    { id: '1', Value: 'Project', status: false },
    { id: '2', Value: 'Job', status: false },
    { id: '3', Value: 'Policy', status: false }
  ];
  enableProject = false;
  enableJob = false;
  enableupdateBtn = false;
  selectedDays: any[] = [];
  enablePolicy = false;
  SchedulerModel: SchedulerModel = {};
  scheduleDetailsModel: ScheduleDetails = {};
  addScheduleLoader = false;
  scheduleDetails: ScheduleDetailsModel = {};
  selected: {startDate: Moment, endDate: Moment};
  onListRefresh = new EventEmitter<any>();
  scheduleDetailsForm: any;
  dayModel: PolicyDayModel = {};
  myDate = new Date().toDateString();
  CurrentTime: any;
  showStartEndTimeError = false;
  showRepeatDaysError = false;
  showStartEndDatesError = false;
  isEditingAllowed = false;
  pageId = ApplicationPages.Scheduler;
  enableRepeatDaysDiv = false;

  constructor( private localStorageService: LocalStorageService,
    private appurl: AppUrlService,
    private schedulerService: SchedulerService,
    private atp: AmazingTimePickerService,
    private datePipe: DatePipe,
    private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: DataSources,
    public dialogRef: MatDialogRef<SchedulerAddComponent>) {
      this.myDate = this.datePipe.transform(this.myDate, 'yyyy-MM-dd');
   }

   // tslint:disable-next-line:use-life-cycle-interface
   ngOnChanges() {
   }

  ngOnInit() {
   this.initForm();
   this.enableProject = true;
   if (this.data.scheduleId !== 0) {
     this.enableupdateBtn = true;
    this.GetScheduleById(this.data.scheduleId);
   } if (this.data.scheduleId === 0) {
    this.SchedulerModel.ChannelId = this.data.channelId;
    this.SchedulerModel.MediumId = this.data.mediumId;
   }
   setInterval(() => {
    this.CurrentTime = new Date().getHours() + ':' + new Date().getMinutes(); }, 1);
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
  }

  policyChanged(policyId) {
  }

  selectionChanged(ev) {
    this.scheduleDetailsForm.projectId = 0;
    this.scheduleDetailsForm.jobId = 0;
    this.scheduleDetailsForm.policyId = 0;
    const val = this.scheduleType.find(x => x.Value == ev.value);
    if (val.Value === 'Project') {
      this.SchedulerModel.ScheduleType = 'Project';
       this.enableJob = false;
       this.enablePolicy = false;
       this.enableProject = true;
    } if (val.Value === 'Job') {
      this.SchedulerModel.ScheduleType = 'Job';
      this.enableJob = true;
      this.enablePolicy = false;
      this.enableProject = false;
    } if (val.Value === 'Policy') {
      this.SchedulerModel.ScheduleType = 'Policy';
      this.enableJob = false;
      this.enablePolicy = true;
      this.enableProject = false; ;
    }
  }

   GetScheduleById(model) {
    this.schedulerService
    .Post(
       this.appurl.getApiUrl() + GLOBAL.API_Scheduler_GetScheduleDetailsById, model
     ).subscribe(response => {
         this.scheduleDetails = response.data.scheduleDetailsModel;
        if (response.data.scheduleDetailsModel !== null && response.data.scheduleDetailsModel !== undefined) {
          this.scheduleDetailsForm.scheduleType = response.data.scheduleDetailsModel.ScheduleType;
          if (this.scheduleDetailsForm.scheduleType === 'Project') {
            this.enableJob = false;
            this.enablePolicy = false;
            this.enableProject = true;
         } if (this.scheduleDetailsForm.scheduleType === 'Job') {
           this.enableJob = true;
           this.enablePolicy = false;
           this.enableProject = false;
         } if (this.scheduleDetailsForm.scheduleType === 'Policy') {
           this.enableJob = false;
           this.enablePolicy = true;
           this.enableProject = false;
         }
          this.scheduleDetailsForm.mediumId = response.data.scheduleDetailsModel.MediumId;
          this.scheduleDetailsForm.channelId = response.data.scheduleDetailsModel.ChannelId;
          this.scheduleDetailsForm.policyId = response.data.scheduleDetailsModel.PolicyId;
          this.scheduleDetailsForm.jobId = response.data.scheduleDetailsModel.JobId;
          this.scheduleDetailsForm.projectId = response.data.scheduleDetailsModel.ProjectId;
          this.scheduleDetailsForm.startTime = response.data.scheduleDetailsModel.StartTime;
          this.scheduleDetailsForm.endTime = response.data.scheduleDetailsModel.EndTime;
          this.scheduleDetailsForm.endDate = StaticUtilities.setLocalDate(response.data.scheduleDetailsModel.EndDate);
          this.scheduleDetailsForm.startDate = StaticUtilities.setLocalDate(response.data.scheduleDetailsModel.StartDate);
          this.scheduleDetailsForm.scheduleId = response.data.scheduleDetailsModel.ScheduleId;
          this.selectedDays = response.data.RepeatDays;
        }
        console.log(this.scheduleDetailsForm);
        // this.dayModel = response.data.policyTimeDetailsById;
        response.data.RepeatDays.forEach(element => {
              // tslint:disable-next-line:no-unused-expression
             const day =  this.days.find(x => x.Value === element);
             day.status = true;
        });
     });
  }

  open() {
    const amazingTimePicker = this.atp.open();
    amazingTimePicker.afterClose().subscribe(time => {
    });
  }

  policyListRefresh(data) {
    this.onListRefresh.emit(data);
  }

  initForm() {
    this.scheduleDetailsForm = {
        scheduleId: 0,
        policyId: 0,
        projectId: 0,
        jobId: 0,
        startDate: '',
        endDate: '',
        startTime: '',
        endTime: '',
        title: '',
        description: '',
        repeatDays: [],
        repeat: 0,
        scheduleType: ''
    };
  }

  selectDays(value) {
    const day1 = (this.days.find(x => x.Value === value));
    if (day1.status === true) {
        day1.status = false;
        const index = this.days.findIndex(x => x.Value == value);
        this.selectedDays.splice(index, 1);
    } else {
      day1.status = true;
      this.selectedDays.push(day1);
    }
    this.SchedulerModel.RepeatDays = this.days;
    if (this.selectedDays.length > 0) {
      this.showRepeatDaysError = false;
    } else {
      this.showRepeatDaysError = true;
    }
  }

  scheduleTime(ev, type) {
      const amazingTimePicker = this.atp.open();
      if (this.scheduleDetailsForm.startTime === '' && this.scheduleDetailsForm.endTime === '') {
      amazingTimePicker.afterClose().subscribe(time => {
        if (type === 'startTime') {
          this.scheduleDetailsForm.startTime = time;
        } if (type === 'endTime') {
          this.scheduleDetailsForm.endTime = time;
        }
        if (this.scheduleDetailsForm.startTime !== '' && this.scheduleDetailsForm.endTime !== '') {
          this.showStartEndTimeError = false;
        }
      });
    }

  }

  updateSchedule(): void {
    if (this.scheduleDetailsForm.scheduleId !== 0) {

      this.SchedulerModel.PolicyId = this.scheduleDetailsForm.policyId;
      this.SchedulerModel.ProjectId = this.scheduleDetailsForm.projectId;
      this.SchedulerModel.JobId = this.scheduleDetailsForm.jobId;
      this.SchedulerModel.StartDate = StaticUtilities.getLocalDate(this.scheduleDetailsForm.startDate);
      this.SchedulerModel.StartTime = this.scheduleDetailsForm.startTime;
      this.SchedulerModel.EndDate = StaticUtilities.getLocalDate(this.scheduleDetailsForm.endDate);
      this.SchedulerModel.EndTime = this.scheduleDetailsForm.endTime;
      this.SchedulerModel.ScheduleId = this.scheduleDetailsForm.scheduleId;
      this.SchedulerModel.RepeatDays = this.days;
      this.SchedulerModel.ChannelId = this.data.channelId;
      this.SchedulerModel.MediumId = this.data.mediumId;
      this.schedulerService.Post(this.appurl.getApiUrl() + GLOBAL.API_Schedule_AddSchedule, this.SchedulerModel)
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.policyListRefresh(data.data.scheduleDetailsModel);
          this.onCancelPopup();
          this.toastr.success(data.Message);
        } else {
          this.toastr.error(data.Message);
        }
      });
    }
  }

  dateChange() {
    if (this.scheduleDetailsForm.startDate !== '' &&  this.scheduleDetailsForm.endDate !== '') {
      this.showStartEndDatesError = false;
    } else {
      this.showStartEndDatesError = true;
    }
  }

  getDay() {
    this.selectedDays = [];
    const date = new Date();
    const day = date.getDay().toString();
    const day1 = (this.days.find(x => x.id === day));
    day1.status = true;
    this.selectedDays.push(day1);
    this.SchedulerModel.RepeatDays = this.days;
  }

  SaveSchedule(): void {
    this.addScheduleLoader = true;
    if (this.data.channelId === 0) {
      this.toastr.error('Please select a channel');
      this.addScheduleLoader = false;
    } else {
      if (this.enableRepeatDaysDiv) {
        this.SchedulerModel.StartDate = this.scheduleDetailsForm.startDate;
        this.SchedulerModel.EndDate = this.scheduleDetailsForm.endDate;
      } else {
        this.SchedulerModel.StartDate = this.data.currentDate;
        this.scheduleDetailsForm.startDate = this.SchedulerModel.StartDate;
        this.SchedulerModel.EndDate = this.SchedulerModel.StartDate;
        this.scheduleDetailsForm.endDate = this.SchedulerModel.EndDate;
        this.getDay();

      }

      if (this.scheduleDetailsForm.startTime !== '' && this.scheduleDetailsForm.endTime !== '') {
        const eventStartTime: any = new Date()
        .setHours(Number(this.scheduleDetailsForm.startTime
        .split(':')[0]), Number(this.scheduleDetailsForm.startTime.split(':')[1]), 0, 0 );
        const eventEndTime: any = new Date()
        .setHours(Number(this.scheduleDetailsForm.endTime
        .split(':')[0]), Number(this.scheduleDetailsForm.endTime.split(':')[1]), 0, 0 );
        if (eventStartTime > eventEndTime) {
          this.toastr.error('Start Time can not be greater than End Time');
        } else {
           if (this.scheduleDetailsForm.startDate !== '' &&  this.scheduleDetailsForm.endDate !== '') {
             this.scheduleDetailsForm.startDate = new Date(Date.UTC((this.scheduleDetailsForm.startDate).getFullYear(),
             new Date(this.scheduleDetailsForm.startDate).getMonth(),
             new Date(this.scheduleDetailsForm.startDate).getDate()));
             this.scheduleDetailsForm.endDate = new Date(Date.UTC((this.scheduleDetailsForm.endDate).getFullYear(),
             new Date(this.scheduleDetailsForm.endDate).getMonth(),
             new Date(this.scheduleDetailsForm.endDate).getDate()));
             if (this.scheduleDetailsForm.startDate > this.scheduleDetailsForm.endDate) {
               this.toastr.error('Start Date can not be greater than End Date');
             } else {
               if (this.scheduleDetailsForm.projectId !== 0
                 || this.scheduleDetailsForm.jobId !== 0
                 || this.scheduleDetailsForm.policyId !== 0) {
                 this.SchedulerModel.PolicyId = this.scheduleDetailsForm.policyId;
                 this.SchedulerModel.ProjectId = this.scheduleDetailsForm.projectId;
                 this.SchedulerModel.JobId = this.scheduleDetailsForm.jobId;
                 // this.SchedulerModel.StartDate = this.scheduleDetailsForm.startDate;
                 this.SchedulerModel.StartTime = this.scheduleDetailsForm.startTime;
                 this.SchedulerModel.EndDate = this.scheduleDetailsForm.endDate;
                 this.SchedulerModel.EndTime = this.scheduleDetailsForm.endTime;
                 this.SchedulerModel.ScheduleId = this.scheduleDetailsForm.scheduleId;
                 this.SchedulerModel.RepeatDays = this.days;
                 this.SchedulerModel.ChannelId = this.data.channelId;
                 this.SchedulerModel.MediumId = this.data.mediumId;
                 this.schedulerService.Post(this.appurl.getApiUrl() + GLOBAL.API_Schedule_AddSchedule, this.SchedulerModel)
                 .subscribe(data => {
                   if (data.StatusCode === 200) {
                     this.policyListRefresh(data.data.scheduleDetailsModel);
                     this.onCancelPopup();
                     this.toastr.success(data.Message);
                   } else {
                     this.toastr.error(data.Message);
                   }
                   this.addScheduleLoader = false;
                 },
                 error => {
                   this.addScheduleLoader = false;
                   this.toastr.error('Some error occured. Please try again later');
                 });
               } else {
                this.addScheduleLoader = false;
                 this.toastr.error('Please select Schedule Type');
               }
             }
            } else {
              this.showStartEndDatesError = true;
            }
         }
    } else {
      this.showStartEndTimeError = true;
      }
    }
  }

  onSubmit(data): void {
    this.addScheduleLoader = true;
    this.scheduleDetails.PolicyScheduleId = 0;
    this.scheduleDetails.StartTime = this.scheduleDetailsForm.startTime;
    this.scheduleDetails.EndTime = this.scheduleDetailsForm.endTime;
    this.scheduleDetails.Description = this.scheduleDetailsForm.description;
    this.scheduleDetails.Title = this.scheduleDetailsForm.title;
    this.scheduleDetails.StartDate = this.scheduleDetailsForm.startDate;
    this.scheduleDetails.EndDate = this.scheduleDetailsForm.endDate;
    this.schedulerService
    .Post(this.appurl.getApiUrl() + GLOBAL.API_Policy_AddSchedule, this.scheduleDetails)
    .subscribe(result => {
         if (result.StatusCode === 200) {
          this.toastr.success(result.Message);
         } else {
            this.toastr.error(result.Message);
         }
         this.addScheduleLoader = false;
    }, error => {
      this.addScheduleLoader = false;
          this.toastr.error('Some error occured. Please try again later.');
    });
    this.policyListRefresh(data);
  }

  onCancelPopup(): void {
    this.dialogRef.close();
  }


  getValidationForStartTime(): boolean {
    if (this.scheduleDetailsForm.startTime > this.scheduleDetailsForm.endTime) {
      this.showStartEndTimeError = true;
      return true;
    } else {
      this.showStartEndTimeError = false;
      return false;
    }
  }

  toggle(ev) {
    if (ev.checked) {
      this.enableRepeatDaysDiv = true;
    } else {
      this.enableRepeatDaysDiv = false;
    }
  }

}

interface DataSources {
  data: any;
  jobList: MarketingJobDetailModel[];
  policyList: PolicyModel[];
  projectList: any[];
  scheduleId: any;
  mediumId: number;
  channelId: number;
  currentDate: any;
}
