import { TestBed } from '@angular/core/testing';

import { EmployeePensionService } from './employee-pension.service';

describe('EmployeePensionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeePensionService = TestBed.get(EmployeePensionService);
    expect(service).toBeTruthy();
  });
});
