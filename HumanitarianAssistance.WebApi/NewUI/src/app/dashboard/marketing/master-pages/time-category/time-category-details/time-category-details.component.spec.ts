import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeCategoryDetailsComponent } from './time-category-details.component';

describe('TimeCategoryDetailsComponent', () => {
  let component: TimeCategoryDetailsComponent;
  let fixture: ComponentFixture<TimeCategoryDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimeCategoryDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeCategoryDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
