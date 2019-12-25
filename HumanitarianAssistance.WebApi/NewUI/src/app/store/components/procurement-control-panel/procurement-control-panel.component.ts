import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { of } from 'rxjs/internal/observable/of';
import { Observable } from 'rxjs/internal/Observable';
import { IReturnModel, IProcurementDetailModel } from '../../models/purchase';
import { TableActionsModel } from 'projects/library/src/public_api';
import { PurchaseService } from '../../services/purchase.service';

@Component({
  selector: 'app-procurement-control-panel',
  templateUrl: './procurement-control-panel.component.html',
  styleUrls: ['./procurement-control-panel.component.scss']
})
export class ProcurementControlPanelComponent implements OnInit {

  returnListHeaders$ = of(['Id', 'Date', 'Quantity']);
  procurementList$: Observable<IReturnModel[]>;
  actions: TableActionsModel;
  hideColums: Observable<{ headers?: string[], items?: string[] }>;
  procurementId: number;
  procurementDetail: IProcurementDetailModel;

  constructor(private router: Router, private routeActive: ActivatedRoute,
    private purchaseService: PurchaseService) { }

  ngOnInit() {
    this.onInItForm();
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: false,
        edit: true
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

     this.hideColums = of({headers: ['Date', 'Quantity'], items: ['Date', 'ReturnedQuantity']});
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
      ReturnsList: [],
      StartingBalance: null,
      Status: null,
      Voucher: null,
    };
  }

  actionEvents(event: any) {
    debugger;

  }

  goToListingPage() {
    this.router.navigate(['../../purchases'] , { relativeTo: this.routeActive });
  }

  getProcurementDetails() {
    this.p
    if (this.procurementId) {
      this.purchaseService.
    }
  }
}
