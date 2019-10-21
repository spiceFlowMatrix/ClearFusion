import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { IFilterModel, HiringList } from '../models/hiring-requests-models';
import { HiringRequestsService } from '../../project-list/hiring-requests/hiring-requests.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-hiring-requests',
  templateUrl: './hiring-requests.component.html',
  styleUrls: ['./hiring-requests.component.scss']
})
export class HiringRequestsComponent implements OnInit {
  projectId: number;
  filterModel: IFilterModel;
  hiringListHeaders$ = of([
    'Hiring Request Id',
    'Job Code',
    'Job Grade',
    'Position',
    'Total Vacancies',
    'Filled Vacancies',
    'Pay Currency',
    'Pay Rate',
    'Status'
  ]);
  hiringList$: Observable<HiringList[]>;
  constructor(
    private router: Router,
    private routeActive: ActivatedRoute,
    public hiringRequestService: HiringRequestsService,
    private loader: CommonLoaderService
  ) {}

  ngOnInit() {
    this.filterModel = {
      FilterValue: '',
      pageIndex: 0,
      pageSize: 10,
      ProjectId: null,
      TotalCount: 0
    };
    this.routeActive.parent.params.subscribe(params => {
      this.projectId = +params['id'];
    });
    this.getAllHiringRequestFilterList();
  }

  //#region  paginatorEvent
  pageEvent(e) {
    this.filterModel.pageIndex = e.pageIndex;
    this.filterModel.pageSize = e.pageSize;
    this.onFilterApplied();
  }
  //#endregion

  //#region "onFilterApplied"
  onFilterApplied() {
    this.getAllHiringRequestFilterList();
  }
  //#endregion

  //#region "getAllProjectActivityList"
  getAllHiringRequestFilterList() {
    this.filterModel.ProjectId = this.projectId;
    this.filterModel.TotalCount = 0;
    //   this.hiringRequestListLoader = true;
    this.hiringRequestService
      .GetProjectHiringRequestFilterList(this.filterModel)
      .subscribe(
        (response: IResponseData) => {
          this.loader.showLoader();
          if (response.statusCode === 200 && response.data !== null) {
            this.filterModel.TotalCount =
              response.total != null ? response.total : 0;
            this.hiringList$ = of(
              response.data.map(element => {
                return {
                  HiringRequestId: element.HiringRequestId,
                  JobCode: element.JobCode,
                  JobGrade: element.JobGrade,
                  Position: element.Position,
                  TotalVacancies: element.TotalVacancies,
                  FilledVacancies: element.FilledVacancies,
                  PayCurrency: element.PayCurrency,
                  PayRate: element.PayRate,
                  Status: element.Status
                } as HiringList;
              })
            );
          }
          this.loader.hideLoader();
        },
        error => {
          this.loader.hideLoader();
        }
      );
  }
  //#endregion
}
