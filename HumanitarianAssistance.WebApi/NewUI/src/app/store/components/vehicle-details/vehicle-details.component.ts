import { Component, OnInit, HostListener } from '@angular/core';
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

  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;

  constructor(private dialog: MatDialog, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      this.vehicleId = params['id'];
    });
  }

  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

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
