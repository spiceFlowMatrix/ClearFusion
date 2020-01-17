import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { LogisticService } from '../logistic.service';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { of, Observable } from 'rxjs';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { TableActionsModel } from 'projects/library/src/public_api';
import { ActivatedRoute, Router } from '@angular/router';
import { AddLogisticItemsComponent } from '../add-logistic-items/add-logistic-items.component';
import { map } from 'rxjs/operators';
import { LogisticRequestStatus, LogisticComparativeStatus, LogisticTenderStatus } from 'src/app/shared/enum';
import { RequestDetail } from '../logistic-request-details/logistic-request-details.component';

@Component({
  selector: 'app-add-logistic-request',
  templateUrl: './add-logistic-request.component.html',
  styleUrls: ['./add-logistic-request.component.scss']
})
export class AddLogisticRequestComponent implements OnInit {

  addLogisticRequestForm: FormGroup;
  model: AddLogisticRequestModel = {RequestName: '', CurrencyId: '' , BudgetLineId: '', OfficeId: ''};
  selectedCurrency;
  requestedItemsHeaders$ = of([
    'Id',
    'ItemId',
    'Item Code',
    'Item Name',
    'Estimated Unit Cost',
    'Requested Units'
  ]);
  actions: TableActionsModel;
  hideItemColums;
  officeDropdown: any[];
  currencyDropdown: any[];
  currencyId$: Observable<IDropDownModel[]>;
  budgetLineId$: Observable<IDropDownModel[]>;
  officeId$: Observable<IDropDownModel[]>;
  projectId;
  storeItemsList: any[] = [];
  storedropdownItemsList: any[];
  requestedItems: any[] = [];
  requestedItems$;
  totalcost = 0;
  buttonText = 'SAVE REQUEST AND ISSUE DIRECT PURCHASE ORDER';
  isRequestFormSubmitted = false;
  requestId = 0;
  requestDetail;
  requestItemList = [];

  constructor(
    private fb: FormBuilder,
    private logisticservice: LogisticService,
    private commonLoader: CommonLoaderService,
    public toastr: ToastrService,
    private routeActive: ActivatedRoute,
    private dialog: MatDialog,
    private router: Router
    ) {
      this.routeActive.queryParams.subscribe(params => {
        this.requestId = +params['requestId'];
    });
    }

  ngOnInit() {
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.getStoreItems();
    this.getCurrencyCodeList();
    this.getBudgetLineList();
    this.getOfficeCodeList();
    if (this.requestId && this.requestId !== 0) {
      this.getRequestDetails();
    }
    this.addLogisticRequestForm = this.fb.group({
      Description: ['', Validators.required],
      CurrencyId : ['', Validators.required],
      OfficeId : ['', Validators.required],
      BudgetLineId : ['', Validators.required]
    });
    this.hideItemColums = of({
      headers: ['Item Code', 'Item Name', 'Estimated Unit Cost', 'Requested Units'],
      items: ['ItemCode', 'ItemName', 'EstimatedUnitCost', 'RequestedUnits']
    });
    this.actions = {
      items: {
        button: { status: false, text: '' },
        edit: false,
        delete: true,
        download: false,
      },
      subitems: {
      }

    };
  }

  onActionClick(event) {
    if (event.type === 'delete') {
      const index = this.requestedItems.findIndex(v => v.ItemId === event.item.ItemId);
      if (index !== -1) {
        this.requestedItems.splice(index, 1);
      }
      this.requestedItems$ = of(this.requestedItems).pipe(
        map(r => r.map(v => ({
          Id: 0,
          ItemId: v.ItemId,
          ItemCode: this.storeItemsList.filter(f => f.ItemId === v.ItemId)[0].ItemCode,
          ItemName: this.storeItemsList.filter(f => f.ItemId === v.ItemId)[0].ItemName,
          EstimatedUnitCost: v.EstimatedUnitCost,
          RequestedUnits: v.RequestedUnits
      }) )));
      this.getTotalCost();
    }
    // if (event.type === 'edit') {
    //   const dialogRef = this.dialog.open(AddLogisticItemsComponent, {
    //     width: '400px',
    //     data: {Id: event.item.Id, ItemId: event.item.ItemId, Quantity: event.item.Quantity,
    //       EstimatedCost: event.item.EstimatedCost, RequestId: this.requestId}
    //   });

    //   dialogRef.afterClosed().subscribe(result => {
    //     if (result !== undefined && result.data != null ) {
    //       this.getAllRequestItems();
    //     }
    //   });
    // }
  }

  goBack() {
    this.router.navigate(['../../logistic-requests'], { relativeTo: this.routeActive });
  }

