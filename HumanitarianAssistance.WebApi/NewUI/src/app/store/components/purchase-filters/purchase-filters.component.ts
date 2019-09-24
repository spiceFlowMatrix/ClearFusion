import { Component, OnInit } from '@angular/core';
import { PurchaseService } from '../../services/purchase.service';

@Component({
  selector: 'app-purchase-filters',
  templateUrl: './purchase-filters.component.html',
  styleUrls: ['./purchase-filters.component.scss']
})
export class PurchaseFiltersComponent implements OnInit {

  foods: any[] ;

  constructor(private purchase: PurchaseService) { }

  ngOnInit() {
    this.foods = [
      {value: 'steak-0', viewValue: 'Steak'},
      {value: 'pizza-1', viewValue: 'Pizza'},
      {value: 'tacos-2', viewValue: 'Tacos'}
    ];
  }

  onPurchaseClick() {
    this.purchase.GetPurchaseFilterList().subscribe(x => {
      debugger;
    });

  }

}
