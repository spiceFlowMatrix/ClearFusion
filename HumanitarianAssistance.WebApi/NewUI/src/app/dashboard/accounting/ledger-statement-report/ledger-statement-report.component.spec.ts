import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LedgerStatementReportComponent } from './ledger-statement-report.component';

describe('LedgerStatementReportComponent', () => {
  let component: LedgerStatementReportComponent;
  let fixture: ComponentFixture<LedgerStatementReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LedgerStatementReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LedgerStatementReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
