import {
  Component,
  OnInit,
  ViewChild,
  TemplateRef,
  ViewEncapsulation
} from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import {
  DataTableModule,
  SharedModule,
  AccordionModule
} from 'primeng/primeng';
import { UserService } from './user.service';
import { MultiSelectModule } from 'primeng/primeng';
import { SelectItem } from 'primeng/components/common/api';
import { GLOBAL } from '../../shared/global';
import { DropdownModule } from 'primeng/primeng';
import { ToastrService } from 'ngx-toastr';
import { TextMaskModule } from 'angular2-text-mask';
import { CustomValidation } from '../../shared/customValidations';
import {
  applicationPages,
  applicationModule
} from '../../shared/application-pages-enum';
import { Router } from '@angular/router';
import { OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { AppSettingsService } from '../../service/app-settings.service';
import { AddUsers, EditUsers } from '../../model/add-user';
import { CommonService } from '../../service/common.service';
import { RestPasswordModel } from '../../model/current-password-model';

export interface UserDetails {
  FirstName;
  LastName;
  Email;
  Office;
  Status;
  UserId;
  Id;
}
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class UserComponent implements OnInit, OnDestroy {
  private unsubscribe: Subject<void> = new Subject<void>();

  // AddUser Modal Popup

  loading = false;
  loadingPermission = false;
  loadingRolesMultiselect = false;
  loadingPermissionForRoles = false;
  loadingRoles = false;
  editRole = false;

  private userName: string;
  selectedValueInPermission: any[];

  selectedValue: any;
  userForm: FormGroup;
  userRoles: FormGroup;
  AddPermissions: FormGroup;
  EditRolePermissions: FormGroup;
  userEditForm: FormGroup;
  ddnSelectedRole: any;
  EditRolePermission: rolePermissions[];
  EditApproveRejectRolePermission: ApproveRejectRolePermission[];
  EditOrderScheduleRolePermission: OrderSchedulePermission[];
  EditAgreeDisagreeRolePermission: AgreeDisagreeRolePermission[];
  // Modal popup instances
  modalRef: BsModalRef;
  modalRefPermission: BsModalRef;
  modalPermission: BsModalRef;
  modalResetPassword: BsModalRef;
  modalRole: BsModalRef;

  applicationPagePermissions: iPermissions;
  projectManagementPermissions: any[];
  agreeDisagreePermissions: any[];
  orderSchedulePermissions: any[];

  modalEditUserPermission: BsModalRef;
  config = {
    animated: true,
    keyboard: true,
    backdrop: true,
    ignoreBackdropClick: false
  };

  // User DataTabale
  userDetails: UserDetails[];

  // Add User dropdowns
  officeId: SelectItem[];
  status: SelectItem[];
  department: SelectItem[];
  roles: SelectItem[];
  RolesUser: SelectItem[];
  Permissions: SelectItem[];
  private ChangePassword: FormGroup;

  // UserId For AddRoles
  UserId: any; // Global UserId FROM Table is selected for controls
  addRoles: any[];
  arrPermissionsId: any[];
  arrPermissionsName: any[];
  permissionsAndRoleModel: any[];
  selectedValueInRoles: any[];
  selectedRole: any;
  setDepartmentValue: any;
  isEditingAllowed: boolean;

  userPermission: any[] = [];
  codePermission: any[] = [];
  employeePermission: any[] = [];
  storePermission: any[] = [];
  accountingPermission: any[] = [];
  marketingPermission: any[] = [];
  projectsPermission: any[] = [];

  editUserPermission: any[] = [];
  editCodePermission: any[] = [];
  editEmployeePermission: any[] = [];
  editStorePermission: any[] = [];
  editAccountingPermission: any[] = [];
  editMarketingPermission: any[] = [];
  editProjectsPermission: any[] = [];

  public mask = [
    '(',
    /[1-9]/,
    /\d/,
    /\d/,
    ')',
    ' ',
    /\d/,
    /\d/,
    /\d/,
    '-',
    /\d/,
    /\d/,
    /\d/,
    /\d/
  ];

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private setting: AppSettingsService,
    private modalService: BsModalService,
    private userService: UserService,
    private commonservice: CommonService
  ) {
    // AddUser Modal Popup
    this.userForm = this.fb.group({
      FirstName: [
        null,
        Validators.compose([
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(20)
        ])
      ],
      LastName: [
        null,
        Validators.compose([
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(20)
        ])
      ],
      Email: [null, [Validators.required, Validators.email]],
      Phone: [null, Validators.compose([Validators.required,
                Validators.minLength(10),
                Validators.maxLength(14)])],
      Password: [
        null,
        Validators.compose([
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(20)
        ])
      ],
      ConfirmPassword: [
        null,
        Validators.compose([
          CustomValidation.ComparePassword,
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(20)
        ])
      ],
      OfficeId: [null, Validators.required],
      Department: [null],
      Status: [null]
    });

    this.ChangePassword = this.fb.group({
      NewPassword: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(20)
        ])
      ],
      ConfirmPassword: [
        '',
        Validators.compose([
          CustomValidation.ConfirmPassword,
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(20)
        ])
      ]
    });
    // Status Array
    this.status = [
      { label: 'Active', value: 1 },
      { label: 'InActive', value: 0 }
    ];

    // Add Roles Form
    this.userRoles = this.fb.group({
      Roles: [null]
    });

    // Add Permissions Form
    this.AddPermissions = this.fb.group({
      Roles: [null],
      Permissions: []
    });

    // Edit Role Permissions Form
    this.EditRolePermissions = this.fb.group({
      Roles: [''],
      Permissions: []
    });

    this.userEditForm = this.fb.group({
      FirstName: [
        null,
        Validators.compose([
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(20)
        ])
      ],
      LastName: [
        null,
        Validators.compose([
          Validators.required,
          Validators.minLength(1),
          Validators.maxLength(20)
        ])
      ],
      Email: [null, [Validators.required, Validators.email]],
      Phone: [null, Validators.required],
      OfficeId: [null, Validators.required],
      Department: [null],
      Status: [null]
    });

    this.isEditingAllowed = this.commonservice.IsEditingAllowed(
      applicationPages.Users
    );
  }

  display = false;
  ngOnInit() {
    this.formInitialize();
    this.getOffices();
    this.getUserList();
    this.getUserRoles();
    this.getAllApplicationPage();
  }

  formInitialize() {
    this.applicationPagePermissions = {
      RoleName: '',
      Permissions: []
    };
  }

  getOffices() {
    this.userService
      .getOffices(this.setting.getBaseUrl() + GLOBAL.API_AllOffice_URL)
      .subscribe(data => {
        this.officeId = [];
        data.data.OfficeDetailsList.forEach(element => {
          this.officeId.push({
            label: element.OfficeName,
            value: element.OfficeId
          });
        });
      });
  }

  getDepartment(officeCode) {
    this.userService
      .getDepartment(
        this.setting.getBaseUrl() + GLOBAL.API_Code_GetDepartmentsByOfficeId,
        officeCode
      )
      .subscribe(data => {
        this.department = [];
        data.data.Departments.forEach(element => {
          this.department.push({
            label: element.DepartmentName,
            value: element.DepartmentId
          });
        });
      });
  }

  getAllApplicationPage() {
    this.userService
    .getPermissions(
      this.setting.getBaseUrl() +
       GLOBAL.API_Permissions_GetAllApplicationPages
       )
       .subscribe(data => {
      if (data.StatusCode === 200) {
      this.applicationPagePermissions.RoleName = '';
      this.applicationPagePermissions.Permissions = [];
      this.projectManagementPermissions = [];
      this.agreeDisagreePermissions = [];
      this.orderSchedulePermissions = [];
      this.userPermission = [];
      this.codePermission = [];
      this.employeePermission = [];
      this.storePermission = [];
      this.projectsPermission = [];
      this.marketingPermission = [];
      this.accountingPermission = [];

      data.data.ApplicationPagesList.forEach(element => {
        this.applicationPagePermissions.Permissions.push({
           PageId: element.PageId,
          PageName: element.PageName,
           Edit: false,
            View: false,
             ModuleName: element.ModuleName,
              ModuleId: element.ModuleId
        });
      });

      const userp = this.applicationPagePermissions.Permissions.filter(
        z => z.ModuleId === applicationModule.Users
      );
      const codep = this.applicationPagePermissions.Permissions.filter(
        z => z.ModuleId === applicationModule.Code
      );
      const hrp = this.applicationPagePermissions.Permissions.filter(
        z => z.ModuleId === applicationModule.HR
      );
      const storep = this.applicationPagePermissions.Permissions.filter(
        z => z.ModuleId === applicationModule.Store
      );
      const accountingp = this.applicationPagePermissions.Permissions.filter(
        z =>
          z.ModuleId === applicationModule.Accounting ||
          z.ModuleId === applicationModule.AccountingNew
      );
      const marketingp = this.applicationPagePermissions.Permissions.filter(
        z => z.ModuleId === applicationModule.Marketing
      );
      const projectsp = this.applicationPagePermissions.Permissions.filter(
        z => z.ModuleId === applicationModule.Projects
      );

      userp.forEach(y => {
        this.userPermission.push({
          PageId: y.PageId,
          PageName: y.PageName,
          Edit: y.Edit,
          View: y.View,
          ModuleName: y.ModuleName,
          ModuleId: y.ModuleId
        });
      });

      codep.forEach(y => {
        this.codePermission.push({
          PageId: y.PageId,
          PageName: y.PageName,
          Edit: y.Edit,
          View: y.View,
          ModuleName: y.ModuleName,
          ModuleId: y.ModuleId
        });
      });

      storep.forEach(y => {
          this.storePermission.push({
            PageId: y.PageId,
            PageName: y.PageName,
            Edit: y.Edit,
            View: y.View,
            ModuleName: y.ModuleName,
            ModuleId: y.ModuleId
          });
        });

        hrp.forEach(y => {
          this.employeePermission.push({
            PageId: y.PageId,
            PageName: y.PageName,
            Edit: y.Edit,
            View: y.View,
            ModuleName: y.ModuleName,
            ModuleId: y.ModuleId
          });
        });

        accountingp.forEach(y => {
          this.accountingPermission.push({
            PageId: y.PageId,
            PageName: y.PageName,
            Edit: y.Edit,
            View: y.View,
            ModuleName: y.ModuleName,
            ModuleId: y.ModuleId
          });
        });

        marketingp.forEach(y => {
          this.marketingPermission.push({
            PageId: y.PageId,
            PageName: y.PageName,
            Edit: y.Edit,
            View: y.View,
            ModuleName: y.ModuleName,
            ModuleId: y.ModuleId
          });
        });

        projectsp.forEach(y => {
          this.projectsPermission.push({
            PageId: y.PageId,
            PageName: y.PageName,
            Edit: y.Edit,
            View: y.View,
            ModuleName: y.ModuleName,
            ModuleId: y.ModuleId
          });
        });


        data.data.ApplicationPagesList.forEach(x => {
          if (x.PageId === applicationPages.Contracts) {
            this.projectManagementPermissions.push({
               PageId: x.PageId,
              PageName: x.PageName,
              Approve: false,
              Reject: false,
              ModuleId: x.ModuleId, ModuleName: x.ModuleName
            });
          }
          if (x.PageId === applicationPages.MarketingJobs) {
            this.projectManagementPermissions.push({ PageId: x.PageId,
              PageName: x.PageName, Approve: false, Reject: false,
              ModuleId: applicationModule.Marketing, ModuleName: 'Marketing'
            });
          }
          if (x.PageId === applicationPages.MarketingJobs) {
            this.agreeDisagreePermissions.push({ PageId: x.PageId,
              PageName: x.PageName, Agree: false, Disagree: false,
              ModuleId: applicationModule.Marketing, ModuleName: 'Marketing'
            });
          }

          if (x.PageId === applicationPages.Policy) {
            this.orderSchedulePermissions.push({ PageId: x.PageId,
              PageName: x.PageName, OrderSchedule: false,
              ModuleId: applicationModule.Marketing, ModuleName: 'Marketing'
            });
          }
          localStorage.setItem('permissions', JSON.stringify(this.applicationPagePermissions.Permissions));
          localStorage.setItem('approveRejectPermissions', JSON.stringify(this.projectManagementPermissions));
          localStorage.setItem('agreeDisagreePermissions', JSON.stringify(this.agreeDisagreePermissions));
          localStorage.setItem('orderSchedulePermissions', JSON.stringify(this.orderSchedulePermissions));
          });
        }
      });
  }

  onChangeOffice(officeCode: any) {
    this.getDepartment(officeCode);
  }

  onRoleChange(event) {
    this.selectedValueInPermission = [];
    this.selectedRole = event.value.id;
    this.loadingPermissionForRoles = true;
    this.userService
      .getPermissionByRoleId(
        this.setting.getBaseUrl() +
          GLOBAL.API_Permissions_GetPermissionsByRoleId,
        this.selectedRole
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            this.loadingPermissionForRoles = false;
            data.data.PermissionsInRolesList.forEach(element => {
              this.selectedValueInPermission.push({
                PermissionId: element.PermissionId,
                PermissionName: element.PermissionName
              });
            });
          }
        },
        error => {
          // this.loading=false;
          // this.toastr.error("There is Some error....");
          if (error.message === 500) {
          } else if (error.message === 0) {
            // this.messages.push({ severity: 'error', summary: 'Error Message', detail: 'Network error, Please try again later' });
          } else {
            // this.messages.push({ severity: 'error', summary: 'Error Message', detail: 'Some error occured, Please contact your admin' });
          }
        }
      );
  }

  getUserList() {
    this.loading = true;
    this.userService
      .GetUserList(
        this.setting.getBaseUrl() + GLOBAL.API_UserDetail_GetUserList
      )
      .subscribe(
        data => {
          this.userDetails = [];
          this.loading = false;
          data.data.UserDetailsList.forEach(element => {
            this.userDetails.push({
              FirstName: element.FirstName,
              LastName: element.LastName,
              Email: element.Username,
              UserId: element.UserID,
              Id: element.Id,
              Office: element.OfficeName,
              Status: element.Status === 1 ? 'Active' : 'InActive'
            });
          });
          this.commonservice.setLoader(false);
        },
        error => {
          this.loading = false;

          if (error.StatusCode === 500) {
            this.toastr.error('Internal Server Error....');
          } else if (error.StatusCode === 401) {
            this.toastr.error('Unauthorized Access Error....');
            // this.messages.push({ severity: 'error', summary: 'Error Message', detail: 'Network error, Please try again later' });
          } else if (error.StatusCode === 403) {
            this.toastr.error('Forbidden Error....');
          } else {
            // this.messages.push({ severity: 'error', summary: 'Error Message', detail: 'Some error occured, Please contact your admin' });
          }
        }
      );
  }

  getUserRoles() {
    this.loadingRoles = true;
    this.userService
      .getUserRoles(
        this.setting.getBaseUrl() + GLOBAL.API_UserRoles_GetRolesList
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.loadingRoles = false;
          this.roles = [];
          data.data.RoleList.forEach(element => {
            this.roles.push({
              label: element.RoleName,
              value: { id: element.Id, name: element.RoleName }
            });
          });
        }

        this.loadingRoles = false;
      });
  }

  onChangePermission(event) {
    this.permissionsAndRoleModel = [];
    event.value.forEach(element => {
      this.permissionsAndRoleModel.push({
        RoleId: this.selectedRole,
        PermissionId: element.PermissionId
      });
    });
  }

  assignRolesToUser(Roles) {
    this.loading = true;
    this.addRoles = [];
    // tslint:disable-next-line:forin
    for (const i in Roles.Roles) {
      this.addRoles.push(Roles.Roles[i].name);
    }
    this.userService
      .assignRolesToUser(
        this.setting.getBaseUrl() + GLOBAL.API_UserRoles_AssignRoleToUser,
        this.UserId,
        this.addRoles
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          // Success
          this.toastr.success('Roles Added Successfully!!!');
          this.getUserList();
          this.modalRefPermission.hide();
          this.loading = false;
        } else {
          this.toastr.error('Error!!!');
          this.loading = false;
        }
      });
  }

  getUserDetailByUserId(UserId) {
    this.userService
      .getUserDetailByUserId(
        this.setting.getBaseUrl() +
          GLOBAL.API_UserDetail_GetUserDetailsByUserId,
        UserId
      )
      .subscribe(data => {
        this.setDepartmentValue = [];
        // this.getDepartment(data.data.UserDetails.OfficeId);
        // data.data.UserDetailsList.forEach(element => {
        //   this.setDepartmentValue.push({ label: element.DepartmentName, value: element.DepartmentId });
        //   this.userEditForm.setValue({
        //     FirstName: element.FirstName,
        //     LastName: element.LastName,
        //     Email: element.Username,
        //     Phone: element.Phone,
        //     OfficeId: element.OfficeId,
        //     Department: element.DepartmentId,
        //     Status: element.Status
        //   });
        // });

        this.userEditForm.setValue({
          FirstName: data.data.UserDetails.FirstName,
          LastName: data.data.UserDetails.LastName,
          Email: data.data.UserDetails.Username,
          Phone: data.data.UserDetails.Phone,
          OfficeId: data.data.UserDetails.OfficeId,
          Department: data.data.UserDetails.DepartmentId,
          Status: data.data.UserDetails.Status
        });
      });
  }

  PermissionsInRoles(value) {
     

    this.loadingPermission = true;
    const isRoleAlreadyExists: any = this.roles.filter(
      x => x.label === this.applicationPagePermissions.RoleName
    );

    if (isRoleAlreadyExists.length <= 0) {
      const filteredApplicationPages: any = {
        RoleName: '',
        Permissions: []
      }; // pages that have permission true for edit or view

      const filteredProjectManagementPermission: any = [];

      this.applicationPagePermissions.Permissions.forEach(element => {
        if (element.Edit || element.View) {
          filteredApplicationPages.Permissions.push({PageId: element.PageId, PageName: element.PageName,
            ModuleName: element.ModuleName, ModuleId: element.ModuleId, Edit: element.Edit, View: element.View,
            Approve: element.Approve, Reject: element.Reject} );
        }
      });

      this.projectManagementPermissions.forEach(element => {
        if (element.Approve || element.Reject) {
          filteredApplicationPages.Permissions.push({PageId: element.PageId, PageName: element.PageName,
            ModuleName: element.ModuleName, ModuleId: element.ModuleId,
            Approve: element.Approve, Reject: element.Reject} );
        }
      });

      this.agreeDisagreePermissions.forEach(element => {
        if (element.Agree || element.Disagree) {
          filteredApplicationPages.Permissions.push({PageId: element.PageId, PageName: element.PageName,
            ModuleName: element.ModuleName, ModuleId: element.ModuleId,
            Agree: element.Agree, Disagree: element.Disagree} );
        }
      });

      this.orderSchedulePermissions.forEach(element => {
        if (element.OrderSchedule) {
          filteredApplicationPages.Permissions.push({PageId: element.PageId, PageName: element.PageName,
            ModuleName: element.ModuleName, ModuleId: element.ModuleId,
            OrderSchedule: element.OrderSchedule} );
        }
      });

      this.userPermission.forEach(element => {
        if (element.Edit || element.View) {
          filteredApplicationPages.Permissions.push({
            PageId: element.PageId,
            PageName: element.PageName,
            ModuleName: element.ModuleName,
            ModuleId: element.ModuleId,
            Edit: element.Edit,
            View: element.View
          });
        }
      });

      this.codePermission.forEach(element => {
        if (element.Edit || element.View) {
          filteredApplicationPages.Permissions.push({
            PageId: element.PageId,
            PageName: element.PageName,
            ModuleName: element.ModuleName,
            ModuleId: element.ModuleId,
            Edit: element.Edit,
            View: element.View
          });
        }
      });

      this.employeePermission.forEach(element => {
        if (element.Edit || element.View) {
          filteredApplicationPages.Permissions.push({
            PageId: element.PageId,
            PageName: element.PageName,
            ModuleName: element.ModuleName,
            ModuleId: element.ModuleId,
            Edit: element.Edit,
            View: element.View
          });
        }
      });

      this.storePermission.forEach(element => {
        if (element.Edit || element.View) {
          filteredApplicationPages.Permissions.push({
            PageId: element.PageId,
            PageName: element.PageName,
            ModuleName: element.ModuleName,
            ModuleId: element.ModuleId,
            Edit: element.Edit,
            View: element.View
          });
        }
      });

      this.marketingPermission.forEach(element => {
        if (element.Edit || element.View) {
          filteredApplicationPages.Permissions.push({
            PageId: element.PageId,
            PageName: element.PageName,
            ModuleName: element.ModuleName,
            ModuleId: element.ModuleId,
            Edit: element.Edit,
            View: element.View
          });
        }
      });

      this.projectsPermission.forEach(element => {
        if (element.Edit || element.View) {
          filteredApplicationPages.Permissions.push({
            PageId: element.PageId,
            PageName: element.PageName,
            ModuleName: element.ModuleName,
            ModuleId: element.ModuleId,
            Edit: element.Edit,
            View: element.View
          });
        }
      });

      this.accountingPermission.forEach(element => {
        if (element.Edit || element.View) {
          filteredApplicationPages.Permissions.push({
            PageId: element.PageId,
            PageName: element.PageName,
            ModuleName: element.ModuleName,
            ModuleId: element.ModuleId,
            Edit: element.Edit,
            View: element.View
          });
        }
      });

      this.projectManagementPermissions.forEach(element => {
        if (element.Approve || element.Reject) {
          filteredProjectManagementPermission.push({
            PageId: element.PageId,
            Approve: element.Approve,
            Reject: element.Reject
          });
        }
      });

      filteredApplicationPages.RoleName = this.applicationPagePermissions.RoleName;
      this.userService
        .PermissionsInRoles(
          this.setting.getBaseUrl() +
            GLOBAL.API_Permissions_AddRoleWithPagePermissions,
          filteredApplicationPages
        )
        .subscribe(data => {
          if (data.StatusCode === 200) {
            this.loadingPermission = false;
            this.toastr.success('Permissions Added Successfully!!!');
            this.modalPermission.hide();
            this.getAllApplicationPage();
            this.getUserRoles();
          } else {
            this.loadingPermission = false;
            this.toastr.error('Error!!!');
          }
        });
    } else {
      this.toastr.warning('Role name already Exists');
      this.loadingPermission = false;
    }
  }

  openModalWithClass(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(
      template,
      Object.assign({}, this.config, { class: 'gray modal-lg' })
    );
  }

  userFormSubmit(model) {
    this.loading = true;
    const obj: any = {};
    const addUser: AddUsers = {
      UserName: model.Email,
      Email: model.Email,
      Password: model.Password,
      FirstName: model.FirstName,
      LastName: model.LastName,
      UserType: localStorage.getItem('UserRoles'),
      OfficeCode: null,
      OfficeName: '',
      DepartmentName: '',
      DepartmentId: model.Department == null ? 0 : model.Department,
      Status: model.Status,
      Phone: model.Phone,
      OfficeId: model.OfficeId,
      UserOfficesList: null
    };
    this.userService
      .AddUser(
        this.setting.getBaseUrl() + GLOBAL.API_UserDetail_AddUser,
        addUser
      )
      .subscribe(
        data => {
          this.loading = false;
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('User Added Successfully!!!');
            this.getUserList();
            this.modalRef.hide();
          } else if (data.StatusCode === 900) {
            this.toastr.error('User already exist!!!');
          } else {
            this.toastr.error('Error!!!');
          }
        },
        error => {
          // error message
        }
      );
    // this.modalRef.hide();
  }

  userEditFormSubmit(model) {
    const obj: any = {};
    const editUser: EditUsers = {
      UserName: model.Email,
      Email: model.Email,
      // Password : model.Password,
      FirstName: model.FirstName,
      LastName: model.LastName,
      UserType: localStorage.getItem('UserRoles'),
      OfficeCode: null,
      OfficeName: '',
      DepartmentName: '',
      DepartmentId: 0,
      Status: model.Status,
      Id: this.UserId,
      Phone: model.Phone,
      OfficeId: model.OfficeId
    };

    this.userService
      .EditUser(
        this.setting.getBaseUrl() + GLOBAL.API_UserDetail_EditUser,
        editUser
      )
      .subscribe(
        data => {
          if (data.StatusCode === 200) {
            // Success
            this.toastr.success('User Updated Successfully!!!');
            this.getUserList();
          } else {
            this.toastr.error('Error!!!');
          }
          this.modalEditUserPermission.hide();
        },
        error => {
          // error message
        }
      );
    this.modalEditUserPermission.hide();
  }

  onSubmitPasswordChange(model: RestPasswordModel) {
    model.UserName = this.userName;

    this.userService
      .resetPassword(
        this.setting.getBaseUrl() + GLOBAL.API_ResetPassword,
        model
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.toastr.success('Password Reset Successfully!');
          this.modalResetPassword.hide();
        } else {
          this.toastr.error('There is somme error');
        }
      });
  }
  openModelOnResetPassword(templateReset: TemplateRef<any>, covalue) {
    this.userName = covalue.Email;
    this.modalResetPassword = this.modalService.show(
      templateReset,
      Object.assign({}, this.config, { class: 'gray modal-lg' })
    );
  }
  // openModalPermissions(templatePermissions: TemplateRef<any>,colvalue) {
  getUserRolesByUserId(UserId) {
    this.loadingRolesMultiselect = true;
    this.userService
      .getUserRolesByUserId(
        this.setting.getBaseUrl() + GLOBAL.API_UserRoles_GetUserRolesByUserId,
        UserId
      )
      .subscribe(data => {
        if (data.StatusCode === 200) {
          this.loadingRolesMultiselect = false;
          this.selectedValueInRoles = [];
          data.data.RoleList.forEach(element => {
            this.selectedValueInRoles.push({
              id: element.Id,
              name: element.RoleName
            });
          });
        }
      });
  }

  openModalPermissions(templatePermissions: TemplateRef<any>, colvalue) {
    this.getUserRolesByUserId(colvalue.Id);
    this.UserId = colvalue.Id;
    this.modalRefPermission = this.modalService.show(
      templatePermissions,
      Object.assign({}, this.config, { class: 'gray modal-lg' })
    );
  }

  openModalAddPermissions(AddPermissionsTemplate: TemplateRef<any>) {
    this.getPermissions();
    this.modalPermission = this.modalService.show(
      AddPermissionsTemplate,
      Object.assign({}, this.config, { class: 'gray modal-lg' })
    );
  }

  openModalEditPermissions(
    EditPermissionsTemplate: TemplateRef<any>,
    colvalue
  ) {
    this.getPermissions();
    this.getUserDetailByUserId(colvalue.Id);
    this.UserId = colvalue.Id;
    this.modalEditUserPermission = this.modalService.show(
      EditPermissionsTemplate,
      Object.assign({}, this.config, { class: 'gray modal-lg' })
    );
  }

  openModalEditRolePermissions(EditPermissionsTemplate: TemplateRef<any>) {
    this.getPermissions();
    this.modalPermission = this.modalService.show(
      EditPermissionsTemplate,
      Object.assign({}, this.config, { class: 'gray modal-lg' })
    );
  }

  getPermissions() {
    this.Permissions = [];
    this.userService
      .getPermissions(
        this.setting.getBaseUrl() + GLOBAL.API_Permissions_GetPermissions
      )
      .subscribe(data => {
        data.data.PermissionsList.forEach(element => {
          this.Permissions.push({
            label: element.Name,
            value: { PermissionId: element.Id, PermissionName: element.Name }
          });
        });
      });
  }

  GetRolePermissions() {

    this.loadingPermission = true;
    if (
      this.ddnSelectedRole !== undefined &&
      this.ddnSelectedRole != null &&
      this.ddnSelectedRole !== ''
    ) {
      this.userService
        .getRolePermissions(
          this.setting.getBaseUrl() +
            GLOBAL.API_Permissions_GetPermissionsOnSelectedRole,
          this.ddnSelectedRole.id
        )
        .subscribe(data => {
          this.EditRolePermission = [];
          this.EditApproveRejectRolePermission = [];
          this.EditAgreeDisagreeRolePermission = [];
          this.EditOrderScheduleRolePermission = [];
          // let applicationpages= localStorage.getItem('ApplicationPages');

          data.data.PermissionsInRole.forEach(e => {
            this.EditRolePermission.push({
              Edit: e.Edit,
              ModuleId: e.ModuleId,
              ModuleName: '',
              PageId: e.PageId,
              PageName: e.PageName,
              View: e.View,
              Approve: e.Approve,
              Reject: e.Reject,
              Agree: e.Agree,
              Disagree: e.Disagree
            });
          });

          data.data.ApproveRejectPermissionsInRole.forEach(element => {
            this.EditApproveRejectRolePermission.push({
              Id: element.Id,
              PageId: element.PageId,
              Approve: element.Approve,
              Reject: element.Reject,
              RoleId: element.RoleId,
              PageName: element.PageName
            });
          });

          data.data.AgreeDisagreePermissionsInRole.forEach(element => {
            this.EditAgreeDisagreeRolePermission.push({
              Id: element.Id,
              PageId: element.PageId,
              Agree: element.Agree,
              Disagree: element.Disagree,
              RoleId: element.RoleId,
              PageName: element.PageName
            });
          });

          data.data.OrderSchedulePermissionsInRole.forEach(element => {
            this.EditOrderScheduleRolePermission.push({
              Id: element.Id,
              PageId: element.PageId,
              OrderSchedule: element.OrderSchedule,
              RoleId: element.RoleId,
              PageName: element.PageName
            });
          });

          const permissionsAgreeDisagree: AgreeDisagreeRolePermission[] = JSON.parse(localStorage.getItem('agreeDisagreePermissions'));
          const orderSchedulePermission: OrderSchedulePermission[] = JSON.parse(localStorage.getItem('orderSchedulePermissions'));
          const permissionsnotpresentAgreeDisagree: AgreeDisagreeRolePermission[] = [];
          const permissionsApproveReject: ApproveRejectRolePermission[] = JSON.parse(localStorage.getItem('approveRejectPermissions'));
          const permissionsnotpresentApproveReject: ApproveRejectRolePermission[] = [];
          const permissionsnotpresentOrderSchedule: OrderSchedulePermission[] = [];
          const permissions: rolePermissions[] = JSON.parse(localStorage.getItem('permissions'));
          const permissionsnotpresent: rolePermissions[] = [];

          // let per= this.EditRolePermission;
          permissions.forEach(x => {
            if (!this.EditRolePermission.some(s => s.PageId === x.PageId)) {
              permissionsnotpresent.push(x);
            }
          },
          permissionsApproveReject.forEach(x => {
            if (!this.EditApproveRejectRolePermission.some(s => s.PageId === x.PageId)) {
              permissionsnotpresentApproveReject.push(x);
            }
          },
          permissionsAgreeDisagree.forEach(x => {
            if (!this.EditAgreeDisagreeRolePermission.some(s => s.PageId === x.PageId)) {
              permissionsnotpresentAgreeDisagree.push(x);
            }
          },
          orderSchedulePermission.forEach(x => {
            if (!this.EditOrderScheduleRolePermission.some(s => s.PageId === x.PageId)) {
              permissionsnotpresentOrderSchedule.push(x);
            }
          })
          ),
          )
          );
        this.EditRolePermission = [...this.EditRolePermission, ...permissionsnotpresent];
        this.EditApproveRejectRolePermission = [...this.EditApproveRejectRolePermission, ...permissionsnotpresentApproveReject];
        this.EditAgreeDisagreeRolePermission = [...this.EditAgreeDisagreeRolePermission, ...permissionsnotpresentAgreeDisagree];
        this.EditOrderScheduleRolePermission = [...this.EditOrderScheduleRolePermission, ...permissionsnotpresentOrderSchedule];
        this.editUserPermission = [];
        this.editCodePermission = [];
        this.editStorePermission = [];
        this.editAccountingPermission = [];
        this.editEmployeePermission = [];
        this.editProjectsPermission = [];
        this.editMarketingPermission = [];

        const userp = this.EditRolePermission.filter(
          z => z.ModuleId === applicationModule.Users
        );
        const codep = this.EditRolePermission.filter(
          z => z.ModuleId === applicationModule.Code
        );
        const hrp = this.EditRolePermission.filter(
          z => z.ModuleId === applicationModule.HR
        );
        const storep = this.EditRolePermission.filter(
          z => z.ModuleId === applicationModule.Store
        );
        const accountingp = this.EditRolePermission.filter(
          z =>
            z.ModuleId === applicationModule.Accounting ||
            z.ModuleId === applicationModule.AccountingNew
        );
        const marketingp = this.EditRolePermission.filter(
          z => z.ModuleId === applicationModule.Marketing
        );
        const projectsp = this.EditRolePermission.filter(
          z => z.ModuleId === applicationModule.Projects
        );

        userp.forEach(y => {
          this.editUserPermission.push({
            PageId: y.PageId,
            PageName: y.PageName,
            Edit: y.Edit,
            View: y.View,
            ModuleName: y.ModuleName,
            ModuleId: y.ModuleId
          });
        });

        codep.forEach(y => {
          this.editCodePermission.push({
            PageId: y.PageId,
            PageName: y.PageName,
            Edit: y.Edit,
            View: y.View,
            ModuleName: y.ModuleName,
            ModuleId: y.ModuleId
          });
        });

        storep.forEach(y => {
            this.editStorePermission.push({
              PageId: y.PageId,
              PageName: y.PageName,
              Edit: y.Edit,
              View: y.View,
              ModuleName: y.ModuleName,
              ModuleId: y.ModuleId
            });
          });

          hrp.forEach(y => {
            this.editEmployeePermission.push({
              PageId: y.PageId,
              PageName: y.PageName,
              Edit: y.Edit,
              View: y.View,
              ModuleName: y.ModuleName,
              ModuleId: y.ModuleId
            });
          });

          accountingp.forEach(y => {
            this.editAccountingPermission.push({
              PageId: y.PageId,
              PageName: y.PageName,
              Edit: y.Edit,
              View: y.View,
              ModuleName: y.ModuleName,
              ModuleId: y.ModuleId
            });
          });

          marketingp.forEach(y => {
            this.editMarketingPermission.push({
              PageId: y.PageId,
              PageName: y.PageName,
              Edit: y.Edit,
              View: y.View,
              ModuleName: y.ModuleName,
              ModuleId: y.ModuleId
            });
          });

          projectsp.forEach(y => {
            this.editProjectsPermission.push({
              PageId: y.PageId,
              PageName: y.PageName,
              Edit: y.Edit,
              View: y.View,
              ModuleName: y.ModuleName,
              ModuleId: y.ModuleId
            });
          });
          this.loadingPermission = false;
      }
      ,
      error => {
        this.loadingPermission = false;
      });
  }
}

