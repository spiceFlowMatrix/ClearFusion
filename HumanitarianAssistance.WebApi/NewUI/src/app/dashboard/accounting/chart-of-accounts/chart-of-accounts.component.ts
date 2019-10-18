import { Component, OnInit } from '@angular/core';
import { IMenuList } from 'src/app/shared/dbheader/dbheader.component';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { accountingNewMaster } from 'src/app/shared/applicationpagesenum';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { UIModuleHeaders } from 'src/app/shared/enum';

@Component({
  selector: 'app-chart-of-accounts',
  templateUrl: './chart-of-accounts.component.html',
  styleUrls: ['./chart-of-accounts.component.scss']
})
export class ChartOfAccountsComponent implements OnInit {
  setSelectedHeader = UIModuleHeaders.ChartOfAccountHeader;
  setProjectHeader = 'Chart Of Accounts';
  menuList: IMenuList[] = [
    {
      Id: 1,
      PageId: accountingNewMaster.Assets,
      Text: 'Assets',
      Link: '/accounting/chart-of-accounts/assets'
    },
    {
      Id: 2,
      PageId: accountingNewMaster.Liabilities,
      Text: 'Liabilities',
      Link: '/accounting/chart-of-accounts/liabilities'
    },
    {
      Id: 3,
      PageId: accountingNewMaster.Income,
      Text: 'Income',
      Link: '/accounting/chart-of-accounts/income'
    },
    {
      Id: 4,
      PageId: accountingNewMaster.Expense,
      Text: 'Expense',
      Link: '/accounting/chart-of-accounts/expense'
    }
  ];
  authorizedMenuList: IMenuList[] = [];

  constructor(
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
  }

  ngOnInit() {}
}
