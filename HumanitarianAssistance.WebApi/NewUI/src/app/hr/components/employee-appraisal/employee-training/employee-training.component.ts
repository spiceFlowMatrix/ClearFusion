import { Component, OnInit, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-employee-training',
  templateUrl: './employee-training.component.html',
  styleUrls: ['./employee-training.component.scss']
})
export class EmployeeTrainingComponent implements OnInit {
  trainingDetailForm: FormGroup;
  isFormSubmitted = false;
  trainingformDataEmit = new EventEmitter<any>();

  trainingProgram = [
    {
      id: 1,
      value: 'Organization vision & Objectives'
    },
    { id: 2, value: 'Employee\'s Work' }
  ];

  yesNoItem = [
    { id: 1, value: 'Yes' },
    { id: 2, value: 'No' }
  ];

  catchLevelItem = [
    { id: 1, value: '1 - Weak' },
    { id: 2, value: '2 - Satisfactory' },
    { id: 3, value: '3 - Average' },
    { id: 4, value: '4 - Good' },
    { id: 5, value: '5 - Excellent' }
  ];

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<any>) {}

  ngOnInit() {
    this.trainingDetailForm = this.fb.group({
      TrainingProgramBasedOn: [null, Validators.required],
      Program: [null, Validators.required],
      Participated: [null, Validators.required],
      CatchLevel: [null, Validators.required],
      RefresherTrm: [null, Validators.required],
      OtherRecommemenedTraining: [null, Validators.required]
    });
  }

  onFormSubmit(data: any) {
    if (this.trainingDetailForm.valid) {
      this.isFormSubmitted = true;
      this.trainingformDataEmit.emit(data);
      this.dialogRef.close();
    }
  }

  onCancelPopup() {
    this.dialogRef.close();
  }
}
