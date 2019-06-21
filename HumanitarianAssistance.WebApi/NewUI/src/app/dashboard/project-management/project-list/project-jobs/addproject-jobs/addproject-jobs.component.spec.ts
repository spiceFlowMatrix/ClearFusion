import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddprojectJobsComponent } from './addproject-jobs.component';

describe('AddprojectJobsComponent', () => {
  let component: AddprojectJobsComponent;
  let fixture: ComponentFixture<AddprojectJobsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddprojectJobsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddprojectJobsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
