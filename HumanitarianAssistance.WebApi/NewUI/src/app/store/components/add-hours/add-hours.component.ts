import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PurchaseService } from '../../services/purchase.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA, MatDatepicker } from '@angular/material';
import { StaticUtilities } from 'src/app/shared/static-utilities';
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
  selector: 'app-add-hours',
  templateUrl: './add-hours.component.html',
  styleUrls: ['./add-hours.component.scss'],
  providers: [
    {
      provide: DateAdapter, useClass: CustomDateAdapter
    }
  ]
})
export class AddHoursComponent implements OnInit {

  addUsageHourForm: FormGroup;
  isAddUsageHourFormSubmitted = false;
  maxDate = new Date();
  @ViewChild(MatDatepicker) picker;

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
    this.maxDate.setDate(this.maxDate.getDate());
  }

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close(false);
  }

  //#endregion
  addHours() {
    if (this.addUsageHourForm.valid) {
      this.isAddUsageHourFormSubmitted = true;

      this.addUsageHourForm.value.Month = StaticUtilities.setLocalDate(this.addUsageHourForm.value.Month);
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

  monthSelected(params) {
    this.addUsageHourForm.controls['Month'].setValue(params);
    this.picker.close();
  }
}
