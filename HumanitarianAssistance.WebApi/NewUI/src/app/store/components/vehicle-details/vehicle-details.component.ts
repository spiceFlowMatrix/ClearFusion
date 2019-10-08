import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { AddMilageComponent } from '../add-milage/add-milage.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.scss']
})
export class VehicleDetailsComponent implements OnInit {

  constructor(private dialog: MatDialog, private router: Router) { }

  ngOnInit() {
  }
  openMilageModal(event) {

    const dialogRef = this.dialog.open(AddMilageComponent, {
      width: '850px',
      data: {
        //value: event.item.Id,
        //  officeId: this.filterValueModel.OfficeId
      }
    });
  }
  goToDetails() {
    this.router.navigate(['store/vehicle/edit',1])
  }
}
