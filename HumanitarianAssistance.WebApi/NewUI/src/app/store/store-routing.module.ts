import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PurchaseListComponent } from './components/purchase-list/purchase-list.component';
import { AddPurchaseComponent } from './components/add-purchase/add-purchase.component';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import { VehicleTrackerComponent } from './components/vehicle-tracker/vehicle-tracker.component';
import { GeneratorTrackerComponent } from './components/generator-tracker/generator-tracker.component';

const routes: Routes = [
  {
    path: '', component: EntryComponentComponent,
    children: [

      { path: 'purchases', component: PurchaseListComponent },
      { path: 'purchase/add', component: AddPurchaseComponent },
      { path: 'vehicle/tracker', component: VehicleTrackerComponent },
      { path: 'generator/tracker', component: GeneratorTrackerComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StoreRoutingModule { }
