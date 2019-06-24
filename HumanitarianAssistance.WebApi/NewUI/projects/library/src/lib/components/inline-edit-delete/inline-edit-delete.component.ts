import { Component, OnInit, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';
import { IInlineEditDeleteModel } from './inline-edit-delete.model';

@Component({
  selector: 'lib-inline-edit-delete',
  templateUrl: './inline-edit-delete.component.html',
  styleUrls: ['./inline-edit-delete.component.css']
})
export class InlineEditDeleteComponent implements OnInit {


  @Input() value: IInlineEditDeleteModel;
  @Input() placeholder = '';
  @Input() type = 'text';
  @Input() required = false;
  @Input() disabled = false;

  @Output() editAction = new EventEmitter();
  @Output() deleteAction = new EventEmitter();

  constructor() {}

  ngOnInit() {
  }

  onEdit() {
    this.editAction.emit(this.value);
  }

  onDelete() {
    this.deleteAction.emit(this.value);
  }

}
