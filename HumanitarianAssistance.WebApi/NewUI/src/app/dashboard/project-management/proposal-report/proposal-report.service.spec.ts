import { TestBed } from '@angular/core/testing';

import { ProposalReportService } from './proposal-report.service';

describe('ProposalReportService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProposalReportService = TestBed.get(ProposalReportService);
    expect(service).toBeTruthy();
  });
});
