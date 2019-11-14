import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MasterInventoryModel } from '../../models/store-configuration';
import { ConfigService } from '../../services/config.service';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { IDropDownModel } from '../../models/purchase';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-add-master-inventory',
  templateUrl: './add-master-inventory.component.html',
  styleUrls: ['./add-master-inventory.component.scss']
})
export class AddMasterInventoryComponent implements OnInit {

  masterForm: FormGroup
  masterInventory: MasterInventoryModel = {};
  accounts$: Observable<IDropDownModel[]>;
  isSaving = false;
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddMasterInventoryComponent>,
    @Inject(MAT_DIALOG_DATA) private data: MasterInventoryModel, private configService: ConfigService, private toastr: ToastrService) { }

  ngOnInit() {

    this.createForm();
    this.getAccountCodes();
    if (this.data.InventoryId) {
      this.masterForm.controls.name.setValue(this.data.InventoryName);
      this.masterForm.controls.description.setValue(this.data.InventoryDescription);
      this.masterForm.controls.accountId.setValue(this.data.InventoryDebitAccount);
      if(this.data.IsTransportCategory == true){
        this.masterForm.controls.istransport.setValue("0");
      }else{
        this.masterForm.controls.istransport.setValue("1");
      }
     
    } else {

    }


  }
  createForm() {
    this.masterForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      accountId: ['', Validators.required],
      istransport: [false]
    })
  }
  getAccountCodes() {
    this.configService.getAllAccounts().subscribe(res => {
      this.accounts$ = of(res.data.AccountDetailList).pipe(
        map(x => x.map(y => {
          return {
            name: y.AccountName,
            value: y.AccountCode
          }
        }))
      )
    });
  }
  cancel(res) {
    this.dialogRef.close(res);
  }
  submit() {
    if (this.masterForm.valid) {
      this.isSaving = true;
      this.masterInventory.AssetType = this.data.AssetType;
      this.masterInventory.InventoryCode = this.data.InventoryCode;
      this.masterInventory.InventoryName = this.masterForm.value.name;
      this.masterInventory.InventoryDescription = this.masterForm.value.description;
      this.masterInventory.InventoryDebitAccount = this.masterForm.value.accountId;
      this.masterInventory.InventoryCreditAccount = null;
      this.masterInventory.IsTransportCategory = (this.masterForm.value.istransport = "0") ? true : false;
      if (this.data.InventoryId) {
        this.masterInventory.InventoryId = this.data.InventoryId;
        this.configService.EditMasterInventory(this.masterInventory).subscribe(() => {
          this.isSaving = false;
          this.toastr.success('Inventory Updated');
          this.cancel(1)
        }
        )

      } else {
        this.configService.AddMasterInventory(this.masterInventory).subscribe(() => {
          this.isSaving = false;
          this.toastr.success('Inventory added');
          this.cancel(1)
        }
        )

      }

    }


  }
}
