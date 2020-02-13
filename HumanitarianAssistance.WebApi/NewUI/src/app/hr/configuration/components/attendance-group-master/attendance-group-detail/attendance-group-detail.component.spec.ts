import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendanceGroupDetailComponent } from './attendance-group-detail.component';

describe('AttendanceGroupDetailComponent', () => {
  let component: AttendanceGroupDetailComponent;
  let fixture: ComponentFixture<AttendanceGroupDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AttendanceGroupDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AttendanceGroupDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
