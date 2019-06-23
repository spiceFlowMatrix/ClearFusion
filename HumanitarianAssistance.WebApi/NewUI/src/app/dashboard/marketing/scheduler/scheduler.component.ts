import { Component, OnInit, ChangeDetectionStrategy, ViewChild, TemplateRef } from '@angular/core';
import { CalendarView, CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent } from 'angular-calendar';
import {
  isSameDay,
  isSameMonth
} from 'date-fns';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { SchedulerAddComponent } from './scheduler-add/scheduler-add.component';
import {
  ChangeDetectorRef
} from '@angular/core';

import moment from 'moment-timezone';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { SchedulerService } from './service/scheduler.service';
import { ToastrService } from 'ngx-toastr';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ScheduleTimeModel, ScheduleDetails, filterSchedulerModel, FormDateModule } from './model/schedulerModel';
import { MarketingJobDetailModel } from '../marketing-jobs/model/marketing-jobs.model';
import { PolicyModel } from '../broadcast-policy/model/policy-model';
import { MediumModel } from '../contracts/model/contract-details.model';
import { MasterPageServiceService } from '../master-pages/service/master-page-service.service';
import { ChannelModel } from '../master-pages/model/mastrer-pages.model';
import { MatDialog } from '@angular/material/dialog';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import 'rxjs/add/observable/interval';
import { PlayoutMinutesComponent } from './playout-minutes/playout-minutes.component';
import { Subscription } from 'rxjs/internal/Subscription';
import { Subject } from 'rxjs/internal/Subject';
import { StaticUtilities } from 'src/app/shared/static-utilities';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  }
};


moment.tz.setDefault('Utc');
@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-scheduler',
  templateUrl: './scheduler.component.html',
  styleUrls: ['./scheduler.component.scss']
})

export class SchedulerComponent implements OnInit {

  // tslint:disable-next-line:max-line-length
  constructor(private localStorageService: LocalStorageService,
  private masterService: MasterPageServiceService,
  private modal: NgbModal,
  private toastr: ToastrService,
  private appurl: AppUrlService,
  private schedulerService: SchedulerService,
  public dialog: MatDialog,
  private cdr: ChangeDetectorRef) {}

