import { Component, OnInit } from '@angular/core';
import { Tab, HrService, SexTypes, GeneralInfo } from '../hr.service';
import { ToastrService } from 'ngx-toastr';
import { GLOBAL } from '../../../shared/global';
import { empty } from 'rxjs/observable/empty';
import { DomSanitizer } from '@angular/platform-browser';
import { AccountsService } from '../../accounts/accounts.service';
import { CodeService } from '../../code/code.service';
import { applicationPages } from '../../../shared/application-pages-enum';
import { CommonService } from '../../../service/common.service';
import { AppSettingsService } from '../../../service/app-settings.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {
  //#region "VARIABLES"

  showInfoTabs: Tab[];
  addEmployeeLoadingPopup = false;
  isEditingAllowed = false;
  selectedOffice = null;

  officeDropdownList: any[] = [];
  documentTypeList: any;

  openInfoTab = 0;

  // Employee Popup
  popupAddEmployeeInfoVisible = false;
  popupImageUpdateVisible = false;
  popupEmployeeInfoVisible = false;
  popupAssignLeaveVisible = false;

  // Loader
  addEmployeePopupLoading: boolean;
  editEmployeePopupLoading: boolean;
  profileImageChangePopupLoading: boolean;
  professionalInfoLoading = false;
  leaveInfoLoading = false;
  loading: boolean;

  // dateOfBirth: string;
  currentDate = new Date();

  showData: any;
  empGeneral: any;
  imageURL: any;
  defaultImagePath: string;
  fileURL: any;
  docURL: any;
  countryId: number;
  officeId: number;
  employeeId: number;
  tabEventValue: number;
  leaveInfoDataSource: LeaveInfoModel[];
  leaveInfoData: LeaveInfoData;
  assignLeaveToEmployee: number;
  disabledDates: any[];
  leaveReasonId: number;
  assignUnitIsValid = true;
  leaveReasonTypeDropdown: any;
  financialYearDropdown: any;

  selectedLeaveList: any[];
  // hiredOnDate: any;

  // for edit form (two way binding)
  
  CountryId: number;
  ProvinceId: number;
  SexId: number;
  OfficeId: number;
  DepartmentId: number;
  editGeneralShowData: GeneralShowData;

  activeEmployeeInfo: any[];
  prospectiveEmployeeInfo: any[];
  terminatedEmployeeInfo: any[];

  showActiveEmployeeData: any[];
  showProspectiveEmployeeData: any[];
  showTerminatedEmployeeData: any[];

  employeeListDetail: any;

  sexTypes: SexTypes[];
  professionTypeDropdown: any[];
  qualificationTypeDropdown: any[];
  countryTypeDropdown: any[];
  stateTypeDropdown: any[];
  employeeTypeDropdown: any[];
  officeTypeDropdown: any[];
  designationDropdown: any[];
  departmentTypeDropdown: any[];

  windows: any;
  rules: any;

  maxDate: Date = new Date(
    this.currentDate.getFullYear() - 18,
    this.currentDate.getMonth(),
    this.currentDate.getDate()
  );
  minDate: Date = new Date(
    this.currentDate.getFullYear() - 80,
    this.currentDate.getMonth(),
    this.currentDate.getDate()
  );

  pattern: any = /^\(\d{3}\)\ \d{3}-\d{4}$/i;
  namePattern: any = /^[^0-9]+$/;

  empDocuments: any;
  showDocumentData: any[];
  popupAddDocumentVisible = false;
  popupEditDocumentVisible = false;
  EmployeeId: any;
  popupEditEmployeeHistoryVisible = false;
  empHealthInfo: any;

  // Document Variables
  docpath: any;
  addNewDocument: any;
  popupVisibleDocument = false;
  popupVisibleAddDoc = false;
  imageURLDoc: any;
  EmployeeDocumentDetails: any[];
  selectedDropdown: any;

  // loader
  addDocPopupLoading = false;

  selectedItemEmployee: any;

  selectedIndex: 0;
  firstTabValue: any;

  Flag = 0; // To prevent repeat tab click event (left side bar)
  Office: number;
  officecodelist: any[];
  imageFlag = true;
  selectedProfileImage: any[] = [];

  maritalStatusDropdown: any[];

  //#endregion "VARIABLES"

  ngOnInit() {
    this.getOfficeCodeList();
    this.imageURL = '';
    this.defaultImagePath = 'assets/images/blank-image.png';
    this.getProfessionType();
    this.getQualificationType();
    this.getCountryType();
    this.GetFinancialYearDropdown();
    this.GetLeaveReasonTypeDropdown();
    this.commonService.getEmployeeOfficeId().subscribe(() => {
      this.Flag = 0; // to set tabs 1
      this.tabOnClick(this.tabEventValue);
    });

    this.isEditingAllowed = this.commonService.IsEditingAllowed(
      applicationPages.Employees
    );
    this.initDocumentTypeList();
  }

  constructor(
    private hrService: HrService,
    private accountservice: AccountsService,
    private setting: AppSettingsService,
    private toastr: ToastrService,
    private _DomSanitizer: DomSanitizer,
    public commonService: CommonService,
    private codeservice: CodeService
  ) {
    this.allFormInitialize();
    this.firstTabValue = this.showInfoTabsMain[0].text;
    this.windows = window;
    this.rules = { X: /[02-9]/ };
    this.sexTypes = hrService.getSexTypes();

    this.tabEventValue = 2;
    this.employeeId = 0;

    this.addNewDocument = {
      DocumentName: '',
      DocumentFilePath: '',
      DocumentDate: '',
      DocumentType: ''
    };

    this.docpath = _DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + 'nodoc.pdf'
    );
  }

  allFormInitialize() {
    this.empGeneral = {
      EmployeeName: null,
      EmployeeTypeId: null,
      FatherName: null,
      PermanentAddress: null,
      City: null,
      ProvinceId: null,
      CountryId: null,
      Phone: null,
      Email: null,
      SexId: null,
      DateOfBirth: null,
      Age: null,
      CurrentAddress: null,
      EmployeePhoto: null,
      Resume: null,
      HigherQualificationId: null,
      ExperienceYear: null,
      ProfessionId: null,
      PreviousWork: null,
      ExperienceMonth: null,
      Remarks: null,
      ReferBy: null,
      OfficeId: null,
      TinNumber: null
    };

    this.empDocuments = {
      DocumentName: null,
      DocumentDate: null,
      DocumentFilePath: null,
      EmployeeID: null,
      DocumentType: null,
    };

    this.empHealthInfo = {
      EmployeeID: null,
      BloodGroup: null,
      Allergies: null,
      PastSurgery: null,
      ChronicDisease: null,
      CurrentMedication: null
    };

    this.leaveInfoData = {
      FinancialYearId: null,
      LeaveReasonId: null,
      Unit: null,
      AssignUnit: null,
      Description: null,
      EmployeeId: null
    };

    this.maritalStatusDropdown = [
      {
        MaritalStatusId: 1,
        MaritalStatusName: 'Single'
      },
      {
        MaritalStatusId: 2,
        MaritalStatusName: 'Married'
      },
      {
        MaritalStatusId: 3,
        MaritalStatusName: 'Other'
      }
    ];
  }

  //#region "TABS"
  // tslint:disable-next-line:member-ordering
  showInfoTabsMain: Tab[] = [
    {
      id: 0,
      text: 'General'
    },
    {
      id: 1,
      text: 'Professional'
    },
    {
      id: 2,
      text: 'History'
    },
    {
      id: 3,
      text: 'Leave'
    },
    {
      id: 4,
      text: 'Health'
    },
    {
      id: 5,
      text: 'Attendance'
    },
    {
      id: 6,
      text: 'Payroll'
    },
    {
      id: 7,
      text: 'Pension & Salary Tax'
    },
    {
      id: 8,
      text: 'Analytical'
    },
    {
      id: 9,
      text: 'Contract'
    }
  ];

  //#endregion "TABS"

  //#region DocumentType List
  initDocumentTypeList() {
    this.documentTypeList = [
      {
        id: 1,
        text: 'Aggrements'
      },
      {
        id: 2,
        text: 'Education Documents'
      },
      {
        id: 3,
        text: 'Letters'
      },
      {
        id: 4,
        text: 'Hiring Documents'
      },
      {
        id: 5,
        text: 'Forms'
      },
      {
        id: 6,
        text: 'Employee Information'
      },
    ];
  }
  //#endregion

  // ADDED FOR EMPLOYEE DOCUMNETS
  showDocs() {
    this.popupVisibleDocument = !this.popupVisibleDocument;
  }

  addDocument() {
    this.popupVisibleAddDoc = !this.popupVisibleAddDoc;
  }
  cancelDeleteVoucher() {
    this.popupVisibleAddDoc = false;
  }
  //#endregion

  //#region "Image Update"
  onImageSelectUpdate(event: any) {
    if (this.imageFlag) {
      const file: File = event.value[0];
      const myReader: FileReader = new FileReader();
      myReader.readAsDataURL(file);
      myReader.onloadend = () => {
        this.imageURL = myReader.result;
      };
      this.popupImageUpdateVisible = true;
    } else {
      this.popupImageUpdateVisible = false;
      this.imageFlag = true;
    }
  }
  //#endregion

  //#region "Profile Image Change Service call"
  ChangeEmployeeImage() {
    this.imageFlag = true;

    this.profileImageChangePopupLoading = true;
    const changeEmployeeImage: any = {
      EmployeeId: this.employeeId,
      EmployeeImage: this.imageURL
    };
    this.hrService
      .EditEmployeeImage(
        this.setting.getBaseUrl() + GLOBAL.API_HR_ChangeEmployeeImage,
        changeEmployeeImage
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Image Updated Successfully!');
            this.showData.EmployeePhoto = this.imageURL;

            if (this.tabEventValue === 1) {
              const itemIndex = this.prospectiveEmployeeInfo.findIndex(
                i => i.EmployeeID === this.employeeId
              );
              this.prospectiveEmployeeInfo[
                itemIndex
              ].EmployeePhoto = this.imageURL;
            } else if (this.tabEventValue === 2) {
              const itemIndex = this.activeEmployeeInfo.findIndex(
                i => i.EmployeeID === this.employeeId
              );
              this.activeEmployeeInfo[itemIndex].EmployeePhoto = this.imageURL;
            } else if (this.tabEventValue === 3) {
              const itemIndex = this.terminatedEmployeeInfo.findIndex(
                i => i.EmployeeID === this.employeeId
              );
              this.terminatedEmployeeInfo[
                itemIndex
              ].EmployeePhoto = this.imageURL;
            }
            this.closeImageUpdateForm();
            // tslint:disable-next-line:curly
          } else this.toastr.error('Something went wrong!');
          this.profileImageChangePopupLoading = false;
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
  //#endregion

  //#region "FILE UPLOAD"
  onImageSelect(event: any) {
    const file: File = event.value[0];
    const myReader: FileReader = new FileReader();
    myReader.readAsDataURL(file);
    myReader.onloadend = () => {
      this.imageURL = myReader.result;
    };
  }

  onFileSelect(event: any) {
    const file: File = event.value[0];
    const myReader: FileReader = new FileReader();
    myReader.readAsDataURL(file);
    myReader.onloadend = () => {
      this.fileURL = myReader.result;
    };
  }

  onDocSelect(event: any) {
    const file: File = event.value[0];
    const myReader: FileReader = new FileReader();
    myReader.readAsDataURL(file);
    myReader.onloadend = () => {
      this.docURL = myReader.result;
    };
  }
  //#endregion

  // Add Document with file uploader
  onFormSubmitDocAdd(data: any) {
    this.addNewDocument.DocumentFilePath = this.imageURLDoc;
    data.EmployeeId = this.employeeId;
    this.AddEmployeeDocument(data);
  }

  // Event Fire on image Selection
  onImageSelectDoc(event: any) {
    const file: File = event.value[0];
    const myReader: FileReader = new FileReader();
    myReader.readAsDataURL(file);
    myReader.onloadend = () => {
      this.imageURLDoc = myReader.result;
    };
  }

  // #region "Tab Events"
  tabOnClick(e: number) {
    if (this.Flag !== e) {
      this.loading = true;
      this.openInfoTab = 0;
      this.selectedIndex = 0;
      this.tabEventValue = e;
      this.Flag = e;
      this.employeeId = 0;
      this.GetAllEmployeeDetails(e);
    }
  }
  //#endregion

  //#region "Show / Hide"
  openInfoTabs(e) {
    this.openInfoTab = e.id;
  }

  openAddForm() {
    this.popupAddEmployeeInfoVisible = true;
    this.empGeneral = {};
    this.imageURL = '';
    this.CountryId = 0;
    // tslint:disable-next-line:radix
    this.empGeneral.OfficeId = parseInt(
      localStorage.getItem('EMPLOYEEOFFICEID')
    );
  }

  showEditForm() {
    // TODO: Bind with editGeneralShowData formData and use to revert changes on cancel
    this.editGeneralShowData = {
      EmployeeName: this.showData.EmployeeName,
      FatherName: this.showData.FatherName,
      EmployeePhoto: this.showData.EmployeePhoto,
      SexId: this.showData.SexId,
      DateOfBirth: this.showData.DateOfBirth,
      Email: this.showData.Email,
      CountryId: this.showData.CountryId,
      ProvinceId: this.showData.ProvinceId,
      Phone: this.showData.Phone,
      City: this.showData.City,
      PermanentAddress: this.showData.PermanentAddress,
      CurrentAddress: this.showData.CurrentAddress,
      HigherQualificationId: this.showData.HigherQualificationId,
      ProfessionId: this.showData.ProfessionId,
      ExperienceYear: this.showData.ExperienceYear,
      ExperienceMonth: this.showData.ExperienceMonth,
      PreviousWork: this.showData.PreviousWork,
      ReferBy: this.showData.ReferBy,
      MaritalStatus: this.showData.MaritalStatus,
      University: this.showData.University,
      PassportNo: this.showData.PassportNo,
      IssuePlace: this.showData.IssuePlace,
      BirthPlace: this.showData.BirthPlace,
      TinNumber: this.showData.TinNumber
    };

    this.popupEmployeeInfoVisible = true;

    this.CountryId = this.showData.CountryId;
    this.getStateType(this.CountryId);
    this.ProvinceId = this.showData.ProvinceId;
    this.SexId = this.showData.SexId;
  }

  closeEmployeeForm() {
    this.popupAddEmployeeInfoVisible = false;
  }

  closeForm() {
    this.openInfoTab = 0;
    this.popupEmployeeInfoVisible = false;

    // form init
    this.CountryId = 0;
  }

  closeImageUpdateForm() {
    this.imageFlag = false;
    this.popupImageUpdateVisible = false;
    this.selectedProfileImage = [];
  }

  //#endregion

  //#region "Left side all employee list display"
  activeEmployee() {
    this.showData = this.showActiveEmployeeData;
  }

  prospectiveEmployee() {
    this.showData = this.showProspectiveEmployeeData;
  }

  terminatedEmployee() {
    this.showData = this.showTerminatedEmployeeData;
  }

  displayProspectiveEmpDetails(model) {
    this.loading = true;
    this.selectedItemEmployee = model.EmployeeId;

    this.openInfoTab = 0;
    this.selectedIndex = 0;

    this.showProspectiveEmployeeData = model;
    this.GetEmployeeDetailsByEmployeeId(model.EmployeeID);
    this.employeeId = model.EmployeeID;

    // this.hiredOnDate = model.HiredOn;
    localStorage.setItem('HIREDON', model.HiredOn);
    localStorage.setItem('SelectedEmployee', this.employeeId !== undefined ? this.employeeId.toString() : '');
  }

  displayActiveEmpDetails(model) {
    this.loading = true;
    this.selectedItemEmployee = model.EmployeeId;

    this.openInfoTab = 0;
    this.selectedIndex = 0;

    this.showActiveEmployeeData = model;
    this.GetEmployeeDetailsByEmployeeId(model.EmployeeID);
    this.employeeId = model.EmployeeID;
    localStorage.setItem('SelectedEmployee', this.employeeId !== undefined ? this.employeeId.toString() : '');
    // this.showData =this.employeeListDetail;
  }

  displayTerminatedEmpDetails(model) {
    this.loading = true;
    this.selectedItemEmployee = model.EmployeeId;

    this.openInfoTab = 0;
    this.selectedIndex = 0;

    this.showTerminatedEmployeeData = model;
    this.GetEmployeeDetailsByEmployeeId(model.EmployeeID);
    this.employeeId = model.EmployeeID;
    localStorage.setItem('SelectedEmployee', this.employeeId !== undefined ? this.employeeId.toString() : '');
  }
  //#endregion

  //#region "Get all Office Details"
  getOfficeCodeList() {
    this.codeservice
      .GetAllCodeList(
        this.setting.getBaseUrl() + GLOBAL.API_OfficeCode_GetAllOfficeDetails
      )
      .subscribe(
        data => {
          this.officecodelist = [];
          if (
            data.StatusCode === 200 &&
            data.data.OfficeDetailsList.length > 0
          ) {
            data.data.OfficeDetailsList.forEach(element => {
              this.officecodelist.push({
                Office: element.OfficeId,
                OfficeCode: element.OfficeCode,
                OfficeName: element.OfficeName,
                SupervisorName: element.SupervisorName,
                PhoneNo: element.PhoneNo,
                FaxNo: element.FaxNo,
                OfficeKey: element.OfficeKey
              });
            });

            const AllOffices = localStorage.getItem('ALLOFFICES') != null ? localStorage.getItem('ALLOFFICES').split(',') : [];

            data.data.OfficeDetailsList.forEach(element => {
              const officeFound = AllOffices.indexOf('' + element.OfficeId);
              if (officeFound !== -1) {
                this.officeDropdownList.push({
                  OfficeId: element.OfficeId,
                  OfficeCode: element.OfficeCode,
                  OfficeName: element.OfficeName,
                  SupervisorName: element.SupervisorName,
                  PhoneNo: element.PhoneNo,
                  FaxNo: element.FaxNo,
                  OfficeKey: element.OfficeKey
                });
              }
            });
            if (localStorage.getItem('EMPLOYEEOFFICEID') === null) {
              localStorage.setItem(
                'EMPLOYEEOFFICEID',
                this.officeDropdownList[0].OfficeId.toString()
              );
              this.selectedOffice =
                this.selectedOffice === null
                  ? this.officeDropdownList[0].OfficeId
                  : this.selectedOffice;
            } else {
              this.selectedOffice = parseInt(
                localStorage.getItem('EMPLOYEEOFFICEID')
              );
            }

            this.tabOnClick(2); // default selected value

            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
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
  }
  //#endregion

  //#region "GET ALL PROFESSION"
  getProfessionType() {
    this.hrService
      .GetAllProfession(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllProfession
      )
      .subscribe(
        data => {
          this.professionTypeDropdown = [];
          data != null || data !== undefined
            ? data.data.ProfessionList.forEach(element => {
                this.professionTypeDropdown.push(element);
              })
            : // tslint:disable-next-line:no-unused-expression
              null;
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
  //#endregion "GET ALL PROFESSION"

  //#region "GET ALL QUALIFICATION"
  getQualificationType() {
    this.hrService
      .GetAllQualification(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllQualification
      )
      .subscribe(
        data => {
          this.qualificationTypeDropdown = [];
          data != null || data !== undefined
            ? data.data.QualificationDetailsList.forEach(element => {
                this.qualificationTypeDropdown.push(element);
              })
            : // tslint:disable-next-line:no-unused-expression
              null;
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
  //#endregion "GET ALL QUALIFICATION"

  //#region "GET ALL COUNTRY"
  getCountryType() {
    this.hrService
      .GetAllCountry(this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllCountry)
      .subscribe(
        data => {
          this.countryTypeDropdown = [];
          data != null || data !== undefined
            ? data.data.CountryDetailsList.forEach(element => {
                this.countryTypeDropdown.push(element);
              })
            : // tslint:disable-next-line:no-unused-expression
              null;
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
  //#endregion "GET ALL COUNTRY"

  //#region "GET ALL STATE"
  getStateType(e) {
    // TODO: Set Value For Add Employee
    this.countryId = e;

    this.hrService
      .GetAllProvinceDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllProvinceDetails,
        e
      )
      .subscribe(
        data => {
          this.stateTypeDropdown = [];
          data != null || data !== undefined
            ? data.data.ProvinceDetailsList.forEach(element => {
                this.stateTypeDropdown.push(element);
              })
            : // tslint:disable-next-line:no-unused-expression
              null;
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
  //#endregion "GET ALL STATE"

  //#region "Get Department Type"
  getDepartmentType(eventId: any) {
    this.officeId = eventId;
    this.hrService
      .GetDepartmentDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetDepartmentsByOfficeId,
        eventId
      )
      .subscribe(
        data => {
          this.departmentTypeDropdown = [];
          data.data.Departments.forEach(element => {
            this.departmentTypeDropdown.push(element);
          });
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
  //#endregion

  //#region "GET ALL EMPLOYEES"
  GetAllEmployeeDetails(EmployeeType: number) {
    // this.loading = true;
    this.hrService
      .GetAllEmployees(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_GetAllEmployeeDetail,
        EmployeeType,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(
        data => {
          this.showData = [];
          this.prospectiveEmployeeInfo = [];
          this.activeEmployeeInfo = [];
          this.terminatedEmployeeInfo = [];

          if (
            data.StatusCode === 200 &&
            data.data.EmployeeDetailsList != null &&
            data.data.EmployeeDetailsList.length > 0
          ) {
            data.data.EmployeeDetailsList.forEach(element => {
              this.showData.push({
                EmployeeID: element.EmployeeID,
                EmployeeName: element.EmployeeName,
                Age: element.Age,
                EmployeePhoto:
                  element.DocumentGUID != null && element.DocumentGUID !== ''
                    ? this.setting.getDocUrl() + element.DocumentGUID
                    : null,
                EmployeeTypeId: element.EmployeeTypeId,
                SexName: element.SexName, // Name Changed
                DocumentGUID: element.DocumentGUID,
                EmployeeDOB: element.EmployeeDOB,
                HiredOn: element.HiredOn,
                CurrentAddress: element.CurrentAddress,
                PermanentAddress: element.PermanentAddress,
                City: element.City,
                ProfessionName: element.Profession,
                ProfessionId: element.ProfessionId,
                PreviousWork: element.PreviousWork,
                EmployeeCode: element.EmployeeCode
              });
            });
          }
          if (this.showData != null) {
            if (this.tabEventValue === 1) {
              this.showInfoTabs = this.showInfoTabsMain.filter(
                r => r.id === 0 || r.id === 1
              );
              this.prospectiveEmployeeInfo = this.showData;
            } else if (this.tabEventValue === 2) {
              this.showInfoTabs = this.showInfoTabsMain;
              this.activeEmployeeInfo = this.showData;
            } else if (this.tabEventValue === 3) {
              this.showInfoTabs = this.showInfoTabsMain.filter(
                r =>
                  r.id === 0 ||
                  r.id === 1 ||
                  r.id === 2 ||
                  r.id === 3 ||
                  r.id === 4 ||
                  r.id === 5
              );
              this.terminatedEmployeeInfo = this.showData;
            }
            // Default call For Prospective Employee
            // method call onClick
            if (
              this.showData != null &&
              this.showData !== empty &&
              this.showData.length > 0
            ) {
              if (this.employeeId === 0) {
                this.GetEmployeeDetailsByEmployeeId(
                  this.showData[0].EmployeeID
                );
                this.employeeId = this.showData[0].EmployeeID;
                localStorage.setItem('HIREDON', this.showData[0].HiredOn);
              } else {
                this.GetEmployeeDetailsByEmployeeId(this.employeeId);
              }
            } else {
              this.loading = false;
              this.commonService.setLoader(false);
            }
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.loading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }

          this.loading = false;
        }
      );
  }
  //#endregion

  //#region "Get All Document Details"
  getAllDocumentDetails(employeeId: any) {
    this.hrService
      .GetAllDocumentDetails(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_GetAllDocumentDetails,
        employeeId
      )
      .subscribe(
        data => {
          this.showDocumentData = [];

          if (
            data.data.EmployeeDocumentList != null &&
            data.data.EmployeeDocumentList.length > 0 &&
            data.StatusCode === 200
          ) {
            data.data.EmployeeDocumentList.forEach(element => {
              this.showDocumentData.push(element);
            });
            this.selectedDropdown = this.showDocumentData[0].DocumentGUID;
            this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
              this.setting.getDocUrl() + this.selectedDropdown
            );
          } else {
            this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
              this.setting.getDocUrl() + 'nodoc.pdf'
            );
          }
          this.loading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.loading = false;
        }
      );
  }
  //#endregion

  //#region  "Get On Click All Employee Details"
  GetEmployeeDetailsByEmployeeId(employeeId: number) {
    this.commonService.setLoader(true);

    if (employeeId === 0) {
      this.toastr.warning('No data to display !');
    } else {
      this.hrService
        .GetEmployeesDetailsByEmployeeId(
          this.setting.getBaseUrl() + GLOBAL.API_Hr_GetEmployeeById,
          employeeId
        )
        .subscribe(
          data => {
            if (
              data.StatusCode === 200 &&
              data.data.EmployeeDetailList != null
            ) {
              this.employeeListDetail = [];
              data.data.EmployeeDetailList.forEach(element => {
                this.employeeListDetail.push(element);
              });
              this.showData = this.employeeListDetail[0];
              this.showData.EmployeePhoto =
                this.showData.DocumentGUID != null &&
                this.showData.DocumentGUID !== ''
                  ? this.setting.getDocUrl() + this.showData.DocumentGUID
                  : null;
              this.getAllDocumentDetails(this.employeeId);
              localStorage.setItem(
                'SelectedEmployee',
                this.employeeId.toString()
              );
              localStorage.setItem(
                'SelectedEmployeeName',
                this.showData !== undefined ? this.showData.EmployeeName.toString() : ''
              );
              this.commonService.setLoader(false);
            } else {
              this.commonService.setLoader(false);
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
            this.loading = false;
          }
        );
    }
  }
  //#endregion

  //#region "ADD EMPLOYEE"
  AddEmployeeDetails(value) {
    this.addEmployeePopupLoading = true;

    const generalInfo: GeneralInfo = {
      EmployeeTypeId: 1,
      EmployeePhoto: this.imageURL,
      Resume: this.fileURL,
      CountryId: this.countryId,
      DateOfBirth: new Date(
        new Date(value.DateOfBirth).getFullYear(),
        new Date(value.DateOfBirth).getMonth(),
        new Date(value.DateOfBirth).getDate(),
        new Date().getHours(),
        new Date().getMinutes(),
        new Date().getSeconds()
      ),
      EmployeeName: value.EmployeeName,
      FatherName: value.FatherName,
      SexId: value.SexId,
      Age: value.Age,
      Email: value.Email,
      ProvinceId: value.ProvinceId,
      Phone: value.Phone,
      City: value.City,
      PermanentAddress: value.PermanentAddress,
      CurrentAddress: value.CurrentAddress,
      HigherQualificationId: value.HigherQualificationId,
      ProfessionId: value.ProfessionId,
      ExperienceYear: value.ExperienceYear,
      ExperienceMonth: value.ExperienceMonth,
      PreviousWork: value.PreviousWork,
      ReferBy: value.ReferBy,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
      University: value.University,
      PassportNo: value.PassportNo,
      IssuePlace: value.IssuePlace,
      BirthPlace: value.BirthPlace,
      MaritalStatusId: value.MaritalStatus,
      PlaceOfBirth: value.BirthPlace,
      TinNumber: value.TinNumber
    };

    this.hrService
      .addEmployee(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_AddEmployees,
        generalInfo
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Employee Added Successfully!!!');
            this.GetAllEmployeeDetails(this.tabEventValue);
            this.popupAddEmployeeInfoVisible = false;
            this.employeeId = 0;
            if (data.LoggerDetailsModel != null) {
            }
            this.fireNotification(data.LoggerDetailsModel);
          }
          this.addEmployeePopupLoading = false; // loader on popup
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.allFormInitialize();
          this.GetEmployeeDetailsByEmployeeId(this.employeeId);
          this.addEmployeePopupLoading = false; // loader on popup
        }
      );
  }
  //#endregion "ADD EMPLOYEE"

  //#region "EDIT EMPLOYEE"
  EditEmployeeDetails(value) {
    this.editEmployeePopupLoading = true;

    const editGeneralInfo: any = {
      EmployeeID: this.employeeId,
      CountryId: this.countryId,
      Age: value.Age,
      City: value.City,
      CurrentAddress: value.CurrentAddress,
      DateOfBirth: value.DateOfBirth,
      Email: value.Email,
      EmployeeName: value.EmployeeName,
      EmployeeTypeId: this.showData.EmployeeTypeId,
      ExperienceMonth: value.ExperienceMonth,
      ExperienceYear: value.ExperienceYear,
      FatherName: value.FatherName,
      HigherQualificationId: value.HigherQualificationId,
      PermanentAddress: value.PermanentAddress,
      Phone: value.Phone,
      PreviousWork: value.PreviousWork,
      ProfessionId: value.ProfessionId,
      ProvinceId: value.ProvinceId,
      ReferBy: value.ReferBy,
      Remarks: value.Remarks,
      SexId: value.SexId,
      // tslint:disable-next-line:radix
      OfficeId: parseInt(localStorage.getItem('EMPLOYEEOFFICEID')),
      University: value.University,
      PassportNo: value.PassportNo,
      IssuePlace: value.IssuePlace,
      BirthPlace: value.BirthPlace,
      MaritalStatus: value.MaritalStatus,
      TinNumber: value.TinNumber
    };
    this.hrService
      .EditEmployee(
        this.setting.getBaseUrl() + GLOBAL.API_Hr_EditEmployeeDetail,
        editGeneralInfo
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Employee Updated Successfully!!!');

            this.GetAllEmployeeDetails(this.tabEventValue);
            this.popupEmployeeInfoVisible = false;

            if (data.LoggerDetailsModel != null) {
            }
            this.fireNotification(data.LoggerDetailsModel);
          }
          this.editEmployeePopupLoading = false; // loader on popup
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.editEmployeePopupLoading = false; // loader on popup
        }
      );
  }
  //#endregion "EDIT EMPLOYEE"

  //#region "Add New Employee Document"
  AddEmployeeDocument(data) {
    this.showAddDocPopupLoading();

    this.accountservice
      .AddEmployeeDocument(
        this.setting.getBaseUrl() + GLOBAL.API_HR_EmployeeDocumentAdd,
        data
      )
      .subscribe(
        // tslint:disable-next-line:no-shadowed-variable
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Document Added Successfully!!!');
          }
          this.getAllDocumentDetails(this.employeeId);
          this.hideShowAddDocPopup();
          this.hideAddDocPopupLoading();
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
          this.hideShowAddDocPopup();
          this.hideAddDocPopupLoading();
        }
      );
  }
  //#endregion

  // ADDED FOR EMPLOYEE DOCUMNETS ENDS

  //#region  "Output Binding for Employee List Refresh"
  employeeListRefresh() {
    if (this.tabEventValue === 1) {
      const itemIndex = this.prospectiveEmployeeInfo.findIndex(
        i => i.EmployeeID === this.employeeId
      );
      this.prospectiveEmployeeInfo.splice(itemIndex, 1);

      // Employee list update  (left side pannel)
      this.prospectiveEmployeeInfo = this.prospectiveEmployeeInfo;

      if (
        this.prospectiveEmployeeInfo != null &&
        this.prospectiveEmployeeInfo.length > 0
      ) {
        this.employeeId = this.prospectiveEmployeeInfo[0].EmployeeID;
        this.GetEmployeeDetailsByEmployeeId(this.employeeId);
      } else if (this.prospectiveEmployeeInfo.length <= 0) {
        // TODO: Initialize fields
        this.showData.EmployeePhoto = this.defaultImagePath;
        this.showData.EmployeeName = null;
        this.showData.SexName = null;
      }
    } else if (this.tabEventValue === 2) {
      const itemIndex = this.activeEmployeeInfo.findIndex(
        i => i.EmployeeID === this.employeeId
      );
      this.activeEmployeeInfo.splice(itemIndex, 1);

      // Employee list update (left side pannel)
      this.activeEmployeeInfo = this.activeEmployeeInfo;

      if (
        this.activeEmployeeInfo != null &&
        this.activeEmployeeInfo.length > 0
      ) {
        this.employeeId = this.activeEmployeeInfo[0].EmployeeID;
        this.GetEmployeeDetailsByEmployeeId(this.employeeId);
      } else if (this.activeEmployeeInfo.length <= 0) {
        // TODO: Initialize fields
        this.showData.EmployeePhoto = this.defaultImagePath;
        this.showData.EmployeeName = null;
        this.showData.SexName = null;
      }
    } else if (this.tabEventValue === 3) {
      const itemIndex = this.terminatedEmployeeInfo.findIndex(
        i => i.EmployeeID === this.employeeId
      );
      this.terminatedEmployeeInfo.splice(itemIndex, 1);

      // TODO: Employee list update (left side pannel)
      this.terminatedEmployeeInfo = this.terminatedEmployeeInfo;

      if (
        this.terminatedEmployeeInfo != null &&
        this.terminatedEmployeeInfo.length > 0
      ) {
        this.employeeId = this.terminatedEmployeeInfo[0].EmployeeID;
        this.GetEmployeeDetailsByEmployeeId(this.employeeId);
      } else if (this.terminatedEmployeeInfo.length <= 0) {
        // TODO: Initialize fields
        this.showData.EmployeePhoto = this.defaultImagePath;
        this.showData.EmployeeName = null;
        this.showData.SexName = null;
      }
    }
  }
  //#endregion

  //#region "Age Calculator"
  ageCalculator(EmployeeDOB) {
    // tslint:disable-next-line:curly
    if (EmployeeDOB == null) return 0;
    const ageDifference =
      (Date.now() - new Date(EmployeeDOB).getTime()) / 31557600000;
    const age = Math.floor(ageDifference);
    return Math.abs(age);
  }
  //#endregion

  //#region "show/hide"
  showAddDocPopupLoading() {
    this.addDocPopupLoading = true;
  }

  hideAddDocPopupLoading() {
    this.addDocPopupLoading = false;
  }

  hideShowAddDocPopup() {
    this.popupVisibleAddDoc = !this.popupVisibleAddDoc;
  }
  //#endregion

  showAssignLeaveLoading() {
    this.professionalInfoLoading = true;
  }

  hideAssignLeaveLoading() {
    this.professionalInfoLoading = false;
  }

  selectDoc(e) {
    this.docpath = this._DomSanitizer.bypassSecurityTrustResourceUrl(
      this.setting.getDocUrl() + e.value
    );
  }

  fireNotification(model) {
    model.CreatedDate = new Date();
    model.NotificationPath = './hr/employees';
    this.commonService.sendMessage(model);
  }

  //#region "Get All Holidays & Already applied leave"
  getAllDisableCalendarDate(employeeId) {
    this.hrService
      .GetDataByEmployeeIdAndOfficeId(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllDisableCalanderDate,
        employeeId,
        // tslint:disable-next-line:radix
        parseInt(localStorage.getItem('EMPLOYEEOFFICEID'))
      )
      .subscribe(
        data => {
          this.disabledDates = [];
          if (
            data.StatusCode === 200 &&
            data.data.ApplyLeaveList != null &&
            data.data.ApplyLeaveList.length > 0
          ) {
            data.data.ApplyLeaveList.forEach(element => {
              this.disabledDates.push(
                new Date(
                  new Date(element.Date).getTime() -
                    new Date().getTimezoneOffset() * 60000
                )
              );
            });
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
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
  //#endregion

  //#region "Get All Leave Info"
  GetAllLeaveInfo(employeeId: number) {
    this.leaveInfoLoading = true;
    this.hrService
      .GetAllLeaveInfoById(
        this.setting.getBaseUrl() + GLOBAL.API_HR_GetAllEmployeeAssignLeave,
        employeeId
      )
      .subscribe(
        data => {
          this.leaveInfoDataSource = [];
          if (
            data.data.AssignLeaveToEmployeeList != null &&
            data.data.AssignLeaveToEmployeeList.length > 0
          ) {
            data.data.AssignLeaveToEmployeeList.forEach(element => {
              this.leaveInfoDataSource.push(element);
            });
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');

          this.getAllDisableCalendarDate(this.employeeId);
          this.leaveInfoLoading = false;
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
  //#endregion

  //#region AddLeaveInfo

  AddLeaveInfo(model: any) {
    this.showAssignLeaveLoading();

    model.EmployeeId = this.assignLeaveToEmployee;
    model.LeaveReasonId = this.leaveReasonId;

    this.hrService
      .addLeaveInfo(
        this.setting.getBaseUrl() + GLOBAL.API_HR_AssignLeaveToEmployeeDetail,
        model
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully!!!');
            this.GetAllLeaveInfo(this.employeeId);
            this.hideAssignLeaveLoading();
            this.hideAssignLeavePopUp();
            this.getAllDisableCalendarDate(this.employeeId);
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 900) this.toastr.warning(data.Message);
          this.leaveInfoLoading = false;
          this.hideAssignLeaveLoading();
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          this.hideAssignLeaveLoading();
          this.hideAssignLeavePopUp();
        }
      );
  }

  //#endregion

  //#region "Add Leave Info"
  triggerEmployeeLeavePopUp(model) {
    this.assignLeaveToEmployee = model.employeeid;
    if (model.displayLeavePopUp) {
      this.popupAssignLeaveVisible = true;
    }
  }
  // endregion

  //#region "Add Leave Info"
  onLeaveFormSubmit(model) {
    if (model.Unit < model.AssignUnit || model.AssignUnit <= 0) {
      this.toastr.error('Assign unit is Exceeded');
      this.assignUnitIsValid = false;
    } else {
      this.AddLeaveInfo(model);
    }
  }
  // endregion

  hideAssignLeavePopUp() {
    this.popupAssignLeaveVisible = false;
  }

  //#region "Get All Unit Type"
  getUnitType(e: any) {
    this.leaveReasonId = e.value;
    const days = this.leaveReasonTypeDropdown.filter(
      a => a.LeaveReasonId === e.value
    );
    this.leaveInfoData.Unit = days[0].Unit;
    this.leaveInfoData.AssignUnit = days[0].Unit;
  }
  //#endregion

  //#region "Get All Financial Year"
  GetFinancialYearDropdown() {
    this.hrService
      .GetAllDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetCurrentFinancialYear
      )
      .subscribe(
        data => {
          this.financialYearDropdown = [];

          if (
            data.data.CurrentFinancialYearList != null &&
            data.data.CurrentFinancialYearList.length > 0
          ) {
            data.data.CurrentFinancialYearList.forEach(element => {
              this.financialYearDropdown.push({
                StartDate: new Date(
                  new Date(element.StartDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                EndDate: new Date(
                  new Date(element.EndDate).getTime() -
                    new Date().getTimezoneOffset() * 60000
                ),
                FinancialYearId: element.FinancialYearId,
                FinancialYearName: element.FinancialYearName
              });
            });
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
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
  //#endregion

  //#region "Get All Leave Reason Type"
  GetLeaveReasonTypeDropdown() {
    this.leaveReasonTypeDropdown = [];
    this.hrService
      .GetAllDropdown(
        this.setting.getBaseUrl() + GLOBAL.API_Code_LeaveReasonType
      )
      .subscribe(
        data => {
          if (
            data.data.LeaveReasonList != null &&
            data.data.LeaveReasonList.length > 0
          ) {
            data.data.LeaveReasonList.forEach(element => {
              this.leaveReasonTypeDropdown.push({
                LeaveReasonId: element.LeaveReasonId,
                ReasonName: element.ReasonName,
                Unit: element.Unit,
                AssignUnit: 0
              });
            });
            // tslint:disable-next-line:curly
          } else if (data.StatusCode === 400)
            this.toastr.error('Something went wrong!');
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
  //#endregion

  saveEmployeeLeave() {
      this.addEmployeePopupLoading = true;

      if (this.selectedLeaveList == undefined) {
          this.toastr.warning('Select leaves to save');
          this.addEmployeePopupLoading = false;
          return;
      }

    this.selectedLeaveList.forEach(
      x => (x.employeeId = this.assignLeaveToEmployee)
    );

    this.hrService
      .AddByModel(
        this.setting.getBaseUrl() +
          GLOBAL.API_EmployeeHR_AddEmployeeLeaveDetails,
        this.selectedLeaveList
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.toastr.success('Added Successfully');

            this.popupAssignLeaveVisible = false;
          }

          if (data.StatusCode === 400) {
            this.toastr.error(data.Message);
          }

          if (data.StatusCode === 900) {
            this.toastr.success(data.Message);
          }
          this.addEmployeePopupLoading = false;
        },
        error => {
          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          }
          // this.commonService.setLoader(false);
          this.addEmployeePopupLoading = false;
        }
      );
  }

  selectionChangedHandler(e) {
    this.selectedLeaveList = e.selectedRowsData;
  }

  //#region "onRowPreparedUpdateSalaryEvent"
  onRowPreparedAssignLeaveEvent(e) {
    if (e.key.unit < e.newData.AssignUnit) {
      this.toastr.error(
        'Assigned leaves can not be greater than alloted leaves'
      );
    }
  }
  //#endregion

  onRowUpdateAssignLeaveEvent(e) {
    if (e.newData.AssignUnit !== '' || e.newData.AssignUnit !== undefined) {
      // tslint:disable-next-line:radix
      if (e.key.Unit < parseInt(e.newData.AssignUnit)) {
        this.toastr.error(
          'Assigned leaves can not be greater than allotted leaves'
        );
        e.newData.AssignUnit = '';
      }
    }
  }

  //#region "on office Selected"
  onOfficeSelected(id) {
    this.selectedOffice = id;

    // this.loading = true;
    // this.commonServices.setLoader(true);
    this.commonService.setEmployeeOfficeId(id);
    this.commonService.getEmployeeOfficeId();
  }
  //#endregion

  onFieldDataChanged(e) {
    if (e.dataField === 'Phone') {
      if (e.value !== undefined) {
        const phone =  e.value.toString();
        if (phone.length > 14 || phone.length < 10) {
          this.toastr.warning('Phone Number should be between 10-14 digits!!!');
        }
      }
    }
  }

  functionCache = {};
  validateRange(min, max) {
    if (!this.functionCache[`min${min}max${max}`])
      this.functionCache[`min${min}max${max}`] = (options: any) => {
        return options.value >= min && options.value <= max;
      };
    return this.functionCache[`min${min}max${max}`];
  }
}

export interface HealthModel {
  HealthInfoId?: number;
  EmployeeId?: number;
  BloodGroup?: string;
  MedicalHistory?: string;
  SmokeAndDrink?: boolean;
  Insurance?: boolean;
  MedicalInsurance?: string;
  MeasureDieases?: boolean;
  AllergicSubstance?: boolean;
  FamilyHistory?: boolean;
}

export interface RadioSource {
  TextValue?: string;
  FlagValue?: boolean;
}

export class GeneralShowData {
  EmployeeName: string;
  FatherName: string;
  EmployeePhoto: string;
  SexId: number;
  DateOfBirth: Date;
  Email: string;
  CountryId: number;
  ProvinceId: number;
  Phone: string;
  City: string;
  PermanentAddress: string;
  CurrentAddress: string;
  HigherQualificationId: number;
  ProfessionId: number;
  ExperienceYear: number;
  ExperienceMonth: number;
  PreviousWork: string;
  ReferBy: string;

  PassportNo: string;
  University: string;
  BirthPlace: string;
  IssuePlace: string;
  MaritalStatus: number;
  TinNumber: string;
}

export class LeaveInfoData {
  FinancialYearId: number;
  LeaveReasonId: number;
  Unit: number;
  AssignUnit: number;
  Description: string;
  EmployeeId: number;
}

export class LeaveInfoModel {
  LeaveId?: number;
  LeaveReasonName: string;
  Unit: number;
  AssignUnit: number;
  BlanceLeave: number;
}
