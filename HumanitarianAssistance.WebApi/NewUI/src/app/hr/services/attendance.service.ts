import { Injectable } from '@angular/core';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { HttpClient } from '@angular/common/http';
import { GLOBAL } from 'src/app/shared/global';
import { map } from 'rxjs/operators';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  //#region "getAttendancelist"
  getAttendanceList(model: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Attendance_GetFilteredAttendanceDetails,
        model
      );

  }

  getPayrollHoursByEmployeeIds(model: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Attendance_GetPayrollDailyHourByEmployeeIds,
        model
      );
  }

  saveEmployeeAttendance(model: any) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Attendance_AddEditEmployeeAttendance,
        model
      );
  }

  markWholeMonthAttendance(model) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Attendance_MarkWholeMonthEmployeeAttendance,
        model
      );
  }

  editEmployeeAttendance(model) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Attendance_EditEmployeeAttendanceByDate,
        model
      );
  }
  //#endregion

  getAttendanceGroupDetailById(id) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Attendance_GetAttendanceGroupDetailById,
        id
      );
  }

  setPayrollHoursToAttendanceGroup(model) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Attendance_AddPayrollDailyHoursToAttendanceGroups,
          model
      );
  }

  getPayrollHoursByAttendanceGroup(model) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Attendance_GetPayrollMonthlyHourByAttendanceGroups,
          model
      );
  }

  editPayrollHoursById(model) {
    return this.globalService
      .post(
        this.appurl.getApiUrl() +
          GLOBAL.API_Attendance_EditPayrollMonthlyHourById,
          model
      );
  }

}
