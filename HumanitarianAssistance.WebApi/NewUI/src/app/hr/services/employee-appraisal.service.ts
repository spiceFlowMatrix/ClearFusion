import { Injectable } from "@angular/core";
import { GlobalService } from "src/app/shared/services/global-services.service";
import { AppUrlService } from "src/app/shared/services/app-url.service";
import { GLOBAL } from "src/app/shared/global";

@Injectable({
  providedIn: "root"
})
export class EmployeeAppraisalService {
  constructor(
    private globalService: GlobalService,
    private appurl: AppUrlService
  ) {}

  getAllAppraisalListEmployeeId(id) {
    return this.globalService.getListById(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_GetEmployeeAppraisalByEmployeeId,
      id
    );
  }

  GetAppraisalQuestions() {
    return this.globalService.getList(
      this.appurl.getApiUrl() + GLOBAL.API_Code_GetAppraisalQuestions
    );
  }

  getfilteredEmployeeList(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() +
        GLOBAL.API_EmployeeDetail_GetFilteredEmployeeList,
      data
    );
  }

  addAppraisalForm(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_AddAppraisalDetail,
      data
    );
  }

  editAppraisalForm(data: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_EditAppraisalDetail,
      data
    );
  }
  approveAppraisaldetail(id: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_ApproveAppraisalDetail,
      id
    );
  }

  rejectAppraisaldetail(id: any) {
    return this.globalService.post(
      this.appurl.getApiUrl() + GLOBAL.API_EmployeeDetail_RejectAppraisalDetail,
      id
    );
  }
}
