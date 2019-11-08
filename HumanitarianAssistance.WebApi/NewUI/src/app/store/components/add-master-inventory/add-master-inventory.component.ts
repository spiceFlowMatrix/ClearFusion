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
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddMasterInventoryComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any, private configService: ConfigService, private toastr: ToastrService) { }

  ngOnInit() {

    this.createForm();
    this.getAccountCodes();
  }
  createForm() {
    this.masterForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      accountId: ['', Validators.required]
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
  cancel() {
    this.dialogRef.close();
  }
  submit() {
    if (this.masterForm.value) {
      this.masterInventory.AssetType = this.data.data.AssetType;
      this.masterInventory.InventoryCode = this.data.data.InventoryCode;
      this.masterInventory.InventoryName = this.masterForm.value.name;
      this.masterInventory.InventoryDescription = this.masterForm.value.description;
      this.masterInventory.InventoryDebitAccount = this.masterForm.value.accountId;
      this.masterInventory.InventoryCreditAccount = null;
      this.configService.AddMasterInventory(this.masterInventory).subscribe(() => {
        this.toastr.success('Inventory added');
        this.cancel()
      }
      )
    }


  }
}
