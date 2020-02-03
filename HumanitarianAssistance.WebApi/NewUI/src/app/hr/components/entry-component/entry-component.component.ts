import { Component, OnInit } from '@angular/core';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';

@Component({
  selector: 'app-entry-component',
  templateUrl: './entry-component.component.html',
  styleUrls: ['./entry-component.component.scss']
})
export class EntryComponentComponent implements OnInit {

  constructor(private globalservice: GlobalSharedService) { }

  ngOnInit() {
    this.globalservice.setMenuHeaderName('Employees');
  }

}
