import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-filters',
  templateUrl: './vehicle-filters.component.html',
  styleUrls: ['./vehicle-filters.component.scss']
})
export class VehicleFiltersComponent implements OnInit {
  isBasic = true;
  constructor() { }

  ngOnInit() {
  }
  clearFilters(){

  }
}
