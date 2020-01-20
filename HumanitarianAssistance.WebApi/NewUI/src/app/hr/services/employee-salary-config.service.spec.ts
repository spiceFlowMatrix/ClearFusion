import { TestBed } from '@angular/core/testing';

import { EmployeeSalaryConfigService } from './employee-salary-config.service';

describe('EmployeeSalaryConfigService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeSalaryConfigService = TestBed.get(EmployeeSalaryConfigService);
    expect(service).toBeTruthy();
  });
});
