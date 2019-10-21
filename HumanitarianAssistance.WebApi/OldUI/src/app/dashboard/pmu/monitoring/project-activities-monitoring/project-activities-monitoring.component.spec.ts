import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectActivitiesMonitoringComponent } from './project-activities-monitoring.component';

describe('ProjectActivitiesMonitoringComponent', () => {
  let component: ProjectActivitiesMonitoringComponent;
  let fixture: ComponentFixture<ProjectActivitiesMonitoringComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectActivitiesMonitoringComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectActivitiesMonitoringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
