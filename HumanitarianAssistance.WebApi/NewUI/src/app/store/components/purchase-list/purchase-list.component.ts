import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { of, Observable } from 'rxjs';
import { PurchaseService } from '../../services/purchase.service';
import { IFilterValueModel, IPurchaseList, IProcurementList, IPurchaseFilterConfigColList } from '../../models/purchase';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { AddProcurementsComponent } from '../add-procurements/add-procurements.component';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { PurchaseFiledConfigComponent } from '../purchase-filed-config/purchase-filed-config.component';
import { TableActionsModel } from 'projects/library/src/public_api';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { FieldConfigService } from '../../services/field-config.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { IDropDownModel } from 'src/app/dashboard/accounting/report-services/report-models';
import { StaticUtilities } from 'src/app/shared/static-utilities';

@Component({
  selector: 'app-purchase-list',
  templateUrl: './purchase-list.component.html',
  styleUrls: ['./purchase-list.component.scss']
})
export class PurchaseListComponent implements OnInit {

  purchaseList$: Observable<IPurchaseList[]>;
  purchaseFilterConfigList$: Observable<IPurchaseFilterConfigColList[]>;
  filterValueModel: IFilterValueModel;
  purchaseRecordCount = 0;
  currencyList$: Observable<IDropDownModel[]>;
  selectedDisplayCurrency: number;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  actions: TableActionsModel;

  showConfig = false;
  @ViewChild(PurchaseFiledConfigComponent) fieldConfig: PurchaseFiledConfigComponent;

  purchaseListHeaders$ = of(['Id', 'Item', 'Purchased By', 'Project', 'Original Cost', 'Depreciated Cost', 'Purchase Date', 'Currency',
    'Purchased Quantity', 'Item Code', 'Project Id', 'Item Code Description', 'Description', 'BudgetLine Name',
    'Depreciation Rate', 'Master Inventory Code', 'Office Code', 'Receipt Date', 'Invoice Date',
    'Received From Location', 'Status']);
  subListHeaders$ = of(['Id', 'Date', 'Employee', 'Must Return', 'Procurement Balance (Quantity)', 'Status']);
  procurementList$: Observable<IProcurementList[]>;
  hideColums: Observable<{ headers?: string[], items?: string[] }>;
  hideColumsSub: Observable<{ headers?: string[], items?: string[] }>;
  columnsToShownInPdf: any[] = [];

  constructor(private purchaseService: PurchaseService,
    private router: Router, private dialog: MatDialog,
    private datePipe: DatePipe, private toastr: ToastrService,
    private loader: CommonLoaderService, private fieldConfigservice: FieldConfigService,
    private globalSharedService: GlobalSharedService, private appurl: AppUrlService) {

    this.filterValueModel = {
      CurrencyId: 0,
      InventoryId: 0,
      InventoryTypeId: 0,
      IssueEndDate: null,
      IssueStartDate: null,
      ItemGroupId: 0,
      ItemId: 0,
      OfficeId: 0,
      JobId: 0,
      ProjectId: 0,
      PurchaseEndDate: null,
      PurchaseStartDate: null,
      ReceiptTypeId: 0,
      DisplayCurrency: 0,
      DepreciationComparisionDate: null,
      PageIndex: 0,
      PageSize: 10,
      TotalCount: 0
    };
  }

