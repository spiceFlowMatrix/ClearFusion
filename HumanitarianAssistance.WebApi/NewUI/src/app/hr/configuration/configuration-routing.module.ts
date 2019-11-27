import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConfigurationComponent } from './configuration.component';
import { DesignationListingComponent } from './components/designation-listing/designation-listing.component';
import { GeneralComponent } from './components/general/general.component';

const routes: Routes = [
  {
    path: '', component: ConfigurationComponent,
    children: [
      {
        path: 'general', component: GeneralComponent
      },
      {
        path: 'designation', component: DesignationListingComponent
      },
      {
        path: '', redirectTo: 'general', pathMatch: 'full'
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationRoutingModule { }
