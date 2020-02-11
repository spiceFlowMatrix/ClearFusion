import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministerPayrollComponent } from './administer-payroll.component';

describe('AdministerPayrollComponent', () => {
  let component: AdministerPayrollComponent;
  let fixture: ComponentFixture<AdministerPayrollComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministerPayrollComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministerPayrollComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
