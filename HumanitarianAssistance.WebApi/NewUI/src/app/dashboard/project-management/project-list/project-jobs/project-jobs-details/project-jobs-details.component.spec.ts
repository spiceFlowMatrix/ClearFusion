import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectJobsDetailsComponent } from './project-jobs-details.component';

describe('ProjectJobsDetailsComponent', () => {
  let component: ProjectJobsDetailsComponent;
  let fixture: ComponentFixture<ProjectJobsDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectJobsDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectJobsDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
