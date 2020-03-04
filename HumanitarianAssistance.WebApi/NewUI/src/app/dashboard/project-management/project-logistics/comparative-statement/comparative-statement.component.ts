import { Component, OnInit, Input, EventEmitter, Output, OnChanges } from '@angular/core';
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
import { LogisticComparativeStatus, LogisticRequestStatus } from 'src/app/shared/enum';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-comparative-statement',
  templateUrl: './comparative-statement.component.html',
  styleUrls: ['./comparative-statement.component.scss']
})
export class ComparativeStatementComponent implements OnInit, OnChanges {

  @Input() requestStatus = 0;
  @Input() comparativeStatus = 1;
  @Input() totalCost = 0;
  @Input() requestedItems: any[];
  @Output() selectedItemChange = new EventEmitter();

  supplierHeaders$ = of(['Id', 'ItemId', 'Supplier', 'Item']);
  supplierSubHeaders$ = of(['']);
  supplierData$;
  hideSupplierColums;
  requestId;
  supplierList: any;
  actions: TableActionsModel;
  statementModel = {
    SubmittedBy: '',
    Description: '',
    selectedSupplier: [],
    attachments: [],
    RejectedBy: ''
  };
  @Output() comparativeStatusChange = new EventEmitter();
  @Output() StatusChange = new EventEmitter();

  constructor(private dialog: MatDialog,
    private routeActive: ActivatedRoute,
    private logisticservice: LogisticService,
    public toastr: ToastrService,
    private commonLoader: CommonLoaderService) { }

  ngOnInit() {
    this.hideSupplierColums = of({
      headers: ['Supplier', 'Item', 'Quantity', 'Total Amount' ],
      items: ['Supplier', 'Item', 'Quantity', 'TotalAmount']
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

  ngOnChanges() {
    this.routeActive.params.subscribe(params => {
      this.requestId = +params['id'];
    });
    if (this.comparativeStatus === LogisticComparativeStatus['Statement Submitted'] ||
    this.comparativeStatus === LogisticComparativeStatus['Statement Rejected']) {
      this.getComparativeStatement();
    }
  }

  openAddSupplierDialog() {
    const dialogRef = this.dialog.open(AddSupplierComponent, {
      width: '450px',
      data: {RequestId: this.requestId}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        this.getSupplierList();
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
            Supplier: v.SourceCode,
            ItemId: v.ItemId,
            Item: v.ItemName,
            Quantity: v.Quantity,
            TotalAmount: (v.FinalUnitPrice * v.Quantity),
            subItems: [{'Heading': '<b>Warranty Document</b>', 'Name': '<a href=' + v.WarrantyUrl + ' target="_blank">'
            + v.WarrantyName + '</a>'},
            {'Heading': '<b>Invoice Document</b>', 'Name': '<a href=' + v.InvoiceUrl + ' target="_blank">'
            + v.InvoiceName + '</a>'},
            {'Heading': '<b>Quantity</b>', 'Name': v.Quantity },
            {'Heading': '<b>Final Unit Cost</b>', 'Name': v.FinalUnitPrice },
            {'Heading': '<b>Total Cost</b>', 'Name': (v.FinalUnitPrice * v.Quantity) }]
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
              this.getSupplierList();
              this.toastr.success('Deleted Sucessfully!');
            } else {
              this.toastr.error('Something went wrong!');
            }
          });
        }
      });
    }
    if (event.type === 'edit') {
      const supplier = this.supplierList.filter(function(v) {
        return v.SupplierId === event.item.Id;
      })[0];
      const editmodel = {
        Id: event.item.Id,
        SourceId: supplier.SourceCodeId,
        Item: supplier.ItemId,
        Quantity: supplier.Quantity,
        FinalUnitPrice: supplier.FinalUnitPrice,
        RequestId: this.requestId,
        InvoiceName: supplier.InvoiceName,
        WarrantyName: supplier.WarrantyName
      };
      // console.log(editmodel);
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
      width: '500px',
      data: {SupplierList: this.supplierList, RequestId: this.requestId}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        if (result.data === 'Success') {
          this.comparativeStatusChange.emit(LogisticComparativeStatus['Statement Submitted']);
        }
      }
    });
  }

  getComparativeStatement() {
    this.logisticservice.getComparativeStatement(this.requestId).subscribe(res => {
      if (res.StatusCode === 200 && res.data.ComparativeStatement != null) {
        this.statementModel = res.data.ComparativeStatement;
        if (this.comparativeStatus === LogisticComparativeStatus['Statement Submitted']) {
          this.actions = {
            items: {
              button: { status: false, text: '' },
              edit: false,
              delete: false,
              download: false,
            },
            subitems: {
            }
          };
        } else {
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
        }
      } else {
        this.toastr.error('Something went wrong!');
      }
    });
  }

  rejectComparativeStatement() {
    this.commonLoader.showLoader();
    this.logisticservice.rejectComparativeStatement(this.requestId).subscribe(res => {
      if (res.StatusCode === 200) {
        this.commonLoader.hideLoader();
        this.comparativeStatusChange.emit(LogisticComparativeStatus['Statement Rejected']);
      } else {
        this.commonLoader.hideLoader();
        this.toastr.error('Something went wrong!');
      }
    });
  }

  approveComparativeStatement() {
    this.commonLoader.showLoader();
    this.logisticservice.approveComparativeStatement(this.requestId).subscribe(res => {
      if (res.StatusCode === 200) {
        this.commonLoader.hideLoader();
        this.comparativeStatusChange.emit(LogisticComparativeStatus['Statement Approved']);
        this.StatusChange.emit(LogisticRequestStatus['Issue Purchase Order']);
      } else {
        this.commonLoader.hideLoader();
        this.toastr.error('Something went wrong!');
      }
    });
  }

  StatusEmit(value) {
    this.StatusChange.emit(value);
  }

  selectedPurchaseItemChange(value) {
    this.selectedItemChange.emit(value);
  }
}
