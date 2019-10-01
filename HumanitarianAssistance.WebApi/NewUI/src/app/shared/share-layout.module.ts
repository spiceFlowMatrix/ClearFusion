import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DbsidebarComponent } from './dbsidebar/dbsidebar.component';
import { DbheaderComponent } from './dbheader/dbheader.component';
import { DbfooterComponent } from './dbfooter/dbfooter.component';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material';

@NgModule({
  declarations: [
    DbsidebarComponent,
    DbheaderComponent,
    DbfooterComponent
  ],
  imports: [
    CommonModule,
    MatButtonModule,
    RouterModule
  ],
  exports:[ 
    DbsidebarComponent,
    DbheaderComponent,
    DbfooterComponent]
})
export class ShareLayoutModule { }
