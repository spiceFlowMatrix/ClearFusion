import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VoucherSummaryReportComponent } from './voucher-summary-report.component';

describe('VoucherSummaryReportComponent', () => {
  let component: VoucherSummaryReportComponent;
  let fixture: ComponentFixture<VoucherSummaryReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VoucherSummaryReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VoucherSummaryReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
