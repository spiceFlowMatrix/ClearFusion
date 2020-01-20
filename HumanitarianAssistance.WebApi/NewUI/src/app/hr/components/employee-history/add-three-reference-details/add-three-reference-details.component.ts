import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { EmployeeHistoryService } from 'src/app/hr/services/employee-history.service';
import { IEmployeeThreeReferenceDetails } from 'src/app/hr/models/employee-history-models';

@Component({
  selector: 'app-add-three-reference-details',
  templateUrl: './add-three-reference-details.component.html',
  styleUrls: ['./add-three-reference-details.component.scss']
})
export class AddThreeReferenceDetailsComponent implements OnInit {
  referenceDetailForm: FormGroup;
  isFormSubmitted = false;
  employeeId: number;
  onThreeReferenceDetailListRefresh = new EventEmitter();
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private employeeHistoryService: EmployeeHistoryService,
    public dialogRef: MatDialogRef<AddThreeReferenceDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.referenceDetailForm = this.fb.group({
      EmployeeID: [''],
      Name: ['', [Validators.required]],
      Relationship: ['', [Validators.required]],
      Position: ['', [Validators.required]],
      Organization: ['', [Validators.required]],
      PhoneNo: [
        null,
        [
          Validators.required,
          Validators.pattern(/^-?(0|[1-9]\d*)?$/),
          Validators.minLength(10),
          Validators.maxLength(14)
        ]
      ],
      Email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit() {
    this.employeeId = this.data.employeeId;
    this.referenceDetailForm.controls['EmployeeID'].setValue(this.employeeId);
  }
  onFormSubmit(data: IEmployeeThreeReferenceDetails) {
    if (this.referenceDetailForm.valid) {
      this.isFormSubmitted = true;
      this.employeeHistoryService.addThreeReferenceDetail(data).subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.toastr.success('Success');
            this.isFormSubmitted = false;
            this.dialogRef.close();
            this.AddThreeReferenceDetailListRefresh();
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
    //#region "Add Three Reference Detail List Refresh"
    AddThreeReferenceDetailListRefresh() {
      this.onThreeReferenceDetailListRefresh.emit();
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
