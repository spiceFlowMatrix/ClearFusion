// import { Component, OnInit, ViewEncapsulation, Pipe } from '@angular/core';
// import { GLOBAL } from '../../../../../shared/global';
// import { HrService } from '../../../hr.service';
// import { CodeService } from '../../../../code/code.service';
// import { AppSettingsService } from '../../../../../Services/App-settings.Service';
// import { ToastrService } from 'ngx-toastr';
// import { commonService } from '../../../../../Services/common.service';
// import { ProjectsService } from '../../../../pmu/projects/projects.service';
// import { TranslateService } from '@ngx-translate/core';
// import { DomSanitizer } from '@angular/platform-browser';

// declare let jsPDF;
// declare var $: any;

// @Component({
//   selector: 'app-employee-contract',
//   templateUrl: './employee-contract.component.html',
//   styleUrls: ['./employee-contract.component.css'],
//   encapsulation: ViewEncapsulation.None
// })
// @Pipe({name: 'safeHtml'})
// export class EmployeeContractComponent implements OnInit {

//   //#region "variables"

//   defaultImagePath = "assets/images/blank-image.png";

//   gradeDropDown = [
//     {
//       GradeId: 1,
//       GradeName: "Grade - 1"
//     },
//     {
//       GradeId: 2,
//       GradeName: "Grade - 2"
//     },
//     {
//       GradeId: 3,
//       GradeName: "Grade - 3"
//     },
//     {
//       GradeId: 4,
//       GradeName: "Grade - 4"
//     },
//     {
//       GradeId: 5,
//       GradeName: "Grade - 5"
//     },
//     {
//       GradeId: 6,
//       GradeName: "Grade - 6"
//     },
//     {
//       GradeId: 7,
//       GradeName: "Grade - 7"
//     },
//     {
//       GradeId: 8,
//       GradeName: "Grade - 8"
//     },
//     {
//       GradeId: 9,
//       GradeName: "Grade - 9"
//     },
//     {
//       GradeId: 10,
//       GradeName: "Grade - 10"
//     },
//     {
//       GradeId: 11,
//       GradeName: "Grade - 11"
//     },
//     {
//       GradeId: 12,
//       GradeName: "Grade - 12"
//     },
//   ];

//   employeeListDataSource: any[];
//   projectDataSource: any[];
//   projectBudgetLineDataSource: any[];
//   officeListDataSource: any[];
//   countryDropdown: any[];
//   stateDropdown: any[];
//   designationDropdown: any[];
//   selectedEmployeeDetails: any;

//   employeeContractForm: EmployeeContractModel;

//   contractDetails: EmployeeContractDetails;

//   //Flag
//   contractEmployeePopup = false;

//   //loader
//   employeeContractLoading = false;

//   //#endregion

//   constructor(private hrService: HrService, private codeService: CodeService, private translate: TranslateService, private setting: AppSettingsService, private toastr: ToastrService, private commonService: commonService, private _DomSanitizer: DomSanitizer) {
//     translate.setDefaultLang('fa');

//   }

//   ngOnInit() {

//     this.initializeForm();

//     this.getAllEmployeeListByOfficeId();
//     this.getAllProjectDetails();
//     this.getAllOfficeList();
//     this.getAllCountry();
//     this.GetAllDesignationDropdown();
//   }

//   //#region "initializeForm"
//   initializeForm() {

//     var employeeIdForDetail = parseInt(localStorage.getItem("SelectedEmployee"));
//     this.getEmployeeDetailByEmployeeId(employeeIdForDetail);

//     this.employeeContractForm = {
//       EmployeeId: employeeIdForDetail,
//       FatherName: null,
//       EmployeeCode: null,
//       Designation: null,
//       ContractStartDate: null,
//       ContractEndDate: null,
//       DurationOfContract: 0,
//       Salary: 0,
//       Grade: null,
//       Project: null,
//       DutyStation: null,
//       Country: null,
//       Province: null,
//       BudgetLine: null,
//       Job: null,
//       WorkTime: 0,
//       WorkDayHours: 0
//     };

