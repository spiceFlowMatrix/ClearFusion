import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseFiltersComponent } from './purchase-filters.component';

describe('PurchaseFiltersComponent', () => {
  let component: PurchaseFiltersComponent;
  let fixture: ComponentFixture<PurchaseFiltersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PurchaseFiltersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PurchaseFiltersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
