import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-vehicle',
  templateUrl: './edit-vehicle.component.html',
  styleUrls: ['./edit-vehicle.component.scss']
})
export class EditVehicleComponent implements OnInit {

  constructor(private router : Router) { }

  ngOnInit() {
  }
  backToDetails(){
    this.router.navigate(['store/vehicle/detail',1])
    }
}
