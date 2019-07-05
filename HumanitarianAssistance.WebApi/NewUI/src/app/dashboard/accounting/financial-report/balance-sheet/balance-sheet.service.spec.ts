import { TestBed } from '@angular/core/testing';

import { BalanceSheetService } from './balance-sheet.service';

describe('BalanceSheetService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BalanceSheetService = TestBed.get(BalanceSheetService);
    expect(service).toBeTruthy();
  });
});
