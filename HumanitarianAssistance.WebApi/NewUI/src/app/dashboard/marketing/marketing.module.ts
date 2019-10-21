import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MarketingComponent } from './marketing.component';
import { MarketingJobsComponent } from './marketing-jobs/marketing-jobs.component';
import { ContractsComponent } from './contracts/contracts.component';
import { BroadcastPolicyComponent } from './broadcast-policy/broadcast-policy.component';
import { SchedulerComponent } from './scheduler/scheduler.component';
import { ModuleExportModule } from '../../shared/module-export.module';
import { JobDetailsComponent } from './marketing-jobs/job-details/job-details.component';
import { MarketingRoutingModule } from './marketing-routing.module';
import { ContractDetailsComponent } from './contracts/contract-details/contract-details.component';
import { BroadcastPolicyDetailsComponent } from './broadcast-policy/broadcast-policy-details/broadcast-policy-details.component';
import { BroadcastPolicyService } from './broadcast-policy/service/broadcast-policy.service';
import { ContractsService } from './contracts/service/contracts.service';
import { MarketingJobsService } from './marketing-jobs/service/marketing-jobs.service';
import { SchedulerService } from './scheduler/service/scheduler.service';
import { ActivityTypeComponent } from './master-pages/activity-type/activity-type.component';
import { MediaCategoryComponent } from './master-pages/media-category/media-category.component';
import { TimeCategoryComponent } from './master-pages/time-category/time-category.component';
import { PhaseComponent } from './master-pages/phase/phase.component';
import { MediumComponent } from './master-pages/medium/medium.component';
import { NatureComponent } from './master-pages/nature/nature.component';
import { MatrixComponent } from './master-pages/matrix/matrix/matrix.component';
import { QualityComponent } from './master-pages/quality/quality.component';
import { UnitRateComponent } from './master-pages/unit-rate/unit-rate.component';
import { ClientsComponent } from './clients/clients.component';
import { ClientDetailsComponent } from './clients/client-details/client-details.component';
import { AutoCompleteModule } from 'primeng/primeng';
import { CategoryDetailComponent } from './master-pages/media-category/category-detail/category-detail.component';
import { MediumDetailsComponent } from './master-pages/medium/medium-details/medium-details.component';
import { NautreDetailsComponent } from './master-pages/nature/nautre-details/nautre-details.component';
import { PhaseDetailsComponent } from './master-pages/phase/phase-details/phase-details.component';
import { TimeCategoryDetailsComponent } from './master-pages/time-category/time-category-details/time-category-details.component';
import { QualityDetailsComponent } from './master-pages/quality/quality-details/quality-details.component';
import { ActivitytypeDetailsComponent } from './master-pages/activity-type/activitytype-details/activitytype-details.component';
import { JobAddComponent } from './marketing-jobs/job-add/job-add.component';
import { ContractApprovalComponent } from './contracts/contract-approval/contract-approval.component';
import { ProducerComponent } from './master-pages/producer/producer.component';
import { ProducerDetailsComponent } from './master-pages/producer/producer-details/producer-details.component';
import { PolicyAddComponent } from './broadcast-policy/policy-add/policy-add.component';
import { SchedulerAddComponent } from './scheduler/scheduler-add/scheduler-add.component';
import { FormsModule } from '@angular/forms';
import { FlatpickrModule } from 'angularx-flatpickr';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { SchedulerModule } from 'angular-calendar-scheduler';
import { AmazingTimePickerModule } from 'amazing-time-picker';
import { StartEndTimeComponent } from './broadcast-policy/start-end-time/start-end-time.component';
import { ChannelComponent } from './master-pages/channel/channel.component';
import { ChannelDetailComponent } from './master-pages/channel/channel-detail/channel-detail.component';
import { PlayoutMinutesComponent } from './scheduler/playout-minutes/playout-minutes.component';


@NgModule({
  imports: [
    CommonModule,
    ModuleExportModule,
    MarketingRoutingModule,
    AutoCompleteModule,
    FormsModule,
    FlatpickrModule.forRoot(),
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory
    }),
    NgbModalModule,
    AmazingTimePickerModule,
    SchedulerModule,
  ],
  declarations: [
    MarketingComponent,
    MarketingJobsComponent,
    ContractsComponent,
    BroadcastPolicyComponent,
    SchedulerComponent,
    JobDetailsComponent,
    ContractDetailsComponent,
    BroadcastPolicyDetailsComponent,
    ActivityTypeComponent,
    MediaCategoryComponent,
    TimeCategoryComponent,
    PhaseComponent,
    MediumComponent,
    NatureComponent,
    MatrixComponent,
    QualityComponent,
    UnitRateComponent,
    ClientsComponent,
    ClientDetailsComponent,
    CategoryDetailComponent,
    MediumDetailsComponent,
    NautreDetailsComponent,
    PhaseDetailsComponent,
    TimeCategoryDetailsComponent,
    QualityDetailsComponent,
    ActivitytypeDetailsComponent,
    JobAddComponent,
    ContractApprovalComponent,
    ProducerComponent,
    ProducerDetailsComponent,
    PolicyAddComponent,
    SchedulerAddComponent,
    SchedulerComponent,
    StartEndTimeComponent,
    ChannelComponent,
    ChannelDetailComponent,
    PlayoutMinutesComponent
  ],
  providers: [
    BroadcastPolicyService,
    ContractsService,
    MarketingJobsService,
    SchedulerService,
    DatePipe
  ],
  exports: [],
  entryComponents: [
    JobAddComponent,
    ContractApprovalComponent,
    PolicyAddComponent,
    SchedulerAddComponent,
    StartEndTimeComponent,
    PlayoutMinutesComponent
  ]
})
export class MarketingModule {}
