import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewInterviewerComponent } from './add-new-interviewer.component';

describe('AddNewInterviewerComponent', () => {
  let component: AddNewInterviewerComponent;
  let fixture: ComponentFixture<AddNewInterviewerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddNewInterviewerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNewInterviewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
