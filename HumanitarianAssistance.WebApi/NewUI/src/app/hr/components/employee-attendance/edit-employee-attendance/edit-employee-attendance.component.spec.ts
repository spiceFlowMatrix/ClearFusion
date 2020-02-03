import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEmployeeAttendanceComponent } from './edit-employee-attendance.component';

describe('EditEmployeeAttendanceComponent', () => {
  let component: EditEmployeeAttendanceComponent;
  let fixture: ComponentFixture<EditEmployeeAttendanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditEmployeeAttendanceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditEmployeeAttendanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
