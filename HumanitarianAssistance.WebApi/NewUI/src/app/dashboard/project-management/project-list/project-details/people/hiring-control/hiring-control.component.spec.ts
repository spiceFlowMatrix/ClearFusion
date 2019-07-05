import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HiringControlComponent } from './hiring-control.component';

describe('HiringControlComponent', () => {
  let component: HiringControlComponent;
  let fixture: ComponentFixture<HiringControlComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HiringControlComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HiringControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
