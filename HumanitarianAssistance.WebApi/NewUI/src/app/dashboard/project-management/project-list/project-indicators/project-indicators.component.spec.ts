import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectIndicatorsComponent } from './project-indicators.component';

describe('ProjectIndicatorsComponent', () => {
  let component: ProjectIndicatorsComponent;
  let fixture: ComponentFixture<ProjectIndicatorsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectIndicatorsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectIndicatorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
