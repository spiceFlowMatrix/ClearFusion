import { Component, OnInit, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-add-fine',
  templateUrl: './add-fine.component.html',
  styleUrls: ['./add-fine.component.scss']
})
export class AddFineComponent implements OnInit {
  onAddFineRefresh = new EventEmitter();
  constructor(
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AddFineComponent>,
  ) { }

  ngOnInit() {
  }

//#region "Add Fine Refresh"
AddFineRefresh() {
  this.onAddFineRefresh.emit();
}
//#endregion
//#region "onCancelPopup"
onCancelPopup(): void {
  this.dialogRef.close();
}
//#endregion
onNoClick(): void {
  this.dialogRef.close();
}
}
