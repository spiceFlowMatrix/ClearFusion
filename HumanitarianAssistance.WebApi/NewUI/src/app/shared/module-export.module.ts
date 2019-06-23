import { NgModule } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LibraryModule } from 'projects/library/src/public_api';
import { DisableControlDirective } from './common-directives/custom-disabled.directive';
import { ProjectPhasePipe } from './pipes/project-phase.pipe';
import { NumberOnlyDirective } from './common-directives/number-only.directive';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatTreeModule } from '@angular/material/tree';
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatMenuModule } from '@angular/material/menu';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatRadioModule } from '@angular/material/radio';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { TooltipModule } from 'primeng/primeng';
import { FileDropModule } from 'ngx-file-drop';

@NgModule({
  declarations: [
    // components
    DisableControlDirective,
    ProjectPhasePipe,
    NumberOnlyDirective,
  ],
  imports: [
    // Custom Modules
    LibraryModule,

    // Modules
    FormsModule,
    ReactiveFormsModule,

    // material
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    MatTabsModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatTreeModule,
    MatSelectModule,
    MatChipsModule,
    MatTooltipModule,
    MatProgressBarModule,
    MatAutocompleteModule,
    MatSlideToggleModule,
    MatRadioModule,
    MatButtonToggleModule,
    MatExpansionModule,
    MatMenuModule,
    MatProgressSpinnerModule,
    NgxMatSelectSearchModule,
    TooltipModule,
    FileDropModule
    // directives
   // NumberOnlyDirective
  ],
  exports: [
    // Custom Modules
    LibraryModule,
    DisableControlDirective,
    NumberOnlyDirective,
    ProjectPhasePipe,

    // Modules
    FormsModule,
    ReactiveFormsModule,

    // material
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatTableModule,
    MatPaginatorModule,
    MatTabsModule,
    MatDialogModule,
    MatSelectModule,
    MatTreeModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatChipsModule,
    MatTooltipModule,
    MatProgressBarModule,
    MatAutocompleteModule,
    MatSlideToggleModule,
    MatRadioModule,
    MatButtonToggleModule,
    MatExpansionModule,
    MatMenuModule,
    MatProgressSpinnerModule,
    NgxMatSelectSearchModule,
    TooltipModule,
    FileDropModule
  ],
  providers: [
    NumberOnlyDirective
  ]
})
export class ModuleExportModule {}
