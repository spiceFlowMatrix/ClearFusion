import { Component, OnInit, Input } from '@angular/core';
import { HiringRequestsService } from './hiring-requests.service';
import { takeUntil } from 'rxjs/operators';
import { ProjectListService } from '../service/project-list.service';
import { ReplaySubject } from 'rxjs';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-hiring-requests',
  templateUrl: './hiring-requests.component.html',
  styleUrls: ['./hiring-requests.component.scss']
})
export class HiringRequestsComponent implements OnInit {

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  projectId: number;

  constructor(
    private toastr: ToastrService,
    public projectListService: ProjectListService,
    public hiringRequestsService: HiringRequestsService,
    private routeActive: ActivatedRoute
  ) { }

  ngOnInit() {
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.getHiringControlPermission();
  }

//#region "getActivitiesControlPermission"
getHiringControlPermission() {
  this.projectListService
    .GetHiringControl(this.projectId)
    .pipe(takeUntil(this.destroyed$))
    .subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data != null) {

          // // console.log(response.data);
          this.hiringRequestsService.setHiringPermissions(response.data);

        } else {
          this.toastr.error(response.message);
        }

      },
      error => {

      }
    );
}
//#endregion
}
