import {
  Component,
  OnInit,
  Input,
  HostListener,
  Output,
  EventEmitter,
  ChangeDetectorRef
} from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { BroadcastPolicyService } from '../service/broadcast-policy.service';
import { MatDialog } from '@angular/material/dialog';
import { GLOBAL } from 'src/app/shared/global';

import {
  PolicyModel,
  PolicyTimeModel,
  PolicyDayModel,
  PolicyOrderScheduleModel
} from '../model/policy-model';
import {
  CalendarView,
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent
} from 'angular-calendar';
import { StartEndTimeComponent } from '../start-end-time/start-end-time.component';
import moment from 'moment-timezone';
import { isSameMonth, isSameDay } from 'date-fns';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { ProducerModel } from '../../master-pages/model/mastrer-pages.model';
import {
  LanguageModel,
  MediaCategoryModel,
  MediumModel
} from '../../contracts/model/contract-details.model';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { ContractsService } from '../../contracts/service/contracts.service';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { Subject } from 'rxjs/internal/Subject';

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
  // changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-broadcast-policy-details',
  templateUrl: './broadcast-policy-details.component.html',
  styleUrls: ['./broadcast-policy-details.component.scss']
})
export class BroadcastPolicyDetailsComponent implements OnInit {
  private _cdr: ChangeDetectorRef;
  @Input() policyId: any;
  @Input() isEditingAllowed: boolean;
  policyDetailLoaderFlag = false;
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  mediums: MediumModel[];
  mediaCategories: MediaCategoryModel[];
  languages: LanguageModel[];
  producers: ProducerModel[];
  policyDetailsForm: any;
  orderScheduleForm: any;
  timeModel: PolicyTimeModel = {};
  orderScheduleModel: PolicyOrderScheduleModel = {};
  dayModel: PolicyDayModel = {};
  policyDetails: PolicyModel = {};
  updatePolicyLoaderFlag = false;
  repeatDaysLoaderFlag = false;
  disableControlPermission = false;
  disableDayBtn = false;
  pageId = ApplicationPages.Policy;
  selectedDays: any[] = [];
  isOrderScheduleAllowed = false;
  @Output() deletePolicy = new EventEmitter<any>();
  @Output() updatePolicyDetails = new EventEmitter<any>();
  days = [
    { id: '1', Value: 'MON', status: false },
    { id: '2', Value: 'TUE', status: false },
    { id: '3', Value: 'WED', status: false },
    { id: '4', Value: 'THU', status: false },
    { id: '5', Value: 'FRI', status: false },
    { id: '6', Value: 'SAT', status: false },
    { id: '7', Value: 'SUN', status: false }
  ];

  view: CalendarView = CalendarView.Day;

  CalendarView = CalendarView;

