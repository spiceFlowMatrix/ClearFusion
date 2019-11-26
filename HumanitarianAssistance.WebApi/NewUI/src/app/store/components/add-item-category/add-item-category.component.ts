import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MasterItemGroupModel } from '../../models/store-configuration';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ConfigService } from '../../services/config.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-item-category',
  templateUrl: './add-item-category.component.html',
  styleUrls: ['./add-item-category.component.scss']
})
export class AddItemCategoryComponent implements OnInit {

  masterForm: FormGroup
  masterInventoryCategory: MasterItemGroupModel = {};
  isSaving = false;
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddItemCategoryComponent>,
    @Inject(MAT_DIALOG_DATA) private data: MasterItemGroupModel, private configService: ConfigService, private toastr: ToastrService) { }

  ngOnInit() {
    this.createForm();
    if (this.data.ItemGroupId) {
      this.masterForm.controls.name.setValue(this.data.ItemGroupName);
      this.masterForm.controls.description.setValue(this.data.Description);
      this.masterForm.controls.inventorytype.setValue(this.data.ItemTypeCategory?this.data.ItemTypeCategory.toString():null);
    }
  }
  createForm() {
    this.masterForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      inventorytype:['1']
    })
  }
  submit() {
    if (this.masterForm.valid) {
      this.isSaving = true;
      this.masterInventoryCategory.Description = this.masterForm.controls.description.value;
      this.masterInventoryCategory.InventoryId = this.data.InventoryId;
      this.masterInventoryCategory.ItemGroupCode = this.data.ItemGroupCode;
      this.masterInventoryCategory.ItemGroupName = this.masterForm.controls.name.value;
      this.masterInventoryCategory.ItemTypeCategory = this.data.IsTransportCategory ?
                                                      Number(this.masterForm.controls.inventorytype.value) : null;
      if (this.data.ItemGroupId) {
        this.masterInventoryCategory.ItemGroupId = this.data.ItemGroupId;
        this.configService.EditItemGroup(this.masterInventoryCategory).subscribe(() => {
          this.isSaving = false;
          this.toastr.success("Group updated successfully");
          this.dialogRef.close(1);
        })

      } else {
        this.configService.AddItemGroup(this.masterInventoryCategory).subscribe(() => {
          this.isSaving = false;
          this.toastr.success("Group added successfully");
          this.dialogRef.close(1);
        })
      }

    }
    console.log(this.masterForm.value);
  }
  cancel() {
    this.dialogRef.close();
  }
}
