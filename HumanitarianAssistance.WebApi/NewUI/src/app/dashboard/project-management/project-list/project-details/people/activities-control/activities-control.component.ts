import { Component, OnInit, Input, OnDestroy, Output, EventEmitter } from '@angular/core';
import { IUserListModel, IAddPeoplePermissionModel } from '../../models/project-details.model';
import { ActivatedRoute } from '@angular/router';
import { ProjectListService } from '../../../service/project-list.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { IProjectPeople } from '../../models/project-people.model';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-activities-control',
  templateUrl: './activities-control.component.html',
  styleUrls: ['./activities-control.component.scss']
})
export class ActivitiesControlComponent implements OnInit, OnDestroy {

  @Input() userList: IUserListModel[] = [];
  @Input() roleList: any[] = [];
  @Output() invokePeoplePermissionChange = new EventEmitter();


  projectId: number;
  tableContentList: IProjectPeople[] = [];
  activitiesAddFlag = false;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    private routeActive: ActivatedRoute,
    private projectListService: ProjectListService,
    private toastr: ToastrService
    ) {
    this.routeActive.parent.params
      .subscribe(params => {
      this.projectId = +params['id'];
    });
  }

  ngOnInit() {
    this.getActivityControl();
  }

  //#region "getActivityControl"
  getActivityControl() {

    this.projectListService.GetActivityControl(this.projectId)
    .pipe(takeUntil(this.destroyed$))
    .subscribe((response: IResponseData) => {
      if (response.data != null) {
        response.data.forEach((element: IProjectPeople) => {
          this.tableContentList.push({
            Id: element.Id,
            ProjectId: element.ProjectId,
            UserId: element.UserId,
            RoleId: element.RoleId,
            DateAdded: StaticUtilities.setLocalDate(element.DateAdded)
          });
        });
      }
    },
    (error) => {
      // error
    });
  }
  //#endregion

  //#region "onAddForm"
  onAddForm(data: IAddPeoplePermissionModel) {

    this.activitiesAddFlag = !this.activitiesAddFlag;

    const controlData: IProjectPeople = {
      Id: null,
      ProjectId: this.projectId,
      RoleId: data.RoleId,
      UserId: data.UserId,
      DateAdded: new Date()
    };
    console.log(controlData);

    this.projectListService.AddUserForActivitiesControl(controlData)
    .pipe(takeUntil(this.destroyed$))
    .subscribe((response: IResponseData) => {
      if (response.data !== 0 && response.statusCode === 200) {
        console.log(response.data);
        this.tableContentList.push({
          Id: response.data,
          ProjectId: controlData.ProjectId,
          RoleId: controlData.RoleId,
          UserId: controlData.UserId,
          DateAdded: controlData.DateAdded
        });
        this.invokePeoplePermissionChange.emit();
      } else if (response.statusCode === 400) {
        this.toastr.error(response.message);
      }
    },
    (error) => {
      // error
    });
  }
  //#endregion

  //#region "editForm"
  editForm(data: IProjectPeople) {
    const controlData: IProjectPeople = {
      Id: data.Id,
      ProjectId: this.projectId,
      RoleId: data.RoleId,
      UserId: data.UserId,
      DateAdded: new Date()
    };
    console.log(controlData);
    this.projectListService
      .EditUserForActivitiesControl(controlData)
      .pipe(takeUntil(this.destroyed$))
      .subscribe((response: IResponseData) => {
        if (response.statusCode === 200) {
          this.invokePeoplePermissionChange.emit();
        } else if (response.statusCode === 400) {
        this.toastr.error(response.message);
      }
    }, error => {});
  }
  //#endregion

  //#region "deleteConfirm"
  deleteConfirm(data: IProjectPeople) {

    const controlData: number = data.Id;
    const index = this.tableContentList.findIndex(x => x.Id === data.Id);

    this.projectListService
      .DeleteUserForActivitiesControl(controlData)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
            this.tableContentList.splice(index, 1);
            this.invokePeoplePermissionChange.emit();
        },
        error => {
          // error
        });
  }
  //#endregion



  ngOnDestroy() {

    this.destroyed$.next(true);
    this.destroyed$.complete();

  }
}
