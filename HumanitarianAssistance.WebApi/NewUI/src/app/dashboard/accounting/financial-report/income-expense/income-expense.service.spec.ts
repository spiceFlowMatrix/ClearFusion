import { TestBed } from '@angular/core/testing';

import { IncomeExpenseService } from './income-expense.service';

describe('IncomeExpenseService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: IncomeExpenseService = TestBed.get(IncomeExpenseService);
    expect(service).toBeTruthy();
  });
});
