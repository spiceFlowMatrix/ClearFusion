import {
  Component,
  OnInit,
  ViewChild,
  TemplateRef,
  Inject
} from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { HrService } from 'src/app/hr/services/hr.service';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormArray,
  FormControl
} from '@angular/forms';
import { HolidayType } from 'src/app/shared/enum';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import {
  RepeatWeeklyDay,
  IOfficeListModel
} from 'src/app/hr/models/employee-holiday.model';
import { TableActionsModel } from 'projects/library/src/public_api';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-holidays',
  templateUrl: './holidays.component.html',
  styleUrls: ['./holidays.component.scss']
})
export class HolidaysComponent implements OnInit {
  @ViewChild('addHoliday') addHoliday: TemplateRef<any>;
  @ViewChild('addWeeklyHoliday') addWeeklyHoliday: TemplateRef<any>;

  holidayList$: Observable<any[]>;
  holidayListHeaders$ = of(['Id', 'Name', 'Date', 'Office']);
  actions: TableActionsModel;

  holidayId: any;
  selectedDate = new Date();
  startAt = new Date();
  minDate = new Date();
  maxDate = new Date(new Date().setMonth(new Date().getMonth() + 1));
  year: any;
  DayAndDate: string;
  holidays: number[] = [];
  addHolidayForm: FormGroup;
  isFormSubmitted = false;
  repeatWeeklyDay: RepeatWeeklyDay[] = [];
  officeList: IOfficeListModel[] = [];
  holidayFormWeeklyDataFlag = false;

  hideColums = of({
    headers: ['Id', 'Name', 'Date', 'Office'],
    items: ['Id', 'Name', 'DateToDisplay', 'Office']
  });
  RecordCount: number;
  pageModel = {
    PageSize: 10,
    PageIndex: 0
  };

  ifAddHolidayFalg = true;

  // weekly holiday