  viewDate: Date = new Date();
  modalData: {
    action: string;
    event: CalendarEvent;
  };

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fa fa-fw fa-pencil"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      }
    },
    {
      label: '<i class="fa fa-fw fa-times"></i>',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        // this.events = this.events.filter(iEvent => iEvent !== event);
        this.handleEvent('Deleted', event);
      }
    }
  ];

  refresh: Subject<any> = new Subject();

  events: CalendarEvent[] = [];

  activeDayIsOpen = true;
  // events: CalendarEvent[] = [];
  InitilizeSchedular() {
    this.activeDayIsOpen = true;
  }
  // tslint:disable-next-line:max-line-length
  constructor(
    private contractService: ContractsService,
    private toastr: ToastrService,
    private policyService: BroadcastPolicyService,
    public dialog: MatDialog,
    private appurl: AppUrlService,
    private localStorageService: LocalStorageService
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.initForm();
    this.MasterPageValues();
    this.isOrderScheduleAllowed = this.localStorageService.IsOrderScheduleAllowed(
      this.pageId
    );
  }

  initForm() {
    this.policyDetailsForm = {
      producerId: 0,
      mediumId: 0,
      categoryId: 0,
      languageId: 0,
      policyId: 0,
      policyName: '',
      description: ''
    };
    this.orderScheduleForm = {
      policyId: 0,
      id: 0,
      startDate: '',
      endDate: ''
    };
  }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    if (this.policyId !== 0 && this.policyId !== undefined) {
      this.GetPolicyById(this.policyId);
      this.getDayScheduleByPolicyId(this.policyId);
      this.getTimeSchedule(this.policyId);
      if (!this.isEditingAllowed) {
        this.disableControlPermission = true;
        this.disableDayBtn = true;
      }
    } else {
    }
  }

  getDayScheduleByPolicyId(id) {
    this.days.forEach(element => {
      const day = this.days.find(x => x.Value === element.Value);
      day.status = false;
    });
    this.policyService
      .Post(
        this.appurl.getApiUrl() + GLOBAL.API_Policy_GetDayScheduleByPolicyId,
        id
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (
              data.data.policyTimeDetailsById !== null &&
              data.data.policyTimeDetailsById !== undefined
            ) {
              if (
                data.data.policyTimeDetailsById.repeatDays !== null &&
                data.data.policyTimeDetailsById.repeatDays !== undefined
              ) {
                this.dayModel = data.data.policyTimeDetailsById;
                data.data.policyTimeDetailsById.repeatDays.forEach(element => {
                  // tslint:disable-next-line:no-unused-expression
                  const day = this.days.find(x => x.Value === element);
                  day.status = true;
                });
              }
            }
          } else {
            // this.toastr.error('Some error occured. Please try again later');
          }
        },
        error => {
          // this.toastr.error('Some error occured. Please try again later');
        }
      );
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

  getTimeSchedule(id) {
    this.events = [];
    this.policyDetailLoaderFlag = true;
    this.policyService
      .Post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Policy_GetGetPolicyTimeScheduleList,
        id
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            data.data.policySchedulesByTimeList.forEach(element => {
              // tslint:disable-next-line:max-line-length
              const eventStartDate: any = new Date().setHours(
                Number(element.StartTime.split(':')[0]),
                Number(element.StartTime.split(':')[1]),
                0,
                0
              );
              // tslint:disable-next-line:max-line-length
              const eventEndDate: any = new Date().setHours(
                Number(element.EndTime.split(':')[0]),
                Number(element.EndTime.split(':')[1]),
                0,
                0
              );

              this.events.push({
                id: element.Id,

                start: new Date(eventStartDate),
                end: new Date(eventEndDate),
                title: element.StartTime + '-' + element.EndTime,
                color: colors.yellow,
                actions: this.actions
                // resizable: {
                // beforeStart: true,
                // afterEnd: true
                // },
                // draggable: true
              });
              this.refresh.next();
            });
          } else {
            // this.toastr.error('Some error occured. Please try again later.');
          }
          this.policyDetailLoaderFlag = false;
        },
        error => {
          this.policyDetailLoaderFlag = false;
          // this.toastr.error('Some error occured. Please try again later');
        }
      );
  }

  MasterPageValues() {
    this.contractService
      .GetMasterPagesList(
        this.appurl.getApiUrl() + GLOBAL.API_Contract_GetMasterPagesValues
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.mediums = data.data.Mediums;
            this.mediaCategories = data.data.MediaCategories;
            this.languages = data.data.Languages;
            this.producers = data.data.Producers;
          } else {
            this.toastr.error('Some error occured. Please try again later.');
          }
          // this.SetDefaultValues();
        },
        error => {
          this.toastr.error('Some error occured. Please try again later');
        }
      );
  }

  GetPolicyById(id) {
    // this.policyDetailLoaderFlag = true;

    // tslint:disable-next-line:max-line-length
    this.policyService
      .GetPolicyById(
        this.appurl.getApiUrl() + GLOBAL.API_Policy_GetPolicyById,
        id
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.policyDetailLoaderFlag = false;
            this.SetPolicyValues(data.data.policyDetailsById);
          } else {
            this.toastr.error('Some error occured. Please try again later');
          }
        },
        error => {
          this.policyDetailLoaderFlag = false;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
  }

  SetPolicyValues(data) {
    this.policyDetailsForm.producerId = data.ProducerId;
    this.policyDetailsForm.categoryId = data.MediaCategoryId;
    this.policyDetailsForm.languageId = data.LanguageId;
    this.policyDetailsForm.mediumId = data.MediumId;
    this.policyDetailsForm.policyName = data.PolicyName;
    this.policyDetailsForm.policyId = data.PolicyId;
    this.policyDetailsForm.description = data.Description;
    this.policyDetails.ProducerId = data.ProducerId;
    this.policyDetails.MediaCategoryId = data.MediaCategoryId;
    this.policyDetails.MediumId = data.MediumId;
    this.policyDetails.LanguageId = data.LanguageId;
    this.policyDetails.PolicyName = data.PolicyName;
    this.policyDetails.PolicyId = data.PolicyId;
    this.policyDetails.Description = data.Description;
    this.policyDetails.PolicyCode = data.PolicyCode;
  }

  selectionChanged() {
    this.updatePolicyLoaderFlag = true;
    this.policyDetails.PolicyId = this.policyDetailsForm.policyId;
    this.policyDetails.ProducerId = this.policyDetailsForm.producerId;
    this.policyDetails.PolicyName = this.policyDetailsForm.policyName;
    this.policyDetails.MediumId = this.policyDetailsForm.mediumId;
    this.policyDetails.MediaCategoryId = this.policyDetailsForm.categoryId;
    this.policyDetails.LanguageId = this.policyDetailsForm.languageId;
    this.policyDetails.Description = this.policyDetailsForm.description;
    this.policyService
      .AddPolicy(
        this.appurl.getApiUrl() + GLOBAL.API_Policy_AddNewPolicy,
        this.policyDetails
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success(data.Message);
            const mediumDetails = this.mediums.find(
              x => x.MediumId === this.policyDetails.MediumId
            );
            this.policyDetails.MediumName = mediumDetails.MediumName;
            this.updatePolicyDetails.emit(this.policyDetails);
          } else {
            this.toastr.error('Some error occured. Please try again later');
          }
          this.updatePolicyLoaderFlag = false;
        },
        error => {
          this.updatePolicyLoaderFlag = false;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
  }

  selectDays(value) {
    this.disableDayBtn = true;
    this.repeatDaysLoaderFlag = true;
    const day1 = this.days.find(x => x.Value === value);
    if (day1.status === true) {
      day1.status = false;
    } else {
      day1.status = true;
    }
    this.selectedDays.push(day1);
    this.timeModel.RepeatDays = this.days;
    this.timeModel.PolicyId = this.policyId;
    this.timeModel.Id = this.dayModel.Id;
    this.policyService
      .Post(
        this.appurl.getApiUrl() + GLOBAL.API_Policy_AddPolicyRepeatDays,
        this.timeModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.getDayScheduleByPolicyId(this.policyId);
            this.dayModel = data.data.policyDayScheduleDetails;
            this.timeModel = {};
            this.toastr.success(data.Message);
          } else {
            this.toastr.error('Some error occured. Please try again later');
          }
          this.disableDayBtn = false;
          this.repeatDaysLoaderFlag = false;
        },
        error => {
          this.disableDayBtn = false;
          this.repeatDaysLoaderFlag = false;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
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
    if (action === 'Deleted') {
      this.openDeleteDialog(event);
    }
    if (action === 'Edited') {
      this.openAddTimeDialog(event);
    }
    // this.modal.open(this.modalContent, { size: 'lg' });
  }

  openDeleteDialog(event) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: 'delete',
      disableClose: false
    });

    dialogRef.componentInstance.confirmMessage =
      Delete_Confirmation_Texts.deleteText1;

    dialogRef.componentInstance.confirmText = Delete_Confirmation_Texts.yesText;

    dialogRef.componentInstance.cancelText = Delete_Confirmation_Texts.noText;

    dialogRef.afterClosed().subscribe(result => {});

    dialogRef.componentInstance.confirmDelete.subscribe(res => {
      this.updatePolicyLoaderFlag = true;
      this.policyService
        .Post(
          this.appurl.getApiUrl() + GLOBAL.API_Policy_DeletePolicyTimeSchedule,
          event.id
        )
        .subscribe(
          data => {
            if (data.StatusCode === 200) {
              this.events = this.events.filter(iEvent => iEvent !== event);
              this.toastr.success(data.Message);
              this.timeModel = {};
            } else {
              this.toastr.error('Some error occured. Please try again later');
            }
            this.updatePolicyLoaderFlag = false;
          },
          error => {
            this.updatePolicyLoaderFlag = false;
            this.toastr.error('Some error occured. Please try again later');
          }
        );
      dialogRef.componentInstance.onCancelPopup();
    });
  }

  openAddTimeDialog(event): void {
    if (event !== undefined) {
      this.timeModel.Id = event.id;
    }
    const dialogRef = this.dialog.open(StartEndTimeComponent, {
      width: '550px',
      data: {
        data: 'hello',
        id: this.timeModel.Id
      }
    });
    dialogRef.componentInstance.onListRefresh.subscribe(data => {
      this.timeModel.PolicyId = this.policyId;
      this.timeModel.StartTime = data.startTime;
      this.timeModel.EndTime = data.endTime;
      // tslint:disable-next-line:max-line-length
      const eventStartDate: any = new Date().setHours(
        Number(this.timeModel.StartTime.split(':')[0]),
        Number(this.timeModel.StartTime.split(':')[1]),
        0,
        0
      );
      // tslint:disable-next-line:max-line-length
      const eventEndDate: any = new Date().setHours(
        Number(this.timeModel.EndTime.split(':')[0]),
        Number(this.timeModel.EndTime.split(':')[1]),
        0,
        0
      );
      this.updatePolicyLoaderFlag = true;
      if (eventStartDate > eventEndDate) {
        this.updatePolicyLoaderFlag = false;
        this.toastr.error('Start Time can not be greater than End Time');
      } else {
        this.policyService
          .Post(
            this.appurl.getApiUrl() +
              GLOBAL.API_Policy_AddNewPolicyTimeSchedule,
            this.timeModel
          )
          .subscribe(
            result => {
              if (result.StatusCode === 200) {
                this.toastr.success(result.Message);
                if (this.timeModel.Id) {
                  this.events = this.events.filter(iEvent => iEvent !== event);
                }
                this.events.push({
                  id: result.data.policyTimeScheduleDetails.Id,
                  start: new Date(eventStartDate),
                  // tslint:disable-next-line:radix
                  end: new Date(eventEndDate),
                  // tslint:disable-next-line:radix
                  // start: addHours(
                  //   startOfDay(new Date()),
                  //   // tslint:disable-next-line:radix
                  //   parseInt(data.startTime)
                  // ),
                  // tslint:disable-next-line:radix
                  // end: addHours(startOfDay(new Date()), parseInt(data.endTime)),
                  title: data.startTime + '-' + data.endTime,
                  color: colors.yellow,
                  actions: this.actions
                  // resizable: {
                  //   beforeStart: true,
                  //   afterEnd: true
                  // },
                  // draggable: true
                });
                this.timeModel = {};
                this.refresh.next();
              } else {
                this.toastr.error(result.Message);
              }
              this.updatePolicyLoaderFlag = false;
            },
            error => {
              this.updatePolicyLoaderFlag = false;
              this.toastr.error('Some error occured. Please try again later');
            }
          );
      }
    });

    dialogRef.afterClosed().subscribe(result => {});
  }

  RequestOrderSchedule() {
    this.orderScheduleModel.PolicyId = this.policyId;
    this.orderScheduleModel.startDate = new Date(
      Date.UTC(
        this.orderScheduleForm.startDate.getFullYear(),
        new Date(this.orderScheduleForm.startDate).getMonth(),
        new Date(this.orderScheduleForm.startDate).getDate()
      )
    );
    this.orderScheduleModel.endDate = new Date(
      Date.UTC(
        this.orderScheduleForm.endDate.getFullYear(),
        new Date(this.orderScheduleForm.endDate).getMonth(),
        new Date(this.orderScheduleForm.endDate).getDate()
      )
    );
    if (this.orderScheduleModel.startDate > this.orderScheduleModel.endDate) {
      this.toastr.error('Start Date can not be greater than End Date');
    } else {
      this.policyService
        .Post(
          this.appurl.getApiUrl() + GLOBAL.API_Policy_AddEditOrderSchedule,
          this.orderScheduleModel
        )
        .subscribe(data => {
          if (data.StatusCode === 200) {
            this.toastr.success(data.Message);
            this.orderScheduleForm.endDate = '';
            this.orderScheduleForm.startDate = '';
          } else {
            this.toastr.error(data.Message);
            this.orderScheduleForm.endDate = '';
            this.orderScheduleForm.startDate = '';
          }
        });
    }
  }
}
