import { TestBed } from '@angular/core/testing';

import { ChartOfAccountsPdfService } from './chart-of-accounts-pdf.service';

describe('ChartOfAccountsPdfService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ChartOfAccountsPdfService = TestBed.get(ChartOfAccountsPdfService);
    expect(service).toBeTruthy();
  });
});
