import { TestBed } from '@angular/core/testing';

import { BroadcastPolicyService } from './broadcast-policy.service';

describe('BroadcastPolicyService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BroadcastPolicyService = TestBed.get(BroadcastPolicyService);
    expect(service).toBeTruthy();
  });
});
