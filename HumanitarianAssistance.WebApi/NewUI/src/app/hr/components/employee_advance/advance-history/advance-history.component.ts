import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder } from '@angular/forms';
import { EmployeeAdvanceService } from 'src/app/hr/services/employee-advance.service';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { of } from 'rxjs/internal/observable/of';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-advance-history',
  templateUrl: './advance-history.component.html',
  styleUrls: ['./advance-history.component.scss']
})
export class AdvanceHistoryComponent implements OnInit {

  advanceHistoryHeader$ = of([
    'Payment Date',
    'Installment Paid',
    'Balance Amount'
  ]);

  advanceHistoryList$: Observable<any[]>;


  constructor(private dialogRef: MatDialogRef<AdvanceHistoryComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, private fb: FormBuilder,
    private toastr: ToastrService, private advanceService: EmployeeAdvanceService,
    private commonLoader: CommonLoaderService) { }

  ngOnInit() {
    this.getAdvanceHistory();
  }

  getAdvanceHistory() {
    this.advanceService.getAdvanceHistory(this.data.Id).subscribe(x => {
      debugger;
      if (x.AdvanceHistory) {
        debugger;
        this.advanceHistoryList$ = of(x.AdvanceHistory.map(y => {
          return {
            PaymentDate: y.PaymentDate,
            InstallmentPaid: y.InstallmentPaid,
            BalanceAmount: y.InstallmentBalance
          };
        }));
      }
    }, error =>  {
      this.toastr.warning(error);
    });
  }
}
