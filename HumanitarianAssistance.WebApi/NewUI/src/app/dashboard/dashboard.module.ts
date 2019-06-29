import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DbsidebarComponent } from '../shared/dbsidebar/dbsidebar.component';
import { DbheaderComponent } from '../shared/dbheader/dbheader.component';
import { DbfooterComponent } from '../shared/dbfooter/dbfooter.component';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';

@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,

    // Material
    MatMenuModule,
    MatIconModule,
    MatSidenavModule,
    MatCardModule,
    MatButtonModule,
  ],
  declarations: [
    DashboardComponent,

    // components
    DbsidebarComponent,
    DbheaderComponent,
    DbfooterComponent
  ]
})
export class DashboardModule { }
