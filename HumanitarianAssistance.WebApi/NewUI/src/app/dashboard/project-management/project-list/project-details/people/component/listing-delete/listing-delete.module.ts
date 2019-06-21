import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListingDeleteComponent } from './listing-delete.component';
import { MatIconModule } from '@angular/material/icon';
import { ProjectPeopleRolePipe } from '../pipes/project-people-role.pipe';
import { UsersPipe } from 'src/app/shared/pipes/users.pipe';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [ListingDeleteComponent, ProjectPeopleRolePipe, UsersPipe],
  imports: [
    CommonModule,
    MatIconModule,
    MatSelectModule,
    MatButtonModule
  ],
  exports: [ListingDeleteComponent]
})
export class ListingDeleteModule {}
