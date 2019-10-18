import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivitiesControlComponent } from './activities-control.component';

describe('ActivitiesControlComponent', () => {
  let component: ActivitiesControlComponent;
  let fixture: ComponentFixture<ActivitiesControlComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActivitiesControlComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivitiesControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
