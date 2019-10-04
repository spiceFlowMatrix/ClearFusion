import { Component, OnInit, HostListener, ElementRef, Input, OnChanges, ChangeDetectorRef, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'hum-config-card',
  templateUrl: './config-card.component.html',
  styleUrls: ['./config-card.component.css']
})
export class ConfigCardComponent implements OnInit, OnChanges {

  constructor(private eRef: ElementRef) { }
  @Input() showCard: boolean;
  @Output() cardState = new EventEmitter<any>()

  isShow = false;
  ngOnChanges(): void {
    this.isShow = this.showCard
  }
  ngOnInit() {
    //this.showCard = false;
  }
  closeCard() {
    this.isShow = false;
    this.cardState.emit(this.isShow);
  }


}
