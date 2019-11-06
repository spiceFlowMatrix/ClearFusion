import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddLogisticItemsComponent } from './add-logistic-items.component';

describe('AddLogisticItemsComponent', () => {
  let component: AddLogisticItemsComponent;
  let fixture: ComponentFixture<AddLogisticItemsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddLogisticItemsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddLogisticItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
