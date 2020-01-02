import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExchangeGainLossReportComponent } from './exchange-gain-loss-report.component';

describe('ExchangeGainLossReportComponent', () => {
  let component: ExchangeGainLossReportComponent;
  let fixture: ComponentFixture<ExchangeGainLossReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExchangeGainLossReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExchangeGainLossReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
