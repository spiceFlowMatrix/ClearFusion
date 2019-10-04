import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StoreRoutingModule } from './store-routing.module';
import { PurchaseFiltersComponent } from './components/purchase-filters/purchase-filters.component';
import { PurchaseListComponent } from './components/purchase-list/purchase-list.component';
import { AddPurchaseComponent } from './components/add-purchase/add-purchase.component';
import { AddProcurementsComponent } from './components/add-procurements/add-procurements.component';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import { MatIconModule, MatMenuModule, MatSidenavModule, MatCardModule, MatButtonModule,
  MatInputModule, MatDividerModule, MatListModule } from '@angular/material';
import { ShareLayoutModule } from '../shared/share-layout.module';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { LibraryModule, SubHeaderTemplateModule } from 'projects/library/src/public_api';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatPaginatorModule } from '@angular/material/paginator';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatDialogModule } from '@angular/material/dialog';
import { PurchaseFiledConfigComponent } from './components/purchase-filed-config/purchase-filed-config.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DocumentUploadComponent } from './components/document-upload/document-upload.component';



@NgModule({
  declarations: [
    PurchaseFiltersComponent,
    PurchaseListComponent,
    AddPurchaseComponent,
    AddProcurementsComponent,
    EntryComponentComponent,
    PurchaseFiledConfigComponent,
    DocumentUploadComponent
    // components
  //  DbsidebarComponent,
   // DbheaderComponent,
   // DbfooterComponent
  ],
  imports: [
    CommonModule,
    StoreRoutingModule,
    ReactiveFormsModule,
    FormsModule,
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
    MatDividerModule,
    MatCheckboxModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatListModule
    // MatRangeDatepickerModule,
    // MatNativeDateModule



  ],
  entryComponents: [AddProcurementsComponent]
})
export class StoreModule { }
