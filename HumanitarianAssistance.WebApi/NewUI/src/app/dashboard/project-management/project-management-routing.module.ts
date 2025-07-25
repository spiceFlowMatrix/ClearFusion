import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectManagementComponent } from './project-management.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectDetailsComponent } from './project-list/project-details/project-details.component';
import { ProgramAreaSectorComponent } from './project-list/project-details/program-area-sector/program-area-sector.component';
import { BudgetlinesComponent } from './project-list/budgetlines/budgetlines.component';
import { ProjectJobsComponent } from './project-list/project-jobs/project-jobs.component';
import { RoleGuardService } from 'src/app/shared/services/role-guard';
import {
  ApplicationModule,
  projectPagesMaster
} from 'src/app/shared/applicationpagesenum';
import { ProjectDashboardComponent } from './project-dashboard/project-dashboard.component';
import { ProjectCashFlowComponent } from './project-cash-flow/project-cash-flow.component';
import { FileUploadDemoComponent } from './file-upload-demo/file-upload-demo.component';
import { ProposalReportComponent } from './proposal-report/proposal-report.component';
import { DonorMasterListComponent } from './project-donor/donor-master-list/donor-master-list.component';
import { CriteriaEvaluationComponent } from './project-list/criteria-evaluation/criteria-evaluation.component';
import { ProposalComponent } from './project-list/proposal/proposal.component';
import { ProjectDetailComponent } from './project-list/project-details/project-detail/project-detail.component';
import { BudgetLineListingComponent } from './project-list/budgetlines/budget-line-listing/budget-line-listing.component';
import { ProjectActivitiesComponent } from './project-list/project-activities/project-activities.component';
import { PeopleComponent } from './project-list/project-details/people/people.component';
import { ProjectIndicatorsComponent } from './project-list/project-indicators/project-indicators.component';
import { JobDetailComponent } from './project-hiring/job-detail/job-detail.component';
import { LogisticRequestsComponent } from './project-logistics/logistic-requests/logistic-requests.component';
import { LogisticRequestDetailsComponent } from './project-logistics/logistic-request-details/logistic-request-details.component';
import { RequestDetailComponent } from './project-hiring/request-detail/request-detail.component';
import { HiringRequestsComponent } from './project-hiring/hiring-requests/hiring-requests.component';
import { InterviewDetailComponent } from './project-hiring/interview-detail/interview-detail.component';
import { AddLogisticRequestComponent } from './project-logistics/add-logistic-request/add-logistic-request.component';
import { SubmitPurchaseListComponent } from './project-logistics/submit-purchase-list/submit-purchase-list.component';

const moduleId: number = ApplicationModule.Projects;

const routes: Routes = [
  {
    path: '',
    component: ProjectManagementComponent,
    children: [
      {
        path: 'project-dashboard',
        component: ProjectDashboardComponent,
        data: {
          module: moduleId,
          page: projectPagesMaster.ProjectDashboard
        }
      },
      {
        path: 'my-projects',
        component: ProjectListComponent,
        canActivate: [RoleGuardService],
        data: {
          module: moduleId,
          page: projectPagesMaster.MyProjects
        }
      },

      {
        path: 'project-donor',
        component: DonorMasterListComponent,
        canActivate: [RoleGuardService],
        data: {
          module: moduleId,
          page: projectPagesMaster.Donors
        }
      },
      {
        path: 'project-cash-flow',
        component: ProjectCashFlowComponent,
        canActivate: [RoleGuardService],
        data: {
          module: moduleId,
          page: projectPagesMaster.ProjectCashFlow
        }
      },
      {
        path: 'proposal-report',
        component: ProposalReportComponent,
        canActivate: [RoleGuardService],
        data: {
          module: moduleId,
          page: projectPagesMaster.ProposalReport
        }
      },

      {
        path: 'file-upload-demo',
        component: FileUploadDemoComponent
      },


      { path: 'programAreaSector/:id', component: ProgramAreaSectorComponent },
      {
        path: 'budgetLines',
        component: BudgetlinesComponent,
        pathMatch: 'full'
      },
      {
        path: 'my-project/:id',
        component: ProjectDetailsComponent,
        children: [
          { path: '', redirectTo: 'detail', pathMatch: 'full' },
          {
            path: 'detail',
            component: ProjectDetailComponent,
          },
          {
            path: 'criteria-evaluation',
            component: CriteriaEvaluationComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.CriteriaEvaluation
            }
          },
          {
            path: 'proposal',
            component: ProposalComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.Proposal
            }
          },
          {
            path: 'project-jobs',
            component: ProjectJobsComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectJobs
            }
          },
          {
            path: 'budget-lines',
            component: BudgetLineListingComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectBudgetLine
            }
          },
          {
            path: 'project-activities',
            component: ProjectActivitiesComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectActivities
            }
          },
          {
            path: 'people',
            component: PeopleComponent,
            //  canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectPeople
            }
          },
          {
            path: 'hiring-request',
            loadChildren:'./project-hiring/project-hiring.module#ProjectHiringModule',
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.HiringRequests
            }

          },

          // {
          //   path: 'job-detail',
          //   component: JobDetailComponent,
          //   canActivate: [RoleGuardService],
          //   data: {
          //     module: moduleId,
          //     page: projectPagesMaster.HiringRequests
          //   }
          // },

          {
            path: 'project-indicators',
            component: ProjectIndicatorsComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectIndicators
            }
          },
          // {
          //   path: 'logistic-requests',
          //   loadChildren: './project-logistics/project-logistics.module#ProjectLogisticsModule'
          // }
          {
            path: 'logistic-requests',
            component: LogisticRequestsComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectIndicators
            }
          },
          {
            path: 'logistic-requests/new-request',
            component: AddLogisticRequestComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectIndicators
            }
          },
          {
            path: 'logistic-requests/:id',
            component: LogisticRequestDetailsComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectIndicators
            },
          //   children: [
          //     {
          //       path: '',
          //       component: LogisticRequestDetailsComponent
          //     },
          //     {
          //     path: 'submit-purchase',
          //     component: SubmitPurchaseListComponent
          //   }
          // ]
          },
          {
            path: 'logistic-requests/:id/submit-purchase',
            component: SubmitPurchaseListComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectIndicators
            },
          }
        ]
      }

    ]
  }
  // { path: 'projects', component: ProjectListComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectManagementRoutingModule { }
