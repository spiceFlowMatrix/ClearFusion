import { Component, OnInit, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { AbstractControl, ControlValueAccessor, NG_VALUE_ACCESSOR, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'lib-hum-dropdown',
  templateUrl: './hum-dropdown.component.html',
  styleUrls: ['./hum-dropdown.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => HumDropdownComponent),
      multi: true

    }
  ]
})
export class HumDropdownComponent implements OnInit, ControlValueAccessor {

  @Input() options: Observable<Array<Object>>;
  @Input() placeHolder: string;
  // @Input() formControl: string;
  @Output() change = new EventEmitter<number>();
  dropControl = new FormControl('');
  constructor() { }

  ngOnInit() {

  }
  optionChange(event) {
    this.change.emit(event)
  }
  writeValue(obj: any): void {
    this.dropControl.setValue(obj);
  }
  registerOnChange(fn: any): void {
    console.log("registerOnChange")
  }
  registerOnTouched(fn: any): void {
    console.log("registerOnTouched")
  }
  setDisabledState?(isDisabled: boolean): void {
    console.log("setDisabledState")
  }
}
