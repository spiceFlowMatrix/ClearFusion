import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEmployeeAppraisalComponent } from './add-employee-appraisal.component';

describe('AddEmployeeAppraisalComponent', () => {
  let component: AddEmployeeAppraisalComponent;
  let fixture: ComponentFixture<AddEmployeeAppraisalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddEmployeeAppraisalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEmployeeAppraisalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
