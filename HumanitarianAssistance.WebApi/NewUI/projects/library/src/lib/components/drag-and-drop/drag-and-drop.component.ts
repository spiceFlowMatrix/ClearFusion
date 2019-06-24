import { Component, OnInit, EventEmitter, Output, ChangeDetectionStrategy } from '@angular/core';
import { UploadEvent, FileSystemFileEntry } from 'ngx-file-drop';
import { FileManagementModel } from 'src/app/shared/file-management/file-management-model';

@Component({
  selector: 'lib-drag-and-drop',
  templateUrl: './drag-and-drop.component.html',
  styleUrls: ['./drag-and-drop.component.css']

})
export class DragAndDropComponent implements OnInit {

  @Output() uploadFileEmit = new EventEmitter<any>();

  constructor() { }

  ngOnInit() {
  }


  public onFileDropped(event: UploadEvent) {

    for (const droppedFile of event.files) {
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;

        fileEntry.file((file: File) => {
          const fileModel: FileManagementModel = {
            File: '',
            FileLength: 120,
            FileName: droppedFile.relativePath,
            FileType: 'doc'
          };

          const formData = new FormData();
          formData.append('fileData', file, droppedFile.relativePath);
          // formData.append('activityId', 'ssss');
          // formData.append('statusId', 'sd');

          // emit
          this.uploadFileEmit.emit(fileModel);
        });
      }
    }
  }
  //#endregion

  onFileOver(event: any) {

  }

  onFileLeave(event: any) {

  }
}
