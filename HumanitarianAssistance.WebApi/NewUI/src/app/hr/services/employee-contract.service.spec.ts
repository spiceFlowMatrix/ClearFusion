import { TestBed } from '@angular/core/testing';

import { EmployeeContractService } from './employee-contract.service';

describe('EmployeeContractService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeContractService = TestBed.get(EmployeeContractService);
    expect(service).toBeTruthy();
  });
});
