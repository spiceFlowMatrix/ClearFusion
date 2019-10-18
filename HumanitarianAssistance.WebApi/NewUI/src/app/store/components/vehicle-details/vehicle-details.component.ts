import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { AddMilageComponent } from '../add-milage/add-milage.component';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.scss']
})
export class VehicleDetailsComponent implements OnInit {

  vehicleId: number;

  constructor(private dialog: MatDialog, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.vehicleId = params['id'];
    });
  }
  openMilageModal() {
    const dialogRef = this.dialog.open(AddMilageComponent, {
      width: '850px',
      data: {
        vehicleId: this.vehicleId
      }
    });
  }
  editVehicleDetail() {
    this.router.navigate(['store/vehicle/edit', this.vehicleId]);
  }
}
