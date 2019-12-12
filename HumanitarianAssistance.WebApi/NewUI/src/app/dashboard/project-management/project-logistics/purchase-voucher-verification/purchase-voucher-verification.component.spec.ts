import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseVoucherVerificationComponent } from './purchase-voucher-verification.component';

describe('PurchaseVoucherVerificationComponent', () => {
  let component: PurchaseVoucherVerificationComponent;
  let fixture: ComponentFixture<PurchaseVoucherVerificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PurchaseVoucherVerificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PurchaseVoucherVerificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
