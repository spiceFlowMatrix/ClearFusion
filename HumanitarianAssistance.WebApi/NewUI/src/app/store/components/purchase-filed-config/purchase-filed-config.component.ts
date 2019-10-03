import { Component, OnInit, HostListener, ElementRef, Input, OnChanges } from '@angular/core';

@Component({
  selector: 'app-purchase-filed-config',
  templateUrl: './purchase-filed-config.component.html',
  styleUrls: ['./purchase-filed-config.component.scss']
})
export class PurchaseFiledConfigComponent implements OnInit {


  @Input() showConfig = false;

  constructor(private eRef : ElementRef) { }
  @HostListener('document:click', ['$event'])
  clickout(event) {
    if (this.eRef.nativeElement.contains(event.target)) {
      // this.showCard = true;
    } else {
      if (this.showConfig) {
        this.showConfig = false;
        console.log('test')
      }

    }
  }

  ngOnInit() {
  }

}
