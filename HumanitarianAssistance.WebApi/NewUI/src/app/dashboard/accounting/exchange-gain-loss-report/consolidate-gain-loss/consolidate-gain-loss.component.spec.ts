import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsolidateGainLossComponent } from './consolidate-gain-loss.component';

describe('ConsolidateGainLossComponent', () => {
  let component: ConsolidateGainLossComponent;
  let fixture: ComponentFixture<ConsolidateGainLossComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsolidateGainLossComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsolidateGainLossComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
