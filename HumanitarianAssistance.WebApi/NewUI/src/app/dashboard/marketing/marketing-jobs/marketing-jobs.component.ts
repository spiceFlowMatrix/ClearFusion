import { Component, OnInit, HostListener } from '@angular/core';
import { MarketingJobsService } from '../marketing-jobs/service/marketing-jobs.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  ContractsListByClient,
  JobPaginationModel,
  MarketingJobDetailModel,
  FilterJobModel
} from './model/marketing-jobs.model';
import { ToastrService } from 'ngx-toastr';
import { JobAddComponent } from './job-add/job-add.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { IResponseData } from '../../accounting/vouchers/models/status-code.model';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-marketing-jobs',
  templateUrl: './marketing-jobs.component.html',
  styleUrls: ['./marketing-jobs.component.scss']
})
export class MarketingJobsComponent implements OnInit {
  //#region Variables

  jobsList: MarketingJobDetailModel[];
  isEditingAllowed = false;
  pageId = ApplicationPages.Jobs;
  colsm6 = 'col-sm-10 col-sm-offset-1';
  jobId = 0;
  jobDetailsForm: FormGroup;
  jobFiltersForm: any;
  filterJobModel: FilterJobModel = {};
  contractsListByClient: ContractsListByClient[];
  isApproveRejectAllowed = false;
  jobFilterLoader = false;

  // MatPaginator Inputs
  length: number;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageIndex: any;
  paginationModel: JobPaginationModel = {};

  // MatPaginator Output
  unitRate?: number;
  jobDetails: MarketingJobDetailModel = {};
  jobFilterOption: any[];
  jobListLoaderFlag = false;

  // screen resize
  scrollStyles: any;
  screenHeight: any;
  screenWidth: any;
  //#endregion

