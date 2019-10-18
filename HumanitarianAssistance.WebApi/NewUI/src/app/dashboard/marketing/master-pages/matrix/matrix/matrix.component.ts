import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ContractsService } from '../../../contracts/service/contracts.service';
import { Router } from '@angular/router';
import { MasterPageServiceService } from '../../service/master-page-service.service';
import { UnitRateModel, UnitRatePaginationModel } from '../../model/mastrer-pages.model';
import { UnitRateComponent } from '../../unit-rate/unit-rate.component';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';
import { ApplicationPages } from '../../../../../shared/applicationpagesenum';
import { LocalStorageService } from '../../../../../shared/services/localstorage.service';
import { IResponseData } from 'src/app/dashboard/accounting/vouchers/models/status-code.model';

@Component({
  selector: 'app-matrix',
  templateUrl: './matrix.component.html',
  styleUrls: ['./matrix.component.scss']
})

export class MatrixComponent implements OnInit {
  @ViewChild(UnitRateComponent) child: UnitRateComponent;
  unitRatesList: UnitRateModel[];
 // unitRate:UnitRateModel
  showUnitRateDetail = false;
  colsm6 = 'col-sm-10 col-sm-offset-1';
  isEditingAllowed = false;
  pageId = ApplicationPages.UnitRates;
  routeId = 0;
  type: any = '';
  unitRateId = 0;
  public selectedRowID;
  screenHeight: any;
  screenWidth: any;
  length:  number;
  pageSize = 10;
  pageIndex = 0;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  paginationModel: UnitRatePaginationModel = {};
  scrollStyles: any;
  unitRateListLoaderFlag = false;
  // tslint:disable-next-line:max-line-length
  constructor(private toastr: ToastrService, private commonLoaderService: CommonLoaderService, private contractService: ContractsService,
    private appurl: AppUrlService, private router: Router,
    private masterPageService: MasterPageServiceService,
    private localStorageService: LocalStorageService
  ) {
    this.getScreenSize();
   }

  ngOnInit() {
    this.getUnitRate();
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(this.pageId);
  }

  getUnitRate() {
    this.unitRateListLoaderFlag = true;
    // this.commonLoaderService.showLoader();
    this.masterPageService.GetUnitRateList().subscribe((data: IResponseData) => {
      if (data.statusCode === 200) {
        this.unitRatesList = data.data;
      } else {
      this.toastr.error('Some error occured. Please try again later');
      }
      this.unitRateListLoaderFlag = false;
    },
    error => {
      this.unitRateListLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
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

  onItemClick(id: number) {
    this.routeId = id;
    if (this.routeId === 0 || this.routeId === null || this.routeId === undefined) {
      this.child.AdditionOfUnitRate();
    }
    this.selectedRowID = id;
    this.showUnitRateDetailPanel();
  }

  //#region "show/hide"
  showUnitRateDetailPanel() {
    this.showUnitRateDetail = true;
    this.colsm6 = this.showUnitRateDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }

  hideUnitRateDetailPanel() {
    this.showUnitRateDetail = false;
    this.colsm6 = this.showUnitRateDetail ? 'col-sm-6' : 'col-sm-10 col-sm-offset-1';
  }
  //#endregion

  //#region "Emit"
  hideDetailPanel(e) {
    this.hideUnitRateDetailPanel();
  }
  //#endregion

  onUnitRateDeleted(id) {
    const index = this.unitRatesList.findIndex(r => r.UnitRateId === id.id);
    this.unitRatesList.splice(index, 1);
    this.hideUnitRateDetailPanel();
  }

  addUnitRateListById(e) {
    this.getUnitRate();
    // this.unitRatesList.push(e);
  }

  updateUnitRateList(e) {
    const index = this.unitRatesList.findIndex(r => r.UnitRateId === e.UnitRateId);
    if (index !== -1) {
      this.unitRatesList[index] = e;
    }
  }

  pagination(event) {
    this.unitRateListLoaderFlag = true;
    this.length = 0;
    this.paginationModel.pageIndex = event.pageIndex;
    this.paginationModel.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
     this.unitRatesList = [];
    // tslint:disable-next-line:max-line-length
    this.masterPageService.GetUnitRatePaginatedList(this.paginationModel).subscribe((data: IResponseData) => {
      if (data.statusCode === 200) {
        this.unitRatesList = data.data;
        this.length = data.total;
      } else if (data.statusCode === 400) {
        this.toastr.error(data.message);
      }
      this.unitRateListLoaderFlag = false;
    },
    error => {
      this.unitRateListLoaderFlag = false;
      this.toastr.error('Some error occured. Please try again later');
    });
  }

}
