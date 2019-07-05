import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GainLossReportRoutingModule } from './gain-loss-report-routing.module';
import { MultiSelectListComponent } from './multi-select-list/multi-select-list.component';
import { GainLossReportComponent } from './gain-loss-report.component';


import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';

import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { TooltipModule } from 'primeng/primeng';
import { LibraryModule } from 'projects/library/src/public_api';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { GainLossReportService } from './gain-loss-report.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PipeExportModule } from 'src/app/shared/pipes/pipe-export/pipe-export.module';

@NgModule({
  declarations: [
    GainLossReportComponent,
    MultiSelectListComponent
  ],
  imports: [
    CommonModule,
    GainLossReportRoutingModule,
    FormsModule,
    ReactiveFormsModule,

    PipeExportModule,
    // Custom Modules
    LibraryModule,

    // material
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatPaginatorModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatSelectModule,
    NgxMatSelectSearchModule,
    MatProgressSpinnerModule,
    TooltipModule
  ],
  // exports: [
  //   CurrencyCodePipe
  // ],
  entryComponents: [
    MultiSelectListComponent
  ],
  providers: [
    // CurrencyCodePipe,
    GainLossReportService
  ]
})
export class GainLossReportModule { }
