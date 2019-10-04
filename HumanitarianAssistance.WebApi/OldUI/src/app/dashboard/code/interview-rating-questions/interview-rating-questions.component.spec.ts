import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewRatingQuestionsComponent } from './interview-rating-questions.component';

describe('InterviewRatingQuestionsComponent', () => {
  let component: InterviewRatingQuestionsComponent;
  let fixture: ComponentFixture<InterviewRatingQuestionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterviewRatingQuestionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterviewRatingQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
