import { OnInit, Input, Component, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'lib-inline-edit',
  templateUrl: './inline-edit.component.html',
  styleUrls: ['./inline-edit.component.css']

})

export class InlineEditComponent implements OnInit {

  @Input() id = '';
  @Input() value: valueModel = {};
  @Input() name = '';
  @Input() placeholder = '';
  @Input() type = 'text';
  @Input() required = false;
  @Input() disabled = false;

  @Output() action = new EventEmitter();
  @Output() addAction = new EventEmitter();

  // editing = false;

  constructor() { }

  ngOnInit() {
  }

  onAdd(value) {
    this.addAction.emit(this.value);
  }

  onEdit(value) {
    // this.editing = false;
    if (this.value.Id !== 0) {
      this.action.emit(this.value);
    } else {
      this.addAction.emit(this.value);
    }
  }

}

// tslint:disable-next-line:class-name
class valueModel {
  Id?: number;
  Name?: string;

  _IsDeleted?: boolean;
  _IsLoading?: boolean;
  _IsError?: boolean;
  // _error?: boolean;
}
