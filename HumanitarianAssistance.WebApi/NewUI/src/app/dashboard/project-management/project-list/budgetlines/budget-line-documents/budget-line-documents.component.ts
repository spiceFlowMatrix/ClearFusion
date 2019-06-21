import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BudgetLineService } from '../budget-line.service';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { BudgetLineImportPopupLoaderComponent } from '../budget-line-import-popup-loader/budget-line-import-popup-loader.component';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';

@Component({
  selector: 'app-budget-line-documents',
  templateUrl: './budget-line-documents.component.html',
  styleUrls: ['./budget-line-documents.component.scss']
})
export class BudgetLineDocumentsComponent implements OnInit {
  //#region "Input/Output"
  @Input() Projectid: number;
  @Output() budgetLineListRefresh = new EventEmitter();
  //#endregion
  headerText = '';
  selectedProject: number;
  budgetLineId: number;
  fileToUpload: File = null;
  dialogRef: any;

  constructor(
    public budgetService: BudgetLineService,
    public toastr: ToastrService,
    public commonLoaderService: CommonLoaderService,
    public dialog: MatDialog,
    private appurl: AppUrlService
  ) {}

  ngOnInit() {}

  //#region "handleFileInput"
  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.selectedProject = this.Projectid;
    this.uploadBudgetLineFile(this.fileToUpload, this.selectedProject);
  }

  //#endregion

  //#region "uploadBudgetLineFile"
  uploadBudgetLineFile(fileToUpload: File, projectId: any) {
    const formData = new FormData();
    formData.append('fileKey', fileToUpload, fileToUpload.name);
    formData.append('projectId', projectId);
    this.uploadFile(formData);
  }

  uploadFile(data: any) {
    this.openBudgetLineDialog();
    // this.commonLoaderService.showLoader();
    this.budgetService.postBudgetLineDocument(data).subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200) {
          this.toastr.success('Excel import successfully');
          this.budgetLineListRefresh.emit(response.statusCode);
        } else if (response.statusCode === 4440) {
          this.toastr.warning('Please check the format');
        } else if (response.statusCode === 120) {
          this.toastr.warning(
            'Please provide correct ProjectId to import excel.'
          );
        } else if (response.statusCode === 400) {
          this.toastr.warning(
            'Please provide correct format to import excel.'
          );
        }

        this.dialogRef.componentInstance.onClosePopup();
      },
      error => {
        console.log(error);
        this.dialogRef.componentInstance.onClosePopup();
        this.toastr.error('Something went Wrong. Please try again');
      }
    );
  }

  //#region "openBudgetLineDialog"
  openBudgetLineDialog(): void {
    this.dialogRef = this.dialog.open(BudgetLineImportPopupLoaderComponent, {
      disableClose: true,
      width: '550px',
      data: {}
    });
  }

  //#endregion

  //#region "DownloadExcelFormat"
  DownloadExcelFormat() {
   window.open(this.appurl.getApiUrl() + GLOBAL.API_BudgetLine_CreateAndDownloadExcelFormat);
  }
  //#endregion
}
