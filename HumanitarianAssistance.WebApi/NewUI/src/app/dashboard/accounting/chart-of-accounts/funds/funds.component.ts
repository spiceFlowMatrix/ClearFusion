import { Component, OnInit } from '@angular/core';
import { AccountHeadTypes_Enum } from 'src/app/shared/enum';


@Component({
  selector: 'app-funds',
  templateUrl: './funds.component.html',
  styleUrls: ['./funds.component.scss']
})
export class FundsComponent implements OnInit {

  ACCOUNT_HEAD_TYPE = AccountHeadTypes_Enum.OwnersEquity;
  ACCOUNT_HEAD_NAME = AccountHeadTypes_Enum[this.ACCOUNT_HEAD_TYPE];


  constructor() {
  }

  ngOnInit() {
  }

}