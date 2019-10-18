import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeCategoryComponent } from './time-category.component';

describe('TimeCategoryComponent', () => {
  let component: TimeCategoryComponent;
  let fixture: ComponentFixture<TimeCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimeCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
