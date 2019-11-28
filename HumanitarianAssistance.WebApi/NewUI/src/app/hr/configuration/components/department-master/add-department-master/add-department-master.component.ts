import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { HrService } from 'src/app/hr/services/hr.service';
import { PurchaseService } from 'src/app/store/services/purchase.service';
import { of } from 'rxjs/internal/observable/of';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-add-department-master',
  templateUrl: './add-department-master.component.html',
  styleUrls: ['./add-department-master.component.scss']
})
export class AddDepartmentMasterComponent implements OnInit {

  addDepartmentForm: FormGroup;
  isFormSubmitted = false;
  title = 'Add Department';
  offices$: Observable<IDropDownModel[]>;

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddDepartmentMasterComponent>,
    private hrService: HrService, private toastr: ToastrService, private purchaseService: PurchaseService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.addDepartmentForm = this.fb.group({
      'DepartmentId': [null],
      'DepartmentName': [null, [Validators.required]],
      'OfficeId': [null, [Validators.required]],
    });

    if (this.data) {
      this.title = 'Edit Department';
      this.addDepartmentForm.get('DepartmentId').patchValue(this.data.DepartmentId);
      this.addDepartmentForm.get('DepartmentName').patchValue(this.data.DepartmentName);
      this.addDepartmentForm.get('OfficeId').patchValue(this.data.OfficeId);
    }
    this.getAllOffice();
  }

  addDepartment() {
    this.isFormSubmitted = true;
    this.hrService.addDepartment(this.addDepartmentForm.value).subscribe(x => {
      if (x.StatusCode === 200) {
        this.toastr.success('Success');
        this.isFormSubmitted = false;
        this.dialogRef.close();
      } else {
        this.toastr.warning('Something went wrong');
        this.isFormSubmitted = false;
      }
    }, error => {
      this.toastr.warning(error);
      this.isFormSubmitted = false;
    });
}

editDepartment() {
  this.isFormSubmitted = true;
  this.hrService.editDepartment(this.addDepartmentForm.value).subscribe(x => {
    if (x) {
      this.toastr.success('Success');
      this.isFormSubmitted = false;
      this.dialogRef.close();
    } else {
      this.toastr.warning('Something went wrong');
      this.isFormSubmitted = false;
    }
  }, error => {
    this.toastr.warning(error);
    this.isFormSubmitted = false;
  });
}

saveDepartment() {
  if (this.addDepartmentForm.valid) {
    if (this.addDepartmentForm.value.DepartmentId == null) {
      this.addDepartment();
    } else {
      this.editDepartment();
    }
  }
}

getAllOffice() {
   this.purchaseService.getAllOfficeList().subscribe(x=> {
    this.offices$ = of(x.data.OfficeDetailsList.map(y => {
      return {
        value: y.OfficeId,
        name: y.OfficeCode + '-' + y.OfficeName
      };
    }));
   });
}

  onCancelPopup() {
    this.dialogRef.close();
  }

}
