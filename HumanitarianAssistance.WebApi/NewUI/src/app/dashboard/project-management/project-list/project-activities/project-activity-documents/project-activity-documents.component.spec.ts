import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectActivityDocumentsComponent } from './project-activity-documents.component';

describe('ProjectActivityDocumentsComponent', () => {
  let component: ProjectActivityDocumentsComponent;
  let fixture: ComponentFixture<ProjectActivityDocumentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectActivityDocumentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectActivityDocumentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
