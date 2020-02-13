import { Component, OnInit } from '@angular/core';
import { HrService } from '../../services/hr.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employee-analytical',
  templateUrl: './employee-analytical.component.html',
  styleUrls: ['./employee-analytical.component.scss']
})
export class EmployeeAnalyticalComponent implements OnInit {
  employeeId: number;
  analyticalInfo: EmployeeAnalyticalInfo[] = [];
  constructor(private hrService: HrService, private routeActive: ActivatedRoute, ) { }

  ngOnInit() {
    this.routeActive.params.subscribe(params => {
      this.employeeId = +params['id'];
      this.getAnalyticalInfo();
    });
  }
  getAnalyticalInfo() {
    this.hrService.GetAllEmployeeSalaryAnalyticalInfo(this.employeeId).subscribe(res => {
      this.analyticalInfo = res.data.EmployeeSalaryAnalyticalInfoList;
    });
  }
}

interface EmployeeAnalyticalInfo {
  AccountCode?: string;
  BudgetLineId?: string;
  EmployeeID?: string;
  ProjectId?: string;
  SalaryPercentage?: string;
  BudgetLineName?: string;
  ProjectName?: string;
  AccountName?: string;
}
