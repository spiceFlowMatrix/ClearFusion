import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { ClientsService } from './service/clients.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import {
  ClientDetailsModel,
  FilterClientModel,
  CategoryModel
} from './model/client.model';
import { ClientDetailsComponent } from './client-details/client-details.component';
import { FormGroup, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { JobPaginationModel } from '../marketing-jobs/model/marketing-jobs.model';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { ContractsService } from '../contracts/service/contracts.service';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { IResponseData } from '../../accounting/vouchers/models/status-code.model';
@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss']
})
export class ClientsComponent implements OnInit {
  filters = [
    { id: '1', Value: 'Equals' },
    { id: '2', Value: 'Greater Than' },
    { id: '3', Value: 'Less Than' }
  ];

  @ViewChild(ClientDetailsComponent) child: ClientDetailsComponent;
  showClientDetail = false;
  firstClient = {};
  clientListDetails: ClientDetailsModel[];
  colsm6 = 'col-sm-10 col-sm-offset-1';
  type: any = '';
  clientId = 0;
  display = '';
  isEditingAllowed = false;
  pageId = ApplicationPages.Clients;
  filtersForm: FormGroup;
  filterModel: FilterClientModel = {};
  categories: CategoryModel[];
  paginationModel: JobPaginationModel = {};
  length: number;
  pageSize = 10;
  pageIndex = 0;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  public selectedRowID;
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  clienttListLoaderFlag = false;
  // tslint:disable-next-line:max-line-length
  constructor(
    private toastr: ToastrService,
    private appurl: AppUrlService,
    private clientsService: ClientsService,
    private contractsService: ContractsService,
    private localStorageService: LocalStorageService
  ) {
    this.getScreenSize();
  }

  initForm() {
    this.filtersForm = new FormGroup({
      clientName: new FormControl(''),
      email: new FormControl(''),
      category: new FormControl(''),
      position: new FormControl(''),
      clientId: new FormControl('')
    });
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

  pagination(event) {
    this.clienttListLoaderFlag = true;
    this.length = 0;
    this.paginationModel.pageIndex = event.pageIndex;
    this.paginationModel.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.clientListDetails = [];
    // tslint:disable-next-line:max-line-length
    this.clientsService
      .PaginatedList(
        this.paginationModel
      )
      .subscribe(
        (data:IResponseData) => {
          if (data.statusCode === 200) {
            this.clientListDetails = data.data;
            this.length = data.total;

          } else {
            this.toastr.error('Some error occured. Please try again later.');
          }
          this.clienttListLoaderFlag = false;
        },
        error => {
          this.clienttListLoaderFlag = false;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
  }

  onSubmit(form: FormGroup) {
    this.filterModel.CategoryId = this.filtersForm.value.category;
    this.filterModel.Email = this.filtersForm.value.email;
    this.filterModel.Position = this.filtersForm.value.position;
    this.filterModel.ClientName = this.filtersForm.value.clientName;
    this.filterModel.ClientId = this.filtersForm.value.clientId;
    this.clientsService
      .GetFilteredList(
        this.filterModel
      )
      .subscribe((result:IResponseData) => {
        this.closeModalDialog();
        this.clientListDetails = [];
        result.data.forEach(element => {
          this.clientListDetails.push(element);
        });
        this.firstClient = this.clientListDetails[0];
        this.child.DisplayFirstEntryOfFilteredList(this.firstClient);
      });
  }

  ngOnInit() {
    this.GetAllClients();
    this.GetCategory();
    this.initForm();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
  }

  GetAllClients() {
    this.clienttListLoaderFlag = true;
    // tslint:disable-next-line:max-line-length
    this.clientsService
      .GetClientsList()
      .subscribe(
        (result: IResponseData)  => {
          if (result.statusCode === 200) {
            this.clientListDetails = result.data;
            this.length = result.total;
          } else {
            this.toastr.error('Some error occured. Please try again later');
          }
          this.clienttListLoaderFlag = false;
        },
        error => {
          this.clienttListLoaderFlag = false;
          this.toastr.error('Some error occured. Please try again later');
        }
      );
  }

  GetCategory() {
    // tslint:disable-next-line:max-line-length
    this.clientsService
      .GetCategory()
      .subscribe((result:IResponseData) => {
        if (result.statusCode === 200) {
          this.categories = result.data;
        }
      });
  }

  onItemClick(id: number) {
    this.closeModalDialog();
    this.filtersForm.reset();
    this.clientId = id;
    if (
      this.clientId === 0 ||
      this.clientId === undefined ||
      this.clientId === null
    ) {
      this.child.ResetFormOnAddNewClient();
      this.child.CreateClientonAddNew();
    }
    this.selectedRowID = id;
    this.showProjectDetailPanel();
  }

  //#region "show/hide"
  showProjectDetailPanel() {
    this.showClientDetail = true;
    this.colsm6 = this.showClientDetail
      ? 'col-sm-6'
      : 'col-sm-10 col-sm-offset-1';
  }

  hideProjectDetailPanel() {
    this.showClientDetail = false;
    this.colsm6 = this.showClientDetail
      ? 'col-sm-6'
      : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "Emit"
  hideDetailPanel(event) {
    this.hideProjectDetailPanel();
  }
  //#endregion

  onClientDeleted(id) {
    const index = this.clientListDetails.findIndex(r => r.ClientId === id.id);
    this.clientListDetails.splice(index, 1);
    this.child.ResetFormOnAddNewClient();
    this.length = this.child.length;
    this.hideProjectDetailPanel();
  }

  DeleteClient(clientId) {
    this.clientsService
      .DeleteClient(clientId
      )
      .subscribe((result:IResponseData) => {
        if (result.statusCode === 200) {
          this.GetAllClients();
        } else {
          this.toastr.error('Some error occured. Please try again later');
        }
      },
      error => {
        this.toastr.error('Some error occured. Please try again later');
      });
  }

  addClientListById(e) {
    this.clientListDetails.unshift(e);
    this.length = e.Count;
  }

  updateClientListById(e) {
    const index = this.clientListDetails.findIndex(
      r => r.ClientId === e.ClientId
    );
    if (index !== -1) {
      this.clientListDetails[index] = e;
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
    this.filtersForm.reset();
    this.clientListDetails = [];
    this.getClientsFirstList();
  }

  getClientsFirstList() {
    // tslint:disable-next-line:max-line-length
    this.clientsService
      .GetClientsList()
      .subscribe((result : IResponseData) => {
        this.clientListDetails = result.data;
        this.firstClient = this.clientListDetails[0];
        this.child.DisplayFirstEntryOfFilteredList(this.firstClient);
      });
  }

  onChange(ev, value) {
    if (ev === 'clientId') {
      this.filterModel.ClientId = value;
    }
    if (ev === 'clientName') {
      this.filterModel.ClientName = value;
    }
    if (ev === 'category') {
      this.filterModel.CategoryId = value;
    }
    if (ev === 'position') {
      this.filterModel.Position = value;
    }
    if (ev === 'email') {
      this.filterModel.Email = value;
    }
    this.clientsService
      .GetFilteredList(
        this.filterModel
      )
      .subscribe((result: IResponseData) => {
        // this.closeModalDialog();
        this.clientListDetails = [];
        result.data.forEach(element => {
          this.clientListDetails.push(element);
        });
        this.firstClient = this.clientListDetails[0];
        this.child.DisplayFirstEntryOfFilteredList(this.firstClient);
      });
  }
}
