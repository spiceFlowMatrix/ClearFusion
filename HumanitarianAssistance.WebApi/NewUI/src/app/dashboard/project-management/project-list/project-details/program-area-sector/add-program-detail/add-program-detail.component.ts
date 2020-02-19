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
  programDataEmit = new EventEmitter<any>();
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<any>,
    public projectListService: ProjectListService,
    public routeActive: ActivatedRoute,
    public route: Router,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {

  }

  ngOnInit() {
    debugger;
    this.addProgramDetailForm = this.fb.group({
      ProgramName: [null, Validators.required]
    });

     this.projectId = this.data.Id;
    console.log(this.projectId);
  }
  onFormSubmit(formdata: any) {
    debugger;
    if (
      formdata !== undefined &&
      formdata != null &&
      this.addProgramDetailForm.valid && this.projectId !== undefined
    ) {
      const programModel: ProgramModel = {
        ProgramName: this.addProgramDetailForm.get('ProgramName').value,
        ProjectId: this.projectId
      };
      this.projectListService
        .AddProjectProgramDetail(programModel)
        .subscribe(response => {
          if (response.statusCode === 200) {
            this.programDataEmit.emit();
          }
        });
    }
    this.dialogRef.close();
  }

  onCancelPopup() {
    this.dialogRef.close();
  }
}
