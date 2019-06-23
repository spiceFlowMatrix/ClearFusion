import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HiringRequestsListingComponent } from './hiring-requests-listing.component';

describe('HiringRequestsListingComponent', () => {
  let component: HiringRequestsListingComponent;
  let fixture: ComponentFixture<HiringRequestsListingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HiringRequestsListingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HiringRequestsListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
