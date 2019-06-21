import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import {
  IUserListModel,
  IAddPeoplePermissionModel
} from '../../models/project-details.model';
import { ActivatedRoute } from '@angular/router';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { ProjectListService } from '../../../service/project-list.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { IProjectPeople } from '../../models/project-people.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-opportunity-control',
  templateUrl: './opportunity-control.component.html',
  styleUrls: ['./opportunity-control.component.scss']
})
export class OpportunityControlComponent implements OnInit, OnDestroy {
  @Input() userList: IUserListModel[] = [];
  @Input() roleList: any[] = [];

  projectId: number;
  tableContentList: IProjectPeople[] = [];
  opportunityAddFlag = false;

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(
    private routeActive: ActivatedRoute,
    private projectListService: ProjectListService,
    private toastr: ToastrService
  ) {
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
  }

  ngOnInit() {
    this.getOpportunityControl();
  }

  //#region "getOpportunityControl"
  getOpportunityControl() {
    const projectId = this.projectId;

    this.projectListService
      .GetOpportunityControl(projectId)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.data != null) {
            response.data.forEach((element: IProjectPeople) => {
              this.tableContentList.push({
                Id: element.Id,
                ProjectId: element.ProjectId,
                UserId: element.UserId,
                RoleId: element.RoleId,
                DateAdded: StaticUtilities.setLocalDate(element.DateAdded),
                IsDeleted: false
              });
            });
          }
        },
        error => {
          // error
        }
      );
  }
  //#endregion

  //#region "onAddForm"
  onAddForm(data: IAddPeoplePermissionModel) {
    this.opportunityAddFlag = !this.opportunityAddFlag;

    const controlData: IProjectPeople = {
      Id: null,
      ProjectId: this.projectId,
      RoleId: data.RoleId,
      UserId: data.UserId,
      DateAdded: new Date()
    };

    this.projectListService
      .AddUserForOpportunityControl(controlData)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          if (response.data != null && response.statusCode === 200) {

            this.tableContentList.push({
              Id: response.data,
              ProjectId: controlData.ProjectId,
              RoleId: controlData.RoleId,
              UserId: controlData.UserId,
              DateAdded: controlData.DateAdded
            });
          } else if (response.statusCode === 400) {
            this.toastr.error(response.message);
          }
        },
        error => {
          // error
        }
      );
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
    this.projectListService
      .EditUserForOpportunityControl(controlData)
      .pipe(takeUntil(this.destroyed$))
      .subscribe((response: IResponseData) => {
        if (response.statusCode === 400) {
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
      .DeleteUserForOpportunityControl(controlData)
      .pipe(takeUntil(this.destroyed$))
      .subscribe(
        (response: IResponseData) => {
          this.tableContentList.splice(index, 1);
        },
        error => {
          // error
        }
      );
  }
  //#endregion

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
