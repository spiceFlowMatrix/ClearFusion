import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  HostListener
} from '@angular/core';
import { Router } from '@angular/router';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DonorDetailModel } from './Models/donar-detail.model';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { ProjectListService } from '../../project-list/service/project-list.service';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'app-donor-master',
  templateUrl: './donor-master.component.html',
  styleUrls: ['./donor-master.component.scss']
})
export class DonorMasterComponent implements OnInit {
  //#region
  projectList: any[];
  donarDetail: DonorDetailModel = {};
  service: Subscription;
  donorForm: FormGroup;
  donorList: any[];
  subscription = new Subscription();
  totalcount: number;
  //#endregion

  //#region Input/Output emit
  @Input() donorId: number;
  @Input() isEditingAllowed: boolean;
  @Output() hideDetailPanel = new EventEmitter<any>();
  @Output() deleteDonor = new EventEmitter<any>();
  @Output() addDonor = new EventEmitter<any>();
  @Output() updateDonor = new EventEmitter<any>();
  archiveButton = false;
  counter = 0;

  // flag
  deletePopupFlag = true;
  // LoaderFlag
  donordetailLoaderFlag = false;
  updateRecordLoader = false;

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  //#endregion

  //#region Constructor
  constructor(
    public router: Router,
    public dialog: MatDialog,
    private fb: FormBuilder,
    public projectListService: ProjectListService,
    private appurl: AppUrlService,
    public toastr: ToastrService
  ) {
    this.getScreenSize();
  }
  //#endregion

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    this.initForm();
    // this.GetCategory();
    if (this.donorId !== 0 && this.donorId !== undefined) {
      this.archiveButton = true;
      this.GetDonarDetailById(this.donorId);
    } else {
      this.archiveButton = false;
    }
  }

  initForm() {
    this.donorForm = this.fb.group({
      DonorId: '',
      Name: ['', Validators.required],
      ContactDesignation: ['', Validators.required],
      ContactPerson: ['', Validators.required],
      ContactPersonEmail: ['', [Validators.required, Validators.email]],
      ContactPersonCell: ['', Validators.required]
    });
  }

  ngOnInit() {}

  //#region  method to reset the form values for  both add and edit
  formReset() {
    this.donorId = 0;
    this.donorForm.reset();
    this.donarDetail = {};
  }
  //#endregion

  //#region Get Donar Detailby id
  GetDonarDetailById(donarId: number) {
    this.donordetailLoaderFlag = true;
    this.donarDetail = {};
    if (donarId != null && donarId !== undefined && donarId !== 0) {
      this.projectListService
        .GetDonarDetailsByDonarId(
          this.appurl.getApiUrl() + GLOBAL.API_Project_GetDonarListById,
          donarId
        )
        .subscribe(
          data => {
            if (data != null) {
              if (data.data.DonorDetailById != null) {
                this.donorForm.setValue({
                  DonorId: data.data.DonorDetailById.DonorId,
                  Name:
                    data.data.DonorDetailById.Name === undefined
                      ? null
                      : data.data.DonorDetailById.Name,
                  ContactPerson:
                    data.data.DonorDetailById.ContactPerson === undefined
                      ? null
                      : data.data.DonorDetailById.ContactPerson,
                  ContactDesignation:
                    data.data.DonorDetailById.ContactDesignation === undefined
                      ? null
                      : data.data.DonorDetailById.ContactDesignation,
                  ContactPersonEmail:
                    data.data.DonorDetailById.ContactPersonEmail === undefined
                      ? null
                      : data.data.DonorDetailById.ContactPersonEmail,
                  ContactPersonCell:
                    data.data.DonorDetailById.ContactPersonCell === undefined
                      ? null
                      : data.data.DonorDetailById.ContactPersonCell
                });
              }
            }
            this.donordetailLoaderFlag = false;
            this.donarDetail.DonorId = data.data.DonorDetailById.DonorId;
            this.donarDetail.Name = data.data.DonorDetailById.Name;
            this.donarDetail.ContactPerson =
              data.data.DonorDetailById.ContactPerson;
            this.donarDetail.ContactPersonEmail =
              data.data.DonorDetailById.ContactPersonEmail;
            this.donarDetail.ContactPersonCell =
              data.data.DonorDetailById.ContactPersonCell;
            this.donarDetail.ContactDesignation =
              data.data.DonorDetailById.ContactDesignation;
          },
          error => {
            this.donordetailLoaderFlag = false;
            this.toastr.error('Something Went Wrong..! Please Try Again');
          }
        );
    }
  }
  //#endregion

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion

  //#region delete donar datail
  onDeleteDonor() {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '300px',
      height: '250px',
      data: 'delete',
      disableClose: false
    });

    dialogRef.componentInstance.confirmMessage =
      Delete_Confirmation_Texts.deleteText1;

    dialogRef.componentInstance.confirmText =
      Delete_Confirmation_Texts.yesText;

    dialogRef.componentInstance.cancelText =
      Delete_Confirmation_Texts.noText;

    dialogRef.afterClosed().subscribe(result => {
    });
    dialogRef.componentInstance.confirmDelete.subscribe(res => {
      dialogRef.componentInstance.isLoading = true;
      if (
        this.donorId != null &&
        this.donorId !== undefined &&
        this.donorId !== 0
      ) {
        this.projectListService
          .DeleteDonorDetail(
            this.appurl.getApiUrl() + GLOBAL.API_Project_DeleteDonorDetails,
            this.donorId
          )
          .subscribe(response => {
            if (response.StatusCode === 200) {
              this.totalcount = response.data.TotalCount;
              this.deleteDonor.emit({ id: this.donorId, count: this.totalcount });
              dialogRef.componentInstance.onCancelPopup();
              this.toastr.success('Donor detail deleted successfully');

            }
            dialogRef.componentInstance.isLoading = false;
          },
        error => {
          this.toastr.error('Someting went wrong');
          dialogRef.componentInstance.isLoading = false;
        });
      }
    });
  }

  //#endregion

  onChange(ev, value) {
    if (ev === 'name') {
      this.donarDetail.Name = value;
    }
    if (ev === 'designation') {
      this.donarDetail.ContactDesignation = value;
    }
    if (ev === 'email') {
      this.donarDetail.ContactPersonEmail = value;
    }
    if (ev === 'cName') {
      this.donarDetail.ContactPerson = value;
    }
    if (ev === 'cCell') {
      this.donarDetail.ContactPersonCell = value;
    }
    if (
      this.donorId === 0 ||
      this.donorId === undefined ||
      this.donorId === null
    ) {
      this.CreateDonor();
    } else {
      this.EditDonor();
    }
  }

  CreateDonor() {
    this.updateRecordLoader = true;

    this.donarDetail.DonorId = 0;
    this.projectListService
      .AddDonorDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditDonorDetails,
        this.donarDetail
      )
      .subscribe(
        response => {
          if (response.data.StatusCode === 200) {
            this.donarDetail = response.data.DonorDetailById;
            this.donorId = response.data.DonorDetailById.DonorId;
            this.addDonor.emit(this.donarDetail);
            this.archiveButton = true;
          }
          this.updateRecordLoader = false;
        },
        error => {
          this.updateRecordLoader = false;
          this.toastr.error('Something Went Wrong. Please try again..!');
        }
      );
  }

  CreateDonoronAddNew() {
    this.donarDetail = {};
    this.donarDetail.DonorId = 0;
    this.projectListService
      .AddDonorDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditDonorDetails,
        this.donarDetail
      )
      .subscribe(response => {
        this.donarDetail = response.data.DonorDetailById;
        this.donorId = response.data.DonorDetailById.DonorId;
        this.donarDetail.Count = response.data.TotalCount;
        this.addDonor.emit(this.donarDetail);
        this.archiveButton = true;
      });
  }

  EditDonor() {
    // tslint:disable-next-line:max-line-length
    this.updateRecordLoader = true;

    this.projectListService
      .AddDonorDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditDonorDetails,
        this.donarDetail
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            this.donarDetail = response.data.DonorDetailById;
            this.updateDonor.emit(this.donarDetail);
            this.toastr.success('Donor details updated successfully');
          }
          this.updateRecordLoader = false;
        },
        error => {
          this.updateRecordLoader = false;
          this.toastr.error('Some error occured. Please try again later.');
        }
      );
  }
  //#endregion

  //#region "onDelete"
  onDelete() {
    this.deletePopupFlag = !this.deletePopupFlag;
  }
  //#endregion
}
