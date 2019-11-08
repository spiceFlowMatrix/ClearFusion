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
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddItemCategoryComponent>,
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
      this.masterInventoryCategory.Description = this.masterForm.controls.description.value;
      this.masterInventoryCategory.InventoryId = this.data.InventoryId;
      this.masterInventoryCategory.ItemGroupCode = this.data.ItemGroupCode;
      this.masterInventoryCategory.ItemGroupName = this.masterForm.controls.name.value;
      this.configService.AddItemGroup(this.masterInventoryCategory).subscribe(() => {
        this.toastr.success("Group Added successfully");
        this.dialogRef.close();
      })
    }
    console.log(this.masterForm.value);
  }
  cancel() {
    this.dialogRef.close();
  }
}
