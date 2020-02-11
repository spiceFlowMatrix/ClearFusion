import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeAnalyticalComponent } from './employee-analytical.component';

describe('EmployeeAnalyticalComponent', () => {
  let component: EmployeeAnalyticalComponent;
  let fixture: ComponentFixture<EmployeeAnalyticalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeAnalyticalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeAnalyticalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
