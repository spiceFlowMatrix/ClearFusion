import { TestBed } from '@angular/core/testing';

import { MasterPageServiceService } from './master-page-service.service';

describe('MasterPageServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MasterPageServiceService = TestBed.get(MasterPageServiceService);
    expect(service).toBeTruthy();
  });
});
