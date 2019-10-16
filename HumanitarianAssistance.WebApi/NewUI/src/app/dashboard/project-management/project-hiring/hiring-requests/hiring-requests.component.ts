import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { HiringList } from '../models/hiring-list';
import { Router } from '@angular/router';

@Component({
  selector: 'app-hiring-requests',
  templateUrl: './hiring-requests.component.html',
  styleUrls: ['./hiring-requests.component.scss']
})
export class HiringRequestsComponent implements OnInit {
  hiringListHeaders$ = of(['Job Code', 'Job Grade', 'Position', 'Total Vacancies', 'Filled Vacancies', 'Pay Currency', 'Pay Rate', 'Status']);
  hiringList$: Observable<HiringList[]>;
  constructor(private router: Router) { }

  ngOnInit() {
    this.hiringList$ = of ([
      {
        JobCode:'1',
        JobGrade: 'A',
        FilledVacancies:'22',
        PayCurrency:'AFG',
        PayRate:'15',
        Position:'Manager',
        Status:'open',
        TotalVacancies:'50'
      },
      {
        JobCode:'1',
        JobGrade: 'A',
        FilledVacancies:'22',
        PayCurrency:'AFG',
        PayRate:'15',
        Position:'Manager',
        Status:'open',
        TotalVacancies:'50'
      },
      {
        JobCode:'1',
        JobGrade: 'A',
        FilledVacancies:'22',
        PayCurrency:'AFG',
        PayRate:'15',
        Position:'Manager',
        Status:'open',
        TotalVacancies:'50'
      },
      {
        JobCode:'1',
        JobGrade: 'A',
        FilledVacancies:'22',
        PayCurrency:'AFG',
        PayRate:'15',
        Position:'Manager',
        Status:'open',
        TotalVacancies:'50'
      }
      
    ] as HiringList[])
  }
  jobDetail(){
  this.router.navigate(['/job-detail'])
  }

}
