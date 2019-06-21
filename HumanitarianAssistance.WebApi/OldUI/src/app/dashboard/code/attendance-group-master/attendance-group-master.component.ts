import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../code.service';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';
import { GLOBAL } from '../../../shared/global';

@Component({
  selector: 'app-attendance-group-master',
  templateUrl: './attendance-group-master.component.html',
  styleUrls: ['./attendance-group-master.component.css']
})
export class AttendanceGroupMasterComponent implements OnInit {

  popupAddAttendanceGroupVisible = false;
  attendanceGroupList: IAttendanceGroup[];
  attendanceGroupData: IAttendanceGroup;
  loader= false;

  constructor(
    private toastr: ToastrService,
    private codeservice: CodeService,
    private commonservice: CommonService,
    private setting: AppSettingsService,
  ) { }

  ngOnInit() {
    this.onInitialize();
    this.getAttendanceGroupList();
  }

  onInitialize() {
    this.attendanceGroupList = [];
    this.attendanceGroupData = {
      Description: '',
      Id: null,
      Name: ''
    };
  }

  getAttendanceGroupList() {
    this.codeservice.GetAllDetails(this.setting.getBaseUrl() + GLOBAL.API_Code_GetAttendanceGroups)
      .subscribe(data => {
        this.onInitialize();
        if (data.StatusCode === 200) {
          if (data.data.AttendanceGroupMasterList.length > 0
            || data.data.AttendanceGroupMasterList !== undefined
            || data.data.AttendanceGroupMasterList !== null) {
            data.data.AttendanceGroupMasterList.forEach(element => {
              this.attendanceGroupList.push(element);
            });
          }
        } else {
          this.toastr.error(data.Message);
        }
      }, error => {
        if (error.StatusCode === 500) {
          this.toastr.error('Internal Server Error....');
        } else if (error.StatusCode === 401) {
          this.toastr.error('Unauthorized Access Error....');
        } else if (error.StatusCode === 403) {
          this.toastr.error('Forbidden Error....');
        }
      });
  }

  onFormSubmit(data: IAttendanceGroup) {
    this.loader = true;
    this.codeservice.AddEditAttendanceGroup(this.setting.getBaseUrl() + GLOBAL.API_Code_AddAttendanceGroups, data)
      .subscribe(x => {
        if (x.StatusCode === 200) {
          this.getAttendanceGroupList();
          this.loader = false;
          this.popupAddAttendanceGroupVisible = false;
        } else {
          this.toastr.error(x.Message);
          this.loader = false;
        }
      }, error => {
        if (error.StatusCode === 500) {
          this.toastr.error('Internal Server Error....');
          this.loader = false;
        } else if (error.StatusCode === 401) {
          this.toastr.error('Unauthorized Access Error....');
          this.loader = false;
        } else if (error.StatusCode === 403) {
          this.toastr.error('Forbidden Error....');
          this.loader = false;
        }
        this.loader = false;
      });
  }

  onEditAttendanceGroup(data: any) {

    const model: IAttendanceGroup = {
      Description: data.key.Description,
      Id: data.key.Id,
      Name: data.key.Name,
    };

    this.codeservice.AddEditAttendanceGroup(this.setting.getBaseUrl() + GLOBAL.API_Code_EditAttendanceGroups, model)
      .subscribe(x => {
        if (x.StatusCode === 200) {
          this.toastr.success(x.Message);
        } else {
          this.toastr.error(x.Message);
        }
      }, error => {
        if (error.StatusCode === 500) {
          this.toastr.error('Internal Server Error....');
        } else if (error.StatusCode === 401) {
          this.toastr.error('Unauthorized Access Error....');
        } else if (error.StatusCode === 403) {
          this.toastr.error('Forbidden Error....');
        }
      });

  }

  ShowPopup() {
    this.popupAddAttendanceGroupVisible = true;
  }

  onCancelClick() {
    this.popupAddAttendanceGroupVisible = false;
  }

}

export interface IAttendanceGroup {
  Id?: number;
  Name: string;
  Description: string;
}