onRolesPermissionUpdate() {
  this.loadingPermission = true;

  const filteredRolePermissions: any = {
    RoleId: '',
    RoleName: '',
    Permissions: []
  };

  this.editUserPermission.forEach(element => {
    if (element.Edit || element.View) {
      filteredRolePermissions.Permissions.push({
        PageId: element.PageId,
        PageName: element.PageName,
        ModuleName: element.ModuleName,
        ModuleId: element.ModuleId,
        Edit: element.Edit,
        View: element.View
      });
    }
  });

  this.editCodePermission.forEach(element => {
    if (element.Edit || element.View) {
      filteredRolePermissions.Permissions.push({
        PageId: element.PageId,
        PageName: element.PageName,
        ModuleName: element.ModuleName,
        ModuleId: element.ModuleId,
        Edit: element.Edit,
        View: element.View
      });
    }
  });

  this.editAccountingPermission.forEach(element => {
    if (element.Edit || element.View) {
      filteredRolePermissions.Permissions.push({
        PageId: element.PageId,
        PageName: element.PageName,
        ModuleName: element.ModuleName,
        ModuleId: element.ModuleId,
        Edit: element.Edit,
        View: element.View
      });
    }
  });

  this.editStorePermission.forEach(element => {
    if (element.Edit || element.View) {
      filteredRolePermissions.Permissions.push({
        PageId: element.PageId,
        PageName: element.PageName,
        ModuleName: element.ModuleName,
        ModuleId: element.ModuleId,
        Edit: element.Edit,
        View: element.View
      });
    }
  });

  this.editEmployeePermission.forEach(element => {
    if (element.Edit || element.View) {
      filteredRolePermissions.Permissions.push({
        PageId: element.PageId,
        PageName: element.PageName,
        ModuleName: element.ModuleName,
        ModuleId: element.ModuleId,
        Edit: element.Edit,
        View: element.View
      });
    }
  });

  this.editProjectsPermission.forEach(element => {
    if (element.Edit || element.View) {
      filteredRolePermissions.Permissions.push({
        PageId: element.PageId,
        PageName: element.PageName,
        ModuleName: element.ModuleName,
        ModuleId: element.ModuleId,
        Edit: element.Edit,
        View: element.View
      });
    }
  });

  this.editMarketingPermission.forEach(element => {
    if (element.Edit || element.View) {
      filteredRolePermissions.Permissions.push({
        PageId: element.PageId,
        PageName: element.PageName,
        ModuleName: element.ModuleName,
        ModuleId: element.ModuleId,
        Edit: element.Edit,
        View: element.View
      });
  }
});

this.EditApproveRejectRolePermission.forEach(element => {

  if (element.Approve || element.Reject) {
    filteredRolePermissions.Permissions.push({PageId: element.PageId, PageName: element.PageName,
     Approve: element.Approve, Reject: element.Reject, ModuleId: applicationModule.Marketing,
     ModuleName: 'Marketing'} );
  }
});

this.EditAgreeDisagreeRolePermission.forEach(element => {

  if (element.Agree || element.Disagree) {
    filteredRolePermissions.Permissions.push({PageId: element.PageId, PageName: element.PageName,
     Agree: element.Agree, Disagree: element.Disagree, ModuleId: applicationModule.Marketing,
    ModuleName: 'Marketing'} );
  }
});

this.EditOrderScheduleRolePermission.forEach(element => {

  if (element.OrderSchedule) {
    filteredRolePermissions.Permissions.push({PageId: element.PageId, PageName: element.PageName,
      OrderSchedule: element.OrderSchedule, ModuleId: applicationModule.Marketing,
    ModuleName: 'Marketing'} );
  }
});

  if (
    this.ddnSelectedRole.id !== undefined &&
    this.ddnSelectedRole.id != null &&
    this.ddnSelectedRole.id !== '' &&
    this.ddnSelectedRole.name !== undefined &&
    this.ddnSelectedRole.name !== '' &&
    this.ddnSelectedRole.name != null &&
    filteredRolePermissions.Permissions.length > 0
  ) {
    filteredRolePermissions.RoleId = this.ddnSelectedRole.id;
    filteredRolePermissions.RoleName = this.ddnSelectedRole.name;
    this.userService
      .updateRolePermissions(
        this.setting.getBaseUrl() +
          GLOBAL.API_Permissions_UpdatePermissionsOnSelectedRole,
        filteredRolePermissions
      )
      .subscribe(data => {
        this.editRole = false;
        this.toastr.success('Permissions Updated Successfully!!!');
        this.loadingPermission = false;
        this.modalPermission.hide();
        this.getUserRoles();
      });
  } else {
    this.loadingPermission = false;
    this.toastr.warning('Role Name Or Permissions Not Set!!!');
  }
}

  EditRole() {
    this.editRole = !this.editRole;
  }

  ngOnDestroy(): void {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }
}

// tslint:disable-next-line:class-name
export interface iPermissions {
 RoleName: string;
 Permissions: any[];
}

// tslint:disable-next-line:class-name
export interface rolePermissions {
  Edit: boolean;
  ModuleId: number;
  ModuleName: string;
  PageId: number;
  PageName: string;
  View: boolean;
  Approve: boolean;
  Reject: boolean;
  Agree: boolean;
  Disagree: boolean;
}

export interface ApproveRejectRolePermission {
  Approve: boolean;
  RoleId: number;
  Reject: boolean;
  PageId: number;
  Id: number;
  PageName: string;
}

export interface AgreeDisagreeRolePermission {
  Agree: boolean;
  RoleId: number;
  Disagree: boolean;
  PageId: number;
  Id: number;
  PageName: string;
}

export interface OrderSchedulePermission {
  OrderSchedule: boolean;
  RoleId: number;
  PageId: number;
  Id: number;
  PageName: string;
}
