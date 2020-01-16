import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeLeaveAddComponent } from './employee-leave-add.component';

describe('EmployeeLeaveAddComponent', () => {
  let component: EmployeeLeaveAddComponent;
  let fixture: ComponentFixture<EmployeeLeaveAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeLeaveAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeLeaveAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