  ngOnInit() {
    this.hideColumsSub = of({
      headers: ['Date', 'Employee', 'Must Return', 'Procurement Balance (Quantity)', 'Status'],
      items: ['IssueDate', 'Employee', 'MustReturn', 'ProcuredAmount', 'Status']
    });

    this.getScreenSize();
    this.getAllCurrencies();
    this.actions = {
      items: {
        button: { status: true, text: 'Add Procurement' },
        delete: false,
        download: false,
        edit: true
      },
      subitems: {
        button: { status: true, text: 'Add Return' },
        delete: true,
        download: false,
        edit: false
      }
    };

    this.fieldConfigservice.data.subscribe(res => {
      if (res.length > 0) {
        const headers = res.map(r => r.headerName);
        const items = res.map(r => r.modelName);
        this.hideColums = of({ headers: headers, items: items });
        this.columnsToShownInPdf = res;
      }
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
      'overflow-x': 'scroll',
      width: this.screenWidth
    };
  }
  //#endregion

  getPurchasesByFilter(filter: IFilterValueModel) {
    this.loader.showLoader();
    this.filterValueModel = filter;
    this.purchaseService
      .getFilteredPurchaseList(filter).subscribe(x => {
        this.purchaseRecordCount = x.RecordCount;
        this.purchaseFilterConfigList$ = of(x.PurchaseList);
        this.purchaseList$ = of(x.PurchaseList.map((element) => {
          return {
            Id: element.PurchaseId,
            Item: element.ItemName,
            PurchasedBy: element.EmployeeName,
            Project: element.ProjectName,
            OriginalCost: element.OriginalCost,
            DepreciatedCost: element.DepreciatedCost,
            PurchaseDate: element.PurchaseDate ? this.datePipe.transform(new Date(element.PurchaseDate), 'dd/MM/yyyy') : null,
            Currency: element.CurrencyName,
            ItemCode: element.ItemCode,
            ProjectId: element.ProjectId,
            ItemCodeDescription: element.ItemCodeDescription,
            Description: element.Description,
            BudgetLineName: element.BudgetLineName,
            DepreciationRate: element.DepreciationRate,
            MasterInventoryCode: element.MasterInventoryCode,
            OfficeCode: element.OfficeCode,
            ReceiptDate: (element.ReceiptDate),
            ItemId: element.ItemId,
            // this.datePipe.transform(new Date(element.ReceiptDate), 'dd/MM/yyyy') : null,
            InvoiceDate: (element.InvoiceDate),
            // this.datePipe.transform(new Date(element.InvoiceDate), 'dd/MM/yyyy') : null,
            ReceivedFromLocationName: element.ReceivedFromLocationName,
            Status: element.Status,
            ProcurementList: element.ProcurementList,
            Quantity: element.Quantity,
            subItemSubtitle: element.LogisticRequestId ? '<b>Note:</b> The purchase was created as a result of <a href="/project/my-project/' + element.ProjectId + '/logistic-requests/' + element.LogisticRequestId + '" target="_blank">purchase-order-id-' + element.LogisticRequestId + ' </a></br>' : '',
            subItems: element.ProcurementList.map((r) => {
              return {
                Id: r.OrderId,
                IssueDate: (r.IssueDate != null && r.IssueDate !== undefined) ?
                  this.datePipe.transform(new Date(r.IssueDate), 'dd/MM/yyyy') : null,
                Employee: r.EmployeeName,
                MustReturn: r.MustReturn ? 'Yes' : 'No',
                ProcuredAmount: r.ProcuredAmount,
                Status: r.IsDeleted ? 'Cancelled' : r.ProcuredAmount > 0 ? 'Active' : 'In-Active',
                itemAction: ((r.MustReturn && r.ProcuredAmount > 0) && !r.IsDeleted) ? (
                  {
                    button: {
                      status: true,
                      text: 'ADD RETURN',
                      type: 'add'
                    },
                    delete: true,
                    download: false,
                    edit: false
                  }) : (
                    {
                      button: {
                        status: false,
                        text: 'ADD RETURN',
                        type: 'add'
                      },
                      delete: false,
                      download: false,
                      edit: false
                    })
                // Returned: r.Returned ? 'Yes' : 'No',
                // ReturnedOn: r.ReturnedOn ? this.datePipe.transform(new Date(r.ReturnedOn), 'dd/MM/yyyy') : null,
              };
            }
            ),
            itemAction: ((element.Quantity - element.ProcurementList
              .filter(x => x.IsDeleted === false)
              .reduce(function (a, b) { return a + b.ProcuredAmount; }, 0)) > 0) ?
              ([
                {
                  button: {
                    status: true,
                    text: 'ADD PROCUREMENT',
                    type: 'add'
                  },
                  delete: false,
                  download: false,
                  edit: false
                }]) : ([
                  {
                    button: {
                      status: false,
                      text: 'ADD PROCUREMENT',
                    },
                    delete: false,
                    download: false,
                    edit: false
                  }])
          } as IPurchaseList;
        }));
        this.loader.hideLoader();
      }, error => {
        this.loader.hideLoader();
      });
  }

  onpurchaseFilterSelected(event: any) {
    this.filterValueModel = {
      CurrencyId: event.value.CurrencyId,
      PurchaseStartDate: StaticUtilities.setLocalDate(event.value.DateOfPurchase),
      IssueStartDate: StaticUtilities.setLocalDate(event.value.DateOfIssue),
      InventoryTypeId: event.value.InventoryTypeId,
      ReceiptTypeId: event.value.ReceiptTypeId,
      OfficeId: event.value.OfficeId,
      ProjectId: event.value.ProjectId,
      JobId: event.value.JobId,
      InventoryId: event.value.InventoryMasterId,
      ItemGroupId: event.value.ItemGroupId,
      ItemId: event.value.ItemId,
      IssueEndDate: null,
      PurchaseEndDate: null,
      DisplayCurrency: this.selectedDisplayCurrency,
      DepreciationComparisionDate: StaticUtilities.setLocalDate(event.value.DepreciationComparisionDate),
      PageIndex: this.filterValueModel.PageIndex,
      PageSize: this.filterValueModel.PageSize
    };

    this.getPurchasesByFilter(this.filterValueModel);
  }

  //#region "pageEvent"
  pageEvent(e) {
    this.filterValueModel.PageIndex = e.pageIndex;
    this.filterValueModel.PageSize = e.pageSize;
    this.getPurchasesByFilter(this.filterValueModel);
  }
  //#endregion

  addPurchase() {
    this.router.navigate(['/store/purchase/add']);
  }
  actionEvents(event: any) {

    if (event.type === 'ADD PROCUREMENT') {
      let remainingQuantity = 0;
      if (event.item.subItems.length > 0) {
        let filteredObjects = (event.item.subItems.filter(x => x.Status !== 'Cancelled'));
         remainingQuantity = (event.item.Quantity - (filteredObjects.reduce(function (a, b) { return a + b.ProcuredAmount; }, 0)));
      } else {
        remainingQuantity = event.item.Quantity;
      }


      // if (!this.filterValueModel.OfficeId) {
      //   this.toastr.warning('Select office before adding procurement');
      //   return;
      // }
      // const data = {
      //   value: event.item.Id,
      //   officeId: this.filterValueModel.OfficeId,
      // };

      this.router.navigate(['/store/purchases/add-procurement'],
        {
          queryParams: {
            quantity: remainingQuantity,
            purchaseId: event.item.Id,
            itemId: event.item.ItemId
          }
        });

      // this.openProcurementDialog(data);
    } else if (event.type === 'edit') {
      this.router.navigate(['/store/purchase/edit/' + event.item.Id]);
    }

  }

  // openProcurementDialog(item) {
  //   const dialogRef = this.dialog.open(AddProcurementsComponent, {
  //     width: '850px',
  //     data: item
  //   });

  //   dialogRef.afterClosed().subscribe(x => {
  //     this.getPurchasesByFilter(this.filterValueModel);
  //   });
  // }

  procurementAction(event: any) {
    if (event.type === 'delete') {
      this.purchaseService.deleteProcurement(event.subItem.Id)
        .subscribe(x => {
          if (x.StatusCode === 200) {
            this.purchaseList$.subscribe((purchase: any) => {
              const index = purchase.findIndex(i => i.Id === event.item.Id);
              if (index >= 0) {
                const subItemIndex = purchase[index].subItems.findIndex(i => i.Id === event.subItem.Id);
                purchase[index].subItems[subItemIndex].Status = 'Cancelled';
              }
              this.purchaseList$ = of(purchase);
            });
            this.toastr.success(x.Message);
          } else {
            this.toastr.warning(x.Message);
          }
        },
          (error) => {
            this.toastr.error(error);
          });
    } else if (event.type === 'edit') {
      if (!this.filterValueModel.OfficeId) {
        this.toastr.warning('Select office before editing procurement');
        return;
      }
      const index = event.item.ProcurementList.findIndex(x => x.OrderId === event.subItem.Id);
      const orderData = event.item.ProcurementList[index];
      const data = {
        value: event.item.Id,
        officeId: this.filterValueModel.OfficeId,
        procurement: orderData
      };

      // this.openProcurementDialog(data);
    } else if (event.type === 'add') {
      this.router.navigate(['store/purchases/procurement-control-panel/' + event.subItem.Id],
        {
          queryParams: {
            quantity: event.subItem.ProcuredAmount,
          }
        }
      );
    }
  }

  getAllCurrencies() {
    this.purchaseService.getAllCurrencies()
      .subscribe(x => {
        if (x.StatusCode === 200) {
          this.currencyList$ = of(x.data.CurrencyList.map(y => {
            return {
              name: y.CurrencyCode + '-' + y.CurrencyName,
              value: y.CurrencyId
            };
          }));
          this.selectedDisplayCurrency = x.data.CurrencyList[0].CurrencyId;
        }
      },
        (error) => {
          this.toastr.error(error);
        });
  }

  selectedDisplayCurrencyChanged() {
    this.filterValueModel.DisplayCurrency = this.selectedDisplayCurrency;
    this.getPurchasesByFilter(this.filterValueModel);

  }

  onPdfExportClick() {
    let pdfColumns;

    this.hideColums.subscribe(x => pdfColumns = x.items);

    const StorePurchaseFilter = this.filterValueModel;
    StorePurchaseFilter.SelectedColumns = [];
    StorePurchaseFilter.SelectedColumns = pdfColumns;

    this.globalSharedService
      .getFile(this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetStorePurchasePdf, StorePurchaseFilter
      )
      .pipe()
      .subscribe();
  }

  showConfiguration() {
    this.fieldConfig.show();
  }
}
