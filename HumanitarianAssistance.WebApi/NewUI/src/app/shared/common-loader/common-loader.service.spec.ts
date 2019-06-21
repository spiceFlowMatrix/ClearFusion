import { TestBed } from '@angular/core/testing';

import { CommonLoaderService } from './common-loader.service';

describe('CommonLoaderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CommonLoaderService = TestBed.get(CommonLoaderService);
    expect(service).toBeTruthy();
  });
});
