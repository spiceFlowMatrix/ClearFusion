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
    { name: 'Store Item Id', value: 1, },
    { name: 'Store Item Code', value: 2 },
    { name: 'Store Item Name', value: 3 },
    { name: 'Store Item Code & Description', value: 4 },
    { name: 'Master Inventory Code', value: 5 },
    { name: 'Description', value: 6 },
    { name: 'Office Code', value: 7 },
    { name: 'BudgetLine', value: 8 },
    { name: 'Project', value: 9 },
    { name: 'Purchase Order Number', value: 10 },
    { name: 'Purchase Order Date', value: 11 },
    { name: 'Invoice Date', value: 12 },
    { name: 'AssetType', value: 13 },
    { name: 'Maker/Country', value: 14 },
    { name: 'Chasis No', value: 15 },
    { name: 'Engine/Serial No', value: 16 },
    { name: 'Registration No', value: 17 },
    { name: 'Identification No', value: 18 },
    { name: 'Model/Type', value: 19 },
    { name: 'Quantity', value: 21 },
    { name: 'Currency', value: 22 },
    { name: 'Receipt Date', value: 24 },
    { name: 'Depreciation Rate(%)', value: 25 },
    { name: 'Depreciation Value At Hand', value: 27 },
    { name: 'Received From Location', value: 29 },
    { name: 'Status', value: 30 },
    { name: 'Purchase Date', value: 34 },
    { name: 'Remarks', value: 35 },
    { name: 'Attachments', value: 36 }
  ];

  selectedOptions: any[] = [];

  constructor(private eRef: ElementRef, private toastr: ToastrService) { }

  ngOnInit() {
    this.getScreenSize();
  }

  show() {
    this.showConfig = true;
    console.log(true);
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
    console.log(this.selectedOptions);
  }
  change(list: any[]) {
    if (list.length > 7) {
      this.toastr.warning('7 Filter option can be selected at a time');
    } else {
       this.configFilterAppliedEvent.emit(list);
    }

  }

}
