import {
  Component,
  OnInit,
  EventEmitter,
  Inject
} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'lib-delete-confirmation',
  templateUrl: './delete-confirmation.component.html',
  styleUrls: ['./delete-confirmation.component.css']
})
export class DeleteConfirmationComponent implements OnInit {
  confirmDelete = new EventEmitter();
  cancelDelete = new EventEmitter();
  confirmMessage = '';
  confirmText = '';
  cancelText = '';
  isLoading = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DataSources,
    public dialogRef: MatDialogRef<DeleteConfirmationComponent>
  ) {}

  ngOnInit() {}

  confirmAction() {
    this.confirmDelete.emit(true);
  }

  onCancelPopup(): void {
    this.dialogRef.close();
  }
}

interface DataSources {
  data: any;
}
