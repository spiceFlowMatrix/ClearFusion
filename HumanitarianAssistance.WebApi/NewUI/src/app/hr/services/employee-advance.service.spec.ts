import { TestBed } from '@angular/core/testing';

import { EmployeeAdvanceService } from './employee-advance.service';

describe('EmployeeAdvanceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeAdvanceService = TestBed.get(EmployeeAdvanceService);
    expect(service).toBeTruthy();
  });
});
