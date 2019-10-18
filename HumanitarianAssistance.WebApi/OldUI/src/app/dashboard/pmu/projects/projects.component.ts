import { Component, OnInit, ViewChild } from '@angular/core';
import { PmuService } from '../pmu.service';
import { Tab, HrService } from '../../hr/hr.service';
import {
  CodeService,
  ProjectDetails,
  CurrencyData
} from '../../code/code.service';
import { GLOBAL } from '../../../shared/global';
import { ToastrService } from 'ngx-toastr';
import { DomSanitizer } from '@angular/platform-browser';
import { AccountsService } from '../../accounts/accounts.service';
import { ProjectsService } from './projects.service';
import { TranslateService } from '@ngx-translate/core';
import { DxDataGridComponent } from 'devextreme-angular';
import { ThirdPartyKey } from '../../../shared/thirdPartyKey';
import { AppSettingsService } from '../../../service/app-settings.service';
import { CommonService, GoogleObj } from '../../../service/common.service';
declare let jsPDF;
declare var $: any;
@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  selectedEmployeesReportData: any[];
  employeeNameDari: any = '';
  fatherNameDari: any = '';
  provinceNameDari: any = '';
  loading = false;
  contractPhoto: any;

  assignedEmployeePopup = false;
  activeEmployeeDetail: any;
  selectedBudgetLineName: string;
  selectedEmployeeDetail = [];
  selectedEmployees: BudgetLineEmployees[];
  selectdBudgetLineId: number;
  AllEmployeeDetails: any[];
  selectedRows: any[];

  base64Img = null;
  margins = {
    top: 70,
    bottom: 40,
    left: 30,
    width: 550
  };

  showInfoTabs: Tab[];
  openInfoTab = 0; // Tabs Toggle Flag
  currencyModel: any[]; // Currency Model Datasource
  selectedProjectModel: any;
  selectedProjectId: any;
  selectedProjectName: any;
  selectedProjectDescription: any;
  ShowCommentsActive = true; // Project Task & Activities
  ShowCommentsInActive = true; // Project Task & Activities
  windows: any;
  projectBudgetLine: any[]; // Related to Tab 2
  projectList: any[]; // Related to Tab 2
  projectBudgetReceivable: any[];
  projectBudgetPayable: any[];
  projectModel: any; // Add Project Details Model
  popupVisible = false; // Add Project Popup Flag Property
  Status: any[]; // Add Project Static Flag
  BudgetLineTypeArr: any[]; // B/L type Datasource
  AddReceivable: any = 0; // Flag for B/L and Receivable Grid
  selectedBudgetLineTypeId: any;
  selectedDescription: any;
  selectedReceivableId: any;
  selectedPayableId: any;
  projectBudgetReceived: any[];
  projectBudgetPaid: any[];
  Amount: any;
  pattern: any;
  defaultVisible = false;
  selectedListItem: any;

  selectedItemProject: any;
  assigneeArr: any[];

  popupActivityDocument: any;

  // Porject Document Variables
  popupVisibleDocument = false;
  showDocumentData: any[];
  selectedDropdown: any;
  docpath: any;
  popupVisibleAddDoc = false;
  addNewDocument: any;
  imageURLDoc: any;

  // Task & Activity variables
  allTaskArr: any[];
  popupVisibleAddTask = false;
  taskModel: any;
  priorityArr: any[];
  popupVisibleAddActivity = false;
  activityModel: any;
  selectedTask: any;
  withShadingOptionsVisible: any = false;
  selectedActivityId: string = null;

  contractEmployeePopup = false;

  //#region "Google Translator"
  public googleObj: GoogleObj = new GoogleObj();
  public key = ThirdPartyKey.googleKey;
  public result = '';
  private btnSubmit: any;
  //#endregion
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;

  defaultImagePath = 'assets/images/blank-image.png';

  constructor(
    private pmuservice: PmuService,
    private projectsService: ProjectsService,
    private accountservice: AccountsService,
    private hrService: HrService,
    private setting: AppSettingsService,
    private codeservice: CodeService,
    private toastr: ToastrService,
    private _DomSanitizer: DomSanitizer,
    private translate: TranslateService,
    private commonService: CommonService
  ) {
    translate.setDefaultLang('fa');
    this.showInfoTabs = [
      {
        id: 0,
        text: 'Project Description'
      },
      // {
      //   id: 1,
      //   text: "Documents"
      // },
      {
        id: 1,
        text: 'Budget'
      },
      {
        id: 2,
        text: 'Task & Activities'
      }
    ];

    this.windows = window;

    this.projectBudgetLine = [
      {
        ProjectId: null,
        BudgetLineId: null,
        BudgetLineTypeId: null,
        Description: null,
        StartDate: null,
        EndDate: null,
        AmountReceivable: null,
        AmountPayable: null
      }
    ];

    this.projectModel = {
      ProjectName: null,
      Description: null,
      StartDate: null,
      EndDate: null,
      CurrencyId: null,
      Budget: null,
      ReceivableAmount: null,
      PayableAmount: null,
      CurrentBalance: null,
      Status: null
    };

    this.Status = [
      {
        Id: 1,
        Value: 'Active'
      },
      {
        Id: 2,
        Value: 'Inactive'
      }
    ];

    this.projectBudgetReceivable = [
      {
        ProjectId: null,
        BudgetLineType: null,
        ExpectedDate: null,
        Amount: null
      }
    ];

    this.selectedProjectModel = {
      ProjectName: null,
      Description: null,
      StartDate: null,
      EndDate: null,
      CurrencyId: null,
      Budget: null,
      ReceivableAmount: null,
      PayableAmount: null,
      CurrentBalance: null,
      Status: null
    };

    this.addNewDocument = {
      DocumentName: '',
      DocumentFilePath: '',
      DocumentDate: ''
    };

    this.priorityArr = [
      {
        PriorityId: 1,
        PriorityName: 'High'
      },
      {
        PriorityId: 2,
        PriorityName: 'Medium'
      },
      {
        PriorityId: 3,
        PriorityName: 'Low'
      }
    ];

    this.assigneeArr = [
      {
        UserId: 1,
        UserName: 'Test User'
      },
      {
        UserId: 2,
        UserName: 'Demo User'
      },
      {
        UserId: 3,
        UserName: 'Test1 User'
      },
      {
        UserId: 4,
        UserName: 'Demo1 User'
      }
    ];
    this.switchLanguage('fr');
    // this.send();
  }
  switchLanguage(language: string) {
    this.translate.use(language);
  }

  ngOnInit() {
    this.getCurrencyCodeList();
    this.getBudgetLineTypes();
    this.getAllProjectDetails();
  }

  //#region "Translate Function"
  send(inputText) {
    // this.googleObj.q = "Employee Contract";
    this.googleObj.q = inputText;
    return this.commonService.translate(this.googleObj, this.key).subscribe(
      (res: any) => {
        return res;
      },
      err => {}
    );
  }
  //#endregion

  //#region
  // Render SideBar Project Details
  getAllProjectDetails() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_ProjectBudget_GetAllProjectDetail
      )
      .subscribe(
        data => {
          this.projectList = [];
          if (
            data.data.ProjectDetailList != null &&
            data.data.ProjectDetailList.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.ProjectDetailList.forEach(element => {
              this.projectList.push(element);
            });
            this.projectModel = data.data.ProjectDetailList[0]; // For First Tym loading
            this.selectedProjectModel = data.data.ProjectDetailList[0];
            this.selectedItemProject = data.data.ProjectDetailList[0];

            if (this.currencyModel != null && this.currencyModel.length > 0) {
              this.projectModel.CurrencyCode = this.currencyModel.filter(
                x => x.CurrencyId === data.data.ProjectDetailList[0].CurrencyId
              )[0].CurrencyCode;
            }

            this.projectModel.StatusValue = this.Status.filter(
              x => x.Id === data.data.ProjectDetailList[0].Status
            )[0].Value;

            this.selectedProjectModel.CurrencyCode = this.projectModel.CurrencyCode;
            this.selectedProjectModel.StatusValue = this.projectModel.StatusValue;

            this.selectedProjectId = data.data.ProjectDetailList[0].ProjectId;
            this.selectedProjectName =
              data.data.ProjectDetailList[0].ProjectName;
            this.selectedProjectDescription =
              data.data.ProjectDetailList[0].Description;
            this.getAllBudgetLineDetails(this.selectedProjectId); // Initial Binding Of tabs
            this.getAllDocumentDetails(this.selectedProjectId);
            this.GetAllTask(this.selectedProjectId);
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  // Get Currency Dropdown List
  getCurrencyCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_CurrencyCodes_GetAllCurrency
      )
      .subscribe(
        data => {
          this.currencyModel = [];
          if (data.data.CurrencyList != null && data.StatusCode === 200) {
            data.data.CurrencyList.forEach(element => {
              this.currencyModel.push(element);
            });
          }
        },

        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  // Get getBudgetLineTypes
  getBudgetLineTypes() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_BudgetLine_GetBudgetLineTypes
      )
      .subscribe(
        data => {
          this.BudgetLineTypeArr = [];
          if (data.data.BudgetLineTypeList != null && data.StatusCode === 200) {
            data.data.BudgetLineTypeList.forEach(element => {
              this.BudgetLineTypeArr.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  // Selected Project Value OnClick
  selectedProject(item) {
    this.selectedItemProject = item;
    this.selectedListItem = item;
    // tslint:disable-next-line:max-line-length
    this.selectedProjectModel = item; // Value assigned when user clicked on add project icon and then on edit detail icon then datasource gets empty.
    this.projectModel = item; // For Project Description DataBinding

    if (this.currencyModel != null && this.currencyModel.length > 0) {
      this.projectModel.CurrencyCode = this.currencyModel.filter(
        x => x.CurrencyId === item.CurrencyId
      )[0].CurrencyCode;
    }
    this.projectModel.StatusValue = this.Status.filter(
      x => x.Id === item.Status
    )[0].Value;
    this.selectedProjectModel.CurrencyCode = this.projectModel.CurrencyCode;
    this.selectedProjectModel.StatusValue = this.projectModel.StatusValue;

    this.selectedProjectId = item.ProjectId;
    this.selectedProjectName = item.ProjectName;
    this.selectedProjectDescription = item.Description;
    this.AddReceivable = 0;

    this.getAllBudgetLineDetails(this.selectedProjectId); // Project Budget Line Hit
    this.getAllDocumentDetails(this.selectedProjectId);
    this.GetAllTask(this.selectedProjectId); // Task & Activity Hit
  }

  // Open Project Modal
  addProjectModal(flag) {
    if (flag === 1) {
      // Flag 1 for add project details
      this.projectModel = {
        ProjectName: null,
        Description: null,
        StartDate: null,
        EndDate: null,
        CurrencyId: null,
        Budget: null,
        ReceivableAmount: null,
        PayableAmount: null,
        CurrentBalance: null,
        Status: null
      };
    } else {
      // Flag 2 for edit project details
      this.projectModel = this.selectedProjectModel;
    }
    this.popupVisible = true;
  }

  // Add project popup
  HidePopup() {
    this.popupVisible = false;
    this.projectModel = this.selectedProjectModel; // Needs To be Changed as the values changes
  }

  // Save From Project Popup (Plus Icon)
  AddEditProjectDetails(model) {
    if (model.ProjectId != null && model.ProjectId !== 0) {
      this.EditProjectDetails(model);
    } else {
      this.AddProjectDetails(model);
    }
  }

  // OnSubmit Add New Project
  AddProjectDetails(model) {
    this.codeservice
      .AddProjectDetails(
        this.setting.getBaseUrl() + GLOBAL.API_ProjectBudget_AddProjectDetails,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Project Added Successfully!!!');
          }
          this.getAllProjectDetails();
          this.popupVisible = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getAllProjectDetails();
        }
      );
  }

  // OnSubmit Edit New Project
  EditProjectDetails(model) {
    this.codeservice
      .AddProjectDetails(
        this.setting.getBaseUrl() + GLOBAL.API_ProjectBudget_EditProjectDetails,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Project Updated Successfully!!!');
          }
          this.getAllProjectDetails();
          this.popupVisible = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getAllProjectDetails();
        }
      );
  }

  // On Tab Selection
  openInfoTabs(e) {
    this.openInfoTab = e.itemIndex;
  }

  // Get All Budget Lines Details
  getAllBudgetLineDetails(ProjectId) {
    this.codeservice
      .GetAllBudgetLineReceivable(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_GetAllBudgetLineDetails,
        ProjectId
      )
      .subscribe(
        data => {
          this.projectBudgetLine = [];
          if (
            data.data.ProjectBudgetLineList != null &&
            data.StatusCode === 200
          ) {
            data.data.ProjectBudgetLineList.forEach(element => {
              this.projectBudgetLine.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  // Budget Line DataGrid Events
  logEvent(eventName, obj) {
    if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
      this.editProjectBudgetLine(value);
    }
    if (eventName === 'RowInserting') {
      this.addProjectBudgetLine(obj.data);
    }
  }

  // Project Budget Line Add
  addProjectBudgetLine(model) {
    model.ProjectId = this.selectedProjectId;
    model.StartDate = new Date(
      new Date(model.StartDate).getFullYear(),
      new Date(model.StartDate).getMonth(),
      new Date(model.StartDate).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );
    model.EndDate = new Date(
      new Date(model.EndDate).getFullYear(),
      new Date(model.EndDate).getMonth(),
      new Date(model.EndDate).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );

    this.codeservice
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_AddBudgetLine,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('BudgetLine Added Successfully!!!');
          }
          this.getAllBudgetLineDetails(this.selectedProjectId);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getAllBudgetLineDetails(this.selectedProjectId);
        }
      );
  }

  // Project Budget Line Edit
  editProjectBudgetLine(model) {
    model.ProjectId = this.selectedProjectId;
    model.StartDate = new Date(
      new Date(model.StartDate).getFullYear(),
      new Date(model.StartDate).getMonth(),
      new Date(model.StartDate).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );
    model.EndDate = new Date(
      new Date(model.EndDate).getFullYear(),
      new Date(model.EndDate).getMonth(),
      new Date(model.EndDate).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );

    this.codeservice
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_EditBudgetLine,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('BudgetLine Updated Successfully!!!');
          }
          this.getAllBudgetLineDetails(this.selectedProjectId);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getAllBudgetLineDetails(this.selectedProjectId);
        }
      );
  }

  // Project Budget Line Receivable Link On Click
  addReceivableClicked(data) {
    this.selectedBudgetLineTypeId = data.data.BudgetLineTypeId;
    this.selectedDescription = data.data.Description;
    localStorage.setItem('SelectedBudgetLineId', data.data.BudgetLineId);
    this.AddReceivable = 1;
    this.getBudgetLineReceivableDetails();
  }

  // Project Budget Line Payable Link On Click
  addPayableClicked(data) {
    this.selectedBudgetLineTypeId = data.data.BudgetLineTypeId;
    this.selectedDescription = data.data.Description;
    localStorage.setItem('SelectedBudgetLineId', data.data.BudgetLineId);
    this.AddReceivable = 3;
    this.getBudgetLinePayableDetails();
  }

  // Task & Activity tabs Accordion Toggle
  ActiveComments() {
    if (this.ShowCommentsActive === true) {
      this.ShowCommentsActive = false;
    } else {
      this.ShowCommentsActive = true;
    }
  }

  // Task & Activity tabs Accordion Toggle
  InActiveComments() {
    if (this.ShowCommentsInActive === true) {
      this.ShowCommentsInActive = false;
    } else {
      this.ShowCommentsInActive = true;
    }
  }

  // Back Button Toggle Functionality
  ShowBudgetLineGrid(gridParam) {
    this.AddReceivable = gridParam;
  }

  // Get All Budget Lines Receivable Details
  getBudgetLineReceivableDetails() {
    this.codeservice
      .GetAllBudgetLineReceivableByProjectId(
        this.setting.getBaseUrl() +
          GLOBAL.API_PMU_GetBudgetLineReceivableDetails,
        this.selectedProjectId,
        localStorage.getItem('SelectedBudgetLineId')
      )
      .subscribe(
        data => {
          this.projectBudgetReceivable = [];
          if (
            data.data.BudgetReceivableList != null &&
            data.StatusCode === 200
          ) {
            data.data.BudgetReceivableList.forEach(element => {
              this.projectBudgetReceivable.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  getBudgetLinePayableDetails() {
    this.codeservice
      .GetAllBudgetLineReceivableByProjectId(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_GetBudgetLinePayableDetails,
        this.selectedProjectId,
        localStorage.getItem('SelectedBudgetLineId')
      )
      .subscribe(
        data => {
          this.projectBudgetPayable = [];
          if (data.data.BudgetPayableList != null && data.StatusCode === 200) {
            data.data.BudgetPayableList.forEach(element => {
              this.projectBudgetPayable.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  // Receivable Grid Events
  logEventReceivable(eventName, obj) {
    if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
      this.editBudgetLineReceivable(value);
    }
    if (eventName === 'RowInserting') {
      this.addBudgetLineReceivable(obj.data);
    }
  }

  // Project Budget Line Receivable Add Installment
  addBudgetLineReceivable(model) {
    model.ProjectId = this.selectedProjectId;
    model.BudgetLineId = localStorage.getItem('SelectedBudgetLineId');
    model.BudgetLineType = this.selectedBudgetLineTypeId;
    model.Description = this.selectedDescription;
    this.codeservice
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_AddBudgetLineReceivable,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Receivable Amount Added Successfully!!!');
          }
          this.getBudgetLineReceivableDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getBudgetLineReceivableDetails();
        }
      );
  }

  // Project Budget Line Receivable Edit Installment
  editBudgetLineReceivable(model) {
    model.ProjectId = this.selectedProjectId;
    model.BudgetLineId = localStorage.getItem('SelectedBudgetLineId');
    model.BudgetLineType = this.selectedBudgetLineTypeId;
    model.Description = this.selectedDescription;
    this.codeservice
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_EditBudgetLineReceivable,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Receivable Amount Updated Successfully!!!');
          }
          this.getBudgetLineReceivableDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getBudgetLineReceivableDetails();
        }
      );
  }

  logEventPayable(eventName, obj) {
    if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
      this.editBudgetLinePayable(value);
    }
    if (eventName === 'RowInserting') {
      this.addBudgetLinePayable(obj.data);
    }
  }

  addBudgetLinePayable(model) {
    model.ProjectId = this.selectedProjectId;
    model.BudgetLineId = localStorage.getItem('SelectedBudgetLineId');
    model.BudgetLineType = this.selectedBudgetLineTypeId;
    this.codeservice
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_AddBudgetLinePayable,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Payable Amount Added Successfully!!!');
          }
          this.getBudgetLinePayableDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getBudgetLinePayableDetails();
        }
      );
  }

  // Project Budget Line Payable Edit Installment
  editBudgetLinePayable(model) {
    model.ProjectId = this.selectedProjectId;
    model.BudgetLineId = localStorage.getItem('SelectedBudgetLineId');
    model.BudgetLineType = this.selectedBudgetLineTypeId;
    this.codeservice
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_EditBudgetLinePayable,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Payable Amount Updated Successfully!!!');
          }
          this.getBudgetLinePayableDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getBudgetLinePayableDetails();
        }
      );
  }

  addReceivedClicked(data) {
    this.AddReceivable = 2;
    this.Amount = data.data.Amount;
    this.selectedReceivableId = data.data.BudgetReceivalbeId;
    this.getBudgetLineReceivedDetails();
  }

  setStateValue(rowData: any, value: any): void {
    if (value > this.Amount) {
      this.pattern = true;
    }
    rowData.BudgetLineId = null;
    (<any>this).defaultSetCellValue(rowData, value);
  }

  getBudgetLineReceivedDetails() {
    this.codeservice
      .GetAllBudgetLineReceivedDetails(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_GetBudgetLineReceivedDetails,
        this.selectedProjectId,
        localStorage.getItem('SelectedBudgetLineId'),
        this.selectedReceivableId
      )
      .subscribe(
        data => {
          this.projectBudgetReceived = [];
          if (
            data.data.BudgetReceivedAmountList != null &&
            data.StatusCode === 200
          ) {
            data.data.BudgetReceivedAmountList.forEach(element => {
              this.projectBudgetReceived.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  getBudgetLinePayableAmountDetails() {
    this.codeservice
      .GetAllBudgetLinePaidDetails(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_GetBudgetLinePaidDetails,
        this.selectedProjectId,
        localStorage.getItem('SelectedBudgetLineId'),
        this.selectedPayableId
      )
      .subscribe(
        data => {
          this.projectBudgetPaid = [];
          if (
            data.data.BudgetPaidAmountList != null &&
            data.StatusCode === 200
          ) {
            data.data.BudgetPaidAmountList.forEach(element => {
              this.projectBudgetPaid.push(element);
            });
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }

  logEventReceivedAmount(eventName, obj) {
    if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
      this.editBudgetLineReceived(value);
    }
    if (eventName === 'RowInserting') {
      this.addBudgetLineReceived(obj.data);
    }
  }

  addBudgetLineReceived(model) {
    model.BudgetReceivalbeId = this.selectedReceivableId;
    this.codeservice
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_AddBudgetLineReceived,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Received Amount Added Successfully!!!');
          }
          this.getBudgetLineReceivedDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getBudgetLineReceivedDetails();
        }
      );
  }

  editBudgetLineReceived(model) {
    this.codeservice
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_EditBudgetLineReceived,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Received Amount Updated Successfully!!!');
          }
          this.getBudgetLineReceivedDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getBudgetLineReceivedDetails();
        }
      );
  }

  addPayableAmountClicked(data) {
    this.AddReceivable = 4;
    this.selectedPayableId = data.data.BudgetPayableId;
    this.getBudgetLinePayableAmountDetails();
  }

  logEventPaidAmount(eventName, obj) {
    if (eventName === 'RowUpdating') {
      const value = Object.assign(obj.oldData, obj.newData); // Merge old data with new Data
      this.editBudgetLinePaid(value);
    }
    if (eventName === 'RowInserting') {
      this.addBudgetLinePaid(obj.data);
    }
  }

  addBudgetLinePaid(model) {
    model.BudgetPayableId = this.selectedPayableId;
    this.codeservice
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_AddBudgetLinePaid,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Paid Amount Added Successfully!!!');
          }
          this.getBudgetLinePayableAmountDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getBudgetLinePayableAmountDetails();
        }
      );
  }

  editBudgetLinePaid(model) {
    model.BudgetPayableId = this.selectedPayableId;
    this.codeservice
      .AddExchangeRate(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_EditBudgetLinePaid,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Paid Amount Updated Successfully!!!');
          }
          this.getBudgetLinePayableAmountDetails();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.getBudgetLinePayableAmountDetails();
        }
      );
  }

  toggleDefault() {
    this.defaultVisible = !this.defaultVisible;
  }

  activityDoc(model) {
    this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + 'nodoc.pdf'
    );
    this.popupActivityDocument = !this.popupActivityDocument;
  }
  //#endregion

  // #region "Project Document"

  showDocs() {
    // this.getAllDocumentDetails(this.selectedProjectId);
    this.popupVisibleDocument = !this.popupVisibleDocument;
  }

  addDocument() {
    this.popupVisibleAddDoc = !this.popupVisibleAddDoc;
  }

  cancelDeleteVoucher() {
    this.popupVisibleAddDoc = !this.popupVisibleAddDoc;
  }

  selectedDoc(e) {
    this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + e.value
    );
    // this.popupVisibleDocument = true;
  }

  onFormSubmitDocAdd(data: any) {
    this.addNewDocument.DocumentFilePath = this.imageURLDoc;
    data.ProjectId = this.selectedProjectId;
    this.AddProjectDocument(data);
  }

  AddProjectDocument(model) {
    this.accountservice
      .AddProjectDocument(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_AddProjectDocument,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Document Added Successfully!!!');
          }
          this.getAllDocumentDetails(this.selectedProjectId);
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
          }
        }
      );
    this.cancelDeleteVoucher();
  }

  // Event Fire on image Selection
  onImageSelectDoc(event: any) {
    const file: File = event.value[0];
    const myReader: FileReader = new FileReader();
    myReader.readAsDataURL(file);
    myReader.onloadend = e => {
      this.imageURLDoc = myReader.result;
    };
  }

  getAllDocumentDetails(ProjectId) {
    this.hrService
      .GetAllProjectDocumentDetails(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_GetProjectDocumentDetail,
        ProjectId
      )
      .subscribe(
        data => {
          this.showDocumentData = [];
          this.selectedDropdown = '';
          if (
            data.data.ProjectDocumentList != null &&
            data.data.ProjectDocumentList.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.ProjectDocumentList.forEach(element => {
              this.showDocumentData.push(element);
            });
            this.selectedDropdown = this.showDocumentData[
              this.showDocumentData.length - 1
            ].DocumentGUID;
            this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
              this.setting.getDocUrl() + this.selectedDropdown
            );
          } else {
            this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
              this.setting.getDocUrl() + 'nodoc.pdf'
            );
          }
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
        }
      );
  }
  // #endregion "Project Document"

  //#region "Task And Activity"
  GetAllTask(ProjectId) {
    this.codeservice
      .GetAllTaskAndActivityList(
        this.setting.getBaseUrl() + GLOBAL.API_TaskAndActivity_GetAllTask,
        ProjectId
      )
      .subscribe(
        data => {
          this.allTaskArr = [];
          if (data.data.TaskMasterList != null && data.StatusCode === 200) {
            data.data.TaskMasterList.forEach(element => {
              this.allTaskArr.push(element);
            });
          }
          const arr = this.allTaskArr;
        },
        error => {
          if (error.StatusCode === 500) {
            // this.toastr.error("Internal Server Error....");
          } else if (error.StatusCode === 401) {
            // this.toastr.error("Unauthorized Access Error....");
          } else if (error.StatusCode === 403) {
            // this.toastr.error("Forbidden Error....");
          }
        }
      );
  }

  addTaskShowPopup() {
    this.taskModel = {
      TaskId: null,
      TaskName: null,
      Description: null,
      Priority: null,
      Assignee: null
    };
    this.popupVisibleAddTask = true;
  }

  HidePopupTaskPopup() {
    this.popupVisibleAddTask = !this.popupVisibleAddTask;
  }

  AddEditTaskDetails(model) {
    model.ProjectId = this.selectedProjectId;
    this.codeservice
      .AddTaskDetails(
        this.setting.getBaseUrl() + GLOBAL.API_TaskAndActivity_AddTask,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Task Added Successfully!!!');
          }
          this.GetAllTask(this.selectedProjectId);
          this.popupVisibleAddTask = !this.popupVisibleAddTask;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.GetAllTask(this.selectedProjectId);
        }
      );
  }

  addActivityShowPopup(event) {
    this.selectedTask = event.TaskId;
    this.activityModel = {
      ActivityName: null,
      Priority: null,
      Description: null,
      Status: null
    };
    this.popupVisibleAddActivity = !this.popupVisibleAddActivity;
  }

  HidePopupActivityPopup() {
    this.popupVisibleAddActivity = !this.popupVisibleAddActivity;
  }

  AddEditActivityDetails(model) {
    model.TaskId = this.selectedTask;
    this.codeservice
      .AddProjectDetails(
        this.setting.getBaseUrl() +
          GLOBAL.API_TaskAndActivity_AddActivityDetail,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Activity Added Successfully!!!');
          }
          this.GetAllTask(this.selectedProjectId);
          this.popupVisibleAddActivity = !this.popupVisibleAddActivity;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.GetAllTask(this.selectedProjectId);
        }
      );
  }

  toggleWiithShadingOptions(ActivityId) {
    this.selectedActivityId = '#activityComments' + ActivityId;
    this.withShadingOptionsVisible = !this.withShadingOptionsVisible;
    // ************ Call the get-comments api***************** (on the basis of activity id)
  }
  //#endregion "Task And Activity"

  //#region "Assigned Employees"

  assignedEmployees(rowData) {
    this.selectedBudgetLineName = rowData.data.Description;
    this.selectdBudgetLineId = rowData.data.BudgetLineId;
    this.getAllEmployeeInBudgetLine();
    this.assignedEmployeePopup = true;
  }

  getAllActiveEmployeeDetail() {
    const employeeType = 2;
    this.hrService
      .GetAllEmployees(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_GetAllEmployeeDetail,
        employeeType,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(data => {
        this.activeEmployeeDetail = [];
        this.AllEmployeeDetails = [];
        if (
          data.StatusCode === 200 &&
          data.data.EmployeeDetailsList.length > 0
        ) {
          this.AllEmployeeDetails = data.data.EmployeeDetailsList;
          data.data.EmployeeDetailsList.forEach(element => {
            const arr = this.selectedEmployeeDetail.filter(
              x => x.EmployeeId === element.EmployeeID
            );
            if (arr.length === 0) {
              this.activeEmployeeDetail.push({
                EmployeeId: element.EmployeeID,
                EmployeeName: element.EmployeeName,
                Email: element.Email,
                Profession: element.Profession,
                ExperienceYear: element.ExperienceYear,
                ExperienceMonth: element.ExperienceMonth,
                Status: arr.length > 0 ? 'selected' : null
              });
            }
          });
          this.selectedRows = this.activeEmployeeDetail.map(
            employee => employee.EmployeeId
          );
        }
      });
  }
  //#endregion

  //#region "Employee Contract Details Report"

  //getEmployeeContractReport(rowData) {
  //  this.assignedEmployeePopup = false;
  //  this.loading = true;
  //  this.projectsService
  //    .GetSelectedEmployeeContract(
  //      this.setting.getBaseUrl() + GLOBAL.API_Code_GetSelectedEmployeeContract,
  //      // tslint:disable-next-line:radix
  //      parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
  //      this.selectedProjectId,
  //      this.selectdBudgetLineId,
  //      rowData.data.EmployeeId
  //    )
  //    .subscribe(data => {
  //      this.selectedEmployeesReportData = [];
  //      if (
  //        data.StatusCode === 200 &&
  //        data.data.EmployeeContractModellst.length > 0
  //      ) {
  //        this.selectedEmployeesReportData =
  //          data.data.EmployeeContractModellst[0];
  //        this.contractPhoto =
  //          this.setting.getDocUrl() +
  //          data.data.EmployeeContractModellst[0].EmployeeImage;
  //        // region "Language Conversion API"
  //        if (data.data.EmployeeContractModellst[0].EmployeeName != null) {
  //          this.googleObj.q =
  //            data.data.EmployeeContractModellst[0].EmployeeName;
  //          this.commonService.translate(this.googleObj, this.key).subscribe(
  //            (res: any) => {
  //              const text = JSON.parse(res._body);
  //              this.employeeNameDari =
  //                text.data.translations[0].translatedText;

  //              // Next
  //              if (data.data.EmployeeContractModellst[0].FatherName != null) {
  //                this.googleObj.q =
  //                  data.data.EmployeeContractModellst[0].FatherName;
  //                this.commonService
  //                  .translate(this.googleObj, this.key)
  //                  .subscribe(
  //                    (res1: any) => {
  //                      const text1 = JSON.parse(res1._body);
  //                      this.fatherNameDari =
  //                        text1.data.translations[0].translatedText;
  //                    },
  //                    err => {}
  //                  );
  //              }

  //              // Next
  //              if (data.data.EmployeeContractModellst[0].Province != null) {
  //                this.googleObj.q =
  //                  data.data.EmployeeContractModellst[0].Province;
  //                this.commonService
  //                  .translate(this.googleObj, this.key)
  //                  .subscribe(
  //                    (res1: any) => {
  //                      const text1 = JSON.parse(res1._body);
  //                      this.provinceNameDari =
  //                        text1.data.translations[0].translatedText;
  //                      // Popup Opens
  //                      // this.assignedEmployeePopup = false;
  //                      this.contractEmployeePopup = true;
  //                      this.loading = false;
  //                    },
  //                    err => {}
  //                  );
  //              }
  //            },
  //            err => {}
  //          );
  //        }
  //        //#endregion
  //      } else {
  //        // this.contractEmployeePopup = true;
  //        this.toastr.warning('No contract to download!');
  //        this.loading = false;
  //      }
  //    });
  //}

  getAllEmployeeInBudgetLine() {
    this.projectsService
      .GetAllEmployeesInBudgetLine(
        this.setting.getBaseUrl() + GLOBAL.API_PMU_GetAllEmployeesBudgetLine,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
        this.selectedProjectId,
        this.selectdBudgetLineId
      )
      .subscribe(data => {
        this.selectedEmployeeDetail = [];
        if (
          data.StatusCode === 200 &&
          data.data.GetAllEmployeesInBudgetLine.length > 0
        ) {
          this.selectedEmployeeDetail = data.data.GetAllEmployeesInBudgetLine;
        }
        this.getAllActiveEmployeeDetail();
      });
  }

  // selectedRows : any[];
  // selectionChangedHandler(selectedRowData, data) {
  selectionChangedHandler(selectedRowData) {
    this.selectedEmployeeDetail.push({
      EmployeeId: selectedRowData.data.EmployeeId,
      EmployeeName: selectedRowData.data.EmployeeName,
      Status: 'Recent'
    });
    const itemIndex = this.activeEmployeeDetail.findIndex(
      x => x.EmployeeId === selectedRowData.data.EmployeeId
    );
    this.activeEmployeeDetail.splice(itemIndex, 1);
  }

  removeAssignedEmployees(data) {
    const itemIndex = this.selectedEmployeeDetail.findIndex(
      x => x.EmployeeId === data.data.EmployeeId
    );
    this.selectedEmployeeDetail.splice(itemIndex, 1);

    const findEmployee = this.AllEmployeeDetails.filter(
      x => x.EmployeeID === data.data.EmployeeId
    );
    this.activeEmployeeDetail.push({
      EmployeeId: findEmployee[0].EmployeeID,
      EmployeeName: findEmployee[0].EmployeeName,
      Email: findEmployee[0].Email,
      Profession: findEmployee[0].Profession,
      ExperienceYear: findEmployee[0].ExperienceYear,
      ExperienceMonth: findEmployee[0].ExperienceMonth,
      Status: null
    });
  }

  saveAssignedEmployees() {
    if (this.selectedEmployeeDetail.length > 0) {
      this.selectedEmployees = [];
      this.selectedEmployeeDetail.forEach(element => {
        this.selectedEmployees.push({
          // tslint:disable-next-line:radix
          OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
          ProjectId: this.selectedProjectId,
          BudgetLineId: this.selectdBudgetLineId,
          EmployeeId: element.EmployeeId,
          EmployeeName: element.EmployeeName,
          IsActive: true
        });
      });
      this.projectsService
        .AssignEmployeeToBudgetLine(
          this.setting.getBaseUrl() + GLOBAL.API_PMU_AssignEmployeeToBudgetLine,
          this.selectedEmployees
        )
        .subscribe(data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Employees assigned successfully!');
            this.assignedEmployeePopup = false;
          // tslint:disable-next-line:curly
          } else
            this.toastr.error(
              'Error in assigning the employees to budget line!'
            );
        });
    } else {
      this.toastr.error('Please select some employees!');
    }
  }
  //#endregion

  //#region "Generate Contract PDF"

  generatePdf() {
    const pdf = new jsPDF('p', 'pt', 'a4');
    pdf.setFontSize(18);
    pdf.fromHTML(
      document.getElementById('contractReportPdf'),
      this.margins.left, // x coord
      this.margins.top,
      {
        // y coord
        width: this.margins.width // max width of content on PDF
      },
      function(dispose) {
        this.headerFooterFormatting(pdf, pdf.internal.getNumberOfPages());
      },
      this.margins
    );

    pdf.save('abc');
    // var iframe = document.createElement('iframe');
    // iframe.setAttribute('style', 'position:absolute;right:0; top:0; bottom:0; height:100%; width:650px; padding:20px;');
    // document.body.appendChild(iframe);

    // iframe.src = pdf.output('datauristring');

    // var pdf = new jsPDF('p', 'pt', 'a4'),
    //   pdfConf = {
    //     pagesplit: true,
    //     background: '#fff'
    //   };
    // pdf.addHTML($("#contractReportPdf"), 0, 15, pdfConf, function () {
    //   // pdf.addPage();
    //   pdf.save('Employee-Contract.pdf');
    // });

    // var options = {};
    // var pdf = new jsPDF('p', 'pt', 'a4');
    // pdf.addHTML($("#pensionReportPdf"), 0, 15, options, function () {
    //   pdf.save('Employee-Pension.pdf');
    // });
  }

  headerFooterFormatting(doc, totalPages) {
    for (let i = totalPages; i >= 1; i--) {
      doc.setPage(i);
      // header
      this.header(doc);

      this.footer(doc, i, totalPages);
      doc.page++;
    }
  }

  header(doc) {
    doc.setFontSize(30);
    doc.setTextColor(40);
    doc.setFontStyle('normal');

    if (this.base64Img) {
      doc.addImage(this.base64Img, 'JPEG', this.margins.left, 10, 40, 40);
    }

    doc.text('Report Header Template', this.margins.left + 50, 40);

    doc.line(3, 70, this.margins.width + 43, 70); // horizontal line
  }

  footer(doc, pageNumber, totalPages) {
    const str = 'Page ' + pageNumber + ' of ' + totalPages;

    doc.setFontSize(10);
    doc.text(str, this.margins.left, doc.internal.pageSize.height - 20);
  }

  // imgToBase64('octocat.jpg', function(base64) {
  //   this.base64Img = base64;
  // })
  //#endregion
}

interface BudgetLineEmployees {
  OfficeId: number;
  ProjectId: number;
  BudgetLineId: number;
  EmployeeId: number;
  EmployeeName: string;
  IsActive: boolean;
  StartDate?: Date;
  EndDate?: Date;
}
