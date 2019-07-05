import { Component, OnInit } from '@angular/core';
import { UIModuleHeaders } from '../../../shared/enum';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-vouchers',
  templateUrl: './vouchers.component.html',
  styleUrls: ['./vouchers.component.scss']
})
export class VouchersComponent implements OnInit {

  setSelectedHeader = UIModuleHeaders.VouchersHeader;
  setProjectHeader = 'Vouchers';

  constructor(private globalService: GlobalSharedService) {
    // Set Menu Header Name
    this.globalService.setMenuHeaderName(this.setProjectHeader);

    // Set Menu Header List
    this.globalService.setMenuList([]);
   }

  ngOnInit() {
  }

}
