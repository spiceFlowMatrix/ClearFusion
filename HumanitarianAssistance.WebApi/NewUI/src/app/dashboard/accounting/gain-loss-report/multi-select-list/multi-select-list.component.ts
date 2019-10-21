import { ShowHideDropdownEnum } from 'src/app/shared/enum';
import { IOfficeList, IJournalList, IProjectList } from '../gain-loss-report.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EventEmitter, OnInit, Inject, Component } from '@angular/core';

@Component({
  selector: 'app-multi-select-list',
  templateUrl: './multi-select-list.component.html',
  styleUrls: ['./multi-select-list.component.scss']
})
export class MultiSelectListComponent implements OnInit {

  officeItemAddRemove = new EventEmitter<IOfficeList>();
  journalItemAddRemove = new EventEmitter<IJournalList>();
  projectItemAddRemove = new EventEmitter<IProjectList>();
  office = ShowHideDropdownEnum.Office;
  journal = ShowHideDropdownEnum.Journal;
  project = ShowHideDropdownEnum.Project;

  constructor(
    public dialogRef: MatDialogRef<MultiSelectListComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DataSources
  ) { }

  ngOnInit() {
  }

  //#region "onOfficeItemClick"
  onOfficeItemClick(e: IOfficeList) {
    this.officeItemAddRemove.emit(e);
  }
  //#endregion

  //#region "onJournalItemClick"
  onJournalItemClick(e: IJournalList) {
    this.journalItemAddRemove.emit(e);
  }
  //#endregion

  //#region "onProjectItemClick"
  onProjectItemClick(e: IProjectList) {
    this.projectItemAddRemove.emit(e);
  }
  //#endregion

}

interface DataSources {
  data: any;
  showHideDropdown: number;
  selectedValues: number[];
}
