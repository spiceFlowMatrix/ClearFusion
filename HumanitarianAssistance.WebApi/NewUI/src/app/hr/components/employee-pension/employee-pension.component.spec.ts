import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeePensionComponent } from './employee-pension.component';

describe('EmployeePensionComponent', () => {
  let component: EmployeePensionComponent;
  let fixture: ComponentFixture<EmployeePensionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeePensionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeePensionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
