import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ConfigurationRoutingModule } from './configuration-routing.module';
import { AddDesignationComponent } from './components/add-designation/add-designation.component';
import { DesignationListingComponent } from './components/designation-listing/designation-listing.component';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import { ConfigurationComponent } from './configuration.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatCardModule } from '@angular/material/card';
import { ShareLayoutModule } from 'src/app/shared/share-layout.module';
import { LibraryModule, SubHeaderTemplateModule } from 'projects/library/src/public_api';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MatDividerModule } from '@angular/material/divider';

@NgModule({
  declarations: [
    AddDesignationComponent,
    DesignationListingComponent,
    EntryComponentComponent,
    ConfigurationComponent,
  ],
  imports: [
    CommonModule,
    ConfigurationRoutingModule,
    MatSidenavModule,
    MatCardModule,
    MatTabsModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDividerModule,
    TextFieldModule,
    FormsModule,
    ShareLayoutModule,
    SubHeaderTemplateModule,
    LibraryModule
  ],
  entryComponents: [AddDesignationComponent]
})
export class ConfigurationModule { }
