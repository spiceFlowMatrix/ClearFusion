import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';
import { HrService } from 'src/app/hr/services/hr.service';
import { MatDialog } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AddAppraisalComponent } from './add-appraisal/add-appraisal.component';

@Component({
  selector: 'app-appraisal-config',
  templateUrl: './appraisal-config.component.html',
  styleUrls: ['./appraisal-config.component.scss']
})
export class AppraisalConfigComponent implements OnInit {
  appraisalList$: Observable<AppraisalQuestions[]>;

  appraisalListHeaders$ = of(['Id', 'Question', 'Dari Question', 'Sequence']);
  hideColums$ = of({ headers: ['Question', 'Dari Question', 'Sequence'], items: ['Question', 'DariQuestion', 'SequenceNo'] });

  actions: TableActionsModel;
  pageModel = {
    PageSize: 10,
    PageIndex: 0
  };
  RecordCount: number;
  Id: number;
  constructor(private hrService: HrService, private dialog: MatDialog, private toastr: ToastrService,
    private commonLoader: CommonLoaderService) {

  }


  ngOnInit() {
    this.getAppraisalQuestions();
    this.actions = {
      items: {
        button: { status: false, text: '' },
        download: false,
        edit: true,
        delete: false,
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false,
      }
    };
  }
  getAppraisalQuestions() {
    this.hrService.getAppraisalQuestions(1).subscribe(res => {
      this.commonLoader.hideLoader();
      this.appraisalList$ = of(res.data.AppraisalList.map(x => {
        return {
          Id: x.AppraisalGeneralQuestionsId,
          Question: x.Question,
          DariQuestion: x.DariQuestion,
          SequenceNo: x.SequenceNo

        } as AppraisalQuestions
      }));
      this.RecordCount = res.RecordCount;
    })
  }
  addAppraisalQues() {
    const dialogRef = this.dialog.open(AddAppraisalComponent, {
      width: '650px',
    });

    dialogRef.afterClosed().subscribe(x => {
      this.getAppraisalQuestions();
    });
  }
  actionEvents(event:any){
    if (event.type === 'edit') {
      const dialogRef = this.dialog.open(AddAppraisalComponent, {
        width: '650px',
        data: event.item
      });

      dialogRef.afterClosed().subscribe(x => {
        this.getAppraisalQuestions();
      });
    }
  }

}

interface AppraisalQuestions {
  Id?: number;
  Question?: string;
  DariQuestion?: string;
  SequenceNo?: number;
}
