import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { PurchaseService } from '../../services/purchase.service';
import { Observable, of, forkJoin } from 'rxjs';
import { IDropDownModel } from '../../models/purchase';

@Component({
  selector: 'app-add-purchase',
  templateUrl: './add-purchase.component.html',
  styleUrls: ['./add-purchase.component.scss']
})
export class AddPurchaseComponent implements OnInit {

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

  constructor(private purchaseService: PurchaseService) { }

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
    this.inventoryType$ = of(response.InventoryTypes.map(y => {
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

  subscribeAssetType(response: any) {
    this.assetType$ = of(response.map(y => {
          return {
            value: y.AssetTypeId,
            name: y.AssetTypeName
          };
        }));
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
    return this.purchaseService.GetAllInventoryTypeList()
    // .subscribe(x => {
    //   this.inventoryType$ = of(x.InventoryTypes.map(y => {
    //     return {
    //       value: y.Id,
    //       name: y.InventoryName
    //     };
    //   }));
    // });
  }

  getAllProjects() {
    return this.purchaseService.GetAllProjectList()
    // .subscribe(x => {
    //   this.project$ = of(x.data.ProjectDetailModel.map(y => {
    //     return {
    //       value: y.ProjectId,
    //       name: y.ProjectCode + '-' + y.ProjectName
    //     };
    //   }));
    // });
  }

  getAllOffice() {
    return this.purchaseService.GetAllOfficeList()
    // .subscribe(x => {
    //   this.offices$ = of(x.data.OfficeDetailsList.map(y => {
    //     return {
    //       value: y.OfficeId,
    //       name: y.OfficeCode + '-' + y.OfficeName
    //     };
    //   }));
    // });
  }

  getAssetType() {
    return this.purchaseService.getPurchaseAssetType()
    // .subscribe(x => {
    //   this.offices$ = of(x.map(y => {
    //     return {
    //       value: y.AssetTypeId,
    //       name: y.AssetTypeName
    //     };
    //   }));
    // });
  }

  getAllCurrency() {
    return this.purchaseService.getAllCurrency()
    // .subscribe(x => {
    //   this.offices$ = of(x.data.CurrencyList.map(y => {
    //     return {
    //       value: y.CurrencyId,
    //       name: y.CurrencyCode + '-' + y.CurrencyName
    //     };
    //   }));
    // });
  }

  getStoreLocations() {
    return this.purchaseService.getAllStoreSource()
    // .subscribe(x => {
    //   this.storeSource$ = of(x.data.SourceCodeDatalist.map(y => {
    //     return {
    //       value: y.SourceCodeId,
    //       name: y.Code + '-' + y.Description
    //     };
    //   }));
    // });
  }

  // getAllEmployeeList() {
  //   this.purchaseService.getEmployeesByOfficeId();
  // }

  getAllStatusAtTimeOfIssue() {
    return this.purchaseService.getAllStatusAtTimeOfIssue()
    // .subscribe(x => {
    //   this.statusList$ = of(x.data.StatusAtTimeOfIssueList.map(y => {
    //     return {
    //       value: y.StatusAtTimeOfIssueId,
    //       name: y.StatusName
    //     };
    //   }));
    // });
  }

}
