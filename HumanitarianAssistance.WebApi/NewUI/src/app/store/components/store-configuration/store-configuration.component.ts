import { Component, OnInit, ViewChild, TemplateRef, HostListener } from '@angular/core';
import { MatDialog } from '@angular/material';
import { of, Observable, forkJoin } from 'rxjs';
import { TableActionsModel } from 'projects/library/src/public_api';
import { ConfigService } from '../../services/config.service';
import { UnitType, SourceCodeType, SourceCode } from '../../models/store-configuration';
import { FormControl, Validators, FormBuilder, FormGroup } from '@angular/forms';
import { concatMap, map } from 'rxjs/operators';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-store-configuration',
  templateUrl: './store-configuration.component.html',
  styleUrls: ['./store-configuration.component.scss']
})
export class StoreConfigurationComponent implements OnInit {
  unitListHeaders$ = of(['Name']);
  sourceCodeHeaders$ = of(['Id', 'SourceCode Id', 'Code', 'Description', 'Address', 'Phone', 'Fax', 'Email Address', 'Guarantor']);

  hideUnitColums: Observable<{ headers?: string[], items?: string[] }>;
  hideSourceCodeColums: Observable<{ headers?: string[], items?: string[] }>;

  unitItems$: Observable<UnitType[]>;
  sourceCodeItems$: Observable<SourceCodeType[]>;
  sourceCodeByType$: Observable<SourceCodeType[]>;

  unitActions: TableActionsModel;

  // typeName: FormControl;
  unitTypeGroup: FormGroup;
  sourCodeForm: FormGroup;
  unitType: UnitType = {};

  sourceCodeTypes: SourceCodeType[] = [];
  sourceCode: SourceCode = {};

  isEditCode = false;
  // screen
  screenHeight: any;
  screenWidth: any;
  scrollStyles: any;


  @ViewChild('unittype') dialogRef: TemplateRef<any>;
  @ViewChild('sourceCode') codeDialogRef: TemplateRef<any>;

  constructor(private dialog: MatDialog,
    private configservice: ConfigService,
    private fb: FormBuilder, private loader: CommonLoaderService,
    private toastr: ToastrService) { }
  //#region "Dynamic Scroll"

  ngOnInit() {
    // this.typeName = new FormControl('', Validators.required);
    this.unitTypeGroup = this.fb.group({
      unitTypeId: [null],
      unitTypeName: ['', [Validators.required]],
      // isDefault: [false]
    });

    this.unitActions = {
      items: {
        edit: true,
        delete: true
      },
      subitems: {

      }
    };
    this.loader.showLoader();
    forkJoin([this.configservice.getUnitType(),
    this.configservice.getAllSourceCodeTypes(),
    this.configservice.getAllStoreSource()]).subscribe(res => {
      this.getAllUnitTypes(res[0]);
      this.getAllSourCodeTypes(res[1]);
      this.getAllsourceCodes(res[2]);
      this.loader.hideLoader();
    });

    this.createSourceCodeForm();
  }
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight;
    this.screenWidth = window.innerWidth;

