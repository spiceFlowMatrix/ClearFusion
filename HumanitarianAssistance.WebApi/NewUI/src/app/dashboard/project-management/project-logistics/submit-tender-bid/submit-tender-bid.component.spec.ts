import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitTenderBidComponent } from './submit-tender-bid.component';

describe('SubmitTenderBidComponent', () => {
  let component: SubmitTenderBidComponent;
  let fixture: ComponentFixture<SubmitTenderBidComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubmitTenderBidComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmitTenderBidComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
