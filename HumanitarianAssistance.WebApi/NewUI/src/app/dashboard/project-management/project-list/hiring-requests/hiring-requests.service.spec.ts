import { TestBed } from '@angular/core/testing';

import { HiringRequestsService } from './hiring-requests.service';

describe('HiringRequestsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HiringRequestsService = TestBed.get(HiringRequestsService);
    expect(service).toBeTruthy();
  });
});
