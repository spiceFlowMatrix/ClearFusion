import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { ToastrModule } from 'ngx-toastr';
import { UserService } from './dashboard/user/user.service';
import { AppRoutingModule } from './app-routing.module';
import { NotFoundComponent } from './not-found/not-found.component';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { CommonService } from './service/common.service';
import { AuthenticationService } from './service/authentication.service';
import { AppSettingsService } from './service/app-settings.service';
import { RoleGuardService } from './service/role-guard.service';
import { AuthGuard } from './auths/authentications';

@NgModule({
  declarations: [AppComponent, NotFoundComponent],
  imports: [
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.5)',
      backdropBorderRadius: '0px',
      fullScreenBackdrop: true,
      primaryColour: '#31c3aa',
      secondaryColour: '#000',
      tertiaryColour: '#a129'
    }),
    // NgxPermissionsModule.forRoot()
  ],
  providers: [
    UserService,
    CommonService,
    AuthenticationService,
    AppSettingsService,
    AuthGuard, // Auth Guard,
    RoleGuardService,

    { provide: LocationStrategy, useClass: HashLocationStrategy }

    // DictionaryService,
    // {
    //     provide: APP_INITIALIZER,
    //     useFactory: ( ps: NgxPermissionsService ) => function() {(data) => {return ps.loadPermissions(data)}},
    //     deps: [ NgxPermissionsService],
    //     multi: true
    //   }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
