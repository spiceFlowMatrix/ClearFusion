import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectActivityFilteringComponent } from './project-activity-filtering.component';

describe('ProjectActivityFilteringComponent', () => {
  let component: ProjectActivityFilteringComponent;
  let fixture: ComponentFixture<ProjectActivityFilteringComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectActivityFilteringComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectActivityFilteringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
