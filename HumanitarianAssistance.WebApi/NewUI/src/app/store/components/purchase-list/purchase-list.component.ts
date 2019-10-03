import { Component, OnInit, HostListener } from '@angular/core';
import { of, Observable } from 'rxjs';
import { PurchaseService } from '../../services/purchase.service';
import { IFilterValueModel, IPurchaseList, IProcurementList } from '../../models/purchase';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { AddProcurementsComponent } from '../add-procurements/add-procurements.component';

@Component({
  selector: 'app-purchase-list',
  templateUrl: './purchase-list.component.html',
  styleUrls: ['./purchase-list.component.scss']
})
export class PurchaseListComponent implements OnInit {

  purchaseList$: Observable<IPurchaseList[]>;
  filterValueModel: IFilterValueModel;
  purchaseRecordCount = 0;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  showConfig = false;

  purchaseListHeaders$ = of(['Id', 'Item', 'Purchased By', 'Project', 'Original Cost', 'Deprecated Cost']);
  subListHeaders$ = of(['Id', 'Date', 'Employee', 'Procured Amount', 'Must Return', 'Returned', 'Returned On']);
  procurementList$: Observable<IProcurementList[]>;

  constructor(private purchaseService: PurchaseService, private router: Router, private dialog: MatDialog) {

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
        this.purchaseRecordCount = x.RecordCount;

        this.purchaseList$ = of(x.PurchaseList.map((element) => {
          return {
            Id: element.PurchaseId,
            Item: element.ItemName,
            PurchasedBy: element.EmployeeName,
            Project: element.ProjectName,
            OriginalCost: element.OriginalCost,
            DepreciatedCost: element.DepreciatedCost,
            subItems: element.ProcurementList
          } as IPurchaseList;
        }));
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
    const dialogRef = this.dialog.open(AddProcurementsComponent, {
      width: '850px',
      data: {
        value: event.Id,
        officeId: this.filterValueModel.OfficeId
      }
    });

    dialogRef.afterClosed().subscribe(x => {
      console.log(x);

      this.purchaseList$.subscribe((purchase) => {
        console.log(purchase);
        // debugger;
        // const index = purchase.findIndex(i => i.Id === x.PurchaseId);
        // if (index !== -1) {
        //   purchase[index].subItems.unshift({
        //     EmployeeName: x.EmployeeName,
        //     IssueDate: x.IssueDate,
        //     IssueId: x.ProcurementId,
        //     MustReturn: x.MustReturn,
        //     ProcuredAmount: x.IssuedQuantity,
        //     Returned: false
        //   });
        // }
      });
    });
  }

  showConfiguration(){
    this.showConfig = true;
  }

}
