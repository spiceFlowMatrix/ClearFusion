import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPayrollHoursComponent } from './add-payroll-hours.component';

describe('AddPayrollHoursComponent', () => {
  let component: AddPayrollHoursComponent;
  let fixture: ComponentFixture<AddPayrollHoursComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddPayrollHoursComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPayrollHoursComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
