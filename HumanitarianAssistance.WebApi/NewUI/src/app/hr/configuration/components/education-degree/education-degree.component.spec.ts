import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EducationDegreeComponent } from './education-degree.component';

describe('EducationDegreeComponent', () => {
  let component: EducationDegreeComponent;
  let fixture: ComponentFixture<EducationDegreeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EducationDegreeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EducationDegreeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
