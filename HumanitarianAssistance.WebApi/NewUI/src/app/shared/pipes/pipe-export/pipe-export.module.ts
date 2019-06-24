import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CurrencyCodePipe } from '../currency-code.pipe';

@NgModule({
  declarations: [
    CurrencyCodePipe
  ],
  imports: [
    CommonModule
  ],
  exports: [
    CurrencyCodePipe
  ]
})
export class PipeExportModule { }
