import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { of, Observable } from 'rxjs';
import { EmployeeListService } from 'src/app/hr/services/employee-list.service';
import { AttendanceService } from 'src/app/hr/services/attendance.service';
import { MatDialog } from '@angular/material';
import { AddPayrollHoursComponent } from '../add-payroll-hours/add-payroll-hours.component';

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
  constructor(private toastr: ToastrService,
    private employeeListService: EmployeeListService,
    private attendanceService: AttendanceService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.getOfficeList();
  }

  ngOnChanges() {
    this.getAttendanceGroupDetailById();
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
