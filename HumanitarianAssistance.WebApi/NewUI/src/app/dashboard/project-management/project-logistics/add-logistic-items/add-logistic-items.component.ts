import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LogisticService } from '../logistic.service';
import { IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-logistic-items',
  templateUrl: './add-logistic-items.component.html',
  styleUrls: ['./add-logistic-items.component.scss']
})
export class AddLogisticItemsComponent implements OnInit {

  addLogisticItemsForm: FormGroup;
  storeItemsList: any[];
  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddLogisticItemsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private logisticservice: LogisticService,
    private commonLoader: CommonLoaderService,
    public toastr: ToastrService) { }

  ngOnInit() {
    this.addLogisticItemsForm = this.fb.group({
      Item: [this.data.ItemId, Validators.required],
      RequestedUnits: [this.data.Quantity, Validators.required],
      EstimatedUnitCost: [this.data.EstimatedCost, Validators.required]
    });
    this.storeItemsList = this.data.Storeitems;
  }

  addItem(value) {
    if (!this.addLogisticItemsForm.valid) {
      this.toastr.warning('Please fill all required fields');
      return false;
    }
    if (this.data.Id == null) {
      const model = {
        ItemId : value.Item,
        RequestedUnits : value.RequestedUnits,
        EstimatedUnitCost : value.EstimatedUnitCost
      };
      this.dialogRef.close({data: model});
    } else {
      this.commonLoader.showLoader();
      const model = {
        Id: this.data.Id,
        ItemId : value.Item,
        RequestedUnits : value.RequestedUnits,
        EstimatedUnitCost : value.EstimatedUnitCost,
        RequestId: this.data.RequestId
      };
      this.logisticservice.editRequestItems(model).subscribe(res => {
        if (res.StatusCode === 200) {
          this.commonLoader.hideLoader();
          this.dialogRef.close({data: res.Message});
        } else {
          this.commonLoader.hideLoader();
          this.dialogRef.close({data: res.Message});
        }
      });
    }

  }
  cancelItem() {
    this.dialogRef.close({data: null});
  }

  get ItemIds() {
    return this.addLogisticItemsForm.get('Item').value;
  }

  // getStoreItems() {
  //   this.logisticservice.getAllStoreItems().subscribe(res => {
  //     this.storeItemsList = [];
  //     if (res.StatusCode === 200 && res.data.InventoryItemList != null) {
  //       res.data.InventoryItemList.forEach(element => {
  //         // this.storeItemsList.push(element);
  //         this.storeItemsList.push({
  //           Id: element.ItemId,
  //           Name: element.ItemCode + '-' + element.ItemName
  //         });
  //       });
  //       // this.addLogisticItemsForm.controls['Item'].setValue(this.data.ItemId);
  //     }
  //   });
  // }

  onOpenedItemSelectChange(event: IOpenedChange) {
    this.addLogisticItemsForm.controls['Item'].setValue(event);
  }
}
