import { NgModule } from '@angular/core';
import { UserComponent } from './user.component';
import { DialogModule } from 'primeng/primeng';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'primeng/components/common/shared';
import { DataTableModule } from 'primeng/components/datatable/datatable';
import { UserService } from './user.service';
import { MultiSelectModule } from 'primeng/components/multiselect/multiselect';
import { DropdownModule } from 'primeng/components/dropdown/dropdown';
import { CommonModule } from '@angular/common';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { TextMaskModule } from 'angular2-text-mask';
import { CheckboxModule } from 'primeng/primeng';
import { UserRoutingModule } from './user-routing.module';
import { RoleGuardService } from '../../service/role-guard.service';

@NgModule({
  declarations: [
    UserComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    DialogModule,
    MultiSelectModule,
    TextMaskModule,
    DropdownModule,
    DataTableModule,
    SharedModule,
    CheckboxModule,
    FormsModule,
    ReactiveFormsModule,
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)',
      backdropBorderRadius: '4px',
      primaryColour: '#31c3aa',
      secondaryColour: '#000',
      tertiaryColour: '#a129'
    }),
  ],
  providers: [UserService, RoleGuardService],

})
export class UserModule { }
