import { Component, OnInit, EventEmitter } from '@angular/core';
import { AddSalaryConfigurationComponent } from '../add-salary-configuration/add-salary-configuration.component';
import { MatDialogRef } from '@angular/material';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-bonus',
  templateUrl: './add-bonus.component.html',
  styleUrls: ['./add-bonus.component.scss']
})
export class AddBonusComponent implements OnInit {
  onAddBonusRefresh = new EventEmitter();
  constructor(
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AddSalaryConfigurationComponent>,
  ) { }

  ngOnInit() {
  }

//#region "Add Salary Configuration Refresh"
AddBonusRefresh() {
  this.onAddBonusRefresh.emit();
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
