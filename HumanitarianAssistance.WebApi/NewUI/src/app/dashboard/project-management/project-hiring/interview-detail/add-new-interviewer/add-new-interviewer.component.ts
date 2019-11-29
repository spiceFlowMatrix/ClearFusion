import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatSelectChange } from '@angular/material';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { HiringRequestsService } from '../../../project-list/hiring-requests/hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-new-interviewer',
  templateUrl: './add-new-interviewer.component.html',
  styleUrls: ['./add-new-interviewer.component.scss']
})
export class AddNewInterviewerComponent implements OnInit {
  employeesList: any[] = [];
  employeesListTwo: any[] = [];
  constructor(
    private commonLoader: CommonLoaderService,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AddNewInterviewerComponent>
  ) {}

  ngOnInit() {
    this.getEmployeeDropDownList();
  }

  getEmployeeDropDownList() {
    this.hiringRequestService.GetAllEmployeeList().subscribe(
      (response: IResponseData) => {
        this.commonLoader.showLoader();
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.employeesList.push({
              EmployeeId: element.EmployeeId,
              EmployeeName: element.EmployeeName,
              EmployeeCode: element.EmployeeCode
            });
          });

          // this.existingCandidatesList2$.subscribe(res => {
          //   this.existingCandidatesList$ = of(
          //     res.filter(x =>
          //       data.value.includes(CandidateStatus[x.CandidateStatus])
          //     )
          //   );
          // });

        }
        this.commonLoader.hideLoader();
      },
      error => {
        this.commonLoader.hideLoader();
      }
    );
  }

  OnInterviewersSelection(data: MatSelectChange) {
    this.employeesListTwo = [];
    data.value.forEach(element => {
      this.employeesListTwo.push(
        this.employeesList.find(x => x.EmployeeId === element)
      );
    });
  }
  OnFormSubmit() {
    if (this.employeesListTwo.length === 0) {
      this.toastr.warning('Please Select Interviewers');
    } else {
      this.dialogRef.close(this.employeesListTwo);
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
