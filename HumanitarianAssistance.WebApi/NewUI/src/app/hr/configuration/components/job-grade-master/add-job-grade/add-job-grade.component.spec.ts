import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddJobGradeComponent } from './add-job-grade.component';

describe('AddJobGradeComponent', () => {
  let component: AddJobGradeComponent;
  let fixture: ComponentFixture<AddJobGradeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddJobGradeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddJobGradeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
