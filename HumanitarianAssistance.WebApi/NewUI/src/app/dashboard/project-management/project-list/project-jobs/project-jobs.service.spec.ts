import { TestBed } from '@angular/core/testing';

import { ProjectJobsService } from './project-jobs.service';

describe('ProjectJobsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectJobsService = TestBed.get(ProjectJobsService);
    expect(service).toBeTruthy();
  });
});
