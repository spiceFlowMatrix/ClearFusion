import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendanceGroupMasterComponent } from './attendance-group-master.component';

describe('AttendanceGroupMasterComponent', () => {
  let component: AttendanceGroupMasterComponent;
  let fixture: ComponentFixture<AttendanceGroupMasterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AttendanceGroupMasterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AttendanceGroupMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
