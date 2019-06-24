import {
  Component,
  OnInit,
  ViewChild,
  Output,
  HostListener
} from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { GLOBAL } from 'src/app/shared/global';
import { DonorMasterComponent } from '../donor-master/donor-master.component';
import {
  DonorDetailModel,
  DonorFilterModel
} from '../donor-master/Models/donar-detail.model';
import { UIModuleHeaders } from '../../../../shared/enum';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { ProjectListService } from '../../project-list/service/project-list.service';
import {
  projectPagesMaster,
  ApplicationPages
} from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-donor-master-list',
  templateUrl: './donor-master-list.component.html',
  styleUrls: ['./donor-master-list.component.scss']
})
export class DonorMasterListComponent implements OnInit {
  setSelectedHeader = UIModuleHeaders.ProjectModule;
  setProjectHeader = 'Projects';
  menuList: IMenuList[] = [
    {
      Id: 1,
      PageId: projectPagesMaster.ProjectDashboard,
      Text: 'Dashboard',
      Link: '/project/project-dashboard'
    },
    {
      Id: 2,
      PageId: projectPagesMaster.MyProjects,
      Text: 'My Projects',
      Link: '/project/my-projects'
    },
    {
      Id: 3,
      PageId: projectPagesMaster.Donors,
      Text: 'Donors',
      Link: '/project/project-donor'
    },
    {
      Id: 4,
      PageId: projectPagesMaster.ProjectCashFlow,
      Text: 'Cash Flow',
      Link: '/project/project-cash-flow'
    },
    {
      Id: 5,
      PageId: projectPagesMaster.ProposalReport,
      Text: 'Proposal Report',
      Link: '/project/proposal-report'
    },
    {
      Id: 6,
      PageId: projectPagesMaster.ProjectIndicators,
      Text: 'Project Indicators',
      Link: '/project/project-indicators'
    }
  ];
  authorizedMenuList: IMenuList[] = [];

  @Output() message: true;
  @ViewChild(DonorMasterComponent) child;

  //#region loaderFlag
  donorListLoaderFlag = false;
  //#endregion loaderFlag

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  donorList: any[];
  tottalRecord: number;
  donarlist: any[];
  cols: any[];
  firsts = 0;
  donorForm: FormGroup;
  pageId = ApplicationPages.Donors;
  showJobDetail = false;
  isEditingAllowed = false;
  colsm6 = 'col-sm-10 col-sm-offset-1';
  selectedItem: any;
  donorId;
  public selectedRowID;
  DonorDetailModel: DonorDetailModel[];
  donorFilterModel: DonorFilterModel;

  constructor(
    public router: Router,
    public dialog: MatDialog,
    public projectListService: ProjectListService,
    private appurl: AppUrlService,
    private localStorageService: LocalStorageService,
    private globalService: GlobalSharedService,
    private localStorageservice: LocalStorageService
  ) {
    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);

    this.authorizedMenuList = this.localStorageservice.GetAuthorizedPages(
      this.menuList
    );

    // Set Menu Header List
    this.globalService.setMenuList(this.authorizedMenuList);

