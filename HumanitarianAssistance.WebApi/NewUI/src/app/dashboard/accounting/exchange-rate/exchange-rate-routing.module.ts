import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ExchangeRateListingComponent } from './exchange-rate-listing/exchange-rate-listing.component';
import { ExchangeRateComponent } from './exchange-rate.component';

const routes: Routes = [
  {
    path: '',
    component: ExchangeRateComponent,
    children: [
      {
        path: '',
        component: ExchangeRateListingComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExchangeRateRoutingModule {}
