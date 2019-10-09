import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneratorTrackerComponent } from './generator-tracker.component';

describe('GeneratorTrackerComponent', () => {
  let component: GeneratorTrackerComponent;
  let fixture: ComponentFixture<GeneratorTrackerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeneratorTrackerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneratorTrackerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
