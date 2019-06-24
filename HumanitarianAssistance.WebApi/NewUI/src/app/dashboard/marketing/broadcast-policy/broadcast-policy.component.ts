import { Component, OnInit, HostListener } from '@angular/core';
import { PolicyAddComponent } from './policy-add/policy-add.component';
import { MatDialog } from '@angular/material';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { BroadcastPolicyService } from './service/broadcast-policy.service';
import {
  PolicyModel,
  FilterPolicyModel,
  PolicyPaginationModel
} from './model/policy-model';
import { ToastrService } from 'ngx-toastr';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';

@Component({
  selector: 'app-broadcast-policy',
  templateUrl: './broadcast-policy.component.html',
  styleUrls: ['./broadcast-policy.component.scss']
})
export class BroadcastPolicyComponent implements OnInit {
  colsm6 = 'col-sm-10 col-sm-offset-1';
  scrollStyles: any;
  screenHeight: any;
  screenWidth: any;
  policyFiltersForm: any;
  policyList: PolicyModel[];
  filterPolicyModel: FilterPolicyModel = {};
  policyListLoaderFlag = false;
  length: number;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageIndex: any;
  paginationModel: PolicyPaginationModel = {};
  policyId: any;
  public selectedRowID;
  showPolicyDetail = false;
  isEditingAllowed = false;
  pageId = ApplicationPages.Policy;
  // tslint:disable-next-line:max-line-length
  constructor(
    private localStorageService: LocalStorageService,
    private toastr: ToastrService,
    private policyService: BroadcastPolicyService,
    public dialog: MatDialog,
    private appurl: AppUrlService
  ) {
    this.getScreenSize();
  }

  ngOnInit() {
    this.getPolicyList();
    this.initForms();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
  }

  initForms() {
    this.policyFiltersForm = {
      searchValues: '',
      PolicyId: true,
      PolicyName: true,
      Medium: true
    };
  }

  onFilterSubmit(e) {
    this.policyListLoaderFlag = true;
    this.filterPolicyModel.Medium = this.policyFiltersForm.Medium;
    this.filterPolicyModel.PolicyId = this.policyFiltersForm.PolicyId;
    this.filterPolicyModel.PolicyName = this.policyFiltersForm.PolicyName;
    this.filterPolicyModel.Value = this.policyFiltersForm.searchValues;
    this.policyService
      .GetFilteredPolicyList(
        this.appurl.getApiUrl() + GLOBAL.API_Policy_GetFilteredPolicyList,
        this.filterPolicyModel
      )
      .subscribe(
        result => {
          if (result.StatusCode === 200) {
            this.policyList = [];
            result.data.PolicyFilteredList.forEach(element => {
              this.policyList.push(element);
            });
          } else {
            this.toastr.error('Some error occured. Please try again later');
          }
          this.policyListLoaderFlag = false;
        },
        error => {
          this.policyListLoaderFlag = false;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
  }

  ResetPolicyFilters() {
    this.filterPolicyModel = {};
    this.initForms();
    this.getPolicyList();
  }

  getPolicyList() {
    this.policyListLoaderFlag = true;
    this.policyService
      .GetPolicyList(this.appurl.getApiUrl() + GLOBAL.API_Policy_GetPolicyList)
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.policyListLoaderFlag = false;
            this.policyList = data.data.policyList;
            this.length = data.data.TotalCount;
          } else {
            this.policyListLoaderFlag = false;
            this.toastr.error('Some error occured. Please try again later');
          }
        },
        error => {
          this.policyListLoaderFlag = false;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
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

  openAddPolicyDialog(): void {
    const dialogRef = this.dialog.open(PolicyAddComponent, {
      width: '550px',
      data: {
        data: 'hello'
      }
    });

    dialogRef.componentInstance.onListRefresh.subscribe(data => {
      this.length = data.TotalCount;
      this.getPolicyList();
    });

    dialogRef.afterClosed().subscribe(result => {});
  }

  DeletePolicy(id) {
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
      dialogRef.componentInstance.isLoading = true;
      // tslint:disable-next-line:max-line-length
      this.policyService
        .DeletePolicy(
          this.appurl.getApiUrl() + GLOBAL.API_Policy_DeletePolicy,
          id
        )
        .subscribe(
          result => {
            if (result.StatusCode === 200) {
              this.toastr.success(result.Message);
              dialogRef.componentInstance.onCancelPopup();
              this.length = result.data.TotalCount;
              this.getPolicyList();
            } else {
              this.toastr.error('Some error occured. Please try again later');
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

  updatePolicy(e) {
    const index = this.policyList.findIndex(r => r.PolicyId === e.PolicyId);
    if (index !== -1) {
      this.policyList[index] = e;
    }
  }

  pagination(event) {
    this.policyListLoaderFlag = true;
    this.length = 0;
    // this.commonLoaderService.showLoader();
    this.paginationModel.pageIndex = event.pageIndex;
    this.paginationModel.pageSize = event.pageSize;
    this.pageIndex = this.paginationModel.pageIndex;
    this.pageSize = this.paginationModel.pageSize;
    this.policyList = [];
    // tslint:disable-next-line:max-line-length
    this.policyService
      .PolicyPaginatedList(
        this.appurl.getApiUrl() + GLOBAL.API_Policy_GetPolicyPaginatedList,
        this.paginationModel
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.policyList = data.data.policyFilterList;
            this.length = data.data.TotalCount;
          } else {
            this.toastr.error('Some error occured. Please try again later.');
          }
          this.policyListLoaderFlag = false;
        },
        error => {
          this.policyListLoaderFlag = false;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
  }

  onItemClick(id) {
    // this.policyFiltersForm.reset();
    this.policyId = id;
    if (
      this.policyId === 0 ||
      this.policyId === undefined ||
      this.policyId === null
    ) {
      // this.child.AddNewContract();
    }
    this.selectedRowID = id;
    this.showProjectDetailPanel();
  }

  showProjectDetailPanel() {
    this.showPolicyDetail = true;
    this.colsm6 = this.showPolicyDetail
      ? 'col-sm-6'
      : 'col-sm-10 col-sm-offset-1';
  }
}
