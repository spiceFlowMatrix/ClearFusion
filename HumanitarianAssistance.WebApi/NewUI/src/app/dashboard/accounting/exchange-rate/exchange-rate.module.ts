import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExchangeRateRoutingModule } from './exchange-rate-routing.module';
import { ExchangeRateComponent } from './exchange-rate.component';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';

import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ExchangeRateListingComponent } from './exchange-rate-listing/exchange-rate-listing.component';
import { ExchangeRateAddComponent } from './exchange-rate-add/exchange-rate-add.component';
import { ExchangeRateDetailComponent } from './exchange-rate-detail/exchange-rate-detail.component';
import { VerifyExchangeRateComponent } from './verify-exchange-rate/verify-exchange-rate.component';
import { ExchangeRateService } from './exchange-rate-listing/exchange-rate.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatStepperModule } from '@angular/material/stepper';

@NgModule({
  declarations: [
    ExchangeRateComponent,
    ExchangeRateListingComponent,
    ExchangeRateAddComponent,
    ExchangeRateDetailComponent,
    VerifyExchangeRateComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ExchangeRateRoutingModule,

    // material
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatPaginatorModule,
    MatDialogModule,
    MatDatepickerModule,
    MatIconModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    MatStepperModule,
  ],
  providers: [
    ExchangeRateService,
  ],
  exports: [],
  entryComponents: [
    ExchangeRateAddComponent,
    VerifyExchangeRateComponent,
  ]
})
export class ExchangeRateModule { }
