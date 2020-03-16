import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StoreRoutingModule } from './store-routing.module';
import { PurchaseFiltersComponent } from './components/purchase-filters/purchase-filters.component';
import { PurchaseListComponent } from './components/purchase-list/purchase-list.component';
import { AddPurchaseComponent } from './components/add-purchase/add-purchase.component';
import { AddProcurementsComponent } from './components/add-procurements/add-procurements.component';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import {
  MatIconModule, MatMenuModule, MatSidenavModule, MatCardModule, MatButtonModule,
  MatInputModule, MatDividerModule, MatListModule, MatExpansionModule,
  MatTabsModule, MatSelectModule, MatOptionModule, MatTreeModule, MatAutocompleteModule
} from '@angular/material';
import { ShareLayoutModule } from '../shared/share-layout.module';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { LibraryModule, SubHeaderTemplateModule } from 'projects/library/src/public_api';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { PurchaseFiledConfigComponent } from './components/purchase-filed-config/purchase-filed-config.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DocumentUploadComponent } from './components/document-upload/document-upload.component';
import { VehicleDetailComponent } from './components/vehicle-detail/vehicle-detail.component';
import { GeneratorDetailComponent } from './components/generator-detail/generator-detail.component';
import { VehicleTrackerComponent } from './components/vehicle-tracker/vehicle-tracker.component';
import { GeneratorTrackerComponent } from './components/generator-tracker/generator-tracker.component';
import { GeneratorFiltersComponent } from './components/generator-filters/generator-filters.component';
import { VehicleFiltersComponent } from './components/vehicle-filters/vehicle-filters.component';
import { GeneratorDetailsComponent } from './components/generator-details/generator-details.component';
import { VehicleDetailsComponent } from './components/vehicle-details/vehicle-details.component';
import { AddMilageComponent } from './components/add-milage/add-milage.component';
import { EditVehicleComponent } from './components/edit-vehicle/edit-vehicle.component';
import { EditGeneratorComponent } from './components/edit-generator/edit-generator.component';
import { AddHoursComponent } from './components/add-hours/add-hours.component';
import { LogsComponent } from './components/logs/logs.component';
import { AddDocumentComponent } from './components/document-upload/add-document.component';
import { StoreConfigurationComponent } from './components/store-configuration/store-configuration.component';
import { StoreItemConfigComponent } from './components/store-item-config/store-item-config.component';
import { AddMasterInventoryComponent } from './components/add-master-inventory/add-master-inventory.component';
import { AddItemCategoryComponent } from './components/add-item-category/add-item-category.component';
import { AddItemComponent } from './components/add-item/add-item.component';

import {MatRadioModule} from '@angular/material/radio';
import { ProcurementControlPanelComponent } from './components/procurement-control-panel/procurement-control-panel.component';


@NgModule({
  declarations: [
    PurchaseFiltersComponent,
    PurchaseListComponent,
    AddPurchaseComponent,
    AddProcurementsComponent,
    EntryComponentComponent,
    PurchaseFiledConfigComponent,
    DocumentUploadComponent,
    VehicleDetailComponent,

    GeneratorDetailComponent,
    VehicleTrackerComponent,
    GeneratorTrackerComponent,
    GeneratorFiltersComponent,
    VehicleFiltersComponent,
    GeneratorDetailsComponent,
    VehicleDetailsComponent,
    AddMilageComponent,
    EditVehicleComponent,
    EditGeneratorComponent,
    AddHoursComponent,
    LogsComponent,
    AddDocumentComponent,
    StoreConfigurationComponent,
    StoreItemConfigComponent,
    AddMasterInventoryComponent,
    AddItemCategoryComponent,
    AddItemComponent,
    ProcurementControlPanelComponent
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
    MatListModule,
    MatExpansionModule,
    MatTabsModule,
    MatTreeModule,
    MatSelectModule,
    MatOptionModule,
    MatRadioModule,
    MatAutocompleteModule
    // MatRangeDatepickerModule,
    // MatNativeDateModule



  ],
  entryComponents: [AddProcurementsComponent, AddMilageComponent,
    AddHoursComponent, AddDocumentComponent, AddMasterInventoryComponent, AddItemCategoryComponent, AddItemComponent]
})
export class StoreModule { }
