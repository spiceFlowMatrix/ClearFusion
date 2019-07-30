import { ProjectListComponent } from './project-list/project-list.component';
import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ProjectManagementComponent } from './project-management.component';
import { ModuleExportModule } from '../../shared/module-export.module';
import { ProjectManagementRoutingModule } from './project-management-routing.module';
import { ProjectDetailsComponent } from './project-list/project-details/project-details.component';
import { ProjectListService } from './project-list/service/project-list.service';
import { MetadataComponent } from './project-list/metadata/metadata.component';
import { ProposalComponent } from './project-list/proposal/proposal.component';
import { BiddingCommitteeComponent } from './project-list/bidding-committee/bidding-committee.component';
import { BudgetlinesComponent } from './project-list/budgetlines/budgetlines.component';
import { ChatboxComponent } from './project-list/project-details/chatbox/chatbox.component';
import { ProgramAreaSectorComponent } from './project-list/project-details/program-area-sector/program-area-sector.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { MultiSelectModule } from 'primeng/multiselect';
import { TableModule } from 'primeng/table';
import { AcceptProposalComponent } from './project-list/accept-proposal/accept-proposal.component';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { FileDropModule } from 'ngx-file-drop';
import { CriteriaEvaluationComponent } from './project-list/criteria-evaluation/criteria-evaluation.component';
import { MatPaginatorModule } from '@angular/material/paginator';

import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { BudgetLineListingComponent } from './project-list/budgetlines/budget-line-listing/budget-line-listing.component';
import { BudgetLineDetailsComponent } from './project-list/budgetlines/budget-line-details/budget-line-details.component';
import { AddbudgetLineComponent } from './project-list/budgetlines/addbudget-line/addbudget-line.component';
import { ProjectJobsComponent } from './project-list/project-jobs/project-jobs.component';

import { AddprojectJobsComponent } from './project-list/project-jobs/addproject-jobs/addproject-jobs.component';
import { ProjectJobsDetailsComponent } from './project-list/project-jobs/project-jobs-details/project-jobs-details.component';
import { ProjectActivitiesComponent } from './project-list/project-activities/project-activities.component';
import { ProjectActivityListingComponent } from './project-list/project-activities/project-activity-listing/project-activity-listing.component';
import { ProjectPlanningComponent } from './project-list/project-activities/project-activity-phase/project-planning/project-planning.component';
import { ProjectActivityFilteringComponent } from './project-list/project-activities/project-activity-filtering/project-activity-filtering.component';
import { ProjectActivityAddComponent } from './project-list/project-activities/project-activity-add/project-activity-add.component';
import { ProjectActivityDocumentsComponent } from './project-list/project-activities/project-activity-documents/project-activity-documents.component';
import {MatSliderModule} from '@angular/material/slider';
import { ProjectActivityPhaseComponent } from './project-list/project-activities/project-activity-phase/project-activity-phase.component';
import { ProjectActivitiesService } from './project-list/project-activities/service/project-activities.service';
import { ProjectDashboardComponent } from './project-dashboard/project-dashboard.component';
import { ProjectCashFlowComponent } from './project-cash-flow/project-cash-flow.component';
import {ChartModule} from 'primeng/chart';
import {ToastModule} from 'primeng/toast';

import {MessageService} from 'primeng/api';
import { ProjectCashFlowService } from './project-cash-flow/project-cash-flow.service';
import { FileUploadDemoComponent } from './file-upload-demo/file-upload-demo.component';
import { ProposalReportComponent } from './proposal-report/proposal-report.component';
import { DonorMasterComponent } from './project-donor/donor-master/donor-master.component';
import { DonorMasterListComponent } from './project-donor/donor-master-list/donor-master-list.component';
import { ProposalReportService } from './proposal-report/proposal-report.service';
import { ProposalReportFilterComponent } from './proposal-report/proposal-report-filter/proposal-report-filter.component';
import {DragDropModule} from '@angular/cdk/drag-drop';
import { MatBadgeModule } from '@angular/material/badge';
import { ProjectDetailComponent } from './project-list/project-details/project-detail/project-detail.component';
import { SubActivitiesComponent } from './project-list/project-activities/project-activity-phase/sub-activities/sub-activities.component';
import { MonitoringComponent } from './project-list/project-activities/project-activity-phase/monitoring/monitoring.component';
import { ProjectIndicatorsComponent } from './project-indicators/project-indicators.component';
import { ProjectIndicatorDetailComponent } from './project-indicators/project-indicator-detail/project-indicator-detail.component';
import { PeopleComponent } from './project-list/project-details/people/people.component';
import { OpportunityControlComponent } from './project-list/project-details/people/opportunity-control/opportunity-control.component';
import { LogisticsControlComponent } from './project-list/project-details/people/logistics-control/logistics-control.component';
import { ActivitiesControlComponent } from './project-list/project-details/people/activities-control/activities-control.component';
import { HiringControlComponent } from './project-list/project-details/people/hiring-control/hiring-control.component';
import { PeopleAddFormComponent } from './project-list/project-details/people/people-add-form/people-add-form.component';
import { MonitoringReviewComponent } from './project-list/project-activities/project-activity-phase/monitoring/monitoring-review/monitoring-review.component';
import { Ng5SliderModule } from 'ng5-slider';
import { AddSubActivitiesComponent } from './project-list/project-activities/project-activity-phase/add-sub-activities/add-sub-activities.component';
import { AddExtensionsComponent } from './project-list/project-activities/project-activity-phase/add-extensions/add-extensions.component';
import { BudgetLineDocumentsComponent } from './project-list/budgetlines/budget-line-documents/budget-line-documents.component';
import { BudgetLineImportPopupLoaderComponent } from './project-list/budgetlines/budget-line-import-popup-loader/budget-line-import-popup-loader.component';
import { ListingDeleteModule } from './project-list/project-details/people/component/listing-delete/listing-delete.module';
import { PipeExportModule } from 'src/app/shared/pipes/pipe-export/pipe-export.module';
import { HiringRequestsComponent } from './project-list/hiring-requests/hiring-requests.component';
import { AddHiringRequestsComponent } from './project-list/hiring-requests/add-hiring-requests/add-hiring-requests.component';
import { HiringRequestDetailsComponent } from './project-list/hiring-requests/hiring-request-details/hiring-request-details.component';
import { HiringRequestsListingComponent } from './project-list/hiring-requests/hiring-requests-listing/hiring-requests-listing.component';
import { AddCandidateDaialogComponent } from './project-list/hiring-requests/add-candidate-daialog/add-candidate-daialog.component';
import { EditCandidateDetailDialogComponent } from './project-list/hiring-requests/edit-candidate-detail-dialog/edit-candidate-detail-dialog.component';
import { ProjectOtherDetailPdfService } from './project-list/project-details/program-area-sector/project-other-detail-pdf.service';
import { MatTabsModule } from '@angular/material/tabs';

