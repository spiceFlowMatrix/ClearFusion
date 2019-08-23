import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubHeaderTemplateComponent } from './sub-header-template.component';

@NgModule({
  declarations: [SubHeaderTemplateComponent],
  imports: [
    CommonModule
  ],
  exports: [
    SubHeaderTemplateComponent
  ]
})
export class SubHeaderTemplateModule { }
