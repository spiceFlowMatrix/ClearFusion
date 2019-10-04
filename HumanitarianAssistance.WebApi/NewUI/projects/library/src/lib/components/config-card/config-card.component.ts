import { Component, OnInit, HostListener, ElementRef, Input, OnChanges } from '@angular/core';

@Component({
  selector: 'hum-config-card',
  templateUrl: './config-card.component.html',
  styleUrls: ['./config-card.component.css']
})
export class ConfigCardComponent implements OnInit {
  constructor(private eRef: ElementRef) { }
 

  ngOnInit() {
  }


}
