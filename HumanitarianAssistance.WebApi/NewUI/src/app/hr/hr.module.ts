import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HrRoutingModule } from './hr-routing.module';
import { ConfigurationModule } from './configuration/configuration.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HrRoutingModule,
   // ConfigurationModule
  ]
})
export class HrModule {
  entryComponents: [];
}
