import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GainLossReportComponent } from './gain-loss-report.component';

describe('GainLossReportComponent', () => {
  let component: GainLossReportComponent;
  let fixture: ComponentFixture<GainLossReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GainLossReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GainLossReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
