import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { of, Observable } from 'rxjs';
import { EmployeeListService } from 'src/app/hr/services/employee-list.service';
import { AttendanceService } from 'src/app/hr/services/attendance.service';
import { MatDialog } from '@angular/material';
import { AddPayrollHoursComponent } from '../add-payroll-hours/add-payroll-hours.component';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { Month } from 'src/app/shared/enum';
import { AddAttendanceGroupComponent } from '../add-attendance-group/add-attendance-group.component';

@Component({
  selector: 'app-attendance-group-detail',
  templateUrl: './attendance-group-detail.component.html',
  styleUrls: ['./attendance-group-detail.component.scss']
})
export class AttendanceGroupDetailComponent implements OnInit, OnChanges {

  @Input() attendanceGroupId;
  @Output() backClick = new EventEmitter();
  officeDropdown$: Observable<any>;
  attendanceGroupDetail: IAttendanceGroupDetail = {AttendanceGroupId: 0, CreatedBy: '', ModifiedBy: '', CreatedDate: null,
  ModifiedDate: null, Name: '', Description: ''};
  selectedOffice = { value: 0, name: 'ADD FOR SELECTED OFFICE' };
  payrollList = [];
  constructor(private toastr: ToastrService,
    private employeeListService: EmployeeListService,
    private attendanceService: AttendanceService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.getOfficeList();
  }

  ngOnChanges() {
    this.getAttendanceGroupDetailById();
    this.getPayrollHoursByAttendanceGroup();
  }

  onBackClick() {
    this.backClick.emit();
  }

  getOfficeList() {
    this.employeeListService
      .GetAllOfficeCodeList()
      .subscribe(
        data => {
          if (data.data.OfficeDetailsList != null) {
            this.officeDropdown$ = of(data.data.OfficeDetailsList.map(y => {
              return {
                value: y.OfficeId,
                name: y.OfficeName
              };
            }));
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  selectedOfficeChanged(office) {
    const dialogRef = this.dialog.open(AddPayrollHoursComponent, {
      width: '500px',
      data: {SelectedOffice: office, AttendanceGroupId: this.attendanceGroupId, AttendanceGroupName: this.attendanceGroupDetail.Name}
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getPayrollHoursByAttendanceGroup();
    });
  }

  getAttendanceGroupDetailById() {
    this.attendanceService.getAttendanceGroupDetailById(this.attendanceGroupId).subscribe(res => {
      if (res && res.AttendanceGroupDetail) {
        this.attendanceGroupDetail = res.AttendanceGroupDetail;
      }
    }, err => {

    });
  }

  addForAllOffice() {
    const dialogRef = this.dialog.open(AddPayrollHoursComponent, {
      width: '500px',
      data: {AttendanceGroupId: this.attendanceGroupId, AttendanceGroupName: this.attendanceGroupDetail.Name}
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getPayrollHoursByAttendanceGroup();
    });
  }

  getPayrollHoursByAttendanceGroup() {
    this.attendanceService.getPayrollHoursByAttendanceGroup(this.attendanceGroupId).subscribe(res => {
      if (res && res.PayrollList) {
        this.payrollList = [];
        res.PayrollList.forEach(element => {
          this.payrollList.push({
            PayrollMonthlyHourId: element.PayrollMonthlyHourId,
            Office: element.Office,
            OfficeId: element.OfficeId,
            InTime: StaticUtilities.setLocalDate(element.InTime),
            OutTime: StaticUtilities.setLocalDate(element.OutTime),
            PayrollMonth: Month[element.PayrollMonth],
            Month: element.PayrollMonth,
            PayrollYear: element.PayrollYear,
            WorkingTime: element.WorkingTime,
            AttendanceGroupId: element.AttendanceGroupId
          });
        });
      }
    }, err => {

    });
  }

  editAttendanceGroup() {
    const dialogRef = this.dialog.open(AddAttendanceGroupComponent, {
      width: '450px',
      data: {AttendanceGroupId: this.attendanceGroupDetail.AttendanceGroupId, Name: this.attendanceGroupDetail.Name,
        Description: this.attendanceGroupDetail.Description}
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getAttendanceGroupDetailById();
    });
  }

  editPayrollHours(PayrollMonthlyHourId, InTime, OutTime, PayrollMonth, PayrollYear, OfficeId, Office) {
    const dialogRef = this.dialog.open(AddPayrollHoursComponent, {
      width: '500px',
      data: {SelectedOffice: {value: OfficeId, name: Office},
        AttendanceGroupId: this.attendanceGroupId,
        AttendanceGroupName: this.attendanceGroupDetail.Name,
        PayrollMonthlyHourId: PayrollMonthlyHourId,
        InTime: InTime,
        OutTime: OutTime,
        PayrollMonth: PayrollMonth,
        PayrollYear: PayrollYear
      }
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getPayrollHoursByAttendanceGroup();
    });
  }
}

export interface IAttendanceGroupDetail {
  AttendanceGroupId;
  CreatedDate;
  ModifiedDate;
  CreatedBy;
  ModifiedBy;
  Name;
  Description;
}
