import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AcceptProposalComponent } from './accept-proposal.component';

describe('AcceptProposalComponent', () => {
  let component: AcceptProposalComponent;
  let fixture: ComponentFixture<AcceptProposalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AcceptProposalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AcceptProposalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
