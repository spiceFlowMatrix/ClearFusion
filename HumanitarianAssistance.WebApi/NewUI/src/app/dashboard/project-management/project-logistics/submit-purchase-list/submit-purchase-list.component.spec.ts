import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitPurchaseListComponent } from './submit-purchase-list.component';

describe('SubmitPurchaseListComponent', () => {
  let component: SubmitPurchaseListComponent;
  let fixture: ComponentFixture<SubmitPurchaseListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubmitPurchaseListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmitPurchaseListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
