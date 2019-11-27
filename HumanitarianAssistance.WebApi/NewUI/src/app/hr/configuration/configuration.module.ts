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
import { MatPaginatorModule } from '@angular/material/paginator';
import { GeneralComponent } from './components/general/general.component';
import { EducationDegreeComponent } from './components/education-degree/education-degree.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { AddEducationDegreeComponent } from './components/education-degree/add-education-degree/add-education-degree.component';

@NgModule({
  declarations: [
    AddDesignationComponent,
    DesignationListingComponent,
    EntryComponentComponent,
    ConfigurationComponent,
    GeneralComponent,
    EducationDegreeComponent,
    AddEducationDegreeComponent,
  ],
  imports: [
    CommonModule,
    ConfigurationRoutingModule,
    MatSidenavModule,
    MatCardModule,
    MatTabsModule,
    MatDialogModule,
    MatExpansionModule,
    MatPaginatorModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDividerModule,
    TextFieldModule,
    FormsModule,
    ShareLayoutModule,
    SubHeaderTemplateModule,
    LibraryModule
  ],
  entryComponents: [AddDesignationComponent, AddEducationDegreeComponent]
})
export class ConfigurationModule { }
