import { ILanguageDetailModel } from './../../../../dashboard/project-management/project-hiring/models/hiring-requests-models';
import { IDropDownModel } from 'src/app/dashboard/accounting/report-services/report-models';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { EmployeeHistoryService } from 'src/app/hr/services/employee-history.service';
import { of } from 'rxjs';

@Component({
  selector: 'app-add-language',
  templateUrl: './add-language.component.html',
  styleUrls: ['./add-language.component.scss']
})
export class AddLanguageComponent implements OnInit {
  languageDetailForm: FormGroup;
  ratingBasedDropDown: any[];
  isFormSubmitted = false;
  employeeId: number;
  onLanguageDetailListRefresh = new EventEmitter();
  languageList$: Observable<IDropDownModel[]>;
  constructor(
    private fb: FormBuilder,
    private commonLoader: CommonLoaderService,
    private toastr: ToastrService,
    private employeeHistoryService: EmployeeHistoryService,
    public dialogRef: MatDialogRef<AddLanguageComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
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
      EmployeeID: [''],
      LanguageId: ['', [Validators.required]],
      Reading: ['', [Validators.required]],
      Writing: ['', [Validators.required]],
      Listening: ['', [Validators.required]],
      Speaking: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.employeeId = this.data.employeeId;
    this.languageDetailForm.controls['EmployeeID'].setValue(this.employeeId);
    this.getLanguageList();
  }

    //#region "get Language  List"
    getLanguageList() {
      this.commonLoader.showLoader();
      this.employeeHistoryService
        .GetLanguageList()
        .subscribe(
          x => {
            this.commonLoader.hideLoader();
            if (x.data.LanguageDetail.length > 0) {
              this.languageList$ = of(
                x.data.LanguageDetail.map(y => {
                  return {
                    value: y.LanguageId,
                    name: y.LanguageName
                  } as IDropDownModel;
                })
              );
            }
          },
          () => {
            this.commonLoader.hideLoader();
          }
        );
    }
    //#endregion

  onFormSubmit(data: any) {
    if (this.languageDetailForm.valid) {
      this.isFormSubmitted = true;
      this.employeeHistoryService.addLanguageDetail(data).subscribe(
        x => {
          if (x.StatusCode === 200) {
            this.toastr.success('Success');
            this.isFormSubmitted = false;
            this.dialogRef.close();
            this.AddLanguageDetailListRefresh();
          } else {
            this.toastr.warning(x.Message);
            this.isFormSubmitted = false;
          }
        },
        error => {
          this.toastr.warning(error);
          this.isFormSubmitted = false;
        }
      );
    }
  }
   //#region "Add Language Detail List Refresh"
   AddLanguageDetailListRefresh() {
    this.onLanguageDetailListRefresh.emit();
  }
  //#endregion
  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion
  onNoClick(): void {
    this.dialogRef.close();
  }
}
