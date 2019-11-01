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
    this.getStoreItems();
    this.addLogisticItemsForm = this.fb.group({
      Item: ['', Validators.required],
      Quantity: [this.data.Quantity, Validators.required],
      EstimatedCost: [this.data.EstimatedCost, Validators.required]
    });
  }

  addItem(value) {
    this.commonLoader.showLoader();
    if (this.data.Id !== undefined && this.data.Id != null ) {
      const model = {
        Id: this.data.Id,
        ItemId : value.Item,
        RequestId : this.data.RequestId,
        Quantity : value.Quantity,
        EstimatedCost : value.EstimatedCost
      };
      this.logisticservice.editRequestItems(model).subscribe(res => {
        if (res.StatusCode === 200 && res.data.logisticItem != null) {
          this.dialogRef.close({data: res.data.logisticItem});
          this.commonLoader.hideLoader();
          this.toastr.success('Updated successfully!');
          // code goes here
        } else {
          this.dialogRef.close({data: null});
          this.commonLoader.hideLoader();
          this.toastr.warning(res.Message);
        }
      });
    } else {
      const model = {
        ItemId : value.Item,
        RequestId : this.data.RequestId,
        Quantity : value.Quantity,
        EstimatedCost : value.EstimatedCost
      };
      this.logisticservice.addRequestItems(model).subscribe(res => {
        if (res.StatusCode === 200 && res.data.logisticItem != null) {
          this.dialogRef.close({data: res.data.logisticItem});
          this.commonLoader.hideLoader();
          this.toastr.success('Item added successfully!');
          // code goes here
        } else {
          this.dialogRef.close({data: null});
          this.commonLoader.hideLoader();
          this.toastr.warning(res.Message);
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

  getStoreItems() {
    this.logisticservice.getAllStoreItems().subscribe(res => {
      this.storeItemsList = [];
      if (res.StatusCode === 200 && res.data.InventoryItemList != null) {
        res.data.InventoryItemList.forEach(element => {
          // this.storeItemsList.push(element);
          this.storeItemsList.push({
            Id: element.ItemId,
            Name: element.ItemName
          });
        });
        this.addLogisticItemsForm.controls['Item'].setValue(this.data.ItemId);
      }
    });
  }

  onOpenedItemSelectChange(event: IOpenedChange) {
    this.addLogisticItemsForm.controls['Item'].setValue(event);
  }
}
