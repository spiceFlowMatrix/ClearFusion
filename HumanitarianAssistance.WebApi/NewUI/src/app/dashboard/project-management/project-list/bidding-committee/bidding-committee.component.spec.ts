import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BiddingCommitteeComponent } from './bidding-committee.component';

describe('BiddingCommitteeComponent', () => {
  let component: BiddingCommitteeComponent;
  let fixture: ComponentFixture<BiddingCommitteeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BiddingCommitteeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BiddingCommitteeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
