import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExchangeRateListingComponent } from './exchange-rate-listing.component';

describe('ExchangeRateListingComponent', () => {
  let component: ExchangeRateListingComponent;
  let fixture: ComponentFixture<ExchangeRateListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExchangeRateListingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExchangeRateListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
