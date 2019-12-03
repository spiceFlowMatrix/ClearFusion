import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddLogisticItemsComponent } from '../add-logistic-items/add-logistic-items.component';

@Component({
  selector: 'app-purchase-final-cost',
  templateUrl: './purchase-final-cost.component.html',
  styleUrls: ['./purchase-final-cost.component.scss']
})
export class PurchaseFinalCostComponent implements OnInit {

  finalCostForm: FormGroup;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  private fb: FormBuilder,
  private dialogRef: MatDialogRef<AddLogisticItemsComponent>) { }

  ngOnInit() {
    this.finalCostForm = this.fb.group({
      FinalUnitCost: ['', Validators.required]
    });
  }

  submitFinalCost() {
    if (this.finalCostForm.valid) {
      this.dialogRef.close({data: {FinalCost: this.finalCostForm.get('FinalUnitCost').value, Id: this.data.Id}});
    }
  }

  closePopup() {
    this.dialogRef.close({data: null});
  }

}
