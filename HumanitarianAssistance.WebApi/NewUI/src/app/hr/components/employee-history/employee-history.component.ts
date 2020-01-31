import { AddSalaryBudgetComponent } from './add-salary-budget/add-salary-budget.component';
import { AddThreeReferenceDetailsComponent } from './add-three-reference-details/add-three-reference-details.component';
import { AddOtherSkillsComponent } from './add-other-skills/add-other-skills.component';
import { AddLanguageComponent } from './add-language/add-language.component';
import { AddHistoryOutsideCountryComponent } from './add-history-outside-country/add-history-outside-country.component';
import { AddCloseRelativeComponent } from './add-close-relative/add-close-relative.component';
import { AddEducationComponent } from './add-education/add-education.component';
import { AddHistoricalLogComponent } from './add-historical-log/add-historical-log.component';
import { EmployeeHistoryService } from './../../services/employee-history.service';
import { Component, OnInit, HostListener } from '@angular/core';
import { of, Observable, forkJoin, ReplaySubject } from 'rxjs';
import {
  IHistoricalLogDetails,
  IEducationDetails,
  IHistoryOutsideCountryDetails,
  IEmployeeCloseRelativeDetails,
  IEmployeeThreeReferenceDetails,
  IEmployeeLanguageDetails,
  IEmployeeOtherSkillDetails,
  IEmployeeSalaryBudgetDetails,
  IHistoryOutsideOrganizationDetails
} from '../../models/employee-history-models';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { RatingAction } from 'src/app/shared/enum';
import { MatDialog } from '@angular/material';
import { TableActionsModel } from 'projects/library/src/public_api';
import { HrService } from '../../services/hr.service';
import { ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { takeUntil } from 'rxjs/operators';
import { AddHistoryOutsideOrganizationComponent } from './add-history-outside-organization/add-history-outside-organization.component';

@Component({
  selector: 'app-employee-history',
  templateUrl: './employee-history.component.html',
  styleUrls: ['./employee-history.component.scss']
})
export class EmployeeHistoryComponent implements OnInit {
  historicalLogHeader$ = of(['Id', 'Date', 'Description']);
  educationHeader$ = of([
    'Id',
    'Education From',
    'Education To',
    'Field of Study',
    'Institute',
    'Degree'
  ]);
  employeeHistoryOCHeader$ = of([
    'Id',
    'Employment Form',
    'Employment To',
    'Organization',
    'Monthly Salary',
    'Reason for Leaving',
    'Position'
  ]);
  employeeHistoryOrgHeader$ = of([
    'Id',
    'Employment Form',
    'Employment To',
    'Organization',
    'Monthly Salary',
    'Reason for Leaving',
    'Position'
  ]);

  infoOfCloseRelativeHeader$ = of([
    'Id',
    'Name',
    'Relationship',
    'Position',
    'Email',
    'Phone',
    'Organization'
  ]);
  infoOfThreeRefHeader$ = of([
    'Id',
    'Name',
    'Relationship',
    'Position',
    'Email',
    'Phone',
    'Organization'
  ]);
  otherSkillHeader$ = of([
    'Id',
    'Type of Skill',
    'Ability Level',
    'Experience',
    'Remarks'
  ]);
  salarybudgetHeader$ = of([
    'Id',
    'Year',
    'Currency',
    'Salary Budget',
    'Budget Disbursed'
  ]);
  languageHeader$ = of([
    'Id',
    'Language',
    'Writing',
    'Speaking',
    'Reading',
    'Listening'
  ]);
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;
  employeeId: number;
  actions: TableActionsModel;
  historicalLogList$: Observable<IHistoricalLogDetails[]>;
  educationList$: Observable<IEducationDetails[]>;
  employeeHistoryOCList$: Observable<IHistoryOutsideCountryDetails[]>;
  employeeHistoryOrgList$: Observable<IHistoryOutsideOrganizationDetails[]>;
  employeeCloseRelativeList$: Observable<IEmployeeCloseRelativeDetails[]>;
  employeeThreeReferenceList$: Observable<IEmployeeThreeReferenceDetails[]>;
  employeeOtherSkillList$: Observable<IEmployeeOtherSkillDetails[]>;
  employeeSalaryBudgetList$: Observable<IEmployeeSalaryBudgetDetails[]>;
  employeeLanguageList$: Observable<IEmployeeLanguageDetails[]>;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);
  constructor(
    public dialog: MatDialog,
    private hrService: HrService,
    private routeActive: ActivatedRoute,
    private commonLoader: CommonLoaderService,
    private datePipe: DatePipe,
    private employeeHistoryService: EmployeeHistoryService
  ) {
    this.actions = {
      items: {
        button: { status: false, text: '' },
        download: false,
        edit: false,
        delete: true
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
    this.getScreenSize();

    forkJoin([
      this.getEmployeeHistoricalLogList(),
      this.getEmployeeEducationDetailsList(),
      this.getEmployeeHistoryOfOutsideCountryDetailList(),
      this.getEmployeeHistoryOfOutsideOrganizationDetailList(),
      this.getEmployeeCloseRelativeDetailList(),
      this.getEmployeeThreeReferenceDetailList(),
      this.getEmployeeOtherSkillDetailList(),
      this.getEmployeeSalaryBudgetDetailList(),
      this.getEmployeeLanguageDetailList()
    ])
      .pipe(takeUntil(this.destroyed$))
      .subscribe(result => {
        this.subscribeEmployeeHistoricalLogList(result[0]);
        this.subscribeEmployeeEducationDetailsList(result[1]);
        this.subscribeEmployeeHistoryOfOutsideCountryDetailList(result[2]);
        this.subscribeEmployeeHistoryOfOutsideOrganizationDetailList(result[3]);
        this.subscribeEmployeeCloseRelativeDetailList(result[4]);
        this.subscribeEmployeeThreeReferenceDetailList(result[5]);
        this.subscribeEmployeeOtherSkillDetailList(result[6]);
        this.subscribeEmployeeSalaryBudgetDetailList(result[7]);
        this.subscribeEmployeeLanguageDetailList(result[8]);
      });
  }
  //#region "Dynamic Scroll"
  @HostListener('window:resize', ['$event'])
  getScreenSize() {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 110 + 'px',
      'overflow-x': 'hidden'
    };
  }
  //#endregion

  //#region "get Employee Historical Log List"
  getEmployeeHistoricalLogList() {
    return this.employeeHistoryService.getHistoricalLogList(this.employeeId);
  }
  subscribeEmployeeHistoricalLogList(response: any) {
    this.commonLoader.showLoader();
    if (response.data.EmployeeHistoryDetailList !== undefined) {
      this.historicalLogList$ = of(
        response.data.EmployeeHistoryDetailList.map(y => {
          return {
            HistoryId: y.HistoryID,
            HistoryDate: this.datePipe.transform(y.HistoryDate, 'dd-MM-yyyy'),
            Description: y.Description
          } as IHistoricalLogDetails;
        })
      );
    }
  }
  //#endregion

  // #region "Add HistoricalLog"
  addHistoricalLog(): void {
    /** Open AddHistoricalLog dialog box*/
    const dialogRef = this.dialog.open(AddHistoricalLogComponent, {
      width: '500px',
      data: {
        employeeId: this.employeeId
      }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onAddHistoricalListRefresh.subscribe(res => {
      this.getEmployeeHistoricalLogList().subscribe(response => {
        this.subscribeEmployeeHistoricalLogList(response);
      });
      this.commonLoader.hideLoader();
    });
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion
  // #region "Delete Historical Log"
  deleteHistoricalLog(HistoryId: number) {
    this.hrService.openDeleteDialog().subscribe(res => {
      if (res === true) {
        this.employeeHistoryService
          .deleteHistoricalLog(HistoryId)
          .subscribe(response => {
            if (response.StatusCode === 200) {
              let index;
              this.historicalLogList$.subscribe(data => {
                index = data.findIndex(x => x.HistoryId === HistoryId);
                data.splice(index, 1);
                this.historicalLogList$ = of(data);
              });
            }
          });
      }
    });
  }
  //#endregion

  //#region "get Employee Education Details List"
  getEmployeeEducationDetailsList() {
    return this.employeeHistoryService.getEducationDetailList(this.employeeId);
  }
  subscribeEmployeeEducationDetailsList(response: any) {
    if (response.data.EmployeeEducationsList !== undefined) {
      this.educationList$ = of(
        response.data.EmployeeEducationsList.map(y => {
          return {
            EmployeeEducationsId: y.EmployeeEducationsId,
            EducationFrom: this.datePipe.transform(
              y.EducationFrom,
              'dd-MM-yyyy'
            ),
            EducationTo: this.datePipe.transform(y.EducationTo, 'dd-MM-yyyy'),
            FieldOfStudy: y.FieldOfStudy,
            Institute: y.Institute,
            Degree: y.Degree
          } as IEducationDetails;
        })
      );
    }
  }
  //#endregion
  // #region "Add Education"
  addEducation(): void {
    /** Open Education dialog box*/
    const dialogRef = this.dialog.open(AddEducationComponent, {
      width: '800px',
      data: {
        employeeId: this.employeeId
      }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onAddEducationListRefresh.subscribe(() => {
      this.getEmployeeEducationDetailsList().subscribe(response => {
        this.subscribeEmployeeEducationDetailsList(response);
      });
      this.commonLoader.hideLoader();
    });
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion
  // #region "Delete Education Detail"
  deleteEducationDetail(EmployeeEducationsId: number) {
    this.hrService.openDeleteDialog().subscribe(res => {
      if (res === true) {
        const model = {
          EmployeeEducationsId: EmployeeEducationsId
        };
        this.employeeHistoryService
          .deleteEducation(model)
          .subscribe(response => {
            if (response.StatusCode === 200) {
              let index;
              this.educationList$.subscribe(data => {
                index = data.findIndex(
                  x => x.EmployeeEducationsId === EmployeeEducationsId
                );
                data.splice(index, 1);
                this.educationList$ = of(data);
              });
            }
          });
      }
    });
  }
  //#endregion

  //#region "get Employee History Of Outside Country Detail List"
  getEmployeeHistoryOfOutsideCountryDetailList() {
    return this.employeeHistoryService.getEmployeeHistoryOfOutsideCountryDetailList(
      this.employeeId
    );
  }
  subscribeEmployeeHistoryOfOutsideCountryDetailList(response: any) {
    if (response.data.EmployeeHistoryOutsideOrganizationList !== undefined) {
      this.employeeHistoryOCList$ = of(
        response.data.EmployeeHistoryOutsideOrganizationList.map(y => {
          return {
            EmployeeHistoryOutsideCountryId: y.EmployeeHistoryOutsideCountryId,
            EmploymentFrom: this.datePipe.transform(
              y.EmploymentFrom,
              'dd-MM-yyyy'
            ),
            EmploymentTo: this.datePipe.transform(y.EmploymentTo, 'dd-MM-yyyy'),
            Organization: y.Organization,
            MonthlySalary: y.MonthlySalary,
            ReasonForLeaving: y.ReasonForLeaving,
            Position: y.Position
          } as IHistoryOutsideCountryDetails;
        })
      );
    }
  }
  //#endregion
  // #region "Add HistoryOutsideCountry"
  addHistoryOutsideCountry(): void {
    /** Open HistoryOutsideCountry dialog box*/
    const dialogRef = this.dialog.open(AddHistoryOutsideCountryComponent, {
      width: '800px',
      data: {
        employeeId: this.employeeId
      }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onAddHistoryOutsideCountryListRefresh.subscribe(
      () => {
        this.getEmployeeHistoryOfOutsideCountryDetailList().subscribe(
          response => {
            this.subscribeEmployeeHistoryOfOutsideCountryDetailList(response);
          }
        );
        this.commonLoader.hideLoader();
      }
    );
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion
  // #region "Delete Outside Country Info"
  deleteOutsideCountryInfo(EmployeeHistoryOutsideCountryId: number) {
    this.hrService.openDeleteDialog().subscribe(res => {
      if (res === true) {
        const model = {
          EmployeeHistoryOutsideCountryId: EmployeeHistoryOutsideCountryId
        };
        this.employeeHistoryService
          .deleteEmployeeHistoryOutsideCountry(model)
          .subscribe(response => {
            if (response.StatusCode === 200) {
              let index;
              this.employeeHistoryOCList$.subscribe(data => {
                index = data.findIndex(
                  x =>
                    x.EmployeeHistoryOutsideCountryId ===
                    EmployeeHistoryOutsideCountryId
                );
                data.splice(index, 1);
                this.employeeHistoryOCList$ = of(data);
              });
            }
          });
      }
    });
  }
  //#endregion







  //#region "get Employee History Of Outside Organization Detail List"
  getEmployeeHistoryOfOutsideOrganizationDetailList() {
    return this.employeeHistoryService.getEmployeeHistoryOfOutsideOrganizationDetailList(
      this.employeeId
    );
  }
  subscribeEmployeeHistoryOfOutsideOrganizationDetailList(response: any) {
    if (response.data.EmployeeHistoryOutsideOrganizationList !== undefined) {
      this.employeeHistoryOrgList$ = of(
        response.data.EmployeeHistoryOutsideOrganizationList.map(y => {
          return {
            EmployeeHistoryOutsideOrganizationId: y.EmployeeHistoryOutsideOrganizationId,
            EmploymentFrom: this.datePipe.transform(
              y.EmploymentFrom,
              'dd-MM-yyyy'
            ),
            EmploymentTo: this.datePipe.transform(y.EmploymentTo, 'dd-MM-yyyy'),
            Organization: y.Organization,
            MonthlySalary: y.MonthlySalary,
            ReasonForLeaving: y.ReasonForLeaving,
            Position: y.Position
          } as IHistoryOutsideOrganizationDetails;
        })
      );
    }
  }
  //#endregion
  // #region "Add HistoryOutsideOrganization"
  addHistoryOutsideOrganization(): void {
    /** Open HistoryOutsideOrganization dialog box*/
    const dialogRef = this.dialog.open(AddHistoryOutsideOrganizationComponent, {
      width: '800px',
      data: {
        employeeId: this.employeeId
      }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onAddHistoryOutsideOrganizationListRefresh.subscribe(
      () => {
        this.getEmployeeHistoryOfOutsideOrganizationDetailList().subscribe(
          response => {
            this.subscribeEmployeeHistoryOfOutsideOrganizationDetailList(response);
          }
        );
        this.commonLoader.hideLoader();
      }
    );
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion
  // #region "Delete Outside Organization Info"
  deleteOutsideOrganizationInfo(EmployeeHistoryOutsideOrganizationId: number) {
    this.hrService.openDeleteDialog().subscribe(res => {
      if (res === true) {
        const model = {
          EmployeeHistoryOutsideOrganizationId: EmployeeHistoryOutsideOrganizationId
        };
        this.employeeHistoryService
          .deleteEmployeeHistoryOutsideOrganization(model)
          .subscribe(response => {
            if (response.StatusCode === 200) {
              let index;
              this.employeeHistoryOrgList$.subscribe(data => {
                index = data.findIndex(
                  x =>
                    x.EmployeeHistoryOutsideOrganizationId ===
                    EmployeeHistoryOutsideOrganizationId
                );
                data.splice(index, 1);
                this.employeeHistoryOrgList$ = of(data);
              });
            }
          });
      }
    });
  }
  //#endregion



















  //#region "get Employee Close Relative Detail List"
  getEmployeeCloseRelativeDetailList() {
    return this.employeeHistoryService.getEmployeeCloseRelativeList(
      this.employeeId
    );
  }
  subscribeEmployeeCloseRelativeDetailList(response: any) {
    if (response.data.EmployeeRelativeInfoList !== undefined) {
      this.employeeCloseRelativeList$ = of(
        response.data.EmployeeRelativeInfoList.map(y => {
          return {
            EmployeeRelativeInfoId: y.EmployeeRelativeInfoId,
            Name: y.Name,
            Relationship: y.Relationship,
            Position: y.Position,
            Email: y.Email,
            PhoneNo: y.PhoneNo,
            Organization: y.Organization
          } as IEmployeeCloseRelativeDetails;
        })
      );
    }
  }
  //#endregion
  // #region "Add CloseRelative"
  addCloseRelative(): void {
    /** Open AddCloseRelative dialog box*/
    const dialogRef = this.dialog.open(AddCloseRelativeComponent, {
      width: '800px',
      data: {
        employeeId: this.employeeId
      }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onAddCloseRelativeDetailListRefresh.subscribe(
      () => {
        this.getEmployeeCloseRelativeDetailList().subscribe(response => {
          this.subscribeEmployeeCloseRelativeDetailList(response);
        });
        this.commonLoader.hideLoader();
      }
    );
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion
  // #region "Delete Close Relative Info"
  deleteCloseRelativeInfo(EmployeeRelativeInfoId: number) {
    this.hrService.openDeleteDialog().subscribe(res => {
      if (res === true) {
        const model = {
          EmployeeRelativeInfoId: EmployeeRelativeInfoId
        };
        this.employeeHistoryService
          .deleteCloseRelativeDetail(model)
          .subscribe(response => {
            if (response.StatusCode === 200) {
              let index;
              this.employeeCloseRelativeList$.subscribe(data => {
                index = data.findIndex(
                  x => x.EmployeeRelativeInfoId === EmployeeRelativeInfoId
                );
                data.splice(index, 1);
                this.employeeCloseRelativeList$ = of(data);
              });
            }
          });
      }
    });
  }
  //#endregion

  //#region "get Employee Three Reference Detail List"
  getEmployeeThreeReferenceDetailList() {
    return this.employeeHistoryService.getEmployeeThreeReferenceDetailList(
      this.employeeId
    );
  }
  subscribeEmployeeThreeReferenceDetailList(response: any) {
    if (response.data.EmployeeRelativeInfoList !== undefined) {
      this.employeeThreeReferenceList$ = of(
        response.data.EmployeeRelativeInfoList.map(y => {
          return {
            EmployeeInfoReferencesId: y.EmployeeInfoReferencesId,
            Name: y.Name,
            Relationship: y.Relationship,
            Position: y.Position,
            Email: y.Email,
            PhoneNo: y.PhoneNo,
            Organization: y.Organization
          } as IEmployeeThreeReferenceDetails;
        })
      );
    }
  }
  //#endregion
  // #region "Add ThreeReference"
  addThreeReference(): void {
    /** Open AddThreeReference dialog box*/
    const dialogRef = this.dialog.open(AddThreeReferenceDetailsComponent, {
      width: '800px',
      data: {
        employeeId: this.employeeId
      }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onThreeReferenceDetailListRefresh.subscribe(
      () => {
        this.getEmployeeThreeReferenceDetailList().subscribe(response => {
          this.subscribeEmployeeThreeReferenceDetailList(response);
        });
        this.commonLoader.hideLoader();
      }
    );
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion
  // #region "Delete Employee Reference Info"
  deleteEmployeeReferenceInfo(EmployeeInfoReferencesId: number) {
    this.hrService.openDeleteDialog().subscribe(res => {
      if (res === true) {
        const model = {
          EmployeeInfoReferencesId: EmployeeInfoReferencesId
        };
        this.employeeHistoryService
          .deleteEmployeeReferenceInfoDetail(model)
          .subscribe(response => {
            if (response.StatusCode === 200) {
              let index;
              this.employeeThreeReferenceList$.subscribe(data => {
                index = data.findIndex(
                  x => x.EmployeeInfoReferencesId === EmployeeInfoReferencesId
                );
                data.splice(index, 1);
                this.employeeThreeReferenceList$ = of(data);
              });
            }
          });
      }
    });
  }
  //#endregion

  //#region "get Employee Other Skill Detail List"
  getEmployeeOtherSkillDetailList() {
    return this.employeeHistoryService.getEmployeeOtherSkillDetailList(
      this.employeeId
    );
  }
  subscribeEmployeeOtherSkillDetailList(response: any) {
    if (response.data.EmployeeOtherSkillsList !== undefined) {
      this.employeeOtherSkillList$ = of(
        response.data.EmployeeOtherSkillsList.map(y => {
          return {
            EmployeeOtherSkillsId: y.EmployeeOtherSkillsId,
            TypeOfSkill: y.TypeOfSkill,
            AbilityLevel: y.AbilityLevel,
            Experience: y.Experience,
            Remarks: y.Remarks
          } as IEmployeeOtherSkillDetails;
        })
      );
    }
  }
  //#endregion
  // #region "Add OtherSkill"
  addOtherSkill(): void {
    /** Open AddOtherSkill dialog box*/
    const dialogRef = this.dialog.open(AddOtherSkillsComponent, {
      width: '800px',
      data: {
        employeeId: this.employeeId
      }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onOtherSkillDetailListRefresh.subscribe(() => {
      this.getEmployeeOtherSkillDetailList().subscribe(response => {
        this.subscribeEmployeeOtherSkillDetailList(response);
      });
      this.commonLoader.hideLoader();
    });
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion
  // #region "Delete Employee Other Skill"
  deleteEmployeeOtherSkill(EmployeeOtherSkillsId: number) {
    this.hrService.openDeleteDialog().subscribe(res => {
      if (res === true) {
        const model = {
          EmployeeOtherSkillsId: EmployeeOtherSkillsId
        };
        this.employeeHistoryService
          .deleteEmployeeOtherSkillDetail(model)
          .subscribe(response => {
            if (response.StatusCode === 200) {
              let index;
              this.employeeOtherSkillList$.subscribe(data => {
                index = data.findIndex(
                  x => x.EmployeeOtherSkillsId === EmployeeOtherSkillsId
                );
                data.splice(index, 1);
                this.employeeOtherSkillList$ = of(data);
              });
            }
          });
      }
    });
  }
  //#endregion

  //#region "get Employee Salary Budget Detail List"
  getEmployeeSalaryBudgetDetailList() {
    return this.employeeHistoryService.getEmployeeSalaryBudgetDetailList(
      this.employeeId
    );
  }
  subscribeEmployeeSalaryBudgetDetailList(response: any) {
    if (response.data.EmployeeSalaryBudgetList !== undefined) {
      this.employeeSalaryBudgetList$ = of(
        response.data.EmployeeSalaryBudgetList.map(y => {
          return {
            EmployeeSalaryBudgetId: y.EmployeeSalaryBudgetId,
            Year: y.Year,
            // CurrencyId: y.CurrencyId,
            CurrencyName: y.CurrencyName,
            SalaryBudget: y.SalaryBudget,
            BudgetDisbursed: y.BudgetDisbursed
          } as IEmployeeSalaryBudgetDetails;
        })
      );
    }
  }
  //#endregion
  // #region "Add SalaryBudget"
  addSalaryBudget(): void {
    /** Open AddSalaryBudget dialog box*/
    const dialogRef = this.dialog.open(AddSalaryBudgetComponent, {
      width: '800px',
      data: {
        employeeId: this.employeeId
      }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onSalaryBudgetDetailListRefresh.subscribe(
      () => {
        this.getEmployeeSalaryBudgetDetailList().subscribe(response => {
          this.subscribeEmployeeSalaryBudgetDetailList(response);
        });
        this.commonLoader.hideLoader();
      }
    );
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion
  // #region "Delete Employee Salary Budget"
  deleteEmployeeSalaryBudget(EmployeeSalaryBudgetId: number) {
    this.hrService.openDeleteDialog().subscribe(res => {
      if (res === true) {
        const model = {
          EmployeeSalaryBudgetId: EmployeeSalaryBudgetId
        };
        this.employeeHistoryService
          .deleteEmployeeSalaryBudgetDetail(model)
          .subscribe(response => {
            if (response.StatusCode === 200) {
              let index;
              this.employeeSalaryBudgetList$.subscribe(data => {
                index = data.findIndex(
                  x => x.EmployeeSalaryBudgetId === EmployeeSalaryBudgetId
                );
                data.splice(index, 1);
                this.employeeSalaryBudgetList$ = of(data);
              });
            }
          });
      }
    });
  }
  //#endregion

  //#region "get Employee Language Detail List"
  getEmployeeLanguageDetailList() {
    return this.employeeHistoryService.getEmployeeLanguageDetailList(
      this.employeeId
    );
  }
  subscribeEmployeeLanguageDetailList(response: any) {
    if (response.data.EmployeeLanguagesList !== undefined) {
      this.employeeLanguageList$ = of(
        response.data.EmployeeLanguagesList.map(y => {
          return {
            SpeakLanguageId: y.SpeakLanguageId,
            Language: y.LanguageName,
            Writing: RatingAction[y.Writing],
            Speaking: RatingAction[y.Speaking],
            Reading: RatingAction[y.Reading],
            Listening: RatingAction[y.Listening]
          } as IEmployeeLanguageDetails;
        })
      );
    }
    this.commonLoader.hideLoader();
  }
  //#endregion
  // #region "Add Language"
  addLanguage(): void {
    /** Open Language dialog box*/
    const dialogRef = this.dialog.open(AddLanguageComponent, {
      width: '800px',
      data: {
        employeeId: this.employeeId
      }
    });
    // refresh the list after new request created
    dialogRef.componentInstance.onLanguageDetailListRefresh.subscribe(() => {
      this.getEmployeeLanguageDetailList().subscribe(response => {
        this.subscribeEmployeeLanguageDetailList(response);
      });
      this.commonLoader.hideLoader();
    });
    dialogRef.afterClosed().subscribe(() => {});
  }
  //#endregion
  // #region "Delete Employee Language Detail"
  deleteEmployeeLanguageDetail(SpeakLanguageId: number) {
    this.hrService.openDeleteDialog().subscribe(res => {
      if (res === true) {
        const model = {
          SpeakLanguageId: SpeakLanguageId
        };
        this.employeeHistoryService
          .deleteEmployeeLanguageDetail(model)
          .subscribe(response => {
            if (response.StatusCode === 200) {
              let index;
              this.employeeLanguageList$.subscribe(data => {
                index = data.findIndex(
                  x => x.SpeakLanguageId === SpeakLanguageId
                );
                data.splice(index, 1);
                this.employeeLanguageList$ = of(data);
              });
            }
          });
      }
    });
  }
  //#endregion

  actionEvents(event: any, type: any) {
    if (event.type === 'delete') {
      switch (type) {
        case 'historicalLog':
          this.deleteHistoricalLog(event.item.HistoryId);
          break;
        case 'education':
          this.deleteEducationDetail(event.item.EmployeeEducationsId);
          break;
        case 'outsideCountryHistory':
          this.deleteOutsideCountryInfo(
            event.item.EmployeeHistoryOutsideCountryId
          );
          break;
          case 'outsideOrganizationHistory':
            this.deleteOutsideOrganizationInfo(
              event.item.EmployeeHistoryOutsideOrganizationId
            );
            break;
        case 'closeReletive':
          this.deleteCloseRelativeInfo(event.item.EmployeeRelativeInfoId);
          break;
        case 'references':
          this.deleteEmployeeReferenceInfo(event.item.EmployeeInfoReferencesId);
          break;
        case 'otherSkill':
          this.deleteEmployeeOtherSkill(event.item.EmployeeOtherSkillsId);
          break;
        case 'salaryBudget':
          this.deleteEmployeeSalaryBudget(event.item.EmployeeSalaryBudgetId);
          break;
        case 'language':
          this.deleteEmployeeLanguageDetail(event.item.SpeakLanguageId);
          break;
      }
    }
  }
}
