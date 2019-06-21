import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VoucherSummaryFilterComponent } from './voucher-summary-filter.component';

describe('VoucherSummaryFilterComponent', () => {
  let component: VoucherSummaryFilterComponent;
  let fixture: ComponentFixture<VoucherSummaryFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VoucherSummaryFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VoucherSummaryFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
