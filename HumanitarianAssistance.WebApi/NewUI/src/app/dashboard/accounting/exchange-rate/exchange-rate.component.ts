import { Component, OnInit } from '@angular/core';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-exchange-rate',
  templateUrl: './exchange-rate.component.html',
  styleUrls: ['./exchange-rate.component.scss']
})
export class ExchangeRateComponent implements OnInit {

  setProjectHeader = 'Exchange Rates';

  constructor(private globalService: GlobalSharedService) {
    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);

    // Set Menu Header List
    this.globalService.setMenuList([]);
  }

  ngOnInit() {

  }

}
