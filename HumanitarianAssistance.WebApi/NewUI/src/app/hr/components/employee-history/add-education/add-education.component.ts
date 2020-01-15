import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IEducationDetails } from 'src/app/hr/models/employee-history-models';
import { ToastrService } from 'ngx-toastr';
import { EmployeeHistoryService } from 'src/app/hr/services/employee-history.service';

@Component({
  selector: 'app-add-education',
  templateUrl: './add-education.component.html',
  styleUrls: ['./add-education.component.scss']
})
export class AddEducationComponent implements OnInit {
  educationForm: FormGroup;
  isFormSubmitted = false;
  employeeId: number;
  onAddEducationListRefresh = new EventEmitter();
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private employeeHistoryService: EmployeeHistoryService,
    public dialogRef: MatDialogRef<AddEducationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.educationForm = this.fb.group({
      EmployeeID: [''],
      EducationFrom: ['', [Validators.required]],
      EducationTo: ['', [Validators.required]],
      Degree: ['', [Validators.required]],
      FieldOfStudy: ['', [Validators.required]],
      Institute: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.employeeId = this.data.employeeId;
    this.educationForm.controls['EmployeeID'].setValue(this.employeeId);
  }

  onFormSubmit(data: IEducationDetails) {
    this.isFormSubmitted = true;
    if (this.educationForm.valid) {
      this.employeeHistoryService.addEducationDetail(data).subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.toastr.success('Success');
            this.isFormSubmitted = false;
            this.dialogRef.close();
            this.AddEducationListRefresh();
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
  //#region "On historical list refresh"
  AddEducationListRefresh() {
    this.onAddEducationListRefresh.emit();
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
