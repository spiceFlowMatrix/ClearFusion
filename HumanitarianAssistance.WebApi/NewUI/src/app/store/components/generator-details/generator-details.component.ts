import { Component, OnInit } from '@angular/core';
import { AddMilageComponent } from '../add-milage/add-milage.component';
import { MatDialog, MatTabContent } from '@angular/material';
import { Router } from '@angular/router';
import { AddHoursComponent } from '../add-hours/add-hours.component';

@Component({
  selector: 'app-generator-details',
  templateUrl: './generator-details.component.html',
  styleUrls: ['./generator-details.component.scss']
})
export class GeneratorDetailsComponent implements OnInit {

  constructor(private dialog: MatDialog, private router: Router) { }

  ngOnInit() {
  }
  openMilageModal() {

    const dialogRef = this.dialog.open(AddHoursComponent, {
      width: '850px',
      data: {
        //value: event.item.Id,
        //  officeId: this.filterValueModel.OfficeId
      }
    });
  }
  goToDetails() {
    this.router.navigate(['store/generator/edit',1])
  }
}
