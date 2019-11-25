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

  supplierHeaders$ = of(['Id', 'Supplier', 'Quantity', 'Final Price']);
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

  ngOnChanges() {
    this.routeActive.params.subscribe(params => {
      this.requestId = +params['id'];
    });
    if (this.comparativeStatus === LogisticComparativeStatus['Statement Submitted'] ||
    this.comparativeStatus === LogisticComparativeStatus['Reject Statement']) {
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
        this.comparativeStatusChange.emit(LogisticComparativeStatus['Reject Statement']);
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
        this.comparativeStatusChange.emit(LogisticComparativeStatus['Approve Statement']);
        this.StatusChange.emit(LogisticRequestStatus['Issue Purchase Order']);
      } else {
        this.commonLoader.hideLoader();
        this.toastr.error('Something went wrong!');
      }
    });
  }

  selectedPurchaseItemChange(value) {
    this.selectedItemChange.emit(value);
  }
}
