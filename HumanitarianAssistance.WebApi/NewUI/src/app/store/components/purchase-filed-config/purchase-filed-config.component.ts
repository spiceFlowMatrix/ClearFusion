import { Component, OnInit, HostListener, ElementRef, Input, OnChanges, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-purchase-filed-config',
  templateUrl: './purchase-filed-config.component.html',
  styleUrls: ['./purchase-filed-config.component.scss']
})
export class PurchaseFiledConfigComponent implements OnInit {


  showConfig = false;

  constructor(private cdr: ChangeDetectorRef) { }

  ngOnInit() {
  }
  show() {
    this.showConfig = true;
   // this.cdr.detectChanges()
  }
  getState(e){
    this.showConfig = e;

  }

}
