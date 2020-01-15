import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { EmployeeHistoryService } from 'src/app/hr/services/employee-history.service';
import { IEmployeeCloseRelativeDetails } from 'src/app/hr/models/employee-history-models';

@Component({
  selector: 'app-add-close-relative',
  templateUrl: './add-close-relative.component.html',
  styleUrls: ['./add-close-relative.component.scss']
})
export class AddCloseRelativeComponent implements OnInit {
  closeRelativeDetailForm: FormGroup;
  isFormSubmitted = false;
  employeeId: number;
  onAddCloseRelativeDetailListRefresh = new EventEmitter();
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private employeeHistoryService: EmployeeHistoryService,
    public dialogRef: MatDialogRef<AddCloseRelativeComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.closeRelativeDetailForm = this.fb.group({
      EmployeeID: [''],
      Name: ['', [Validators.required]],
      Relationship: ['', [Validators.required]],
      Position: ['', [Validators.required]],
      Organization: ['', [Validators.required]],
      Email: ['', [Validators.required, Validators.email]],
      PhoneNo: ['', [Validators.required]]
    });
  }
  ngOnInit() {
    this.employeeId = this.data.employeeId;
    this.closeRelativeDetailForm.controls['EmployeeID'].setValue(this.employeeId);
  }

  onFormSubmit(data: IEmployeeCloseRelativeDetails) {
    if (this.closeRelativeDetailForm.valid) {
      this.isFormSubmitted = true;
      this.employeeHistoryService.addCloseRelativeDetail(data).subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.toastr.success('Success');
            this.isFormSubmitted = false;
            this.dialogRef.close();
            this.AddCloseRelativeDetailListRefresh();
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
  //#region "Add Close Relative Detail List Refresh"
  AddCloseRelativeDetailListRefresh() {
    this.onAddCloseRelativeDetailListRefresh.emit();
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
