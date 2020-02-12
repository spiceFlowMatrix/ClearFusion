import { TestBed } from '@angular/core/testing';

import { EmployeeAppraisalService } from './employee-appraisal.service';

describe('EmployeeAppraisalService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeAppraisalService = TestBed.get(EmployeeAppraisalService);
    expect(service).toBeTruthy();
  });
});
