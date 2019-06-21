import { Component, OnInit, Input, Output, EventEmitter, OnChanges, HostListener } from '@angular/core';
import { CategoryModel, ClientDetailsModel } from '../model/client.model';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ClientsService } from '../service/clients.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { Delete_Confirmation_Texts } from 'src/app/shared/enum';
import { MatDialog } from '@angular/material';
import { DeleteConfirmationComponent } from 'projects/library/src/lib/components/delete-confirmation/delete-confirmation.component';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrls: ['./client-details.component.scss']
})
export class ClientDetailsComponent implements OnInit, OnChanges {

  //#region "variables"
  categories: CategoryModel[];
  @Input() clientId: number;
  @Input() isEditingAllowed: boolean;
  @Output() deleteClient = new EventEmitter<any>();
  @Output() addClientList = new EventEmitter<any>();
  @Output() updateClientList = new EventEmitter<any>();
  @Output() hideDetailPanel = new EventEmitter<any>();
  archiveButton = false;
  clientDetails: ClientDetailsModel = {};
  clientDetailsForm;
  validateForm: boolean;
  selectedclientId: any;
  length?: number;
  // formErrors = {
  //   'clientName': '',
  //   'email':'',
  //   'phone':''
  // };
  // validationMessages = {
  //   'clientName': {
  //     'required': 'Client Name is required.',
  //     'minlength': 'Client Name must be at least 2 characters long.',
  //     'maxlength': 'Client Name cannot be more than 25 characters long.'
  //   },
  //   'email': {
  //     'required': 'Email is required.'
  //   },
  //   'phone': {
  //     'required': 'Phone Number is required.'
  //   }
  // };
  isSubmitted = false;

  // scroll
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  clientDetailsLoader = false;



  //#endregion

  // tslint:disable-next-line:max-line-length
  constructor(public dialog: MatDialog, public toastr: ToastrService, private commonLoaderService: CommonLoaderService, private appurl: AppUrlService, private clientsService: ClientsService) {
    this.getScreenSize();
   }

  ngOnChanges(): void {
    this.initForm();
    this.GetCategory();
    if (this.clientId !== 0 && this.clientId !== undefined) {
      this.archiveButton = true;
      this.GetClientDetailsById(this.clientId);
    } else {
      this.archiveButton = false;
      // this.CreateClientonAddNew();
      // this.clientDetails = {};
    }

    if (this.isEditingAllowed) {
      this.clientDetailsForm.disable();
    }
  }

  ngOnInit() {

  }


  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
      this.screenHeight = window.innerHeight;
      this.screenWidth = window.innerWidth;

