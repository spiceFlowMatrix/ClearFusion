import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IAddExtensionDataSources } from '../../models/project-activities.model';
import { Subscription } from 'rxjs/internal/Subscription';
import { ProjectActivitiesService } from '../../service/project-activities.service';
import { ToastrService } from 'ngx-toastr';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-add-extensions',
  templateUrl: './add-extensions.component.html',
  styleUrls: ['./add-extensions.component.scss']
})
export class AddExtensionsComponent implements OnInit {

  extensionForm: FormGroup;
  addExtensionLoaderFlag = false;
  activityId: number;

  onListRefresh = new EventEmitter();
  addExtensionSubscription: Subscription;


  constructor(private fb: FormBuilder,
    private activitiesService: ProjectActivitiesService,
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AddExtensionsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IAddExtensionDataSources
    ) {
      this.activityId = data.ActivityId;
    }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.extensionForm = this.fb.group({
      StartDate: [new Date(), Validators.required],
      EndDate: [null, Validators.required],
      Description: ['', Validators.required],
    });
  }


  addExtension(data: any) {
    if (this.extensionForm.valid) {
      this.addExtensionLoaderFlag = true;

      const extensionData: any = {
        // Planning
        ActivityId: this.activityId,
        StartDate: StaticUtilities.getLocalDate(data.StartDate),
        EndDate: StaticUtilities.getLocalDate(data.EndDate),
        Description: data.Description,
      };

        this.addExtensionSubscription = this.activitiesService
          .AddProjectActivityExtension(extensionData)
          .subscribe(
            (response: IResponseData) => {
              if (response.statusCode === 200) {
                this.onCancelPopup();
                this.onListRefresh.emit();
              } else {
                this.toastr.error(response.message);
              }
              this.addExtensionLoaderFlag = false;
            },
            error => {
              this.toastr.error('Someting went wrong');
              this.addExtensionLoaderFlag = false;
            }
          );
      }
    }


  onFormSubmit(data: any) {
    this.addExtension(data);
  }

  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion

  get extensionStartDate() {
    return this.extensionForm.get('StartDate').value;
  }
}
