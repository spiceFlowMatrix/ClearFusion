import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LogisticService } from '../logistic.service';
import { IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';

@Component({
  selector: 'app-add-logistic-items',
  templateUrl: './add-logistic-items.component.html',
  styleUrls: ['./add-logistic-items.component.scss']
})
export class AddLogisticItemsComponent implements OnInit {

  addLogisticItemsForm: FormGroup;
  storeItemsList: any[];
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddLogisticItemsComponent>,
    private logisticservice: LogisticService) { }

  ngOnInit() {
    this.addLogisticItemsForm = this.fb.group({
      Item: ['', Validators.required],
      Quantity: ['', Validators.required],
      EstimatedCost: ['', Validators.required]
    });
    this.getStoreItems();
  }

  addItem() {

  }
  cancelItem() {
    this.dialogRef.close();
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
      }
    });
  }

  onOpenedItemSelectChange(event: IOpenedChange) {
    this.addLogisticItemsForm.controls['Item'].setValue(event.Value);
  }
}
