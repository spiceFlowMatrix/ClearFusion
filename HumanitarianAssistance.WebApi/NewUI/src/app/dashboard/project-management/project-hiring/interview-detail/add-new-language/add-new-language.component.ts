import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
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
      LanguageName: '',
      LanguageReading: '',
      LanguageWriting: '',
      LanguageListining: '',
      LanguageSpeaking: ''
    });
  }
  ngOnInit() {}
  onFormSubmit(data: any) {
    this.dialogRef.close(data);
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
