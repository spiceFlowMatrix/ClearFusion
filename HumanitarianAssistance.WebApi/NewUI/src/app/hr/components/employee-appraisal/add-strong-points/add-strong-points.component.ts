import { Component, OnInit, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-add-strong-points',
  templateUrl: './add-strong-points.component.html',
  styleUrls: ['./add-strong-points.component.scss']
})
export class AddStrongPointsComponent implements OnInit {
  addStrongPointDetailForm: FormGroup;
  isFormSubmitted = false;
  strongPointDataEmit = new EventEmitter<any>();
  constructor(private dialogRef: MatDialogRef<any>,private fb: FormBuilder) {}

  ngOnInit() {
    this.addStrongPointDetailForm = this.fb.group({
      StrongPoints: [null, Validators.required],
      AppraisalStrongPointsId: [null]
    });
  }

  onFormSubmit(formdata: any) {
    if (formdata !== undefined && formdata != null) {
      this.strongPointDataEmit.emit(formdata);
    }
  }

  onCancelPopup() {
    this.dialogRef.close();
  }
}
