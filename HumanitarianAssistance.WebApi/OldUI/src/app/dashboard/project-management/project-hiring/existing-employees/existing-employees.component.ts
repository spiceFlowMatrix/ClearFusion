import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CodeService } from '../../../code/code.service';
import { GLOBAL } from '../../../../shared/global';
import { AppSettingsService } from '../../../../service/app-settings.service';

@Component({
  selector: 'app-existing-employees',
  templateUrl: './existing-employees.component.html',
  styleUrls: ['./existing-employees.component.css']
})
export class ExistingEmployeesComponent implements OnInit {
  //#region "variables"
  existingEmployeeDataSource: any[];

  constructor(
    private codeservice: CodeService,
    private setting: AppSettingsService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.existingEmployeeDataSource = [];
  }

  //#region "edit"
  EditExistingEmployee(data) {
    this.codeservice
      .AddEditDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_Accounting_EditVouchersTransaction,
        data
      )
      .subscribe(
        res => {
          if (res.StatusCode === 200) {
            this.toastr.success('Transaction Updated Successfully!!!');
          }
        },
        error => {}
      );
  }
  //#endregion

  //#region "logEvent"
  logEvent(eventName, obj) {}
  //#endregion

  //#region "onActionEvent"
  onActionEvent(actionName, e) {}
  //#endregion
}
