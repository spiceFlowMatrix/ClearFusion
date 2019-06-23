import { TestBed, inject } from '@angular/core/testing';

import { PmuService } from './pmu.service';

describe('PmuService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PmuService]
    });
  });

  it('should be created', inject([PmuService], (service: PmuService) => {
    expect(service).toBeTruthy();
  }));
});
