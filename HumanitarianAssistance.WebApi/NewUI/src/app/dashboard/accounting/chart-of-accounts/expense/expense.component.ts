import { Component, OnInit } from '@angular/core';
import { AccountHeadTypes_Enum } from 'src/app/shared/enum';

@Component({
  selector: 'app-expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.scss']
})
export class ExpenseComponent implements OnInit {

  ACCOUNT_HEAD_TYPE = AccountHeadTypes_Enum.Expense;
  ACCOUNT_HEAD_NAME = AccountHeadTypes_Enum[this.ACCOUNT_HEAD_TYPE];

  constructor() {
  }

  ngOnInit() {
  }


}
