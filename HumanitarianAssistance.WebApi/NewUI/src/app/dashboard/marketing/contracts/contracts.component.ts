import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ContractsService } from '../contracts/service/contracts.service';
import { GLOBAL } from 'src/app/shared/global';
import { Router } from '@angular/router';
import { ContractDetailsComponent } from './contract-details/contract-details.component';
import { CurrencyModel, ActivityTypeModel, ContractDetailsModel } from './model/contract-details.model';
import { ClientDetailsModel, FilterModel } from '../clients/model/client.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { debug } from 'util';
import { Activities } from '../../../shared/enum';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { JobPaginationModel } from '../marketing-jobs/model/marketing-jobs.model';
import { ApplicationPages } from '../../../shared/applicationpagesenum';
import { LocalStorageService } from '../../../shared/services/localstorage.service';
@Component({
  selector: 'app-contracts',
  templateUrl: './contracts.component.html',
  styleUrls: ['./contracts.component.scss']
})

export class ContractsComponent implements OnInit {

  filters = [
    { id: '1', Value: 'Equals' },
    { id: '2', Value: 'Greater Than' },
    { id: '3', Value: 'Less Than' }
  ];

  ApprovalModel = [
    { disp: 'Yes', Value: true },
    { disp: 'No', Value: false },
  ];

  @ViewChild(ContractDetailsComponent) child: ContractDetailsComponent;
  showContractDetail = false;
  currencies: CurrencyModel[];
  activities: ActivityTypeModel[];
  clientList: ClientDetailsModel[];
  filterModel: FilterModel = {};
  paginationModel: JobPaginationModel = {};
  isEditingAllowed = false;
  pageId = ApplicationPages.Contracts;
  colsm6 = 'col-sm-10 col-sm-offset-1';
  contractId: any;
  type: any = '';
  display = '';
  public selectedRowID;
  filtersForm: FormGroup;
  firstFilterContract = {};
  length:  number;
  pageSize = 10;
  pageIndex = 0;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  contractListLoaderFlag = false;
  // tslint:disable-next-line:max-line-length
  constructor(private toastr: ToastrService, private commonLoaderService: CommonLoaderService, private contractsService: ContractsService, private appurl: AppUrlService, private router: Router,
    private localStorageService: LocalStorageService) {
    this.initForm();
    this.getScreenSize();
  }
  contractsList: ContractDetailsModel[];

  ngOnInit() {
    this.getContractsList();
    this.MasterPageValues();
    this.getClientList();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
      this.screenHeight = window.innerHeight;
      this.screenWidth = window.innerWidth;
      this.scrollStyles = {
        'overflow-y': 'auto',
        'height': this.screenHeight - 110 + 'px',
        'overflow-x': 'hidden'
        };
  }
  //#endregion

  initForm() {
    this.filtersForm = new FormGroup({
      clientName: new FormControl(''),
      unitRate: new FormControl(''),
      currency: new FormControl(''),
      activity: new FormControl(''),
      filterType: new FormControl(''),
      contractId: new FormControl(''),
      isApproved: new FormControl('')
    });
  }

