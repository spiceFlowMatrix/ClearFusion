import { Component, OnInit, HostListener, ElementRef, Input, OnChanges } from '@angular/core';

@Component({
  selector: 'app-purchase-filed-config',
  templateUrl: './purchase-filed-config.component.html',
  styleUrls: ['./purchase-filed-config.component.scss']
})
export class PurchaseFiledConfigComponent implements OnInit {


   showConfig = false;

  constructor(private eRef: ElementRef) { }
 
  ngOnInit() {
  }
  show() {
    this.showConfig = true;
    console.log(true);
  }

}
