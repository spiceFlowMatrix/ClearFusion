import { Component, OnInit } from '@angular/core';
import { of, Observable } from 'rxjs';
import { HiringList } from '../models/hiring-list';

@Component({
  selector: 'app-request-detail',
  templateUrl: './request-detail.component.html',
  styleUrls: ['./request-detail.component.scss']
})
export class RequestDetailComponent implements OnInit {
  newCandidatesHeaders$ = of(['Employee Code', 'Full Name', 'Gender', 'Interview', 'Candidate Status']);
  newCandidatesList$: Observable<HiringList[]>;
  constructor() { }

  ngOnInit() {
    this.newCandidatesList$ = of ([
      {
        JobCode:'E1534',
        JobGrade: 'Employee Name',
        FilledVacancies:'Male',
        PayCurrency:'<a>Interview Id</a>',
        PayRate:'<p style="color: #bcbc1e";>pending interview</p>',
      }
   
      
    ] as HiringList[])
  }

}