  getStoreItems() {
    this.logisticservice.getAllStoreItems().subscribe(res => {
      this.storeItemsList = [];
      this.storedropdownItemsList = [];
      if (res.StatusCode === 200 && res.data.InventoryItemList != null) {
        res.data.InventoryItemList.forEach(element => {
          this.storeItemsList.push(element);
          this.storedropdownItemsList.push({
            Id: element.ItemId,
            Name: element.ItemCode + '-' + element.ItemName
          });
        });
        // this.addLogisticItemsForm.controls['Item'].setValue(this.data.ItemId);
      }
    });
  }

  getCurrencyCodeList() {
    this.logisticservice
      .GetAllCurrencyCodeList()
      .subscribe(
        data => {
          this.currencyDropdown = [];
          if (data.data.CurrencyList != null) {
            data.data.CurrencyList.forEach(element => {
              this.currencyDropdown.push(element);
            });
            // this.addLogisticRequestForm.controls['CurrencyId'].disable();
            this.currencyId$ = of(this.currencyDropdown.map(y => {
              return {
                value: y.CurrencyId,
                name: y.CurrencyCode + '-' + y.CurrencyName
              };
            }));
            this.selectedCurrency = this.currencyDropdown.filter(x => x.CurrencyCode === 'USD')[0].CurrencyId;
            this.addLogisticRequestForm.controls['CurrencyId'].setValue(this.selectedCurrency);
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  getOfficeCodeList() {
    this.logisticservice
      .GetAllOfficeCodeList()
      .subscribe(
        data => {
          if (data.data.OfficeDetailsList != null) {
            this.officeDropdown = [];
            data.data.OfficeDetailsList.forEach(element => {
              this.officeDropdown.push({
                Id: element.OfficeId,
                Name: element.OfficeName
              });
            });
            this.officeId$ = of(this.officeDropdown.map(y => {
              return {
                value: y.Id,
                name: y.Name
              };
            }));
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  getBudgetLineList() {
    this.logisticservice
      .GetBudgetLineListByProjectId(this.projectId)
      .subscribe(
        data => {
          this.currencyDropdown = [];
          if (data.data.ProjectBudgetLineDetailList != null) {
            this.budgetLineId$ = of(data.data.ProjectBudgetLineDetailList.map(y => {
              return {
                value: y.BudgetLineId,
                name: y.BudgetName
              };
            }));
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  addNewItem() {
    const dialogRef = this.dialog.open(AddLogisticItemsComponent, {
      width: '450px',
      height: '430px',
      data: {'Storeitems': this.storedropdownItemsList}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        const dialogdata = result.data;
        if (this.requestedItems.findIndex(x => x.ItemId === dialogdata.ItemId) === -1) {
          this.requestedItems.push(dialogdata);
          console.log(this.requestedItems);
          this.requestedItems$ = of(this.requestedItems).pipe(
            map(r => r.map(v => ({
              Id: 0,
              ItemId: v.ItemId,
              ItemCode: this.storeItemsList.filter(f => f.ItemId === v.ItemId)[0].ItemCode,
              ItemName: this.storeItemsList.filter(f => f.ItemId === v.ItemId)[0].ItemName,
              EstimatedUnitCost: v.EstimatedUnitCost,
              RequestedUnits: v.RequestedUnits
            }) )));
            this.getTotalCost();
        } else {
          this.toastr.warning('Item already exists!');
        }

      }
    });
  }
  addRequest(value) {
    if (!this.addLogisticRequestForm.valid) {
      this.toastr.warning('Please fill all required fields!');
      return false;
    }
    this.commonLoader.showLoader();
    this.model = {
      // ProjectId: this.data.ProjectId,
      RequestName: value.Name,
      CurrencyId: value.CurrencyId,
      OfficeId: value.OfficeId,
      BudgetLineId: value.BudgetLineId
    };
    // this.model.TotalCost = value.TotalCost;
    this.logisticservice.addLogisticRequest(this.model).subscribe(res => {
      if (res.StatusCode === 200) {
        // this.dialogRef.close({data: {RequestId: res.data.logisticRequestId, Status: 1, TotalCost: 0,
          // RequestName: this.model.RequestName}});
        this.commonLoader.hideLoader();
        this.toastr.success('Request added successfully!');
      } else {
        // this.dialogRef.close({data: null});
        this.commonLoader.hideLoader();
        this.toastr.error('Something went wrong!');
      }
    });
  }

  getTotalCost() {
    this.totalcost = 0;
    this.requestedItems.forEach(e => {
      this.totalcost += (e.EstimatedUnitCost * e.RequestedUnits);
    });
    this.changeButtonText();
  }

  changeButtonText() {
    if (this.totalcost < 200) {
      this.buttonText = 'SAVE REQUEST AND ISSUE DIRECT PURCHASE ORDER';
    } else if ((this.totalcost >= 200) && (this.totalcost < 10000)) {
      this.buttonText = 'SAVE REQUEST AND ISSUE COMPARATIVE STATEMENT';
    } else if ((this.totalcost >= 10000) && (this.totalcost < 60000)) {
      this.buttonText = 'SAVE REQUEST AND ISSUE LOCAL TENDER';
    } else if (this.totalcost >= 60000) {
      this.buttonText = 'SAVE REQUEST AND ISSUE INTERNATIONAL TENDER';
    }
  }

  saveRequest() {
    let model;
    if (!this.addLogisticRequestForm.valid) {
      return;
    }
    if (this.requestedItems.length === 0) {
      this.toastr.warning('Please add request items!');
      return;
    }
    this.isRequestFormSubmitted = true;
    model = {
      ProjectId: this.projectId,
      Description: this.addLogisticRequestForm.get('Description').value,
      CurrencyId: this.addLogisticRequestForm.get('CurrencyId').value,
      OfficeId: this.addLogisticRequestForm.get('OfficeId').value,
      BudgetLineId: this.addLogisticRequestForm.get('BudgetLineId').value,
      TotalCost: this.totalcost,
      RequestedItems: this.requestedItems
    };
    if (this.totalcost < 200) {
      model.Status = LogisticRequestStatus['New Request'];
      model.ComparativeStatus = LogisticComparativeStatus.NotValid;
      model.TenderStatus = LogisticTenderStatus.NotValid;
    } else if ((this.totalcost >= 200) && (this.totalcost < 10000)) {
      model.Status = LogisticRequestStatus['New Request'];
      model.ComparativeStatus = LogisticComparativeStatus['Pending'];
      model.TenderStatus = LogisticTenderStatus.NotValid;
    } else if ((this.totalcost >= 10000) && (this.totalcost < 60000)) {
      model.Status = LogisticRequestStatus['New Request'];
      model.ComparativeStatus = LogisticComparativeStatus.NotValid;
      model.TenderStatus = LogisticTenderStatus.Pending;
    } else if (this.totalcost >= 60000) {
      model.Status = LogisticRequestStatus['New Request'];
      model.ComparativeStatus = LogisticComparativeStatus.NotValid;
      model.TenderStatus = LogisticTenderStatus.Pending;
    }

    if (this.requestId && this.requestId !== 0) {
      model.RequestId = this.requestId;
      this.logisticservice.editLogisticRequest(model).subscribe(res => {
        if (res.StatusCode === 200) {
          this.router.navigate(['../../logistic-requests/' + this.requestId], { relativeTo: this.routeActive });
        } else {
          this.isRequestFormSubmitted = false;
          this.toastr.error(res.Message);
        }
      });
    } else {
      this.logisticservice.addLogisticRequest(model).subscribe(res => {
        if (res.StatusCode === 200 && res.data.logisticRequestId != null) {
          this.router.navigate(['../../logistic-requests/' + res.data.logisticRequestId], { relativeTo: this.routeActive });
        } else {
          this.isRequestFormSubmitted = false;
          this.toastr.error(res.Message);
        }
      });
    }
  }

  cancelRequest() {
    // this.dialogRef.close({data: null});
  }

  getRequestDetails() {
    this.commonLoader.showLoader();
    this.logisticservice.getLogisticRequestDetail(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 && res.data.logisticRequest != null) {
        this.requestDetail = res.data.logisticRequest;
        console.log(this.requestDetail);
        this.addLogisticRequestForm.setValue({
          Description: this.requestDetail.Description,
          CurrencyId : this.requestDetail.CurrencyId,
          OfficeId : this.requestDetail.OfficeId,
          BudgetLineId : this.requestDetail.BudgetLineId
        });
        this.getAllRequestItems();
      } else {
        this.commonLoader.hideLoader();
      }
    });
  }

  getAllRequestItems() {
    this.logisticservice.getAllRequestItems(this.requestId).subscribe(res => {
      this.requestItemList = [];
      if (res.StatusCode === 200 && res.data.LogisticsItemList != null) {
        res.data.LogisticsItemList.forEach(element => {
          this.requestedItems.push({
            Id: element.Id,
            ItemId : element.ItemId,
            RequestedUnits : element.Quantity,
            EstimatedUnitCost : element.EstimatedCost
          });
        });
        this.requestedItems$ = of(this.requestedItems).pipe(
          map(r => r.map(v => ({
            Id: v.Id,
            ItemId: v.ItemId,
            ItemCode: this.storeItemsList.filter(f => f.ItemId === v.ItemId)[0].ItemCode,
            ItemName: this.storeItemsList.filter(f => f.ItemId === v.ItemId)[0].ItemName,
            EstimatedUnitCost: v.EstimatedUnitCost,
            RequestedUnits: v.RequestedUnits
          }) )));
          this.getTotalCost();
          this.commonLoader.hideLoader();
      } else {
        this.commonLoader.hideLoader();
      }
    });
  }
}

export class AddLogisticRequestModel {
  RequestName;
  CurrencyId;
  BudgetLineId;
  OfficeId;
}
