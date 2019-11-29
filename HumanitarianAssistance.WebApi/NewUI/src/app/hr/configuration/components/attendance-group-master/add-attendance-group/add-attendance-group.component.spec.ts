import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAttendanceGroupComponent } from './add-attendance-group.component';

describe('AddAttendanceGroupComponent', () => {
  let component: AddAttendanceGroupComponent;
  let fixture: ComponentFixture<AddAttendanceGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddAttendanceGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAttendanceGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
