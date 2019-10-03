import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseFiledConfigComponent } from './purchase-filed-config.component';

describe('PurchaseFiledConfigComponent', () => {
  let component: PurchaseFiledConfigComponent;
  let fixture: ComponentFixture<PurchaseFiledConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PurchaseFiledConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PurchaseFiledConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
