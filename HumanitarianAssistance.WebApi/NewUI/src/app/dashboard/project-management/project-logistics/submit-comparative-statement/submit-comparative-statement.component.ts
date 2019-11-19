import { Component, OnInit, Inject } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

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
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  private dialogRef: MatDialogRef<SubmitComparativeStatementComponent>) { }

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

}