      this.scrollStyles = {
        'overflow-y': 'auto',
        'height': this.screenHeight - 110 + 'px',
        'overflow-x': 'hidden'
        };
  }
  //#endregion

  CreateClientonAddNew() {
    this.clientDetails = {};
    this.clientsService.EditClient(this.clientDetails)
    .subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.commonLoaderService.hideLoader();
        this.toastr.success(result.message);
        this.clientDetails = {};
      this.clientDetails = result.data;
      this.addClientList.emit(this.clientDetails);
      this.selectedclientId = result.data.ClientId;
      this.archiveButton = true;
      } else {
        this.toastr.error('Some error occured.Please try again later.');
      }
    },
    error => {
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  initForm() {
    this.clientDetailsForm = new FormGroup({
      clientName: new FormControl(''),
      focalPoint: new FormControl(''),
      position: new FormControl(''),
      phone: new FormControl(''),
      email: new FormControl(''),
      category: new FormControl(''),
      history: new FormControl(''),
      physicialAddress: new FormControl(''),
      clientBackground: new FormControl(''),
    });
  }

ResetFormOnAddNewClient() {
  this.selectedclientId = 0;
  this.clientDetails = {};
  this.clientDetailsForm.reset();
}

  //#region "GetClientDetailsById"
  GetClientDetailsById(clientId: number) {
    this.clientDetailsLoader = true;
    this.selectedclientId = clientId;
    this.clientsService.GetClientById(clientId).subscribe((result:IResponseData) => {
    if (result.statusCode === 200) {
    this.clientDetails = result.data;
    this.selectedclientId = this.clientDetails.ClientId;
    this.clientDetailsForm.controls['clientName'].setValue(this.clientDetails.ClientName);
    this.clientDetailsForm.controls['focalPoint'].setValue(this.clientDetails.FocalPoint);
    this.clientDetailsForm.controls['position'].setValue(this.clientDetails.Position);
    this.clientDetailsForm.controls['phone'].setValue(this.clientDetails.Phone);
    this.clientDetailsForm.controls['email'].setValue(this.clientDetails.Email);
    this.clientDetailsForm.controls['category'].setValue(this.clientDetails.CategoryId);
    this.clientDetailsForm.controls['history'].setValue(this.clientDetails.History);
    this.clientDetailsForm.controls['physicialAddress'].setValue(this.clientDetails.PhysicialAddress);
    this.clientDetailsForm.controls['clientBackground'].setValue(this.clientDetails.ClientBackground);
      } else {
        this.toastr.error('Some error occured. Please try again later');
      }
      this.clientDetailsLoader = false;
    },
    error => {
      this.clientDetailsLoader = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }
  //#endregion

  //#region "GetCategory"
  GetCategory() {
    // tslint:disable-next-line:max-line-length
    this.clientsService.GetCategory().subscribe((result: IResponseData) => {
      if (result.statusCode === 200) {
        this.categories = result.data;
      }
    });
  }
  //#endregion

  //#region "DeleteClient"
  DeleteClient(id) {
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
    this.clientsService.DeleteClient(id).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.toastr.success(result.message);
        this.length = result.total;
        this.deleteClient.emit({ id: id });
        dialogRef.componentInstance.onCancelPopup();
        this.selectedclientId = 0;
      } else {
        this.toastr.error('Some error occured. Please try again later');
      }
      dialogRef.componentInstance.isLoading = false;
    },
    error => {
      dialogRef.componentInstance.isLoading = false;
      this.toastr.error('Some error occured. Please try again later');
    });
    });
  }
  //#endregion

  onChange(ev, data) {
    if (ev === 'clientName') {
      this.clientDetails.ClientName = data;
    }
    if (ev === 'category') {
      this.clientDetails.CategoryId = data;
    }
    if (ev === 'clientBackground') {
      this.clientDetails.ClientBackground = data;
    }
    if (ev === 'email') {
      this.clientDetails.Email = data;
    }
    if (ev === 'focalPoint') {
      this.clientDetails.FocalPoint = data;
    }
    if (ev === 'history') {
      this.clientDetails.History = data;
    }
    if (ev === 'phone') {
      this.clientDetails.Phone = data;
    }
    if (ev === 'physicialAddress') {
      this.clientDetails.PhysicialAddress = data;
    }
    if (ev === 'position') {
      this.clientDetails.Position = data;
    }
   // this.onValueChanged();
    if (this.clientDetailsForm.value.clientName === '' || this.clientDetailsForm.value.clientName === undefined) {
      this.validateForm = false;
    } else {
      if (this.clientDetails.ClientId === 0 || this.clientDetails.ClientId === undefined || this.clientDetails.ClientId === null) {
        this.AddNewClient();
      } else {
        this.EditClient();
      }
    }
  }

  AddNewClient() {
    
    this.clientsService.EditClient(this.clientDetails)
      .subscribe((result:IResponseData) => {
        if (result.statusCode === 200) {
          this.toastr.success(result.message);
          this.archiveButton = true;
          this.clientDetails = {};
          this.clientDetails = result.data;
          this.addClientList.emit(this.clientDetails);
        } else {
          this.toastr.error(result.message);
        }

      },
      error => {
        this.toastr.error('Some error occured. Please try again later');
      });
  }
  EditClient() {
    this.clientDetailsForm.disable();
    this.clientsService.EditClient(this.clientDetails)
      .subscribe((result:IResponseData) => {
        if (result.statusCode === 200) {
          this.toastr.success(result.message);
        this.clientDetails = {};
        this.clientDetails = result.data;
        this.updateClientList.emit(this.clientDetails);
        } else {
          this.toastr.error(result.message);
        }
        this.clientDetailsForm.enable();
      },
      error => {
        this.clientDetailsForm.enable();
        this.toastr.error('Some error occured. Please try again later');
      });
  }



  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion
  getErrorMessage() {
    return this.clientDetailsForm.clientName.hasError('required') ? 'You must enter a value' : '';
  }

 //Comment code by As 07-06-2019

  // onValueChanged(data?: any) {
  //   debugger;
  //   if (!this.clientDetailsForm) { return; }
  //   const form = this.clientDetailsForm;
  //   for (const field in this.formErrors) {
  //     if (this.formErrors.hasOwnProperty(field)) {
  //       this.formErrors[field] = '';
  //       const control = form.get(field);
  //       if (control && !control.valid) {
  //         const messages = this.validationMessages[field];
  //         for (const key in control.errors) {
  //           if (control.errors.hasOwnProperty(key)) {
  //             this.formErrors[field] += messages[key] + ' ';
  //           }
  //         }
  //       }
  //     }
  //   }
  // }

  DisplayFirstEntryOfFilteredList(data) {
    this.clientDetailsForm.controls['clientName'].setValue(data.ClientName);
    this.clientDetailsForm.controls['focalPoint'].setValue(data.FocalPoint);
    this.clientDetailsForm.controls['position'].setValue(data.Position);
    this.clientDetailsForm.controls['phone'].setValue(data.Phone);
    this.clientDetailsForm.controls['email'].setValue(data.Email);
    this.clientDetailsForm.controls['category'].setValue(data.CategoryId);
    this.clientDetailsForm.controls['history'].setValue(data.History);
    this.clientDetailsForm.controls['physicialAddress'].setValue(data.PhysicialAddress);
    this.clientDetailsForm.controls['clientBackground'].setValue(data.ClientBackground);
  }
}
