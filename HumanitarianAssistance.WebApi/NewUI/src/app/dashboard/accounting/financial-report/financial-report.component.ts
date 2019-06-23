import { Component, OnInit } from '@angular/core';
import { UIModuleHeaders } from '../../../shared/enum';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { accountingNewMaster } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-financial-report',
  templateUrl: './financial-report.component.html',
  styleUrls: ['./financial-report.component.scss']
})
export class FinancialReportComponent implements OnInit {

  setSelectedHeader = UIModuleHeaders.FinancialAccountHeader;
  setProjectHeader = 'Financial Report';
  menuList: IMenuList[] = [
    {
      Id: 1,
      PageId: accountingNewMaster.BalanceSheet,
      Text: 'Balance Sheet',
      Link: '/accounting/financial-report/balance-sheet'
    },
    {
      Id: 2,
      PageId: accountingNewMaster.IncomeExpenseReport,
      Text: 'Income Expense Report',
      Link: '/accounting/financial-report/income-expense-report'
    }
  ];
  authorizedMenuList: IMenuList[] = [];

  constructor(private globalService: GlobalSharedService,
    private localStorageservice: LocalStorageService
  ) {
    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);

    this.authorizedMenuList = this.localStorageservice.GetAuthorizedPages(
      this.menuList
    );

    // Set Menu Header List
    this.globalService.setMenuList(this.authorizedMenuList);
   }


  ngOnInit() {
  }

}
