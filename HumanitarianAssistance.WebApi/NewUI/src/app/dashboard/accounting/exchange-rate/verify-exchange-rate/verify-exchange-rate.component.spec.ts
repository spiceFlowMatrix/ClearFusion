import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VerifyExchangeRateComponent } from './verify-exchange-rate.component';

describe('VerifyExchangeRateComponent', () => {
  let component: VerifyExchangeRateComponent;
  let fixture: ComponentFixture<VerifyExchangeRateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VerifyExchangeRateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerifyExchangeRateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
