import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-logistic-items',
  templateUrl: './add-logistic-items.component.html',
  styleUrls: ['./add-logistic-items.component.scss']
})
export class AddLogisticItemsComponent implements OnInit {

  addLogisticItemsForm: FormGroup;
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddLogisticItemsComponent>) { }

  ngOnInit() {
    this.addLogisticItemsForm = this.fb.group({
      Item: ['', Validators.required],
      Quantity: ['', Validators.required],
      EstimatedCost: ['', Validators.required]
    });
  }

  addItem() {

  }
  cancelItem() {
    this.dialogRef.close();
  }
}
