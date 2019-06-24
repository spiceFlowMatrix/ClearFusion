import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetlinesComponent } from './budgetlines.component';

describe('BudgetlinesComponent', () => {
  let component: BudgetlinesComponent;
  let fixture: ComponentFixture<BudgetlinesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BudgetlinesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BudgetlinesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