//     this.contractDetails = {
//       EmployeeName: null,
//       FatherName: null,
//       EmployeeCode: null,
//       Designation: null,
//       ContractStartDate: null,
//       ContractEndDate: null,
//       DurationOfContract: null,
//       Salary: null,
//       Grade: null,
//       ProjectName: null,
//       ProjectCode: null,
//       DutyStation: null,
//       Province: null,
//       BudgetLine: null,
//       Job: null,
//       WorkTime: null,
//       WorkDayHours: null,
//       ContentEnglish: null,
//       ContentDari: null,
//       EmployeeImage: null
//     }

//   }
//   //#endregion

//   englishContent:any;
//   //#region "getAllEmployeeListByOfficeId"
//   getSelectedEmployeeContractByEmployeeId() {

//     this.showEmployeeContractLoading();

//     var selectedEmployee = parseInt(localStorage.getItem('SelectedEmployee'));
//     this.hrService.GetAllDetailByGenericParameter(this.setting.getBaseUrl() + GLOBAL.API_Hr_GetSelectedEmployeeContractByEmployeeId, "EmployeeId", selectedEmployee).subscribe(
//       data => {
//         // this.employeeListDataSource = [];
//         if (data.StatusCode == 200 && data.data.EmployeeContractDetails != null) {
//           this.englishContent = this._DomSanitizer.bypassSecurityTrustHtml(data.data.EmployeeContractDetails.ContentEnglish)["changingThisBreaksApplicationSecurity"];
//           this.contractDetails = data.data.EmployeeContractDetails;
//           this.contractDetails.EmployeeImage = data.data.EmployeeContractDetails.EmployeeImage != "" ? this.setting.getDocUrl() + data.data.EmployeeContractDetails.EmployeeImage : this.defaultImagePath;
//           this.showContractEmployeePopup();
//         }
//         else {
//           this.toastr.warning(data.Message);
//         }
//         this.hideEmployeeContractLoading();
//       },
//       error => {
//       });
//   }
//   //#endregion

//   //#region "getAllEmployeeListByOfficeId"
//   getAllEmployeeListByOfficeId() {
//     var officeId = parseInt(localStorage.getItem('OFFICEID'));
//     this.hrService.GetAllDetail(this.setting.getBaseUrl() + GLOBAL.API_Code_GetEmployeeDetailByOfficeId, officeId).subscribe(
//       data => {
//         this.employeeListDataSource = [];
//         if (data.StatusCode == 200 && data.data.EmployeeDetailListData != null && data.data.EmployeeDetailListData.length > 0) {
//           data.data.EmployeeDetailListData.forEach(element => {
//             this.employeeListDataSource.push(element);
//           });
//         }

//         this.employeeListDataSource = this.commonService.sortDropdown(this.employeeListDataSource, "CodeEmployeeName");

//       },
//       error => {
//       });
//   }
//   //#endregion

//   //#region "getAllProjectDetails"
//   getAllProjectDetails() {
//     this.codeService.GetAllCodeList(this.setting.getBaseUrl() + GLOBAL.API_ProjectBudget_GetAllProjectDetail).subscribe(
//       data => {
//         this.projectDataSource = [];
//         if (data.data.ProjectDetailList != null && data.data.ProjectDetailList.length > 0 && data.StatusCode == 200) {
//           data.data.ProjectDetailList.forEach(element => {
//             this.projectDataSource.push(element);
//           });
//         }
//       },
//       error => {

//       });
//   }
//   //#endregion

//   //#region "getAllOfficeList"
//   getAllOfficeList() {
//     this.codeService.GetAllCodeList(this.setting.getBaseUrl() + GLOBAL.API_AllOffice_URL).subscribe(
//       data => {
//         this.officeListDataSource = [];

//         if (data.data.OfficeDetailsList != null && data.StatusCode == 200) {
//           if (data.data.OfficeDetailsList.length > 0) {

//             data.data.OfficeDetailsList.forEach(element => {
//               this.officeListDataSource.push({
//                 OfficeId: element.OfficeId,
//                 OfficeName: element.OfficeName
//               });
//             })

