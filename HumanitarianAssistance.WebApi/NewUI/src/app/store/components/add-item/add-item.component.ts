import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ConfigService } from '../../services/config.service';
import { ToastrService } from 'ngx-toastr';
import { MasterInventoryItemModel } from '../../models/store-configuration';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.scss']
})
export class AddItemComponent implements OnInit {

  masterForm: FormGroup
  inventoryItem: MasterInventoryItemModel = {};
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddItemComponent>,
    @Inject(MAT_DIALOG_DATA) private data: MasterInventoryItemModel, private configService: ConfigService, private toastr: ToastrService) { }
  isSaving = false;
  ngOnInit() {
    this.createForm();
    if (this.data.ItemId) {
      this.masterForm.controls.name.setValue(this.data.ItemName);
      this.masterForm.controls.description.setValue(this.data.Description);

    }
  }
  createForm() {
    this.masterForm = this.fb.group({
      name: ['', Validators.required],
      description: ['']
    })
  }
  submit() {
    if (this.masterForm.valid) {
      this.isSaving = true;
      this.inventoryItem.Description = this.masterForm.controls.description.value;
      this.inventoryItem.ItemName = this.masterForm.controls.name.value;
      this.inventoryItem.ItemCode = this.data.ItemCode;
      this.inventoryItem.ItemGroupId = this.data.ItemGroupId;
      this.inventoryItem.ItemType = 4;
      this.inventoryItem.ItemInventory = this.data.ItemInventory;

      if (this.data.ItemId) {
        this.inventoryItem.ItemId = this.data.ItemId;
        this.configService.EditItem(this.inventoryItem).subscribe(() => {
          this.isSaving = false;
          this.toastr.success('Item updated successfully');
          this.dialogRef.close();
        })
      } else {
        this.configService.AddItem(this.inventoryItem).subscribe(() => {
          this.isSaving = false;
          this.toastr.success('Item added successfully');
          this.dialogRef.close();
        })
      }


    }
  }
  cancel() {
    this.dialogRef.close();
  }
}
