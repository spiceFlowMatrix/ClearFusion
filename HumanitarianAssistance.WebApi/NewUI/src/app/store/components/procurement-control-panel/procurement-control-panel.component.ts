import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';
import { of } from 'rxjs/internal/observable/of';
import { Observable } from 'rxjs/internal/Observable';
import { IReturnModel, IProcurementDetailModel } from '../../models/purchase';
import { TableActionsModel } from 'projects/library/src/public_api';
import { PurchaseService } from '../../services/purchase.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-procurement-control-panel',
  templateUrl: './procurement-control-panel.component.html',
  styleUrls: ['./procurement-control-panel.component.scss']
})
export class ProcurementControlPanelComponent implements OnInit {

  returnListHeaders$ = of(['Id', 'Date', 'Quantity']);
  procurement: IProcurementDetailModel;
  returnList: Observable<IReturnModel[]>;
  addReturnsForm: FormGroup;
  actions: TableActionsModel;
  hideColums: Observable<{ headers?: string[], items?: string[] }>;
  procurementId: number;
  procurementDetail: IProcurementDetailModel;

  @ViewChild('unittype') dialogRef: TemplateRef<any>;

  constructor(private router: Router, private routeActive: ActivatedRoute,
    private purchaseService: PurchaseService, private dialog: MatDialog,
    private fb: FormBuilder, private toastr: ToastrService) { }

  ngOnInit() {
    this.onInItForm();
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: false,
        edit: false
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false,
        edit: false
      }
    };

    this.routeActive.params.subscribe(params => {
      this.procurementId = +params['id'];
    });

    this.hideColums = of({ headers: ['Date', 'Quantity'], items: ['Date', 'ReturnedQuantity'] });
    this.getProcurementDetails();
  }

  onReturnFormInIt() {
    this.addReturnsForm = this.fb.group({
      'Date': [null, Validators.required],
      'Quantity': [1, [Validators.required, Validators.min(1), Validators.max(this.procurementDetail.StartingBalance -
        this.procurementDetail.CurrentBalance)]]
    });
  }

  onInItForm() {
    this.procurementDetail = {
      CurrentBalance: null,
      Date: null,
      Id: null,
      IssuedToEmployee: null,
      ItemCode: null,
      MustReturn: null,
      Project: null,
      PurchaseId: null,
      ReturnedQuantity: null,
      StartingBalance: null,
      Status: null,
      Voucher: null,
    };
  }

  actionEvents(event: any) {
    debugger;

    if (event.type === 'delete') {
      this.purchaseService.deleteReturnProcurement(event.item.Id).subscribe(x=> {
        if (x) {
          this.getProcurementDetails();
        }
      });
    }
  }

  addReturns() {
    this.onReturnFormInIt();
    this.openAddReturns();
  }

  goToListingPage() {
    this.router.navigate(['store/purchases']);
  }

  openAddReturns() {
    this.dialog.open(this.dialogRef, {
      width: '600px'
    });
  }

  saveAddReturns() {
    debugger;

    if (!this.addReturnsForm.valid) {
      this.toastr.warning('Please correct form errors and submit again');
    }

    const model = {
      PurchaseId: this.procurementDetail.PurchaseId,
      ProcurementId: this.procurementDetail.Id,
      ReturnedDate: StaticUtilities.getLocalDate(this.addReturnsForm.value.Date) ,
      ReturnedQuantity: this.addReturnsForm.value.Quantity
    };

    this.purchaseService.addProcurementReturn(model).subscribe(x => {
      this.dialog.closeAll();
      this.getProcurementDetails();
    });
  }

  getProcurementDetails() {
    if (this.procurementId) {
      this.purchaseService.getProcurementDetailWithReturnsList(this.procurementId)
        .subscribe(x => {
          if (x && x.ProcurementDetail) {
            debugger;
            this.procurementDetail.Id = x.ProcurementDetail.Id;
            this.procurementDetail.Date = x.ProcurementDetail.Date;
            this.procurementDetail.ItemCode = x.ProcurementDetail.ItemCode;
            this.procurementDetail.PurchaseId = x.ProcurementDetail.PurchaseId;
            this.procurementDetail.Status = x.ProcurementDetail.Status;
            this.procurementDetail.Voucher = x.ProcurementDetail.VoucherNo;
            this.procurementDetail.IssuedToEmployee = x.ProcurementDetail.EmployeeName;
            this.procurementDetail.StartingBalance = x.ProcurementDetail.StartingBalance;
            this.procurementDetail.CurrentBalance = x.ProcurementDetail.CurrentBalance;
            this.procurementDetail.Project = x.ProcurementDetail.ProjectName;
            this.procurementDetail.MustReturn = x.ProcurementDetail.MustReturn;

            this.returnList = of(x.ProcurementDetail.ProcurementReturnList);
          }
        });
    }
  }
}
