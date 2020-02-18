import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VouchersComponent } from './vouchers.component';
import { VoucherDetailsComponent } from './voucher-details/voucher-details.component';
import { VoucherAddComponent } from './voucher-add/voucher-add.component';
import { VoucherSummaryComponent } from './voucher-details/voucher-summary/voucher-summary.component';
import { VouchersRoutingModule } from './vouchers-routing.module';
import { VoucherService } from './voucher.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';

import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatMenuModule } from '@angular/material/menu';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { TooltipModule } from 'primeng/primeng';
import { LibraryModule, SubHeaderTemplateModule } from 'projects/library/src/public_api';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { DocumentListingComponent } from 'projects/library/src/lib/components/document-listing/document-listing.component';
import { AngularSplitModule } from 'angular-split';
import { VoucherListingComponent } from './components/voucher-listing/voucher-listing.component';
import { MatDividerModule, MatTableModule, MatListModule, MatAutocompleteModule, MatSortModule } from '@angular/material';
import { SatDatepickerModule } from 'saturn-datepicker';
import { AddVoucherComponent } from './components/add-voucher/add-voucher.component';
import { VoucherDetailComponent } from './components/voucher-detail/voucher-detail.component';
import { ModifyTransactionComponent } from './components/modify-transaction/modify-transaction.component';
import { AddDocumentsComponent } from './components/add-documents/add-documents.component';

@NgModule({
  declarations: [

    VouchersComponent,
    VoucherListingComponent,
    VoucherDetailsComponent,
    VoucherAddComponent,
    VoucherSummaryComponent,
    AddVoucherComponent,
    VoucherDetailComponent,
    ModifyTransactionComponent,
    AddDocumentsComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    VouchersRoutingModule,

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
    MatProgressBarModule,
    MatMenuModule,
    MatProgressSpinnerModule,
    TooltipModule,
    SubHeaderTemplateModule,
    MatDividerModule,
    SatDatepickerModule,
    MatTableModule,
    MatListModule,
    MatAutocompleteModule,
    MatSortModule,
    AngularSplitModule.forChild(),
  ],
  providers: [
    VoucherService
  ],
  entryComponents: [
    VoucherAddComponent,
    DocumentListingComponent,
    AddDocumentsComponent
  ]
})
export class VouchersModule { }
