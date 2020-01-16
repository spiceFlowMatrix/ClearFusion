import { IDropDownModel } from './../../../store/models/purchase';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CommonLoaderService } from 'src/app/shared/common-loader/common-loader.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-employee-pension',
  templateUrl: './employee-pension.component.html',
  styleUrls: ['./employee-pension.component.scss']
})
export class EmployeePensionComponent implements OnInit {
  pensionListHeaders$ = of(['Date', 'Gross Salary', 'Pension Rate', 'Pension Deducation','Profit','Total']);
  taxListHeaders$ = of(['Date', 'Currency', 'Office', 'Total Tax']);
  pensionList$: Observable<any[]>;
  taxList$: Observable<any[]>;
  currencyList$: Observable<IDropDownModel[]>;
  financialYearList$: Observable<IDropDownModel[]>;
  constructor(
    private toastr: ToastrService,
    private commonLoader: CommonLoaderService
  ) {
    this.currencyList$ = of([
      { name: '1st', value: 1 },
      { name: '2nd', value: 2 },
    ] as IDropDownModel[]);
    this.financialYearList$ = of([
      { name: '1st', value: 1 },
      { name: '2nd', value: 2 },
    ] as IDropDownModel[]);
  }

  ngOnInit() {
  }

}