//             this.officeListDataSource = this.commonService.sortDropdown(this.officeListDataSource, "OfficeName");

//           }
//         }

//       },
//       error => {

//       });
//   }
//   //#endregion

//   //#region "getAllCountry"
//   getAllCountry() {
//     this.hrService.GetAllCountry(this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllCountry).subscribe(
//       data => {
//         this.countryDropdown = [];

//         if (data.data.CountryDetailsList != null && data.StatusCode == 200) {
//           if (data.data.CountryDetailsList.length > 0) {

//             data.data.CountryDetailsList.forEach(element => {
//               this.countryDropdown.push(element);
//             })

//             this.countryDropdown = this.commonService.sortDropdown(this.countryDropdown, "CountryName");

//           }
//         }

//       },
//       error => {

//       }
//     )
//   }
//   //#endregion

//   //#region "getAllState"
//   getAllState(e) {

//     this.showEmployeeContractLoading();

//     this.hrService.GetAllProvinceDetails(this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllProvinceDetails, e).subscribe(
//       data => {
//         this.stateDropdown = [];

//         if (data.data.ProvinceDetailsList != null && data.StatusCode == 200) {
//           if (data.data.ProvinceDetailsList.length > 0) {

//             data.data.ProvinceDetailsList.forEach(element => {
//               this.stateDropdown.push(element);
//             });

//             this.stateDropdown = this.commonService.sortDropdown(this.stateDropdown, "ProvinceName");

//           }
//         }
//         this.hideEmployeeContractLoading();

//       },
//       error => {

//       });
//   }
//   //#endregion

//   //#region "Get All Employee Details By EmployeeId"
//   getEmployeeDetailByEmployeeId(employeeId: number) {

//     this.showEmployeeContractLoading();

//     this.hrService.GetEmployeeDataByEmployeeId(this.setting.getBaseUrl() + GLOBAL.API_Code_GetEmployeeDetailByEmployeeId, employeeId).subscribe(
//       data => {
//         this.selectedEmployeeDetails = [];
//         if (data.StatusCode == 200 && data.data.EmployeeDetailListData != null && data.data.EmployeeDetailListData.length > 0) {
//           data.data.EmployeeDetailListData.forEach(element => {
//             this.selectedEmployeeDetails.push(element);
//           });
//           this.employeeContractForm.FatherName = this.selectedEmployeeDetails[0].FathersName;
//           this.employeeContractForm.EmployeeCode = this.selectedEmployeeDetails[0].EmployeeCode;
//           this.employeeContractForm.ContractEndDate = this.selectedEmployeeDetails[0].ContractEndDate;
//           this.employeeContractForm.ContractStartDate = this.selectedEmployeeDetails[0].ContractStartDate;
//           this.employeeContractForm.DutyStation = this.selectedEmployeeDetails[0].DutyStation;
//           this.employeeContractForm.OfficeId = this.selectedEmployeeDetails[0].officeId;
//         }

//         this.hideEmployeeContractLoading();

//       },
//       error => {

//       });
//   }
//   //#endregion

//   //#region "Get Designation Type"
//   GetAllDesignationDropdown() {
//     this.hrService.GetAllDropdown(this.setting.getBaseUrl() + GLOBAL.API_Code_GetAllDesignation).subscribe(
//       data => {
//         this.designationDropdown = [];
//         if (data.data.DesignationList != null) {
//           if (data.data.DesignationList.length > 0) {
//             data.data.DesignationList.forEach(element => {
//               this.designationDropdown.push(element);
//             });
//           }
//         }
//         else
//           this.toastr.error(data.Message);
//       },
//       error => {

//       }
//     );
//   }
//   //#endregion

//   //#region "Get All Budget Lines Details"
//   getAllBudgetLineDetails(ProjectId: any) {
//     this.showEmployeeContractLoading();
//     this.codeService.GetAllBudgetLineReceivable(this.setting.getBaseUrl() + GLOBAL.API_PMU_GetAllBudgetLineDetails, ProjectId).subscribe(
//       data => {
//         this.projectBudgetLineDataSource = [];
//         if (data.data.ProjectBudgetLineList != null && data.StatusCode == 200) {
//           data.data.ProjectBudgetLineList.forEach(element => {
//             this.projectBudgetLineDataSource.push(element);
//           });
//         }
//         this.hideEmployeeContractLoading();
//       },
//       error => {

