import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExitInterviewQuestionsComponent } from './exit-interview-questions.component';

describe('ExitInterviewQuestionsComponent', () => {
  let component: ExitInterviewQuestionsComponent;
  let fixture: ComponentFixture<ExitInterviewQuestionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExitInterviewQuestionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExitInterviewQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
