import { Component, OnInit, AfterViewInit, EventEmitter } from '@angular/core';
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
    'Evaluation Date',
    'Appraisal Score',
    'Total Appraisal Score',
    'Direct Supervisor',
    'Appraisal Status'
  ]);
  hideColumsAppraisalList$ = of({
    headers: [
      'Evaluation Date',
      'Appraisal Score',
      'Total Appraisal Score',
      'Direct Supervisor',
      'Appraisal Status'
    ],
    items: [
      'EvaluationDate',
      'AppraisalScore',
      'TotalAppraisalScore',
      'DirectSupervisor',
      'AppraisalStatus'
    ]
  });
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
            if (
              res &&
              res.AppraisalList !== undefined &&
              res.AppraisalList.length > 0
            ) {
              this.appraisalList$ = of(
                res.AppraisalList.map(y => {
                  return {
                    EmployeeAppraisalDetailsId: y.EmployeeAppraisalDetailsId,
                    EvaluationDate: y.EvaluationDisplayDate,
                    AppraisalScore: y.AppraisalScore,
                    TotalAppraisalScore: y.TotalScore,
                    DirectSupervisor: y.Position == null ? '' : y.Position,
                    AppraisalStatus: (y.AppraisalStatus == true ) ? 'Approved' : ( y.AppraisalStatus == false ? 'Rejected' : 'Pending'),

                    itemAction: !y.AppraisalStatus
                      ? [
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
                          },
                          {
                            button: {
                              status: false,
                              text: 'APPROVE',
                              type: 'save'
                            },
                            delete: false,
                            download: false,
                            edit: true
                          }
                        ]
                      : [
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
                              text: 'APPROVED',
                              type: 'text'
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
    // this.router.navigate(['addAppraisal',], { relativeTo: this.routeActive });
    this.router.navigate(['addAppraisal'], { relativeTo: this.routeActive });
  }
  ActionEvents(event: any) {
    if (event.type === 'edit') {
      this.editAppraisalList(event.item.EmployeeAppraisalDetailsId);
    }
    if (event.type === 'APPROVE') {
      this.AddApprovedAppraisal(event.item.EmployeeAppraisalDetailsId);
    }
    if (event.type === 'REJECT') {
      this.RejectAppraisal(event.item.EmployeeAppraisalDetailsId);
    }
    if (event.type === 'VIEW') {
      this.ViewAppraisal(event.item.EmployeeAppraisalDetailsId);
    }
  }
  editAppraisalList(id: any) {
    console.log(id);
    if (id != undefined) {
      this.router.navigate(['addAppraisal', id], {
        relativeTo: this.routeActive
      });
    }
  }
  AddApprovedAppraisal(id: any) {
    this.appraisalService.approveAppraisaldetail(id).subscribe(res => {
      this.getAllAppraisalList();
      console.log(res);
    });
  }

  RejectAppraisal(id: any) {
    this.appraisalService.rejectAppraisaldetail(id).subscribe(res => {
      console.log(res);
      this.getAllAppraisalList();
    });
  }

  ViewAppraisal(id: any) {
    if (id !== undefined) {
      this.router.navigate(['addAppraisal', id], {
        relativeTo: this.routeActive,
        queryParams: { isViewed: 'false' }
      });
    }
  }
}
