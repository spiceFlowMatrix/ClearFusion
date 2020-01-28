import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConfigurationComponent } from './configuration.component';
import { DesignationListingComponent } from './components/designation-listing/designation-listing.component';
import { GeneralComponent } from './components/general/general.component';
import { ExitInterviewQuestionsComponent } from './components/exit-interview-questions/exit-interview-questions.component';
import { LeaveTypeComponent } from './components/leave-type/leave-type.component';
import { AppraisalConfigComponent } from './components/appraisal-config/appraisal-config.component';

const routes: Routes = [
  {
    path: '', component: ConfigurationComponent,
    children: [
      {
        path: 'general', component: GeneralComponent
      },
      {
        path: 'designation', component: DesignationListingComponent
      },
      {
        path: 'exit-interview-questions', component: ExitInterviewQuestionsComponent
      },
      {
        path: 'leave-policy', component: LeaveTypeComponent
      },
      {
        path: 'appraisal-questions', component: AppraisalConfigComponent
      },
      {
        path: '', redirectTo: 'general', pathMatch: 'full'
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationRoutingModule { }
