import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'hum-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.css']
})
export class ButtonComponent implements OnInit {
  @Input() btnClass: string;
  @Input() type: string;
  @Input() text: string;
  @Output() click = new EventEmitter<any>();

  btnType = '';
  constructor() { }

  ngOnInit() {
  }
  btnClick() {
    this.click.emit();
  }

}
