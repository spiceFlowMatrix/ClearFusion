import { Component, OnInit, Input } from '@angular/core';
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
  employeeDetail: IEmployeeDetail;
  constructor(private hrControlPanelService: HrControlPanelService, private toAstr: ToastrService) { }

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
      ExperienceMonth: '',
      ExperienceYear: '',
      FirstName: '',
      HiredOn: '',
      JobDescription: '',
      LastName: '',
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
    this.hrControlPanelService.getEmployeeDetail(id).subscribe((x) => {
      if (x.EmployeeDetail) {
        this.employeeDetail = {
          AttendanceGroup: x.EmployeeDetail.AttendanceGroup,
          Country: x.EmployeeDetail.Country,
          CurrentAddress: x.EmployeeDetail.CurrentAddress,
          DateOfBirth: x.EmployeeDetail.DateOfBirth,
          DutyStation: x.EmployeeDetail.DutyStation,
          Email: x.EmployeeDetail.Email,
          EmployementStatus: x.EmployeeDetail.EmployementStatus,
          ExperienceMonth: x.EmployeeDetail.ExperienceMonth,
          ExperienceYear: x.EmployeeDetail.ExperienceYear,
          FirstName: x.EmployeeDetail.FirstName,
          HiredOn: x.EmployeeDetail.HiredOn,
          JobDescription: x.EmployeeDetail.JobDescription,
          LastName: x.EmployeeDetail.LastName,
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
          IsResigned: x.EmployeeDetail.IsResigned,
          ResignationStatus: x.EmployeeDetail.ResignationStatus
        };
      }
    }, error => {
      this.toAstr.warning(error);
    });
  }
}
