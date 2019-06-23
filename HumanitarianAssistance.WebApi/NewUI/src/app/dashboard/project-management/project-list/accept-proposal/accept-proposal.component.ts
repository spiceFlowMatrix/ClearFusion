import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {
  FormGroup,
  Validators,
  FormBuilder
} from '@angular/forms';
import {
  UploadEvent,
  FileSystemFileEntry,
  UploadFile
} from 'ngx-file-drop';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ProjectListService } from '../service/project-list.service';
import { IApproveRejectModel } from '../project-details/models/project-details.model';

@Component({
  selector: 'app-accept-proposal',
  templateUrl: './accept-proposal.component.html',
  styleUrls: ['./accept-proposal.component.scss']
})
export class AcceptProposalComponent implements OnInit {
  approvalForm = this.fb.group({
    CommentText: ['', Validators.required],
    file: ['']
  });

  //#region variables
  ProjectDescription: any;
  myGroup: FormGroup;
  IsAppRej: any;
  public files: UploadFile[] = [];
  // Projectid: number;
  imageUrl: any = null;
  data: any;
  FileName: string;

  // win/loss
  winLossMessage: string;
  winLossFileName: string;
  winLossImageUrl: any;

  commonLoaderFlag = false;
  commonWinLossFlag = false;
  //#endregion

  //#region  input/output emit
  @Input() projectId: number;
  @Input() winProjectFlag: any;
  @Output() appovalData = new EventEmitter();
  @Output() rejectedApproval = new EventEmitter();

  @Output() winLossApproval = new EventEmitter<any>();

  uploadUrl: string;
  formData = new FormData();

  //#endregion
  constructor(
    private fb: FormBuilder,
    private appurl: AppUrlService,
    public projectListService: ProjectListService
  ) {
    this.uploadUrl = this.appurl.getUploadDocUrl();
  }

  ngOnInit() {
    // this.Projectid = + this.routeActive.snapshot.paramMap.get('id');
    this.FileName = null;
    this.winLossMessage = '';
    this.winLossFileName = '';
  }

  //#region  click to emit event of approval to parent
  isApproved(text: any) {
    this.commonLoaderFlag = true;
    // data = this.imageUrl;
    // this.appovalData.emit({ text, tr, data });
    const obj: IApproveRejectModel = {
      text: text.CommentText, // "CommentText" is inside the form
      flag: true,
      data: this.formData
    };

    this.appovalData.emit(obj);
  }

  isRejected(text: any) {
    this.commonLoaderFlag = true;
    const obj: IApproveRejectModel = {
      text: text.CommentText, // "CommentText" is inside the form
      flag: false,
      data: this.formData
    };

    this.rejectedApproval.emit(obj);
  }

  public fileOver(event) {}

  public fileLeave(event) {}

  public dropped(event: UploadEvent) {
    this.files = event.files;
    this.FileName = this.files[0].relativePath;
    const droppedFile = event.files[0];
    const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
    const reader = new FileReader();

    fileEntry.file(file => {
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imageUrl = reader.result;
      };
    });
  }

  // drag and drop review documents 27/03/2019
  //#region start proposal drag and drop
  public UploadReviewDragAndDropFile(event: UploadEvent) {
    this.files = event.files;
    this.FileName = this.files[0].relativePath;
    for (const droppedFile of event.files) {
      // Is it a file?
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
        fileEntry.file((file: File) => {
          // Here you can access the real file
          // You could upload it like this:
          this.formData = new FormData();
          this.formData.append('filesData', file, droppedFile.relativePath);
          this.formData.append('projectId', this.projectId.toString());
        });
      }
    }
  }
  //#endregion

  DeleteFile(data) {
    if (data != null) {
      this.FileName = null;
    }
  }

  buttonDisabled() {
    // if (this.rejectLoaderFlag || this.commonLoaderFlag) {
    if (this.commonLoaderFlag || !this.approvalForm.valid) {
      return true;
    } else {
      return false;
    }
  }

  //#region "win/loss"

  //#endregion click to emit event of win /loss to parent
  isWin() {
    this.commonWinLossFlag = true;
    const obj: IApproveRejectModel = {
      text: this.winLossMessage, // "CommentText" is inside the form
      flag: true,
      data: this.formData
    };
    this.winLossApproval.emit(obj);
  }

  isLoss() {
    this.commonWinLossFlag = true;
    const obj: IApproveRejectModel = {
      text: this.winLossMessage, // "CommentText" is inside the form
      flag: false,
      data: this.formData
    };
    this.winLossApproval.emit(obj);
  }
  //#region

  public winLossfileOver(event) {}

  public winLossFileLeave(event) {}

  public winLossDropped(event: UploadEvent) {
    this.files = event.files;
    this.winLossFileName = this.files[0].relativePath;
    const droppedFile = event.files[0];
    const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
    const reader = new FileReader();

    fileEntry.file(file => {
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.winLossImageUrl = reader.result;
      };
    });
  }

  // drag and drop review documents 27/03/2019
  //#region start proposal drag and drop
  public UploadWinLossDragAndDropFile(event: UploadEvent) {
    this.files = event.files;
    this.winLossFileName = this.files[0].relativePath;
    for (const droppedFile of event.files) {
      // Is it a file?
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
        fileEntry.file((file: File) => {
          // Here you can access the real file
          // You could upload it like this:
          this.formData = new FormData();
          this.formData.append('filesData', file, droppedFile.relativePath);
          this.formData.append('projectId', this.projectId.toString());
        });
      }
    }
  }
  //#endregion

  winLossDeleteFile() {
    this.winLossFileName = '';
  }

  winlossButtonDisabled() {
    // (this.lossLoaderFlag || this.winLoaderFlag) &&
    if (
      this.winLossFileName === '' ||
      this.winLossMessage === '' ||
      this.commonWinLossFlag
    ) {
      return true;
    } else {
      return false;
    }
  }
  //#endregion
}
