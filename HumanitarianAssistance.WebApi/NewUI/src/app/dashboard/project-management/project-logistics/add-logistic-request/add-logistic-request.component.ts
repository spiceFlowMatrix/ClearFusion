import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { LogisticService } from '../logistic.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-logistic-request',
  templateUrl: './add-logistic-request.component.html',
  styleUrls: ['./add-logistic-request.component.scss']
})
export class AddLogisticRequestComponent implements OnInit {

  addLogisticRequestForm: FormGroup;
  model: AddLogisticRequestModel = {ProjectId: '', TotalCost: '', RequestName: ''};
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddLogisticRequestComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private logisticservice: LogisticService,
    private commonLoader: CommonLoaderService,
    public toastr: ToastrService,
    ) { }

  ngOnInit() {
    this.addLogisticRequestForm = this.fb.group({
      Name: ['', Validators.required]
    });
  }

  addRequest(value) {
    this.commonLoader.showLoader();
    this.model.ProjectId = this.data.ProjectId;
    this.model.RequestName = value.Name;
    // this.model.TotalCost = value.TotalCost;
    this.logisticservice.addLogisticRequest(this.model).subscribe(res => {
      if (res.StatusCode === 200) {
        this.dialogRef.close({data: {RequestId: res.data.logisticRequestId, Status: 1, TotalCost: this.model.TotalCost,
          RequestName: this.model.RequestName}});
        this.commonLoader.hideLoader();
        this.toastr.success('Request added successfully!');
      } else {
        this.dialogRef.close({data: null});
        this.commonLoader.hideLoader();
        this.toastr.error('Something went wrong!');
      }
    });
  }

  cancelRequest() {
    this.dialogRef.close({data: null});
  }
}

export class AddLogisticRequestModel {
  ProjectId;
  TotalCost;
  RequestName;
}
