import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { EmployeeListService } from '../../services/employee-list.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-termination',
  templateUrl: './employee-termination.component.html',
  styleUrls: ['./employee-termination.component.scss']
})
export class EmployeeTerminationComponent implements OnInit {
  terminationForm: FormGroup;
  err = null;

  constructor(private fb: FormBuilder,
    public dialogRef: MatDialogRef<EmployeeTerminationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private employeeListService: EmployeeListService,
    private commonLoader: CommonLoaderService,
    private toastr: ToastrService,
    private router: Router) {
    this.terminationForm = this.fb.group({
      TerminationDate: [null, Validators.required],
      ReasonOfTermination: [null, Validators.required]
    });
   }

  ngOnInit() {
  }

  onCancelPopup() {
    this.dialogRef.close();
  }

  formSubmit(value) {
    this.err = null;
    if (!this.terminationForm.valid) {
      return;
    }
    const model = {
      EmployeeId: this.data.EmployeeId,
      TerminationDate: StaticUtilities.getLocalDate(value.TerminationDate),
      ReasonOfTermination: value.ReasonOfTermination
    };
    this.commonLoader.showLoader();
    this.employeeListService.terminateEmployeeByEmployeeeId(model).subscribe(res => {
      if (res) {
        this.commonLoader.hideLoader();
        this.dialogRef.close();
        this.toastr.success('Employee Terminated Successfully!');
        this.router.navigate(['/hr/employees']);
      } else {
        this.err = 'Something went wrong!';
        this.commonLoader.hideLoader();
      }
    }, err => {
      this.err = err;
      this.commonLoader.hideLoader();
    });
  }

}
