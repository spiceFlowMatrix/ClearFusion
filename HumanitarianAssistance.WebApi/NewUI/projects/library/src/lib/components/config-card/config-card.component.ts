import { Component, OnInit, HostListener, ElementRef, Input, OnChanges, ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'hum-config-card',
  templateUrl: './config-card.component.html',
  styleUrls: ['./config-card.component.css']
})
export class ConfigCardComponent implements OnInit, OnChanges {

  constructor(private eRef: ElementRef) { }
  @Input() showCard: boolean;

  isShow = false;
  ngOnChanges(): void {
    this.isShow = this.showCard
  }
  ngOnInit() {
    //this.showCard = false;
  }
  closeCard() {
    this.isShow = false;
    
  }


}
