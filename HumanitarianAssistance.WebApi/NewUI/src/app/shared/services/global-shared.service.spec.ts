import { TestBed } from '@angular/core/testing';

import { GlobalSharedService } from './global-shared.service';

describe('GlobalSharedService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GlobalSharedService = TestBed.get(GlobalSharedService);
    expect(service).toBeTruthy();
  });
});
