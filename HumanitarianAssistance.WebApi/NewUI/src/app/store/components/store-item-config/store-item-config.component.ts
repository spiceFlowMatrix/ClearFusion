import { Component, OnInit, Input } from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlattener, MatTreeFlatDataSource } from '@angular/material';
import { ConfigService } from '../../services/config.service';
import { InventoryModel } from '../../models/store-configuration';

@Component({
  selector: 'app-store-item-config',
  templateUrl: './store-item-config.component.html',
  styleUrls: ['./store-item-config.component.scss']
})
export class StoreItemConfigComponent implements OnInit {
  @Input() assetType = 1;
  inventories: InventoryModel[] = [];

  private _transformer = (node: InventoryModel, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.Name,
      id: node.Id,
      level: level,
      code:node.Code,
      description:node.Description,
      addText: node.addText,
      editText: node.editText
    };
  }
  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.children);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor(private configService: ConfigService) {

  }

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;
  ngOnInit() {
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
      console.log(this.inventories);
    })
  }

}

interface ItemNode {
  name: string;
  id: number;
  children?: ItemNode[];
}

const TREE_DATA: ItemNode[] = [
  {
    name: 'Fruit',
    id: 1,

    children: [
      {
        name: 'Apple', id: 1,
        children: [
          { name: 'Broccoli', id: 1 },
          { name: 'Brussel sprouts', id: 1 },
        ]
      },
      {
        name: 'Banana', id: 1,
        children: [
          { name: 'Broccoli', id: 1 },
          { name: 'Brussel sprouts', id: 1 },
        ]
      },
      {
        name: 'Fruit loops', id: 1,
        children: [
          { name: 'Broccoli', id: 1 },
          { name: 'Brussel sprouts', id: 1 },
        ]
      },
    ]
  }, {
    name: 'Vegetables',
    id: 1,
    children: [
      {
        name: 'Green',
        id: 1,
        children: [
          { name: 'Broccoli', id: 1 },
          { name: 'Brussel sprouts', id: 1 },
        ]
      }, {
        name: 'Orange',
        id: 1,
        children: [
          { name: 'Pumpkins', id: 1 },
          { name: 'Carrots', id: 1 },
        ]
      },
    ]
  },
];

/** Flat node with expandable and level information */
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
  code: string;
  description: string;
  addText: string;
  editText: string;
}

