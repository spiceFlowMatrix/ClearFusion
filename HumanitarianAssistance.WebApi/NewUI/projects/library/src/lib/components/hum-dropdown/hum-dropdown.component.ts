import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'lib-hum-dropdown',
  templateUrl: './hum-dropdown.component.html',
  styleUrls: ['./hum-dropdown.component.css']
})
export class HumDropdownComponent implements OnInit {
  @Input() options: Observable<Array<Object>>;
  @Input() placeHolder: string;
  @Input() formControl: AbstractControl;
  @Output() change = new  EventEmitter<number>();
  constructor() { }

  ngOnInit() {
  }
  optionChange(event) {
    this.change.emit(event)
  }
}
