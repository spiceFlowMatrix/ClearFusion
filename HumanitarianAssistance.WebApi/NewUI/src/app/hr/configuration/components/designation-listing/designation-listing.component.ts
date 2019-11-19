import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';

@Component({
  selector: 'app-designation-listing',
  templateUrl: './designation-listing.component.html',
  styleUrls: ['./designation-listing.component.scss']
})
export class DesignationListingComponent implements OnInit {

  designationList$: Observable<any[]>;
  subListHeaders: Observable<any[]>;

  designationListHeaders$ = of(['Id', 'Name', 'Description']);
  subListHeaders$ = of(['Id', 'Question']);

  constructor() { }

  ngOnInit() {
    this.designationList$ = of([{Id: 1, Name: 'abc', Description: 'description'}]);
  }

}
