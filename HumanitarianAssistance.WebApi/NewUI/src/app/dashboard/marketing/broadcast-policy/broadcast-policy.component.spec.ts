import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BroadcastPolicyComponent } from './broadcast-policy.component';

describe('BroadcastPolicyComponent', () => {
  let component: BroadcastPolicyComponent;
  let fixture: ComponentFixture<BroadcastPolicyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BroadcastPolicyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BroadcastPolicyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