  pagination(event) {
    // this.commonLoaderService.showLoader();
    this.contractListLoaderFlag = true;
    this.paginationModel.pageIndex = event.pageIndex;
    this.paginationModel.pageSize = event.pageSize;
    this.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
     this.contractsList = [];
    // tslint:disable-next-line:max-line-length
    this.contractsService.PaginatedList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetContractsPaginatedList,  this.paginationModel).subscribe(data => {
      if (data.StatusCode === 200) {
        this.contractsList = data.data.ContractDetails;
        this.length = data.data.jobListTotalCount;
        // this.commonLoaderService.hideLoader();
        this.contractListLoaderFlag = false;
      } else {
        // this.commonLoaderService.hideLoader();
        this.contractListLoaderFlag = false;
        this.toastr.error('Some error occured. Please try again later.');
      }
    },
    error => {
      // this.commonLoaderService.hideLoader();
      this.contractListLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  onSubmit(form: FormGroup) {
    this.filterModel.CurrencyId = this.filtersForm.value.currency;
    this.filterModel.ActivityTypeId = this.filtersForm.value.activity;
    this.filterModel.UnitRate = this.filtersForm.value.unitRate;
    this.filterModel.ClientName = this.filtersForm.value.clientName;
    this.filterModel.ContractId = this.filtersForm.value.contractId;
    this.filterModel.FilterType = this.filtersForm.value.filterType;
    // this.clientNameModel.find(c => c.ClientName === type).ClientId;
    // this.filterModel.ClientName = this.filtersForm.value.clientName;
    this.contractsService.GetFilteredList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetFilteredList, this.filterModel)
      .subscribe(result => {
        this.closeModalDialog();
        this.contractsList = [];
        result.data.ContractDetails.forEach(element => {
          this.contractsList.push(element);
        });
        this.firstFilterContract = this.contractsList[0];
        this.child.DisplayFirstEntryOfFilteredList(this.firstFilterContract);
      });
  }

  getContractsList() {
    // this.commonLoaderService.showLoader();
    this.contractListLoaderFlag = true;
    this.contractsList = [];
    this.contractsService.GetContractsList(this.appurl.getApiUrl() + GLOBAL.API_Job_GetContractsList).subscribe(data => {
      if (data.StatusCode === 200) {
        if (data.data.ContractDetails != null) {
          if (data.data.ContractDetails.length > 0) {
            data.data.ContractDetails.forEach(element => {
              this.contractsList.push({
                ContractId: element.ContractId,
                ContractCode: element.ContractCode,
                ClientName: element.ClientName,
                UnitRate: element.UnitRate,
                CurrencyId: element.CurrencyId,
                StartDate: element.StartDate,
                EndDate: element.EndDate,
                LanguageId: element.LanguageId,
                MediumId: element.MediumId,
                NatureId: element.NatureId,
                TimeCategoryId: element.TimeCategoryId,
                MediaCategoryId: element.MediaCategoryId,
                QualityId: element.QualityId,
                IsCompleted: element.IsCompleted,
                ClientId: element.ClientId,
                ActivityTypeId: element.ActivityTypeId,
                UnitRateId: element.UnitRateId,
                IsApproved: element.IsApproved,
                IsDeclined: element.IsDeclined,
                Activity: element.Activity,
                Type: element.Type,
                Count: element.Count
              });
            });
          }
        }
        this.length = data.data.jobListTotalCount;
        this.contractListLoaderFlag = false;
        // this.commonLoaderService.hideLoader();
      } else {
        // this.commonLoaderService.hideLoader();
        this.contractListLoaderFlag = false;
        this.toastr.error('Some error occured. Please try again later');
      }

      // data.data.ContractDetails.forEach(element => {
      //   this.contractsList.push({
      //     Activity: element.ActivityTypeId === 13 ? 'Broadcasting' : 'Production'
      //   });

      // });
    },
    error => {
      // this.commonLoaderService.hideLoader();
      this.contractListLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  getContractsFirstList() {
    this.contractsList = [];
    this.contractsService.GetContractsList(this.appurl.getApiUrl() + GLOBAL.API_Job_GetContractsList).subscribe(data => {
      this.contractsList = data.data.ContractDetails;
      this.firstFilterContract = this.contractsList[0];
      this.child.DisplayFirstEntryOfFilteredList(this.firstFilterContract);
      // data.data.ContractDetails.forEach(element => {
      //   this.contractsList.push({
      //     Activity: element.ActivityTypeId === 13 ? 'Broadcasting' : 'Production'
      //   });

      // });
    });
  }
  getClientList() {
    this.contractsService.GetClientList(this.appurl.getApiUrl() + GLOBAL.API_Client_GetAllClient).subscribe(data => {
      this.clientList = data.data.ClientDetails;
    });
  }

  MasterPageValues() {
    // tslint:disable-next-line:max-line-length
    this.contractsService.GetMasterPagesList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetMasterPagesValues).subscribe(data => {
      this.currencies = data.data.Currencies;
      this.activities = data.data.ActivityTypes;
    });
  }

  // onItemClick(data: any, type) {
  //   this.showContractDetail = this.type === type ? !this.showContractDetail : this.showContractDetail;
  //   this.colsm6 = this.showContractDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  //   this.type = type;
  //   this.child.contractDetails = {};
  //   this.child.AddNewContract();
  // }

  // GetContractDetailsById(type, contractId) {
  //   this.child.type = type;
  //   if ((this.contractId === contractId && this.type === type) || this.contractId === undefined) {

  //     this.showContractDetail = !this.showContractDetail;
  //   }

  //   this.contractId = contractId;
  //   this.child.GetContractById(this.contractId);

  //   if (this.type === type || this.type !== undefined) {
  //     this.colsm6 = this.showContractDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  //   }


  // this.contractId = contractId;
  // this.showContractDetail = !this.showContractDetail;
  // this.colsm6 = this.showContractDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  // }

  // addNewJob() {
  // }

  // GetContractList(e) {
  //   if (this.child.types === "Add") {
  //     this.contractsList.push(e);
  //   }
  //   if ( this.child.types === "Edit") {
  //     const index = this.contractsList.findIndex(r => r.ContractId === e.ContractId);
  //     this.contractsList.splice(index, 1);
  //     this.contractsList.push(e);
  //   }
  // }

  onItemClick(id: number) {
    this.closeModalDialog();
    this.filtersForm.reset();
    this.contractId = id;
    if (this.contractId === 0 || this.contractId === undefined || this.contractId === null) {
      this.child.AddNewContract();
    }
    this.selectedRowID = id;
    this.showProjectDetailPanel();
  }

  //#region "show/hide"
  showProjectDetailPanel() {
    this.showContractDetail = true;
    this.colsm6 = this.showContractDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideProjectDetailPanel() {
    this.showContractDetail = false;
    this.colsm6 = this.showContractDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "Emit"
  hideDetailPanel(e) {
    this.hideProjectDetailPanel();
  }
  //#endregion

  onContractDeleted(id) {
    const index = this.contractsList.findIndex(r => r.ContractId === id.id);
    this.contractsList.splice(index, 1);
    this.length = this.child.length;
    this.child.AddNewContract();
    this.hideProjectDetailPanel();
  }

  DeleteClient(clientId) {
    this.contractsService.DeleteContract(this.appurl.getApiUrl() + GLOBAL.API_Contract_DeleteContract, clientId).subscribe(result => {
      this.getContractsList();
    });
  }

  addContractListById(e) {
    this.contractsList.push(e);
    this.length = e.Count;
  }

  updateContractListById(e) {
    const index = this.contractsList.findIndex(r => r.ContractId === e.ContractId);
    if (index !== -1) {
      this.contractsList[index] = e;
    }
  }

  openModalFilter() {
    this.display = 'block';
  }

  closeModalDialog() {
    this.display = 'none';
  }

  RefreshFilters() {
    this.filterModel = {};
    this.firstFilterContract = {};
    this.filtersForm.reset();
    // this.contractsList = [];
    this.getContractsFirstList();
  }

  onChange(ev, value) {
    if (ev === 'contractId') {
      this.filterModel.ContractId = value;
    }
    if (ev === 'email') {
      this.filterModel.FilterType = value;
    }
    if (ev === 'clientName') {
      this.filterModel.ClientName = value;
    }
    if (ev === 'currency') {
      this.filterModel.CurrencyId = value;
    }
    if (ev === 'activity') {
      this.filterModel.ActivityTypeId = value;
    }
    if (ev === 'unitRate') {
      this.filterModel.UnitRate = value;
    }
    if (ev === 'filterType') {
      this.filterModel.FilterType = value;
    }
    if (ev === 'Approved') {
      this.filterModel.IsApproved = value;
      if (value === false) {
        this.filterModel.YesOrNo = 'No';
      }
      if (value === true) {
        this.filterModel.YesOrNo = 'Yes';
      }
    }
    this.contractsService.GetFilteredList(this.appurl.getApiUrl() + GLOBAL.API_Contract_GetFilteredList, this.filterModel)
      .subscribe(result => {
        // this.closeModalDialog();
        this.contractsList = [];
        result.data.ContractDetails.forEach(element => {
          this.contractsList.push(element);
        });
        this.firstFilterContract = this.contractsList[0];
        this.child.DisplayFirstEntryOfFilteredList(this.firstFilterContract);
      });
  }
}
