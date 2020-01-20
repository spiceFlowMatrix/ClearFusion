import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeSalaryConfigComponent } from './employee-salary-config.component';

describe('EmployeeSalaryConfigComponent', () => {
  let component: EmployeeSalaryConfigComponent;
  let fixture: ComponentFixture<EmployeeSalaryConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeSalaryConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeSalaryConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
