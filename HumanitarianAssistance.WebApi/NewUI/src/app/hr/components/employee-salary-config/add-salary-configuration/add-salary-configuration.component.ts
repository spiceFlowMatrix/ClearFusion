import { Component, OnInit, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-add-salary-configuration',
  templateUrl: './add-salary-configuration.component.html',
  styleUrls: ['./add-salary-configuration.component.scss']
})
export class AddSalaryConfigurationComponent implements OnInit {
  onAddSalaryConfigurationRefresh = new EventEmitter();
  constructor(
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AddSalaryConfigurationComponent>,
  ) { }

  ngOnInit() {
  }
//#region "Add Salary Configuration Refresh"
AddSalaryConfigurationRefresh() {
  this.onAddSalaryConfigurationRefresh.emit();
}
//#endregion
//#region "onCancelPopup"
onCancelPopup(): void {
  this.dialogRef.close();
}
//#endregion
onNoClick(): void {
  this.dialogRef.close();
}
}
