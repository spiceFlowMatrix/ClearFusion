import { Routes, RouterModule } from "@angular/router";
import { ProjectManagementComponent } from "./project-management.component";
import { ProjectMainComponent } from "./project-main/project-main.component";
import { NgModule } from "@angular/core";
import { ProjectPurchaseComponent } from "./project-purchase/project-purchase.component";
import { ProjectHiringComponent } from "./project-hiring/project-hiring.component";

const projectrouter: Routes =
    [{
        path: '', component: ProjectManagementComponent,
        children:
            [
                { path: 'projectsmain', component: ProjectMainComponent },
                { path: 'project-purchase', component: ProjectPurchaseComponent },
                { path: 'project-hiring', component: ProjectHiringComponent },
            ]
    }];

@NgModule({
    imports: [RouterModule.forChild(projectrouter)],
    exports: [RouterModule]
})
export class ProjectManagementRoutingModule { }