import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { Observable } from 'rxjs/Observable';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { MatDialog } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { EmployeeContractService } from 'src/app/hr/services/employee-contract.service';
import { ActivatedRoute } from '@angular/router';
import { EmployeeAdvanceService } from 'src/app/hr/services/employee-advance.service';
import { NewAdvanceRequestComponent } from '../new-advance-request/new-advance-request.component';
import { AdvanceBtnStatus } from 'src/app/shared/enum';
import { ToastrService } from 'ngx-toastr';
import { AdvanceHistoryComponent } from '../advance-history/advance-history.component';

@Component({
  selector: 'app-employee-advance-list',
  templateUrl: './employee-advance-list.component.html',
  styleUrls: ['./employee-advance-list.component.scss']
})
export class EmployeeAdvanceListComponent implements OnInit {

  advanceListHeader$ = of([
    'Currency',
    'Approved By',
    'Mode of Return',
    'Request Amount',
    'Advance Amount',
    'Status'
  ]);

  errorMessage = '';

  actions: TableActionsModel;
  advanceList$: Observable<any[]>;
  employeeId: number;
  hideColums$ = of({
    headers: ['Currency', 'Approved By', 'Mode of Return', 'Request Amount', 'Advance Amount', 'Status'],
    items: ['CurrencyName', 'ApprovedByEmployeeName', 'ModeOfReturn', 'RequestAmount', 'AdvanceAmount', 'Status']
  });
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(public dialog: MatDialog,
    private datePipe: DatePipe,
    private commonLoader: CommonLoaderService,
    private toastr: ToastrService,
    private advanceService: EmployeeAdvanceService,
    private routeActive: ActivatedRoute) {
    this.actions = {
      items: {
        button: { status: false, text: '' },
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
    this.getAdvanceListByEmployeeId();
  }

  getAdvanceListByEmployeeId() {
    this.errorMessage = '';
    this.advanceService.getAdvanceListEmployeeId(this.employeeId)
      .subscribe(x => {
        if (x && x.Advances) {
          this.advanceList$ = of(x.Advances.map(y => {
            return {
              AdvanceId: y.AdvanceId,
              CurrencyName: y.CurrencyName,
              ApprovedByEmployeeName: y.ApprovedByEmployeeName,
              ModeOfReturn: y.ModeOfReturn,
              RequestAmount: y.RequestAmount,
              AdvanceAmount: y.AdvanceAmount,
              Status: y.Status,
              itemAction: (y.Status !== 'UnApproved') ? ([
                {
                  button: {
                    status: true,
                    text: 'SEE HISTORY',
                    type: 'text'
                  },
                  delete: false,
                  download: false,
                  edit: false
                }
              ]) : ([
                  {
                    button: {
                      status: true,
                      text: 'APPROVE',
                      type: 'save'
                    },
                    delete: false,
                    download: false,
                    edit: false
                  },
                  {
                    button: {
                      status: true,
                      text: 'REJECT',
                      type: 'cancel'
                    },
                    delete: false,
                    download: false,
                    edit: false
                  },
                  {
                    button: {
                      status: true,
                      text: 'SEE HISTORY',
                      type: 'text'
                    },
                    delete: false,
                    download: false,
                    edit: false
                  },
                  {
                    button: {
                      status: true,
                      text: 'EDIT',
                      type: 'edit'
                    },
                    delete: false,
                    download: false,
                    edit: false
                  }])
            };
          }));
        }
      }, error => {
        this.errorMessage = error;
      });
  }

  openAddAdvancePopUp(status: number, id: number) {
    const dialogRef = this.dialog.open(NewAdvanceRequestComponent, {
      width: '450px',
      data: {
        EmployeeId: this.employeeId,
        btnStatus: status,
        Id: id
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getAdvanceListByEmployeeId();
    });
  }

  showAdvanceHistoryPopUp(id: number) {
    const dialogRef = this.dialog.open(AdvanceHistoryComponent, {
      width: '450px',
      data: {
        Id: id,
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getAdvanceListByEmployeeId();
    });
  }

  addAdvanceRequest() {
    this.openAddAdvancePopUp(AdvanceBtnStatus.NEW, 0);
  }

  actionEvents(event) {
    if (event.type === 'APPROVE') {
      this.openAddAdvancePopUp(AdvanceBtnStatus.APPROVE, event.item.AdvanceId);
    } else if (event.type === 'REJECT') {
      this.rejectAdvance(event.item.AdvanceId);
    } else if (event.type === 'EDIT') {
      this.openAddAdvancePopUp(AdvanceBtnStatus.EDIT, event.item.AdvanceId);
    } else if (event.type === 'SEE HISTORY') {
      this.showAdvanceHistoryPopUp(event.item.AdvanceId);
    }
  }

  rejectAdvance(id: number) {
    this.advanceService.rejectAdvance(id).subscribe(x => {
      if (x) {
        this.toastr.success('Rejected Successfully');
        this.getAdvanceListByEmployeeId();
      } else {
        this.toastr.warning('Something went wrong. Please try again');
      }
    }, error => {
      this.toastr.warning(error);
    });
  }
}
