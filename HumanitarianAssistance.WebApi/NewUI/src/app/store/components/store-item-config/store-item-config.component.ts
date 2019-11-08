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
      editText: node.editText
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

      console.log(this.dataSource.data);
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
    const dg = this.dialog.open(AddMasterInventoryComponent, {
      width: this.modelWidth,
      data: { data }
    })
    dg.afterClosed().subscribe(res => {
      this.getAllInventories();
    })
  }


  openItem(level, inventoryId, invId) {
    switch (level) {
      case 0:
        this.configService.getGroupItemCode(inventoryId, this.assetType).subscribe(res => {
          this.masterInventoryGroup.InventoryId = inventoryId;
          this.masterInventoryGroup.ItemGroupCode = res.data.ItemGroupCode
        })
        const dg = this.dialog.open(AddItemCategoryComponent, {
          width: this.modelWidth,
          data: this.masterInventoryGroup
        })
        dg.afterClosed().subscribe(res => {
          this.getAllInventories();
        })
        break;
      case 1:

        this.configService.getItemCode(inventoryId, this.assetType).subscribe(res => {
          this.masterInventoryItem.ItemGroupId = new Number(inventoryId);
          this.masterInventoryItem.ItemCode = res.data.InventoryItemCode;
          this.masterInventoryItem.ItemInventory = new Number(invId);
        })
        const dgItem = this.dialog.open(AddItemComponent, {
          width: this.modelWidth,
          data: this.masterInventoryItem
        })
        dgItem.afterClosed().subscribe(res => {
          this.getAllInventories();
        })
        break;
      default:
        break;
    }

  }
  openEditItem(level, level2ID, level1ID, level0ID) {

    console.log(level, level2ID, level1ID, level0ID)
    switch (level) {
      case 0:
        const inventory = this.inventories.find(x => x.Id == level2ID);
        console.log(inventory);
        break;
      case 1:
        const itemgroup = this.inventories.find(x => x.Id == level1ID).children.find(x => x.Id == level2ID);
        console.log(itemgroup);
        break;
      case 2:
        const item = this.inventories.find(x => x.Id == level1ID).children.find(x => x.Id == level0ID).children.find(x => x.Id = level2ID);
        console.log(item);
        break;

      default:
        break;
    }

    // switch (level) {
    //   case 0:
    //     this.configService.getGroupItemCode(inventoryId, this.assetType).subscribe(res => {
    //       this.masterInventoryGroup.InventoryId = inventoryId;
    //       this.masterInventoryGroup.ItemGroupCode = res.data.ItemGroupCode
    //     })
    //     const dg = this.dialog.open(AddItemCategoryComponent, {
    //       width: this.modelWidth,
    //       data: this.masterInventoryGroup
    //     })
    //     dg.afterClosed().subscribe(res => {
    //       this.getAllInventories();
    //     })
    //     break;
    //   case 1:

    //     this.configService.getItemCode(inventoryId, this.assetType).subscribe(res => {
    //       this.masterInventoryItem.ItemGroupId = new Number(inventoryId);
    //       this.masterInventoryItem.ItemCode = res.data.InventoryItemCode;
    //       this.masterInventoryItem.ItemInventory =  new Number(invId);
    //     })
    //     const dgItem = this.dialog.open(AddItemComponent, {
    //       width: this.modelWidth,
    //       data: this.masterInventoryItem
    //     })
    //     dgItem.afterClosed().subscribe(res => {
    //       this.getAllInventories();
    //     })
    //     break;
    //   default:
    //     break;
    // }

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
}

