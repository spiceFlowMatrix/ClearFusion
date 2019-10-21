import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ActivityTypeModel } from '../../../contracts/model/contract-details.model';
import { MasterPageServiceService } from '../../service/master-page-service.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Component({
  selector: 'app-activitytype-details',
  templateUrl: './activitytype-details.component.html',
  styleUrls: ['./activitytype-details.component.scss']
})
export class ActivitytypeDetailsComponent implements OnInit {
  @Input() activityTypeId: number;
  @Output() hideDetailPanel = new EventEmitter<any>();
  @Output() deleteActivity = new EventEmitter<any>();
  @Output() addActivity = new EventEmitter<any>();
  @Output() updateActivity = new EventEmitter<any>();
  activityDetailsForm;
  archiveButton = false;
  activityDetail: ActivityTypeModel = {};
  activityTypeDetailsLoaderFlag = false;
  // tslint:disable-next-line:max-line-length
  constructor( public toastr: ToastrService, private commonLoaderService: CommonLoaderService, private activityService: MasterPageServiceService, private appurl: AppUrlService) { }

  // tslint:disable-next-line:use-life-cycle-interface
  ngOnChanges(): void {
    this.initForm();
    // this.GetCategory();
    if (this.activityTypeId !== 0 && this.activityTypeId !== undefined) {
      this.archiveButton = true;
      this.GetActivityId(this.activityTypeId);
    } else {
      this.archiveButton = false;
    }
  }

  initForm() {
    this.activityDetailsForm = new FormGroup({
      activityName: new FormControl('', [Validators.required])
    });
  }

  ResetFormOnAddNewActivity() {
    this.activityDetail = {};
    this.activityDetailsForm.reset();
  }

  ngOnInit() {
  }

  GetActivityId(id) {
    this.activityTypeDetailsLoaderFlag = true;
    // tslint:disable-next-line:max-line-length
    this.activityService.GetActivityById(id).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.activityDetail = result.data;
        this.activityDetailsForm = new FormGroup({
          activityName: new FormControl(this.activityDetail.ActivityName, [Validators.required])
        });
      } else {
        this.toastr.error('Some error occured.Please try again later');
      }
      this.activityTypeDetailsLoaderFlag = false;
    },
    error => {
      // this.commonLoaderService.hideLoader();
      this.activityTypeDetailsLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

  onChange(value) {
    this.activityDetail.ActivityName = value;
    if (this.activityTypeId === 0 || this.activityTypeId === undefined || this.activityTypeId === null) {
      this.CreateActivity();
    } else {
      this.EditActivity();
    }
  }

  CreateActivity() {
    // tslint:disable-next-line:max-line-length
    this.activityService.AddActivity(this.activityDetail).subscribe((result:IResponseData) => {
      if(result.statusCode === 200) {
        this.activityDetail = result.data;
        this.addActivity.emit(this.activityDetail);
        this.archiveButton = true;
      }
    });
  }

  EditActivity() {
    // tslint:disable-next-line:max-line-length
    this.activityService.AddActivity(this.activityDetail).subscribe((result:IResponseData) => {
     if (result.statusCode === 200) {
      this.activityDetail = result.data.activityById;
      this.updateActivity.emit(this.activityDetail);
     }
    });
  }

  //#region "emit"
  onHideDetailPanel() {
    this.hideDetailPanel.emit();
  }
  //#endregion

  DeleteActivity(id) {
    // tslint:disable-next-line:max-line-length
    this.activityService.DeleteActivity(id).subscribe((result:IResponseData) => {
      if (result.statusCode === 200) {
        this.deleteActivity.emit({ id: id });
      }
    });
  }
}
