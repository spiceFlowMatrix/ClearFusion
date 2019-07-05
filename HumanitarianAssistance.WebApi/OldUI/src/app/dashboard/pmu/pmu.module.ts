import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProjectsComponent } from './projects/projects.component';
import { ImplementationComponent } from './implementation/implementation.component';
import { MonitoringComponent } from './monitoring/monitoring.component';
import { PmuRoutingModule } from './pmu-routing.module';
import { PmuComponent } from './pmu.component';
import {
  DxDataGridComponent,
  DxDataGridModule,
  DxSelectBoxModule,
  DxCheckBoxModule,
  DxNumberBoxModule,
  DxButtonModule,
  DxFormModule,
  DxFormComponent,
  DxPopupModule,
  DxTemplateModule,
  DxDropDownBoxModule,
  DxTextAreaModule,
  DxDateBoxModule,
  DxFileUploaderModule,
  DxRadioGroupModule,
  DxTabsModule,
  DxPopoverModule,
  DxListModule,
  DxTreeViewModule
} from 'devextreme-angular';
import { PmuService } from './pmu.service';
import { AddNewComponent } from './projects/add-new/add-new.component';
import { DocumentsComponent } from './projects/documents/documents.component';
import { ProjectActivitiesComponent } from './projects/project-activities/project-activities.component';
import { QuestionsComponent } from './projects/questions/questions.component';
import { ProjectsService } from './projects/projects.service';
import { EditComponent } from './projects/edit/edit.component';
import { ProjectActivitiesMonitoringComponent } from './monitoring/project-activities-monitoring/project-activities-monitoring.component';
import { BeneficiariesMonitoringComponent } from './monitoring/beneficiaries-monitoring/beneficiaries-monitoring.component';
import { BeneficiariesComponent } from './projects/beneficiaries/beneficiaries.component';
import { HrService } from '../hr/hr.service';
import { CodeService } from '../code/code.service';
import { TaskActivityComponent } from './task-activity/task-activity.component';
import { AccountsService } from '../accounts/accounts.service';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import {
  TranslateModule,
  TranslateLoader,
  TranslateService
} from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PmuRoutingModule,
    DxDataGridModule,
    DxSelectBoxModule,
    DxCheckBoxModule,
    DxNumberBoxModule,
    DxButtonModule,
    DxFormModule,
    DxPopupModule,
    DxTemplateModule,
    DxDropDownBoxModule,
    DxTextAreaModule,
    DxDateBoxModule,
    DxTabsModule,
    DxFileUploaderModule,
    DxRadioGroupModule,
    DxPopoverModule,
    DxListModule,
    DxTreeViewModule,
    HttpClientModule,
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)',
      backdropBorderRadius: '4px',
      primaryColour: '#31c3aa',
      secondaryColour: '#000',
      tertiaryColour: '#a129'
    }),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  declarations: [
    ImplementationComponent,
    MonitoringComponent,
    PmuComponent,
    AddNewComponent,
    BeneficiariesComponent,
    DocumentsComponent,
    ProjectsComponent,
    ProjectActivitiesComponent,
    QuestionsComponent,
    EditComponent,
    ProjectActivitiesMonitoringComponent,
    BeneficiariesMonitoringComponent,
    TaskActivityComponent
  ],
  providers: [
    PmuService,
    ProjectsService,
    HrService,
    CodeService,
    AccountsService,
    TranslateService,
    HttpClient
  ]
})
export class PMUModule {}

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
