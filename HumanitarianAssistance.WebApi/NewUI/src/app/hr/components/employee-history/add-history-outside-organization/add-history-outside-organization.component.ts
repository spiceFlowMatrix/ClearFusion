import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { EmployeeHistoryService } from 'src/app/hr/services/employee-history.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IHistoryOutsideOrganizationDetails } from 'src/app/hr/models/employee-history-models';

@Component({
  selector: 'app-add-history-outside-organization',
  templateUrl: './add-history-outside-organization.component.html',
  styleUrls: ['./add-history-outside-organization.component.scss']
})
export class AddHistoryOutsideOrganizationComponent implements OnInit {
  historyOutsideOrganizationDetailForm: FormGroup;
  isFormSubmitted = false;
  employeeId: number;
  onAddHistoryOutsideOrganizationListRefresh = new EventEmitter();
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private employeeHistoryService: EmployeeHistoryService,
    public dialogRef: MatDialogRef<AddHistoryOutsideOrganizationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.historyOutsideOrganizationDetailForm = this.fb.group({
      EmployeeID: [''],
      EmploymentFrom: ['', [Validators.required]],
      EmploymentTo: ['', [Validators.required]],
      Organization: ['', [Validators.required]],
      MonthlySalary: ['', [Validators.required]],
      ReasonForLeaving: ['', [Validators.required]],
      Position: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.employeeId = this.data.employeeId;
    this.historyOutsideOrganizationDetailForm.controls['EmployeeID'].setValue(
      this.employeeId
    );
  }

  onFormSubmit(data: IHistoryOutsideOrganizationDetails) {
    if (this.historyOutsideOrganizationDetailForm.valid) {
      this.isFormSubmitted = true;
      this.employeeHistoryService.addHistoryOutsideOrganization(data).subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.toastr.success('Success');
            this.isFormSubmitted = false;
            this.AddHistoryOutsideOrganizationListRefresh();
            this.dialogRef.close();
          } else {
            this.toastr.warning(x.Message);
            this.isFormSubmitted = false;
          }
        },
        error => {
          this.toastr.warning(error);
          this.isFormSubmitted = false;
        }
      );
    }
  }
  //#region "On add history Outside Organization refresh"
  AddHistoryOutsideOrganizationListRefresh() {
    this.onAddHistoryOutsideOrganizationListRefresh.emit();
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
