import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { EmployeeHistoryService } from 'src/app/hr/services/employee-history.service';
import { IEmployeeOtherSkillDetails } from 'src/app/hr/models/employee-history-models';

@Component({
  selector: 'app-add-other-skills',
  templateUrl: './add-other-skills.component.html',
  styleUrls: ['./add-other-skills.component.scss']
})
export class AddOtherSkillsComponent implements OnInit {
  otherSkillForm: FormGroup;
  isFormSubmitted = false;
  employeeId: number;
  onOtherSkillDetailListRefresh = new EventEmitter();
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private employeeHistoryService: EmployeeHistoryService,
    public dialogRef: MatDialogRef<AddOtherSkillsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.otherSkillForm = this.fb.group({
      EmployeeID: [''],
      AbilityLevel: ['', [Validators.required]],
      Experience: ['', [Validators.required]],
      Remarks: ['', [Validators.required]],
      TypeOfSkill: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.employeeId = this.data.employeeId;
    this.otherSkillForm.controls['EmployeeID'].setValue(this.employeeId);
  }
  onFormSubmit(data: IEmployeeOtherSkillDetails) {
    if (this.otherSkillForm.valid) {
      this.isFormSubmitted = true;
      this.employeeHistoryService.addOtherSkillDetail(data).subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.toastr.success('Success');
            this.isFormSubmitted = false;
            this.dialogRef.close();
            this.AddOtherSkillDetailListRefresh();
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
  //#region "Add Other Skill Detail List Refresh"
  AddOtherSkillDetailListRefresh() {
    this.onOtherSkillDetailListRefresh.emit();
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
