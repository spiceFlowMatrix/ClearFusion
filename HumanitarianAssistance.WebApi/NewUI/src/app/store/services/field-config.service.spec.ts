import { TestBed } from '@angular/core/testing';

import { FieldConfigService } from './field-config.service';

describe('FieldConfigService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FieldConfigService = TestBed.get(FieldConfigService);
    expect(service).toBeTruthy();
  });
});
