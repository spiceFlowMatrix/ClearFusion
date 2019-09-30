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
  constructor() { }

  @Input() options: Observable<Array<Object>>;
  @Input() placeHolder: string;
  // @Input() formControl: string;
  @Output() change = new EventEmitter<number>();
  dropControl = new FormControl('');
  val = '';


  onChange: any = () => { console.log('Change'); };
  onTouch: any = () => { };
  set value(val) {
    if (val !== undefined && this.val !== val) {
      this.val = val;
      this.onChange(val);
      this.onTouch(val);
    }

  }
  ngOnInit() {

  }
  optionChange(event) {
    this.change.emit(event);
  }
  writeValue(obj: any): void {
    this.value = obj;
    this.dropControl.setValue(obj);
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
    console.log('registerOnChange');
  }
  registerOnTouched(fn: any): void {
    // tslint:disable-next-line: quotemark
    console.log("registerOnTouched");
  }
  setDisabledState?(isDisabled: boolean): void {
    console.log('setDisabledState');
  }
}
