import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeneficiariesMonitoringComponent } from './beneficiaries-monitoring.component';

describe('BeneficiariesMonitoringComponent', () => {
  let component: BeneficiariesMonitoringComponent;
  let fixture: ComponentFixture<BeneficiariesMonitoringComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BeneficiariesMonitoringComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeneficiariesMonitoringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
