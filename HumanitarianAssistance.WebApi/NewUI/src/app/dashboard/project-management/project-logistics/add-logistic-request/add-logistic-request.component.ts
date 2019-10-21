import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-add-logistic-request',
  templateUrl: './add-logistic-request.component.html',
  styleUrls: ['./add-logistic-request.component.scss']
})
export class AddLogisticRequestComponent implements OnInit {

  addLogisticRequestForm: FormGroup;
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddLogisticRequestComponent>) { }

  ngOnInit() {
    this.addLogisticRequestForm = this.fb.group({
      Name: ['', Validators.required],
      TotalCost: ['', Validators.required]
    });
  }

  addRequest() {

  }

  cancelRequest() {
    this.dialogRef.close();
  }
}
