import { Component, OnInit, HostListener, ElementRef, Input, OnChanges, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

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

  // Input/Output
  @Output() configFilterAppliedEvent = new EventEmitter<any>();

  columnsToShow = [
    { name: 'Store Item Id', modelName: 'ItemId', value: 1, },
    { name: 'Store Item Code', modelName: 'ItemCode', value: 2 },
    { name: 'Store Item Name', modelName: 'ItemName', value: 3 },
    { name: 'Store Item Code & Description', modelName: 'ItemCodeDescription', value: 4 },
    { name: 'Master Inventory Code', modelName: 'MasterInventoryCode', value: 5 },
    { name: 'Description', modelName: 'Description', value: 6 },
    { name: 'Office Code', modelName: 'OfficeCode', value: 7 },
    { name: 'BudgetLine', modelName: 'BudgetLineName', value: 8 },
    { name: 'Project', modelName: 'ProjectName', value: 9 },
    { name: 'Purchase Order Number', modelName: 'PurchaseOrderNumber', value: 10 },
    { name: 'Purchase Order Date', modelName: 'PurchaseDate', value: 11 },
    { name: 'Invoice Date', modelName: 'InvoiceDate', value: 12 },
    { name: 'AssetType', modelName: 'AssetTypeId', value: 13 },
    { name: 'Maker/Country', modelName: 'MakerCountry', value: 14 },
    { name: 'Chasis No', modelName: 'ChasisNo', value: 15 },
    { name: 'Engine/Serial No', modelName: 'EngineSerialNo', value: 16 },
    { name: 'Registration No', modelName: 'RegistrationNo', value: 17 },
    { name: 'Identification No', modelName: 'IdentificationNo',  value: 18 },
    { name: 'Model/Type', modelName: 'ModelType', value: 19 },
    { name: 'Quantity', modelName: 'PurchasedQuantity', value: 21 },
    { name: 'Currency', modelName: 'CurrencyName', value: 22 },
    { name: 'Receipt Date', modelName: 'ReceiptDate', value: 24 },
    { name: 'Depreciation Rate(%)', modelName: 'DepreciationRate', value: 25 },
    { name: 'Depreciation Value At Hand', modelName: 'DepreciatedCost', value: 27 },
    { name: 'Received From Location',  modelName: 'ReceivedFromLocationName', value: 29 },
    { name: 'Status', modelName: 'Status', value: 30 },
  ];

  selectedOptions: any[] = [];

  constructor(private eRef: ElementRef, private toastr: ToastrService) { }

  ngOnInit() {
    this.getScreenSize();
  }

  show() {
    this.showConfig = true;
   // this.cdr.detectChanges()
  }
  getState(e){
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

  applyConfigFilter() {
    // console.log(this.selectedOptions);
  }
  change(list: any[]) {
    if (list.length > 5) {
      this.toastr.warning('6 Filter option can be selected at a time');
    } else {
       this.configFilterAppliedEvent.emit(list);
    }

  }

}
