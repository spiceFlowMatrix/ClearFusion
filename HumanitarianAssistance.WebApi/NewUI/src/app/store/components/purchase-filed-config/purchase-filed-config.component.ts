import { Component, OnInit, HostListener, ElementRef, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FieldConfigService } from '../../services/field-config.service';

@Component({
  selector: 'app-purchase-filed-config',
  templateUrl: './purchase-filed-config.component.html',
  styleUrls: ['./purchase-filed-config.component.scss']
})
export class PurchaseFiledConfigComponent implements OnInit {


  showConfig = false;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  // name = field to be shown on config filter
  // headerName= purchaselist header name
  // modelName=  purchaselist model key name
  // isSelected= keys to be shown checked by default
  // value = used at the time of pdf download for identifying selected columns to be printed on pdf

  columnsToShow = [
    { name: 'Purchase Id', headerName: 'Id', modelName: 'Id', isSelected: true, value: 1 },
    { name: 'Store Item Name', headerName: 'Item', modelName: 'Item', isSelected: true, value: 2 },
    { name: 'Purchased By', headerName: 'Purchased By', modelName: 'PurchasedBy', isSelected: true, value: 3 },
    { name: 'Project', headerName: 'Project', modelName: 'Project', isSelected: true, value: 4 },
    { name: 'Original Cost', headerName: 'Original Cost', modelName: 'OriginalCost', isSelected: true, value: 5 },
    { name: 'Depreciated Cost', headerName: 'Depreciated Cost', modelName: 'DepreciatedCost', isSelected: true, value: 6 },
    { name: 'Purchase Date', headerName: 'Purchase Date', modelName: 'PurchaseDate', isSelected: false, value: 7 },
    { name: 'Currency', headerName: 'Currency', modelName: 'Currency', isSelected: false, value: 8 },
    { name: 'PurchasedQuantity', headerName: 'PurchasedQuantity', modelName: 'PurchasedQuantity', isSelected: false, value: 9 },
    { name: 'Item Code', headerName: 'Item Code', modelName: 'ItemCode', isSelected: false, value: 10 },
    { name: 'Project Id', headerName: 'Project Id', modelName: 'ProjectId', isSelected: false, value: 11 },
    { name: 'Item Code Description', headerName: 'Item Code Description', modelName: 'ItemCodeDescription', isSelected: false, value: 12 },
    { name: 'Description', headerName: 'Description', modelName: 'Description', isSelected: false, value: 13 },
    { name: 'BudgetLine Name', headerName: 'BudgetLine Name', modelName: 'BudgetLineName', isSelected: false, value: 14 },
    { name: 'Depreciation Rate', headerName: 'Depreciation Rate', modelName: 'DepreciationRate', isSelected: false, value: 15 },
    { name: 'Master Inventory Code', headerName: 'Master Inventory Code', modelName: 'MasterInventoryCode', isSelected: false, value: 16 },
    { name: 'Office Code', headerName: 'Office Code', modelName: 'OfficeCode', isSelected: false, value: 17 },
    { name: 'Receipt Date', headerName: 'Receipt Date', modelName: 'ReceiptDate', isSelected: false, value: 18 },
    { name: 'Invoice Date', headerName: 'Invoice Date', modelName: 'InvoiceDate', isSelected: false, value: 19 },
    { name: 'Received From Location', headerName: 'Received From Location', modelName: 'ReceivedFromLocationName', isSelected: false,
      value: 20 },
    { name: 'Status', headerName: 'Status', modelName: 'Status', isSelected: false, value: 21 }
  ];
  selectedOptions: any[] = [];

  constructor(private eRef: ElementRef, private toastr: ToastrService, private fieldConfig: FieldConfigService) { }

  ngOnInit() {
    this.getScreenSize();
    this.change(this.columnsToShow.slice(0, 6));
    this.selectedOptions.push(this.columnsToShow.slice(0, 6));
  }

  show() {
    this.showConfig = true;
  }
  getState(e) {
    this.showConfig = e;
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 170 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  change(list: any[]) {
    this.fieldConfig.updateList(list);
  }
}
