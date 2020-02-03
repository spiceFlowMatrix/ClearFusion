import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SetEmployeeAttendanceComponent } from './set-employee-attendance.component';

describe('SetEmployeeAttendanceComponent', () => {
  let component: SetEmployeeAttendanceComponent;
  let fixture: ComponentFixture<SetEmployeeAttendanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SetEmployeeAttendanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetEmployeeAttendanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
