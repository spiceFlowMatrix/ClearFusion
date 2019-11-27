import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HrService } from 'src/app/hr/services/hr.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-profession',
  templateUrl: './add-profession.component.html',
  styleUrls: ['./add-profession.component.scss']
})
export class AddProfessionComponent implements OnInit {

  addProfessionForm: FormGroup;
  isFormSubmitted = false;
  title = 'Add Profession';

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<AddProfessionComponent>,
    private hrService: HrService, private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }


  ngOnInit() {
    this.addProfessionForm = this.fb.group({
      'ProfessionId': [null],
      'ProfessionName': [null, [Validators.required]],
    });

    if (this.data) {
      this.title = 'Edit Profession';
      this.addProfessionForm.get('ProfessionId').patchValue(this.data.ProfessionId);
      this.addProfessionForm.get('ProfessionName').patchValue(this.data.ProfessionName);
    }
  }

  addProfession() {
    this.isFormSubmitted = true;
    this.hrService.addProfession(this.addProfessionForm.value).subscribe(x => {
      if (x.StatusCode === 200) {
        this.toastr.success('Success');
        this.isFormSubmitted = false;
        this.dialogRef.close();
      } else {
        this.toastr.warning(x.Message);
        this.isFormSubmitted = false;
      }
    }, error => {
      this.toastr.warning(error);
      this.isFormSubmitted = false;
    });
}

editProfession() {
  this.isFormSubmitted = true;
  this.hrService.editProfession(this.addProfessionForm.value).subscribe(x => {
    if (x.StatusCode === 200) {
      this.toastr.success('Success');
      this.isFormSubmitted = false;
      this.dialogRef.close();
    } else {
      this.toastr.warning(x.Message);
      this.isFormSubmitted = false;
    }
  }, error => {
    this.toastr.warning(error);
    this.isFormSubmitted = false;
  });
}

saveProfession() {
  if (this.addProfessionForm.valid) {
    if (this.addProfessionForm.value.ProfessionId == null) {
      this.addProfession();
    } else {
      this.editProfession();
    }
  }
}

  onCancelPopup() {
    this.dialogRef.close();
  }

}