    this.getScreenSize();
  }

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

  ngOnInit() {
    this.initilizeDonorFilterModel();
     this.getAllDonorFilterList();
    // this.getAllDonorList();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
  }

  //#region initilizeModel
  initilizeDonorFilterModel() {
    this.donorFilterModel = {
      FilterValue: '',
      DonorNameFlag: true,
      DateFlag: true,

      pageIndex: 0,
      pageSize: 10,
      totalCount: 0
    };
  }

  //#endregion

  //#region pageEvent
  pageEvent(e) {
    this.donorFilterModel.pageIndex = e.pageIndex;
    this.donorFilterModel.pageSize = e.pageSize;

    this.onFilterApplied();
  }

  //#endregion
  onFilterApplied() {
    this.getAllDonorFilterList();
  }

  paginate(event) {
    this.firsts = event.first / event.rows;
  }

  //#region "getAllDonorList"
  getAllDonorFilterList() {
    this.donorListLoaderFlag = true;
    this.donorFilterModel.totalCount = 0;
    this.projectListService
      .GetAllDonorfilterList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllDonorFilterList,
        this.donorFilterModel
      )
      .subscribe(
        data => {
          this.donorList = [];
          if (data.data.DonorDetail.length > 0 && data.StatusCode === 200) {
            // this.projectFilterModel.totalCount = res.data.TotalCount != null ? res.data.TotalCount : 0;
            this.donorFilterModel.totalCount =
              data.data.TotalCount != null ? data.data.TotalCount : 0;
            // this.tottalRecord = data.data.DonorDetail.length;
            data.data.DonorDetail.forEach(element => {
              this.donorList.push({
                DonorId: element.DonorId,
                Name: element.Name,
                ContactPerson: element.ContactPerson,
                ContactDesignation: element.ContactDesignation,
                ContactPersonEmail: element.ContactPersonEmail,
                ContactPersonCell: element.ContactPersonCell
              });
              this.DonorDetailModel = this.donorList;
            });
          }

          this.donorListLoaderFlag = false;
        },
        error => {
          this.donorListLoaderFlag = false;
        }
      );
  }
  //#endregion

  //#region "getAllDonorList"
  // getAllDonorList() {
  //   this.donorListLoaderFlag = true;
  //   this.donorFilterModel.totalCount = 0;
  //   this.donorList = [];
  //   this.projectListService
  //     .GetAllDonorList(
  //       this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllDonorList
  //     )
  //     .subscribe(
  //       data => {
  //         if (data.data.DonorDetail.length > 0 && data.StatusCode === 200) {
  //           this.donorFilterModel.totalCount = data.data.DonorDetail.length;
  //           // this.tottalRecord = data.data.DonorDetail.length;
  //           data.data.DonorDetail.forEach(element => {
  //             this.donorList.push({
  //               DonorId: element.DonorId,
  //               Name: element.Name,
  //               ContactPerson: element.ContactPerson,
  //               ContactDesignation: element.ContactDesignation,
  //               ContactPersonEmail: element.ContactPersonEmail,
  //               ContactPersonCell: element.ContactPersonCell
  //             });
  //             this.DonorDetailModel = this.donorList;
  //           });
  //           // this.cols = [
  //           //   { field: 'Name', header: 'Donar Name' },
  //           //   { field: 'ContactPerson', header: 'Donar Contact Person Name' },
  //           //   { field: 'ContactDesignation', header: 'Donar Contact Designation' },
  //           //   { field: 'ContactPersonEmail', header: 'Donar Contact Person Email' },
  //           //   { field: 'ContactPersonCell', header: 'Donar Contact Person Cell' }
  //           // ];
  //         }

  //         this.donorListLoaderFlag = false;
  //       },
  //       error => {
  //         this.donorListLoaderFlag = false;
  //       }
  //     );
  // }
  //#endregion

  onItemClick(id: number) {
    this.donorId = id;
    if (
      this.donorId === 0 ||
      this.donorId === undefined ||
      this.donorId === null
    ) {
      this.child.formReset();
      this.child.CreateDonoronAddNew();
    }
    this.selectedRowID = id;
    this.showProjectDetailPanel();
  }

  //#region "show/hide"
  showProjectDetailPanel() {
    this.showJobDetail = true;
    this.colsm6 = this.showJobDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideProjectDetailPanel(e) {
    this.showJobDetail = false;
    this.colsm6 = this.showJobDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  onDonorDeleted(id) {
    const index = this.DonorDetailModel.findIndex(x => x.DonorId === id.id);
    this.donorFilterModel.totalCount = id.count;
    this.DonorDetailModel.splice(index, 1);
    this.child.formReset();
  }

  addDonorList(e) {
    this.DonorDetailModel.push(e);
    this.donorFilterModel.totalCount = e.Count;
  }

  updateDonorList(e) {
    const index = this.DonorDetailModel.findIndex(r => r.DonorId === e.DonorId);
    if (index !== -1) {
      this.DonorDetailModel[index] = e;
    }
  }
}
