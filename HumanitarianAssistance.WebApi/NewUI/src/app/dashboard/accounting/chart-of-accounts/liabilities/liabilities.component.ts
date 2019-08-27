import { Component, OnInit } from '@angular/core';
import { AccountHeadTypes_Enum } from 'src/app/shared/enum';


@Component({
  selector: 'app-liabilities',
  templateUrl: './liabilities.component.html',
  styleUrls: ['./liabilities.component.scss']
})
export class LiabilitiesComponent implements OnInit {

  ACCOUNT_HEAD_TYPE = AccountHeadTypes_Enum.Liabilities;
  ACCOUNT_HEAD_NAME = AccountHeadTypes_Enum[this.ACCOUNT_HEAD_TYPE];

  constructor() {
  }

  ngOnInit() {
  }

}