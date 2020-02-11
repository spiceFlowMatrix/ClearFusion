import { Component, OnInit, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-add-weak-points',
  templateUrl: './add-weak-points.component.html',
  styleUrls: ['./add-weak-points.component.scss']
})
export class AddWeakPointsComponent implements OnInit {
  addWeakPointDetailForm: FormGroup;
  isFormSubmitted = false;
  weakPointDataEmit = new EventEmitter<any>();
  constructor(private dialogRef: MatDialogRef<any>,
    private fb: FormBuilder) { }

  ngOnInit() {
    this.addWeakPointDetailForm = this.fb.group({
      WeakPoints: [null, Validators.required],
      AppraisalWeakPointsId: [null]
    });
  }

  onFormSubmit(formdata: any){
    if (formdata !== undefined && formdata != null) {
      this.weakPointDataEmit.emit(formdata);
    }
  }
  onCancelPopup() {
    this.dialogRef.close();
  }
}
