import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneratorFiltersComponent } from './generator-filters.component';

describe('GeneratorFiltersComponent', () => {
  let component: GeneratorFiltersComponent;
  let fixture: ComponentFixture<GeneratorFiltersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeneratorFiltersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneratorFiltersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