  constructor(
    private marketingJobService: MarketingJobsService,
    private toastr: ToastrService,
    public dialog: MatDialog,
    private localStorageService: LocalStorageService
  ) {
    this.initForm();
    this.getScreenSize();
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;
    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  ngOnInit() {
    this.initForms();
    // this.getJobsList();
    this.PaginatedJobList();
    this.getContracts();
    // this.getContractsByClient();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
    this.isApproveRejectAllowed = this.localStorageService.IsApproveRejectAllowed(
      this.pageId
    );
  }

  initForms() {
    this.jobFiltersForm = {
      searchValues: '',
      JobId: true,
      JobName: true,
      FinalPrice: true,
      Approved: true
    };
  }

  openAddJobDialog(): void {
    // NOTE: It passed the data into the Add Voucher Model

    const dialogRef = this.dialog.open(JobAddComponent, {
      width: '550px',
      data: {
        data: 'hello',
        contractsList: this.contractsListByClient
      }
    });

    dialogRef.componentInstance.onListRefresh.subscribe(() => {
      // this.getJobsList();
      this.PaginatedJobList();
    });

    dialogRef.afterClosed().subscribe(result => {});
  }

  calculateJobPrice(ev, value) {
    if (ev === 'minutes') {
      this.jobDetails.Minutes = value;
    }
    if (ev === 'rate') {
      this.jobDetails.FinalRate = value;
    }
    this.jobDetails.ActualPrice = this.unitRate * this.jobDetails.Minutes;
    // tslint:disable-next-line:max-line-length
    if (
      this.jobDetails.Minutes !== undefined &&
      this.jobDetails.Minutes !== null &&
      this.jobDetails.UnitRate !== 0 &&
      this.jobDetails.UnitRate !== undefined &&
      this.jobDetails.Minutes !== null
    ) {
      this.jobDetails.TotalPrice =
        this.jobDetails.Minutes * this.jobDetails.FinalRate;
      this.jobDetails.Discount =
        this.jobDetails.ActualPrice - this.jobDetails.TotalPrice;
      this.jobDetails.DiscountPercent =
        (this.jobDetails.Discount / this.jobDetails.ActualPrice) * 100;
      if (
        this.jobDetails.TotalPrice !== 0 &&
        this.jobDetails.TotalPrice !== undefined &&
        this.jobDetails.TotalPrice !== null
      ) {
        this.jobDetailsForm.controls['discount'].setValue(
          this.jobDetails.Discount
        );
        this.jobDetailsForm.controls['discountPercent'].setValue(
          this.jobDetails.DiscountPercent
        );
        this.jobDetailsForm.controls['total'].setValue(
          this.jobDetails.TotalPrice
        );
      }
    }
  }

  onContractChange(data) {
    this.unitRate = 0;
    const str = this.contractsListByClient.find(x => x.ContractId === data);
    this.unitRate = str.UnitRate;
    this.jobDetails.FinalRate = this.unitRate;
    this.jobDetails.UnitRate = this.unitRate;
    this.jobDetailsForm.controls['unitRate'].setValue(this.unitRate);
    this.jobDetailsForm.controls['rate'].setValue(this.unitRate);
  }

  onFilterSubmit(e) {
    this.jobListLoaderFlag = true;
    this.filterJobModel.Approved = this.jobFiltersForm.Approved;
    this.filterJobModel.FinalPrice = this.jobFiltersForm.FinalPrice;
    this.filterJobModel.JobId = this.jobFiltersForm.JobId;
    this.filterJobModel.JobName = this.jobFiltersForm.JobName;
    this.filterJobModel.Value = this.jobFiltersForm.searchValues;
    this.marketingJobService.GetFilteredJobList(this.filterJobModel).subscribe(
      (result: IResponseData) => {
        if (result.statusCode === 200) {
          this.jobsList = [];
          result.data.forEach(element => {
            this.jobsList.push(element);
          });
          this.length = result.total;
        } else {
          this.toastr.error('Some error occured. Please try again later');
        }
        this.jobListLoaderFlag = false;
      },
      error => {
        this.jobListLoaderFlag = false;
        this.toastr.error('Some error occured. Please try again later');
      }
    );
  }

  ResetJobFilters() {
    this.filterJobModel = {};
    this.initForms();
    // this.getJobsList();
    this.PaginatedJobList();
  }

  DeleteJob(id) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: 'delete',
      disableClose: false
    });

    dialogRef.componentInstance.confirmMessage =
      Delete_Confirmation_Texts.deleteText1;

    dialogRef.componentInstance.confirmText = Delete_Confirmation_Texts.yesText;

    dialogRef.componentInstance.cancelText = Delete_Confirmation_Texts.noText;

    dialogRef.afterClosed().subscribe(result => {});

    dialogRef.componentInstance.confirmDelete.subscribe(res => {
      const index = this.jobsList.findIndex(x => x.JobId === id);

      dialogRef.componentInstance.isLoading = true;
      this.marketingJobService.DeleteJob(id).subscribe(
        (data: IResponseData) => {
          if (data.statusCode === 200) {
            this.toastr.success(data.message);
            this.jobsList.splice(index, 1);
            this.length = data.total;
            dialogRef.componentInstance.onCancelPopup();
          } else {
            this.toastr.error('Some error occured. Please try again later.');
          }
          dialogRef.componentInstance.isLoading = false;
        },
        error => {
          dialogRef.componentInstance.isLoading = false;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
    });
  }

  ApproveJob(id) {
    const index = this.jobsList.findIndex(x => x.JobId === id);
    this.jobsList[index]._IsLoading = true;
    this.marketingJobService.ApproveJob(id).subscribe(
      (data: IResponseData) => {
        if (data.statusCode === 200) {
          // this.getJobsList();
          this.PaginatedJobList();
          this.toastr.success(data.message);
        } else {
          this.toastr.error('Some error occured. Please try again later.');
        }
        this.jobsList[index]._IsLoading = false;
      },
      error => {
        this.jobsList[index]._IsLoading = false;
        this.toastr.error('Some error occured. Please try again later');
      }
    );
  }

  // getContractsByClient() {
  //   // tslint:disable-next-line:max-line-length
  //   this.marketingJobService
  //     .GetContractListByClient()
  //     .subscribe((data: IResponseData) => {
  //       if (data.statusCode === 200) {
  //         this.contractsListByClient = data.data;
  //       }
  //     });
  // }

  initForm() {
    this.jobDetailsForm = new FormGroup({
      jobname: new FormControl('', [Validators.required]),
      endDate: new FormControl('', [Validators.required]),
      contractname: new FormControl('', [Validators.required]),
      minutes: new FormControl('', [Validators.required]),
      rate: new FormControl('', [Validators.required]),
      total: new FormControl('', [Validators.required]),
      unitRate: new FormControl('', [Validators.required]),
      discount: new FormControl('', [Validators.required]),
      discountPercent: new FormControl('', [Validators.required])
    });
    this.jobFiltersForm = new FormGroup({
      searchValue: new FormControl(''),
      jobFilterName: new FormControl('')
    });
  }

  setPageSizeOptions(setPageSizeOptionsInput: string) {
    this.pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
  }

  PaginatedJobList() {
    this.jobListLoaderFlag = true;
    this.paginationModel.pageIndex = this.pageIndex;
    this.paginationModel.pageSize =  this.pageSize;
    this.marketingJobService.PaginatedJobList(this.paginationModel).subscribe(
      (data: IResponseData) => {
        if (data.statusCode === 200) {
          this.jobsList = data.data;
          this.length = data.total;
        } else {
          this.toastr.error('Some error occured. Please try again later.');
        }
        this.jobListLoaderFlag = false;
      },
      error => {
        this.jobListLoaderFlag = false;
        this.toastr.error('Some error occured. Please try again later');
      }
    );
  }

  Pagination(event) {
    this.jobListLoaderFlag = true;
    this.length = 0;
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.PaginatedJobList();
    // this.jobsList = [];
    // tslint:disable-next-line:max-line-length
    // this.marketingJobService
    // .PaginatedJobList(
    //   this.paginationModel
    // )
    // .subscribe(
    //   (data: IResponseData) => {
    //     if (data.statusCode === 200) {
    //       this.jobsList = data.data;
    //       this.length = data.total;
    //     } else {
    //       this.toastr.error('Some error occured. Please try again later.');
    //     }
    //     this.jobListLoaderFlag = false;
    //   },
    //   error => {
    //     this.jobListLoaderFlag = false;
    //     this.toastr.error('Some error occured. Please try again later');
    //   }
    // );
  }

  // getJobsList() {
  //   this.jobListLoaderFlag = true;
  //   this.jobsList = [];
  //   this.marketingJobService
  //     .GetJobList()
  //     .subscribe(
  //       (data: IResponseData) => {
  //         if (data.statusCode === 200) {
  //           if (data.data != null) {
  //             if (data.data.length > 0) {
  //               data.data.forEach(element => {
  //                 this.jobsList.push({
  //                   JobId: element.JobId,
  //                   JobName: element.JobName,
  //                   JobCode: element.JobCode,
  //                   UnitRate: element.UnitRate,
  //                   FinalRate: element.FinalRate,
  //                   FinalPrice: element.FinalPrice,
  //                   TotalPrice: element.TotalPrice,
  //                   ActualPrice: element.ActualPrice,
  //                   Discount: element.Discount,
  //                   DiscountPercent: element.DiscountPercent,
  //                   EndDate: element.EndDate,
  //                   ContractId: element.ContractId,
  //                   Minutes: element.Minutes,
  //                   IsApproved: element.IsApproved,
  //                   count: element.count,
  //                   CreatedBy: element.CreatedBy,
  //                   IsAgreementApproved: element.IsAgreementApproved,
  //                   ClientId: element.ClientId,
  //                   CurrencyCode: element.CurrencyCode,
  //                   _IsLoading: false
  //                 });
  //               });
  //             }
  //           }
  //           this.length = data.total;
  //         } else {
  //           this.toastr.error('Some error occured. Please try again later');
  //         }
  //         this.jobListLoaderFlag = false;
  //       },
  //       error => {
  //         this.jobListLoaderFlag = false;
  //         this.toastr.error('Some error occured. Please try again later');
  //       }
  //     );
  // }

   getContracts() {
    // tslint:disable-next-line:max-line-length
    this.marketingJobService
      .GetContractListByClient()
      .subscribe((data: IResponseData) => {
        if (data.statusCode === 200) {
          this.contractsListByClient = data.data;
        }
      });
  }

  handleKeyboardEvent(event: KeyboardEvent) {
    const key = event.key;
    if (key === 'Enter') {
      this.onFilterSubmit(event);
    }
  }
}
