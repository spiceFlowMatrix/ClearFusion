import { Component, OnInit, Input } from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlattener, MatTreeFlatDataSource, MatDialog } from '@angular/material';
import { ConfigService } from '../../services/config.service';
import { InventoryModel, MasterInventoryModel, MasterItemGroupModel, MasterInventoryItemModel } from '../../models/store-configuration';
import { AddMasterInventoryComponent } from '../add-master-inventory/add-master-inventory.component';
import { AddItemCategoryComponent } from '../add-item-category/add-item-category.component';
import { AddItemComponent } from '../add-item/add-item.component';

@Component({
  selector: 'app-store-item-config',
  templateUrl: './store-item-config.component.html',
  styleUrls: ['./store-item-config.component.scss']
})
export class StoreItemConfigComponent implements OnInit {
  @Input() assetType = 1;
  inventories: InventoryModel[] = [];
  modelWidth = '600px';

  masterInventory: MasterInventoryModel = {};
  masterInventoryGroup: MasterItemGroupModel = {};
  masterInventoryItem: MasterInventoryItemModel = {};


  private _transformer = (node: InventoryModel, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.Name,
      id: node.Id,
      invId: node.InventoryId,
      groupId: node.ItemGroupId,
      level: level,
      code: node.Code,
      description: node.Description,
      addText: node.addText,
      editText: node.editText,
      isTransportCategory: node.IsTransportCategory ? node.IsTransportCategory : null,
      itemTypeCategory: node.ItemTypeCategory
    };
  }
  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.children);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor(private configService: ConfigService, private dialog: MatDialog) {

  }

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;
  ngOnInit() {
    this.getAllInventories();
  }
  getAllInventories() {
    this.configService.getInventories(this.assetType).subscribe(res => {
      this.inventories = res.ResponseData;
      this.inventories = this.inventories.map(res => {
        res.addText = "add item category"
        res.editText = "edit master inventory"
        res.children.map(res2 => {
          res2.addText = "add item "
          res2.editText = "edit item category"
          res2.children.map(res3 => {
            res3.editText = "edit item"
          })
        })
        return res;
      })
      this.dataSource.data = this.inventories;
    })
  }
  // master inventory region
  addMasterInventory() {
    this.configService.getInventoryCode(this.assetType).subscribe(res => {
      this.masterInventory.InventoryCode = res.data.InventoryCode;
      this.masterInventory.AssetType = this.assetType;
      this.openMasterInv(this.masterInventory);
    })
  }
  openMasterInv(data: MasterInventoryModel) {
    this.masterInventory = data;
    const dg = this.dialog.open(AddMasterInventoryComponent, {
      width: this.modelWidth,
      data: this.masterInventory
    })
    dg.afterClosed().subscribe(res => {
      if (res == 1)
        this.getAllInventories();
    })
  }


  openItem(level, inventoryId, invId, isTransportCategory) {
    switch (level) {
      case 0:
        this.configService.getGroupItemCode(inventoryId, this.assetType).subscribe(res => {
          this.masterInventoryGroup.InventoryId = inventoryId;
          this.masterInventoryGroup.ItemGroupCode = res.data.ItemGroupCode;
          this.masterInventoryGroup.IsTransportCategory = isTransportCategory;
        })
        const dg = this.dialog.open(AddItemCategoryComponent, {
          width: this.modelWidth,
          data: this.masterInventoryGroup
        })
        dg.afterClosed().subscribe(res => {
          if (res == 1)
            this.getAllInventories();
        })
        break;
      case 1:

        this.configService.getItemCode(inventoryId, this.assetType).subscribe(res => {
          this.masterInventoryItem.ItemTypeCategory = Number(this.inventories.find(x => x.Id == invId).children.find(x => x.Id == inventoryId).ItemTypeCategory);
          if (this.masterInventoryItem.ItemTypeCategory) {
            this.masterInventoryItem.isGenerator = this.masterInventoryItem.ItemTypeCategory == 2 ? true : false;
          } else {
            this.masterInventoryItem.isGenerator = null
          }
          this.masterInventoryItem.ItemGroupId = new Number(inventoryId);
          this.masterInventoryItem.ItemCode = res.data.InventoryItemCode;
          this.masterInventoryItem.ItemInventory = new Number(invId);
          this.masterInventoryItem.AssetType = Number(this.assetType);
          const dgItem = this.dialog.open(AddItemComponent, {
            width: this.modelWidth,
            data: this.masterInventoryItem
          })
          dgItem.afterClosed().subscribe(res => {
            if (res == 1)
              this.getAllInventories();
          })
        })


        break;
      default:
        break;
    }

  }
  openEditItem(level, level2ID, level1ID, level0ID, isTransport, itemcattype) {
    switch (level) {
      case 0:
        const inventory = this.inventories.find(x => x.Id == level2ID);
        this.masterInventory.InventoryId = inventory.Id;
        this.masterInventory.AssetType = inventory.AssetType;
        this.masterInventory.InventoryCode = inventory.Code;
        this.masterInventory.InventoryDebitAccount = inventory.InventoryDebitAccount;
        this.masterInventory.InventoryDescription = inventory.Description;
        this.masterInventory.InventoryName = inventory.Name;
        this.masterInventory.IsTransportCategory = isTransport;
        const dg = this.dialog.open(AddMasterInventoryComponent, {
          width: this.modelWidth,
          data: this.masterInventory
        });
        dg.afterClosed().subscribe(res => {
          if (res == 1)
            this.getAllInventories();
        });
        break;
      case 1:

        const itemgroup = this.inventories.find(x => x.Id == level1ID).children.find(x => x.Id == level2ID);


        const isTransportCategory = this.inventories.find(x => x.Id == level1ID).IsTransportCategory;
        this.masterInventoryGroup.Description = itemgroup.Description;
        this.masterInventoryGroup.InventoryId = itemgroup.InventoryId;
        this.masterInventoryGroup.ItemGroupCode = itemgroup.Code;
        this.masterInventoryGroup.ItemGroupId = itemgroup.Id;
        this.masterInventoryGroup.ItemGroupName = itemgroup.Name;
        this.masterInventoryGroup.IsTransportCategory = isTransportCategory
        this.masterInventoryGroup.ItemTypeCategory = itemcattype;
        const dgGroup = this.dialog.open(AddItemCategoryComponent, {
          width: this.modelWidth,
          data: this.masterInventoryGroup
        })
        dgGroup.afterClosed().subscribe(res => {
          if (res == 1)
            this.getAllInventories();
        })
        break;
      case 2:
        const item = this.inventories.find(x => x.Id == level1ID).children.find(x => x.Id == level0ID).children.find(x => x.Id == level2ID);
        if (this.inventories.find(x => x.Id == level1ID).children.find(x => x.Id == level0ID).ItemTypeCategory != null) {
          this.masterInventoryItem.isGenerator = this.inventories.find(x => x.Id == level1ID).children.find(x => x.Id == level0ID).ItemTypeCategory == 2 ? true : false;
        } else {
          this.masterInventoryItem.isGenerator = null;
        }

        this.masterInventoryItem.ItemTypeCategory = Number(this.inventories.find(x => x.Id == level1ID).children.find(x => x.Id == level0ID).children.find(x => x.Id == level2ID).ItemTypeCategory);
        this.masterInventoryItem.Description = item.Description;
        this.masterInventoryItem.ItemCode = item.Code;
        this.masterInventoryItem.ItemGroupId = item.ItemGroupId;
        this.masterInventoryItem.ItemId = item.Id;
        this.masterInventoryItem.ItemInventory = item.InventoryId;
        this.masterInventoryItem.ItemName = item.Name;
        this.masterInventoryItem.ItemType = null;
        this.masterInventoryItem.AssetType = Number(this.assetType);
        // console.log(level, level2ID, level1ID, level0ID, isTransport, itemcattype)
        const dgItem = this.dialog.open(AddItemComponent, {
          width: this.modelWidth,
          data: this.masterInventoryItem
        })
        dgItem.afterClosed().subscribe(res => {
          if (res == 1)
            this.getAllInventories();

        })
        break;

      default:
        break;
    }

  }
}

/** Flat node with expandable and level information */
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  id: number;
  invId: number;
  groupId: number;
  level: number;
  code: string;
  description: string;
  addText: string;
  editText: string;
  isTransportCategory?: boolean;
  itemTypeCategory
}

