import { EmployeeContractService } from './../../services/employee-contract.service';
import { Component, OnInit } from '@angular/core';
import { of, Observable, ReplaySubject } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';
import { MatDialog } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { takeUntil } from 'rxjs/operators';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-employee-contract',
  templateUrl: './employee-contract.component.html',
  styleUrls: ['./employee-contract.component.scss']
})
export class EmployeeContractComponent implements OnInit {
  contractListHeader$ = of([
    'Id',
    'From Date',
    'To Date',
    'Project',
    'Budget Line',
    'Salary Currecny',
    'Base Salary'
  ]);
  actions: TableActionsModel;
  contractList$: Observable<any[]>;
  displayAddContract = true;
  employeeId: number;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    public dialog: MatDialog,
    private datePipe: DatePipe,
    private commonLoader: CommonLoaderService,
    private contractService: EmployeeContractService,
    private routeActive: ActivatedRoute
  ) {
    this.actions = {
      items: {
        button: { status: true, text: '' },
        download: false,
        edit: false,
        delete: false
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false
      }
    };
  }

  ngOnInit() {
    this.routeActive.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
    this.getEmployeeContractDetailByEmployeeId();
  }

  getEmployeeContractDetailByEmployeeId() {
    this.commonLoader.showLoader();
    this.contractService
      .getEmployeeContractDetailByEmployeeId(this.employeeId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            this.contractList$ = of(
              response.data.EmployeeContractDetails.map(y => {
                return {
                  Id: y.EmployeeContractId,
                  FromDate: this.datePipe.transform(
                    y.ContractStartDate,
                    'd/M/yyyy'
                  ),
                  ToDate: this.datePipe.transform(
                    y.ContractEndDate,
                    'd/M/yyyy'
                  ),
                  Project: y.ProjectName,
                  BudgetLine: y.BudgetLine,
                  SalaryCurrecny: y.Country,
                  BaseSalary: y.Salary,
                  itemAction: [
                    {
                      button: {
                        status: true,
                        text: 'Export',
                        type: 'pdf'
                      },
                      delete: false,
                      download: false,
                      edit: false
                    }
                  ]
                };
              })
            );
          }
          this.commonLoader.hideLoader();
        },
        () => {
          this.commonLoader.hideLoader();
        }
      );
  }
  // #region "Add Contract"
  displayAddContractForm(): void {
    this.displayAddContract = false;
  }
  //#endregion
  hideAddContract(): void {
    this.displayAddContract = true;
    this.getAdvanceListByEmployeeId();
  }

  getAdvanceListByEmployeeId() {
    debugger;
    this.
  }

  actionEvents(event: any) {
    console.log(event);
  }
}
