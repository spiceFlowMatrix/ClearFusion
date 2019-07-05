import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetLineImportPopupLoaderComponent } from './budget-line-import-popup-loader.component';

describe('BudgetLineImportPopupLoaderComponent', () => {
  let component: BudgetLineImportPopupLoaderComponent;
  let fixture: ComponentFixture<BudgetLineImportPopupLoaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BudgetLineImportPopupLoaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BudgetLineImportPopupLoaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
