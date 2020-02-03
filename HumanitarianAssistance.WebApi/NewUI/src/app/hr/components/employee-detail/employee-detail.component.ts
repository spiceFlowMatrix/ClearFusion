import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HrControlPanelService } from '../../services/hr-control-panel.service';
import { IEmployeeDetail } from '../../models/employee-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.scss']
})
export class EmployeeDetailComponent implements OnInit {
  showDetail = false;
  @Input() employeeId: number;
  @Output() employeeStatus = new EventEmitter<number>();
  employeeDetail: IEmployeeDetail;
  constructor(
    private hrControlPanelService: HrControlPanelService,
    private toAstr: ToastrService
  ) {}

  ngOnInit() {
    this.onModelInIt();
  }

  ngOnChanges() {
    this.getEmployeeDetail(this.employeeId);
  }

  onModelInIt() {
    this.employeeDetail = {
      AttendanceGroup: '',
      Country: '',
      CurrentAddress: '',
      DateOfBirth: '',
      DutyStation: '',
      Email: '',
      EmployementStatus: '',
      EmploymentStatusId: 0,
      ExperienceMonth: '',
      ExperienceYear: '',
      Name: '',
      HiredOn: '',
      JobDescription: '',
      FatherName: '',
      PermanentAddress: '',
      Phone: '',
      PreviousWork: '',
      Profession: '',
      Qualification: '',
      Resigned: '',
      ResignedOn: '',
      ResignedReason: '',
      Sex: '',
      State: '',
      Terminated: '',
      TerminatedOn: '',
      TerminationReason: '',
      IsResigned: false,
      ResignationStatus: 0
    };
  }

  show() {
    this.showDetail = true;
  }
  getState(e) {
    this.showDetail = e;
  }

  getEmployeeDetail(id) {
    this.hrControlPanelService.getEmployeeDetail(id).subscribe(
      x => {
        if (x.EmployeeDetail) {
          this.employeeDetail = {
            AttendanceGroup: x.EmployeeDetail.AttendanceGroup,
            Country: x.EmployeeDetail.Country,
            CurrentAddress: x.EmployeeDetail.CurrentAddress,
            DateOfBirth: x.EmployeeDetail.DateOfBirth,
            DutyStation: x.EmployeeDetail.DutyStation,
            Email: x.EmployeeDetail.Email,
            EmployementStatus: x.EmployeeDetail.EmployementStatus,
            EmploymentStatusId: x.EmployeeDetail.EmploymentStatusId,
            ExperienceMonth: x.EmployeeDetail.ExperienceMonth,
            ExperienceYear: x.EmployeeDetail.ExperienceYear,
            Name: x.EmployeeDetail.Name,
            HiredOn: x.EmployeeDetail.HiredOn,
            JobDescription: x.EmployeeDetail.JobDescription,
            FatherName: x.EmployeeDetail.FatherName,
            PermanentAddress: x.EmployeeDetail.PermanentAddress,
            Phone: x.EmployeeDetail.Phone,
            PreviousWork: x.EmployeeDetail.PreviousWork,
            Profession: x.EmployeeDetail.Profession,
            Qualification: x.EmployeeDetail.Qualification,
            Resigned: x.EmployeeDetail.Resigned,
            ResignedOn: x.EmployeeDetail.ResignedOn,
            ResignedReason: x.EmployeeDetail.ResignedReason,
            Sex: x.EmployeeDetail.Sex,
            State: x.EmployeeDetail.State,
            Terminated: x.EmployeeDetail.Terminated,
            TerminatedOn: x.EmployeeDetail.TerminatedOn,
            TerminationReason: x.EmployeeDetail.TerminationReason,
            OfficeId: x.EmployeeDetail.OfficeId,
            IsResigned: x.EmployeeDetail.IsResigned,
            ResignationStatus: x.EmployeeDetail.ResignationStatus
          };
          this.employeeStatus.emit(this.employeeDetail.EmploymentStatusId);
        }
        localStorage.setItem(
          'SelectedOfficeId',
          this.employeeDetail.OfficeId.toString()
        );
        const fullName =
          this.employeeDetail.Name;
        localStorage.setItem('selectedEmployeeName', fullName);
      },
      error => {
        this.toAstr.warning(error);
      }
    );
  }
}
