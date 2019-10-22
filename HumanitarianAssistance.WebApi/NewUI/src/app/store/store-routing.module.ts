import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PurchaseListComponent } from './components/purchase-list/purchase-list.component';
import { AddPurchaseComponent } from './components/add-purchase/add-purchase.component';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import { VehicleTrackerComponent } from './components/vehicle-tracker/vehicle-tracker.component';
import { GeneratorTrackerComponent } from './components/generator-tracker/generator-tracker.component';
import { VehicleDetailsComponent } from './components/vehicle-details/vehicle-details.component';
import { GeneratorDetailsComponent } from './components/generator-details/generator-details.component';
import { EditVehicleComponent } from './components/edit-vehicle/edit-vehicle.component';
import { EditGeneratorComponent } from './components/edit-generator/edit-generator.component';

const routes: Routes = [
  {
    path: '', component: EntryComponentComponent,
    children: [

      { path: 'purchases', component: PurchaseListComponent },
      { path: 'purchase/add', component: AddPurchaseComponent },
      { path: 'purchase/edit/:id', component: AddPurchaseComponent },
      { path: 'vehicle/tracker', component: VehicleTrackerComponent },
      { path: 'generator/tracker', component: GeneratorTrackerComponent },
      { path: 'vehicle/detail/:id', component: VehicleDetailsComponent },
      { path: 'generator/detail/:id', component: GeneratorDetailsComponent },
      { path: 'vehicle/edit/:id', component: EditVehicleComponent },
      { path: 'generator/edit/:id', component: EditGeneratorComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StoreRoutingModule { }
