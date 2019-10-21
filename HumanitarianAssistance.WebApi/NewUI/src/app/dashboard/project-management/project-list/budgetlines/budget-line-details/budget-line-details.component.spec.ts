import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetLineDetailsComponent } from './budget-line-details.component';

describe('BudgetLineDetailsComponent', () => {
  let component: BudgetLineDetailsComponent;
  let fixture: ComponentFixture<BudgetLineDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BudgetLineDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BudgetLineDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