    this.scrollStyles = {
      'overflow-y': 'auto',
      height: this.screenHeight - 80 + 'px',
      'overflow-x': 'hidden'
    };
  }

  // Unit type configurations start
  getAllUnitTypes(res) {
    this.unitItems$ = of(res.data.PurchaseUnitTypeList).pipe(map(x => x.map(y => ({
      UnitTypeId: y.UnitTypeId,
      UnitTypeName: y.UnitTypeName,
      // IsDefaultText: y.IsDefault ? 'Yes' : 'No',
      // IsDefault: y.IsDefault

    }))));
    this.hideUnitColums = of({ headers: ['Name'], items: ['UnitTypeName'] });

  }
  addUnitType() {
    this.unitTypeGroup.reset();
    this.openUnitType();
  }

  openUnitType() {
    this.dialog.open(this.dialogRef, {
      width: '400px'
    });
  }

  saveUnit() {
    if (this.unitTypeGroup.valid) {
      this.loader.showLoader();
      if (this.unitTypeGroup.value.unitTypeId) {
        // this.unitType.UnitTypeName = this.unitTypeGroup.value.typeName;
        this.configservice.editUnit(this.unitTypeGroup.value).subscribe(res => {
          this.toastr.success('Updated Successfully');
          this.configservice.getUnitType().subscribe(res1 => {
            this.getAllUnitTypes(res1);
            this.loader.hideLoader();
          });
          this.unitType = {};
          this.dialog.closeAll();
        });
      } else {
       // this.unitType.UnitTypeName = this.typeName.value;
       this.unitTypeGroup.value.unitTypeId = 0;
        this.configservice.saveUnit(this.unitTypeGroup.value).subscribe(res => {
          this.toastr.success('Added Successfully');
          this.configservice.getUnitType().subscribe(res1 => {
            this.getAllUnitTypes(res1);
          });
          this.dialog.closeAll();
          this.loader.hideLoader();
        });
      }
      this.unitTypeGroup.reset();
    }
  }
  unitAction(data) {
    if (data.type == 'delete') {
      this.configservice.openDeleteDialog().subscribe(res => {
        if (res) {
          this.unitType = data.item;
          this.configservice.deleteUnit(this.unitType).subscribe(res => {
            this.toastr.success('Deleted Successfully');
            this.configservice.getUnitType().subscribe(res1 => {
              this.getAllUnitTypes(res1);
            });
          });
        }
      });

    }
    if (data.type === 'edit') {
      this.unitTypeGroup.reset();
      // this.unitType = data.item;
      // this.typeName.setValue(this.unitType.UnitTypeName);
      this.unitTypeGroup.patchValue({
        unitTypeId: data.item.UnitTypeId,
        unitTypeName: data.item.UnitTypeName,
        // isDefault: data.item.IsDefault
      });
      this.openUnitType();
    }

  }
  // Unit type configurations ends


  // source code configuration start
  createSourceCodeForm() {
    const contactRegex = /^[0-9]{10,14}$/;
    this.sourCodeForm = this.fb.group({
      sourceCodeId: [''],
      code: [''],
      description: ['', Validators.required],
      address: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern(contactRegex)]],
      fax: ['', [Validators.required]],
      emailAddress: ['', [Validators.required, Validators.email]],
      guarantor: ['', [Validators.required]],
      codeTypeId: ['', [Validators.required]]
    });
  }
  getAllSourCodeTypes(res) {
    this.sourceCodeTypes = res.data.SourceCodeTypelist;
  }
  getAllsourceCodes(res) {
    this.sourceCodeItems$ = of(res);
    this.sourceCodeByType$ = of(res.filter(r => r.CodeTypeId == 1));
    this.hideSourceCodeColums = of({
      headers: ['Code', 'Description', 'Address', 'Phone', 'Fax', 'Email Address', 'Guarantor'],
      items: ['Code', 'Description', 'Address', 'Phone', 'Fax', 'EmailAddress', 'Guarantor']
    });
  }
  codePopUp(codeTypeId) {
    this.configservice.getCodeByType(codeTypeId).subscribe(res => {
      this.isEditCode = false;
      this.sourCodeForm.reset();
      this.sourCodeForm.controls.code.setValue(res.data.StoreSourceCode);
      this.sourCodeForm.controls.codeTypeId.setValue(codeTypeId);
      this.dialog.open(this.codeDialogRef, {
        width: '500px'
      });
    });

  }
  openCodeType(e: SourceCodeType) {
    this.sourceCodeItems$.subscribe(res => {
      this.sourceCodeByType$ = of(res.filter(r => r.CodeTypeId == e.CodeTypeId));
    });
  }
  saveCode() {
    this.sourceCode = {};
    this.sourceCode.Code = this.sourCodeForm.controls.code.value;
    this.sourceCode.Description = this.sourCodeForm.controls.description.value;
    this.sourceCode.Address = this.sourCodeForm.controls.address.value;
    this.sourceCode.Phone = this.sourCodeForm.controls.phone.value;
    this.sourceCode.Fax = this.sourCodeForm.controls.fax.value;
    this.sourceCode.EmailAddress = this.sourCodeForm.controls.emailAddress.value;
    this.sourceCode.Guarantor = this.sourCodeForm.controls.guarantor.value;
    this.sourceCode.CodeTypeId = this.sourCodeForm.controls.codeTypeId.value;
    if (this.isEditCode) {
      this.sourceCode.SourceCodeId = this.sourCodeForm.controls.sourceCodeId.value;
      this.configservice.editCode(this.sourceCode).pipe(
        concatMap(val => {
          this.toastr.success('Updated Successfully');
          return this.configservice.getSourceCodeById(this.sourceCode.CodeTypeId);
        })
      ).subscribe(res => {
        this.sourceCodeByType$ = of(res);
        this.dialog.closeAll();
        this.isEditCode = false;
        this.sourceCode = {};
        this.sourCodeForm.reset();
      });
    } else {
      this.sourceCode.SourceCodeId = 0;
      this.configservice.saveCode(this.sourceCode).pipe(
        concatMap(val => {
          this.toastr.success('Added Successfully');
          return this.configservice.getSourceCodeById(this.sourceCode.CodeTypeId);
        })
      ).subscribe(res => {
        this.sourceCodeByType$ = of(res);
        this.dialog.closeAll();
        this.sourceCode = {};
        this.sourCodeForm.reset();
      });
    }




  }
  codeAction(data) {
    if (data.type == 'delete') {
      this.configservice.openDeleteDialog().subscribe(res => {
        if (res) {
          this.configservice.deleteCode(data.item.SourceCodeId).pipe(
            concatMap(val => {
              this.toastr.success('Deleted Successfully');
              return this.configservice.getSourceCodeById(data.item.CodeTypeId);
            })).subscribe(res => {
              this.sourceCodeByType$ = of(res);
            });
        }
      });

    }
    if (data.type == 'edit') {

      this.sourCodeForm.controls.code.setValue(data.item.Code);
      this.sourCodeForm.controls.description.setValue(data.item.Description);
      this.sourCodeForm.controls.address.setValue(data.item.Address);
      this.sourCodeForm.controls.phone.setValue(data.item.Phone);
      this.sourCodeForm.controls.fax.setValue(data.item.Fax);
      this.sourCodeForm.controls.emailAddress.setValue(data.item.EmailAddress);
      this.sourCodeForm.controls.guarantor.setValue(data.item.Guarantor);
      this.sourCodeForm.controls.codeTypeId.setValue(data.item.CodeTypeId);
      this.sourCodeForm.controls.sourceCodeId.setValue(data.item.SourceCodeId);

      this.isEditCode = true;
      this.dialog.open(this.codeDialogRef, {
        width: '500px'
      });


    }
  }
  // source code configuration ends



}
