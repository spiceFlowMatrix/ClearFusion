import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/lib/models/table-actions-model';
import { AddLogisticRequestComponent } from '../add-logistic-request/add-logistic-request.component';
import { MatDialog, MatDialogRef } from '@angular/material';
import { Router, ActivatedRoute } from '@angular/router';
import { LogisticService } from '../logistic.service';
import { map } from 'rxjs/operators';
import { LogisticRequestStatus } from 'src/app/shared/enum';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { IDropDownModel } from 'src/app/dashboard/accounting/report-services/report-models';

@Component({
  selector: 'app-logistic-requests',
  templateUrl: './logistic-requests.component.html',
  styleUrls: ['./logistic-requests.component.scss']
})
export class LogisticRequestsComponent implements OnInit {

  logisticListHeaders$ = of([
    'Id',
    'Code',
    'Office',
    'Budget Line',
    'Currency',
    'Total Estimated Cost',
    'Processing Type',
    'Status'
  ]);
  navLinks: any[] = [];
  logisticListData$: Observable<any>;
  actions: TableActionsModel;
  projectId;
  requestCount = 0;
  PageIndex = 0;
  PageSize = 10;
  logisticRequestList;
  hideRequestColums;

  constructor(
    private dialog: MatDialog,
    private router: Router,
    private routeActive: ActivatedRoute,
    private logisticservice: LogisticService,
    private commonLoader: CommonLoaderService,
    public toastr: ToastrService
    ) {
      this.navLinks = [
        {
          label: 'Active',
          link: './general',
          index: 0
        },
        {
          label: 'Archieved',
          link: './designation',
          index: 1
        },
      ];
    }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: false,
        download: false,
      },
      subitems: {
      }

    };
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.getAllRequest();
    this.hideRequestColums = of({
      headers: ['Code', 'Office', 'Budget Line', 'Currency', 'Total Estimated Cost', 'Processing Type',
      'Status'],
      items: ['Code', 'Office', 'BudgetLine', 'Currency', 'TotalEstimatedCost', 'ProcessingType',
      'Status']
    });
  }

  addNewRequest() {
    this.router.navigate(['../logistic-requests/new-request'], { relativeTo: this.routeActive });
  }
  pageEvent(e) {
    this.PageIndex = e.pageIndex;
    this.PageSize = e.pageSize;
    this.getAllRequest();
  }

  // openAddRequestDialog(): void {
  //   const dialogRef = this.dialog.open(AddLogisticRequestComponent, {
  //     width: '450px',
  //     data: {ProjectId: this.projectId, Currency: this.currencyId$, Office: this.officeId$, BudgetLine: this.budgetLineId$}
  //   });

  //   dialogRef.afterClosed().subscribe(result => {
  //     if (result !== undefined && result.data != null ) {
  //       this.refreshRequestList(result.data);
  //     }
  //   });
  // }

  // refreshRequestList(value) {
  //   this.logisticRequestList.push(value);
  //   this.logisticListData$ = of(this.logisticRequestList).pipe(
  //     map(r => r.map(v => ({
  //       Id: v.RequestId,
  //       Name: v.RequestName,
  //       Status: LogisticRequestStatus[v.Status],
  //       TotalCost: v.TotalCost
  //      }) as IRequestList)));
  //   this.requestCount++;
  // }
  requestRowClicked(event) {
    this.router.navigate([event.Id], { relativeTo: this.routeActive });
  }

  getAllRequest() {
    this.commonLoader.showLoader();
    const model = {
      projectId: this.projectId,
      PageIndex: this.PageIndex,
      PageSize: this.PageSize
    };
    this.logisticservice.getAllLogisticRequests(model).subscribe(res => {
      this.logisticRequestList = [];
      if (res.StatusCode === 200 && res.data.logisticRequestList != null) {
        this.requestCount = res.data.TotalCount;
        res.data.logisticRequestList.forEach(element => {
          this.logisticRequestList.push(element);
        });
        this.logisticListData$ = of(this.logisticRequestList).pipe(
          map(r => r.map(v => ({
            Id: v.RequestId,
            Code: v.RequestCode,
            Office: v.Office,
            BudgetLine: v.BudgetLine,
            Currency: v.Currency,
            TotalEstimatedCost: v.TotalCost,
            ProcessingType: v.ProcessingType,
            Status: v.Status
           }) as IRequestList)));
      }
      this.commonLoader.hideLoader();
    });
  }

  // onDeleteRequest(event) {
  //   if (event.type === 'delete') {
  //     this.logisticservice.openDeleteDialog().subscribe(v => {
  //       if (v) {
  //         this.logisticservice.deleteLogisticRequestById(event.item.Id).subscribe(res => {
  //           if (res.StatusCode === 200) {
  //             this.refreshRequestListAfterDelete(event.item.Id);
  //             this.toastr.success('Deleted Sucessfully!');
  //           } else {
  //             this.toastr.error('Something went wrong!');
  //           }
  //         });
  //       }
  //     });
  //   }
  // }

  // refreshRequestListAfterDelete(value) {
  //   const index = this.logisticRequestList.findIndex(v => v.RequestId === value);
  //   if (index !== -1) {
  //     this.logisticRequestList.splice(index, 1);
  //   }
  //   this.logisticListData$ = of(this.logisticRequestList).pipe(
  //     map(r => r.map(v => ({
  //       Id: v.RequestId,
  //       Name: v.RequestName,
  //       Status: ((LogisticRequestStatus[v.Status] === 'NewRequest') ? 'New Request' : LogisticRequestStatus[v.Status]),
  //       TotalCost: v.TotalCost
  //      }) as IRequestList)));
  // }
}

interface IRequestList {
  Code;
  Office;
  BudgetLine;
  Currency;
  TotalEstimatedCost;
  ProcessingType;
  Status;
}
