import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ConfigurationRoutingModule } from './configuration-routing.module';
import { AddDesignationComponent } from './components/add-designation/add-designation.component';
import { DesignationListingComponent } from './components/designation-listing/designation-listing.component';
import { EntryComponentComponent } from './components/entry-component/entry-component.component';
import { ConfigurationComponent } from './configuration.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatCardModule } from '@angular/material/card';
import { ShareLayoutModule } from 'src/app/shared/share-layout.module';
import { LibraryModule, SubHeaderTemplateModule } from 'projects/library/src/public_api';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MatDividerModule } from '@angular/material/divider';
import { MatPaginatorModule } from '@angular/material/paginator';
import { GeneralComponent } from './components/general/general.component';
import { EducationDegreeComponent } from './components/education-degree/education-degree.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { AddEducationDegreeComponent } from './components/education-degree/add-education-degree/add-education-degree.component';
import { OfficeMasterComponent } from './components/office-master/office-master.component';
import { AddOfficeMasterComponent } from './components/office-master/add-office-master/add-office-master.component';
import { DepartmentMasterComponent } from './components/department-master/department-master.component';
import { AddDepartmentMasterComponent } from './components/department-master/add-department-master/add-department-master.component';
import { JobGradeMasterComponent } from './components/job-grade-master/job-grade-master.component';
import { AddJobGradeComponent } from './components/job-grade-master/add-job-grade/add-job-grade.component';
import { AttendanceGroupMasterComponent } from './components/attendance-group-master/attendance-group-master.component';
import { AddAttendanceGroupComponent } from './components/attendance-group-master/add-attendance-group/add-attendance-group.component';
import { ProfessionMasterComponent } from './components/profession-master/profession-master.component';
import { AddProfessionComponent } from './components/profession-master/add-profession/add-profession.component';
import { QualificationMasterComponent } from './components/qualification-master/qualification-master.component';
import { AddQualificationComponent } from './components/qualification-master/add-qualification/add-qualification.component';
import { ExitInterviewQuestionsComponent } from './components/exit-interview-questions/exit-interview-questions.component';
import { AddExitInterviewQuestionsComponent } from './components/exit-interview-questions/add-exit-interview-questions/add-exit-interview-questions.component';
import { LeaveTypeComponent } from './components/leave-type/leave-type.component';
import { AddLeaveTypeComponent } from './components/leave-type/add-leave-type/add-leave-type.component';
import { HolidaysComponent } from './components/holidays/holidays.component';
import { MatDatepickerModule, MatSelectModule, MatCheckboxModule } from '@angular/material';

@NgModule({
  declarations: [
    AddDesignationComponent,
    DesignationListingComponent,
    EntryComponentComponent,
    ConfigurationComponent,
    GeneralComponent,
    EducationDegreeComponent,
    AddEducationDegreeComponent,
    OfficeMasterComponent,
    AddOfficeMasterComponent,
    DepartmentMasterComponent,
    AddDepartmentMasterComponent,
    JobGradeMasterComponent,
    AddJobGradeComponent,
    AttendanceGroupMasterComponent,
    AddAttendanceGroupComponent,
    ProfessionMasterComponent,
    AddProfessionComponent,
    QualificationMasterComponent,
    AddQualificationComponent,
    ExitInterviewQuestionsComponent,
    AddExitInterviewQuestionsComponent,
    LeaveTypeComponent,
    AddLeaveTypeComponent,
    HolidaysComponent,
  ],
  imports: [
    CommonModule,
    ConfigurationRoutingModule,
    MatSidenavModule,
    MatCardModule,
    MatTabsModule,
    MatDialogModule,
    MatExpansionModule,
    MatPaginatorModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDividerModule,
    TextFieldModule,
    FormsModule,
    ShareLayoutModule,
    SubHeaderTemplateModule,
    LibraryModule,
    MatDatepickerModule,
    MatSelectModule,
    MatCheckboxModule
  ],
  entryComponents: [AddDesignationComponent, AddEducationDegreeComponent, AddOfficeMasterComponent,
                   AddDepartmentMasterComponent, AddJobGradeComponent, AddAttendanceGroupComponent,
                   AddProfessionComponent, AddQualificationComponent, AddExitInterviewQuestionsComponent,
                  AddLeaveTypeComponent]
})
export class ConfigurationModule { }
