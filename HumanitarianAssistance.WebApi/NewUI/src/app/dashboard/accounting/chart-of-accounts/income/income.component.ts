import { Component, OnInit } from '@angular/core';
import { AccountHeadTypes_Enum } from 'src/app/shared/enum';

@Component({
  selector: 'app-income',
  templateUrl: './income.component.html',
  styleUrls: ['./income.component.scss']
})
export class IncomeComponent implements OnInit {

  ACCOUNT_HEAD_TYPE = AccountHeadTypes_Enum.Income;
  ACCOUNT_HEAD_NAME = AccountHeadTypes_Enum[this.ACCOUNT_HEAD_TYPE];

  constructor() {
  }

  ngOnInit() {
  }

}