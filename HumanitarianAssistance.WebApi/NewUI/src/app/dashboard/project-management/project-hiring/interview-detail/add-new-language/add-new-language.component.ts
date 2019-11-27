import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { HiringRequestsService } from '../../../project-list/hiring-requests/hiring-requests.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-add-new-language',
  templateUrl: './add-new-language.component.html',
  styleUrls: ['./add-new-language.component.scss']
})
export class AddNewLanguageComponent implements OnInit {

  languageDetailForm: FormGroup;
  ratingBasedDropDown: any[];
  constructor(
    private fb: FormBuilder,
    private commonLoader: CommonLoaderService,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AddNewLanguageComponent>,
  ) {
    this.ratingBasedDropDown = [
      {
        Id: 1,
        value: '1-Poor'
      },
      {
        Id: 2,
        value: '2-Good'
      },
      {
        Id: 3,
        value: '3-Very Good'
      },
      {
        Id: 4,
        value: '4-Excellent'
      }
    ];
    this.languageDetailForm = this.fb.group({
      LanguageName: ['', [Validators.required]],
      LanguageReading: ['', [Validators.required]],
      LanguageWriting: ['', [Validators.required]],
      LanguageListining: ['', [Validators.required]],
      LanguageSpeaking: ['', [Validators.required]]
    });
  }
  ngOnInit() {}
  onFormSubmit(data: any) {
    if (this.languageDetailForm.valid) {
    this.dialogRef.close(data);
    } else {
      this.toastr.warning('Form is Not Valid');
    }
  }
  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion
  onNoClick(): void {
    this.dialogRef.close();
  }
}
