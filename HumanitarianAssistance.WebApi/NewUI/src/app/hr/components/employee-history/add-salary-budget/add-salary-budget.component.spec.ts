import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSalaryBudgetComponent } from './add-salary-budget.component';

describe('AddSalaryBudgetComponent', () => {
  let component: AddSalaryBudgetComponent;
  let fixture: ComponentFixture<AddSalaryBudgetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddSalaryBudgetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddSalaryBudgetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
