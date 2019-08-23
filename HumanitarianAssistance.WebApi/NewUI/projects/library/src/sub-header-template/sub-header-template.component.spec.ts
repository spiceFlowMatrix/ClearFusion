import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubHeaderTemplateComponent } from './sub-header-template.component';

describe('SubHeaderTemplateComponent', () => {
  let component: SubHeaderTemplateComponent;
  let fixture: ComponentFixture<SubHeaderTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubHeaderTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubHeaderTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
