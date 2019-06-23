import { TestBed } from '@angular/core/testing';

import { ProjectCashFlowService } from './project-cash-flow.service';

describe('ProjectCashFlowService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectCashFlowService = TestBed.get(ProjectCashFlowService);
    expect(service).toBeTruthy();
  });
});
