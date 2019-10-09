import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleTrackerComponent } from './vehicle-tracker.component';

describe('VehicleTrackerComponent', () => {
  let component: VehicleTrackerComponent;
  let fixture: ComponentFixture<VehicleTrackerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VehicleTrackerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VehicleTrackerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
