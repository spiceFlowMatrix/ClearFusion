import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ProjectListService } from '../../../service/project-list.service';
import { ProgramModel } from '../../models/project-details.model';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-program-detail',
  templateUrl: './add-program-detail.component.html',
  styleUrls: ['./add-program-detail.component.scss']
})
export class AddProgramDetailComponent implements OnInit {
  addProgramDetailForm: FormGroup;
  isFormSubmitted = false;
  projectId: number;
  err = null;

  programDataEmit = new EventEmitter<any>();
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<any>,
    public projectListService: ProjectListService,
    public routeActive: ActivatedRoute,
    public route: Router,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit() {
    this.addProgramDetailForm = this.fb.group({
      ProgramName: [null, Validators.required]
    });

    this.projectId = this.data.Id;
    console.log(this.projectId);
  }
  onFormSubmit(formdata: any) {
    this.err = null;
    if (
      formdata !== undefined &&
      formdata != null &&
      this.addProgramDetailForm.valid &&
      this.projectId !== undefined
    ) {
      this.isFormSubmitted = true;
      const programModel: ProgramModel = {
        ProgramName: this.addProgramDetailForm.get('ProgramName').value,
        ProjectId: this.projectId
      };
      this.projectListService.AddProjectProgramDetail(programModel).subscribe(
        response => {
          if (response.StatusCode === 200) {
            this.programDataEmit.emit(true);
            this.isFormSubmitted = false;
            this.dialogRef.close();
          }
          if (response.StatusCode === 420) {
            this.err = 'Program already exist.';
            this.isFormSubmitted = false;
          }
        },
        error => {
          this.programDataEmit.emit(true);
          this.isFormSubmitted = false;
        }
      );
    }
  }

  onCancelPopup() {
    this.dialogRef.close();
  }
}
