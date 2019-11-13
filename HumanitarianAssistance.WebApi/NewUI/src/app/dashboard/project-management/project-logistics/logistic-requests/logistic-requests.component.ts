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
    'Name',
    'Status',
    'Total Cost',
  ]);
  logisticListData$: Observable<any>;
  actions: TableActionsModel;
  projectId;
  logisticRequestList;
  officeDropdown: any[];
  currencyDropdown: any[];
  currencyId$: Observable<IDropDownModel[]>;
  budgetLineId$: Observable<IDropDownModel[]>;
  officeId$: Observable<IDropDownModel[]>;
  constructor(
    private dialog: MatDialog,
    private router: Router,
    private routeActive: ActivatedRoute,
    private logisticservice: LogisticService,
    private commonLoader: CommonLoaderService,
    public toastr: ToastrService
    ) { }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: false,
      },
      subitems: {
      }

    };
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.getAllRequest();
    this.getCurrencyCodeList();
    this.getOfficeCodeList();
    this.getBudgetLineList();
  }

  openAddRequestDialog(): void {
    const dialogRef = this.dialog.open(AddLogisticRequestComponent, {
      width: '450px',
      data: {ProjectId: this.projectId, Currency: this.currencyId$, Office: this.officeId$, BudgetLine: this.budgetLineId$}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && result.data != null ) {
        this.refreshRequestList(result.data);
      }
    });
  }

  refreshRequestList(value) {
    this.logisticRequestList.push(value);
    this.logisticListData$ = of(this.logisticRequestList).pipe(
      map(r => r.map(v => ({
        Id: v.RequestId,
        Name: v.RequestName,
        Status: LogisticRequestStatus[v.Status],
        TotalCost: v.TotalCost
       }) as IRequestList)));
  }
  requestRowClicked(event) {
    this.router.navigate([event.Id], { relativeTo: this.routeActive });
  }

  getAllRequest() {
    this.logisticservice.getAllLogisticRequests(this.projectId).subscribe(res => {
      this.logisticRequestList = [];
      if (res.StatusCode === 200 && res.data.logisticRequestList != null) {
        res.data.logisticRequestList.forEach(element => {
          this.logisticRequestList.push(element);
        });
        this.logisticListData$ = of(this.logisticRequestList).pipe(
          map(r => r.map(v => ({
            Id: v.RequestId,
            Name: v.RequestName,
            Status: ((LogisticRequestStatus[v.Status] === 'NewRequest') ? 'New Request' : LogisticRequestStatus[v.Status]),
            TotalCost: v.TotalCost
           }) as IRequestList)));
      }
    });
  }

  onDeleteRequest(event) {
    if (event.type === 'delete') {
      this.logisticservice.openDeleteDialog().subscribe(v => {
        if (v) {
          this.logisticservice.deleteLogisticRequestById(event.item.Id).subscribe(res => {
            if (res.StatusCode === 200) {
              this.refreshRequestListAfterDelete(event.item.Id);
              this.toastr.success('Deleted Sucessfully!');
            } else {
              this.toastr.error('Something went wrong!');
            }
          });
        }
      });
    }
  }

  refreshRequestListAfterDelete(value) {
    const index = this.logisticRequestList.findIndex(v => v.RequestId === value);
    if (index !== -1) {
      this.logisticRequestList.splice(index, 1);
    }
    this.logisticListData$ = of(this.logisticRequestList).pipe(
      map(r => r.map(v => ({
        Id: v.RequestId,
        Name: v.RequestName,
        Status: ((LogisticRequestStatus[v.Status] === 'NewRequest') ? 'New Request' : LogisticRequestStatus[v.Status]),
        TotalCost: v.TotalCost
       }) as IRequestList)));
  }

  getCurrencyCodeList() {
    this.logisticservice
      .GetAllCurrencyCodeList()
      .subscribe(
        data => {
          this.currencyDropdown = [];
          if (data.data.CurrencyList != null) {
            data.data.CurrencyList.forEach(element => {
              this.currencyDropdown.push(element);
            });

            // this.selectedCurrency = this.currencyDropdown[0].CurrencyId;
            // this.addLogisticRequestForm.controls['CurrencyId'].setValue(this.selectedCurrency);
            this.currencyId$ = of(this.currencyDropdown.map(y => {
              return {
                value: y.CurrencyId,
                name: y.CurrencyCode + '-' + y.CurrencyName
              };
            }));
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  getOfficeCodeList() {
    this.logisticservice
      .GetAllOfficeCodeList()
      .subscribe(
        data => {
          if (data.data.OfficeDetailsList != null) {
            this.officeDropdown = [];
            data.data.OfficeDetailsList.forEach(element => {
              this.officeDropdown.push({
                Id: element.OfficeId,
                Name: element.OfficeName
              });
            });
            this.officeId$ = of(this.officeDropdown.map(y => {
              return {
                value: y.Id,
                name: y.Name
              };
            }));
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  getBudgetLineList() {
    this.logisticservice
      .GetBudgetLineListByProjectId(this.projectId)
      .subscribe(
        data => {
          this.currencyDropdown = [];
          if (data.data.ProjectBudgetLineDetailList != null) {
            this.budgetLineId$ = of(data.data.ProjectBudgetLineDetailList.map(y => {
              return {
                value: y.BudgetLineId,
                name: y.BudgetName
              };
            }));
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
}

interface IRequestList {
  Name;
  Status;
  TotalCost;
}
