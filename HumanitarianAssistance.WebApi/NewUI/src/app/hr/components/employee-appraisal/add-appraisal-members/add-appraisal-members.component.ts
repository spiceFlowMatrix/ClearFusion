import { Component, OnInit, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
// import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { EmployeeAppraisalService } from 'src/app/hr/services/employee-appraisal.service';
import {
  IEmployeeDetailModel,
  IEmployeeListModel
} from 'src/app/hr/models/IGeneralProfessionalIndicatorModel';
import { MatDialogRef } from '@angular/material';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-add-appraisal-members',
  templateUrl: './add-appraisal-members.component.html',
  styleUrls: ['./add-appraisal-members.component.scss']
})
export class AddAppraisalMembersComponent implements OnInit {
  title = 'Add Appraisal Evaluation Team Members';
  err = null;

  addAppraisalMembersForm: FormGroup;
  filterdOptionsEmployeeList: Observable<IEmployeeListModel[]>;
  employeeDetailModel: IEmployeeDetailModel;
  isFormSubmitted = false;
  selectedEmployeeId: number;
  employeeList: IEmployeeListModel[] = [];
  employeeDetailList: IEmployeeDetailModel[] = [];

  employeeListEmit = new EventEmitter<any>();
  addMemberLoaderFlag = false;
  constructor(
    private fb: FormBuilder,
    private appraisalService: EmployeeAppraisalService,
    private dialogRef: MatDialogRef<any>
  ) {}

  ngOnInit() {
    this.addAppraisalMembersForm = this.fb.group({
      EmployeeId: [null]
    });

    this.initEmployeeDetailModel();
    this.filterdOptionsEmployeeList = this.addAppraisalMembersForm.controls[
      'EmployeeId'
    ].valueChanges.pipe(
      startWith<string | IEmployeeListModel>(''),
      map(value => this._filter(value.toString()))
    );
  }

  initEmployeeDetailModel() {
    this.employeeDetailModel = {
      EmployeeCode: null,
      Type: null,
      EmployeeName: null,
      EmployeeId: null
    };
  }

  private _filter(value: string): IEmployeeListModel[] {
    const filterValue = value.toLowerCase();
    if (value.length > 2) {
      this.getFilteredEmployeeList(filterValue);
      return this.employeeList.filter(
        x => x.EmployeeName.toLowerCase().indexOf(filterValue) === 0
      );
    }
  }

  getFilteredEmployeeList(data: string) {
    // this.err = null;
    this.addMemberLoaderFlag = true;
    if (data !== undefined && data != null) {
      this.employeeDetailModel.EmployeeName = data;
      this.appraisalService
        .getfilteredEmployeeList(this.employeeDetailModel)
        .subscribe(
          resp => {
            // if (resp === true) {
              this.employeeList = [];
              this.employeeDetailList = [];
              if (resp !== undefined && resp.EmployeeList.length > 0) {
                resp.EmployeeList.forEach(element => {
                  this.employeeList.push({
                    EmployeeId: element.EmployeeId,
                    EmployeeName: element.EmployeeName
                  });
                  // to get the all records if employee
                  this.employeeDetailList.push({
                    EmployeeId: element.EmployeeId,
                    EmployeeCode: element.EmployeeCode,
                    EmployeeName: element.EmployeeName,
                    Type: element.Type == null ? '' : element.Type
                  });
                });
              }
              this.addMemberLoaderFlag = false;
            // } else {
            //   // this.err = 'No record Found';
            //   this.addMemberLoaderFlag = false;
            // }
          },
          error => {
            this.err = 'No record Found';
            this.addMemberLoaderFlag = false;
          }
        );
    }
  }

  saveForm() {
    if (this.selectedEmployeeId != undefined) {
      if (this.addAppraisalMembersForm.valid) {
        this.isFormSubmitted = true;
        const filterEmlployee = this.employeeDetailList.filter(
          x => x.EmployeeId === this.selectedEmployeeId
        );
        this.employeeListEmit.emit(filterEmlployee);
        this.dialogRef.close();
      }
    }
  }

  onCancelPopup() {
    this.dialogRef.close();
  }

  onChangeEmployeeValue(event: any, id: number) {
    console.log(event.value);
    console.log(id);
    if (id !== undefined && id != null) {
      this.selectedEmployeeId = id;
    }
    // this.addAppraisalMembersForm.controls['EmployeeId'].setValue(id);
  }
}
