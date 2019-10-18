import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectActivityListingComponent } from './project-activity-listing.component';

describe('ProjectActivityListingComponent', () => {
  let component: ProjectActivityListingComponent;
  let fixture: ComponentFixture<ProjectActivityListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectActivityListingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectActivityListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
