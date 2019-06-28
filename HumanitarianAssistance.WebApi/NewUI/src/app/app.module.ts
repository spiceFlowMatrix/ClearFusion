import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AppRoutingModule } from './app-routing.module';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { DataInterceptor } from './shared/services/data-interceptor';
import { GlobalService } from './shared/services/global-services.service';
import { AppUrlService } from './shared/services/app-url.service';
import { AuthGuard } from './shared/auth/auth-guard';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LibraryModule } from '../../projects/library/src/lib/library.module';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonLoaderComponent } from './shared/common-loader/common-loader.component';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CommonLoaderService } from './shared/common-loader/common-loader.service';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { DbstyleGuideComponent } from './shared/dbstyle-guide/dbstyle-guide.component';
import { RoleGuardService } from './shared/services/role-guard';
import { LocalStorageService } from './shared/services/localstorage.service';
import { LoginService } from './login/login.service';
import { GlobalSharedService } from './shared/services/global-shared.service';
import { DatePipe } from '@angular/common';
import { AngularSplitModule } from 'angular-split';
import { SignalRService } from './shared/services/signal-r.service';
import { MAT_DATE_LOCALE, MatNativeDateModule } from '@angular/material/core';
import { MatSidenavModule } from '@angular/material/sidenav';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CommonLoaderComponent,

    // component
    DbstyleGuideComponent,


  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    AngularSplitModule.forRoot(),

    LibraryModule, // custome lib

    // material
    BrowserAnimationsModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSidenavModule,
    MatProgressBarModule,

    NgxSpinnerModule,
  ],

  exports: [
  ],

  providers: [
    AuthGuard,
    // { provide: HttpHandler, useClass: DataInterceptor },
    GlobalService,
    GlobalSharedService,
    CommonLoaderService,
    LocalStorageService,
    AppUrlService,
    LoginService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: DataInterceptor,
      multi: true,
    },
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }, // use to format date : dd/MM/yyyy
    RoleGuardService,
    DatePipe,
    SignalRService
    // { provide: ErrorHandler, useClass: GlobalErrorHandler }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
