import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ProjectListService } from '../../../service/project-list.service';
import { SectorModel } from '../../models/project-details.model';
import { Dialog } from 'primeng/primeng';

@Component({
  selector: 'app-add-sector-detail',
  templateUrl: './add-sector-detail.component.html',
  styleUrls: ['./add-sector-detail.component.scss']
})
export class AddSectorDetailComponent implements OnInit {
  addSectorDetailForm: FormGroup;
  isFormSubmitted = false;
  projectId: number;
  sectorDataEmit = new EventEmitter<any>();
  constructor(
    private fb: FormBuilder,
    public projectListService: ProjectListService,
    private dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit() {
    this.addSectorDetailForm = this.fb.group({
      SectorName: [null, Validators.required]
    });
    this.projectId = this.data.Id;
  }
  onFormSubmit(formdata: any) {
    debugger;
    if (
      formdata !== undefined &&
      formdata != null &&
      this.addSectorDetailForm.valid && this.projectId !== undefined
    ) {
      const sectorModel: SectorModel = {
        SectorName: this.addSectorDetailForm.get('SectorName').value,
        ProjectId: this.projectId
      };
      this.projectListService
        .AddProjectSectorDetail(sectorModel)
        .subscribe(response => {
          if (response.statusCode === 200) {
              this.sectorDataEmit.emit();
          }
        });
    }
    this.dialogRef.close();
  }
  onCancelPopup() {
    this.dialogRef.close();
  }
}
