import { Component, OnInit, AfterViewInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/lib/models/table-actions-model';
import { EmployeeAppraisalService } from '../../services/employee-appraisal.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-employee-appraisal',
  templateUrl: './employee-appraisal.component.html',
  styleUrls: ['./employee-appraisal.component.scss']
})
export class EmployeeAppraisalComponent implements OnInit {
  appraisalListHeader$ = of([
    'EvaluationDate',
    'AppraisalScore',
    'TotalAppraisalScore',
    'DirectSupervisor',
    'AppraisalStatus'
  ]);
  appraisalList$: Observable<any[]>;
  actions: TableActionsModel;
  employeeId: number;

  constructor(
    private appraisalService: EmployeeAppraisalService,
    private routeActive: ActivatedRoute,
    private router: Router
  ) {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        download: false,
        edit: true,
        delete: false
      },
      subitems: {
        button: { status: false, text: '' },
        delete: false,
        download: false
      }
    };
  }

  ngOnInit() {
    this.routeActive.params.subscribe(params => {
      this.employeeId = +params['id'];
    });
    this.getAllAppraisalList();
  }


  getAllAppraisalList() {
    if (this.employeeId != undefined && this.employeeId != null) {
      this.appraisalService
        .getAllAppraisalListEmployeeId(this.employeeId)
        .subscribe(
          res => {
            if (res && res.AppraisalList) {
              this.appraisalList$ = of(
                res.AppraisalList.map(y => {
                  return {
                    EvaluationDate: y.EvaluationDisplayDate,

                    AppraisalScore: y.AppraisalScore,
                    TotalAppraisalScore: y.TotalScore,
                    DirectSupervisor: y.Position,
                    AppraisalStatus: 0,
                    itemAction: [
                      {
                        button: {
                          status: true,
                          text: 'VIEW',
                          type: 'text'
                        },
                        delete: false,
                        download: false,
                        edit: false
                      },
                      {
                        button: {
                          status: true,
                          text: 'REJECT',
                          type: 'cancel'
                        },
                        delete: false,
                        download: false,
                        edit: false
                      },
                      {
                        button: {
                          status: true,
                          text: 'APPROVE',
                          type: 'save'
                        },
                        delete: false,
                        download: false,
                        edit: false
                      }
                    ]
                  };
                })
              );
            }
          },
          err => {}
        );
    }
  }

  addNewAppraisal() {
    this.router.navigate(['addAppraisal'], { relativeTo: this.routeActive });
  }


}
