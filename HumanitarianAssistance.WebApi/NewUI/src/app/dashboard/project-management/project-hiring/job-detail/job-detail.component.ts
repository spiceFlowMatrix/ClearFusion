import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { forkJoin, ReplaySubject, Observable, of } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-job-detail',
  templateUrl: './job-detail.component.html',
  styleUrls: ['./job-detail.component.scss']
})
export class JobDetailComponent implements OnInit {
  addJobForm: FormGroup;
  projectId: number;
  isAddJobFormSubmitted = false;
  officeList$: Observable<IDropDownModel[]>;
  gradeList$: Observable<IDropDownModel[]>;
  payCurrencyList$: Observable<IDropDownModel[]>;
  ProfessionList$: Observable<IDropDownModel[]>;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    private fb: FormBuilder,
    private routeActive: ActivatedRoute,
    private commonLoader: CommonLoaderService,
    private toastr: ToastrService,
    private router: Router,
    private hiringRequestService: HiringRequestsService
  ) {
    this.addJobForm = this.fb.group({
      description: ['', [Validators.required]],
      office: ['', [Validators.required]],
      position: ['', [Validators.required]],
      jobGrade: ['', [Validators.required]],
      totalVacancies: ['', [Validators.required]],
      payCurrency: ['', [Validators.required]],
      payRate: ['', [Validators.required]],
      projectId: ['']
    });
  }

  ngOnInit() {
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    forkJoin([
      this.getAllOfficeList(),
      this.getAllJobGradeList(),
      this.getAllPayCurrencyList(),
      this.getAllProfessionList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeOfficeList(result[0]);
        this.subscribeGradeList(result[1]);
        this.subscribePayCurrencyList(result[2]);
        this.subscribeProfessionList(result[3]);
      });
  }

  getAllOfficeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetOfficeList();
  }
  getAllJobGradeList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetJobGradeList();
  }
  getAllPayCurrencyList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetCurrencyList();
  }
  getAllProfessionList() {
    this.commonLoader.showLoader();
    return this.hiringRequestService.GetProfessionList();
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
  subscribeGradeList(response: any) {
    this.commonLoader.hideLoader();
    this.gradeList$ = of(
      response.data.map(y => {
        return {
          value: y.GradeId,
          name: y.GradeName
        };
      })
    );
  }
  subscribePayCurrencyList(response: any) {
    this.commonLoader.hideLoader();
    this.payCurrencyList$ = of(
      response.data.map(y => {
        return {
          value: y.CurrencyId,
          name: y.CurrencyName
        };
      })
    );
  }
  subscribeProfessionList(response: any) {
    this.commonLoader.hideLoader();
    this.ProfessionList$ = of(
      response.data.map(y => {
        return {
          value: y.ProfessionId,
          name: y.ProfessionName
        };
      })
    );
  }

  addJobFormSubmit() {
    if (this.addJobForm.valid) {
      this.addJobForm.controls['projectId'].setValue(this.projectId);
      this.isAddJobFormSubmitted = true;
      this.hiringRequestService.addJobHiringDetails(this.addJobForm.value)
      .pipe(takeUntil(this.destroyed$))
      .subscribe();
      this.isAddJobFormSubmitted = false;
      this.toastr.success('Job Sucessfully Inserted');
    } else {
      this.toastr.warning('Please correct errors in job form and submit again');
    }
  }
  cancelButtonClicked() {
    this.router.navigate(['../hiring-request']);
  }
}
