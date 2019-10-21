import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetBalanceComponent } from './budget-balance.component';

describe('BudgetBalanceComponent', () => {
  let component: BudgetBalanceComponent;
  let fixture: ComponentFixture<BudgetBalanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BudgetBalanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BudgetBalanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
