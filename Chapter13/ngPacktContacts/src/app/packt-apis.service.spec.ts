import { TestBed, inject } from '@angular/core/testing';

import { PacktAPIsService } from './packt-apis.service';

describe('PacktAPIsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PacktAPIsService]
    });
  });

  it('should be created', inject([PacktAPIsService], (service: PacktAPIsService) => {
    expect(service).toBeTruthy();
  }));
});
