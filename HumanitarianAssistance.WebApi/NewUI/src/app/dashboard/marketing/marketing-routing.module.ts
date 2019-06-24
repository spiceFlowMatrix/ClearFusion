import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MarketingComponent } from './marketing.component';
import { MarketingJobsComponent } from './marketing-jobs/marketing-jobs.component';
import { ContractsComponent } from './contracts/contracts.component';
import { BroadcastPolicyComponent } from './broadcast-policy/broadcast-policy.component';
import { SchedulerComponent } from './scheduler/scheduler.component';
import { JobDetailsComponent } from './marketing-jobs/job-details/job-details.component';
import { ContractDetailsComponent } from './contracts/contract-details/contract-details.component';
import { BroadcastPolicyDetailsComponent } from './broadcast-policy/broadcast-policy-details/broadcast-policy-details.component';
import { ActivityTypeComponent } from './master-pages/activity-type/activity-type.component';
import { MediaCategoryComponent } from './master-pages/media-category/media-category.component';
import { MediumComponent } from './master-pages/medium/medium.component';
import { TimeCategoryComponent } from './master-pages/time-category/time-category.component';
import { PhaseComponent } from './master-pages/phase/phase.component';
import { NatureComponent } from './master-pages/nature/nature.component';
import { MatrixComponent } from './master-pages/matrix/matrix/matrix.component';
import { QualityComponent } from './master-pages/quality/quality.component';
import { UnitRateComponent } from './master-pages/unit-rate/unit-rate.component';
import { ClientsComponent } from './clients/clients.component';
import { ClientDetailsComponent } from './clients/client-details/client-details.component';
import { MediumDetailsComponent } from './master-pages/medium/medium-details/medium-details.component';
import { NautreDetailsComponent } from './master-pages/nature/nautre-details/nautre-details.component';
import { PhaseDetailsComponent } from './master-pages/phase/phase-details/phase-details.component';
import { TimeCategoryDetailsComponent } from './master-pages/time-category/time-category-details/time-category-details.component';
import { QualityDetailsComponent } from './master-pages/quality/quality-details/quality-details.component';
import { ActivitytypeDetailsComponent } from './master-pages/activity-type/activitytype-details/activitytype-details.component';
import {
  ApplicationPages,
  ApplicationModule,
  marketingPagesMaster
} from '../../shared/applicationpagesenum';
import { ProducerComponent } from './master-pages/producer/producer.component';
import { ProducerDetailsComponent } from './master-pages/producer/producer-details/producer-details.component';
import { ChannelComponent } from './master-pages/channel/channel.component';
import { ChannelDetailComponent } from './master-pages/channel/channel-detail/channel-detail.component';
import { RoleGuardService } from 'src/app/shared/services/role-guard';


const ModuleId = ApplicationModule.Marketing;

const routes: Routes = [
  {
    path: '',
    component: MarketingComponent,
    children: [
      {
        // NOTE: Best Routing for changeing the URL by working on show / Hide component
        // DISCLAMER: Dont change this
        path: 'jobs',
        component: MarketingJobsComponent,
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.Jobs
        }
        // children: [
        //     {
        //         path: ':id', component: JobDetailsComponent
        //     }
        // ]
      },
      {
        path: 'jobs/:id',
        component: JobDetailsComponent
      },
      {
        path: 'contracts',
        component: ContractsComponent,
        children: [
          {
            path: ':id',
            component: ContractDetailsComponent
          }
        ],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.Contracts
        }
      },
      {
        path: 'policy',
        component: BroadcastPolicyComponent
      },
      {
        path: 'policy/:id',
        component: BroadcastPolicyDetailsComponent
      },
      {
        path: 'scheduler',
        component: SchedulerComponent,
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.Scheduler
        }
      },
      // {
      //   path: '',
      //   redirectTo: 'jobs',
      //   pathMatch: 'full'
      // },
      {
        path: 'activityType',
        component: ActivityTypeComponent,
        children: [
          {
            path: 'activityType-details/:id',
            component: ActivitytypeDetailsComponent
          }
        ],
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.ActivityType
        }
      },
      {
        path: 'media-category',
        component: MediaCategoryComponent,
        children: [
          {
            path: 'category-details/:id',
            component: MediaCategoryComponent
          }
        ],
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.MediaCategory
        }
      },
      {
        path: 'medium',
        component: MediumComponent,
        children: [
          {
            path: 'medium-details/:id',
            component: MediumDetailsComponent
          }
        ],
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.Medium
        }
      },
      {
        path: 'nature',
        component: NatureComponent,
        children: [
          {
            path: 'nautre-details/:id',
            component: NautreDetailsComponent
          }
        ],
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.Nature
        }
      },
      {
        path: 'phase',
        component: PhaseComponent,
        children: [
          {
            path: 'phase-details/:id',
            component: PhaseDetailsComponent
          }
        ],
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.Phase
        }
      },
      {
        path: 'time-category',
        component: TimeCategoryComponent,
        children: [
          {
            path: 'timeCategory-details/:id',
            component: TimeCategoryDetailsComponent
          }
        ],
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.TimeCategory
        }
      },
      {
        path: 'unitRateList',
        component: MatrixComponent,
        children: [
          {
            path: '/:id',
            component: UnitRateComponent
          }
        ],
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.UnitRates
        }
      },
      {
        path: 'quality',
        component: QualityComponent,
        children: [
          {
            path: 'quality-details/:id',
            component: QualityDetailsComponent
          }
        ],
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.Quality
        }
      },
      {
        path: 'producer',
        component: ProducerComponent,
        children: [
          {
            path: 'producer-details/:id',
            component: ProducerDetailsComponent
          }
        ],
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.Producer
        }
      },
      {
        path: 'channel',
        component: ChannelComponent,
        children: [
          {
            path: 'channel-details/:id',
            component: ChannelDetailComponent
          }
        ],
        canActivate: [RoleGuardService],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.Channels
        }
      },
      {
        path: 'clients',
        component: ClientsComponent,
        children: [
          {
            path: 'client-details/:id',
            component: ClientDetailsComponent
          }
        ],
        data: {
          module: ModuleId,
          page: marketingPagesMaster.Clients
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule] // important to export
})
export class MarketingRoutingModule {}
