import { Component, OnInit, Inject } from '@angular/core';
import {MAT_DIALOG_DATA,MatDialogRef} from '@angular/material/dialog';
import { GLOBAL } from 'src/app/shared/global';
import { GlobalSharedService } from 'src/app/shared/services/global-shared.service';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-project-other-detail-pdf',
  templateUrl: './project-other-detail-pdf.component.html',
  styleUrls: ['./project-other-detail-pdf.component.scss']
})
export class ProjectOtherDetailPdfComponent implements OnInit {

  myForm: FormGroup;
  constructor(public dialogRef: MatDialogRef<ProjectOtherDetailPdfComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,private globalSharedService: GlobalSharedService,private appurl: AppUrlService,
    private fb: FormBuilder) {
      this.myForm = this.fb.group({
        SelectAll: [false],
        opportunitytype: [false],
        donor: [false],
        opportunityno: [false],
        opportunity: [false],
        enddate: [false],
        opportunitydesc: [false],
        country: [false],
        province: [false],
        district: [false],
        office: [false],
        sector: [false],
        program: [false],
        startdate: [false],
        projgoal: [false],
        projobj: [false],
        reoidate: [false],
        submissiondate: [false],
        mainactivities: [false],
        dirbenmale: [false],
        dirbenfemale: [false],
        indirbenmale: [false],
        indirbenfemale: [false],
        strengthconsideration: [false],
        genderconsideration: [false],
        genderremarks: [false],
        security: [false],
        securityconsideration: [false],
        securityremarks: [false],
        ProjectId: ['']

      });
    }

  ngOnInit() {
  }
  onExportPdf() {
    // console.log(this.myForm.value);
    // set your pdf values here
    this.myForm.value.ProjectId = this.data.ProjectId;
    this.globalSharedService
      .getFile(this.appurl.getApiUrl() + GLOBAL.API_Pdf_GetProjectOtherDetailReportPdf,
      this.myForm.value
      )
      .pipe()
      .subscribe();
      this.dialogRef.close();
    // this.setProjectOtherDetailValueForPdf();
    // this.pDetailPdfService.onExportPdf(this.projectOtherDetailPdf);
  }
  closeModal(): void {
    this.dialogRef.close();
  }
  SelectAllChange(){
    if (this.myForm.controls['SelectAll'].value) {
      this.myForm.patchValue({
        opportunitytype: true,
        donor: true,
        opportunityno: true,
        opportunity: true,
        enddate: true,
        opportunitydesc: true,
        country: true,
        province: true,
        district: true,
        office: true,
        sector: true,
        program: true,
        startdate: true,
        projgoal: true,
        projobj: true,
        reoidate: true,
        submissiondate: true,
        mainactivities: true,
        dirbenmale: true,
        dirbenfemale: true,
        indirbenmale: true,
        indirbenfemale: true,
        strengthconsideration: true,
        genderconsideration: true,
        genderremarks: true,
        security: true,
        securityconsideration: true,
        securityremarks: true
      });
    } else {
      this.myForm.patchValue({
        opportunitytype: false,
        donor: false,
        opportunityno: false,
        opportunity: false,
        enddate: false,
        opportunitydesc: false,
        country: false,
        province: false,
        district: false,
        office: false,
        sector: false,
        program: false,
        startdate: false,
        projgoal: false,
        projobj: false,
        reoidate: false,
        submissiondate: false,
        mainactivities: false,
        dirbenmale: false,
        dirbenfemale: false,
        indirbenmale: false,
        indirbenfemale: false,
        strengthconsideration: false,
        genderconsideration: false,
        genderremarks: false,
        security: false,
        securityconsideration: false,
        securityremarks: false
      });
    }
  }
}
