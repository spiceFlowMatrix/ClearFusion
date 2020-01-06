import { Component, OnInit, Inject } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { LogisticService } from '../logistic.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { FileSourceEntityTypes } from 'src/app/shared/enum';
import { takeUntil, map } from 'rxjs/operators';
import { ReplaySubject, of, Observable } from 'rxjs';
import { IOpenedChange } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { TableActionsModel } from 'projects/library/src/public_api';

@Component({
  selector: 'app-submit-comparative-statement',
  templateUrl: './submit-comparative-statement.component.html',
  styleUrls: ['./submit-comparative-statement.component.scss']
})
export class SubmitComparativeStatementComponent implements OnInit {

  displayedColumns: string[] = ['select', 'Id', 'Supplier', 'Quantity', 'FinalPrice'];
  dataSource;
  storeSource;
  attachments: any[] = [];
  // selection = new SelectionModel<any>(true, []);
  statementform: FormGroup;
  docHeaders$ = of([
    'Id',
    'File Name'
  ]);
  docActions: TableActionsModel;
  docData$: Observable<any>;
  hideDocColums;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  private dialogRef: MatDialogRef<SubmitComparativeStatementComponent>,
  private fb: FormBuilder,
  private commonLoader: CommonLoaderService,
  public toastr: ToastrService,
  private logisticservice: LogisticService,
  private globalSharedService: GlobalSharedService) { }

  ngOnInit() {
    this.storeSource = this.data.SupplierList.map(function(val) {
      return {
          Id: val.SupplierId,
          Name: val.SourceCode + '-' + val.StoreSourceDescription + ' Item:' + val.ItemCode + '-' +
          val.ItemName + ' ' + val.CurrencyCode + ' ' + val.FinalUnitPrice
      };
    });
    this.hideDocColums = of({
      headers: ['File Name'],
      items: ['FileName']
    });
    this.docActions = {
      items: {
        button: { status: false, text: '' },
        edit: false,
        delete: true,
        download: false,
      },
      subitems: {
      }

    };
    // console.log(this.data.SupplierList);
    // this.dataSource = new MatTableDataSource<any>(this.itemdata);
    this.statementform = this.fb.group({
      Description: ['', Validators.required],
      StoreSourceCode: [[], Validators.required]
    });
  }

  // /** Whether the number of selected elements matches the total number of rows. */
  // isAllSelected() {
  //   const numSelected = this.selection.selected.length;
  //   const numRows = this.dataSource.data.length;
  //   return numSelected === numRows;
  // }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  // masterToggle() {
  //   this.isAllSelected() ?
  //       this.selection.clear() :
  //       this.dataSource.data.forEach(row => this.selection.select(row));
  // }

  // checkboxLabel(row?: any): string {
  //   if (!row) {
  //     return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
  //   }
  //   return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  // }

  cancelSubmission() {
    this.dialogRef.close({data: null});
  }

  openInput() {
    document.getElementById('fileInput').click();
  }

  fileChange(file: any) {
    this.attachments.push(file);
    this.docData$ = of(this.attachments).pipe(
      map(r => r.map((v, i) => ({
        Id: i,
        FileName: v[0].name,
       }) )));
  }

  onDocActionClick(event) {
    if (event.type === 'delete') {
      this.logisticservice.openDeleteDialog().subscribe(v => {
        if (v) {
          const index = event.item.Id;
          if (index !== -1) {
            this.attachments.splice(index, 1);
            this.docData$ = of(this.attachments).pipe(
              map(r => r.map((x, i) => ({
                Id: i,
                FileName: x[0].name,
               }) )));
          }
        }
      });
    }
  }

  get storeSourceCodes() {
    return this.statementform.get('StoreSourceCode').value;
  }

  onOpenedStoreSourceChange(event: IOpenedChange) {
    this.statementform.controls['StoreSourceCode'].setValue(event.Value);
  }

  SubmitStatement(value) {
    if (!this.statementform.valid) {
      this.toastr.warning('Please fill all required fields!');
      return;
    }
    this.commonLoader.showLoader();
    const model = {
      RequestId: this.data.RequestId,
      SupplierIds: value.StoreSourceCode,
      Description: value.Description
    };
    console.log(model);
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
