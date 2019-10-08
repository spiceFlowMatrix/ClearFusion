import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneratorDetailsComponent } from './generator-details.component';

describe('GeneratorDetailsComponent', () => {
  let component: GeneratorDetailsComponent;
  let fixture: ComponentFixture<GeneratorDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeneratorDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeneratorDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
