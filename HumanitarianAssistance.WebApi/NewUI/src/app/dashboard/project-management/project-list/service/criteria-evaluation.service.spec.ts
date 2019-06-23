import { TestBed } from '@angular/core/testing';

import { CriteriaEvaluationService } from './criteria-evaluation.service';

describe('CriteriaEvaluationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CriteriaEvaluationService = TestBed.get(CriteriaEvaluationService);
    expect(service).toBeTruthy();
  });
});
