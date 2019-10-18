import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketingJobsComponent } from './marketing-jobs.component';

describe('MarketingJobsComponent', () => {
  let component: MarketingJobsComponent;
  let fixture: ComponentFixture<MarketingJobsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarketingJobsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarketingJobsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
