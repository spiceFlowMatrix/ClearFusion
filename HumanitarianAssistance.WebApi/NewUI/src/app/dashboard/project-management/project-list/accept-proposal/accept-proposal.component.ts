import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {
  FormGroup,
  Validators,
  FormBuilder,
  FormControl
} from '@angular/forms';
import { UploadEvent, FileSystemFileEntry, UploadFile } from 'ngx-file-drop';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ProjectListService } from '../service/project-list.service';
import { IApproveRejectModel } from '../project-details/models/project-details.model';
import { ToastrService } from 'ngx-toastr';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { IWinLossProjectDetailModel } from '../models/projectList.model';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-accept-proposal',
  templateUrl: './accept-proposal.component.html',
  styleUrls: ['./accept-proposal.component.scss']
})
export class AcceptProposalComponent implements OnInit {
  approvalForm: FormGroup;

  //#region variables
  ProjectDescription: any;
  myGroup: FormGroup;
  IsAppRej: any;
  public files: UploadFile[] = [];
  // Projectid: number;
  imageUrl: any = null;
  data: any;
  winlossmodel: IWinLossProjectDetailModel;
  approvedProjectDetailLoader = false;
  // win/loss

  winLossImageUrl: any;

  commonLoaderFlag = false;
  commonWinLossFlag = false;
  disableApprovedButton = false;
  diableWinLossButton = false;
  // isApprovalRejectedflag: boolean = null;
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
    public projectListService: ProjectListService,
    private toastr: ToastrService
  ) {
    this.uploadUrl = this.appurl.getUploadDocUrl();
  }

  ngOnInit() {
    this.initForm();
    // this.Projectid = + this.routeActive.snapshot.paramMap.get('id');
    this.getApprovedProjectDetail(this.projectId);
    this.getProjectWinLossDetailById(this.projectId);
    this.initModel();
  }

  initForm() {
    this.approvalForm = new FormGroup({
      CommentText: new FormControl('', [Validators.required]),
      FileName: new FormControl('')
    });
  }
  initModel() {
    this.winlossmodel = {
      FileName: null,
      FilePath: null,
      WinLossMessage: '',
      WinLossFileName: '',
      WinlossFilePath: null,
      IsReviewApproved: null,
      IsProposalAccept: null
    };
  }
  //#region  click to emit event of approval to parent
  isApproved(text: any) {
    this.commonLoaderFlag = true;

    // ext != ".jpeg" && ext != ".png" && ext != ".jpg" && ext != ".gif" && ext != ".rtf"
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
    this.winlossmodel.FileName = this.files[0].relativePath;
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
  //#region upload review doc drag and drop
  public UploadReviewDragAndDropFile(event: UploadEvent) {
    this.files = event.files;
    if (/\.(gif|jpg|jpeg|tiff|png)$/i.test(this.files[0].relativePath)) {
      this.toastr.warning('File format is not correct.');
      return;
    }
    this.winlossmodel.FileName = this.files[0].relativePath;

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
      // to hide the anchor tag
      this.winlossmodel.IsReviewApproved = null;
      this.winlossmodel.FileName = null;
    }
  }

  buttonDisabled() {
    // if (this.rejectLoaderFlag || this.commonLoaderFlag) {
    if (
      this.commonLoaderFlag ||
      !this.approvalForm.valid ||
      this.disableApprovedButton === true
    ) {
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
      text: this.winlossmodel.WinLossMessage, // "CommentText" is inside the form
      flag: true,
      data: this.formData
    };
    this.winLossApproval.emit(obj);
  }

  isLoss() {
    this.commonWinLossFlag = true;
    const obj: IApproveRejectModel = {
      text: this.winlossmodel.WinLossMessage, // "CommentText" is inside the form
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
    this.winlossmodel.WinLossFileName = this.files[0].relativePath;
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
  public UploadWinLossDragAndDropFile(event: any) {
    this.files = event.files;

    if (/\.(gif|jpg|jpeg|tiff|png)$/i.test(this.files[0].relativePath)) {
      this.toastr.warning('File format is not correct.');
      return;
    }

    this.winlossmodel.WinLossFileName = this.files[0].relativePath;

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
    this.winlossmodel.WinLossFileName = '';
  }

  winlossButtonDisabled() {
    // (this.lossLoaderFlag || this.winLoaderFlag) &&
    if (
      this.winlossmodel.WinLossFileName === '' ||
      this.winlossmodel.WinLossMessage === '' ||
      this.commonWinLossFlag ||
      this.diableWinLossButton === true
    ) {
      return true;
    } else {
      return false;
    }
  }
  //#endregion

  //#region "getApprovedProjectDetail"
  getApprovedProjectDetail(projectId: number) {
    this.approvedProjectDetailLoader = true;
    if (projectId != null && projectId !== undefined) {
      this.projectListService
        .GetApprovalProjectDetailById(projectId)
        .subscribe(response => {
          if (response.data != null && response.statusCode === 200) {
            this.approvalForm.controls['CommentText'].setValue(
              response.data.CommentText
            );
            // to bind the filename value
            // this.FileName = response.data.FileName;
            this.winlossmodel.FileName = response.data.FileName;
            this.winlossmodel.FilePath = response.data.FilePath;
            // check if isapproved is true then only we disable the button
            this.winlossmodel.IsReviewApproved = response.data.IsApproved;
            this.winlossmodel.IsProposalAccept =
              response.data.IsProposalRejected;
            if (
              response.data.IsApproved === true ||
              response.data.IsProposalRejected === false // * the value from Project Proposal detail
            ) {
              this.disableApprovedButton = true;
            }
          }
          this.approvedProjectDetailLoader = false;
        },
        (error) => {
          this.approvedProjectDetailLoader = false;
          this.toastr.error('Someting went wrong');
        }
        );
    }
  }
  //#endregion

  //#region "getwinLossProjectDetail"
  getProjectWinLossDetailById(projectId: number) {
    if (projectId != null && projectId !== undefined) {
      this.projectListService
        .GetProjectWinLossDetailById(projectId)
        .subscribe(response => {
          if (response.data != null && response.statusCode === 200) {
            this.winlossmodel.WinLossMessage = response.data.CommentText;
            this.winlossmodel.WinLossFileName = response.data.FileName;
            this.winlossmodel.WinlossFilePath = response.data.FilePath;
            this.diableWinLossButton = true;
          }
        });
    }
  }
  //#endregion
  //#region "onDocumentClicked"
  onDocumentClicked(filePath: string) {
    this.documentDownload(filePath);
  }
  //#endregion
  documentDownload(objectName: string) {
    const objectData = {
      ObjectName: objectName
    };
    this.projectListService.GetProjectSignedUrl(objectData).subscribe(
      (data: IResponseData) => {
        if (data != null) {
          if (data.statusCode === 200 && data.data != null) {
            window.open(data.data, '_blank');
          } else if (data.statusCode === 200) {
            this.toastr.error(data.message);
          }
        }
      },
      error => {}
    );
  }
  //#endregion
}
