import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/lib/models/table-actions-model';
import { AddLogisticItemsComponent } from '../add-logistic-items/add-logistic-items.component';
import { MatDialog } from '@angular/material';


@Component({
  selector: 'app-logistic-request-details',
  templateUrl: './logistic-request-details.component.html',
  styleUrls: ['./logistic-request-details.component.scss']
})
export class LogisticRequestDetailsComponent implements OnInit {

  requestedItemsHeaders$ = of([
    'Item',
    'Quantity',
    'Estimated Cost',
    'Availability'
  ]);
  requestedItemsData$ = of([
    {'Item': 'Item1', 'Quantity': '2', 'Estimated Cost': '8500' , 'Availability': '3'},
    {'Item': 'Item2', 'Quantity': '3', 'Estimated Cost': '9200' , 'Availability': '4'}
  ]);
  actions: TableActionsModel;
  constructor(private dialog: MatDialog) { }

  ngOnInit() {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        edit: true,
        delete: true,
        download: false,
      },
      subitems: {
      }

    };
  }
  addItemDialog() {
    const dialogRef = this.dialog.open(AddLogisticItemsComponent, {
      width: '300px'
      // data: {name: this.name, animal: this.animal}
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

}
