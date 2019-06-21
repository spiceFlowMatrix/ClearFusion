import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule, MatSidenavModule, MatToolbarModule, MatCardModule, MatButtonModule, } from '@angular/material';
import { DbsidebarComponent } from '../shared/dbsidebar/dbsidebar.component';
import { DbheaderComponent } from '../shared/dbheader/dbheader.component';
import { DbfooterComponent } from '../shared/dbfooter/dbfooter.component';

@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,

    // Material
    MatMenuModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
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
