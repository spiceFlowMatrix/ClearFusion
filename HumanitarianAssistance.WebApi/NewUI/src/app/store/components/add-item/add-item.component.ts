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
    @Inject(MAT_DIALOG_DATA) private data: any, private configService: ConfigService, private toastr: ToastrService) { }

  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.masterForm = this.fb.group({
      name: ['', Validators.required],
      description: ['']
    })
  }
  submit() {
    if (this.masterForm.valid) {
      this.inventoryItem.Description = this.masterForm.controls.description.value;
      this.inventoryItem.ItemName = this.masterForm.controls.name.value;
      this.inventoryItem.ItemCode = this.data.ItemCode;
      this.inventoryItem.ItemGroupId = this.data.ItemGroupId;
      this.inventoryItem.ItemType = 4;
      this.inventoryItem.ItemInventory = this.data.ItemInventory;
      this.configService.AddItem(this.inventoryItem).subscribe(() => {
       this.toastr.success('Item added successfully');
       this.dialogRef.close();
      })
    }
  }
  cancel(){
    this.dialogRef.close();
  }
}
