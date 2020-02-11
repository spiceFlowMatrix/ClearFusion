import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HrService } from 'src/app/hr/services/hr.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-office-master',
  templateUrl: './add-office-master.component.html',
  styleUrls: ['./add-office-master.component.scss']
})
export class AddOfficeMasterComponent implements OnInit {

  addOfficeForm: FormGroup;
  isFormSubmitted = false;
  title = 'Add Office';

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddOfficeMasterComponent>,
    private hrService: HrService, private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }


  ngOnInit() {
    this.addOfficeForm = this.fb.group({
      'OfficeId': [null],
      'OfficeName': [null, [Validators.required]],
      'OfficeCode': [null, [Validators.required]],
      'SupervisorName': [null, [Validators.required]],
      'FaxNo': [null, [Validators.required]],
      'PhoneNo': [null, [Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern('[0-9]{0,10}')]],
      'Department': this.fb.array([], Validators.required),
    });

    if (this.data) {
      this.title = 'Edit Office';
      this.addOfficeForm.get('OfficeId').patchValue(this.data.OfficeId);
      this.addOfficeForm.get('OfficeName').patchValue(this.data.OfficeName);
      this.addOfficeForm.get('OfficeCode').patchValue(this.data.OfficeCode);
      this.addOfficeForm.get('SupervisorName').patchValue(this.data.SupervisorName);
      this.addOfficeForm.get('FaxNo').patchValue(this.data.FaxNo);
      this.addOfficeForm.get('PhoneNo').patchValue(this.data.PhoneNo);

      if ( this.data.subItems === undefined && this.data.subItems.length < 0) {
        this.addDepartment();
      } else {
        this.data.subItems.forEach(x=> {
          this.editDepartmentPatchValue(x);
        });
      }
    } else {
      this.addDepartment();
    }
  }

  addDepartment() {
    (<FormArray>this.addOfficeForm.get('Department')).push(this.fb.group({
      DepartmentId: [''],
      DepartmentName: ['', Validators.required]
    }));
  }
  deleteDepartment(index: number) {
    (<FormArray>this.addOfficeForm.get('Department')).removeAt(index);
  }

  editDepartmentPatchValue(item: any) {
    (<FormArray>this.addOfficeForm.get('Department')).push(this.fb.group({
      DepartmentId: [item.DepartmentId],
      DepartmentName: [item.DepartmentName, Validators.required]
    }));
  }

  addOffice() {
    this.isFormSubmitted = true;
    this.hrService.addOffice(this.addOfficeForm.value).subscribe(x => {
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

editOffice() {
  this.isFormSubmitted = true;
  this.hrService.editOffice(this.addOfficeForm.value).subscribe(x => {
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

saveOffice() {
  if (this.addOfficeForm.valid) {
    if (this.addOfficeForm.value.OfficeId == null) {
      this.addOffice();
    } else {
      this.editOffice();
    }
  }
}

  onCancelPopup() {
    this.dialogRef.close();
  }

}
