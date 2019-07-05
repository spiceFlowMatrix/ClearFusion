import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivitytypeDetailsComponent } from './activitytype-details.component';

describe('ActivitytypeDetailsComponent', () => {
  let component: ActivitytypeDetailsComponent;
  let fixture: ComponentFixture<ActivitytypeDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActivitytypeDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivitytypeDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
