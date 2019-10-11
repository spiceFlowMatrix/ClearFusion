import { Component, OnInit, Input, Output, EventEmitter, forwardRef, Self } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, FormControl, NgControl, Validators } from '@angular/forms';
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
  @Input() validation = false;
  @Input() disabled = false;
  // @Input() formControl: string;
  @Output() change = new EventEmitter<number>();

  public value: number;
  public isDisabled: boolean;
  private onChange;

  private onTouch;

  dropControl = new FormControl('');
  constructor() {
  }

  ngOnInit() {

  }
  // handle change event
  optionChange(event) {
    this.onChange(event);
    this.onTouch();
    this.value = event;
    this.change.emit(event);
  }
  // set a initial value
  writeValue(obj: any): void {
    this.value = obj;
    this.dropControl.setValue(obj);
    if (this.validation) {
      this.dropControl.setValidators([Validators.required]);
    }
    if (this.disabled) {
      this.dropControl.disable();
    }
  }
  // register a function for value changes
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  // register a function for value touched
  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }
}
