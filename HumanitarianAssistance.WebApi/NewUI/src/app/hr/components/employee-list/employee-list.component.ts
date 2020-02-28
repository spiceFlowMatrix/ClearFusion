import { Component, OnInit, ViewChild, ElementRef, ViewEncapsulation } from '@angular/core';
import { EmployeeListService } from '../../services/employee-list.service';
import { Observable, of } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { EmployeeFilterModel, EmployeeDetailList } from '../../models/employee-list.model';
import { EmploymentStatus, Month } from 'src/app/shared/enum';
import { MatTableDataSource, MatDatepicker, MatDialog, MatOption } from '@angular/material';
import { Router } from '@angular/router';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { SetEmployeeAttendanceComponent } from '../set-employee-attendance/set-employee-attendance.component';
import { AttendanceService } from '../../services/attendance.service';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { AddSalaryConfigurationComponent } from '../employee-salary-config/add-salary-configuration/add-salary-configuration.component';
import { IncrementDecrementSalaryComponent } from '../employee-salary-config/increment-decrement-salary/increment-decrement-salary.component';
import { AdministerPayrollComponent } from '../administer-payroll/administer-payroll.component';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {

  officeDropdown: any[];
  @ViewChild('allSelected') private allSelected: MatOption;
  selectedOffice = [];
  employeeListFilterForm: FormGroup;
  genderList$: Observable<IDropDownModel[]>;
  accountStatusList$: Observable<IDropDownModel[]>;
  employeeList: EmployeeDetailList[] = [];
  selection = new SelectionModel<EmployeeDetailList>(true, []);
  displayedColumns = ['select', 'Code', 'Name',
    'FatherName', 'Designation', 'Sex', 'EmploymentStatus', 'CreatedDate', 'HiredDate'];
  filterModel: EmployeeFilterModel = {
    EmployeeIdFilter: null, EmploymentStatusFilter: 0, NameFilter: null,
    PageIndex: 0, PageSize: 10, OfficeIds: [], GenderFilter: 0
  };
  employeeDataSource;
  TotalCount = 0;
  isLoading = false;
  EmployeeAttendanceList: any[];
  AttendanceDates: Date[] = [];
  MonthsList$: Observable<IDropDownModel[]>;
  Office = new FormControl([]);

  constructor(
    private employeeListService: EmployeeListService,
    private attendanceService: AttendanceService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private router: Router,
    private commonLoader: CommonLoaderService,
    private dialog: MatDialog) {
    this.accountStatusList$ = of([
      { name: 'Prospective', value: 1 },
      { name: 'Active', value: 2 },
      { name: 'Terminated', value: 3 }
    ] as IDropDownModel[]);
    this.genderList$ = of([
      { name: 'Male', value: 1 },
      { name: 'Female', value: 2 },
      { name: 'Other', value: 3 }
    ] as IDropDownModel[]);
    this.employeeListFilterForm = this.fb.group({
      Name: [''],
      // LastName: [''],
      Sex: [''],
      EmploymentStatus: [''],
      EmployeeId: ['']
    });
  }

  ngOnInit() {
    this.getOfficeList();
    this.getAllMonthList();
  }

  getOfficeList() {
    this.employeeListService
      .GetAllOfficeCodeList()
      .subscribe(
        data => {
          if (data.data.OfficeDetailsList != null) {
            this.officeDropdown = data.data.OfficeDetailsList.map(y => {
              return {
                OfficeId: y.OfficeId,
                OfficeName: y.OfficeName
              };
            });
            this.Office.patchValue([...this.officeDropdown.map(item => item.OfficeId), 0]);
            this.allSelected.select();
            this.filterModel.OfficeIds = this.Office.value.filter(x => x !== 0);
            this.getFilteredEmployeeList(this.filterModel);
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

  toggleAllSelection() {
    if (this.allSelected.selected) {

      this.Office
        .patchValue([...this.officeDropdown.filter(x => x !== 0).map(item => item.OfficeId), 0]);
    } else {
      this.Office.patchValue([]);
    }
    this.filterModel.OfficeIds = this.Office.value.filter(x => x !== 0);
    this.getFilteredEmployeeList(this.filterModel);
  }

  togglePerOne(all) {
    if (this.allSelected.selected) {
      this.allSelected.deselect();
      this.filterModel.OfficeIds = this.Office.value.filter(x => x !== 0);
      this.getFilteredEmployeeList(this.filterModel);
      return false;
    }
    if (this.Office.value.length === this.officeDropdown.length) {
      this.allSelected.select();
    }
    this.filterModel.OfficeIds = this.Office.value.filter(x => x !== 0);
    this.getFilteredEmployeeList(this.filterModel);
  }

  // selectedOfficeChanged(office) {
  //   this.selectedOffice = {
  //     value: office.value,
  //     name: office.name
  //   };
  //   this.filterModel.OfficeId = office.value;
  //   this.getFilteredEmployeeList(this.filterModel);
  // }

  getFilteredEmployeeList(filterModel) {
    this.selection.clear();
    this.employeeListService.getAllEmployeeList(filterModel).subscribe(res => {
      if (res.EmployeeList) {
        this.employeeList = [];
        this.TotalCount = res.RecordCount;
        res.EmployeeList.forEach(element => {
          this.employeeList.push({
            EmployeeId: element.EmployeeID,
            Code: element.EmployeeCode,
            Name: element.Name,
            FatherName: element.FatherName,
            Designation: element.Designation,
            Sex: element.SexName,
            // LastName: element.LastName,
            EmploymentStatus: EmploymentStatus[element.EmployeeTypeId],
            // Profession: (element.Profession === undefined) ? 'N/A' : element.Profession,
            HiredDate: element.HiredDate,
            CreatedDate: element.CreatedDate
          });
        });
        this.employeeDataSource = new MatTableDataSource<EmployeeDetailList>(this.employeeList);
      }
    }, err => {
      this.toastr.warning(err);
    });
  }

  filterEmployee(value) {
    this.filterModel = {
      EmployeeIdFilter: value.EmployeeId, EmploymentStatusFilter: value.EmploymentStatus,
      NameFilter: value.Name, PageIndex: 0, PageSize: 10, OfficeIds: this.Office.value.filter(x => x !== 0),
      GenderFilter: value.Sex
    };
    this.getFilteredEmployeeList(this.filterModel);
  }

  navigateToDetails(row?: EmployeeDetailList) {
    this.router.navigate(['/hr/employee/' + row.EmployeeId]);
  }

  pageEvent(e) {
    this.filterModel.PageIndex = e.pageIndex;
    this.filterModel.PageSize = e.pageSize;
    this.filterModel.OfficeIds = this.Office.value.filter(x => x !== 0);
    this.getFilteredEmployeeList(this.filterModel);
  }

  deleteEmployee() {
    if (this.selection.selected.length === 0) {
      this.toastr.warning('Please select at least 1 record!');
      return;
    }
    this.commonLoader.showLoader();
    const EmpIds: number[] = [];
    this.selection.selected.forEach(e => {
      EmpIds.push(e.EmployeeId);
    });
    this.employeeListService.deleteMurtipleEmployeesById(EmpIds).subscribe(res => {
      if (res) {
        this.toastr.success('Deleted Successfully!');
        this.getFilteredEmployeeList(this.filterModel);
        this.commonLoader.hideLoader();
      } else {
        this.toastr.warning('Something went wrong');
        this.commonLoader.hideLoader();
      }
    }, err => {
      this.toastr.warning(err);
      this.commonLoader.hideLoader();
    });
  }

  setAttendance(picker: MatDatepicker<'dd/MM/yyyy'>) {
    if (this.selection.selected.length === 0) {
      return;
    }
    picker.open();
  }

  onDateChanged(event) {
    this.AttendanceDates = [];
    this.commonLoader.showLoader();
    // const attendanceDate = event.target.value;
    // const model = {
    //   Date: StaticUtilities.getLocalDate(attendanceDate),
    //   OfficeId: this.selectedOffice.value,
    //   EmpIds: []
    // };
    const attendanceFromDate = event.target.value.begin;
    const attendanceToDate = event.target.value.end;
    const model = {
      FromDate: StaticUtilities.getLocalDate(attendanceFromDate),
      ToDate: StaticUtilities.getLocalDate(attendanceToDate),
      OfficeIds: this.Office.value.filter(x => x !== 0),
      EmpIds: []
    };
    this.selection.selected.forEach(res => {
      model.EmpIds.push(res.EmployeeId);
    });
    this.EmployeeAttendanceList = this.selection.selected;
    this.attendanceService.getPayrollHoursByEmployeeIds(model).subscribe(
      res => {
        this.EmployeeAttendanceList.forEach(e => {
          const emp = res.EmployeeAttendanceGroupDetail.filter(x => x.EmployeeID === e.EmployeeId);
          if (emp.length !== 0) {
            e.InTime = new Date(
              new Date(emp[0].InTime).getTime() -
              new Date().getTimezoneOffset() * 60000
            );
            e.OutTime = new Date(
              new Date(emp[0].OutTime).getTime() -
              new Date().getTimezoneOffset() * 60000
            );
            e.Attendance = emp[0].AttendanceType;
            e.AttendanceGroupId = emp[0].AttendanceGroupId;
            e.OfficeId = emp[0].OfficeId;
          }
        });
        this.commonLoader.hideLoader();
        const dialogRef = this.dialog.open(SetEmployeeAttendanceComponent, {
          width: '600px',
          data: {
            'AttendanceDates': this.getAllDates(attendanceFromDate, attendanceToDate), 'EmployeeList': this.EmployeeAttendanceList,
            'errors': false, 'errorMessage': '', 'OfficeId': this.Office.value.filter(x => x !== 0)
          }
        });
        dialogRef.afterClosed().subscribe(result => {
          if (result !== undefined && result.data != null) {
          }
        });
      },
      err => {
        this.commonLoader.hideLoader();
        const dialogRef = this.dialog.open(SetEmployeeAttendanceComponent, {
          width: '600px',
          data: {
            'AttendanceDates': this.getAllDates(attendanceFromDate, attendanceToDate), 'EmployeeList': this.EmployeeAttendanceList,
            'errors': true, 'errorMessage': err, 'OfficeId': this.Office.value.filter(x => x !== 0)
          }
        });
        dialogRef.afterClosed().subscribe(result => {
          if (result !== undefined && result.data != null) {
          }
        });
      }
    );
  }

  setFixedSalary() {
    const dialogRef = this.dialog.open(AddSalaryConfigurationComponent, {
      width: '500px',
      autoFocus: false,
      data: {
        ForAllEmployees: true,
        SelectedEmployees: this.selection.selected
      }
    });
    dialogRef.afterClosed().subscribe(() => {

    });
  }

  incrementDecrementSalary() {
    const dialogRef = this.dialog.open(IncrementDecrementSalaryComponent, {
      width: '500px',
      autoFocus: false,
      data: {
        SelectedEmployees: this.selection.selected
      }
    });
    dialogRef.afterClosed().subscribe(() => {

    });
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.employeeList.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.employeeList.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: EmployeeDetailList): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.EmployeeId + 1}`;
  }
  resetFilter() {
    this.filterModel = {
      EmployeeIdFilter: null, EmploymentStatusFilter: 0, NameFilter: null,
      PageIndex: 0, PageSize: 10, OfficeIds: this.Office.value.filter(x => x !== 0), GenderFilter: 0
    };
    // this.filterModel.OfficeIds = [this.selectedOffice.value];
    this.getFilteredEmployeeList(this.filterModel);
    this.employeeListFilterForm.reset();
  }
  getAllDates(startDate, endDate) {
    let currentDate = new Date(startDate);
    while (currentDate <= endDate) {
      this.AttendanceDates.push(currentDate);
      currentDate = new Date(
        currentDate.getFullYear(),
        currentDate.getMonth(),
        currentDate.getDate() + 1);
    }
    console.log(this.AttendanceDates);
    return this.AttendanceDates;
  }

  createAllEmployeesToUser() {
    this.isLoading = true;
    this.employeeListService.createAllEmployeesToUser().subscribe(res => {
      this.toastr.success("All Employees created as users");
      this.isLoading = false
    }, err => {
      this.toastr.success("Something went wrong");
      this.isLoading = false
    });
  }

  getAllMonthList() {
    const monthDropDown: IDropDownModel[] = [];
    for (let i = Month['January']; i <= Month['December']; i++) {
      monthDropDown.push({ name: Month[i], value: i });
    }
    this.MonthsList$ = of(monthDropDown);
  }

  administerPayroll(month, monthName) {
    const dialogRef = this.dialog.open(AdministerPayrollComponent, {
      width: '950px',
      autoFocus: false,
      data: {
        SelectedEmployees: this.selection.selected,
        Month: month,
        MonthName: monthName,
        OfficeId: this.Office.value.filter(x => x !== 0)
      }
    });
    dialogRef.afterClosed().subscribe(() => {

    });
  }

  exportPayrollExcel(month, monthName) {
    const employeeIds = this.selection.selected.map(s => s.EmployeeId );
    const model = {
      Month: month,
      OfficeId: this.Office.value,
      SelectedEmployees: employeeIds
    };

    this.employeeListService.exportPayrollExcel(model).subscribe(res => {
      if (res) {
        this.commonLoader.hideLoader();
        if (res && res.error) {
          this.toastr.warning(res.message);
        }
      } else {
        this.commonLoader.hideLoader();
      }
    }, error => {
      this.toastr.warning(error);
      this.commonLoader.hideLoader();
    });

  }

  disableExportPdf() {
    if (this.Office.value.length === 0 || this.Office.value.length > 1) {
      return true;
    } else if (this.selection.selected.length === 0) {
      return true;
    } else {
      return false;
    }
  }
}
