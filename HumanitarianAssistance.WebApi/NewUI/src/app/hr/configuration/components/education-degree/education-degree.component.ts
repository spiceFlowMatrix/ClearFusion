import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { TableActionsModel } from 'projects/library/src/public_api';
import { HrService } from 'src/app/hr/services/hr.service';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';

@Component({
  selector: 'app-education-degree',
  templateUrl: './education-degree.component.html',
  styleUrls: ['./education-degree.component.scss']
})
export class EducationDegreeComponent implements OnInit {

  designationList$: Observable<any[]>;
  designationListHeaders$ = of(['Id', 'Name']);

  actions: TableActionsModel;
  pageModel = {
    PageSize: 10,
    PageIndex: 0
  };
  RecordCount: number;

  constructor(private hrService: HrService, private dialog: MatDialog, private toastr: ToastrService,
    private commonLoader: CommonLoaderService) {

      this.actions = {
        items: {
          button: { status: false, text: '' },
          delete: false,
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

  ngOnInit() {
  }

  getEducationDegreeList() {
    this.commonLoader.showLoader();
    this.hrService.getDesignationList(this.pageModel).subscribe(x => {
      this.commonLoader.hideLoader();
      this.designationList$ = of(x.DesignationList.map(element => {
        return {
          Id: element.DesignationId,
          Designation: element.Designation,
          Description: element.Description,
          subItems: element.TechnicalQuestionList ? (element.TechnicalQuestionList.map((r) => {
            return {
              QuestionId: r.QuestionId,
              Question: r.Question
            };
          })) : null
        };
      }));
      this.RecordCount = x.RecordCount;
    }, error => {
      this.commonLoader.hideLoader();
    });
  }

//   addDesignation() {
//     const dialogRef = this.dialog.open(AddDesignationComponent, {
//       width: '650px',
//     });

//     dialogRef.afterClosed().subscribe(x => {
//       this.getDesignationList();
//     });
// }

  // actionEvents(event: any) {
  //   if (event.type === 'edit') {
  //     const dialogRef = this.dialog.open(AddDesignationComponent, {
  //       width: '650px',
  //       data: event.item
  //     });

  //     dialogRef.afterClosed().subscribe(x => {
  //       this.getDesignationList();
  //     });
  //   }
  // }

}