  @ViewChild('modalContent') modalContent: TemplateRef<any>;
  view: CalendarView = CalendarView.Day;
  jobsList: MarketingJobDetailModel[];
  CalendarView = CalendarView;
  policyList: PolicyModel[] = [];
  schedulrTimeModel: ScheduleTimeModel[] = [];
  filterScheduler:  filterSchedulerModel = {};
  viewDate: Date = new Date();
  projectList: any[];
  scheduleDetailsForm: any;
  dateForm: FormDateModule;
  scheduleDetailList: ScheduleDetails[] = [];
  mediumId = 0;
  modalData: {
    action: string;
    event: CalendarEvent;
  };
  sub: Subscription;
  mediumList: MediumModel[];
  channelList: ChannelModel[];
  actionsEdit: CalendarEventAction[] = [
    {
      label: '<i class="fa fa-fw fa-pencil"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      }
    },
    {
      label: '<i class="fa fa-fw fa-times"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Deleted', event);
      }
    },
    {
      label: '<i class="fa fa-fw fa-clock-o"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('PlayoutTime', event);
      }
    }
  ];
  actionsView: CalendarEventAction[] = [
    {
      label: '<i class="fa fa-fw fa-pencil"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      }
    }
  ];
  refresh: Subject<any> = new Subject();
  events: CalendarEvent[] = [];
  isEditingAllowed = false;
  pageId = ApplicationPages.Scheduler;
  activeDayIsOpen = true;
  currentDate: Date = new Date();
  date: any;

  InitilizeSchedular() {
    this.activeDayIsOpen = true;
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      this.viewDate = date;
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void {
    event.start = newStart;
    event.end = newEnd;
    this.handleEvent('Dropped or resized', event);
    this.refresh.next();
  }

  handleEvent(action: string, event: CalendarEvent): void {
    this.modalData = { event, action };
    this.modal.open(this.modalContent, { size: 'lg' });
    if (action === 'Edited') {
      this.addEvent(event);
    } if (action === 'Deleted') {
      this.DeleteEvent(event);
    } if (action === 'PlayoutTime') {
      this.addPlayoutMinute(event);
    }
  }

  addPlayoutMinute(event): void {
    const dialogRef = this.dialog.open(PlayoutMinutesComponent, {
      width: '550px',
      data: {
        data: 'hello',
        scheduleId: event.id
    }
    });
  }

  addEvent(event): void {
    const schdList = this.scheduleDetailList.find(x => x.ScheduleId === event.id);
    const dialogRef = this.dialog.open(SchedulerAddComponent, {
      width: '550px',
      data: {
        data: 'hello',
        jobList: this.jobsList,
        policyList: this.policyList,
        projectList: this.projectList,
        scheduleId: schdList.ScheduleId,
        mediumId: this.scheduleDetailsForm.mediumId,
        channelId: this.scheduleDetailsForm.channelId
    }
    });

    dialogRef.componentInstance.onListRefresh.subscribe((data) => {
      this.events = [];
      this.GetAllSchedulerList();
       this.refresh.next();
      this.dialog.closeAll();
    });
    dialogRef.afterClosed().subscribe(result => {
    });
    this.refresh.next();
  }

  addNewEvent() {
     const dialogRef = this.dialog.open(SchedulerAddComponent, {
       width: '550px',
       data: {
         data: 'hello',
         jobList: this.jobsList,
         policyList: this.policyList,
         projectList: this.projectList,
         scheduleId: 0,
         mediumId: this.scheduleDetailsForm.mediumId,
         channelId: this.scheduleDetailsForm.channelId,
         currentDate: this.dateForm.startDate
     }
     });
     dialogRef.componentInstance.onListRefresh.subscribe((data) => {
       this.events = [];
      this.GetAllSchedulerList();
       this.refresh.next();
       this.dialog.closeAll();
     });
     dialogRef.afterClosed().subscribe(result => {
       console.log(result);
     });
     this.refresh.next();
  }

  DeleteEvent(event) {
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
      this.schedulerService
      .Post(
        this.appurl.getApiUrl() + GLOBAL.API_Schedule_DeleteSchedule,
        event.id
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.events = this.events.filter(iEvent => iEvent !== event);
            this.refresh.next();
            this.toastr.success(data.Message);
          } else {
            this.toastr.error('Some error occured. Please try again later');
          }
          dialogRef.componentInstance.isLoading = false;
        },
        error => {
          dialogRef.componentInstance.isLoading = false;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
      dialogRef.componentInstance.onCancelPopup();
    });
  }

