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
  @Input() isSubmit = false;
  @Input() click = new EventEmitter<any>();

  btnType = '';
  constructor() { }

  ngOnInit() {
  //  this.isSubmit = false;
  }
  btnClick() {
    this.click.emit();
  }

}
