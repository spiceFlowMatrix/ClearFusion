import { TestBed } from '@angular/core/testing';

import { ProjectIndicatorService } from './project-indicator.service';

describe('ProjectIndicatorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectIndicatorService = TestBed.get(ProjectIndicatorService);
    expect(service).toBeTruthy();
  });
});
