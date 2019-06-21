import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectActivityAddComponent } from './project-activity-add.component';

describe('ProjectActivityAddComponent', () => {
  let component: ProjectActivityAddComponent;
  let fixture: ComponentFixture<ProjectActivityAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectActivityAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectActivityAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
