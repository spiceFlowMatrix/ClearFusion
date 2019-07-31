import { TestBed } from '@angular/core/testing';

import { ProjectOtherDetailPdfService } from './project-other-detail-pdf.service';

describe('ProjectOtherDetailPdfService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProjectOtherDetailPdfService = TestBed.get(ProjectOtherDetailPdfService);
    expect(service).toBeTruthy();
  });
});
