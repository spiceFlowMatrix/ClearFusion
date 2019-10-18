import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LogisticsControlComponent } from './logistics-control.component';

describe('LogisticsControlComponent', () => {
  let component: LogisticsControlComponent;
  let fixture: ComponentFixture<LogisticsControlComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LogisticsControlComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LogisticsControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
