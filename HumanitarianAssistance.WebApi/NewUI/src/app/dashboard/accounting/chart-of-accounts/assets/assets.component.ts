import { Component, OnInit } from '@angular/core';
import { AccountHeadTypes_Enum } from 'src/app/shared/enum';

@Component({
  selector: 'app-assets',
  templateUrl: './assets.component.html',
  styleUrls: ['./assets.component.scss']
})
export class AssetsComponent implements OnInit {

  ACCOUNT_HEAD_TYPE = AccountHeadTypes_Enum.Assets;
  ACCOUNT_HEAD_NAME = AccountHeadTypes_Enum[this.ACCOUNT_HEAD_TYPE];

  constructor() {
  }

  ngOnInit() {
  }

}
