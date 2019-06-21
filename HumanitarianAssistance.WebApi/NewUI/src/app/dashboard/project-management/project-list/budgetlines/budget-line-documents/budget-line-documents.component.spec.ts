import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetLineDocumentsComponent } from './budget-line-documents.component';

describe('BudgetLineDocumentsComponent', () => {
  let component: BudgetLineDocumentsComponent;
  let fixture: ComponentFixture<BudgetLineDocumentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BudgetLineDocumentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BudgetLineDocumentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
