import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppSidebarComponent } from './shared/appSideBar.component';
import { AppHeaderComponent } from './shared/appHeader.component';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { UserService } from './user/user.service';
import { MessageService } from 'primeng/components/common/messageservice';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import {
  DxPopoverModule,
  DxTemplateModule,
  DxSelectBoxModule,
  DxListModule
} from 'devextreme-angular';
import { CodeService } from './code/code.service';
import { RouterModule } from '@angular/router';
import { HubComponentComponent } from './shared/hub-component/hub-component.component';
import { NgxPermissionsModule } from 'ngx-permissions';
import { NotificationPanelComponent } from './shared/notification-panel/notification-panel.component';
import { CommonService } from '../service/common.service';
import { AuthenticationService } from '../service/authentication.service';
import { AppSettingsService } from '../service/app-settings.service';
// import { SignalRService } from "../Services/signalR.Service";

@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule, // Routing
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    DxSelectBoxModule,
    DxListModule,
    DxPopoverModule,
    DxTemplateModule,

    ModalModule.forRoot(),
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.5)',
      backdropBorderRadius: '0px',
      fullScreenBackdrop: true,
      primaryColour: '#31c3aa',
      secondaryColour: '#000',
      tertiaryColour: '#a129'
    }),
    NgxPermissionsModule.forRoot()
  ],
  declarations: [
    DashboardComponent,
    AppHeaderComponent,
    AppSidebarComponent,
    HubComponentComponent,
    NotificationPanelComponent
  ],
  providers: [
    UserService,
    CommonService,
    AuthenticationService,
    AppSettingsService,
    CodeService
    // SignalRService
  ]
})
export class DashboardModule {
  // static forRoot(culture: string){
  //     return {
  //         ngModule : DashboardModule,
  //         providers : [
  //             { provide: commonService, useValue : culture }
  //         ]
  //     };
  // }
}
