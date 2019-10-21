import { Component, OnInit } from '@angular/core';
import {
  applicationPages,
  applicationModule
} from '../../../shared/application-pages-enum';
import { CommonService } from '../../../service/common.service';

@Component({
  selector: 'app-store-master',
  templateUrl: './store-master.component.html',
  styleUrls: ['./store-master.component.css']
})
export class StoreMasterComponent implements OnInit {
  //#region "variables"
  storeMasterTabs = [
    // {
    //   id: 1,
    //   text: "Inventory",
    // },
    // {
    //   id: 2,
    //   text: "Inventory Items",
    // },
    {
      id: 3,
      text: 'Items Types'
    },
    {
      id: 4,
      text: 'Unit Types'
    },
    {
      id: 5,
      text: 'Item Specification'
    }
  ];
  isEditingAllowed = false;

  storeMasterSelectedTab = 3;

  //#endregion

  constructor(private commonservice: CommonService) {}

  ngOnInit() {
    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.Categories
    );
  }

  //#region "selectStoreMasterTab"
  selectStoreMasterTab(e) {
    if (e != null) {
      e.itemData.id === 1
        ? (this.storeMasterSelectedTab = 1)
        : e.itemData.id === 2
        ? (this.storeMasterSelectedTab = 2)
        : e.itemData.id === 3
        ? (this.storeMasterSelectedTab = 3)
        : e.itemData.id === 4
        ? (this.storeMasterSelectedTab = 4)
        : (this.storeMasterSelectedTab = 5);
    }
  }
  //#endregion
}

class InventoryModel {
  InventoryId: any;
  InventoryCode: any;
  InventoryName: any;
  InventoryDescription: any;
  InventoryDebitAccount: any;
  InventoryCreditAccount: any;
  AssetType: any;
}