GetAllSchedulerList() {
  this.events = [];
  this.schedulerService
  .Post(
     this.appurl.getApiUrl() + GLOBAL.API_Scheduler_GetAllScheduleList, 'true'
   ).subscribe(response => {
    if (response.StatusCode === 200) {
      this.scheduleDetailList =  response.data.SchedulerList;
      if (this.isEditingAllowed) {
        this.PermissionAction(true, response.data.SchedulerList);
      } else {
        this.PermissionAction(false, response.data.SchedulerList);
      }
  } else {
      this.toastr.error('Some error occured. Please try again later.');
    }
  },
  error => {
    this.toastr.error('Some error occured. Please try again later.');
  });
}

  GetAllList() {
    this.schedulerService
    .Post(
       this.appurl.getApiUrl() + GLOBAL.API_Scheduler_GetAllPolicyScheduleList, 'test'
     ).subscribe(response => {
       if (response.StatusCode === 200) {
        this.policyList = response.data.policyList;
        this.projectList = response.data.ProjectDetailModel;
        this.jobsList = response.data.JobDetailsModel;
    } else {
      this.policyList = null;
      this.projectList = null;
      this.events = null;
    }
     },
     error => {
       this.toastr.error('Some error occured. Please try again later');
     });
  }

  GetMedium() {
    this.mediumList = [];
    this.masterService.GetList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetMedium)
    .subscribe(data => {
      if (data.StatusCode === 200) {
        this.mediumList =  data.data.Mediums;
        this.scheduleDetailsForm.mediumId = this.mediumList[0].MediumId;
        this.channelList = data.data.Channels.filter(x => x.MediumId == this.scheduleDetailsForm.mediumId);
        this.dateForm.startDate = this.currentDate;
        this.filterScheduler.MediumId = this.scheduleDetailsForm.mediumId;
       if (this.channelList.length > 0) {
        this.scheduleDetailsForm.channelId = data.data.channelById.ChannelId;
        this.filterScheduler.ChannelId = this.scheduleDetailsForm.channelId;
       } else {
        this.filterScheduler.ChannelId = 0;
        this.scheduleDetailsForm.channelId = 0;
       }
      } else {
        this.toastr.error('Some error occured. Please try again later.');
      }
    },
    error => {
      this.toastr.error('Some error occured. Please try again later.');
    });
  }

  selectionChanged(ev) {
     this.mediumId = ev.value;
     this.filterScheduler.MediumId = this.mediumId;
     if (this.mediumId !== 0) {
      this.channelList = [];
      this.events = [];
      this.masterService.Post(this.appurl.getApiUrl() + GLOBAL.API_Scheduler_GetChannelByMedium, this.mediumId)
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.channelList =  data.data.ChannelList;
          this.scheduleDetailsForm.channelId = this.channelList[0].ChannelId;
          this.filterScheduler.ChannelId =  this.scheduleDetailsForm.channelId;
        } else {
          this.toastr.error('Some error occured. Please try again later.');
        }
      },
      error => {
        this.toastr.error('Some error occured. Please try again later.');
      });
    }
  }

   selection(ev) {
    this.filterScheduler.ChannelId = ev;
    this.filterScheduler.StartDate = StaticUtilities.getLocalDate(this.dateForm.startDate);
    this.filterSchedulerList(this.filterScheduler);
  }

  filterSchedulerList(data) {
    this.events = [];
    this.filterScheduler.ChannelId = data.ChannelId;
    this.filterScheduler.StartDate = StaticUtilities.getLocalDate(data.StartDate);
    this.schedulerService.Post(this.appurl.getApiUrl() + GLOBAL.API_Scheduler_FilterScheduleList, this.filterScheduler)
    .subscribe(data => {
      if (data.StatusCode === 200) {
        this.scheduleDetailList =  data.data.SchedulerList;
        if (this.isEditingAllowed) {
          this.PermissionAction(true, data.data.SchedulerList);
        } else {
          this.PermissionAction(false, data.data.SchedulerList);
        }
    } else {
        this.toastr.error('Some error occured. Please try again later.');
      }
    },
    error => {
      this.toastr.error('Some error occured. Please try again later.');
    });
  }

  dateFilter(ev) {
    this.filterScheduler.StartDate = ev;
    this.filterSchedulerList(this.filterScheduler);
  }

  initForm() {
    this.scheduleDetailsForm = {
      mediumId: 0,
      channelId: 0
    };
    this.dateForm = {
      startDate: null
    };
  }

  refreshDate() {
    this.GetAllSchedulerList();
    this.GetMedium();
  }

  ngOnInit() {
   this.initForm();
   this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
   this.GetMedium();
   this.GetAllList();
   this.GetAllSchedulerList();
  }

  PermissionAction(ev, element) {
    switch (ev) {
    case true:
    element.forEach(element => {
      const eventStartDate: any = new Date()
      .setHours(Number(element.StartTime.split(':')[0]), Number(element.StartTime.split(':')[1]), 0, 0 );
      const eventEndDate: any = new Date()
      .setHours(Number(element.EndTime.split(':')[0]), Number(element.EndTime.split(':')[1]), 0, 0 );
        this.events.push({
          id: element.ScheduleId,
          start: new Date(eventStartDate),
          end: new Date(eventEndDate),
          title: element.StartTime + '-' + element.EndTime + '-' + element.Name,
          color: colors.yellow,
          actions: this.actionsEdit
        });
    this.refresh.next();
  });
      break;
    case false:
   element.forEach(element => {
      const eventStartDate: any = new Date()
      .setHours(Number(element.StartTime.split(':')[0]), Number(element.StartTime.split(':')[1]), 0, 0 );
      const eventEndDate: any = new Date()
      .setHours(Number(element.EndTime.split(':')[0]), Number(element.EndTime.split(':')[1]), 0, 0 );
        this.events.push({
          id: element.ScheduleId,
          start: new Date(eventStartDate),
          end: new Date(eventEndDate),
          title: element.StartTime + '-' + element.EndTime + '-' + element.Name,
          color: colors.yellow,
          actions: this.actionsView
        });
    this.refresh.next();
  });
      break;
    }
  }
}
