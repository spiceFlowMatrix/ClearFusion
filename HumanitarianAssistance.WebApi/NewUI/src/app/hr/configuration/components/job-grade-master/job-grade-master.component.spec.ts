import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JobGradeMasterComponent } from './job-grade-master.component';

describe('JobGradeMasterComponent', () => {
  let component: JobGradeMasterComponent;
  let fixture: ComponentFixture<JobGradeMasterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JobGradeMasterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JobGradeMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
