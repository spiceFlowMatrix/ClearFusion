import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseFinalCostComponent } from './purchase-final-cost.component';

describe('PurchaseFinalCostComponent', () => {
  let component: PurchaseFinalCostComponent;
  let fixture: ComponentFixture<PurchaseFinalCostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PurchaseFinalCostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PurchaseFinalCostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
