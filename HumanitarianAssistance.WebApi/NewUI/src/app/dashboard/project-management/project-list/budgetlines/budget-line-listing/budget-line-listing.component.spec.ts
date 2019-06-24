import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetLineListingComponent } from './budget-line-listing.component';

describe('BudgetLineListingComponent', () => {
  let component: BudgetLineListingComponent;
  let fixture: ComponentFixture<BudgetLineListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BudgetLineListingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BudgetLineListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
