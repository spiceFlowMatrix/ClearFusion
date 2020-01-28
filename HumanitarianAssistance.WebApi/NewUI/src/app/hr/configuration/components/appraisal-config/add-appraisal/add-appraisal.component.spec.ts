import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAppraisalComponent } from './add-appraisal.component';

describe('AddAppraisalComponent', () => {
  let component: AddAppraisalComponent;
  let fixture: ComponentFixture<AddAppraisalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddAppraisalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAppraisalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
