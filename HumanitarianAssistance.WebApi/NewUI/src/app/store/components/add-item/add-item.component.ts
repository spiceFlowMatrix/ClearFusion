import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ConfigService } from '../../services/config.service';
import { ToastrService } from 'ngx-toastr';
import { MasterInventoryItemModel } from '../../models/store-configuration';
import { Observable, of } from 'rxjs';
import { IDropDownModel } from '../../models/purchase';
import { map, reduce } from 'rxjs/operators';
import { PurchaseService } from '../../services/purchase.service';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.scss']
})
export class AddItemComponent implements OnInit {

  masterForm: FormGroup
  itemsTypes$: Observable<IDropDownModel[]>;
  unitTypes$: Observable<IDropDownModel[]>;
  inventoryItem: MasterInventoryItemModel = {};

  items: IDropDownModel[] = [
    { value: 1, name: 'Vehicle' },
    { value: 2, name: 'Generator' },
    { value: 3, name: 'Mobil oil' },
    { value: 4, name: 'Service & Maintenance' },
    { value: 5, name: 'Spare parts' },
    { value: 6, name: 'Fuel' }
  ]
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddItemComponent>,
    @Inject(MAT_DIALOG_DATA) private data: MasterInventoryItemModel,
    private configService: ConfigService, private toastr: ToastrService,
    private purchaseService: PurchaseService) { }
  isSaving = false;
  ngOnInit() {
    this.createForm();
    switch (this.data.AssetType) {
      case 1:
        const ids1 = [6, 3, 4]
        this.itemsTypes$ = of(this.items.filter(x => ids1.includes(x.value)));
        break;
      case 2:
        const ids2 = [5, 1, 2]
        this.itemsTypes$ = of(this.items.filter(x => ids2.includes(x.value)));
        break;
      case 3:
        const ids3 = [1, 2]
        this.itemsTypes$ = of(this.items.filter(x => ids3.includes(x.value)));
        break;
      default:
        break;
    }

    if (this.data.ItemId) {
      this.masterForm.controls.name.setValue(this.data.ItemName);
      this.masterForm.controls.description.setValue(this.data.Description);
      this.masterForm.controls.itemtypecategory.setValue(this.data.ItemTypeCategory ? this.data.ItemTypeCategory : null);
      this.masterForm.controls.defaultunittype.setValue(this.data.DefaultUnitType);
    }

    this.getAllUnitTypeDetails();
  }
  createForm() {
    this.masterForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      itemtypecategory: [''],
      defaultunittype: [null, Validators.required]
    });
  }
  submit() {
    if (this.masterForm.valid) {
      this.isSaving = true;
      this.inventoryItem.Description = this.masterForm.controls.description.value;
      this.inventoryItem.ItemName = this.masterForm.controls.name.value;
      this.inventoryItem.ItemCode = this.data.ItemCode;
      this.inventoryItem.ItemGroupId = this.data.ItemGroupId;
      this.inventoryItem.ItemType = null;
      this.inventoryItem.ItemInventory = this.data.ItemInventory;
      this.inventoryItem.ItemTypeCategory = this.masterForm.controls.itemtypecategory.value;
      this.inventoryItem.DefaultUnitType = this.masterForm.controls.defaultunittype.value;


      if (this.data.ItemId) {
        this.inventoryItem.ItemId = this.data.ItemId;
        this.configService.EditItem(this.inventoryItem).subscribe(() => {
          this.isSaving = false;
          this.toastr.success('Item updated successfully');
          this.dialogRef.close(1);
        });
      } else {
        this.configService.AddItem(this.inventoryItem).subscribe(() => {
          this.isSaving = false;
          this.toastr.success('Item added successfully');
          this.dialogRef.close(1);
        });
      }
    }
  }
  cancel() {
    this.dialogRef.close();
  }

  getAllUnitTypeDetails() {
    this.purchaseService.getAllUnitTypeDetails().subscribe(response => {
      this.unitTypes$ = of(
        response.data.PurchaseUnitTypeList.map(y => {
          return {
            name: y.UnitTypeName,
            value: y.UnitTypeId
          };
        }));
    })
  }
}
