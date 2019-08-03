import {
  ProjectProgramModel,
  ProjectAreaModel,
  ProjectSectorModel,
  ProjectOtherDetailModel,
  OfficeModel,
  SecurityModel,
  strengthModel,
  GenderConsiderationvalueModel,
  DonorModel,
  CurrencyModel,
  securityConsiderationMultiSelectModel,
  ProvinceMultiSelectModel,
  DistrictMultiSelectModel,
  CountryMultiSelectModel,
  IProjectOtherDetailPdf
} from './../models/project-details.model';
import { Validators } from '@angular/forms';
import { Component, OnInit, Inject, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { AppUrlService } from 'src/app/shared/services/app-url.service';
import { ProjectListService } from '../../service/project-list.service';
import { GLOBAL } from 'src/app/shared/global';
import {
  SectorModel,
  ProgramModel,
  AreaModel
} from '../models/project-details.model';
import { FormControl } from '@angular/forms';
import { SelectItem } from 'primeng/primeng';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { startWith, map } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { IDataSource } from 'projects/library/src/lib/components/search-dropdown/search-dropdown.model';
import { Observable } from 'rxjs/internal/Observable';
import { ApplicationPages } from 'src/app/shared/applicationpagesenum';
import { LocalStorageService } from 'src/app/shared/services/localstorage.service';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { ProjectOtherDetailPdfService } from './project-other-detail-pdf.service';

@Component({
  selector: 'app-program-area-sector',
  templateUrl: './program-area-sector.component.html',
  styleUrls: ['./program-area-sector.component.scss']
})
export class ProgramAreaSectorComponent implements OnInit {
  //#region variables

  projectOtherDetailPdf: IProjectOtherDetailPdf = {
    // Opportunity Details
    ProjectName: '',
    Description: '',
    OpportunityType: '',
    Donor: '',
    OpportunityNo: '',
    Opportunity: '',
    OpportunityDescription: '',
    Country: '',
    Province: '',
    District: '',
    Office: '',
    Sector: '',
    Program: '',
    StartDate: '',
    EndDate: '',

    // Project Objective & Goal
    ProjectGoal: '',
    ProjectObjective: '',
    MainActivities: '',
    REOIReceiveDate: '',
    SubmissionDate: '',

    // Beneficiary Details
    DirectbeneficiarMale: '',
    InDirectbeneficiarMale: '',
    DirectbeneficiarFemale: '',
    InDirectbeneficiarFemale: '',
    TotalDirectBeneficiary: '',
    TotalInDirectBeneficiary: '',

    // Gender Consideration
    StrengthConsideration: '',
    GenderConsideration: '',
    GenderRemarks: '',

    // Security Consideration
    Security: '',
    SecurityConsideration: '',
    SecurityRemarks: ''
  };


  opportunityNo = new FormControl('', [
    Validators.required,
    // Validators.pattern(this.unamePattern)
  ]);
  opportunity = new FormControl('', [Validators.required]);
  opportunitydescription = new FormControl('', [Validators.required]);
  budget = new FormControl('', [Validators.required]);
  beneficiaryMale = new FormControl('', [Validators.required]);
  beneficiaryFemale = new FormControl('', [Validators.required]);
  projectGoal = new FormControl('', [Validators.required]);
  projectObjective = new FormControl('', [Validators.required]);
  mainActivities = new FormControl('', [Validators.required]);
  GenderRemarks = new FormControl('', [Validators.required]);
  SecurityRemarks = new FormControl('', [Validators.required]);
  InDirectBeneficiaryFemale = new FormControl('', [Validators.required]);
  InDirectBeneficiaryMale = new FormControl('', [Validators.required]);
  myControl = new FormControl();
  filteredOptions: Observable<ProgramModel[]>;

  myControlArea = new FormControl();
  filteredOptionArea: Observable<AreaModel[]>;

  myControlSector = new FormControl();
  filterdOptionSector: Observable<SectorModel[]>;

  // searching data sources
  donorDataSource: IDataSource[];
  officeDataSource: IDataSource[];
  strengthDataSource: IDataSource[];
  genderConsiderationDataSource: IDataSource[];
  securityDataSource: IDataSource[];
  securityConsDataSource: IDataSource[];
  //#endregion

  StartDate: any;
  EndDate: any;
  REOIReceiveDate: any;
  SubmissionDate: any;
  isEditingAllowed = false;
  pageId = ApplicationPages.ProjectDetails;

  Sectorlist: SectorModel[] = [];
  Programlist: ProgramModel[];
  Arealist: AreaModel[];
  Area: string[];
  Program: string[];
  Sector: string[];
  Programvalue: any[];
  Areavalue: any[];
  Sectorvalue: any[];

  AreaName: string;
  ProgramName: string;
  ProgramId: number;
  AreaId: number;
  SectorId: number;
  ProjectId: number;
  Provincevalue: any[];
  // ProjectProvinceModel:ProjectProvinceModel[];
  ProgramModel: ProgramModel;
  selectedProvince: string[] = [];
  getprovinceList: string[] = [];
  Province: SelectItem[];
  // SecurityConsideration: SelectItem[];
  selectedSecurityConsideration: string[] = [];
  // ProvinceId: any[];
  DistrictMultiSelectList: SelectItem[];
  selectedDistrict: string[] = [];
  projectotherDetail: ProjectOtherDetailModel;
  Office: string[];
  Officevalue: any[];
  Officelist: OfficeModel[];
  SecurityName: string;
  Security: string[];
  Securityvalue: any[];
  Securitylist: SecurityModel[];
  strength: string[];
  strengthvalue: any[];
  strengthlist: strengthModel[];
  GenderConsideration: string[];
  GenderConsiderationvalue: any[];
  GenderConsiderationvaluelist: GenderConsiderationvalueModel[];
  Donor: string[];
  DonorName: string;
  Donorvalue: any[];
  DonorList: DonorModel[];
  CurrencyList: CurrencyModel[];
  Currency: string[];
  Currencyvalue: any[];
  DonorId: number;
  Strength: string;
  OfficeName: string;
  OtherProjectList: any[];
  GenderConsiderationName: string;

  CountrySelectionList: SelectItem[];
  ProvinceSelectionList: SelectItem[];
  SecurityConsiderationList: SelectItem[];
  securityConsiderationMultiselect: securityConsiderationMultiSelectModel;
  countryMultiSelectModel: CountryMultiSelectModel;
  provinceMultiSelectModel: ProvinceMultiSelectModel;
  districtMultiSelctModel: DistrictMultiSelectModel;
  ProjectProgramModel: ProjectProgramModel;
  projectAreaModel: ProjectAreaModel;
  projectSectorModel: ProjectSectorModel;
  sectorModel: SectorModel;

  //#region flag
  countrySelectedFlag = true;
  countryDistrictFlag = false;
  provinceSelectedFlag = true;
  provinceDistrictFlag = false;
  programListFlag = false;
  areaListFlag = false;
  sectorListFlag = false;
  opportunityFlag = false;
  projectOtherDetailPageFlag = false;
  donorFlag = false;
  districtFlag = false;
  //#endregion

  // opportunity Type List
  OpportunityTypeList = [
    { Id: 1, Name: 'EOI#' },
    { Id: 2, Name: 'Concept#' },
    { Id: 3, Name: 'RFX#' }
  ];

  constructor(
    private localStorageService: LocalStorageService,
    public dialog: MatDialog,
    public projectListService: ProjectListService,
    private appurl: AppUrlService,
    public matAutocomplete: MatAutocompleteModule,
    public toastr: ToastrService,
    public router: Router,
    public commonLoaderService: CommonLoaderService,
    private pDetailPdfService: ProjectOtherDetailPdfService,
    private _cdr: ChangeDetectorRef,


    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {
    this.ProjectId = this.data.id;
    this.donorDataSource = [];
    this.officeDataSource = [];
    this.strengthDataSource = [];
    this.securityDataSource = [];
    this.securityConsDataSource = [];

    this.GetAllProgramList();
    this.GetAllAreaList();
    this.GetAllSectorList();

    this.getAllCountryList();
   // this.getAllProvinceList();
    this.GetAllCurrency();
    this.GetAllStrengthConsiderationDetails();
    this.GetAllSecurityDetails();
    this.GetAllGenderConsiderationDetails();
    this.GetAllDonorList();
    this.GetAllSecurityConsideration();
    this.GetAllOfficeList();
    this.initProjectOtherDetail();
    this.initMultiselctSecurityModel();
    this.GetSecurityConsiderationByProjectId(this.ProjectId);
    this.GetCountryByProjectId(this.ProjectId);
    this.GetProvinceByProjectId(this.ProjectId);
    this.initCountryMultiSelectModel();
    this.initProvinceMultiSelectModel();
    this.initDistrictMultiSelectModel();
    this.GetDistrictByProjectId(this.ProjectId);
    this.isEditingAllowed = this.localStorageService.IsEditingAllowed(
      this.pageId
    );
    this.initProgramModel();
    this.initAreaModel();
    this.initSectorModel();
    this.initProjectSectorModel();
    this.GetOtherProjectDetailById(this.ProjectId);
    this.getProjectAreaById(this.ProjectId);
    this.getProjectProgramById(this.ProjectId);
    this.getProjectSectorById(this.ProjectId);
  }

  // ngOnchanges() {
  //   this.getProjectSectorById(this.ProjectId);
  //   this.getProjectProgramById(this.ProjectId);
  // }
  //#region  initialize model
  initProjectOtherDetail() {
    this.projectotherDetail = {
      ProjectId: this.ProjectId,
      ProjectOtherDetailId: 0,
      opportunityNo: null,
      opportunity: null,
      opportunitydescription: null,
      beneficiaryMale: 0,
      beneficiaryFemale: 0,
      projectGoal: null,
      projectObjective: null,
      mainActivities: null,
      GenderRemarks: null,
      SecurityRemarks: null,
      StrengthConsiderationId: null,
      SecurityId: null,
      GenderConsiderationId: null,
      ProvinceId: null,
      InDirectBeneficiaryFemale: 0,
      InDirectBeneficiaryMale: 0,
      OpportunityType: null,
    };
  }

  initMultiselctSecurityModel() {
    this.securityConsiderationMultiselect = {
      SecurityConsiderationMultiSelectId: null,
      SecurityConsiderationId: null,
      ProjectId: null
    };
  }

  initCountryMultiSelectModel() {
    this.countryMultiSelectModel = {
      CountryMultiSelectId: null,
      ProjectId: null,
      CountryId: null,
      CountrySelectionId: null
    };
  }

  initProvinceMultiSelectModel() {
    this.provinceMultiSelectModel = {
      ProvinceMultiSelectId: null,
      ProjectId: null,
      ProvinceId: null,
      ProvinceSelectionId: null
    };
  }

  initDistrictMultiSelectModel() {
    this.districtMultiSelctModel = {
      DistrictMultiSelectId: null,
      ProjectId: null,
      DistrictID: null,
      DistrictSelectionId: null,
      ProvinceId: null
    };
  }

  initProgramModel() {
    this.ProjectProgramModel = {
      ProgramId: null,
      ProjectId: null,
      ProjectProgramId: null
    };
  }

  initAreaModel() {
    this.projectAreaModel = {
      AreaId: null,
      ProjectId: null,
      ProjectAreaId: null
    };
  }

  initSectorModel() {
    this.projectSectorModel = {
      ProjectId: null,
      SectorId: null,
      ProjectSectorId: null
    };
  }

  initProjectSectorModel() {
    this.sectorModel = {
      SectorId: null,
      SectorName: null,
      SectorCode: null,
      ProjectId: null
    };
  }
  //#endregion

  //#region closeProgramAreaSectorModal
  closeProgramAreaSectorModal() {
    this.dialog.closeAll();
  }
  //#endregion

  //#region Program master page //*********** Program master page ***********//

  //#region GetAllProgramList
  GetAllProgramList() {
    this.programListFlag = true;
    this.Programlist = [];
    this.projectListService
      .GetAllProgramList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProgramList
      )
      .subscribe(
        data => {
          if (data != null) {
            if (data.data.programDetails != null) {
              data.data.programDetails.forEach(element => {
                this.Programlist.push({
                  ProgramId: element.ProgramId,
                  ProgramName: element.ProgramName
                });
              });

              this.filteredOptions = this.myControl.valueChanges.pipe(
                startWith<string | ProgramModel>(''),
                map(ProgramId =>
                  typeof ProgramId === 'string'
                    ? ProgramId
                    : ProgramId.ProgramName
                ),
                map(ProgramName =>
                  ProgramName
                    ? this._filter(ProgramName)
                    : this.Programlist.slice()
                )
              );
            }
            this.programListFlag = false;
          }
        },
        error => {
          this.programListFlag = false;
          this.toastr.error('Something went wrong ! Please try again');
        }
      );
  }
  //#endregion

  //#region getProjectProgramById
  getProjectProgramById(ProjectId: any) {
    const Id = ProjectId;
    this.projectListService
      .GetProjectProgramById(
        this.appurl.getApiUrl() + GLOBAL.API_Project_getProjectProgramById,
        Id
      )
      .subscribe(
        data => {
          if (data != null) {
            if (data.data.projectProgram != null && data.StatusCode === 200) {
              const filtered: any[] = [];
              for (let i = 0; i < this.Programlist.length; i++) {
                if (
                  data.data.projectProgram.ProgramId ===
                  this.Programlist[i].ProgramId
                ) {
                  filtered.push(this.Programlist[i]);
                }
              }
              if (filtered.length > 0) {
                this.Program = filtered[0].ProgramName;
                // return this.Programvalue = this.getProgramSaveValue(filtered[0].ProgramName);
              }
            }
            if (data.StatusCode === 400) {
              this.toastr.error(data.Message);
            }
          }
        },
        error => {
          this.toastr.error('Something went wrong ! Please try again');
        }
      );
  }
  //#endregion

  //#region _filter the Program from ProgramList
  private _filter(name: string): ProgramModel[] {
    const filterValue = name.toLowerCase();
    return this.Programlist.filter(
      option => option.ProgramName.toLowerCase().indexOf(filterValue) === 0
    );
  }
  //#endregion

  //#region  AddeditSelectedProgramvalue
  AddeditSelectedProgramvalue(event: any) {
    if (event != null && event !== undefined) {
      const projectProgramModel: ProjectProgramModel = {
        ProjectId: this.ProjectId,
        ProgramId: event.source.value.ProgramId
      };

      // tslint:disable-next-line:max-line-length
      this.projectListService
        .AddeditSelectProjectProgramvalue(
          this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditProjectProgram,
          projectProgramModel
        )
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
            }
            if (response.StatusCode === 400) {
              this.toastr.error(response.Message);
            }
          },
          error => {
            this.toastr.error('Something went wrong ! Please try again');
          }
        );
    }
  }
  //#endregion

  //#region displaySelectedProgram from Program List selection
  displaySelectedProgram(obj?: ProgramModel): string | undefined {
    return obj ? obj.ProgramName : undefined;
  }
  //#endregion

  //#region AddProgram  add new program on plus icon
  AddProgram(data: any) {
    if (data.value != null) {
      this.programListFlag = true;
      const programModel: ProgramModel = {
        ProgramName: data.value,
        ProjectId: this.ProjectId
      };
      if (
        programModel.ProgramName !== undefined &&
        programModel.ProgramName !== '' &&
        programModel.ProgramName !== ' ' && programModel.ProgramName !== null
      ) {
        this.projectListService
          .AddProgramDetail(
            this.appurl.getApiUrl() + GLOBAL.API_Project_AddProgramDetails,
            programModel
          )
          .subscribe(
            response => {
              if (response.StatusCode === 200) {
                this.programListFlag = false;
                this.GetAllProgramList();
              }
              if (response.StatusCode === 420) {
                this.programListFlag = false;
                this.toastr.warning('Program is allready exists in list');
              }
              if (response.StatusCode === 400) {
                this.toastr.error(response.Message);
                this.programListFlag = false;
              }
              if (response.StatusCode === 501) {
                this.toastr.warning(response.Message);
                this.programListFlag = false;
              }
            },
            error => {
              this.toastr.error('Something went wrong ! Pelese try again');
              this.programListFlag = false;
            }
          );
      } else {
        this.programListFlag = false;
        this.toastr.warning('Please check Program name');
      }
    } else {
      this.toastr.warning('Please add new Program');
      this.programListFlag = false;
    }
  }
  //#endregion

  //#endregion  end Program master page //** **/

  //#region Area master page //***********    Area master page ***********//

  //#region GetAllAreaList
  GetAllAreaList() {
    this.Arealist = [];
    this.areaListFlag = true;
    this.projectListService
      .GetAllAreaList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllAreaList
      )
      .subscribe(
        data => {
          if (data != null) {
            if (data.data.AreaDetail != null) {
              data.data.AreaDetail.forEach(element => {
                this.Arealist.push({
                  AreaId: element.AreaId,
                  AreaName: element.AreaName
                });
              });
              this.filteredOptionArea = this.myControlArea.valueChanges.pipe(
                startWith<string | AreaModel>(''),
                map(AreaId =>
                  typeof AreaId === 'string' ? AreaId : AreaId.AreaName
                ),
                map(AreaName =>
                  AreaName ? this._filterArea(AreaName) : this.Arealist.slice()
                )
              );
              this.areaListFlag = false;
            }
            if (data.StatusCode === 400) {
              this.toastr.error(data.Message);
            }
          }
        },
        error => {
          this.areaListFlag = false;
          this.toastr.error('Somethimg went wrong ! Try again');
        }
      );
  }
  //#endregion

  //#region _filterArea the Area from Arealist
  private _filterArea(name: string): AreaModel[] {
    const filterValue = name.toLowerCase();
    return this.Arealist.filter(
      option => option.AreaName.toLowerCase().indexOf(filterValue) === 0
    );
  }
  //#endregion

  //#region displaySelectedArea from Program List selection
  displaySelectedArea(obj?: AreaModel): string | undefined {
    return obj ? obj.AreaName : undefined;
  }
  //#endregion

  //#region getProjectAreaById
  getProjectAreaById(ProjectId: any) {
    const Id = ProjectId;
    this.projectListService
      .getProjectAreaById(
        this.appurl.getApiUrl() + GLOBAL.API_Project_getProjectAreaById,
        Id
      )
      .subscribe(
        data => {
          if (data != null) {
            if (data.data.projectArea != null && data.StatusCode === 200) {
              const filtered: any[] = [];
              for (let i = 0; i < this.Arealist.length; i++) {
                if (data.data.projectArea.AreaId === this.Arealist[i].AreaId) {
                  filtered.push(this.Arealist[i]);
                }
              }
              if (filtered.length > 0) {
                this.Area = filtered[0].AreaName;
                // return this.Areavalue = this.getAreaSaveValue(filtered[0].AreaName);
              }
            }
          }
          if (data.StatusCode === 400) {
            this.toastr.error(data.Message);
          }
        },
        error => {
          this.toastr.error('Something went wrong ! Please Try again ');
        }
      );
  }
  //#endregion

  //#region AddeditSelectAreaProgramvalue
  AddeditSelectAreaProgramvalue(event: any) {
    if (event != null && event !== undefined) {
      const projectAreaModel: ProjectAreaModel = {
        ProjectId: this.ProjectId,
        AreaId: event.source.value.AreaId
      };
      this.projectListService
        .AddeditSelectAreaProgramvalue(
          this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditProjectArea,
          projectAreaModel
        )
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
            }
            if (response.StatusCode === 400) {
              this.toastr.error(response.Message);
            }
          },
          error => {
            this.toastr.error('Something went wrong ! Please try Again');
          }
        );
    }
  }

  //#endregion

  //#region AddAreaDeatil add new area
  AddAreaDeatil(data: any) {
    const areaModel: AreaModel = {
      AreaName: data.value
    };
    if (areaModel.AreaName !== undefined) {
      this.projectListService
        .AddAreaDetail(
          this.appurl.getApiUrl() + GLOBAL.API_Project_AddAreaDetails,
          areaModel
        )
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
              this.GetAllAreaList();
              this.toastr.success('Area Added Successfully!!!', 'action');
              this.Area = [];
            }
            if (response.StatusCode === 420) {
              this.toastr.warning('Program is allready exists in list');
            }
          },
          error => {
            this.toastr.error('Something went wrong! Please try again');
          }
        );
    }
  }
  //#endregion

  //#endregion end Area master page

  //#region Sector master page//***********   Sector master page ***********//

  //#region  GetAllSectorList
  GetAllSectorList() {
    this.Sectorlist = [];
    this.sectorListFlag = true;
    this.projectListService
      .GetAllSectorList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllSectorList
      )
      .subscribe(
        data => {
          if (data != null) {
            if (data.data.sectorDetails != null) {
              data.data.sectorDetails.forEach(element => {
                this.Sectorlist.push({
                  SectorId: element.SectorId,
                  SectorName: element.SectorName
                });
              });
              this._cdr.detectChanges();
              this.filterdOptionSector = this.myControlSector.valueChanges.pipe(
                startWith<string | SectorModel>(''),
                map(SectorId =>
                  typeof SectorId === 'string' ? SectorId : SectorId.SectorName
                ),
                map(SectorName =>
                  SectorName
                    ? this._filterSector(SectorName)
                    : this.Sectorlist.slice()
                )
              );
            }
            this.sectorListFlag = false;
          }
        },
        error => {
          this.sectorListFlag = false;
          this.toastr.error('Something went wrong ! Please try again');
        }
      );
  }
  //#endregion

  //#region _filter the Program from ProgramList
  private _filterSector(name: string): SectorModel[] {
    const filterValue = name.toLowerCase();
    return this.Sectorlist.filter(
      option => option.SectorName.toLowerCase().indexOf(filterValue) === 0
    );
  }
  //#endregion




  //#region AddSectorDeatil
  AddSectorDeatil(data: any) {
    if (data.value != null) {
      this.sectorListFlag = true;
      const sectorModel: SectorModel = {
        SectorName: data.value,
        ProjectId: this.ProjectId
      };
      if (sectorModel.SectorName !== undefined &&
        sectorModel.SectorName !== '' &&
        sectorModel.SectorName !== ' ' && sectorModel.SectorName !== null) {
        this.projectListService
          .AddSectorDetail(
            this.appurl.getApiUrl() + GLOBAL.API_Project_AddSectorDetails,
            sectorModel
          )
          .subscribe(
            response => {
              if (response.StatusCode === 200) {
                this.sectorListFlag = false;
                this.GetAllSectorList();
                // this.toastr.success('Sector Added Successfully!!!', 'action');
                // duration: 3000,
                // });
                // this.Sector = [];
              }
              if (response.StatusCode === 420) {
                this.toastr.warning('Sector is allready exists in list');
                this.sectorListFlag = false;
              }
              if (response.StatusCode === 400) {
                this.toastr.error(response.Message);
                this.sectorListFlag = false;
              }
              if (response.StatusCode === 501) {
                this.toastr.warning(response.Message);
                this.sectorListFlag = false;
              }
            },
            error => {
              this.toastr.error('Something went wrong ! Please try again');
              this.sectorListFlag = false;
            }
          );
      } else {
        this.sectorListFlag = false;
        this.toastr.warning('Please check sector name');
      }
    } else {
      this.toastr.warning('Please add new sector');
      this.sectorListFlag = false;
    }
  }
  //#endregion

  //#region getProjectSectorById
  getProjectSectorById(projectId: any) {
    const Id = projectId;
    const obj = this.Sectorlist;
    this.projectListService
      .getProjectSectorById(
        this.appurl.getApiUrl() + GLOBAL.API_Project_getProjectSectorById,
        Id
      )
      .subscribe(
        data => {
          if (data != null) {
            if (data.data.projectSector != null && data.StatusCode === 200) {
              const filtered: any[] = [];
              for (let i = 0; i < obj.length; i++) {
                if (
                  data.data.projectSector.SectorId ===
                  obj[i].SectorId
                ) {
                  filtered.push(obj[i]);
                }
              }
              if (filtered.length > 0) {
                this.Sector = filtered[0].SectorName;
               // console.log(this.Sector);
                // return this.Sectorvalue = this.getSectorSaveValue(filtered[0].SectorName);
              }
            }
            if (data.StatusCode === 400) {
              this.toastr.error(data.Message);
            }
          }
        },
        error => {
          this.toastr.error('Something went wrong ! please try again');
        }
      );
  }
  //#endregion

  //#region AddeditSelectSectorvalue when choose from dropdown
  AddeditSelectSectorvalue(event: any) {
    // this.sectorListFlag = true;
    if (event != null && event !== undefined) {
      const projectSectorModel: ProjectSectorModel = {
        ProjectId: this.ProjectId,
        SectorId: event.source.value.SectorId
      };
      this.projectListService
        .AddeditSelectSectorvalue(
          this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditProjectSector,
          projectSectorModel
        )
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
              this._cdr.detectChanges();
              this.sectorListFlag = false;
            }
            if (response.StatusCode === 400) {
              this.toastr.error(response.Message);
              this.sectorListFlag = false;
            }
          },
          error => {
            this.sectorListFlag = false;
            this.toastr.error('Something went wrong ! Please try Again');
          }
        );
    }
  }
  //#endregion

  //#endregion end sector master page

  //#region GetAllProvinceList ProvinceDetailsChange  & GetAllDistrictvalueByProvinceId MULTISELECT

  // GetAllProvinceList

  getAllCountryList() {
    // this.Province = [];
    this.countryDistrictFlag = true;
    this.CountrySelectionList = [];
    this.projectListService
      .getAllCountryList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllCountryDetails
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200 && data != null) {
            if (data.data.CountryDetailsList != null) {
              data.data.CountryDetailsList.forEach(element => {
                this.CountrySelectionList.push({
                  value: element.CountryId,
                  label: element.CountryName
                });
              });
            }
            this.countryDistrictFlag = false;
          }
        },
        error => {
          this.countryDistrictFlag = false;
        }
      );
  }

  GetCountryByProjectId(ProjectId: number) {
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.projectListService
        .GetOtherSecurityConsiByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetCountryByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.CountryMultiSelectById != null) {
              [this.countryMultiSelectModel.CountryId] =
                data.data.CountryMultiSelectById;
              this.getAllProvinceListByCountryId(
                [this.countryMultiSelectModel.CountryId]
              );
            }
          }
        });
    }
  }

   // add province by id

   onCountryDetailsChange(ev, data: number) {
    // this.countryDistrictFlag = true;
    this.ProvinceSelectionList = [];
    if (ev === 'countrySelction' && data != null) {
      this.countryMultiSelectModel.CountryId = data;
      this.AddEditonCountryDetails(this.countryMultiSelectModel);
    }
  }

  AddEditonCountryDetails(model: any) {
    if (model != null) {
      this.countryDistrictFlag = true;

      const obj: any = {
        ProjectId: this.ProjectId,
        CountryId: [model.CountryId]
      };
      this.projectListService
        .AddEditCountryMultiSelect(
          this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddEditMultiSelectCountry,
          obj
        )
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
              this.getAllProvinceListByCountryId(
                [this.countryMultiSelectModel.CountryId]
              );
            }
            this.countryDistrictFlag = false;
            this.provinceSelectedFlag = false;
          },
          error => {
            this.countryDistrictFlag = false;
            this.provinceSelectedFlag = false;
          }
        );
    }
  }

  getAllProvinceListByCountryId(model: any) {
    const id = model;
    console.log(id);
    this.provinceDistrictFlag = true;
    this.ProvinceSelectionList = [];
    this.projectListService
      .getAllProvinceListByCountryId(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllProvinceDetailsByCountryId,
        id
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200 && data != null) {
            if (data.data.ProvinceDetailsList != null) {
              data.data.ProvinceDetailsList.forEach(element => {
                this.ProvinceSelectionList.push({
                  value: element.ProvinceId,
                  label: element.ProvinceName
                });
              });
            }
            this.GetProvinceByProjectId(this.ProjectId);
          }
          this.provinceDistrictFlag = false;
        },
        error => {
          this.provinceDistrictFlag = false;
        }
      );
  }

  //#region displaySelectedSector from sector List selection
  displaySelectedSector(obj?: SectorModel): string | undefined {
    return obj ? obj.SectorName : undefined;
  }
  //#endregion

  GetProvinceByProjectId(ProjectId: number) {
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.projectListService
        .GetOtherSecurityConsiByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetProvinceByProjectId,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.ProvinceMultiSelectById != null) {
              this.provinceMultiSelectModel.ProvinceId =
                data.data.ProvinceMultiSelectById;
              this.GetAllDistrictvalueByProvinceId(
                this.provinceMultiSelectModel.ProvinceId
              );
            }
          }
        });
    }
  }

  // add province by id

  onProvinceDetailsChange(ev, data: number[]) {
    this.provinceDistrictFlag = true;
    this.DistrictMultiSelectList = [];
    if (ev === 'provinceSelction' && data != null) {
      this.provinceMultiSelectModel.ProvinceId = data;
      this.AddEditonProvinceDetails(this.provinceMultiSelectModel);
    }
  }

  AddEditonProvinceDetails(model: any) {
    if (model != null) {
      const obj: ProvinceMultiSelectModel = {
        ProjectId: this.ProjectId,
        ProvinceId: model.ProvinceId
      };
      this.projectListService
        .AddEditProvinceMultiSelect(
          this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddEditMultiSelectProvince,
          obj
        )
        .subscribe(
          response => {
            if (response.StatusCode === 200) {
              this.provinceDistrictFlag = false;
              this.GetAllDistrictvalueByProvinceId(
                this.provinceMultiSelectModel.ProvinceId
              );
            }
            this.provinceSelectedFlag = false;
          },
          error => {
            this.provinceSelectedFlag = false;
          }
        );
    }
  }

  // to get the list of District on select of province id
  GetAllDistrictvalueByProvinceId(model: any) {
    const id = model;
    this.provinceSelectedFlag = true;
    this.DistrictMultiSelectList = [];
    this.projectListService
      .GetAllDistrictvalueByProvinceId(
        this.appurl.getApiUrl() +
        GLOBAL.API_Project_GetAllDistrictvalueByProvinceId,
        id
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            if (data != null) {
              if (data.data.Districtlist != null) {
                data.data.Districtlist.forEach(element => {
                  this.DistrictMultiSelectList.push({
                    value: element.DistrictID,
                    label: element.District
                  });
                });
              }
              this.GetDistrictByProjectId(this.ProjectId);
            }
          }
          this.provinceSelectedFlag = false;
        },
        error => {
          this.provinceSelectedFlag = false;
        }
      );
  }

  // get District byProject id
  GetDistrictByProjectId(ProjectId: number) {
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.provinceSelectedFlag = true;
      this.projectListService
        .GetOtherSecurityConsiByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetDistrictByProjectId,
          ProjectId
        )
        .subscribe(
          data => {
            if (data != null) {
              if (data.StatusCode === 200) {
                if (data.data.DistrictMultiSelectById != null) {
                  this.districtMultiSelctModel.DistrictID =
                    data.data.DistrictMultiSelectById;
                }
              }
            }
            this.provinceSelectedFlag = false;
          },
          error => {
            this.provinceSelectedFlag = false;
          }
        );
    }
  }

  onDistrictDetailsChange(ev, data: number[]) {
    this.districtFlag = true;
    if (ev === 'districtSelction' && data != null) {
      this.districtMultiSelctModel.DistrictID = data;
      this.AddEditDistrictMultiSelect(this.districtMultiSelctModel);
    }
  }

  AddEditDistrictMultiSelect(model: any) {
    if (model != null) {
      const obj: DistrictMultiSelectModel = {
        ProjectId: this.ProjectId,
        DistrictID: model.DistrictID,
        ProvinceId: this.provinceMultiSelectModel.ProvinceId
      };
      this.projectListService
        .AddEditDistrictMultiSelect(
          this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddEditMultiSelectDistrict,
          obj
        )
        .subscribe(response => {
          if (response.StatusCode === 200) {
            this.districtFlag = false;
          }
        },
          error => {
            this.districtFlag = false;
          }
        );
    }
  }

  //#endregion

  //#region  securityConsSelction Multiselect
  GetSecurityConsiderationByProjectId(ProjectId: number) {
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.projectListService
        .GetOtherSecurityConsiByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetSecurityConsiderationById,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            if (data.data.SecurityConsiderationMultiSelectById != null) {
              this.securityConsiderationMultiselect.SecurityConsiderationId =
                data.data.SecurityConsiderationMultiSelectById;
            }
          }
        });
    }
  }

  GetAllSecurityConsideration() {
    this.SecurityConsiderationList = [];
    this.projectListService
      .GetAllSecurityConsideration(
        this.appurl.getApiUrl() +
        GLOBAL.API_Project_GetAllSecurityConsiderationDetails
      )
      .subscribe(data => {
        if (data != null) {
          if (data.data.SecurityConsiderationDetail != null) {
            this.securityConsDataSource = [];
            data.data.SecurityConsiderationDetail.forEach(element => {
              this.SecurityConsiderationList.push({
                value: element.SecurityConsiderationId,
                label: element.SecurityConsiderationName
              });
            });
            data.data.SecurityConsiderationDetail.forEach(element => {
              this.securityConsDataSource.push({
                Id: element.SecurityConsiderationId,
                Name: element.SecurityConsiderationName
              });
            });

            // this.GetOtherProjectDetailById(this.ProjectId);
          }
        }
      });
  }

  onProjectSecurityConsiderationMultiChange(ev, data: number[]) {
    if (ev === 'securityConsSelction' && data != null) {
      this.securityConsiderationMultiselect.SecurityConsiderationId = data;
      // var unique = Array.from(new Set(data))
      this.AddEditSecurityCMultiSelect(this.securityConsiderationMultiselect);
    }
  }

  AddEditSecurityCMultiSelect(model: any) {

    if (model != null) {
      const obj: securityConsiderationMultiSelectModel = {
        // SecurityConsiderationMultiSelectId:model.SecurityConsiderationMultiSelectId,
        ProjectId: this.ProjectId,
        SecurityConsiderationId: model.SecurityConsiderationId
      };
      this.projectListService
        .AddEditSecurityMultiSelect(
          this.appurl.getApiUrl() +
          GLOBAL.API_Project_AddEditMultiSelectSecurityConsideration,
          obj
        )
        .subscribe(response => {
          if (response.StatusCode === 200) {
          }
        });
    }
  }

  //#endregion

  //#region GetAllStrengthConsiderationDetails
  GetAllStrengthConsiderationDetails() {
    this.strengthlist = [];
    this.projectListService
      .GetAllStrengthConsiderationDetails(
        this.appurl.getApiUrl() +
        GLOBAL.API_Project_GetAllStrengthConsiderationDetails
      )
      .subscribe(data => {
        if (data != null) {
          if (data.data.StrengthConsiderationDetail != null) {
            this.strengthDataSource = [];
            data.data.StrengthConsiderationDetail.forEach(element => {
              this.strengthlist.push({
                StrengthConsiderationId: element.StrengthConsiderationId,
                StrengthConsiderationName: element.StrengthConsiderationName
              });
            });
          }
          // bind dataSource to search list
          data.data.StrengthConsiderationDetail.forEach(element => {
            this.strengthDataSource.push({
              Id: element.StrengthConsiderationId,
              Name: element.StrengthConsiderationName
            });
          });
        }
      });
  }
  filterstrengthSingle(event) {
    const query = event.query;
    return (this.strengthvalue = this.filterstrength(query, this.strengthlist));
  }
  filterstrength(query, strengthlist: any[]): any[] {
    // in a real application, make a request to a remote url with the query and return filtered results, for demo we filter at client side
    const filtered: any[] = [];
    for (let i = 0; i < strengthlist.length; i++) {
      const strength = strengthlist[i];
      if (query == null) {
        filtered.push(strength.StrengthConsiderationName);
      } else if (
        strength.StrengthConsiderationName.toLowerCase().indexOf(
          query.toLowerCase()
        ) !== -1
      ) {
        filtered.push(strength.StrengthConsiderationName);
      }
    }
    return filtered;
  }
  //#endregion

  //#region Currency  detail
  GetAllCurrency() {
    this.CurrencyList = [];
    this.projectListService
      .GetAllCurrency(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllCurrency)
      .subscribe(data => {
        if (data != null) {
          if (data.data.CurrencyList != null) {
            data.data.CurrencyList.forEach(element => {
              this.CurrencyList.push({
                CurrencyId: element.CurrencyId,
                CurrencyCode: element.CurrencyCode
              });
            });
          }
        }
      });
  }
  filterCurrencySingle(event) {
    const query = event.query;
    return (this.Currencyvalue = this.filterCurrency(query, this.CurrencyList));
  }
  filterCurrency(query, CurrencyList: any[]): any[] {
    // in a real application, make a request to a remote url with the query and return filtered results, for demo we filter at client side
    const filtered: any[] = [];
    for (let i = 0; i < CurrencyList.length; i++) {
      const Currency = CurrencyList[i];
      if (query == null) {
        filtered.push(Currency.CurrencyCode);
      } else if (
        Currency.CurrencyCode.toLowerCase().indexOf(query.toLowerCase()) !== -1
      ) {
        filtered.push(Currency.CurrencyCode);
      }
    }
    return filtered;
  }
  //#endregion

  //#region GenderConsideration
  GetAllGenderConsiderationDetails() {
    this.GenderConsiderationvaluelist = [];
    this.projectListService
      .GetAllGender(
        this.appurl.getApiUrl() +
        GLOBAL.API_Project_GetAllGenderConsiderationDetails
      )
      .subscribe(data => {
        if (data != null) {
          if (data.data.GenderConsiderationDetail != null) {
            this.genderConsiderationDataSource = [];
            data.data.GenderConsiderationDetail.forEach(element => {
              this.GenderConsiderationvaluelist.push({
                GenderConsiderationId: element.GenderConsiderationId,
                GenderConsiderationName: element.GenderConsiderationName
              });
            });
            data.data.GenderConsiderationDetail.forEach(element => {
              this.genderConsiderationDataSource.push({
                Id: element.GenderConsiderationId,
                Name: element.GenderConsiderationName
              });
            });
            // this.GetOtherProjectDetailById(this.ProjectId);
          }
        }
      });
  }
  filterGenderConsiderationSingle(event) {
    const query = event.query;
    return (this.GenderConsiderationvalue = this.filterGenderConsideration(
      query,
      this.GenderConsiderationvaluelist
    ));
  }
  filterGenderConsideration(query, GenderConsiderationvaluelist: any[]): any[] {
    // in a real application, make a request to a remote url with the query and return filtered results, for demo we filter at client side
    const filtered: any[] = [];
    for (let i = 0; i < GenderConsiderationvaluelist.length; i++) {
      const GenderConsideration = GenderConsiderationvaluelist[i];
      if (query == null) {
        filtered.push(GenderConsideration.GenderConsiderationName);
      } else if (
        GenderConsideration.GenderConsiderationName.toLowerCase().indexOf(
          query.toLowerCase()
        ) !== -1
      ) {
        filtered.push(GenderConsideration.GenderConsiderationName);
      }
    }
    return filtered;
  }
  //#endregion

  //#region Get All Security
  GetAllSecurityDetails() {
    this.Securitylist = [];
    this.projectListService
      .GetAllSecurityDetails(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllSecurityDetails
      )
      .subscribe(data => {
        if (data != null) {
          if (data.data.SecurityDetail != null) {
            this.securityDataSource = [];
            data.data.SecurityDetail.forEach(element => {
              this.Securitylist.push({
                SecurityId: element.SecurityId,
                SecurityName: element.SecurityName
              });
            });
            // bind dataSource to search list
            data.data.SecurityDetail.forEach(element => {
              this.securityDataSource.push({
                Id: element.SecurityId,
                Name: element.SecurityName
              });
            });
          }
        }
      });
  }
  filterSecuritySingle(event) {
    const query = event.query;
    return (this.Securityvalue = this.filterSecurity(query, this.Securitylist));
  }
  filterSecurity(query, Securitylist: any[]): any[] {
    // in a real application, make a request to a remote url with the query and return filtered results, for demo we filter at client side
    const filtered: any[] = [];
    for (let i = 0; i < Securitylist.length; i++) {
      const Security = Securitylist[i];
      if (query == null) {
        filtered.push(Security.SecurityName);
      } else if (
        Security.SecurityName.toLowerCase().indexOf(query.toLowerCase()) !== -1
      ) {
        filtered.push(Security.SecurityName);
      }
    }
    return filtered;
  }
  //#endregion

  //#region "openedChange" for project donor detail change
  openedChange(event: any, data: any) {
    this.projectotherDetail.DonorId = event.Value !== undefined ? event.Value : this.projectotherDetail.DonorId;
    // let obj = this.donorDataSource.findIndex(x => x.Id == event.Value);
    // this.onProjectotherDetailsChange(data, this.donorDataSource[obj].Name);
    this.onProjectotherDetailsChange(data, event.Value);

  }

  //#endregion

  //#region  Donor list
  GetAllDonorList() {
    this.DonorList = [];
    this.projectListService
      .GetAllDonorList(
        this.appurl.getApiUrl() + GLOBAL.API_Project_GetAllDonorList
      )
      .subscribe(data => {
        if (data != null) {
          if (data.data.DonorDetail != null) {
            this.donorDataSource = [];
            data.data.DonorDetail.forEach(element => {
              this.DonorList.push({
                DonorId: element.DonorId,
                Name: element.Name
              });
            }
            );
            data.data.DonorDetail.forEach(x => {
              this.donorDataSource.push({
                Id: x.DonorId,
                Name: x.Name
              });
            });
            //  this.GetOtherProjectDetailById(this.ProjectId);
          }
        }
      });
  }
  filterDonorSingle(event) {
    const query = event.query;
    return (this.Donorvalue = this.filterDonor(query, this.DonorList));

    // this.onProjectotherDetailsChange(event,this.Donorvalue)
  }
  filterDonor(query, DonorList: any[]): any[] {
    // in a real application, make a request to a remote url with the query and return filtered results, for demo we filter at client side
    const filtered: any[] = [];
    for (let i = 0; i < DonorList.length; i++) {
      const Donor = DonorList[i];
      if (query == null) {
        filtered.push(Donor.Name);
      } else if (Donor.Name.toLowerCase().indexOf(query.toLowerCase()) !== -1) {
        filtered.push(Donor.Name);
      }
    }
    return filtered;
  }

  //#endregion

  //#region GetProjectOtherDetailById

  GetOtherProjectDetailById(ProjectId: number) {
    this.projectOtherDetailPageFlag = true;
    this.opportunityFlag = true;
    this.OtherProjectList = [];
    if (ProjectId != null && ProjectId !== undefined && ProjectId !== 0) {
      this.projectListService
        .GetOtherProjectDetailsByProjectId(
          this.appurl.getApiUrl() + GLOBAL.API_GetProjectOtherDetailById,
          ProjectId
        )
        .subscribe(data => {
          if (data != null) {
            this.projectOtherDetailPageFlag = false;
            this.opportunityFlag = false;
            if (data.data.OtherProjectDetailById != null) {
              this.projectotherDetail.ProjectOtherDetailId =
                data.data.OtherProjectDetailById.ProjectOtherDetailId;
              this.projectotherDetail.opportunityNo =
                data.data.OtherProjectDetailById.opportunityNo;
              this.projectotherDetail.opportunity =
                data.data.OtherProjectDetailById.opportunity;
              this.projectotherDetail.opportunitydescription =
                data.data.OtherProjectDetailById.opportunitydescription;
              this.projectotherDetail.ProjectId =
                data.data.OtherProjectDetailById.ProjectId;
              this.projectotherDetail.ProvinceId =
                data.data.OtherProjectDetailById.ProvinceId;
              this.projectotherDetail.DistrictID =
                data.data.OtherProjectDetailById.DistrictID;
              this.projectotherDetail.StartDate =
                data.data.OtherProjectDetailById.StartDate;
              this.projectotherDetail.EndDate =
                data.data.OtherProjectDetailById.EndDate;
              this.projectotherDetail.OpportunityType =
                data.data.OtherProjectDetailById.OpportunityType;
              this.projectotherDetail.InDirectBeneficiaryFemale =
                data.data.OtherProjectDetailById.InDirectBeneficiaryFemale;
              if (this.projectotherDetail.InDirectBeneficiaryFemale === undefined
                ? 0 : this.projectotherDetail.InDirectBeneficiaryFemale) {
                this.projectotherDetail.InDirectBeneficiaryMale =
                  data.data.OtherProjectDetailById.InDirectBeneficiaryMale;
              }
              if (this.projectotherDetail.InDirectBeneficiaryMale === undefined
                ? 0 : this.projectotherDetail.InDirectBeneficiaryMale) {
                this.projectotherDetail.OfficeId =
                  data.data.OtherProjectDetailById.OfficeId;
              }
              // if (data.data.OtherProjectDetailById.OfficeId != null &&
              //   data.data.OtherProjectDetailById.OfficeId != undefined) {
              //   this.OfficeName = this.Officelist.find(
              //     x => x.OfficeId === data.data.OtherProjectDetailById.OfficeId
              //   ).OfficeName;
              // }
              // this.projectotherDetail.CurrencyId = data.data.OtherProjectDetailById.CurrencyId;
              this.projectotherDetail.DonorId =
                data.data.OtherProjectDetailById.DonorId;
              this.projectotherDetail.StrengthConsiderationId =
                data.data.OtherProjectDetailById.StrengthConsiderationId;
              this.projectotherDetail.beneficiaryMale =
                data.data.OtherProjectDetailById.beneficiaryMale;
              if (this.projectotherDetail.beneficiaryMale === undefined
                ? 0 : this.projectotherDetail.beneficiaryMale) {
                this.projectotherDetail.beneficiaryFemale =
                  data.data.OtherProjectDetailById.beneficiaryFemale;
              }
              if (this.projectotherDetail.beneficiaryFemale === undefined
                ? 0 : this.projectotherDetail.beneficiaryFemale) {
                this.projectotherDetail.beneficiaryFemale =
                  data.data.OtherProjectDetailById.beneficiaryFemale;
              }
              this.projectotherDetail.projectGoal =
              data.data.OtherProjectDetailById.projectGoal;
              this.projectotherDetail.projectObjective =
                data.data.OtherProjectDetailById.projectObjective;
              this.projectotherDetail.mainActivities =
                data.data.OtherProjectDetailById.mainActivities;
              this.projectotherDetail.SubmissionDate =
                data.data.OtherProjectDetailById.SubmissionDate;
              this.projectotherDetail.REOIReceiveDate =
                data.data.OtherProjectDetailById.REOIReceiveDate;
              this.projectotherDetail.GenderConsiderationId =
                data.data.OtherProjectDetailById.GenderConsiderationId;
              // if (
              //   data.data.OtherProjectDetailById.GenderConsiderationId != null
              // ) {
              //   this.GenderConsiderationName = this.GenderConsiderationvaluelist.find(
              //     x =>
              //       x.GenderConsiderationId ===
              //       data.data.OtherProjectDetailById.GenderConsiderationId
              //   ).GenderConsiderationName;
              // }
              this.projectotherDetail.GenderRemarks =
                data.data.OtherProjectDetailById.GenderRemarks;
              this.projectotherDetail.SecurityId =
                data.data.OtherProjectDetailById.SecurityId;
              // if (data.data.OtherProjectDetailById.SecurityId != null) {
              //   this.SecurityName = this.Securitylist.find(
              //     x =>
              //       x.SecurityId === data.data.OtherProjectDetailById.SecurityId
              //   ).SecurityName;
              // }
              this.projectotherDetail.SecurityConsiderationId =
                data.data.OtherProjectDetailById.SecurityConsiderationId;

              this.projectotherDetail.SecurityRemarks =
                data.data.OtherProjectDetailById.SecurityRemarks;
              if (this.projectotherDetail.SecurityConsiderationId != null) {
                const selectedSecurityConsideration = data.data.OtherProjectDetailById.SecurityConsiderationId.split(
                  ','
                );
                this.selectedSecurityConsideration = [];
                if (selectedSecurityConsideration.length > 0) {
                  selectedSecurityConsideration.forEach(element => {
                    this.selectedSecurityConsideration.push(element);
                  });
                }
              }
              // if(data.data.OtherProjectDetailById.ProvinceId!=null){
              //   // to get multiselect value of province
              //  // var selectedprovince = data.data.OtherProjectDetailById.ProvinceId.split(',');
              //   this.selectedProvince=[];
              //   if (selectedprovince.length > 0) {
              //     selectedprovince.forEach(element => {
              //       this.selectedProvince.push(element);
              //     });
              //     // this.selectedProvince=[data.data.OtherProjectDetailById.ProvinceId];
              //   }
              // }
              if (data.data.OtherProjectDetailById.DistrictID != null) {
                const selectedDist = data.data.OtherProjectDetailById.DistrictID.split(
                  ','
                );
                this.selectedDistrict = [];
                if (selectedDist.length > 0) {
                  selectedDist.forEach(element => {
                    this.selectedDistrict.push(element);
                  });
                }
              }
            }
          }
          if (data.StatusCode === 400) {
            this.toastr.error('No data found');
            this.opportunityFlag = false;
          }
        },
          error => {
            // this.toastr.error('Something went wrong! Please try again');
            this.opportunityFlag = false;
            this.opportunityFlag = false;
          }
        );
    }
  }
  //#endregion

  // opportunityType selection change

  onOpportunitytypeChange(event: any, data: any) {
    if (event === 'opportunityType' && data != null) {
      this.opportunityFlag = true;
      this.projectotherDetail.OpportunityType = data;
      this.AddEditOtherProjectDetails(this.projectotherDetail);
    }
  }

  //#region  "AddEditOtherProjectDetails" add/edit other project c0omon to entire page
  AddEditOtherProjectDetails(model: any) {
    if (model.ProjectOtherDetailId === 0) {
      this.commonLoaderService.showLoader();
    }
    const obj: ProjectOtherDetailModel = {
      ProjectOtherDetailId: model.ProjectOtherDetailId,
      opportunityNo: model.opportunityNo,
      opportunity: model.opportunity,
      opportunitydescription: model.opportunitydescription,
      ProjectId: model.ProjectId,
      ProvinceId: model.ProvinceId,
      DistrictID: model.DistrictID,
      OfficeId: model.OfficeId,
      StartDate: model.StartDate,
      EndDate: model.EndDate,
      CurrencyId: model.CurrencyId,
      budget: model.budget,
      beneficiaryMale: model.beneficiaryMale,
      beneficiaryFemale: model.beneficiaryFemale,
      projectGoal: model.projectGoal,
      projectObjective: model.projectObjective,
      mainActivities: model.mainActivities,
      DonorId: model.DonorId,
      SubmissionDate: model.SubmissionDate,
      REOIReceiveDate: model.REOIReceiveDate,
      StrengthConsiderationId: model.StrengthConsiderationId,
      GenderConsiderationId: model.GenderConsiderationId,
      GenderRemarks: model.GenderRemarks,
      SecurityId: model.SecurityId,
      // SecurityConsiderationId: model.SecurityConsiderationId,
      SecurityRemarks: model.SecurityRemarks,
      InDirectBeneficiaryFemale: model.InDirectBeneficiaryFemale,
      InDirectBeneficiaryMale: model.InDirectBeneficiaryMale,
      OpportunityType: model.OpportunityType
    };

    this.projectListService
      .AddEditProjectotherDetail(
        this.appurl.getApiUrl() + GLOBAL.API_Project_AddEditProjectotherDetail,
        obj
      )
      .subscribe(
        response => {
          if (response.StatusCode === 200) {
            this.opportunityFlag = false;
            this.donorFlag = false;
            this.projectotherDetail.ProjectOtherDetailId = response.CommonId.Id;
            if (this.projectotherDetail.ProjectOtherDetailId > 0) {
              this.commonLoaderService.hideLoader();
            }

            // let projectname = this.projectDetail.ProjectName;
            // let projectDes = this.projectDetail.ProjectDescription;
            // this.snackBar.open("Project other Details Added Successfully!!!", "action", {
            //   duration: 2000,
            // });
            // this.selectedProvince=["59"];
          }
          if (response.StatusCode === 400) {
            this.toastr.error(response.Message);
            this.donorFlag = true;
            this.opportunityFlag = false;
          }
        },
        error => {
          this.toastr.error('Something went wrong! Please try again');
          this.commonLoaderService.hideLoader();
          this.opportunityFlag = false;
          this.donorFlag = true;
        }
      );
  }
  //#endregion

  //#region to onProjectotherDetailsChange add/Edit project other detail
  onProjectotherDetailsChange(ev, data: any) {
    // this.selectedProvinceValue(ev);
    const projectotherDetail: ProjectOtherDetailModel = {
      ProjectId: this.ProjectId,
      ProjectOtherDetailId: this.projectotherDetail.ProjectOtherDetailId
    };
    if (data != null && data !== '' && data !== undefined) {
      if (ev === 'Donor') {
        this.donorFlag = true;
        this.projectotherDetail.DonorId = data;
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'opportunityNo') {
        if (this.opportunityNo.valid) {
          this.projectotherDetail.opportunityNo = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'opportunity') {
        if (this.opportunity.valid) {
          this.projectotherDetail.opportunity = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'opportunitydescription') {
        if (this.opportunitydescription.valid) {
          this.projectotherDetail.opportunitydescription = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }

      if (ev === 'selectedDistrict') {
        this.projectotherDetail.DistrictID = Array.from(
          new Set(data)
        ).toString();
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'Office') {
        this.projectotherDetail.OfficeId = data;
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'startDate') {
        this.projectotherDetail.StartDate =
          data != null ? this.setDateTime(data) : null;
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }

      if (ev === 'endDate') {
        this.projectotherDetail.EndDate =
          data != null ? this.setDateTime(data) : null;
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }

      if (ev === 'beneficiaryMale') {
        if (this.beneficiaryMale.valid) {
          this.projectotherDetail.beneficiaryMale = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'beneficiaryFemale') {
        if (this.beneficiaryFemale.valid) {
          this.projectotherDetail.beneficiaryFemale = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'projectGoal') {
        if (this.projectGoal.valid) {
          this.projectotherDetail.projectGoal = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'projectObjective') {
        if (this.projectObjective.valid) {
          this.projectotherDetail.projectObjective = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'mainActivities') {
        if (this.mainActivities.valid) {
          this.projectotherDetail.mainActivities = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }

      if (ev === 'submissionDate') {
        this.projectotherDetail.SubmissionDate =
          data != null ? this.setDateTime(data) : null;
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'rEOIReceiveDate') {
        this.projectotherDetail.REOIReceiveDate =
          data != null ? this.setDateTime(data) : null;
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'strength') {
        this.projectotherDetail.StrengthConsiderationId = data;
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'genderConsideration') {
        this.projectotherDetail.GenderConsiderationId = data;
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'GenderRemarks') {
        if (this.GenderRemarks.valid) {
          this.projectotherDetail.GenderRemarks = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'Security') {
        this.projectotherDetail.SecurityId = data;
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      // if (ev === 'selectedSecurityConsideration') {
      //   // var unique = Array.from(new Set(data))
      //   this.projectotherDetail.SecurityConsiderationId = Array.from(
      //     new Set(data)
      //   ).toString();
      //   this.AddEditOtherProjectDetails(this.projectotherDetail);
      // }
      if (ev === 'SecurityRemarks') {
        if (this.SecurityRemarks.valid) {
          this.projectotherDetail.SecurityRemarks = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'InDirectBeneficiaryMale') {
        if (this.InDirectBeneficiaryMale.valid) {
          this.projectotherDetail.InDirectBeneficiaryMale = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
      if (ev === 'InDirectBeneficiaryFemale') {
        if (this.InDirectBeneficiaryFemale.valid) {
          this.projectotherDetail.InDirectBeneficiaryFemale = data;
        }
        this.AddEditOtherProjectDetails(this.projectotherDetail);
      }
    }
  }
  //#endregion

  //#region "openedOfficeChange" for project oddice detail change
  openeOpportunityChange(event: any, data: any) {
    this.projectotherDetail.OpportunityType = event.Value !== undefined ? event.Value : this.projectotherDetail.OpportunityType;
    this.onProjectotherDetailsChange(data, this.projectotherDetail.OpportunityType);
  }
  //#endregion

  //#region "openedOfficeChange" for project oddice detail change
  openedOfficeChange(event: any, data: any) {
    this.projectotherDetail.OfficeId = event.Value !== undefined ? event.Value : this.projectotherDetail.OfficeId;
    this.onProjectotherDetailsChange(data, this.projectotherDetail.OfficeId);
  }
  //#endregion

  //#region "openedOfficeChange" for project oddice detail change
  openedStrengthChange(event: any, data: any) {
    this.projectotherDetail.StrengthConsiderationId = event.Value !== undefined ?
      event.Value : this.projectotherDetail.StrengthConsiderationId;
    this.onProjectotherDetailsChange(data, this.projectotherDetail.StrengthConsiderationId);
  }
  //#endregion

  //#region "openedGenderChange"
  openedGenderChange(event: any, data: any) {
    this.projectotherDetail.GenderConsiderationId = event.Value !== undefined ? event.Value : this.projectotherDetail.GenderConsiderationId;
    this.onProjectotherDetailsChange(data, this.projectotherDetail.GenderConsiderationId);
  }
  //#endregion


  openedSecurityChange(event: any, data: any) {
    this.projectotherDetail.SecurityId = event.Value !== undefined ? event.Value : this.projectotherDetail.SecurityId;
    this.onProjectotherDetailsChange(data, this.projectotherDetail.SecurityId);

  }


  openedSecurityConsChange(event: any, data: any) {
    this.projectotherDetail.SecurityConsiderationId = event.Value !== undefined ?
      event.Value : this.projectotherDetail.SecurityConsiderationId;
    this.onProjectSecurityConsiderationMultiChange(data, event.Value);
  }



  // #region to GetAllOfficeList
  // to unsbscribe the the service code
  // this.service.unsubscribe();

  GetAllOfficeList() {
    this.Officelist = [];
    this.projectListService
      .GetAllOfficeList(this.appurl.getApiUrl() + GLOBAL.API_code_GetAllOffice)
      .subscribe(data => {
        if (data != null) {
          if (data.data.OfficeDetailsList != null) {
            this.officeDataSource = [];
            data.data.OfficeDetailsList.forEach(element => {
              this.Officelist.push({
                OfficeId: element.OfficeId,
                OfficeName: element.OfficeName
              });
            });
            data.data.OfficeDetailsList.forEach(element => {
              this.officeDataSource.push({
                Id: element.OfficeId,
                Name: element.OfficeName
              });
            });
            // this.GetOtherProjectDetailById(this.ProjectId);
          }
        }
      });
  }
  filterOfficeSingle(event) {
    const query = event.query;
    return (this.Officevalue = this.filterOffice(query, this.Officelist));
  }
  filterOffice(query, Officelist: any[]): any[] {
    // in a real application, make a request to a remote url with the query and return filtered results, for demo we filter at client side
    const filtered: any[] = [];
    for (let i = 0; i < Officelist.length; i++) {
      const Office = Officelist[i];
      if (query == null) {
        filtered.push(Office.OfficeName);
      } else if (
        Office.OfficeName.toLowerCase().indexOf(query.toLowerCase()) !== -1
      ) {
        filtered.push(Office.OfficeName);
      }
    }
    return filtered;
  }

  //#endregion

  //#region onlyUnique and setDateTime
  onlyUnique(value, index, self) {
    return self.indexOf(value) === index;
  }

  setDateTime(data): any {
    return new Date(
      new Date(data).getFullYear(),
      new Date(data).getMonth(),
      new Date(data).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds()
    );
  }

  // get budgetLineStartDate() {
  //   return this.budgetLineBreakdownFlowForm.get('BudgetLineStartDate').value;
  // }

  //#endregion

  
  setProjectOtherDetailValueForPdf() {

    this.projectOtherDetailPdf = {
      // Opportunity Details
      ProjectName : this.data.projectName,
      Description : this.data.description,
      OpportunityType: this.OpportunityTypeList.find(x => x.Id === this.projectotherDetail.OpportunityType) != null ? this.OpportunityTypeList.find(x => x.Id === this.projectotherDetail.OpportunityType).Name : '',
      Donor: this.donorDataSource.find(x => x.Id === this.projectotherDetail.DonorId) != null ? this.donorDataSource.find(x => x.Id === this.projectotherDetail.DonorId).Name : '',
      OpportunityNo:  this.projectotherDetail.opportunityNo != null ? this.projectotherDetail.opportunityNo : '',
      Opportunity:  this.projectotherDetail.opportunity != null ? this.projectotherDetail.opportunity : '',
      OpportunityDescription: this.projectotherDetail.opportunitydescription != null ? this.projectotherDetail.opportunitydescription : '',

      Country: this.CountrySelectionList.find(x => x.value === this.countryMultiSelectModel.CountryId) != null ? this.CountrySelectionList.find(x => x.value === this.countryMultiSelectModel.CountryId).label : '',
      Province: this.provinceMultiSelectModel.ProvinceId.map(x => this.ProvinceSelectionList.filter(y => y.value === x).map(z => z.label)).toString(),
      District: this.districtMultiSelctModel.DistrictID.map(x => this.DistrictMultiSelectList.filter(y => y.value === x).map(z => z.label)).toString(),
      Office: this.donorDataSource.find(x => x.Id === this.projectotherDetail.OfficeId) != null ? this.officeDataSource.find(x => x.Id === this.projectotherDetail.OfficeId).Name : '', 
      Sector: this.Sector.toString(),
      Program: this.Program.toString(),
      StartDate: this.projectotherDetail.StartDate != null ? this.projectotherDetail.StartDate : '',
      EndDate: this.projectotherDetail.EndDate != null ? this.projectotherDetail.EndDate : '',
  
      // Project Objective & Goal
      ProjectGoal: this.projectotherDetail.projectGoal != null ? this.projectotherDetail.projectGoal : '',
      ProjectObjective: this.projectotherDetail.projectObjective != null ? this.projectotherDetail.projectObjective : '',
      MainActivities: this.projectotherDetail.mainActivities != null ? this.projectotherDetail.mainActivities : '',
      REOIReceiveDate: this.projectotherDetail.REOIReceiveDate != null ? this.projectotherDetail.REOIReceiveDate : '',
      SubmissionDate: this.projectotherDetail.SubmissionDate != null ? this.projectotherDetail.SubmissionDate : '',
  
      // Beneficiary Details
      DirectbeneficiarMale: this.projectotherDetail.beneficiaryMale != null ?  this.projectotherDetail.beneficiaryMale.toString() : '',
      InDirectbeneficiarMale: this.projectotherDetail.InDirectBeneficiaryMale != null ?  this.projectotherDetail.InDirectBeneficiaryMale.toString() : '',
      DirectbeneficiarFemale: this.projectotherDetail.beneficiaryFemale != null ?  this.projectotherDetail.beneficiaryFemale.toString() : '',
      InDirectbeneficiarFemale: this.projectotherDetail.InDirectBeneficiaryFemale != null ?  this.projectotherDetail.InDirectBeneficiaryFemale.toString() : '',
     
      TotalDirectBeneficiary: this.projectotherDetail.beneficiaryFemale != null && this.projectotherDetail.beneficiaryMale != null ? (this.projectotherDetail.beneficiaryFemale + this.projectotherDetail.beneficiaryMale).toString() : '',
      TotalInDirectBeneficiary: this.projectotherDetail.InDirectBeneficiaryMale != null && this.projectotherDetail.InDirectBeneficiaryFemale != null ? (this.projectotherDetail.InDirectBeneficiaryMale + this.projectotherDetail.InDirectBeneficiaryFemale).toString() : '',
  
      // Gender Consideration
      StrengthConsideration: this.projectotherDetail.StrengthConsiderationId != null ? (this.strengthDataSource.find(x => x.Id === this.projectotherDetail.StrengthConsiderationId) ?  this.strengthDataSource.find(x => x.Id === this.projectotherDetail.StrengthConsiderationId).Name : '') : '',
      GenderConsideration:  this.projectotherDetail.GenderConsiderationId != null ? this.GenderConsiderationvaluelist.find(x => x.GenderConsiderationId === this.projectotherDetail.GenderConsiderationId).GenderConsiderationName : '',
      GenderRemarks: this.projectotherDetail.GenderRemarks != null ? this.projectotherDetail.GenderRemarks : '',
  
      // Security Consideration
      Security: this.projectotherDetail.StrengthConsiderationId != null ? this.Securitylist.find(x => x.SecurityId === this.projectotherDetail.SecurityId).SecurityName : '',
      SecurityConsideration: this.securityConsiderationMultiselect.SecurityConsiderationId.map(x => this.securityConsDataSource.filter(y => y.Id === x).map(z => z.Name)).toString(),
      SecurityRemarks: this.projectotherDetail.SecurityRemarks != null ? this.projectotherDetail.SecurityRemarks : ''
    };
  }


  //#region "onExportPdf"
  onExportPdf() {
    // set your pdf values here

    this.setProjectOtherDetailValueForPdf();
    
    console.log(this.projectOtherDetailPdf);

    this.pDetailPdfService.onExportPdf(this.projectOtherDetailPdf);
  }
  //#endregion

}
