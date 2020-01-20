import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IHistoryOutsideCountryDetails } from 'src/app/hr/models/employee-history-models';
import { ToastrService } from 'ngx-toastr';
import { EmployeeHistoryService } from 'src/app/hr/services/employee-history.service';

@Component({
  selector: 'app-add-history-outside-country',
  templateUrl: './add-history-outside-country.component.html',
  styleUrls: ['./add-history-outside-country.component.scss']
})
export class AddHistoryOutsideCountryComponent implements OnInit {
  historyOutsideCountryDetailForm: FormGroup;
  isFormSubmitted = false;
  employeeId: number;
  onAddHistoryOutsideCountryListRefresh = new EventEmitter();
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private employeeHistoryService: EmployeeHistoryService,
    public dialogRef: MatDialogRef<AddHistoryOutsideCountryComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.historyOutsideCountryDetailForm = this.fb.group({
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
    this.historyOutsideCountryDetailForm.controls['EmployeeID'].setValue(this.employeeId);
  }

  onFormSubmit(data: IHistoryOutsideCountryDetails) {
    if (this.historyOutsideCountryDetailForm.valid) {
      this.isFormSubmitted = true;
      this.employeeHistoryService.addHistoryOutsideCountry(data).subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.toastr.success('Success');
            this.isFormSubmitted = false;
            this.dialogRef.close();
            this.AddHistoryOutsideCountryListRefresh();
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
    //#region "On add history Outside Country refresh"
    AddHistoryOutsideCountryListRefresh() {
      this.onAddHistoryOutsideCountryListRefresh.emit();
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
