import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { IUserListModel } from '../../../models/project-details.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IProjectPeople } from '../../../models/project-people.model';

@Component({
  selector: 'app-listing-delete',
  templateUrl: './listing-delete.component.html',
  styleUrls: ['./listing-delete.component.scss']
})
export class ListingDeleteComponent implements OnInit {

  @Input() userList: IUserListModel[] = [];
  @Input() roleList: any[] = [];
  @Input() tableContentList: IProjectPeople[] = [];
  @Output() deleteConfirm = new EventEmitter<any>();
  @Output() editForm = new EventEmitter<any>();
  @Input() showEdit = true;
  @Input() showDelete = true;


  tableHeaderList: string[] = ['User Name', 'Role', 'Date Added'];


  constructor(public dialog: MatDialog, private fb: FormBuilder) { }

  ngOnInit() {
  }

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

  editAction(data: any) {
    data.IsDeleted = true;

  }

  onEditFormSubmit(data: IProjectPeople) {
    data.IsDeleted = !data.IsDeleted;
    this.editForm.emit(data);
  }

  onEditCancel(data: IProjectPeople) {
    data.IsDeleted = !data.IsDeleted;
  }

}
