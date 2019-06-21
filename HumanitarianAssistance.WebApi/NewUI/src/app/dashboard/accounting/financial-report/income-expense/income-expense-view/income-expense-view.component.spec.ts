import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeExpenseViewComponent } from './Income-expense-view.component';

describe('IcomeExpenseViewComponent', () => {
  let component: IncomeExpenseViewComponent;
  let fixture: ComponentFixture<IncomeExpenseViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IncomeExpenseViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IncomeExpenseViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
