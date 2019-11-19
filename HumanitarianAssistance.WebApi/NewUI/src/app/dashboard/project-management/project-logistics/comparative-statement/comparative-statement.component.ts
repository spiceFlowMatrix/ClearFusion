import { Component, OnInit, Input } from '@angular/core';
import { of } from 'rxjs';
import { AddSupplierComponent } from '../add-supplier/add-supplier.component';
import { MatDialog } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { LogisticService } from '../logistic.service';
import { map } from 'rxjs/operators';
import { TableActionsModel } from 'projects/library/src/public_api';
import { ToastrService } from 'ngx-toastr';
import { SubmitPurchaseListComponent } from '../submit-purchase-list/submit-purchase-list.component';
import { SubmitComparativeStatementComponent } from '../submit-comparative-statement/submit-comparative-statement.component';

@Component({
  selector: 'app-comparative-statement',
  templateUrl: './comparative-statement.component.html',
  styleUrls: ['./comparative-statement.component.scss']
})
export class ComparativeStatementComponent implements OnInit {

  @Input() requestStatus = 0;
  @Input() comparativeStatus = 1;
  @Input() totalCost = 0;

  supplierHeaders$ = of(['Id', 'Supplier', 'Quantity', 'Final Price']);
  supplierSubHeaders$ = of(['']);
  supplierData$;
  hideSupplierColums;
  requestId;
  supplierList: any;
  actions: TableActionsModel;

  constructor(private dialog: MatDialog,
    private routeActive: ActivatedRoute,
    private logisticservice: LogisticService,
    public toastr: ToastrService) { }

  ngOnInit() {
    this.hideSupplierColums = of({
      headers: ['Supplier', 'Quantity', 'Final Price'],
      items: ['Supplier', 'Quantity', 'FinalPrice']
    });
    this.routeActive.params.subscribe(params => {
      this.requestId = +params['id'];
    });
    this.actions = {
      items: {
        button: { status: false, text: '' },
        edit: true,
        delete: true,
        download: false,
      },
      subitems: {
      }

    };
    this.getSupplierList();
  }

  openAddSupplierDialog() {
    const dialogRef = this.dialog.open(AddSupplierComponent, {
      width: '450px',
      data: {RequestId: this.requestId}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        this.supplierList.push(result.data);
        this.supplierData$ = of(this.supplierList).pipe(
          map(r => r.map(v => ({
            Id: v.SupplierId,
            Supplier: v.SupplierName,
            Quantity: v.Quantity,
            FinalPrice: v.FinalPrice,
           }) )));
      }
    });
  }

  getSupplierList() {
    this.logisticservice.getSuppliersList(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 && res.data.LogisticsSupplierList != null) {
        this.supplierList = [];
        res.data.LogisticsSupplierList.forEach(element => {
          this.supplierList.push(element);
        });
        this.supplierData$ = of(this.supplierList).pipe(
          map(r => r.map(v => ({
            Id: v.SupplierId,
            Supplier: v.SupplierName,
            Quantity: v.Quantity,
            FinalPrice: v.FinalPrice,
           }) )));
      } else {

      }
    });
  }

  onActionClick(event) {
    if (event.type === 'delete') {
      this.logisticservice.openDeleteDialog().subscribe(v => {
        if (v) {
          this.logisticservice.deleteSupplierById(event.item.Id).subscribe(res => {
            if (res.StatusCode === 200) {
              this.refreshSupplierListAfterDelete(event.item.Id);
              this.toastr.success('Deleted Sucessfully!');
            } else {
              this.toastr.error('Something went wrong!');
            }
          });
        }
      });
    }
    if (event.type === 'edit') {
      const editmodel = {
        Id: event.item.Id,
        Supplier: event.item.Supplier,
        Quantity: event.item.Quantity,
        FinalPrice: event.item.FinalPrice,
        RequestId: this.requestId
      };
      const dialogRef = this.dialog.open(AddSupplierComponent, {
        width: '450px',
        data: editmodel
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result !== undefined && result.data != null ) {
          this.getSupplierList();
        }
      });
    }
  }

  refreshSupplierListAfterDelete(supplierId) {
    const index = this.supplierList.findIndex(v => v.SupplierId === supplierId);
    if (index !== -1) {
      this.supplierList.splice(index, 1);
    }
    this.supplierData$ = of(this.supplierList).pipe(
      map(r => r.map(v => ({
        Id: v.SupplierId,
        Supplier: v.SupplierName,
        Quantity: v.Quantity,
        FinalPrice: v.FinalPrice,
       }) )));
  }

  submitStatement() {
    const dialogRef = this.dialog.open(SubmitComparativeStatementComponent, {
      width: '650px',
      data: {SupplierList: this.supplierList}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
      }
    });
  }

}
