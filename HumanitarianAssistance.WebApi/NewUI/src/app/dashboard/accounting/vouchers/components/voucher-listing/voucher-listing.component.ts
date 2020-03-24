import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { IDropDownModel } from 'src/app/store/models/purchase';
import { Month, VoucherType } from 'src/app/shared/enum';
import { of } from 'rxjs/internal/observable/of';
import { FormGroup, FormBuilder, NgModel, FormControl } from '@angular/forms';
import { VoucherService } from '../../voucher.service';
import { IResponseData } from '../../models/status-code.model';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { DatePipe } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { StaticUtilities } from 'src/app/shared/static-utilities';
import { ToastrService } from 'ngx-toastr';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { GLOBAL } from 'src/app/shared/global';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { MatOption } from '@angular/material';
import { PermissionEnum } from 'src/app/shared/permission-enum';
import { Auth0Service } from 'src/app/auth0/auth0.service';


@Component({
  selector: 'app-voucher-listing',
  templateUrl: './voucher-listing.component.html',
  styleUrls: ['./voucher-listing.component.scss']
})
export class VoucherListingComponent implements OnInit {

  monthsList$: Observable<IDropDownModel[]>;
  selectedMonth: IDropDownModel;
  voucherFilterForm: FormGroup;
  currency$: Observable<IDropDownModel[]>;
  journalList$: Observable<IDropDownModel[]>;
  operationalTypes$: Observable<IDropDownModel[]>;
  selectedOffices: any[];
  // officeForm: FormGroup;
  Office = new FormControl([]);
  @ViewChild('allSelected') private allSelected: MatOption;
  selectedVoucherNo = 30;
  offices: any[];
  ELEMENT_DATA: any[] = [];
  permissionsEnum = PermissionEnum;
  voucherDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
  pagingModel = {
    pageIndex: 0,
    pageSize: 10,
    recordCount: 0
  };

  constructor(private fb: FormBuilder, private voucherService: VoucherService,
    private datePipe: DatePipe, private router: Router, private routeActive: ActivatedRoute,
    private toastr: ToastrService, private globalSharedService: GlobalSharedService,
    private appurl: AppUrlService, private auth0Service: Auth0Service) {
    this.selectedMonth = { name: 'SELECT MONTH', value: 0 };
    this.voucherFilterForm = this.fb.group({
      'Search': [null],
      'Date': [{ 'begin': null, 'end':  null}],
      'CurrencyId': [null],
      'OperationalType': [null],
      'JournalId': [null]
    });
    this.operationalTypes$ = of([
      {name: 'Direct Voucher', value: 2 },
      {name: 'Logistics', value: 3 }
    ]);
  }

  ngOnInit() {
    this.getAllMonthList();
    this.getAllCurrency();
    this.getJournalList();
    this.getOfficeList();

  }


  //#region "Get all month list for ExperienceInMonth dropdown"
  getAllMonthList() {
    const monthDropDown: IDropDownModel[] = [];
    for (let i = Month['January']; i <= Month['December']; i++) {
      monthDropDown.push({ name: Month[i], value: i });
    }
    this.monthsList$ = of(monthDropDown);
  }
  //#endregion

  selectedMonthChanged(SelectedMonth) {
    this.selectedMonth = {
      name: SelectedMonth.name,
      value: SelectedMonth.value
    };
  }

  getAllCurrency() {
    return this.voucherService.GetCurrencyList().subscribe(x => {
      this.currency$ = of(x.data.map(y => {
        return {
          value: y.CurrencyId,
          name: y.CurrencyCode + '-' + y.CurrencyName
        };
      }));
    });
  }

  //#region "getJournalList"
  getJournalList() {
    this.voucherService.GetJournalList().subscribe(
      (response: IResponseData) => {
        if (response.statusCode === 200 && response.data !== null) {
          this.journalList$ = of(response.data.map(y => {
            return {
              value: y.JournalCode,
              name: y.JournalName
            };
          }));
        }
      },
      error => { }
    );
  }
  //#endregion

