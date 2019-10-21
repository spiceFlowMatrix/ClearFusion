import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PurchaseService } from '../../services/purchase.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-milage',
  templateUrl: './add-milage.component.html',
  styleUrls: ['./add-milage.component.scss']
})
export class AddMilageComponent implements OnInit {

  mileageForm: FormGroup;
  isAddMileageFormSubmitted = false;

  constructor(private fb: FormBuilder, private purchaseService: PurchaseService,

    private commonLoader: CommonLoaderService, public toastr: ToastrService,

    private dialogRef: MatDialogRef<AddMilageComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    debugger;

    this.mileageForm = this.fb.group({
      'VehicleId': [data.vehicleId, [Validators.required]],
      'Mileage': [null, [Validators.required]],
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
  addMilage() {
    this.isAddMileageFormSubmitted = true;
    if (this.mileageForm.valid) {
      this.purchaseService.addVehicleMileage(this.mileageForm.value)
        .subscribe(x => {
          if (x) {
            this.dialogRef.close(false);
            this.isAddMileageFormSubmitted = false;
            this.toastr.success('Added Successfully');
          } else {
            this.toastr.warning('Something went wrong');
            this.isAddMileageFormSubmitted = false;
          }
        }, error => {
          this.toastr.warning('Something went wrong');
          this.isAddMileageFormSubmitted = false;
        });

    }
  }
}

export const MY_FORMATS = {
  parse: {
    dateInput: 'MM',
  },
  display: {
    dateInput: 'MM',
    monthYearLabel: 'MMM',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM',
  },
};