@NgModule({
  imports: [
    CommonModule,
    ModuleExportModule,
    PipeExportModule,
    ProjectManagementRoutingModule,
    AutoCompleteModule,
    TableModule,
    MultiSelectModule,
    CalendarModule,
    DropdownModule,
    FileDropModule,
    MatPaginatorModule,
    MatAutocompleteModule,
    MatSliderModule,
    ChartModule,
    ToastModule,
    DragDropModule,
    MatBadgeModule,
    Ng5SliderModule,
    ListingDeleteModule,
    MatTabsModule
    
  ],
  declarations: [
    ProjectManagementComponent,
    ProjectListComponent,
    ProjectDetailsComponent,
    MetadataComponent,
    ProposalComponent,
    BiddingCommitteeComponent,
    BudgetlinesComponent,
    ChatboxComponent,
    DonorMasterComponent,
    DonorMasterListComponent,
    ProgramAreaSectorComponent,
    AcceptProposalComponent,
    CriteriaEvaluationComponent,
    BudgetLineListingComponent,
    BudgetLineDetailsComponent,
    AddbudgetLineComponent,
    ProjectJobsComponent,

    AddprojectJobsComponent,

    ProjectJobsDetailsComponent,
    ProjectActivitiesComponent,
    ProjectActivityListingComponent,
    ProjectPlanningComponent,
    ProjectActivityFilteringComponent,
    ProjectActivityAddComponent,
    ProjectActivityDocumentsComponent,
    ProjectActivityPhaseComponent,
    ProjectDashboardComponent,
    ProjectCashFlowComponent,
    FileUploadDemoComponent,
    ProposalReportComponent,
    ProposalReportFilterComponent,
    ProjectDetailComponent,
    SubActivitiesComponent,
    MonitoringComponent,
    ProjectIndicatorsComponent,
    ProjectIndicatorDetailComponent,
    PeopleComponent,
    OpportunityControlComponent,
    LogisticsControlComponent,
    ActivitiesControlComponent,
    HiringControlComponent,
    PeopleAddFormComponent,
    MonitoringReviewComponent,
    AddSubActivitiesComponent,
    AddExtensionsComponent,
    BudgetLineDocumentsComponent,
    BudgetLineImportPopupLoaderComponent,
    HiringRequestsComponent,
    AddHiringRequestsComponent,
    HiringRequestDetailsComponent,
    HiringRequestsListingComponent,
    AddCandidateDaialogComponent,
    EditCandidateDetailDialogComponent

  ],
  providers: [
    DatePipe,
    ProjectListService,
    ProjectActivitiesService,
    ProjectCashFlowService,
    ProposalReportService,
    MessageService,

    //pdf
    ProjectOtherDetailPdfService
  ],
  entryComponents: [
    AddbudgetLineComponent,
    AddprojectJobsComponent,
    ProjectJobsDetailsComponent,
    ProjectActivityDocumentsComponent,
    ProjectActivityAddComponent,
    MonitoringReviewComponent,
    AddSubActivitiesComponent,
    AddExtensionsComponent,
    BudgetLineImportPopupLoaderComponent,
    AddHiringRequestsComponent,
    AddCandidateDaialogComponent,
    EditCandidateDetailDialogComponent
  ]
})
export class ProjectManagementModule {}
