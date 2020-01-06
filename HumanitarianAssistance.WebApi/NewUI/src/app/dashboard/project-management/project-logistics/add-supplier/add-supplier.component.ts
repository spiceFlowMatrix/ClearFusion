import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, Form, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { LogisticService } from '../logistic.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/store/services/config.service';
import { elementClassProp } from '@angular/core/src/render3';
import { IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { FileSourceEntityTypes } from 'src/app/shared/enum';
import { takeUntil } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';

@Component({
  selector: 'app-add-supplier',
  templateUrl: './add-supplier.component.html',
  styleUrls: ['./add-supplier.component.scss']
})
export class AddSupplierComponent implements OnInit {

  addSupplierForm: FormGroup;
  sourceCodeList: any[] = [];
  storedropdownItemsList: any[];
  invoiceAttachment = [];
  warrantyAttachment = [];

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddSupplierComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private logisticservice: LogisticService,
    private commonLoader: CommonLoaderService,
    public toastr: ToastrService,
    private configService: ConfigService,
    private globalSharedService: GlobalSharedService) { }

  ngOnInit() {
    this.getAllStoreSuppliers();
    this.getStoreItems();
    this.addSupplierForm = this.fb.group({
      StoreSourceCode: [this.data.SourceId, Validators.required],
      ItemId: [this.data.Item, Validators.required],
      Quantity: [this.data.Quantity, Validators.required],
      FinalUnitPrice: [this.data.FinalUnitPrice, Validators.required]
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
      this.toastr.warning('Please fill all required fields!');
      return;
    }
    if (this.data.Id !== undefined && this.data.Id != null ) {
      this.commonLoader.showLoader();
      const model = {
        SupplierId: this.data.Id,
        SourceId: value.StoreSourceCode,
        Quantity: value.Quantity,
        FinalUnitPrice: value.FinalUnitPrice,
        ItemId: value.ItemId,
        isInvoiceUpdated: (this.invoiceAttachment.length === 0) ? false : true,
        isWarrantyUpdated: (this.warrantyAttachment.length === 0) ? false : true
      };
      this.logisticservice.editSuppliers(model).subscribe(res => {
        if (res.StatusCode === 200) {
          let uploadModel: any[] = [];
          if (this.invoiceAttachment.length !== 0) {
            uploadModel.push({
              fileType: FileSourceEntityTypes.LogisticSupplierInvoice,
              SupplierId: this.data.Id,
              file: this.invoiceAttachment[0][0]
            });
          }
          if (this.warrantyAttachment.length !== 0) {
            uploadModel.push({
              fileType: FileSourceEntityTypes.LogisticSupplierWarranty,
              SupplierId: this.data.Id,
              file: this.warrantyAttachment[0][0]
            });
          }
          for (let i = 0; i < uploadModel.length; i++) {
            this.globalSharedService
            .uploadFile(uploadModel[i].fileType, uploadModel[i].SupplierId, uploadModel[i].file)
            .pipe(takeUntil(this.destroyed$))
            .subscribe(y => {
              if (i === (uploadModel.length - 1)) {
                this.dialogRef.close({data: 'Success'});
                this.commonLoader.hideLoader();
                this.toastr.success('Updated successfully!');
              }
            });
          }
          if (uploadModel.length === 0) {
            this.dialogRef.close({data: 'Success'});
            this.commonLoader.hideLoader();
            this.toastr.success('Updated successfully!');
          }
        } else {
          this.dialogRef.close({data: null});
          this.commonLoader.hideLoader();
          this.toastr.warning(res.Message);
        }
      });
    } else {
      if (this.invoiceAttachment.length === 0) {
        this.toastr.warning('Please Upload Invoice Attachment!');
        return;
      }
      if (this.warrantyAttachment.length === 0) {
        this.toastr.warning('Please Upload Warranty Attachment!');
        return;
      }
      this.commonLoader.showLoader();
      const model = {
        RequestId: this.data.RequestId,
        StoreSourceCode: value.StoreSourceCode,
        Quantity: value.Quantity,
        ItemId: value.ItemId,
        FinalUnitPrice: value.FinalUnitPrice
      };
      this.logisticservice.addSuppliers(model).subscribe(res => {
        if (res.StatusCode === 200 && res.CommonId.LongId != null) {
          const uploadModel = [
            {
              fileType: FileSourceEntityTypes.LogisticSupplierInvoice,
              SupplierId: res.CommonId.LongId,
              file: this.invoiceAttachment[0][0]
            },
            {
              fileType: FileSourceEntityTypes.LogisticSupplierWarranty,
              SupplierId: res.CommonId.LongId,
              file: this.warrantyAttachment[0][0]
            },
          ];
          for (let i = 0; i < uploadModel.length; i++) {
            this.globalSharedService
            .uploadFile(uploadModel[i].fileType, uploadModel[i].SupplierId, uploadModel[i].file)
            .pipe(takeUntil(this.destroyed$))
            .subscribe(y => {
              if (i === (uploadModel.length - 1)) {
                this.dialogRef.close({data: 'Success'});
                this.commonLoader.hideLoader();
                this.toastr.success('Supplier added successfully!');
              }
            });
          }
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
    this.addSupplierForm.controls['StoreSourceCode'].setValue(event);
  }

  onOpenedItemChange(event: IOpenedChange) {
    this.addSupplierForm.controls['ItemId'].setValue(event);
  }

  cancel() {
    this.dialogRef.close({data: null});
  }

}
