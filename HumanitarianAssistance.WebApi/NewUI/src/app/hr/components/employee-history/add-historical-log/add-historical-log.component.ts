import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IHistoricalLogDetails } from 'src/app/hr/models/employee-history-models';
import { EmployeeHistoryService } from 'src/app/hr/services/employee-history.service';

@Component({
  selector: 'app-add-historical-log',
  templateUrl: './add-historical-log.component.html',
  styleUrls: ['./add-historical-log.component.scss']
})
export class AddHistoricalLogComponent implements OnInit {
  historicalLogForm: FormGroup;
  isFormSubmitted = false;
  employeeId: number;
  onAddHistoricalListRefresh = new EventEmitter();
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private employeeHistoryService: EmployeeHistoryService,
    public dialogRef: MatDialogRef<AddHistoricalLogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.historicalLogForm = this.fb.group({
      EmployeeID: [''],
      Description: ['', [Validators.required]],
      HistoryDate: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.employeeId = this.data.employeeId;
    this.historicalLogForm.controls['EmployeeID'].setValue(this.employeeId);
  }
  onFormSubmit(data: IHistoricalLogDetails) {
    this.isFormSubmitted = true;
    if (this.historicalLogForm.valid) {
      this.employeeHistoryService.addHistoricalLogDetail(data).subscribe(x => {
        if (x.StatusCode === 200) {
          this.toastr.success('Success');
          this.isFormSubmitted = false;
          this.AddHistoricalListRefresh();
          this.dialogRef.close();
        } else {
          this.toastr.warning(x.Message);
          this.isFormSubmitted = false;
        }
      }, error => {
        this.toastr.warning(error);
        this.isFormSubmitted = false;
      });
     }
  }

    //#region "On historical list refresh"
    AddHistoricalListRefresh() {
      this.onAddHistoricalListRefresh.emit();
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
