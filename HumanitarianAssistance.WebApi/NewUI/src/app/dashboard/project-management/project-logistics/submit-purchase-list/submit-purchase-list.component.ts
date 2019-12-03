import { Component, OnInit, Inject } from '@angular/core';
import { MatTableDataSource, MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { element } from '@angular/core/src/render3';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { of, Observable } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';
import { LogisticService } from '../logistic.service';
import { map } from 'rxjs/operators';
import { PurchaseFinalCostComponent } from '../purchase-final-cost/purchase-final-cost.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-submit-purchase-list',
  templateUrl: './submit-purchase-list.component.html',
  styleUrls: ['./submit-purchase-list.component.scss']
})
export class SubmitPurchaseListComponent implements OnInit {
  itemdata;
  displayedColumns: string[] = ['select', 'Id', 'Items', 'Quantity', 'EstimatedCost'];
  dataSource;
  purchaseSubmitForm: FormGroup;
  requestId;
  purchasedItemsHeaders$ = of([
    'Id',
    'Item Code',
    'Item Name',
    'Final Unit Cost',
    'Requested Units'
  ]);
  docHeaders$ = of([
    'Id',
    'File Name'
  ]);
  requestItemList = [];
  purchasedItems$;
  attachments = [];
  actions: TableActionsModel;
  docActions: TableActionsModel;
  selection = new SelectionModel<any>(true, []);
  hideItemColums: any;
  hideDocColums: any;
  docData$: Observable<any>;
  isFormSubmitted = false;
  constructor(private router: Router,
    private routeActive: ActivatedRoute,
    private fb: FormBuilder,
    private logisticservice: LogisticService,
    private dialog: MatDialog,
    public toastr: ToastrService) { }

  ngOnInit() {
    // this.itemdata = this.data.requestedItems.map(function(val) {
    //   return {
    //       Id: val.Id,
    //       Items: val.Item,
    //       Quantity: val.Quantity,
    //       EstimatedCost: val.EstimatedCost
    //   };
    // });
    this.routeActive.params.subscribe(params => {
      this.requestId = +params['id'];
    });
    this.getAllRequestItems();
    this.actions = {
      items: {
        button: { status: false, text: '' },
        edit: true,
        delete: false,
        download: false,
      },
      subitems: {
      }

    };
    this.docActions = {
      items: {
        button: { status: false, text: '' },
        edit: false,
        delete: true,
        download: false,
      },
      subitems: {
      }

    };
    this.hideItemColums = of({
      headers: ['Item Code', 'Item Name', 'Final Unit Cost', 'Requested Units'],
      items: ['ItemCode', 'ItemName', 'FinalUnitCost', 'RequestedUnits']
    });
    this.hideDocColums = of({
      headers: ['File Name'],
      items: ['FileName']
    });
    this.purchaseSubmitForm = this.fb.group({
      PurchaseDate: [null, [Validators.required]]
    });
    this.dataSource = new MatTableDataSource<any>(this.itemdata);
    // console.log(this.itemdata);
  }

  goBack() {
    this.router.navigate(['../../' + this.requestId], { relativeTo: this.routeActive });
  }
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  submitPurchase() {
    if (this.selection.selected.length === 0) {
      return false;
    } else {
      // this.dialogRef.close({data: this.selection.selected});
    }
  }

  getAllRequestItems() {
    this.logisticservice.getAllRequestItems(this.requestId).subscribe(res => {
      this.requestItemList = [];
      if (res.StatusCode === 200 && res.data.LogisticsItemList != null) {
        res.data.LogisticsItemList.forEach(e => {
          this.requestItemList.push(e);
        });
        this.purchasedItems$ = of(this.requestItemList).pipe(
          map(r => r.map(v => ({
            Id: v.Id,
            ItemCode: v.ItemCode,
            ItemName: v.Item,
            FinalUnitCost: v.EstimatedCost,
            RequestedUnits: v.Quantity
           }) )));
      }
    });
  }

  onActionClick(event) {
    if (event.type === 'edit') {
      const dialogRef = this.dialog.open(PurchaseFinalCostComponent, {
        width: '450px',
        height: '380px',
        data: {Id: event.item.Id, ItemCode: event.item.ItemCode, ItemName: event.item.ItemName, RequestedUnits: event.item.RequestedUnits}
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result !== undefined && result.data != null ) {
            const index = this.requestItemList.findIndex(v => v.Id === result.data.Id);
            if (index !== -1) {
              this.requestItemList[index].EstimatedCost = result.data.FinalCost;
              this.purchasedItems$ = of(this.requestItemList).pipe(
                map(r => r.map(v => ({
                  Id: v.Id,
                  ItemCode: v.ItemCode,
                  ItemName: v.Item,
                  FinalUnitCost: v.EstimatedCost,
                  RequestedUnits: v.Quantity
                 }) )));
            }
          } else {

          }
      });
    }

  }

  onDocActionClick(event) {
    if (event.type === 'delete') {
      this.logisticservice.openDeleteDialog().subscribe(v => {
        if (v) {
          const index = event.item.Id;
          if (index !== -1) {
            this.attachments.splice(index, 1);
            this.docData$ = of(this.attachments).pipe(
              map(r => r.map((x, i) => ({
                Id: i,
                FileName: x[0].name,
               }) )));
          }
        }
      });
    }
  }

  openInput() {
    document.getElementById('fileInput').click();
  }

  fileChange(file: any) {
    this.attachments.push(file);
    this.docData$ = of(this.attachments).pipe(
      map(r => r.map((v, i) => ({
        Id: i,
        FileName: v[0].name,
       }) )));
  }

  submitPurchaseOrder() {
    if (!this.purchaseSubmitForm.valid) {
      return;
    }
    if (this.attachments.length === 0) {
      this.toastr.warning('Please upload attachment!');
      return;
    }
    const item = this.requestItemList.map(v => ({
        Id: v.Id,
        FinalCost: v.EstimatedCost
       }) );
    const model = {
      PurchaseDate: this.purchaseSubmitForm.get('PurchaseDate').value,
      ItemModel: item,
      RequestId: this.requestId
    };
  }

  cancelSubmission() {
   // this.dialogRef.close({data: null});
  }
}