  addWeeklyHolidayForm: FormGroup;
  ifAddWeeklyHolidayFalg = true;
  isWeeklyFormValidFlag = false;
  constructor(
    public dialog: MatDialog,
    public hrservice: HrService,
    public fb: FormBuilder,
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService
  ) {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        download: false,
        edit: true,
        delete: true
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false
      }
    };
  }

  ngOnInit() {
    this.getAllHolidays();
    this.initForm();
    this.getOfficeList();
    this.initWeeklyHolidayForm();
    // this.getWeeklyHolidays(this.);
  }

  initForm() {
    this.addHolidayForm = this.fb.group({
      HolidayId: [null],
      HolidayName: ['', [Validators.required]],
      Remarks: [''],
      Date: [''],
      HolidayTypeId: ['']
    });
  }

  initWeeklyHolidayForm() {
    this.addWeeklyHolidayForm = this.fb.group({
      Sun: [null,  this.NullCheckValidator.bind(this)],
      Mon: [null,  this.NullCheckValidator.bind(this)],
      Tue: [null, this.NullCheckValidator.bind(this)],
      Wed: [null, this.NullCheckValidator.bind(this)],
      Thu: [null, this.NullCheckValidator.bind(this)],
      Fri: [null, this.NullCheckValidator.bind(this)],
      Sat: [null, this.NullCheckValidator.bind(this)]
    });
  }

  addHolidayPopup() {
    this.ifAddHolidayFalg = true;
    this.initForm();
    const control = new FormControl([]);
    this.addHolidayForm.addControl('OfficeId', control);
    this.addHolidayForm.controls['OfficeId'].setValidators(Validators.required);
    const diagRef = this.dialog.open(this.addHoliday, {
      width: '500px'
    });
  }

  addWeekendPopup() {
    this.ifAddWeeklyHolidayFalg = true;
    this.initWeeklyHolidayForm();
    // const control = new FormControl([]);
    // this.addWeeklyHolidayForm.addControl('OfficeId', control);
    // this.addWeeklyHolidayForm.controls['OfficeId'].setValidators(
    //   Validators.required
    // );
    const diagRef = this.dialog.open(this.addWeeklyHoliday, {
      width: '600px'
    });
  }

  //#region "cancel popup"
  onCancelPopup() {
    this.dialog.closeAll();
  }
  //#endregion

  //#region "Calander" event
  onSelect(event) {
    this.selectedDate = event;
    const dateString = event.toDateString();
    const dateValue = dateString.split(' ');
    this.year = dateValue[3];
    this.DayAndDate =
      dateValue[0] + ',' + ' ' + dateValue[1] + ' ' + dateValue[2];
  }
  cfDateFilter(d: Date): boolean {
    const day = d.getDay();
    // Prevent Saturday and Sunday from being selected.
    return day !== 0 && day !== 6;
  }
  //#endregion

  //#region "getAllHolidays"
  getAllHolidays() {
    this.commonLoader.showLoader();
    this.hrservice.getAllHolidaysList(this.pageModel).subscribe(
      res => {
        console.log(res);
        if (res.holidaylist.length > 0 && res !== undefined) {
          this.holidayList$ = of(
            res.holidaylist.map(element => {
              return {
                Id: element.HolidayId,
                Name: element.HolidayName,
                DateToDisplay: element.DateToDisplay,
                Office: element.OfficeName,
                OfficeId: element.OfficeId,
                Remarks: element.Remarks,
                Date: element.Date,
                HolidayTypeId: element.HolidayType
              };
            })
          );
          this.RecordCount = res.totalCount;
          this.commonLoader.hideLoader();
        }
      },
      error => {
        this.toastr.warning(error);
        this.commonLoader.hideLoader();
      }
    );
  }
  //#endregion

  //#region  "Get weekly holiday"
  getWeeklyHolidays() {
    // if (officeId != null && officeId != undefined) {
    this.hrservice.getWeeklyHolidaysList().subscribe(response => {
      if (
        response.data !== undefined &&
        response.data != null &&
        response.data.HolidayWeeklyDetailsList.length > 0
      ) {
        response.data.HolidayWeeklyDetailsList.forEach(element => {
          if (element.Day === 'Sunday') {
            this.addWeeklyHolidayForm.value.Sun = true;
          }
          if (element.Day === 'Monday') {
            this.addWeeklyHolidayForm.value.Mon = true;
          }
          if (element.Day === 'Tuesday') {
            this.addWeeklyHolidayForm.value.Tue = true;
          }
          if (element.Day === 'Wednesday') {
            this.addWeeklyHolidayForm.value.Wed = true;
          }
          if (element.Day === 'Thursday') {
            this.addWeeklyHolidayForm.value.Thu = true;
          }
          if (element.Day === 'Friday') {
            this.addWeeklyHolidayForm.value.Fri = true;
          }
          if (element.Day === 'Saturday') {
            this.addWeeklyHolidayForm.value.Sat = true;
          }
        });
        this.holidayFormWeeklyDataFlag = true;
      }
    });
    // }
  }
  //#endregion

  //#region "getOfficeList"
  getOfficeList() {
    this.hrservice.getAllOfficeCodeList().subscribe(
      response => {
        this.officeList = [];
        if (response.StatusCode === 200 && response.data !== null) {
          response.data.OfficeDetailsList.forEach(element => {
            this.officeList.push({
              OfficeId: element.OfficeId,
              OfficeName: element.OfficeName
            });
          });
        }
      },
      error => {}
    );
  }
  //#endregion

  //#region "pageEvent"
  pageEvent(e) {
    this.pageModel.PageIndex = e.pageIndex;
    this.pageModel.PageSize = e.pageSize;
    this.getAllHolidays();
  }
  //#endregion

  //#region "onparticular holiday office change"
  onOfficeChange(data: any) {
    if (data.value.length > 0 && data.value !== undefined) {
      this.addHolidayForm.value.OfficeId = data.value;
      // this.getWeeklyHolidays(data.value);
    }
  }
  //#endregion

  // //#region "weelky holiday addWeekendPopup"
  // onWeeklyHolidayOfficeChange(data: any) {
  //   if (data.value.length > 0 && data.value !== undefined) {
  //     this.addWeeklyHolidayForm.value.OfficeId = data.value;
  //   }
  // }
  // //#endregion

  //#region "saveHolidayForm for daily and weekly"
  saveHolidayForm(event: any, formData: any) {
    debugger;
    if (formData === true) {
      // Note: add holiday in particular day
      if (event === 'ParticularDay' && formData === true) {
      this.isFormSubmitted = true;
        this.addHolidayForm.value.HolidayTypeId = HolidayType.ParticularDay;
        const finalData: any = {
          HolidayId: this.addHolidayForm.value.HolidayId,
          Date: StaticUtilities.getLocalDate(this.selectedDate),
          HolidayName: this.addHolidayForm.value.HolidayName,
          Remarks: this.addHolidayForm.value.Remarks,
          HolidayType: this.addHolidayForm.value.HolidayTypeId,
          RepeatWeeklyDay: this.repeatWeeklyDay,
          OfficeId: this.addHolidayForm.value.OfficeId
        };

        if (
          this.addHolidayForm.value.HolidayId === 0 ||
          this.addHolidayForm.value.HolidayId === '' ||
          this.addHolidayForm.value.HolidayId == null
        ) {
          this.addHolidayData(finalData);
        } else {
          this.editHolidayDate(finalData);
        }
      } else if (event === 'Weekly') {
        // Note: add weekly holiday
        if (this.isWeeklyFormValidFlag === true) {
          this.isFormSubmitted = true;
          this.repeatWeeklyDay = [];
          if (this.addWeeklyHolidayForm.value.Sun === true) {
            this.repeatWeeklyDay.push({
              Day: 'Sunday'
            });
          }
          if (this.addWeeklyHolidayForm.value.Mon === true) {
            this.repeatWeeklyDay.push({
              Day: 'Monday'
            });
          }
          if (this.addWeeklyHolidayForm.value.Tue === true) {
            this.repeatWeeklyDay.push({
              Day: 'Tuesday'
            });
          }
          if (this.addWeeklyHolidayForm.value.Wed === true) {
            this.repeatWeeklyDay.push({
              Day: 'Wednesday'
            });
          }
          if (this.addWeeklyHolidayForm.value.Thu === true) {
            this.repeatWeeklyDay.push({
              Day: 'Thursday'
            });
          }
          if (this.addWeeklyHolidayForm.value.Fri === true) {
            this.repeatWeeklyDay.push({
              Day: 'Friday'
            });
          }
          if (this.addWeeklyHolidayForm.value.Sat === true) {
            this.repeatWeeklyDay.push({
              Day: 'Saturday'
            });
          }

          const weeklyfinalData: any = {
            HolidayId: 0,
            HolidayType: HolidayType.Weekly,
            RepeatWeeklyDay: this.repeatWeeklyDay,
            Date: new Date()
            // OfficeId: this.addWeeklyHolidayForm.value.OfficeId
          };
          // if (
          //   this.addHolidayForm.value.HolidayId === 0 ||
          //   this.addHolidayForm.value.HolidayId === '' ||
          //   this.addHolidayForm.value.HolidayId == null
          // ) {
          this.addHolidayData(weeklyfinalData);
          // } else {
          //   this.editHolidayDate(weeklyfinalData);
          // }
          this.isFormSubmitted = false;
        }
      } else {
      this.isFormSubmitted = false;
      }
    } else {
      this.isFormSubmitted = false;
    }
  }
  //#endregion

  //#region "addHolidayData" for particular day and weekly
  addHolidayData(finalData: any) {
    this.hrservice.addHoliday(finalData).subscribe(
      x => {
        if (x != null && x != undefined) {
          this.isFormSubmitted = false;
          this.initForm();
          this.dialog.closeAll();
          this.getAllHolidays();
          this.getWeeklyHolidays();
        } else {
          this.isFormSubmitted = false;
        }
      },
      error => {
        this.toastr.warning(error);
        this.isFormSubmitted = false;
      }
    );
  }
  //#endregion

  //#region "Edit holiday detail" for particular day and weekly holiday
  editHolidayDate(finalData: any) {
    this.hrservice.editHoliday(finalData).subscribe(
      x => {
        if (x != null && x !== undefined) {
          this.isFormSubmitted = false;
          this.initForm();
          this.dialog.closeAll();
          this.getAllHolidays();
        } else {
          this.isFormSubmitted = false;
        }
      },
      error => {
        this.toastr.warning(error);
        this.isFormSubmitted = false;
      }
    );
  }
  //#endregion

  //#region action event delete and edit
  actionEvents(event: any) {
    if (event.type === 'delete') {
      this.hrservice.openDeleteDialog().subscribe(res => {
        if (res === true) {
          this.holidayId = event.item.Id;
          this.hrservice.deleteHoliday(this.holidayId).subscribe(response => {
            if (response.StatusCode === 200) {
              this.getAllHolidays();
            }
          });
        }
      });
    }
    if (event.type === 'edit') {
      this.initForm();
      const control = new FormControl(null);
      this.addHolidayForm.addControl('OfficeId', control);
      this.ifAddHolidayFalg = false;
      if (event.item !== undefined) {
        this.addHolidayForm.get('HolidayId').setValue(event.item.Id);
        this.addHolidayForm.get('HolidayName').setValue(event.item.Name);
        this.addHolidayForm.get('Remarks').setValue(event.item.Remarks);
        this.addHolidayForm
          .get('HolidayTypeId')
          .setValue(event.item.HolidayTypeId);
        this.addHolidayForm.controls['OfficeId'].setValue(event.item.OfficeId);
      }
      this.selectedDate = event.item.Date;
      this.addHolidayForm.get('Date').setValue(this.selectedDate);

      const dialogRef = this.dialog.open(this.addHoliday, {
        width: '450px',
        data: event.item
      });
      dialogRef.afterClosed().subscribe(x => {
        this.getAllHolidays();
      });
    }
  }
  //#endregion
  NullCheckValidator(control: FormControl) {
    debugger;
    if (control.value != null ) {
      this.isWeeklyFormValidFlag = true;
    } else {
      this.isWeeklyFormValidFlag = false;
    }
  }
}
