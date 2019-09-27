import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StoreRoutingModule } from './store-routing.module';
import { PurchaseFiltersComponent } from './components/purchase-filters/purchase-filters.component';
import { PurchaseListComponent } from './components/purchase-list/purchase-list.component';
import { AddPurchaseComponent } from './components/add-purchase/add-purchase.component';
import { AddProcurementsComponent } from './components/add-procurements/add-procurements.component';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import { MatIconModule, MatMenuModule, MatSidenavModule, MatCardModule, MatButtonModule, MatInputModule, MatNativeDateModule, MatDividerModule } from '@angular/material';
import { ShareLayoutModule } from '../shared/share-layout.module';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { LibraryModule, SubHeaderTemplateModule } from 'projects/library/src/public_api';
import { SubHeaderTemplateComponent } from 'projects/library/src/sub-header-template/sub-header-template.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatPaginatorModule } from '@angular/material/paginator';



@NgModule({
  declarations: [
    PurchaseFiltersComponent,
    PurchaseListComponent,
    AddPurchaseComponent,
    AddProcurementsComponent,
    EntryComponentComponent
    // components
  //  DbsidebarComponent,
   // DbheaderComponent,
   // DbfooterComponent
  ],
  imports: [
    CommonModule,
    StoreRoutingModule,
    ReactiveFormsModule,
    // Material
    MatMenuModule,
    MatIconModule,
    MatSidenavModule,
    MatCardModule,
    MatButtonModule,
    ShareLayoutModule,
    SubHeaderTemplateModule,
    LibraryModule,
    MatDatepickerModule,
    MatInputModule,
    MatPaginatorModule,
    MatDividerModule
    // MatRangeDatepickerModule,
    // MatNativeDateModule



  ],
  entryComponents: [AddProcurementsComponent]
})
export class StoreModule { }
