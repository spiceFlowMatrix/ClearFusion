import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, Form, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { LogisticService } from '../logistic.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-supplier',
  templateUrl: './add-supplier.component.html',
  styleUrls: ['./add-supplier.component.scss']
})
export class AddSupplierComponent implements OnInit {

  addSupplierForm: FormGroup;
  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddSupplierComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private logisticservice: LogisticService,
    private commonLoader: CommonLoaderService,
    public toastr: ToastrService) { }

  ngOnInit() {
    this.addSupplierForm = this.fb.group({
      Supplier: [this.data.Supplier, Validators.required],
      Quantity: [this.data.Quantity, Validators.required],
      FinalCost: [this.data.FinalPrice, Validators.required]
    });
  }

  addSupplier(value) {
    if (!this.addSupplierForm.valid) {
      return;
    }
    this.commonLoader.showLoader();
    if (this.data.Id !== undefined && this.data.Id != null ) {
      const model = {
        SupplierId: this.data.Id,
        SupplierName: value.Supplier,
        Quantity: value.Quantity,
        FinalPrice: value.FinalCost
      };
      this.logisticservice.editSuppliers(model).subscribe(res => {
        if (res.StatusCode === 200) {
          this.dialogRef.close({data: 'Updated'});
          this.commonLoader.hideLoader();
          this.toastr.success('Updated successfully!');
        } else {
          this.dialogRef.close({data: null});
          this.commonLoader.hideLoader();
          this.toastr.warning(res.Message);
        }
      });
    } else {
      const model = {
        RequestId: this.data.RequestId,
        SupplierName: value.Supplier,
        Quantity: value.Quantity,
        FinalCost: value.FinalCost
      };
      this.logisticservice.addSuppliers(model).subscribe(res => {
        if (res.StatusCode === 200 && res.CommonId.LongId != null) {
          const retmodel = {
            SupplierId: res.CommonId.LongId,
            SupplierName: value.Supplier,
            Quantity: value.Quantity,
            FinalPrice: value.FinalCost
          };
          this.dialogRef.close({data: retmodel});
          this.commonLoader.hideLoader();
          this.toastr.success('Supplier added successfully!');
          // code goes here
        } else {
          this.dialogRef.close({data: null});
          this.commonLoader.hideLoader();
          this.toastr.warning(res.Message);
        }
      });
    }
  }

  cancel() {
    this.dialogRef.close({data: null});
  }

}
