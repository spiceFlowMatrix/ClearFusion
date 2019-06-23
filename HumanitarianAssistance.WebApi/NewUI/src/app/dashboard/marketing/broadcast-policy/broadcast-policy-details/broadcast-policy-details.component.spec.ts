import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BroadcastPolicyDetailsComponent } from './broadcast-policy-details.component';

describe('BroadcastPolicyDetailsComponent', () => {
  let component: BroadcastPolicyDetailsComponent;
  let fixture: ComponentFixture<BroadcastPolicyDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BroadcastPolicyDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BroadcastPolicyDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
