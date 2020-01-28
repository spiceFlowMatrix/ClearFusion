import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { EmployeeAdvanceService } from 'src/app/hr/services/employee-advance.service';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { of } from 'rxjs/internal/observable/of';
import { Observable } from 'rxjs/internal/Observable';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { AdvanceBtnStatus } from 'src/app/shared/enum';

@Component({
  selector: 'app-new-advance-request',
  templateUrl: './new-advance-request.component.html',
  styleUrls: ['./new-advance-request.component.scss']
})
export class NewAdvanceRequestComponent implements OnInit {

  newAdvanceRequestForm: FormGroup;
  isFormSubmitted = false;
  headerText = 'Add Advance Request';
  advanceBtnStatus = AdvanceBtnStatus; // use enum in html
  employeeList$: Observable<IDropDownModel[]>;

  constructor(private dialogRef: MatDialogRef<NewAdvanceRequestComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private fb: FormBuilder,
    private toastr: ToastrService, private advanceService: EmployeeAdvanceService,
    private commonLoader: CommonLoaderService) {
      this.onFormInIt();
    }

  ngOnInit() {
    debugger;
    this.getEmployeeList();
    if (this.data.btnStatus === AdvanceBtnStatus.NEW) {
      this.headerText = 'Add Advance Request';
    } else if (this.data.btnStatus === AdvanceBtnStatus.APPROVE) {
      this.headerText = 'Approve Advance';
      const control = new FormControl();
      this.newAdvanceRequestForm.addControl('AdvanceAmount', control);
      this.getAdvanceDetailById();
    } else if (this.data.btnStatus === AdvanceBtnStatus.EDIT) {
      this.headerText = 'Edit Advance';
      this.getAdvanceDetailById();
    }
  }

  onFormInIt() {
    this.newAdvanceRequestForm = this.fb.group({
      'AdvanceId': [null],
      'AdvanceDate': [new Date(), [Validators.required]],
      'ApprovedBy': [null, [Validators.required]],
      'NumberOfInstallments': [null, [Validators.required, Validators.min(1)]],
      'ModeOfReturn': [null, [Validators.required]],
      'RequestAmount': [null, [Validators.required]],
      'Description': [null, [Validators.required]]
    });
  }

  closeDialog() {
    this.dialogRef.close();
  }

  getEmployeeList() {
    this.advanceService.getEmployeeList().subscribe(x =>
      this.employeeList$ = of(x.data.EmployeeDetailListData.map(y => {
        return {
          name: y.CodeEmployeeName,
          value: y.EmployeeId
        };
      }))
      );
  }

  onFormSubmit() {
    if (!this.newAdvanceRequestForm.valid) {
      this.toastr.warning('Please correct form errors and submit again');
    }

    if (this.data.btnStatus === AdvanceBtnStatus.NEW) {
      this.addNewAdvance();
    } else if (this.data.btnStatus === AdvanceBtnStatus.APPROVE) {
      this.approveAdvance();
    } else if (this.data.btnStatus === AdvanceBtnStatus.EDIT) {
      this.editAdvance();
    }
  }

  addNewAdvance() {
    this.isFormSubmitted = true;

    const model = {
      EmployeeId: this.data.EmployeeId,
      AdvanceDate: StaticUtilities.getLocalDate(this.newAdvanceRequestForm.value.AdvanceDate),
      ApprovedByEmployeeId: this.newAdvanceRequestForm.value.ApprovedBy,
      NumberOfInstallments: this.newAdvanceRequestForm.value.NumberOfInstallments,
      ModeOfReturn: this.newAdvanceRequestForm.value.ModeOfReturn,
      RequestAmount: this.newAdvanceRequestForm.value.RequestAmount,
      Description: this.newAdvanceRequestForm.value.Description,
    };

    this.advanceService.addAdvance(model).subscribe(x => {
      if (x && x.Success) {
        this.isFormSubmitted = false;
        this.toastr.success('Added Successfully');
        this.closeDialog();
      } else {
        this.isFormSubmitted = false;
      }
    }, error => {
      this.isFormSubmitted = false;
      this.toastr.warning(error);
    });
  }

  approveAdvance() {
    this.isFormSubmitted = true;

    const model = {
      AdvanceId: this.newAdvanceRequestForm.getRawValue().AdvanceId,
      AdvanceAmount: this.newAdvanceRequestForm.getRawValue().AdvanceAmount
    };

    this.advanceService.approveAdvance(model).subscribe(x => {
      if (x) {
        this.isFormSubmitted = false;
        this.toastr.success('Approved Successfully');
        this.closeDialog();
      } else {
        this.isFormSubmitted = false;
      }
    }, error => {
      this.isFormSubmitted = false;
      this.toastr.warning(error);
    });
  }

  editAdvance() {
    this.isFormSubmitted = true;
    const model = {
      AdvanceId: this.newAdvanceRequestForm.value.AdvanceId,
      RequestAmount: this.newAdvanceRequestForm.value.RequestAmount,
      AdvanceDate: this.newAdvanceRequestForm.value.AdvanceDate,
      ApprovedBy: this.newAdvanceRequestForm.value.ApprovedBy,
      NumberOfInstallments: this.newAdvanceRequestForm.value.NumberOfInstallments,
      ModeOfReturn: this.newAdvanceRequestForm.value.ModeOfReturn,
      Description: this.newAdvanceRequestForm.value.Description,
    };

    this.advanceService.editAdvance(model).subscribe(x => {
      if (x) {
        this.isFormSubmitted = false;
        this.toastr.success('Updated Successfully');
        this.closeDialog();
      } else {
        this.isFormSubmitted = false;
      }
    }, error => {
      this.isFormSubmitted = false;
      this.toastr.warning(error);
    });
  }

  getAdvanceDetailById() {
    this.advanceService.getAdvanceDetailById(this.data.Id).subscribe(x => {
      if (x && x.AdvanceDetail) {

        if (this.data.btnStatus === AdvanceBtnStatus.APPROVE) {
          this.newAdvanceRequestForm = this.fb.group({
            'AdvanceId': [{value: x.AdvanceDetail.AdvanceId, disabled: true}],
            'AdvanceDate':  [{value: x.AdvanceDetail.AdvanceDate, disabled: true}],
            'ApprovedBy': [{value: x.AdvanceDetail.ApprovedBy, disabled: true}],
            'NumberOfInstallments': [{value: x.AdvanceDetail.NumberOfInstallments, disabled: true}],
            'ModeOfReturn': [{value: x.AdvanceDetail.ModeOfReturn, disabled: true}],
            'RequestAmount': [{value: x.AdvanceDetail.RequestAmount, disabled: true}],
            'Description': [{value: x.AdvanceDetail.Description, disabled: true}]
          });

          const control = new FormControl();
          this.newAdvanceRequestForm.addControl('AdvanceAmount', control);
        } else if (this.data.btnStatus === AdvanceBtnStatus.EDIT) {
          this.newAdvanceRequestForm.patchValue({
            'AdvanceId': x.AdvanceDetail.AdvanceId,
            'AdvanceDate': x.AdvanceDetail.AdvanceDate,
            'ApprovedBy': x.AdvanceDetail.ApprovedBy,
            'NumberOfInstallments': x.AdvanceDetail.NumberOfInstallments,
            'ModeOfReturn': x.AdvanceDetail.ModeOfReturn,
            'RequestAmount': x.AdvanceDetail.RequestAmount,
            'Description': x.AdvanceDetail.Description
          });
        }

      } else {
        this.toastr.warning('Please try again');
      }
    }, error => {
      this.isFormSubmitted = false;
      this.toastr.warning(error);
    });
  }
}