  displayedColumns: string[] = ['select', 'ReferenceNo', 'Description', 'Journal', 'VoucherDate', 'Status'];

  selection = new SelectionModel<any>(true, []);

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.voucherDataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.voucherDataSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

  //#region "getVoucherList"
  getVoucherList() {
    const officeIds: any[] = [];

    if (this.Office.value.length > 0) {
      this.Office.value.forEach(x => {
        officeIds.push(x);
      });
    }

   const model = {
     FilterValue: this.voucherFilterForm.value.Search,
     StartDate: this.voucherFilterForm.value.Date.begin != null ?
                  StaticUtilities.getLocalDate(this.voucherFilterForm.value.Date.begin) : null,
     EndDate: this.voucherFilterForm.value.Date.end != null ? StaticUtilities.getLocalDate(this.voucherFilterForm.value.Date.end) : null,
     CurrencyId: this.voucherFilterForm.value.CurrencyId,
     OperationalType: this.voucherFilterForm.value.OperationalType,
     JournalId: this.voucherFilterForm.value.JournalId,
     OfficeIds: officeIds,
     PageIndex: this.pagingModel.pageIndex,
     pageSize: this.pagingModel.pageSize
   };

    this.voucherService.GetVoucherList(model).subscribe(
      response => {
        this.ELEMENT_DATA = [];
        if (
          response.StatusCode === 200 &&
          response.data.VoucherDetailList != null
        ) {
          if (response.data.VoucherDetailList.length > 0) {
            this.pagingModel.recordCount = response.data.TotalCount != null ? response.data.TotalCount : 0;
            this.selectedVoucherNo = response.data.VoucherDetailList[0].VoucherNo;
            response.data.VoucherDetailList.forEach(element => {
              this.ELEMENT_DATA.push({
                VoucherNo: element.VoucherNo,
                CurrencyCode: element.CurrencyCode,
                CurrencyId: element.CurrencyId,
                VoucherDate:
                  element.VoucherDate != null
                    ? this.datePipe.transform(new Date(
                      new Date(element.VoucherDate).getTime() -
                        new Date().getTimezoneOffset() * 60000
                    ), 'dd-MM-yyyy')
                    : null,
                ChequeNo: element.ChequeNo,
                ReferenceNo: element.ReferenceNo,
                Description: element.Description,
                Journal: element.JournalName,
                JournalCode: element.JournalCode,
                VoucherTypeId: element.VoucherTypeId,
                OfficeId: element.OfficeId,
                ProjectId: element.ProjectId,
                BudgetLineId: element.BudgetLineId,
                OfficeName: element.OfficeName,
                Status: element.IsVoucherVerified ? 'Verified' : 'Unverified',
                IsVoucherVerified: element.IsVoucherVerified,
                OperationalType: element.OperationalType
              });
            });
            this.voucherDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
            this.selection.clear();
          } else {
            this.ELEMENT_DATA = [];
            this.voucherDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
            this.selection.clear();
          }
        } else {
          this.voucherDataSource = new MatTableDataSource<any>(this.ELEMENT_DATA);
            this.selection.clear();
        }
        // this.commonLoader.hideLoader();
      },
      error => {
        // this.commonLoader.hideLoader();
      }
    );
  }
  //#endregion

  verifySelectedVouchers() {
    const ids: any[] = [];
    if (this.selection.selected.length > 0) {
      for (let i = 0; i < this.selection.selected.length; i++ ) {
        if (!this.selection.selected[i].IsVoucherVerified) {
          ids.push(this.selection.selected[i].VoucherNo);
        }
      }
    } else {
      this.toastr.warning('Please select atleast 1 record to validate');
      return;
    }

    if (ids.length <= 0) {
      this.toastr.warning('Please select atleast 1 unverified voucher to validate');
      return;
    }

    this.voucherService.verifySelectedVouchers(ids).subscribe(x => {
      if (x) {
        this.toastr.success('Verified successfully');
        this.getVoucherList();
      }
    }, error => {
      this.toastr.warning(error);
    });
  }

