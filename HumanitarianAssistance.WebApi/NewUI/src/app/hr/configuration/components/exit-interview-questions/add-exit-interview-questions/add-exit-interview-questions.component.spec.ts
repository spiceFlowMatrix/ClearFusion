import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddExitInterviewQuestionsComponent } from './add-exit-interview-questions.component';

describe('AddExitInterviewQuestionsComponent', () => {
  let component: AddExitInterviewQuestionsComponent;
  let fixture: ComponentFixture<AddExitInterviewQuestionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddExitInterviewQuestionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddExitInterviewQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
