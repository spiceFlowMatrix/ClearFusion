import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectIndicatorDetailComponent } from './project-indicator-detail.component';

describe('ProjectIndicatorDetailComponent', () => {
  let component: ProjectIndicatorDetailComponent;
  let fixture: ComponentFixture<ProjectIndicatorDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectIndicatorDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectIndicatorDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
