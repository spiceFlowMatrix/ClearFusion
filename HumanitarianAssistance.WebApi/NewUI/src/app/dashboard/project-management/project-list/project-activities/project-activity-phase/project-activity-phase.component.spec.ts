import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectActivityPhaseComponent } from './project-activity-phase.component';

describe('ProjectActivityPhaseComponent', () => {
  let component: ProjectActivityPhaseComponent;
  let fixture: ComponentFixture<ProjectActivityPhaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectActivityPhaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectActivityPhaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
