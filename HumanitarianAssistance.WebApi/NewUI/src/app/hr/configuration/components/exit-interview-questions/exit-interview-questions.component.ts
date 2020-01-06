import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { HrService } from 'src/app/hr/services/hr.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { AddExitInterviewQuestionsComponent } from './add-exit-interview-questions/add-exit-interview-questions.component';
import { QuestionType, QuestionTypeName } from 'src/app/shared/enum';
import { ConfigService } from 'src/app/store/services/config.service';

@Component({
  selector: 'app-exit-interview-questions',
  templateUrl: './exit-interview-questions.component.html',
  styleUrls: ['./exit-interview-questions.component.scss']
})
export class ExitInterviewQuestionsComponent implements OnInit {

  exitInterviewQuestionsList$: Observable<any[]>;

  exitInterviewQuestionsHeaders$ = of(['Id', 'Question Text', 'Question Type', 'Sequence Position']);
  hideColums$: Observable<{ headers?: string[], items?: string[] }>;

  actions: TableActionsModel;
  pageModel = {
    PageSize: 10,
    PageIndex: 0
  };
  RecordCount: number;

  constructor(private hrService: HrService, private dialog: MatDialog, private toastr: ToastrService,
    private commonLoader: CommonLoaderService, private configservice: ConfigService,) {
      this.hideColums$ = of({headers: ['Id', 'Question Text', 'Question Type', 'Sequence Position'],
      items: ['Id', 'QuestionText', 'QuestionTypeText', 'SequencePosition']});
  }

  ngOnInit() {
    this.getExitInterviewQuestionsList();

    this.actions = {
      items: {
        button: { status: false, text: '' },
        delete: true,
        download: false,
        edit: true
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false,
      }
    };
  }

  getExitInterviewQuestionsList() {
    this.commonLoader.showLoader();
    this.hrService.getExitInterviewQuestionsList(this.pageModel).subscribe(x => {
      this.commonLoader.hideLoader();
      if (x.Result.length > 0) {
        this.exitInterviewQuestionsList$ = of(x.Result.map(element => {
          return {
            Id: element.Id,
            QuestionText: element.QuestionText,
            QuestionType: element.QuestionType,
            QuestionTypeText: QuestionTypeName.get(element.QuestionType),
            SequencePosition: element.SequencePosition,
          };
        }));
      }

      this.RecordCount = x.RecordCount;
    }, error => {
      this.commonLoader.hideLoader();
    });
  }

  addQuestion() {
      const dialogRef = this.dialog.open(AddExitInterviewQuestionsComponent, {
        width: '650px',
      });

      dialogRef.afterClosed().subscribe(x => {
        this.getExitInterviewQuestionsList();
      });
  }

  actionEvents(event: any) {
    if (event.type === 'edit') {
      const dialogRef = this.dialog.open(AddExitInterviewQuestionsComponent, {
        width: '650px',
        data: event.item
      });

      dialogRef.afterClosed().subscribe(x => {
        this.getExitInterviewQuestionsList();
      });
    } else if (event.type === 'delete') {
      this.configservice.openDeleteDialog().subscribe(res => {
        if (res) {
          this.commonLoader.showLoader();
          this.hrService.deleteExitinterviewQuestion(event.item.Id).subscribe(x => {
            this.commonLoader.hideLoader();
            if (x) {
              this.toastr.success('Deleted');
              this.getExitInterviewQuestionsList();
            }

            this.RecordCount = x.RecordCount;
          }, error => {
            this.commonLoader.hideLoader();
          });
        }
      });

    }
  }

  //#region "pageEvent"
  pageEvent(e) {
    this.pageModel.PageIndex = e.pageIndex;
    this.pageModel.PageSize = e.pageSize;
    this.getExitInterviewQuestionsList();
  }
  //#endregion

}
