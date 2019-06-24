import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { LoginRoutingModule } from './login-routing.module';
import { CodeService } from '../dashboard/code/code.service';
import { CommonService } from '../service/common.service';
import { AuthenticationService } from '../service/authentication.service';
import { AppSettingsService } from '../service/app-settings.service';

@NgModule({
  declarations: [LoginComponent],
  imports: [
    LoginRoutingModule,
    // HttpModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)',
      backdropBorderRadius: '4px',
      primaryColour: '#31c3aa',
      secondaryColour: '#000',
      tertiaryColour: '#a129'
    })
  ],

  providers: [
    CommonService,
    AuthenticationService,
    AppSettingsService,
    CodeService
    // httpClientService,
    // MessageService
  ]
})
export class LoginModule {}
