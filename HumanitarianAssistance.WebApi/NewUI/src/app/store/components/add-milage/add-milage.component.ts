import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PurchaseService } from '../../services/purchase.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { MatDatepicker } from '@angular/material';
import {DateAdapter, NativeDateAdapter} from '@angular/material/core';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import {default as _rollupMoment, Moment} from 'moment';

const moment = _rollupMoment || _moment;

// See the Moment.js docs for the meaning of these formats:
// https://momentjs.com/docs/#/displaying/format/
class CustomDateAdapter extends NativeDateAdapter {
  format(date: Date, displayFormat: Object): string {
    const formatString = 'MMMM YYYY';
    return moment(date).format(formatString);
  }
}

@Component({
  selector: 'app-add-milage',
  templateUrl: './add-milage.component.html',
  styleUrls: ['./add-milage.component.scss'],
  providers: [
    {
      provide: DateAdapter, useClass: CustomDateAdapter
    }
  ]
})
export class AddMilageComponent implements OnInit {

  mileageForm: FormGroup;
  isAddMileageFormSubmitted = false;
  maxDate = new Date();
  @ViewChild(MatDatepicker) picker;

  constructor(private fb: FormBuilder, private purchaseService: PurchaseService,
    private commonLoader: CommonLoaderService, public toastr: ToastrService,
    private dialogRef: MatDialogRef<AddMilageComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {

    this.mileageForm = this.fb.group({
      'VehicleId': [data.vehicleId, [Validators.required]],
      'Mileage': [null, [Validators.required]],
      'Month': [moment(), [Validators.required]]
    });

    this.maxDate.setDate(this.maxDate.getDate());
  }

  ngOnInit() {
  }

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close(false);
  }

  //#endregion
  addMilage() {
    if (this.mileageForm.valid) {
      this.mileageForm.value.Month = StaticUtilities.setLocalDate(this.mileageForm.value.Month);
      this.isAddMileageFormSubmitted = true;
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
          this.toastr.warning(error);
          this.isAddMileageFormSubmitted = false;
        });

    }
  }

  monthSelected(params) {
    this.mileageForm.controls['Month'].setValue(params);
    this.picker.close();
  }
}

