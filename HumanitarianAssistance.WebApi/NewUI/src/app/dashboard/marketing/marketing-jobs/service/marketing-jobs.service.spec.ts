import { TestBed } from '@angular/core/testing';

import { MarketingJobsService } from './marketing-jobs.service';

describe('MarketingJobsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MarketingJobsService = TestBed.get(MarketingJobsService);
    expect(service).toBeTruthy();
  });
});