//       });
//   }
//   //#endregion

//   //#region "AddEmployeeContract"
//   AddEmployeeContract(data: any) {

//     this.showEmployeeContractLoading();

//     this.hrService.addProfessionalInfo(this.setting.getBaseUrl() + GLOBAL.API_Hr_AddEmployeeContractDetails, data).subscribe(
//       data => {
//         if (data.StatusCode == 200)
//           this.toastr.success("Contract Updated Successfully !");
//         else {
//           this.toastr.warning(data.Message);
//         }
//         this.hideEmployeeContractLoading();
//       },
//       error => {

//       });
//   }
//   //#endregion

//   //#region "onemployeeContractFormSubmit"
//   onEmployeeContractFormSubmit(data: any) {

//     var dataModel: EmployeeContractModel = {
//       EmployeeId: data.EmployeeId,
//       FatherName: data.FatherName,
//       EmployeeCode: data.EmployeeCode,
//       Designation: data.Designation,
//       ContractStartDate: data.ContractStartDate != null ? new Date(new Date(data.ContractStartDate).getFullYear(), new Date(data.ContractStartDate).getMonth(), new Date(data.ContractStartDate).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()) : null,
//       ContractEndDate: data.ContractEndDate != null ? new Date(new Date(data.ContractEndDate).getFullYear(), new Date(data.ContractEndDate).getMonth(), new Date(data.ContractEndDate).getDate(), new Date().getHours(), new Date().getMinutes(), new Date().getSeconds()) : null,
//       DurationOfContract: data.DurationOfContract,
//       Salary: data.Salary,
//       Grade: data.Grade,
//       Project: data.Project,
//       DutyStation: data.DutyStation,

//       Country: data.Country,

//       Province: data.Province,
//       BudgetLine: data.BudgetLine,
//       Job: data.Job,
//       WorkTime: data.WorkTime,
//       WorkDayHours: data.WorkDayHours
//     }

//     this.AddEmployeeContract(dataModel);

//   }
//   //#endregion

//   //#region "onViewEmployeeContract"
//   onViewEmployeeContract() {
//     // this.contractDetails = {
//     //   EmployeeName: this.employeeListDataSource.filter(x => x.EmployeeId == this.employeeContractForm.EmployeeId)[0].EmployeeName,
//     //   FatherName: this.employeeContractForm.FatherName,
//     //   EmployeeCode: this.employeeContractForm.EmployeeCode,
//     //   Designation: this.employeeContractForm.Designation != null ? this.designationDropdown.filter(x => x.DesignationId == this.employeeContractForm.Designation)[0].Designation : null,
//     //   ContractStartDate: this.employeeContractForm.ContractStartDate,
//     //   ContractEndDate: this.employeeContractForm.ContractEndDate,
//     //   DurationOfContract: this.employeeContractForm.DurationOfContract,
//     //   Salary: this.employeeContractForm.Salary,
//     //   Grade: this.employeeContractForm.Grade != null ? this.gradeDropDown.filter(x => x.GradeId == this.employeeContractForm.Grade)[0].GradeName : null,
//     //   ProjectName: this.employeeContractForm.Project != null ? this.projectDataSource.filter(x => x.ProjectId == this.employeeContractForm.Project)[0].ProjectName : null,
//     //   ProjectCode: this.employeeContractForm.Project,
//     //   DutyStation: this.employeeContractForm.DutyStation != null ? this.officeListDataSource.filter(x => x.OfficeId == this.employeeContractForm.DutyStation)[0].OfficeName : null,
//     //   Province: this.employeeContractForm.Province != null ? this.stateDropdown.filter(x => x.ProvinceId == this.employeeContractForm.Province)[0].ProvinceName : null,
//     //   BudgetLine: this.employeeContractForm.BudgetLine != null ? this.projectBudgetLineDataSource.filter(x => x.BudgetLineId == this.employeeContractForm.BudgetLine)[0].Description : null,
//     //   Job: this.employeeContractForm.Job,
//     //   WorkTime: this.employeeContractForm.WorkTime,
//     //   WorkDayHours: this.employeeContractForm.WorkDayHours,
//     //   ContentEnglish: "this.employeeContractForm.ContentEnglish",
//     //   ContentDari: "this.employeeContractForm.ContentDari",
//     //   EmployeeImage: this.defaultImagePath,
//     // }

