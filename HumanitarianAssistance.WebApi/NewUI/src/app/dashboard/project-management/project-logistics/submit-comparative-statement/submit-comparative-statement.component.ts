import { Component, OnInit, Inject } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { LogisticService } from '../logistic.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { FileSourceEntityTypes } from 'src/app/shared/enum';
import { takeUntil } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';

@Component({
  selector: 'app-submit-comparative-statement',
  templateUrl: './submit-comparative-statement.component.html',
  styleUrls: ['./submit-comparative-statement.component.scss']
})
export class SubmitComparativeStatementComponent implements OnInit {

  displayedColumns: string[] = ['select', 'Id', 'Supplier', 'Quantity', 'FinalPrice'];
  dataSource;
  itemdata;
  attachments: any[] = [];
  selection = new SelectionModel<any>(true, []);
  statementform: FormGroup;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  private dialogRef: MatDialogRef<SubmitComparativeStatementComponent>,
  private fb: FormBuilder,
  private commonLoader: CommonLoaderService,
  public toastr: ToastrService,
  private logisticservice: LogisticService,
  private globalSharedService: GlobalSharedService) { }

  ngOnInit() {
    this.itemdata = this.data.SupplierList.map(function(val) {
      return {
          Id: val.SupplierId,
          Supplier: val.SupplierName,
          Quantity: val.Quantity,
          FinalPrice: val.FinalPrice
      };
    });
    this.dataSource = new MatTableDataSource<any>(this.itemdata);
    this.statementform = this.fb.group({
      Description: ['', Validators.required]
    });
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  cancelSubmission() {
    this.dialogRef.close({data: null});
  }

  openInput() {
    document.getElementById('fileInput').click();
  }

  fileChange(file: any) {
    this.attachments.push(file);
  }

  SubmitStatement() {
    if (!this.statementform.valid) {
      return;
    }
    if (this.selection.selected.length === 0) {
      this.toastr.warning('Please Select Suppliers!');
      return;
    }
    this.commonLoader.showLoader();
    const supplierIds: any[] = [];
    this.selection.selected.forEach(element => {
      supplierIds.push(element.Id);
    });
    const model = {
      RequestId: this.data.RequestId,
      SupplierIds: supplierIds,
      Description: this.statementform.controls['Description'].value
    };
    this.logisticservice.SubmitComparativeStatement(model).subscribe(res => {
      if (res.StatusCode === 200) {
        let count = 1;
        this.attachments.forEach(element => {
          this.globalSharedService
                  .uploadFile(FileSourceEntityTypes.ComparativeStatement, this.data.RequestId, element[0])
                  .pipe(takeUntil(this.destroyed$))
                  .subscribe(y => {
                    if (count === this.attachments.length) {
                      this.dialogRef.close({data: 'Success'});
                      this.commonLoader.hideLoader();
                      this.toastr.success('Success');
                    } else {
                      count++ ;
                    }
                  });
        });
        if (this.attachments.length === 0) {
          this.dialogRef.close({data: 'Success'});
          this.commonLoader.hideLoader();
          this.toastr.success('Success');
        }
      } else {
        this.toastr.error('Something went wrong!');
        this.commonLoader.hideLoader();
        this.dialogRef.close({data: null});
      }
    });
  }
}
