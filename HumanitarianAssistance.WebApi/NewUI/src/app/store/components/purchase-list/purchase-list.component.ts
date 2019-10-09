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

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  actions: TableActionsModel;

  showConfig = false;
  @ViewChild(PurchaseFiledConfigComponent) fieldConfig: PurchaseFiledConfigComponent;

  purchaseListHeaders$ = of(['Id', 'Item', 'Purchased By', 'Project', 'Original Cost', 'Deprecated Cost']);
  subListHeaders$ = of(['Id', 'Date', 'Employee', 'Procured Amount', 'Must Return', 'Returned', 'Returned On']);
  procurementList$: Observable<IProcurementList[]>;


  constructor(private purchaseService: PurchaseService,
    private router: Router, private dialog: MatDialog,
    private datePipe: DatePipe, private toastr: ToastrService, private loader: CommonLoaderService) {

    this.filterValueModel = {
      CurrencyId: null,
      InventoryId: null,
      InventoryTypeId: null,
      IssueEndDate: null,
      IssueStartDate: null,
      ItemGroupId: null,
      ItemId: null,
      OfficeId: null,
      JobId: null,
      ProjectId: null,
      PurchaseEndDate: null,
      PurchaseStartDate: null,
      ReceiptTypeId: null,
      PageIndex: 0,
      PageSize: 10,
      TotalCount: null
    };
  }

  ngOnInit() {
    this.getScreenSize();
    this.actions = {
      items: {
        button: { status: true, text: 'Add Procurement' },
        delete: false,
        download: false,
      },
      subitems: {
        button: { status: false, text: '' },
        delete: true,
        download: false,
      }

    }
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

  getPurchasesByFilter(filter: IFilterValueModel) {
    this.filterValueModel = filter;
    this.purchaseService
      .getFilteredPurchaseList(filter).subscribe(x => {
        this.loader.showLoader();
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
            subItems: element.ProcurementList.map((r)=> {
              return {
                Id : r.OrderId, 
                IssueDate :r.IssueDate ? this.datePipe.transform(new Date(r.IssueDate),"dd/MM/yyyy") :null,
                Employee : r.EmployeeName,
                ProcuredAmount: r.ProcuredAmount,
                MustReturn: r.MustReturn?"Yes":"No",
                Returned: r.Returned?"Yes":"No",
                ReturnedOn: r.ReturnedOn ? this.datePipe.transform(new Date(r.ReturnedOn),"dd/MM/yyyy"):null


              }
            })
          } as IPurchaseList;
        }));
        this.loader.hideLoader();
      });
  }

  onpurchaseFilterSelected(event: any) {
    this.filterValueModel = {
      CurrencyId: event.value.CurrencyId,
      PurchaseStartDate: event.value.DateOfPurchase,
      IssueStartDate: event.value.DateOfIssue,
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
  openProcurementModal(event: any) {
    if (event.type == "button") {
      const dialogRef = this.dialog.open(AddProcurementsComponent, {
        width: '850px',
        data: {
          value: event.item.Id,
          officeId: this.filterValueModel.OfficeId
        }
      });

      dialogRef.afterClosed().subscribe(x => {
        console.log(x);

        this.purchaseList$.subscribe((purchase) => {
          console.log(purchase);

          const index = purchase.findIndex(i => i.Id === x.PurchaseId);
          if (index !== -1) {
            purchase[index].subItems.unshift({
              EmployeeName: x.EmployeeName,
              IssueDate: this.datePipe.transform(x.IssueDate, 'dd-MM-yyyy'),
              OrderId: x.ProcurementId,
              MustReturn: x.MustReturn,
              ProcuredAmount: x.IssuedQuantity,
              Returned: false
            });
          }
          this.purchaseList$ = of(purchase);
        });
      });
    }

  }

  deleteProcurement(event: any) {
    this.purchaseService.deleteProcurement(event.subItem.OrderId)
      .subscribe(x => {
        if (x.StatusCode === 200) {
          this.purchaseList$.subscribe((purchase) => {
            const index = purchase.findIndex(i => i.Id === event.item.Id);
            if (index >= 0) {
              const subItemIndex = purchase[index].subItems.findIndex(i => i.OrderId === event.subItem.OrderId);
              purchase[index].subItems.splice(subItemIndex, 1);
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
  }

  configFilterAppliedEvent(event: any) {

    let headers: any[]=[];

    event.forEach(element => {
      headers.push(element.name);
    });

     this.purchaseListHeaders$ = of(headers);

    this.purchaseFilterConfigList$.subscribe(y => {

      // this.purchaseList$ = of(y.map((element) => {
      //   return {

      //   } as IPurchaseList;
      // }));
    });

  }

  showConfiguration() {
    this.fieldConfig.show();
  }
}
