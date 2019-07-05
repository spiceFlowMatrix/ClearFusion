import { Component, OnInit, Input, EventEmitter, Output, ChangeDetectionStrategy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DeleteConfirmationComponent } from '../delete-confirmation/delete-confirmation.component';

@Component({
  selector: 'lib-listing-delete',
  templateUrl: './listing-delete.component.html',
  styleUrls: ['./listing-delete.component.css']

})
export class ListingDeleteComponent implements OnInit {
  //#region "variables"
  @Input() showDelete = false;
  @Input() tableHeaderList: string[] = [];
  @Input() tableContentList: any[] = [];

  @Output() deleteConfirm = new EventEmitter<any>();

  //#endregion

  constructor(public dialog: MatDialog) {}

  ngOnInit() {}

  confirmDeleteAction(data: any) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: {},
      autoFocus: false
    });

    dialogRef.componentInstance.confirmMessage = 'Are you Sure ?';
    dialogRef.componentInstance.confirmText = 'Confirm';
    dialogRef.componentInstance.cancelText = 'Cancel';

    dialogRef.componentInstance.confirmDelete.subscribe(res => {
      this.deleteConfirm.emit(data);
      dialogRef.componentInstance.onCancelPopup();
    });
  }
}
