import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { PurchaseService } from '../../services/purchase.service';
import { Observable, of, forkJoin } from 'rxjs';
import { IDropDownModel } from '../../models/purchase';

@Component({
  selector: 'app-add-purchase',
  templateUrl: './add-purchase.component.html',
  styleUrls: ['./add-purchase.component.scss']
})
export class AddPurchaseComponent implements OnInit {

  addPurchase: FormGroup;

  inventoryType$: Observable<IDropDownModel[]>;
  storeInventory$: Observable<IDropDownModel[]>;
  storeItemGroups: Observable<IDropDownModel[]>;
  storeItems$: Observable<IDropDownModel[]>;
  offices$: Observable<IDropDownModel[]>;
  project$: Observable<IDropDownModel[]>;
  budgetLine$: Observable<IDropDownModel[]>;
  assetType$: Observable<IDropDownModel[]>;
  unit$: Observable<IDropDownModel[]>;
  currency$: Observable<IDropDownModel[]>;
  storeSource$: Observable<IDropDownModel[]>;
  employeeList$: Observable<IDropDownModel[]>;
  receiptType$: Observable<IDropDownModel[]>;
  statusList$: Observable<IDropDownModel[]>;

  constructor(private purchaseService: PurchaseService,
    private fb: FormBuilder) {

    this.addPurchase = this.fb.group({
      'InventoryType': [2, [Validators.required]],
      'Inventory': [null, [Validators.required]],
      'ItemGroup': [null, [Validators.required]],
      'Item': [null, [Validators.required]],
      'Office': [null, [Validators.required]],
      'Project': [null],
      'BudgetLine': [null],
      'PurchaseOrderNo': [null],
      'PurchaseOrderDate': [null, [Validators.required]],
      'InvoiceDate': [null],
      'InvoiceNo': [null],
      'AssetType': [null],
      'Unit': [null],
      'Quantity': [null],
      'Currency': [null, [Validators.required]],
      'Price': [null, [Validators.required]],
      'ReceivedFromLocation': [null],
      'ReceivedFromEmployee': [null],
      'ReceiptType': [null, [Validators.required]],
      'Status': [null]
    });
    this.addPurchase.valueChanges.subscribe(r => {
      console.log(r);
    });
  }


  ngOnInit() {

    forkJoin([
      this.getAllInventoryTypes(),
      this.getAllProjects(),
      this.getAllOffice(),
      this.getAssetType(),
      this.getAllCurrency(),
      this.getStoreLocations(),
      this.getAllStatusAtTimeOfIssue(),
    ]).subscribe(result => {
      this.subscribeInventoryTypes(result[0]);
      this.subscribeAllProjects(result[1]);
      this.subscribeAllOffice(result[2]);
      this.subscribeAssetType(result[3]);
      this.subscribeAllCurrency(result[4]);
      this.subscribeStoreLocations(result[5]);
      this.subscribeAllStatusAtTimeOfIssue(result[6]);
    });
  }

  subscribeInventoryTypes(response: any) {

    this.inventoryType$ = of(response.Result.map(y => {
      return {
        value: y.Id,
        name: y.InventoryName
      };
    }));
  }

  subscribeAllProjects(response: any) {
    this.project$ = of(response.data.ProjectDetailModel.map(y => {
      return {
        value: y.ProjectId,
        name: y.ProjectCode + '-' + y.ProjectName
      };
    }));
  }

  subscribeAllOffice(response: any) {
    this.offices$ = of(response.data.OfficeDetailsList.map(y => {
      return {
        value: y.OfficeId,
        name: y.OfficeCode + '-' + y.OfficeName
      };
    }));
  }

  subscribeAssetType(response: any[]) {

    const asset: IDropDownModel[] = [];

    response.forEach(x => {
      asset.push({
        value: x.AssetTypeId,
        name: x.AssetTypeName
      });
    });

    this.assetType$ = of(asset);
  }

  subscribeAllCurrency(response: any) {
    this.currency$ = of(response.data.CurrencyList.map(y => {
      return {
        value: y.CurrencyId,
        name: y.CurrencyCode + '-' + y.CurrencyName
      };
    }));
  }

  subscribeStoreLocations(response: any) {
    this.storeSource$ = of(response.data.SourceCodeDatalist.map(y => {
      return {
        value: y.SourceCodeId,
        name: y.Code + '-' + y.Description
      };
    }));
  }

  subscribeAllStatusAtTimeOfIssue(response: any) {
    this.statusList$ = of(response.data.StatusAtTimeOfIssueList.map(y => {
      return {
        value: y.StatusAtTimeOfIssueId,
        name: y.StatusName
      };
    }));
  }

  getAllInventoryTypes() {
    return this.purchaseService.GetAllInventoryTypeList();
  }

  getAllProjects() {
    return this.purchaseService.GetAllProjectList();
  }

  getAllOffice() {
    return this.purchaseService.GetAllOfficeList();
  }

  getAssetType() {
    return this.purchaseService.getPurchaseAssetType();
  }

  getAllCurrency() {
    return this.purchaseService.getAllCurrency();
  }

  getStoreLocations() {
    return this.purchaseService.getAllStoreSource();
  }

  // getAllEmployeeList() {
  //   this.purchaseService.getEmployeesByOfficeId();
  // }

  getAllStatusAtTimeOfIssue() {
    return this.purchaseService.getAllStatusAtTimeOfIssue();
  }

  getInventoryTypeSelectedValue(event: any) {
    //  console.log(event);
    // this.addPurchase.get('InventoryType').patchValue(event);
    this.getInventoriesByInventoryTypeId(event);
  }

  getMasterInventorySelectedValue(event: any) {
    // this.getAllStoreItemGroups(event);
  }

  getInventoriesByInventoryTypeId(inventoryTypeId: number) {
    this.purchaseService
      .GetInventoriesByInventoryTypeId(inventoryTypeId)
      .subscribe(x => {

        this.storeInventory$ = of(x.data.map(y => {
          return {
            name: y.InventoryCode + '-' + y.InventoryName,
            value: y.InventoryId
          };
        }));
      });
  }

}
