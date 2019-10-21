import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observable, forkJoin, ReplaySubject, of } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { takeUntil } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';

@Component({
  selector: 'app-add-hiring-request',
  templateUrl: './add-hiring-request.component.html',
  styleUrls: ['./add-hiring-request.component.scss']
})
export class AddHiringRequestComponent implements OnInit {
  addHiringRequestForm: FormGroup;
  professionList$: Observable<IDropDownModel[]>;
  officeList$: Observable<IDropDownModel[]>;
  genderList$: Observable<IDropDownModel[]>;
  jobShiftList$: Observable<IDropDownModel[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    public dialogRef: MatDialogRef<AddHiringRequestComponent>,
    private commonLoader: CommonLoaderService,
    private hiringRequestService: HiringRequestsService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {
    this.addHiringRequestForm = this.fb.group({
      JobCategory: [null, [Validators.required]],
      MinEducationLevel: [null, [Validators.required]],
      TotalVacancy: [null, [Validators.required]],
      Position: [null, [Validators.required]],
      Organization: [null, [Validators.required]],
      Office: [null, [Validators.required]],
      ContractType: [null, [Validators.required]],
      ContractDuration: [null, [Validators.required]],
      Gender: [null, [Validators.required]],
      SalaryRange: [null, [Validators.required]],
      AnouncingDate: [null, [Validators.required]],
      ClosingDate: [null, [Validators.required]],
      FilledVacancy: [null, [Validators.required]],
      JobType: [null, [Validators.required]],
      JobShift: [null, [Validators.required]],
      JobStatus: [null, [Validators.required]],
      Experience: [null, [Validators.required]],
      Background: [null, [Validators.required]],
      SpecificDutiesAndResponsibilities: [null, [Validators.required]],
      KnowledgeAndSkillsRequired: [null, [Validators.required]],
      SubmissionGuidelines: [null, [Validators.required]]
    });
    this.genderList$ = of([
      { name: 'Male', value: 1 },
      { name: 'Female', value: 2 },
      { name: 'Other', value: 3 }
    ] as IDropDownModel[]);

    this.jobShiftList$ = of([
      { name: 'Day', value: 1 },
      { name: 'Night', value: 2 }
    ] as IDropDownModel[]);
  }

  ngOnInit() {
    forkJoin([this.getAllProfessionList(), this.getAllOfficeList()])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeProfessionList(result[0]);
        this.subscribeOfficeList(result[1]);
      });
  }

  getAllProfessionList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetProfessionList();
  }
  getAllOfficeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetOfficeList();
  }
  subscribeProfessionList(response: any) {
    this.commonLoader.hideLoader();
    this.professionList$ = of(
      response.data.map(y => {
        return {
          value: y.ProfessionId,
          name: y.ProfessionName
        };
      })
    );
  }
  subscribeOfficeList(response: any) {
    this.commonLoader.hideLoader();
    this.officeList$ = of(
      response.data.map(y => {
        return {
          value: y.OfficeId,
          name: y.OfficeName
        };
      })
    );
  }
  onFormSubmit(data: any) {
    if (this.addHiringRequestForm.valid) {
      console.log(data);
    }
  }
  //#region "onCancelPopup"
  onCancelPopup(): void {
    this.dialogRef.close();
  }
  //#endregion
}
