import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PmuComponent } from './pmu.component';

describe('PmuComponent', () => {
  let component: PmuComponent;
  let fixture: ComponentFixture<PmuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PmuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PmuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
