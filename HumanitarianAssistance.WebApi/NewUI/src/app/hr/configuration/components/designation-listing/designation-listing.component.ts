import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { HrService } from 'src/app/hr/services/hr.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { AddDesignationComponent } from '../add-designation/add-designation.component';

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

  constructor(private hrService: HrService, private dialog: MatDialog, private toastr: ToastrService) { }

  ngOnInit() {
    this.getDesignationList();
  }

  getDesignationList() {
    this.hrService.getDesignationList().subscribe(x =>
      this.designationList$ = of(x.data.DesignationList.map(element => {
        return {
          Id: element.DesignationId,
          Designation: element.Designation,
          Description: element.Description
        };
      })
      ));
  }

  addDesignation() {
      const dialogRef = this.dialog.open(AddDesignationComponent, {
        width: '850px',
      });

      // dialogRef.afterClosed().subscribe(x => {
      //   // console.log(x);

      //   this.purchaseList$.subscribe((purchase) => {
      //     // console.log(purchase);

      //     const index = purchase.findIndex(i => i.Id === x.PurchaseId);
      //     if (index !== -1) {
      //       purchase[index].subItems.unshift({
      //         Id: x.ProcurementId,
      //         IssueDate: this.datePipe.transform(x.IssueDate, 'dd-MM-yyyy'),
      //         Employee: x.EmployeeName,
      //         ProcuredAmount: x.IssuedQuantity,
      //         MustReturn: (x.MustReturn === 'Yes' || x.MustReturn)  ? 'Yes' : 'No',
      //         Returned: 'No',
      //         ReturnedOn: null
      //       } as IProcurementList);
      //     }
      //     this.purchaseList$ = of(purchase);
      //   });
      // });

  }
}