//     this.getSelectedEmployeeContractByEmployeeId();
//   }
//   //#endregion

//   //#region "onFieldDataChanged"
//   onFieldDataChanged(e) {
//     if (e != null) {
//       if (e.dataField == "Project" && e.value != null) {
//         if (e.value != null && e.value != 0) {
//           this.projectBudgetLineDataSource = [];
//           this.getAllBudgetLineDetails(e.value);
//         }
//       }
//       if (e.dataField == "Country" && e.value != null) {
//         if (e.value != null && e.value != 0) {
//           this.stateDropdown = [];
//           this.getAllState(e.value);
//         }
//       }

//       if (e.dataField == "EmployeeId" && e.value != null) {
//         if (e.value != null && e.value != 0) {
//           this.selectedEmployeeDetails = [];
//           this.getEmployeeDetailByEmployeeId(e.value);
//         }
//       }

//     }

//   }
//   //#endregion

//   //#region "generatePdf"
//   base64Img = null;
//   margins = {
//     top: 70,
//     bottom: 40,
//     left: 30,
//     width: 550
//   };
//   generatePdf() {

//     var pdf = new jsPDF('p', 'pt', 'legal'),
//       pdfConf = {
//         // pagesplit: true,
//         background: '#fff'
//       };

//     pdf.addHTML($("#contractReportPdf"), 0, 15, pdfConf, function () {
//       pdf.save('Employee-Contract.pdf');
//     });

//     // var pdf = new jsPDF('p', 'pt', 'a4');
//     // pdf.setFontSize(18);
//     // pdf.fromHTML(document.getElementById('contractReportPdf'),
//     //   this.margins.left, // x coord
//     //   this.margins.top,
//     //   {
//     //     // y coord
//     //     width: this.margins.width// max width of content on PDF
//     //   }, function (dispose) {
//     //     this.headerFooterFormatting(pdf, pdf.internal.getNumberOfPages());
//     //   },
//     //   this.margins);

//     // pdf.save('abc');

//   }
//   //#endregion

//   //#region "show/hide"
//   showContractEmployeePopup() {
//     this.contractEmployeePopup = true;
//   }
//   hideContractEmployeePopup() {
//     this.contractEmployeePopup = false;
//   }
//   //#endregion

//   //#region "Loader"
//   showEmployeeContractLoading() {
//     this.employeeContractLoading = true;
//   }
//   hideEmployeeContractLoading() {
//     this.employeeContractLoading = false;
//   }

// }

// class EmployeeContractModel {
//   EmployeeId: any;
//   FatherName: any;
//   EmployeeCode: any;
//   Designation: any;
//   ContractStartDate: any;
//   ContractEndDate: any;
//   DurationOfContract: any;
//   Salary: any;
//   Grade: any;
//   Project: any;
//   DutyStation: any;

//   Country: any;

//   Province: any;
//   BudgetLine: any;
//   Job: any;
//   WorkTime: any;
//   WorkDayHours: any;
//   OfficeId?:number;
// }

// class EmployeeContractDetails {
//   EmployeeName: any;
//   FatherName: any;
//   EmployeeCode: any;
//   Designation: any;
//   ContractStartDate: any;
//   ContractEndDate: any;
//   DurationOfContract: any;
//   Salary: any;
//   Grade: any;
//   ProjectName: any;
//   ProjectCode: any;
//   DutyStation: any;
//   Province: any;
//   BudgetLine: any;
//   Job: any;
//   WorkTime: any;
//   WorkDayHours: any;
//   ContentEnglish: any;
//   ContentDari: any;
//   EmployeeImage: any;
// }
