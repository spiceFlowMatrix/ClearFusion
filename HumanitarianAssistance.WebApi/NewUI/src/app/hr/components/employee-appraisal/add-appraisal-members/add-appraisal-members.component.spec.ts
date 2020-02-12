import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAppraisalMembersComponent } from './add-appraisal-members.component';

describe('AddAppraisalMembersComponent', () => {
  let component: AddAppraisalMembersComponent;
  let fixture: ComponentFixture<AddAppraisalMembersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddAppraisalMembersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAppraisalMembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
