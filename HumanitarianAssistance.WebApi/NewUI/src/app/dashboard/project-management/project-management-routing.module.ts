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
import { ProjectIndicatorsComponent } from 'src/app/dashboard/project-management/project-indicators/project-indicators.component';
import { PeopleComponent } from './project-list/project-details/people/people.component';
import { HiringRequestsComponent } from './project-list/hiring-requests/hiring-requests.component';
import { HiringRequestsListingComponent } from './project-list/hiring-requests/hiring-requests-listing/hiring-requests-listing.component';

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
        path: 'project-indicators',
        component: ProjectIndicatorsComponent,
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
            data: {
              module: moduleId,
              page: projectPagesMaster.CriteriaEvaluation
            }
          },
          {
            path: 'proposal',
            component: ProposalComponent,
            data: {
              module: moduleId,
              page: projectPagesMaster.Proposal
            }
          }, {
            path: 'project-jobs',
            component: ProjectJobsComponent,
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectJobs
            }
          },
          {
            path: 'budget-lines',
            component: BudgetLineListingComponent,
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectBudgetLine
            }
          },
          {
            path: 'project-activities',
            component: ProjectActivitiesComponent,
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectActivities
            }
          },
          {
            path: 'people',
            component: PeopleComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.ProjectPeople
            }
          },
          {
            path: 'hiring-request',
            component: HiringRequestsComponent,
            canActivate: [RoleGuardService],
            data: {
              module: moduleId,
              page: projectPagesMaster.HiringRequests
            }
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
export class ProjectManagementRoutingModule {}
