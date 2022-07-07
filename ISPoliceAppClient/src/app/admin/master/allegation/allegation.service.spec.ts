import { TestBed } from '@angular/core/testing';

import { AllegationService } from './allegation.service';

describe('AllegationService', () => {
  let service: AllegationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AllegationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
