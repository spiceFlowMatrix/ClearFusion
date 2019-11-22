import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEducationDegreeComponent } from './add-education-degree.component';

describe('AddEducationDegreeComponent', () => {
  let component: AddEducationDegreeComponent;
  let fixture: ComponentFixture<AddEducationDegreeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddEducationDegreeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEducationDegreeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
