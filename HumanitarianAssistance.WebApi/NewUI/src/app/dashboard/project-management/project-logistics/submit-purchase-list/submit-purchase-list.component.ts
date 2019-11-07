import { Component, OnInit, Inject } from '@angular/core';
import { MatTableDataSource, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { element } from '@angular/core/src/render3';

@Component({
  selector: 'app-submit-purchase-list',
  templateUrl: './submit-purchase-list.component.html',
  styleUrls: ['./submit-purchase-list.component.scss']
})
export class SubmitPurchaseListComponent implements OnInit {
  itemdata;
  displayedColumns: string[] = ['select', 'Id', 'Items', 'Quantity', 'EstimatedCost'];
  dataSource;
  selection = new SelectionModel<any>(true, []);
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  private dialogRef: MatDialogRef<SubmitPurchaseListComponent>) { }

  ngOnInit() {
    this.itemdata = this.data.requestedItems.map(function(val) {
      return {
          Id: val.Id,
          Items: val.Item,
          Quantity: val.Quantity,
          EstimatedCost: val.EstimatedCost
      };
    });
    this.dataSource = new MatTableDataSource<any>(this.itemdata);
    // console.log(this.itemdata);
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

  submitPurchase() {
    if (this.selection.selected.length === 0) {
      return false;
    } else {
      this.dialogRef.close({data: this.selection.selected});
    }
  }

  cancelSubmission() {
    this.dialogRef.close({data: null});
  }
}
