import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-vehicle-filters',
  templateUrl: './vehicle-filters.component.html',
  styleUrls: ['./vehicle-filters.component.scss']
})
export class VehicleFiltersComponent implements OnInit {
  isBasic = true;

  vehicleTrackerFilterForm: FormGroup;

  constructor(private _fb: FormBuilder) {
    this.vehicleTrackerFilterForm = this._fb.group({
      'PlateNo': [null],
      'Driver': [null],
      'Office': [null],
      'TotalCost': [null]
    });
  }

  ngOnInit() {
  }
  clearFilters(){

  }
}
