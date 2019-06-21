import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitRateComponent } from './unit-rate.component';

describe('UnitRateComponent', () => {
  let component: UnitRateComponent;
  let fixture: ComponentFixture<UnitRateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnitRateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnitRateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
