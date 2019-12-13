import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, Form, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { LogisticService } from '../logistic.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/store/services/config.service';
import { elementClassProp } from '@angular/core/src/render3';
import { IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';

@Component({
  selector: 'app-add-supplier',
  templateUrl: './add-supplier.component.html',
  styleUrls: ['./add-supplier.component.scss']
})
export class AddSupplierComponent implements OnInit {

  addSupplierForm: FormGroup;
  sourceCodeList: any[] = [];
  storedropdownItemsList: any[];
  invoiceAttachment;
  warrantyAttachment;

  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddSupplierComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private logisticservice: LogisticService,
    private commonLoader: CommonLoaderService,
    public toastr: ToastrService,
    private configService: ConfigService) { }

  ngOnInit() {
    this.getAllStoreSuppliers();
    this.getStoreItems();
    this.addSupplierForm = this.fb.group({
      StoreSourceCode: ['', Validators.required],
      ItemId: ['', Validators.required],
      Quantity: [this.data.Quantity, Validators.required],
      FinalUnitPrice: [this.data.FinalPrice, Validators.required]
    });

  }

  openInvoiceInput() {
    document.getElementById('invoicefileInput').click();
  }

  invoiceFileChange(file: any) {
    this.invoiceAttachment = [];
    this.invoiceAttachment.push(file);
  }

  openWarrantyInput() {
    document.getElementById('warrantyfileInput').click();
  }

  warrantyFileChange(file: any) {
    this.warrantyAttachment = [];
    this.warrantyAttachment.push(file);
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

  getAllStoreSuppliers() {
    this.configService.getSourceCodeById(2).subscribe(res => {
      this.sourceCodeList = [];
        res.forEach(element => {
          this.sourceCodeList.push({
            Id : element.SourceCodeId,
            Name: element.Code + '-' + element.Description
          });
        });
    });
  }

  getStoreItems() {
    this.logisticservice.getAllStoreItems().subscribe(res => {
      // this.storeItemsList = [];
      this.storedropdownItemsList = [];
      if (res.StatusCode === 200 && res.data.InventoryItemList != null) {
        res.data.InventoryItemList.forEach(element => {
          // this.storeItemsList.push(element);
          this.storedropdownItemsList.push({
            Id: element.ItemId,
            Name: element.ItemCode + '-' + element.ItemName
          });
        });
        // this.addLogisticItemsForm.controls['Item'].setValue(this.data.ItemId);
      }
    });
  }

  get StoreSuppliers() {
    return this.addSupplierForm.get('StoreSourceCode').value;
  }

  get ItemId() {
    return this.addSupplierForm.get('ItemId').value;
  }

  onOpenedItemSuppliersChange(event: IOpenedChange) {
    this.addSupplierForm.controls['StoreSourceCode'].setValue(event.Value);
  }

  onOpenedItemChange(event: IOpenedChange) {
    this.addSupplierForm.controls['ItemId'].setValue(event.Value);
  }

  cancel() {
    this.dialogRef.close({data: null});
  }

}
