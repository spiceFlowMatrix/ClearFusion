import { TestBed } from '@angular/core/testing';

import { BudgetLineService } from './budget-line.service';

describe('BudgetLineService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BudgetLineService = TestBed.get(BudgetLineService);
    expect(service).toBeTruthy();
  });
});
