import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PurchaseService } from '../../services/purchase.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-add-hours',
  templateUrl: './add-hours.component.html',
  styleUrls: ['./add-hours.component.scss']
})
export class AddHoursComponent implements OnInit {

  addUsageHourForm: FormGroup;
  isAddUsageHourFormSubmitted = false;


  constructor(private fb: FormBuilder, private purchaseService: PurchaseService,
    public toastr: ToastrService,
    private dialogRef: MatDialogRef<AddHoursComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
      this.addUsageHourForm = this.fb.group({
        'GeneratorId': [data.generatorId, [Validators.required]],
        'Hours': [null, [Validators.required]],
        'Month': [null, [Validators.required]]
      });
    }

  ngOnInit() {
  }

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close(false);
  }

  //#endregion
  addHours() {
    if (this.addUsageHourForm.valid) {
      this.isAddUsageHourFormSubmitted = true;
      this.purchaseService.addGeneratorUsageHours(this.addUsageHourForm.value)
        .subscribe(x => {
          if (x) {
            this.dialogRef.close(false);
            this.isAddUsageHourFormSubmitted = false;
            this.toastr.success('Added Successfully');
          } else {
            this.toastr.warning('Something went wrong');
            this.isAddUsageHourFormSubmitted = false;
          }
        }, error => {
          this.toastr.warning('Something went wrong');
          this.isAddUsageHourFormSubmitted = false;
        });

    }
  }
}
