import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProposalReportFilterComponent } from './proposal-report-filter.component';

describe('ProposalReportFilterComponent', () => {
  let component: ProposalReportFilterComponent;
  let fixture: ComponentFixture<ProposalReportFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProposalReportFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProposalReportFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
