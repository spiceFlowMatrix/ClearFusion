import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { IBudgetLineDataSource } from '../addbudget-line/budget-line-datasource.model';

@Component({
  selector: 'app-budget-line-import-popup-loader',
  templateUrl: './budget-line-import-popup-loader.component.html',
  styleUrls: ['./budget-line-import-popup-loader.component.scss']
})
export class BudgetLineImportPopupLoaderComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA)public data: IBudgetLineDataSource,
    public dialogRef: MatDialogRef<BudgetLineImportPopupLoaderComponent>
  ) { }

  ngOnInit() {
  }

  onClosePopup() {
    this.dialogRef.close();
  }

}
