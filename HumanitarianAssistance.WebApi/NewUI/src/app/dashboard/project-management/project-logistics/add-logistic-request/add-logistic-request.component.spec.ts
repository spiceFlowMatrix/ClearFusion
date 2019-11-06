import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddLogisticRequestComponent } from './add-logistic-request.component';

describe('AddLogisticRequestComponent', () => {
  let component: AddLogisticRequestComponent;
  let fixture: ComponentFixture<AddLogisticRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddLogisticRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddLogisticRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
