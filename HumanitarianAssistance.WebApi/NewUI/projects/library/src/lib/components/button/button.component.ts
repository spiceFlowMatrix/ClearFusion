import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'hum-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.css']
})
export class ButtonComponent implements OnInit {
  @Input() btnClass: string;
  @Input() type: string;
  @Input() text: string;

  btnType = ''
  constructor() { }

  ngOnInit() {
  }

}
