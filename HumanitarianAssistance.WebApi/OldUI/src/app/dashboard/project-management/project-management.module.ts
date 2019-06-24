import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectManagementComponent } from './project-management.component';
import { ProjectMainComponent } from './project-main/project-main.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProjectManagementRoutingModule } from './project-management-routing.module';
import {
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
  DxScrollViewModule
} from 'devextreme-angular';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { CodeService } from '../code/code.service';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { CriteriaEvaluationComponent } from './project-main/criteria-evaluation/criteria-evaluation.component';
import { ActivitiesComponent } from './project-main/activities/activities.component';
import { ProcurementItemsComponent } from './project-main/procurement-items/procurement-items.component';
import { BudgetLinesComponent } from './project-main/budget-lines/budget-lines.component';
import { ProjectJobsComponent } from './project-main/project-jobs/project-jobs.component';
import { HiringComponent } from './project-main/hiring/hiring.component';
import { BiddingCommitteeComponent } from './project-main/bidding-committee/bidding-committee.component';
import { ProjectPurchaseComponent } from './project-purchase/project-purchase.component';
import { ProjectHiringComponent } from './project-hiring/project-hiring.component';
import { NewCandidatesComponent } from './project-hiring/new-candidates/new-candidates.component';
import { ExistingEmployeesComponent } from './project-hiring/existing-employees/existing-employees.component';
import { NewOpportunityComponent } from './project-main/new-opportunity/new-opportunity.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ProjectManagementRoutingModule,
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
    DxScrollViewModule,
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
    ProjectMainComponent,
    ProjectManagementComponent,
    CriteriaEvaluationComponent,
    ActivitiesComponent,
    ProcurementItemsComponent,
    BudgetLinesComponent,
    ProjectJobsComponent,
    HiringComponent,
    BiddingCommitteeComponent,
    ProjectPurchaseComponent,
    ProjectHiringComponent,
    NewCandidatesComponent,
    ExistingEmployeesComponent,
    NewOpportunityComponent
  ],
  providers: [CodeService, HttpClient]
})
export class ProjectManagementModule {}

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