  deleteSelectedVouchers() {
    const ids: any[] = [];
    if (this.selection.selected.length > 0) {

      const storeVouchers = this.selection.selected.filter(x => x.OperationalType ===  VoucherType.Logistics ||
        x.OperationalType === VoucherType.Store);

        if (storeVouchers.length > 0) {
          let voucherCodes = '';

          for (let i = 0; i < storeVouchers.length; i++) {
            voucherCodes = voucherCodes + storeVouchers[i].ReferenceNo;
          }
           this.toastr.warning('Cannot delete purchase order vouchers ' + voucherCodes);
        }

      for (let i = 0; i < this.selection.selected.length; i++ ) {
          ids.push(this.selection.selected[i].VoucherNo);
      }
    } else {
      this.toastr.warning('Please select atleast 1 record to delete');
      return;
    }

    this.voucherService.deleteSelectedVouchers(ids).subscribe(x => {
      if (x) {
        this.toastr.success('deleted successfully');
        this.getVoucherList();
      }
    }, error => {
      this.toastr.warning(error);
    });

  }

  applyFilter() {
    this.getVoucherList();
  }

  addVoucher() {
    this.router.navigate(['../add-voucher'], { relativeTo: this.routeActive });
  }

  //#region "getOfficeList"
  getOfficeList() {
    this.voucherService.GetOfficeList().subscribe(
      (response: IResponseData) => {
        this.offices = [];
        if (response.statusCode === 200 && response.data !== null) {
          response.data.forEach(element => {
            this.offices.push({
              OfficeId: element.OfficeId,
              OfficeName: element.OfficeName
            });
          });
           this.Office.patchValue([...this.offices.map(item => item.OfficeId), 0]);
          this.allSelected.select();
          this.getVoucherList();
        }
      },
      error => {}
    );
  }
  //#endregion

  navigateToDetails(detail) {
    this.selectedVoucherNo = detail.VoucherNo;
    this.router.navigate(['../' + detail.VoucherNo], { relativeTo: this.routeActive });
  }

  //#region "pageEvent"
  pageEvent(e) {
    this.pagingModel.pageIndex = e.pageIndex;
    this.pagingModel.pageSize = e.pageSize;
    this.getVoucherList();
  }
  //#endregion

  // toCheck: boolean =  false;

  equals(objOne, objTwo) {
    if (typeof objOne !== 'undefined' && typeof objTwo !== 'undefined') {
      return objOne.id === objTwo.id;
    }
  }

  exportSelectedVouchers() {
    const model = {
      VoucherIdList: []
    };

    if (this.selection.selected.length === 0) {
      this.toastr.warning('Please select atleast 1 record for pdf export');
      return;
    }

    this.selection.selected.forEach(x => {
      model.VoucherIdList.push(x.VoucherNo);
    });
    this.globalSharedService
    .getFile(this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetAllVoucherSummaryReportPdf,
    model
    )
    .subscribe();
  }

  onOpenedChange(event) {
    if (!event) {
      this.getVoucherList();
    }
  }

  resetFilter() {
    this.voucherFilterForm.reset();
    this.voucherFilterForm = this.fb.group({
      'Search': [null],
      'Date': [{ 'begin': null, 'end':  null}],
      'CurrencyId': [null],
      'OperationalType': [null],
      'JournalId': [null]
    });
    this.getVoucherList();
  }

  toggleAllSelection() {
    if (this.allSelected.selected) {

      this.Office
        .patchValue([...this.offices.filter(x => x !== 0).map(item => item.OfficeId), 0]);
    } else {
      this.Office.patchValue([]);
    }
    this.getVoucherList();
  }

  tosslePerOne(all){
    if (this.allSelected.selected) {
     this.allSelected.deselect();
     return false;
 }
   if (this.Office.value.length==this.offices.length) {
    this.allSelected.select();
   }
     this.getVoucherList();

 }

 checkPermission(permission) {
  return this.auth0Service.checkPermissions(permission);
}

}
